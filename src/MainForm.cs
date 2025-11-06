using System.Diagnostics;
using System.Runtime.InteropServices;

namespace SleepShutdown.WinForms
{
    public enum ActionMode
    {
        Shutdown,
        Hibernate,
        MonitorOff
    }

    public partial class MainForm : Form
    {
        private DateTime? endAt;
        private bool paused = false;
        private TimeSpan remainingPaused = TimeSpan.Zero;

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);
        private const int WM_SYSCOMMAND = 0x0112;
        private const int SC_MONITORPOWER = 0xF170;

        public MainForm()
        {
            InitializeComponent();

            cboAction.SelectedIndex = 0;

            btnStart.Click += (s, e) => StartTimer();
            btnPauseResume.Click += (s, e) => TogglePause();
            btnCancel.Click += (s, e) => CancelTimer();
            lnkAbort.LinkClicked += (s, e) => AbortSystemShutdown();

            timer.Tick += (s, e) => TickCountdown();

            tray.Icon = Properties.Resources.icon;
            this.Icon = Properties.Resources.icon;

            trayMenu.Items.Add("창 열기", null, (s, e) => ShowFromTray());
            trayMenu.Items.Add("시작", null, (s, e) => StartTimer()).Name = "mStart";
            trayMenu.Items.Add("일시정지/재개", null, (s, e) => TogglePause()).Name = "mPause";
            trayMenu.Items.Add("취소", null, (s, e) => CancelTimer()).Name = "mCancel";
            trayMenu.Items.Add(new ToolStripSeparator());
            trayMenu.Items.Add("종료", null, (s, e) => { tray.Visible = false; Application.Exit(); });

            tray.ContextMenuStrip = trayMenu;
            tray.DoubleClick += (s, e) => ShowFromTray();

            FormClosing += (s, e) =>
            {
                if (e.CloseReason == CloseReason.UserClosing)
                {
                    e.Cancel = true;
                    HideToTray();
                }
            };
        }

        private void StartTimer()
        {
            if (timer.Enabled && !paused)
            {
                MessageBox.Show("이미 실행 중입니다.", "안내", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var minutes = (int)numMinutes.Value;
            if (!paused)
            {
                endAt = DateTime.Now.AddMinutes(minutes);
            }
            else
            {
                endAt = DateTime.Now + remainingPaused;
                paused = false;
            }

            timer.Start();
            btnStart.Enabled = false;
            btnPauseResume.Enabled = true;
            btnCancel.Enabled = true;
            numMinutes.Enabled = false;
            cboAction.Enabled = false;

            tray.BalloonTipTitle = "타이머 시작";
            tray.BalloonTipText = $"{GetActionLabel()} {minutes}분 뒤 실행 예정";
            tray.ShowBalloonTip(2000);
            UpdateTrayMenuState();
            UpdateCountdownLabel();
        }

        private void TogglePause()
        {
            if (!timer.Enabled && !paused) return;

            if (!paused)
            {
                if (endAt.HasValue)
                {
                    remainingPaused = endAt.Value - DateTime.Now;
                    if (remainingPaused < TimeSpan.Zero) remainingPaused = TimeSpan.Zero;
                }
                timer.Stop();
                paused = true;
                btnPauseResume.Text = "재개";
                tray.BalloonTipTitle = "일시정지";
                tray.BalloonTipText = $"남은 시간: {Format(remainingPaused)}";
                tray.ShowBalloonTip(1500);
            }
            else
            {
                paused = false;
                endAt = DateTime.Now + remainingPaused;
                timer.Start();
                btnPauseResume.Text = "일시정지";
                tray.BalloonTipTitle = "재개";
                tray.BalloonTipText = $"남은 시간: {Format(endAt.Value - DateTime.Now)}";
                tray.ShowBalloonTip(1500);
            }
            UpdateTrayMenuState();
            UpdateCountdownLabel();
        }

        private void CancelTimer()
        {
            timer.Stop();
            paused = false;
            endAt = null;
            remainingPaused = TimeSpan.Zero;
            btnStart.Enabled = true;
            btnPauseResume.Enabled = false;
            btnPauseResume.Text = "일시정지";
            btnCancel.Enabled = false;
            numMinutes.Enabled = true;
            cboAction.Enabled = true;

            lblCountdown.Text = "남은 시간: -";
            tray.BalloonTipTitle = "취소됨";
            tray.BalloonTipText = "타이머가 취소되었습니다.";
            tray.ShowBalloonTip(1500);
            UpdateTrayMenuState();
        }

        private void TickCountdown()
        {
            if (!endAt.HasValue) return;

            var remain = endAt.Value - DateTime.Now;
            if (remain <= TimeSpan.Zero)
            {
                timer.Stop();
                lblCountdown.Text = "남은 시간: 00:00:00";
                ExecuteAction();
                CancelTimer();
                return;
            }

            lblCountdown.Text = $"남은 시간: {Format(remain)}";
            tray.Text = $"SleepShutdown - {Format(remain)}";
        }

        private void ExecuteAction()
        {
            try
            {
                var mode = GetSelectedMode();
                switch (mode)
                {
                    case ActionMode.Shutdown:
                        RunShutdown();
                        break;
                    case ActionMode.Hibernate:
                        RunHibernate();
                        break;
                    case ActionMode.MonitorOff:
                        TurnOffMonitor();
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("실행 중 오류: " + ex.Message, "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RunShutdown()
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "shutdown",
                Arguments = "/s /t 0 /f",
                UseShellExecute = false,
                CreateNoWindow = true
            });
        }

        private void RunHibernate()
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "shutdown",
                Arguments = "/h",
                UseShellExecute = false,
                CreateNoWindow = true
            });
        }

        private void TurnOffMonitor()
        {
            SendMessage(this.Handle, WM_SYSCOMMAND, (IntPtr)SC_MONITORPOWER, (IntPtr)2);
        }

        private void AbortSystemShutdown()
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = "shutdown",
                    Arguments = "/a",
                    UseShellExecute = false,
                    CreateNoWindow = true
                });
                MessageBox.Show("예약된 종료가 있으면 취소됩니다.", "안내", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("취소 중 오류: " + ex.Message, "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void HideToTray()
        {
            Hide();
            tray.BalloonTipTitle = "백그라운드 동작";
            tray.BalloonTipText = "여기를 더블클릭하면 창을 다시 열 수 있습니다.";
            tray.ShowBalloonTip(1500);
        }

        private void ShowFromTray()
        {
            Show();
            WindowState = FormWindowState.Normal;
            Activate();
        }

        private void UpdateTrayMenuState()
        {
            var mStart = trayMenu.Items["mStart"];
            var mPause = trayMenu.Items["mPause"];
            var mCancel = trayMenu.Items["mCancel"];

            if (mStart != null) mStart.Enabled = btnStart.Enabled;
            if (mPause != null) mPause.Enabled = btnPauseResume.Enabled;
            if (mCancel != null) mCancel.Enabled = btnCancel.Enabled;
        }

        private void UpdateCountdownLabel()
        {
            if (paused)
                lblCountdown.Text = $"남은 시간(일시정지): {Format(remainingPaused)}";
        }

        private static string Format(TimeSpan t) =>
            $"{(int)t.TotalHours:00}:{t.Minutes:00}:{t.Seconds:00}";

        private ActionMode GetSelectedMode()
        {
            return cboAction.SelectedIndex switch
            {
                0 => ActionMode.Shutdown,
                1 => ActionMode.Hibernate,
                2 => ActionMode.MonitorOff,
                _ => ActionMode.Shutdown
            };
        }

        private string GetActionLabel() => cboAction.Text;
    }
}