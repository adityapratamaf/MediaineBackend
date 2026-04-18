dotnet ef migrations add FirstMigration \
--project Mediaine.Infrastructure \
--startup-project Mediaine.API

dotnet ef database update \
--project Mediaine.Infrastructure \
--startup-project Mediaine.API