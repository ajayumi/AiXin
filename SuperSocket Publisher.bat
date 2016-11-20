@echo off
@echo 应用程序发布脚本 Copyright ajayumi 2016

@echo 请确认 Thirdparty 目录下是否存在 SuperSocket 和 dynamicviewmodel 文件夹
@echo 如若没有，请将 Thirdparty 目录下的文件分别解压到相应的文件夹中
@echo.
@echo 请自行到 SuperSocket 文件夹中，执行 Build.bat，BuildServerManager.bat 脚本
@echo.

set JsonNetDir=%~dp0References\Newtonsoft.Json
set JsonNetVer01=9.0.1
set JsonNetVer02=5.0.6

@echo. 下载第三方组件库
"%~dp0nuget.exe" Install Newtonsoft.Json -Version %JsonNetVer01% -OutputDirectory "%JsonNetDir%"
"%~dp0nuget.exe" Install Newtonsoft.Json -Version %JsonNetVer02% -OutputDirectory "%JsonNetDir%"

xcopy "%JsonNetDir%\Newtonsoft.Json.%JsonNetVer01%\lib" "Thirdparty\SuperSocket\Reference\Json.NET" /S /E /I /Y

cd /d "%~dp0Thirdparty\supersocket"
cmd /c "Build.bat"
cmd /c "BuildServerManager.bat"

cd ../../

set Rebuild=Debug

@echo.
@echo 发布 SuperSocket Server 动态库
@echo -----------------------------------------------------------------

set OutputDir=References\SuperSocket
mkdir "%OutputDir%"

xcopy "Thirdparty\SuperSocket\bin\Net40\%Rebuild%" "%OutputDir%" /S /E /I /Y


@echo.
@echo 发布 AgentClient.WPF
@echo -----------------------------------------------------------------

xcopy "%JsonNetDir%\Newtonsoft.Json.%JsonNetVer02%\lib" "Thirdparty\SuperSocket\Reference\Json.NET" /S /E /I /Y

set msbuild="C:\Program Files (x86)\MSBuild\14.0\Bin\msbuild.exe"
%msbuild% %~dp0Thirdparty\SuperSocket\Management\AgentClient.WPF\SuperSocket.ServerManager.Client.WPF.csproj /p:Configuration=Release /t:Clean;Rebuild

xcopy "Thirdparty\supersocket\Management\AgentClient.WPF\bin\Release" "Dependencies\ServerManagement" /S /E /I /Y

@echo 发布成功！
@pause
