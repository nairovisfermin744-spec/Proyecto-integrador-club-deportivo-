# Procesos del sistema

## Proceso 1: Iniciar sesión y abrir el menú

**Entrada:** nombre de usuario y contraseña.  
**Proceso:** el sistema consulta la tabla `usuarios` usando parámetros SQL.  
**Resultado esperado:** si las credenciales corresponden a un usuario activo, abre el menú principal; de lo contrario, muestra un aviso y permanece en el acceso.

## Proceso 2: Registrar un socio

**Entradas obligatorias:** documento, nombre y apellido. Teléfono y email son opcionales.  
**Proceso:** validar los datos obligatorios; intentar insertar el registro; la base de datos impide duplicar el documento.  
**Resultado esperado:** mostrar confirmación si se guarda, o un mensaje claro si falta información, el documento ya existe o hay un problema de conexión.

## Proceso 3: Consultar socios

**Entrada:** no requiere filtros en esta versión.  
**Proceso:** recuperar los socios ordenados por apellido y nombre.  
**Resultado esperado:** mostrar los datos básicos en una grilla de solo lectura.

## Proceso 4: Registrar una cuota

**Entradas:** documento del socio, monto y fecha de pago.  
**Proceso:** localizar el socio, validar que el monto sea positivo, registrar el pago y actualizar el vencimiento dentro de una transacción.  
**Resultado esperado:** el pago y el vencimiento se guardan juntos; si ocurre un error, la transacción se revierte.

## Proceso 5: Consultar vencimientos

**Entrada:** fecha de corte seleccionada.  
**Proceso:** consultar socios con vencimiento de cuota menor o igual a la fecha.  
**Resultado esperado:** mostrar el listado ordenado por fecha de vencimiento.

## Casos de error que se deben comprobar

- Usuario o contraseña incorrectos.
- Base de datos apagada o datos de conexión incorrectos.
- Alta sin documento, nombre o apellido.
- Documento duplicado.
- Búsqueda de documento inexistente.
- Intento de pago sin socio seleccionado.
- Monto no positivo.
- Fecha de consulta sin resultados.

## Alcance no implementado aún

El enunciado general menciona también el registro de no socios, la emisión/entrega de carnet y el cobro de cuota diaria. Estos procesos no están implementados en esta primera versión. Deben añadirse si la docente exige que el sistema completo, y no solo el sub-sistema del primer entregable, quede terminado.
