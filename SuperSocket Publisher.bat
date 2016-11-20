@echo off
@echo Ӧ�ó��򷢲��ű� Copyright ajayumi 2016

@echo ��ȷ�� Thirdparty Ŀ¼���Ƿ���� SuperSocket �� dynamicviewmodel �ļ���
@echo ����û�У��뽫 Thirdparty Ŀ¼�µ��ļ��ֱ��ѹ����Ӧ���ļ�����
@echo.
@echo �����е� SuperSocket �ļ����У�ִ�� Build.bat��BuildServerManager.bat �ű�
@echo.

set JsonNetDir=%~dp0References\Newtonsoft.Json
set JsonNetVer01=9.0.1
set JsonNetVer02=5.0.6

@echo. ���ص����������
"%~dp0nuget.exe" Install Newtonsoft.Json -Version %JsonNetVer01% -OutputDirectory "%JsonNetDir%"
"%~dp0nuget.exe" Install Newtonsoft.Json -Version %JsonNetVer02% -OutputDirectory "%JsonNetDir%"

xcopy "%JsonNetDir%\Newtonsoft.Json.%JsonNetVer01%\lib" "Thirdparty\SuperSocket\Reference\Json.NET" /S /E /I /Y

cd /d "%~dp0Thirdparty\supersocket"
cmd /c "Build.bat"
cmd /c "BuildServerManager.bat"

cd ../../

set Rebuild=Debug

@echo.
@echo ���� SuperSocket Server ��̬��
@echo -----------------------------------------------------------------

set OutputDir=References\SuperSocket
mkdir "%OutputDir%"

xcopy "Thirdparty\SuperSocket\bin\Net40\%Rebuild%" "%OutputDir%" /S /E /I /Y


@echo.
@echo ���� AgentClient.WPF
@echo -----------------------------------------------------------------

xcopy "%JsonNetDir%\Newtonsoft.Json.%JsonNetVer02%\lib" "Thirdparty\SuperSocket\Reference\Json.NET" /S /E /I /Y

set msbuild="C:\Program Files (x86)\MSBuild\14.0\Bin\msbuild.exe"
%msbuild% %~dp0Thirdparty\SuperSocket\Management\AgentClient.WPF\SuperSocket.ServerManager.Client.WPF.csproj /p:Configuration=Release /t:Clean;Rebuild

xcopy "Thirdparty\supersocket\Management\AgentClient.WPF\bin\Release" "Dependencies\ServerManagement" /S /E /I /Y

@echo �����ɹ���
@pause
