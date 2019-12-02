
cd /d ReportUploader
dotnet publish -r win-x64 -c Release /p:PublishSingleFile=true /p:PublishTrimmed=true
dotnet publish -r win-x64 -c Debug /p:PublishSingleFile=true /p:PublishTrimmed=true

cd /d ..\GoogleDriveUploader
dotnet publish -r win-x64 -c Release /p:PublishSingleFile=true /p:PublishTrimmed=true
dotnet publish -r win-x64 -c Debug /p:PublishSingleFile=true /p:PublishTrimmed=true