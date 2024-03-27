install_deps:
	dotnet tool install --global dotnet-ef
db_add:
	dotnet ef --project DatabaseMigrationHandler/ migrations add $(name)
db_update:
	dotnet ef --project DatabaseMigrationHandler/ database update
