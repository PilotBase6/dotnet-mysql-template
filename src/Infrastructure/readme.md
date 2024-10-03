## Como crear, ejecutar y revertir una migración

1. Acceder al proyecto donde se crean y ejecutan las migraciones

   ```bash
      cd src/Infrastructure
   
   ```

2. Crear la migración
   Estos comandos deben ejecutarse en Infrastructure

   ```bash
   dotnet ef migrations add dotnet_1-initial-create --context AppDbContextUpgrader --output-dir Context/Migrations

   ```

3. aplicar la migración

   1. localmente
      ```bash
      dotnet ef database update --context AppDbContextUpgrader  --connection "Server=dotnet-test-mysql;Port=3306;Database=testdb;User Id=testdotnet;Password=testdotnet;Allow User Variables=true;Default Command Timeout=0;"
      ```
4. Reversar la migraciòn

   ```bash
        dotnet ef database update --context AsteriskContextUpgrader {{Nombre de la migracion que se quiere devolver}} --connection "Server=dotnet-test-mysql;Port=3306;Database=testdb;User Id=testdotnet;Password=testdotnet;Allow User Variables=true;Default Command Timeout=0;"
   ```

   1.Ejemplo

   ```bash
       dotnet ef database update --context AppDbContextUpgrader 0 --connection "Server=dotnet-test-mysql;Port=3306;Database=testdb;User Id=testdotnet;Password=testdotnet;Allow User Variables=true;Default Command Timeout=0;"
   ```