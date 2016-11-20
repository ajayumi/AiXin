del db.sqlite

set prjName=ajayumi.AiXin.Data.Migrator
set env=Debug

src\packages\FluentMigrator.Tools.1.6.2\tools\x86\40\Migrate.exe /connection "Data Source=db.sqlite;Version=3;" /db sqlite /target src\Datas\%prjName%\bin\%env%\%prjName%.dll

copy "db.sqlite" "src\Servers\ajayumi.AiXin.Server.Host.Console\bin\%env%\"

copy "db.sqlite" "Publisher\AiXin_Server_4.6.0\"

@echo del db.sqlite

pause