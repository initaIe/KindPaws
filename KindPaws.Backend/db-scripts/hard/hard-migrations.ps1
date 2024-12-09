cd ../..

$timestamp = Get-Date -Format "yyyyMMdd_HHmmss"
$authMigrationName = "Auth_$timestamp"
$dbContextName = "AuthWriteDbContext"
$infrastructurePath = ".\src\Services\AuthService\KindPaws.Auth.Infrastructure\"
$startupPath = ".\src\Services\AuthService\KindPaws.Auth.Presentation\"

Write-Host "Starting drop db..." -ForegroundColor Yellow
dotnet ef database drop -f -c $dbContextName -p $infrastructurePath -s $startupPath
Write-Host "DB was droped..." -ForegroundColor Green

Write-Host "Deleting all migrations..." -ForegroundColor Yellow
Remove-Item -Path .\src\Services\AuthService\KindPaws.Auth.Infrastructure\Migrations* -Force
Write-Host "All migrations were deleted..." -ForegroundColor Green

Write-Host "Starting add migrations..." -ForegroundColor Yellow
dotnet ef migrations add $authMigrationName -c $dbContextName -p $infrastructurePath -s $startupPath
Write-Host "Migrations were added..." -ForegroundColor Green

Write-Host "Starting DB update..." -ForegroundColor Yellow
dotnet ef database update -c $dbContextName -p $infrastructurePath -s $startupPath
Write-Host "DB was updated..." -ForegroundColor Green

pause
