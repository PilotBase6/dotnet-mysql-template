        ## Descripción
Prueba técnica para Treda Solutions donde se crea una aplicación CRUD utilizando .Net 8, MySQL y ASP .NET MVC. 

El proyecto está diseñado para gestionar usuarios. Contiene dos vistas principales:
1. **Vista de Autenticación**: Permite registrar nuevos usuarios y autenticar las credenciales de los usuarios existentes.
2. **Vista de Usuarios**: Permite obtener la lista de usuarios, actualizar y eliminar.

## Requisitos

1. Docker
2. VSC
3. Extensión Remote Development

## Instalación

1. Clonar el repositorio.
2. Se debe abrir el dev container Ctrl + Shift + p => Dev Containers: Reopen Container
3. Crear y Ejecutar las migraciones (Las instrucción se encuentran en src/Infrastructure/readme.md)
4. En este punto puede probar la aplicación en un servicio consistente con los ambientes de producción
    
    ```bash
    http://localhost:8080/api/swagger
    ```
   
   O puede ejecutar la aplicación en un ambiente de desarrollo accediendo a src/Api

    ```bash
    cd src/Api
    http://localhost:5000/api/swagger
    ```

5. Para acceder a PhpMyAdmin se puede desde:

    ```bash
    http://localhost:8081
    ```

## DockerHub Container Registry

    
```bash
docker pull pilotbase6/dotnet-mysql-test
```

Recordatorio: Si quieres ejecutar la imagen del proyecto recuerda tener un servicio de MySQL corriendo en el mismo Network (Cambiar el nombre del servicio MySQL en el MYSQL_CONNECTION_STRING).

```bash
docker run -it \
--network dev-dotnet-test \
-p 5500:5500 \
-e ASPNETCORE_URLS="http://*:5500" \
-e MYSQL_CONNECTION_STRING="Server=dotnet-test-mysql;Port=3306;Database=testdb;User Id=testdotnet;Password=testdotnet;Allow User Variables=true;Default Command Timeout=0;" \
-e JWT_SECRET_KEY="6a449699-8c17-4f9b-afb7-7aa9ef475792" \
-e JWT_ISSUER="test-user-jwt" \
-e JWT_AUDIENCE="http://localhost:5500/" \
pilotbase6/dotnet-mysql-test:latest
```

En la aplicación hay dos archivos que son importantes para mi solución:

Repository.cs: Contiene una clase abstracta que contiene los métodos CRUD reutilizables, al tener un tipado generico permite que cualquier módelo lo utilice facilitando la creación de nuevos servicios y manteniendo una consistencia.

IService.cs Contiene un conjunto de interfaces que mantienen una consistencia de la manera en que se deben desarrollar los diferentes servicios, esto ayuda a mantener un orden tanto en el desarrollo del código como en las respuestas de las APIs.
