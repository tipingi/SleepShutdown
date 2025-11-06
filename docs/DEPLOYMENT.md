# 배포 파일 생성 방법

이 문서는 `SleepShutdown.WinForms` 애플리케이션의 배포 파일을 생성하는 방법을 설명합니다.

배포 파일은 단일 실행 파일(`.exe`) 형태로 생성되며, .NET 런타임이 설치되지 않은 Windows 환경에서도 바로 실행할 수 있습니다.

## 생성 절차

1. **터미널 열기**: 프로젝트의 루트 디렉터리(`E:\git\SleepShutdown.WinForms`)에서 PowerShell 또는 명령 프롬프트(CMD)를 엽니다.

2. **게시 명령 실행**: 아래의 명령어를 복사하여 터미널에 붙여넣고 실행합니다.

   ```powershell
   dotnet publish "src\SleepShutdown.WinForms.csproj" -r win-x64 --configuration Release -p:PublishSingleFile=true --self-contained true
   ```

3. **생성된 파일 확인**: 명령 실행이 완료되면, 생성된 배포 파일은 아래 경로에서 확인할 수 있습니다.

   `src\bin\Release\net8.0-windows\win-x64\publish\`

   이 폴더 안에 있는 `SleepShutdown.WinForms.exe` 파일이 바로 실행 가능한 배포 파일입니다.

## 명령어 옵션 설명

- `dotnet publish "src\SleepShutdown.WinForms.csproj"`: 지정된 프로젝트를 게시합니다.
- `-r win-x64`: 64비트 Windows를 대상 운영 체제로 지정합니다.
- `--configuration Release`: '릴리스' 모드로 빌드하여 최적화된 버전을 생성합니다.
- `-p:PublishSingleFile=true`: 모든 종속성과 런타임을 포함하는 단일 실행 파일을 생성합니다.
- `--self-contained true`: .NET 런타임을 애플리케이션에 포함시켜, 런타임이 설치되지 않은 환경에서도 실행되도록 합니다.
