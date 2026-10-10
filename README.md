# Sistema de Gestión del Club Deportivo

Proyecto académico en C# Windows Forms y MySQL para administrar socios, cuotas y vencimientos.

## Funcionalidades
- Inicio de sesión.
- Alta y consulta de socios.
- Registro de pagos de cuotas.
- Consulta de cuotas vencidas o próximas a vencer.
- Base de datos MySQL con script de creación.

## Requisitos
- Windows.
- Visual Studio 2022 o posterior con desarrollo de escritorio .NET.
- .NET 8 SDK.
- MySQL Server 8 o compatible.

## Cómo ejecutar
1. Crear la base de datos ejecutando `database/club_deportivo.sql` en MySQL.
2. Abrir `CLUB_DEPORTIVO.sln` o el archivo `src/ClubDeportivo/ClubDeportivo.csproj` en Visual Studio.
3. Ajustar la cadena de conexión en `src/ClubDeportivo/Infraestructura/Conexion.cs`.
4. Restaurar paquetes NuGet y ejecutar el proyecto.

## Acceso inicial
El script crea un usuario de demostración: **admin / admin123**. Cambiar la contraseña antes de usar datos reales.

## Estado de la entrega
La estructura y el código se preparan para la consigna académica. Antes de entregar, es obligatorio compilar y probar en Visual Studio con una instancia real de MySQL. La conexión, el funcionamiento local y las capturas de pruebas no deben considerarse verificados hasta completar esos pasos.

## Estructura
- `src/ClubDeportivo`: aplicación Windows Forms.
- `database`: script SQL.
- `docs`: pruebas y notas de entrega.
