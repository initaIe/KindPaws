cd ../..

$timestamp = Get-Date -Format "yyyyMMdd_HHmmss"

$accountsMigrationName = "Accounts_$timestamp"
$volunteersMigrationName = "Volunteers_$timestamp"
$speciesMigrationName = "Species_$timestamp"

Write-Host "Starting add migrations..." -ForegroundColor Yellow
dotnet ef migrations add $accountsMigrationName -c AccountsWriteDbContext -p .\src\Accounts\KindPaws.Accounts.Infrastructure\ -s .\src\KindPaws.WEB\
dotnet ef migrations add $volunteersMigrationName -c VolunteersWriteDbContext -p .\src\Volunteers\KindPaws.Volunteers.Infrastructure\ -s .\src\KindPaws.WEB\
dotnet ef migrations add $speciesMigrationName -c SpeciesWriteDbContext -p .\src\Species\KindPaws.Species.Infrastructure\ -s .\src\KindPaws.WEB\
Write-Host "Migrations was added..." -ForegroundColor Green

Write-Host "Starting DB updated..." -ForegroundColor Yellow
dotnet ef database update -c AccountsWriteDbContext -p .\src\Accounts\KindPaws.Accounts.Infrastructure\ -s .\src\KindPaws.WEB\
dotnet ef database update -c VolunteersWriteDbContext -p .\src\Volunteers\KindPaws.Volunteers.Infrastructure\ -s .\src\KindPaws.WEB\
dotnet ef database update -c SpeciesWriteDbContext -p .\src\Species\KindPaws.Species.Infrastructure\ -s .\src\KindPaws.WEB\
Write-Host "DB was updated..." -ForegroundColor Green

pause