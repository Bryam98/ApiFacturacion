# ApiFacturacion
Api rest para llevar control de ventas y facturación.


#1 Para ejecutar la Api debe tener Instalado .NET 5 SDK

#2 La api rest usa como motor de base datos LocalDB, asi que para ejecutarlo debe tener instalado LocalDB, y cambiar la cadena de conexion
en al archivo appsettings.Development.Json.

#3 abra visual studio ejecute en la consola de administrador de paquetes nuget el comando:

Update-database

#4 Ejecutar el Siguiente trigger para la actulizacion de la tasa de cambio en el tabla producto al hacer
un Update en la tabla Tasa de Cambio.

CREATE TRIGGER Trigger_actulizarDolar
ON TasaCambio FOR UPDATE 
AS
declare @IdProducto int, @IdTasa int, @PrecioC decimal(10,2), @PrecioCambio decimal(6,4), @PrecioD decimal(10,2)
Select @IdProducto = p.IdProducto, @IdTasa = t.IdTasa, @PrecioC = p.PrecioCordoba, @PrecioCambio = t.PrecioCambio, @PrecioD = p.PrecioDolar
from inserted t Inner join Producto p on t.IdTasa = P.IdTasa
BEGIN
update Producto set PrecioDolar = @PrecioC / @PrecioCambio where IdProducto = @IdProducto
END


