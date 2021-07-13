CREATE TABLE Product(
 ProductID UNIQUEIDENTIFIER primary key,
 ProductCode varchar(20) ,
 SalePrice numeric(18, 0),
 DiscountPrice numeric(18, 0),
 CreatedBy UNIQUEIDENTIFIER,
 CreatedDate datetime,
 ModifiBy UNIQUEIDENTIFIER,
 ModifyDate datetime,
 IsHot bit,
 IsActive bit,
 ImagePath1 varchar(Max),
 ImagePath2 varchar(Max)
)
insert into Product values(NEWID(),'HIK-0001',100000,100000,null,'2021-03-15',null,'2021-03-15',1, 1, 'assets/img/portfolio/cam1.jpg','assets/img/portfolio/cam1.jpg')
insert into Product values(NEWID(),'HIK-0002',100000,100000,null,'2021-03-15',null,'2021-03-15',1, 1, 'assets/img/portfolio/cam1.jpg','assets/img/portfolio/cam1.jpg')
select * from Product
GO
CREATE TABLE ProductLang(
ProductLangID UNIQUEIDENTIFIER primary key,
ProductID UNIQUEIDENTIFIER ,
[LangID] varchar(20),
ProductName nvarchar(MAX)
)
insert into ProductLang values (newid(),'vi-VN',  ( CONVERT(uniqueidentifier,'FAECD6FE-B72F-460A-A89C-FCDF5D702D75')),'HIK VISION 1 vn')
insert into ProductLang values (newid(),'en-US', ( CONVERT(uniqueidentifier,'FAECD6FE-B72F-460A-A89C-FCDF5D702D75')),'HIK VISION 1 en')
select * from ProductLang



CREATE TABLE Category(
 CategoryID int primary key,
 CategoryCode varchar(20) ,
 CreatedBy UNIQUEIDENTIFIER NULL,
 CreatedDate datetime NULL,
 ModifiBy UNIQUEIDENTIFIER NULL,
 ModifyDate datetime NULL, 
 ParentID int NULL,
 IsActive bit
)
CREATE TABLE CategoryLang(
 CategoryLangID int primary key,
 CategoryID int ,
 LangID varchar(20),
 CategoryName nvarchar(500) NULL,

)
