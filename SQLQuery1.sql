use PayBlockDB

create table dbo.Locatii(
IdLocatii int identity(1,1) NOT NULL PRIMARY KEY,
IdUser int NOT NULL FOREIGN KEY REFERENCES Utilizator(ID),
judet varchar(30),
oras varchar(30),
strada varchar(60),
bloc varchar(10),
nrlocatari int,
nrapometre int
);

ALTER TABLE Utilizator
ADD CNP varchar(13);



create table dbo.Judet(
IDJudet int NOT NULL PRIMARY KEY,
NumeJudet varchar(30)
);

insert into Oras values(1,'Timisoara');
select * from Oras

insert into Utilizator values(1,'anavlad@yahoo.com','123','Ana','Vlad','0770825264','6000913204960');
select * from Utilizator

create table dbo.Oras(
IDOras int NOT NULL PRIMARY KEY,
NumeOras varchar(30)
);

create table dbo.Strada(
IDStrada int NOT NULL PRIMARY KEY,
NumeStrada varchar(30)
);

create table dbo.Bloc(
IDBloc int NOT NULL PRIMARY KEY,
NumarBloc int
);


create table dbo.RaportApometre(
IDApo int NOT NULL PRIMARY KEY FOREIGN KEY REFERENCES Apometre(IDApometru),
cantitate varchar(30),
dataApo varchar(30)
);


create table dbo.Facturi(
IDUser int NOT NULL PRIMARY KEY,
IDLoc int NOT NULL FOREIGN KEY REFERENCES Locatii(IDLocatii),
totalRece varchar(30),
totalRetim varchar(30),
administrator varchar(20),
curatenie varchar(15),
impozit varchar(15),
dataPlata varchar(30),
totalPlata varchar(50),
);

create table dbo.Tarife(
IDTarife int NOT NULL PRIMARY KEY,
Luna varchar(30),
An varchar(30),
IDLocatie int NOT NULL FOREIGN KEY REFERENCES Locatii(IDLocatii),
pretApa varchar(20),
pretRetim varchar(20),
pretAdministrator varchar(20),
pretCuratenie varchar(20),
pretImpozit varchar(30)
);

create table dbo.Locatii(
IdLocatii int identity(1,1) NOT NULL PRIMARY KEY,
IdUser int NOT NULL FOREIGN KEY REFERENCES Utilizator(ID),
judet varchar(30),
oras varchar(30),
strada varchar(60),
bloc varchar(10),
apartament int,
nrlocatari int,
nrapometre int
);

drop table Apometre

create table dbo.Apartament(
IDApartament int identity(1,1) NOT NULL PRIMARY KEY,
NumarApartament int
);

use PayDB

select * from Apometre

DELETE FROM Apometre WHERE IdUser=1011;

SELECT * FROM Utilizator WHERE ID = 1011; 

insert into Apometre values(4, '222','234','0','0','2023-06-03');

create table dbo.Apometre(
IdApometre int identity(1,1) NOT NULL PRIMARY KEY,
Iduser int NOT NULL FOREIGN KEY REFERENCES Utilizator(ID),
ap1 varchar(10),
ap2 varchar(10),
ap3 varchar(10),
ap4 varchar(10),
TransmitereData date
);

create table dbo.Tarife(
IdTarife int identity(1,1) NOT NULL PRIMARY KEY,
Id int NOT NULL FOREIGN KEY REFERENCES Utilizator(ID),
pret_retim varchar(10),
pret_curatenie varchar(10),
pret_admin varchar(10),
pret_apa varchar(10),
TransmitereData date
);

select * from Apometre
insert into Apometre values(1011, '324','329','0','0','2023-06-03');

DELETE FROM Apometre WHERE IdApometre=3;

create table dbo.Factura(
IdFactura int identity(1,1) NOT NULL PRIMARY KEY,
Id_utilizator int NOT NULL FOREIGN KEY REFERENCES Utilizator(ID),
TotalApa decimal(20),
TotalRetim decimal(20),
TotalAdmin decimal(20),
TotalCuratenie decimal(20),
ApaUsage decimal(20),
RetimUsage decimal(20),
AdminUsage decimal(20),
CuratenieUsage decimal(20),
TransmitereData date
);

drop table Factura