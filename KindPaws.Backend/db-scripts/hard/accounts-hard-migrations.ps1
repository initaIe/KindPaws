cd ../..

$timestamp = Get-Date -Format "yyyyMMdd_HHmmss"

$accountsMigrationName = "Accounts_$timestamp"

Write-Host "Starting drop db..." -ForegroundColor Yellow
dotnet ef database update 0 -c AccountsWriteDbContext -p .\src\Accounts\KindPaws.Accounts.Infrastructure\ -s .\src\KindPaws.WEB\
Write-Host "DB was droped..." -ForegroundColor Green

Write-Host "Deleting all migrations..." -ForegroundColor Yellow
Remove-Item -Path .\src\Accounts\KindPaws.Accounts.Infrastructure\Migrations\* -Force
Write-Host "All migrations were deleted..." -ForegroundColor Green

Write-Host "Starting add migrations..." -ForegroundColor Yellow
dotnet ef migrations add $accountsMigrationName -c AccountsWriteDbContext -p .\src\Accounts\KindPaws.Accounts.Infrastructure\ -s .\src\KindPaws.WEB\
Write-Host "Migrations were added..." -ForegroundColor Green

Write-Host "Starting DB update..." -ForegroundColor Yellow
dotnet ef database update -c AccountsWriteDbContext -p .\src\Accounts\KindPaws.Accounts.Infrastructure\ -s .\src\KindPaws.WEB\
Write-Host "DB was updated..." -ForegroundColor Green

pause
