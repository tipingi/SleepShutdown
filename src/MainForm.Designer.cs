namespace SleepShutdown.WinForms;

partial class MainForm
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        numMinutes = new NumericUpDown();
        cboAction = new ComboBox();
        btnStart = new Button();
        btnPauseResume = new Button();
        btnCancel = new Button();
        lblCountdown = new Label();
        lnkAbort = new LinkLabel();
        tray = new NotifyIcon(components);
        trayMenu = new ContextMenuStrip(components);
        timer = new System.Windows.Forms.Timer(components);
        lblTime = new Label();
        lblMode = new Label();
        ((System.ComponentModel.ISupportInitialize)numMinutes).BeginInit();
        SuspendLayout();
        // 
        // numMinutes
        // 
        numMinutes.Location = new Point(368, 111);
        numMinutes.Margin = new Padding(4, 5, 4, 5);
        numMinutes.Maximum = new decimal(new int[] { 1440, 0, 0, 0 });
        numMinutes.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
        numMinutes.Name = "numMinutes";
        numMinutes.Size = new Size(150, 31);
        numMinutes.TabIndex = 0;
        numMinutes.Value = new decimal(new int[] { 90, 0, 0, 0 });
        // 
        // cboAction
        // 
        cboAction.DropDownStyle = ComboBoxStyle.DropDownList;
        cboAction.FormattingEnabled = true;
        cboAction.Items.AddRange(new object[] { "종료", "최대 절전", "모니터 끄기" });
        cboAction.Location = new Point(110, 109);
        cboAction.Margin = new Padding(4, 5, 4, 5);
        cboAction.Name = "cboAction";
        cboAction.Size = new Size(150, 33);
        cboAction.TabIndex = 2;
        // 
        // btnStart
        // 
        btnStart.Location = new Point(51, 174);
        btnStart.Margin = new Padding(4, 5, 4, 5);
        btnStart.Name = "btnStart";
        btnStart.Size = new Size(150, 50);
        btnStart.TabIndex = 3;
        btnStart.Text = "시작";
        btnStart.UseVisualStyleBackColor = true;
        // 
        // btnPauseResume
        // 
        btnPauseResume.Enabled = false;
        btnPauseResume.Location = new Point(209, 174);
        btnPauseResume.Margin = new Padding(4, 5, 4, 5);
        btnPauseResume.Name = "btnPauseResume";
        btnPauseResume.Size = new Size(150, 50);
        btnPauseResume.TabIndex = 4;
        btnPauseResume.Text = "일시정지";
        btnPauseResume.UseVisualStyleBackColor = true;
        // 
        // btnCancel
        // 
        btnCancel.Enabled = false;
        btnCancel.Location = new Point(368, 174);
        btnCancel.Margin = new Padding(4, 5, 4, 5);
        btnCancel.Name = "btnCancel";
        btnCancel.Size = new Size(150, 50);
        btnCancel.TabIndex = 5;
        btnCancel.Text = "취소";
        btnCancel.UseVisualStyleBackColor = true;
        // 
        // lblCountdown
        // 
        lblCountdown.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
        lblCountdown.Location = new Point(153, 33);
        lblCountdown.Margin = new Padding(4, 0, 4, 0);
        lblCountdown.Name = "lblCountdown";
        lblCountdown.Size = new Size(260, 50);
        lblCountdown.TabIndex = 6;
        lblCountdown.Text = "남은 시간: -";
        lblCountdown.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // lnkAbort
        // 
        lnkAbort.AutoSize = true;
        lnkAbort.Location = new Point(184, 242);
        lnkAbort.Margin = new Padding(4, 0, 4, 0);
        lnkAbort.Name = "lnkAbort";
        lnkAbort.Size = new Size(201, 25);
        lnkAbort.TabIndex = 7;
        lnkAbort.TabStop = true;
        lnkAbort.Text = "비상 탈출(shutdown /a)";
        // 
        // tray
        // 
        tray.ContextMenuStrip = trayMenu;
        tray.Text = "SleepShutdown";
        tray.Visible = true;
        // 
        // trayMenu
        // 
        trayMenu.ImageScalingSize = new Size(24, 24);
        trayMenu.Name = "trayMenu";
        trayMenu.Size = new Size(61, 4);
        // 
        // timer
        // 
        timer.Interval = 1000;
        // 
        // lblTime
        // 
        lblTime.AutoSize = true;
        lblTime.Location = new Point(309, 114);
        lblTime.Name = "lblTime";
        lblTime.Size = new Size(52, 25);
        lblTime.TabIndex = 8;
        lblTime.Text = "시간:";
        // 
        // lblMode
        // 
        lblMode.AutoSize = true;
        lblMode.Location = new Point(51, 114);
        lblMode.Name = "lblMode";
        lblMode.Size = new Size(52, 25);
        lblMode.TabIndex = 9;
        lblMode.Text = "모드:";
        // 
        // MainForm
        // 
        AutoScaleDimensions = new SizeF(10F, 25F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(587, 318);
        Controls.Add(lblMode);
        Controls.Add(lblTime);
        Controls.Add(lnkAbort);
        Controls.Add(lblCountdown);
        Controls.Add(btnCancel);
        Controls.Add(btnPauseResume);
        Controls.Add(btnStart);
        Controls.Add(cboAction);
        Controls.Add(numMinutes);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        Margin = new Padding(4, 5, 4, 5);
        MaximizeBox = false;
        MinimumSize = new Size(609, 374);
        Name = "MainForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "SleepShutdown";
        ((System.ComponentModel.ISupportInitialize)numMinutes).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    internal NumericUpDown numMinutes;
    internal ComboBox cboAction;
    internal Button btnStart;
    internal Button btnPauseResume;
    internal Button btnCancel;
    internal Label lblCountdown;
    internal LinkLabel lnkAbort;
    internal NotifyIcon tray;
    internal ContextMenuStrip trayMenu;
    internal System.Windows.Forms.Timer timer;
    private Label lblTime;
    private Label lblMode;
}
