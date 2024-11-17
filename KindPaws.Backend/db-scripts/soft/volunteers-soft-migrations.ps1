cd ../..

$timestamp = Get-Date -Format "yyyyMMdd_HHmmss"

$volunteersMigrationName = "Volunteers_$timestamp"

Write-Host "Starting add migrations..." -ForegroundColor Yellow
dotnet ef migrations add $volunteersMigrationName -c VolunteersWriteDbContext -p .\src\Volunteers\KindPaws.Volunteers.Infrastructure\ -s .\src\KindPaws.WEB\
Write-Host "Migrations was added..." -ForegroundColor Green

Write-Host "Starting DB updated..." -ForegroundColor Yellow
dotnet ef database update -c VolunteersWriteDbContext -p .\src\Volunteers\KindPaws.Volunteers.Infrastructure\ -s .\src\KindPaws.WEB\
Write-Host "DB was updated..." -ForegroundColor Green

pause