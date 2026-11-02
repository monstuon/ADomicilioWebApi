# ADomicilioWebApi

Web API (ASP.NET Web API, .NET Framework) del sistema A Domicilio.

## Requisitos

- Visual Studio 2019 o superior con la carga de trabajo *ASP.NET y desarrollo web*
- .NET Framework (ver `TargetFrameworkVersion` en `ADomicilioWebApi.csproj`)
- Acceso a la base de datos SQL Server configurada en `Web.config`

## Puesta en marcha

1. Clonar el repositorio.
2. Abrir `ADomicilioWebApi.sln` en Visual Studio.
3. Restaurar los paquetes NuGet (se hace automáticamente al compilar).
4. Ajustar la cadena de conexión `ApplicationDbContext` en `Web.config` según el entorno.
5. Ejecutar con IIS Express (F5).

## Estructura

| Carpeta | Contenido |
|---|---|
| `Controllers/` | Controladores de la API |
| `Models/` | Modelos y acceso a datos |
| `MessageHandlers/` | Handlers HTTP (DelegatingHandler) |
| `App_Start/` | Configuración de rutas, Web API y bundles |
| `Areas/` | Áreas (documentación de ayuda de la API) |
