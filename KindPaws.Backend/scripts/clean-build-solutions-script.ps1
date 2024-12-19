cd ../

dotnet clean

dotnet restore ./KindPaws.Backend.sln

dotnet build ./src/Shared/KindPaws.SharedKernel/ --configuration Release --no-restore
dotnet build ./src/Shared/KindPaws.Core/ --configuration Release --no-restore
dotnet build ./src/Shared/KindPaws.Framewowk/ --configuration Release --no-restore

dotnet build ./src/Services/AuthService/KindPaws.Auth.Application --configuration Release --no-restore
dotnet build ./src/Services/AuthService/KindPaws.Auth.Contracts --configuration Release --no-restore
dotnet build ./src/Services/AuthService/KindPaws.Auth.Domain --configuration Release --no-restore
dotnet build ./src/Services/AuthService/KindPaws.Auth.Infrastructure --configuration Release --no-restore
dotnet build ./src/Services/AuthService/KindPaws.Auth.Presentation --configuration Release --no-restore

pause