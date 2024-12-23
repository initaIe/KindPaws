cd ../..

$timestamp = Get-Date -Format "yyyyMMdd_HHmmss"

$Name = "Auth"

$authMigrationName = "Auth_$timestamp"
$dbContextName = "AuthWriteDbContext"
$infrastructurePath = ".\src\Services\AuthService\KindPaws.Auth.Infrastructure\"
$startupPath = ".\src\Services\AuthService\KindPaws.Auth.Presentation\"
$migrationsPath = ".\src\Services\AuthService\KindPaws.Auth.Infrastructure\Persistence\Migrations*"
$addMigrationsPath = "Persistence\Migrations"

Write-Host "Starting drop $Name db..." -ForegroundColor Yellow
dotnet ef database drop -f -c $dbContextName -p $infrastructurePath -s $startupPath
Write-Host "$Name DB was droped..." -ForegroundColor Green

Write-Host "Deleting $Name all migrations..." -ForegroundColor Yellow
Remove-Item -Path $migrationsPath -Recurse -Force
Write-Host "$Name All migrations were deleted..." -ForegroundColor Green

Write-Host "Starting add $Name migrations..." -ForegroundColor Yellow
dotnet ef migrations add $authMigrationName -c $dbContextName -p $infrastructurePath -s $startupPath -o $addMigrationsPath
Write-Host "$Name Migrations were added..." -ForegroundColor Green

Write-Host "Starting $Name DB update..." -ForegroundColor Yellow
dotnet ef database update -c $dbContextName -p $infrastructurePath -s $startupPath
Write-Host "$Name DB was updated..." -ForegroundColor Green
