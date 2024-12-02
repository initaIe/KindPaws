# Очистка локальных NuGet-кэшей
Write-Host "Listing all NuGet cache locations..."
dotnet nuget locals --list all

# Укажите версии для обновления
$packageVersions = @{
    "KindPaws.SharedKernel" = "1.1.3"
    "KindPaws.Core" = "1.0.8"
    "KindPaws.Framework" = "1.1.0"
}

# Настройка NuGet аутентификации
$nugetSource = "https://nuget.pkg.github.com/initaIe/index.json"  # Замените <owner> на имя вашей организации или пользователя
$nugetUsername = "initaIe"  # Ваш GitHub username
$nugetToken = "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"  # Ваш Personal Access Token

# Проверить, существует ли источник "github"
if (!(dotnet nuget list source | Select-String -Pattern "github")) {
    Write-Host "Adding GitHub Packages as a NuGet source"
    dotnet nuget add source `
        --name "github" `
        --username $nugetUsername `
        --password $nugetToken `
        --store-password-in-clear-text `
        $nugetSource
} else {
    Write-Host "NuGet source 'github' already exists. Skipping addition."
}

# Найти и обновить все проекты, где установлены целевые пакеты
Get-ChildItem -Recurse -Filter *.csproj | ForEach-Object {
    $csproj = $_.FullName

    # Обновить каждый из указанных пакетов
    foreach ($packageName in $packageVersions.Keys) {
        if (Select-String -Path $csproj -Pattern "<PackageReference Include=`"$packageName`"" -Quiet) {
            Write-Host "Updating $packageName in $csproj to version $($packageVersions[$packageName])"
            dotnet add $csproj package $packageName --version $($packageVersions[$packageName])
        } else {
            Write-Host "Skipping $csproj ($packageName not found)"
        }
    }
}

pause