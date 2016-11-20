
set prjName=ajayumi.AiXin.Data.Migrator

@echo src\packages\FluentMigrator.Tools.1.6.2\tools\AnyCPU\40\Migrate.exe /connection "Data Source=12.112.2.88;port=12012;Initial Catalog=ajTorrentStore;user id=ajTorrentStore;password=ajTorrentStore;CharSet=utf8;" /db mysql /target src\Data\%prjName%\bin\Debug\%prjName%.dll

pause