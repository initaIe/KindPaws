cd ../..

$timestamp = Get-Date -Format "yyyyMMdd_HHmmss"

$speciesMigrationName = "Species_$timestamp"

Write-Host "Starting drop db..." -ForegroundColor Yellow
dotnet ef database update 0 -c SpeciesWriteDbContext -p .\src\Species\KindPaws.Species.Infrastructure\ -s .\src\KindPaws.WEB\
Write-Host "DB was droped..." -ForegroundColor Green

Write-Host "Deleting all migrations..." -ForegroundColor Yellow
Remove-Item -Path .\src\Species\KindPaws.Species.Infrastructure\Migrations\* -Force
Write-Host "All migrations were deleted..." -ForegroundColor Green

Write-Host "Starting add migrations..." -ForegroundColor Yellow
dotnet ef migrations add $speciesMigrationName -c SpeciesWriteDbContext -p .\src\Species\KindPaws.Species.Infrastructure\ -s .\src\KindPaws.WEB\
Write-Host "Migrations were added..." -ForegroundColor Green

Write-Host "Starting DB update..." -ForegroundColor Yellow
dotnet ef database update -c SpeciesWriteDbContext -p .\src\Species\KindPaws.Species.Infrastructure\ -s .\src\KindPaws.WEB\
Write-Host "DB was updated..." -ForegroundColor Green

pause
