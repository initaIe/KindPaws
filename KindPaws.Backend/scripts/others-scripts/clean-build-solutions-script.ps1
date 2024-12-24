cd ../..

dotnet clean

dotnet restore

dotnet build ./src/Shared/KindPaws.SharedKernel/ --configuration Release --no-restore
dotnet build ./src/Shared/KindPaws.Core/ --configuration Release --no-restore
dotnet build ./src/Shared/KindPaws.Framewowk/ --configuration Release --no-restore

dotnet build ./src/Services/AuthService/KindPaws.Auth.Application --configuration Release --no-restore
dotnet build ./src/Services/AuthService/KindPaws.Auth.Contracts --configuration Release --no-restore
dotnet build ./src/Services/AuthService/KindPaws.Auth.Domain --configuration Release --no-restore
dotnet build ./src/Services/AuthService/KindPaws.Auth.Infrastructure --configuration Release --no-restore
dotnet build ./src/Services/AuthService/KindPaws.Auth.Presentation --configuration Release --no-restore

dotnet build ./src/Services/PetService/KindPaws.Pets.Application --configuration Release --no-restore
dotnet build ./src/Services/PetService/KindPaws.Pets.Contracts --configuration Release --no-restore
dotnet build ./src/Services/PetService/KindPaws.Pets.Domain --configuration Release --no-restore
dotnet build ./src/Services/PetService/KindPaws.Pets.Infrastructure --configuration Release --no-restore
dotnet build ./src/Services/PetService/KindPaws.Pets.Presentation --configuration Release --no-restore


dotnet build ./src/Services/UserService/KindPaws.Users.Application --configuration Release --no-restore
dotnet build ./src/Services/UserService/KindPaws.Users.Contracts --configuration Release --no-restore
dotnet build ./src/Services/UserService/KindPaws.Users.Domain --configuration Release --no-restore
dotnet build ./src/Services/UserService/KindPaws.Users.Infrastructure --configuration Release --no-restore
dotnet build ./src/Services/UserService/KindPaws.Users.Presentation --configuration Release --no-restore

pause