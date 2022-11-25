-------------------------------------------- EJECUTAR ESTO -----------------------------------------------------------
create database Albatros

use albatros

create table Impuestos(
	ID int identity primary key not null,
	Impuesto float not null
);

create table Productos(
	Codigo int primary key,
	Descripcion varchar(50) not null,
	Precio float not null, 
	FK_ISV int foreign key references Impuestos(ID)
);

create table Inventario(
CodigoProducto int primary key foreign key references Productos(Codigo) not null,
Cantidad float not null
);


create table Clientes(
	ID int identity primary key,
	Nombre varchar(50) not null,
	Sexo varchar(10) not null,
	RTN varchar(13) not null,
	Direccion varchar(200) not null,
);

create table Factura(
	ID int identity primary key not null,
	Fecha date not null,
	TotalImpuesto float not null,
	Total float,
	CodigoCliente int foreign key references Clientes(ID)	
);

CREATE table DetalleFactura(
	ID int identity primary key not null,	
	Cantidad float not null,		
	CodigoProducto int foreign key references Productos(Codigo),	
	CodigoFact int foreign key references Factura(ID),
);


insert into Impuestos VALUES (8)
insert into Impuestos VALUES (15)
insert into Impuestos VALUES (25)


insert into Productos values (100000, 'Maseca', 10, 1)
insert into Productos values (100001, 'Leche Nido', 100, 2)
insert into Productos values (100010, 'Jugo Naturas', 15, 3)
insert into Productos values (100011, 'Frijoles Naturas', 35, 1)
insert into Productos values (100100, 'Huevos', 5, 2)


insert into Inventario VALUES (100000, 10)
insert into Inventario VALUES (100001, 15)
insert into Inventario VALUES (100010, 8)
insert into Inventario VALUES (100011, 30)
insert into Inventario VALUES (100100, 60)


insert into Clientes values ('José Eduardo Reyes', 'Masculino', '1504200100088', 'Res. Palma Real')
insert into Clientes values ('Fernando Nuñez', 'Masculino', '0801199512345', 'Col. Minas de oro')