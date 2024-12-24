$originalDirectory = Get-Location

$scripts = @(
    ".\auth-migrations-script.ps1",
    ".\pets-migrations-script.ps1",
    ".\users-migrations-script.ps1"
)

foreach ($script in $scripts) {
    try {
        Write-Host "Starting script: $script" -ForegroundColor Green
        
        & $script
        
        if ($LASTEXITCODE -ne 0) {
            Write-Host "Error in script: $script" -ForegroundColor Red
        } else {
            Write-Host "Script $script completed successfully" -ForegroundColor Cyan
        }
    } catch {
        Write-Host "Exception in script: $script - $_" -ForegroundColor Red
    } finally {
        Set-Location $originalDirectory
        Write-Host "Returned to directory: $originalDirectory" -ForegroundColor Yellow
    }
}

pause
