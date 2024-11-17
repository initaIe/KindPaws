cd ../..

$timestamp = Get-Date -Format "yyyyMMdd_HHmmss"

$accountsMigrationName = "Accounts_$timestamp"

Write-Host "Starting add migrations..." -ForegroundColor Yellow
dotnet ef migrations add $accountsMigrationName -c AccountsWriteDbContext -p .\src\Accounts\KindPaws.Accounts.Infrastructure\ -s .\src\KindPaws.WEB\
Write-Host "Migrations was added..." -ForegroundColor Green

Write-Host "Starting DB updated..." -ForegroundColor Yellow
dotnet ef database update -c AccountsWriteDbContext -p .\src\Accounts\KindPaws.Accounts.Infrastructure\ -s .\src\KindPaws.WEB\
Write-Host "DB was updated..." -ForegroundColor Green

pause