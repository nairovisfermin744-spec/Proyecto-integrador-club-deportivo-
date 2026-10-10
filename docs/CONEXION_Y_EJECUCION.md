# Conexión y ejecución local

1. Instalar MySQL Server y Visual Studio con la carga de trabajo **Desarrollo de escritorio de .NET**.
2. Ejecutar el script `database/club_deportivo.sql`.
3. Abrir `ClubDeportivo.sln`.
4. En Visual Studio, restaurar los paquetes NuGet.
5. Editar `src/ClubDeportivo/Infraestructura/Conexion.cs` para colocar servidor, puerto, base, usuario y contraseña correctos.
6. Compilar y ejecutar.
7. Iniciar sesión con `admin / admin123` solo en el entorno local de prueba.

## Si falla la conexión

- Confirmar que el servicio MySQL esté iniciado.
- Confirmar puerto, usuario, contraseña y nombre de la base.
- Verificar que el script SQL se haya ejecutado sin errores.
- No publicar contraseñas reales en el repositorio.

## Limitación importante

La aplicación se creó usando controles WinForms por código para evitar depender de archivos Designer. En este entorno no fue posible compilarla ni conectarla a un servidor MySQL real. Por eso, el estado de compilación y las pruebas de ejecución deben completarse en Visual Studio antes de la entrega.
