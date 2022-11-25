


-------------------------------------------- NO EJECUTAR ESTO -----------------------------------------------------------


-------------------------------------------- Detalle Factura -----------------------------------------------------------
select c.Codigo, c.Descripcion, b.Cantidad, c.Precio, d.Impuesto, SUM((d.Impuesto / 100)* c.Precio)[Impuesto_Producto] ,SUM(b.Cantidad * c.Precio)[Total] from Factura a inner join DetalleFactura b
ON a.ID = b.CodigoFact inner join Productos c
ON b.CodigoProducto = c.Codigo inner join Impuestos d
ON d.ID = c.FK_ISV
where b.CodigoFact = 8
group by b.ID, c.Codigo ,c.Descripcion, b.Cantidad, c.Precio, d.Impuesto

-------------------------------------------- Detalle Factura Con cliente Para el reporte -----------------------------------------------------------
select e.Nombre, e.Direccion,e.RTN, c.Codigo, c.Descripcion, b.Cantidad, c.Precio, d.Impuesto, SUM((d.Impuesto / 100)* c.Precio)[Impuesto_Producto] ,SUM(b.Cantidad * c.Precio)[Total] from Factura a inner join DetalleFactura b
ON a.ID = b.CodigoFact inner join Productos c
ON b.CodigoProducto = c.Codigo inner join Impuestos d
ON d.ID = c.FK_ISV inner join Clientes e
ON e.ID = a.CodigoCliente
where a.ID = 1
group by e.Nombre, e.Direccion,e.RTN, c.Codigo, c.Descripcion, b.Cantidad, c.Precio, d.Impuesto
-------------------------------------------- Productos -----------------------------------------------------------
select a.Codigo, a.Descripcion, a.Precio, b.Impuesto from Productos a inner join Impuestos b
ON a.FK_ISV = b.ID

-------------------------------------------- Detalle Factura -----------------------------------------------------------
select c.Codigo ,c.Descripcion, b.Cantidad, c.Precio, d.Impuesto, SUM((d.Impuesto / 100)* c.Precio)[Impuesto_Producto] ,SUM(b.Cantidad * c.Precio)[Total] from Factura a inner join DetalleFactura b
ON a.ID = b.CodigoFact inner join Productos c
ON b.CodigoProducto = c.Codigo inner join Impuestos d
ON d.ID = c.FK_ISV
where b.CodigoFact = 8
group by b.ID, c.Codigo ,c.Descripcion, b.Cantidad, c.Precio, d.Impuesto


-------------------------------------------- Detallte Factura Impuesto Total -----------------------------------------------------------
select SUM((d.Impuesto / 100)* c.Precio)[Impuesto_Producto] from Factura a inner join DetalleFactura b
ON a.ID = b.CodigoFact inner join Productos c
ON b.CodigoProducto = c.Codigo inner join Impuestos d
ON d.ID = c.FK_ISV
where b.CodigoFact = 8

-------------------------------------------- Detallte Factura Impuesto Total -----------------------------------------------------------
select SUM(b.Cantidad * c.Precio)[Total_Factura] from Factura a inner join DetalleFactura b
ON a.ID = b.CodigoFact inner join Productos c
ON b.CodigoProducto = c.Codigo inner join Impuestos d
ON d.ID = c.FK_ISV
where b.CodigoFact = 8

------------------------------------- Obtener Ultimo ID de la factura creada -----------------------------------
select top 1 ID from Factura order by ID DESC

------------------------------------- Select inventario -----------------------------------

select a.CodigoProducto, b.Descripcion, a.Cantidad, b.Precio, c.Impuesto from Inventario a inner join Productos b
ON a.CodigoProducto = b.Codigo inner join Impuestos c
ON b.FK_ISV = c.ID

------------------------------------- Facturas  -----------------------------------

Select a.ID, b.Nombre, a.TotalImpuesto, a.Total from Factura a inner join Clientes b
ON a.CodigoCliente = b.ID


Select a.Codigo, a.Descripcion, a.Precio, b.Impuesto from Productos a inner join Impuestos b on a.FK_ISV = b.ID

select * from Factura

delete from Productos where Codigo = 100000
insert into DetalleFactura VALUES (5 , '100000', 12)

create procedure InsertDetalleFactura 
@Cantidad int,
@CodigoProducto varchar(6), 
@CodigoFact int
as
begin 
	insert into DetalleFactura Values(@Cantidad, @CodigoProducto, @CodigoFact)
end

exec InsertDetalleFactura 5, '100000', 12


select * from Productos

select TOP 5 b.Descripcion, a.Cantidad from Inventario a inner join Productos b
On a.CodigoProducto = b.Codigo
order by CodigoProducto DESC

insert into Inventario VALUES (155555, 35)


------------------------ Datos para el inventario Inventario ------------------------
insert into Inventario VALUES (155555, 35)
insert into Inventario VALUES (155550, 25)
insert into Inventario VALUES (100000, 15)

------------------------ Datos Cliente ------------------------
insert into Clientes(Nombre, RTN, Direccion) VALUES ('José Eduardo Reyes', '1504200100088', 'Col.Minitas')

select * from Productos where Descripcion like '%Maseca%'

