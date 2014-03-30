SMShound - Capturador de SMS en sitios Web públicos
================================================
================================================
Configuración
================================================

La aplicación se puede configurar desde SMShound.exe.config definiéndose los parámetros:

- SegundosEntreCiclos
Es el número de segundos que tienen que pasar entre las consultas que se hagan a los distintos servicios Web

- FicheroSalida
Nombre del fichero CSV donde se escribirán los SMS recuperados

- FicheroError
Nombre del fichero donde se escribirán los errores capturados

- FicheroEnlaces
Nombre del fichero que contiene los enlaces a diferentes páginas Web con SMS públicos

- MaximoMensajesRepetidosHastaFin
Valor establecido para calcular cuando se están repitiendo SMS y detener su descarga

- FicheroPalabrasClave
Nombre del fichero donde se esncuentran las palabras clave que se buscarán en los mensajes y se notificarán
por correo electrónico. No es necesario que haya palabras clave definidas.

- SMTPservidor, SMTPpuerto, SMTPusarSSL, SMTPusuario, SMTPclave
Datos del proveedor de correo para enviar correos electrónicos cuando se encuentre una palabra clave



================================================
================================================
Agregar nuevos servicios de mensajería
================================================

Si se quiere agregar un nuevo servicio de SMS habrá que:

1. En el método ManejadorServiciosSMS.ObtenerEntradas(...), agregar un nuevo IF...ELSE para identificar el nuevo dominio
2. Agregar en esa misma clase un nuevo método para gestionar esa captura basándose en los métodos ya desarrollados
