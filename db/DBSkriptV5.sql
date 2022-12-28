SET NOCOUNT ON
GO
USE master
go
if exists(select * from sysdatabases where name = 'db_Gainzbourg')
Begin
	Raiserror('Datenbank exisrte bereits wurde gelöscht',0,1)
	ALTER DATABASE db_Gainzbourg set single_user with rollback immediate
	drop database db_Gainzbourg
end
go
create database db_Gainzbourg
go
use db_GainzBourg
go
create table tbl_Trainer(
	id int IDENTITY (1, 1) NOT NULL,
	benutzername nvarchar(10) not null,
	vorname nvarchar(32),
	nachname nvarchar(32),
	passwort nchar(64) not null,
	headcoach binary constraint df_headcoach default(0) not null,
	deleted bit constraint df_delted default(0) not null,

	constraint pk_Trainer primary key(id)
);
go
create table tbl_Mitglied(
	id int IDENTITY (1, 1) NOT NULL ,
	trainerid int,
	vorname nvarchar(32),
	nachname nvarchar(32),
	AHV nchar(13),
	mitgliedschaftAnfang date,
	mitgliedschaftEnde date,
	gebursdatum date,
	mail nvarchar(32),

	constraint pk_Mitglied primary key(id),
	constraint fk_Trainer foreign key (trainerId) references tbl_Trainer(id)


);
go
create table tbl_Gruppenkurs(
	id int IDENTITY (1, 1) NOT NULL ,
	trainerId int,
	bezeichnung nvarchar(32),
	beginn datetime,
	ende datetime,
	beschreibung text,

	constraint pk_Gruppenkurs primary key(id),
	constraint fk_Gruppenkurs_Trainer foreign key (trainerId) references tbl_Trainer(id)
	
);
go
create table tbl_Ausruestung(
	artNr int NOT NULL ,
	bezeichnung nvarchar(32),
	gewichtKg smallint,
	anzahl tinyint

	constraint pk_Ausruestung primary key(artNr)
);
go



CREATE TRIGGER trg_OnDeleteTrainer
    ON tbl_Trainer
    instead of delete
AS
  update tbl_Trainer
  set deleted = 1
  where id = (select id from deleted) 
  
  delete tbl_Gruppenkurs
  where trainerId = (select id from deleted)
GO
delete from tbl_Trainer where 1 = 1
go
insert into tbl_Trainer(benutzername,vorname,nachname,passwort,headcoach)
values
('admin','Aron','Baur','8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918',1)
