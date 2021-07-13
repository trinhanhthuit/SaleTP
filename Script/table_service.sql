CREATE TABLE [dbo].[Service](
	[ServiceID] [uniqueidentifier] NOT NULL primary key,
	[LinkCode] [varchar](20) NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiBy] [uniqueidentifier] NULL,
	[ModifyDate] [datetime] NULL,
	[IsActive] [bit] NULL,
	[ImagePath1] [varchar](max) NULL,
	[ImagePath2] [varchar](max) NULL,
	)
	GO
	--drop table [dbo].[ServiceLang]
CREATE TABLE [dbo].[ServiceLang](
	[ServiceLangID] [uniqueidentifier] NOT NULL primary key,
	[ServiceID] [uniqueidentifier]  NULL,
	[LangID] varchar(10) NULL,
	[ServiceName] nvarchar(MAX) NULL,
	[Title] nvarchar(MAX) NULL,
	[ShortContent] nvarchar(MAX) NULL,
	[LongContent] nvarchar(MAX) NULL,
	)

CREATE TABLE [dbo].[Image](
	[ImageID] [uniqueidentifier] NOT NULL primary key,
	[Path1] varchar(max) NULL,
	[Path2] varchar(max) NULL,
	[Path3] varchar(max) NULL,
	[Path4] varchar(max) NULL,
	[Path5] varchar(max) NULL,
	[Path6] varchar(max) NULL,
	[Path7] varchar(max) NULL,
	[Path8] varchar(max) NULL,
	[Path9] varchar(max) NULL,
	[Path10] varchar(max) NULL,
	[CategoryID] int NULL,
	[PathID] [uniqueidentifier] NULL,
	)

CREATE TABLE [dbo].[ImageCategory](
	[ImageCategoryID] int NOT NULL primary key,
	[ImageCategoryName] nvarchar(max) NULL,
	)

--Drop table [dbo].[Testimonial]
CREATE TABLE [dbo].[Testimonial](
[TestimonialID] int NOT NULL primary key,
[ImagePath1] varchar(max) NULL,
[ImagePath2] varchar(max) NULL,
[SortOrder] int NULL,
[IsActive] bit NULL,
)

CREATE TABLE [dbo].[TestimonialLang](
[TestimonialLangID] [uniqueidentifier] NOT NULL primary key,
[TestimonialID] int NULL,
[LangID] varchar(10) NULL,
[CustomerName] nvarchar(max) NULL,
[Position] nvarchar(max) NULL,
[Content] nvarchar(max) NULL,
[SubContent] nvarchar(max) NULL,
)

DROP TABLE[dbo].[WhyUS]

CREATE TABLE [dbo].[WhyUS](
[ID] [int] NOT NULL primary key IDENTITY(1,1),  
[CreatedBy] uniqueidentifier NULL,
[CreatedByDate] datetime NULL,
[ModifyBy] uniqueidentifier NULL,
[ModifyDate] datetime NULL,
ImagePath1 varchar(max) NULL,
ImagePath2 varchar(max) NULL,
SortOrder int NULL,
[IsActive] bit NULL
)

CREATE TABLE [dbo].[WhyUSLang](
[WhyUSLangID] [int] NOT NULL primary key IDENTITY(1,1),  
[ID] int NULL,
[LangID] varchar(10) NULL,
[Title] nvarchar(Max) NULL,
[SubContent] nvarchar(Max) NULL,
[Content] nvarchar(Max) NULL,

)
DROP TABLE[dbo].[WhyUSDetail]
CREATE TABLE [dbo].[WhyUSDetail](
[WhyUSDetailID] [int] NOT NULL primary key IDENTITY(1,1),  
[CreatedBy] uniqueidentifier NULL,
[CreatedByDate] datetime NULL,
[ModifyBy] uniqueidentifier NULL,
[ModifyDate] datetime NULL,
ImagePath1 varchar(max) NULL,
ImagePath2 varchar(max) NULL,
SortOrder int NULL,
[IsActive] bit NULL
)
CREATE TABLE [dbo].[WhyUSDetailLang](
[WhyUSDetailLangID] [int] NOT NULL primary key IDENTITY(1,1),  
[WhyUSDetailID] int NULL,
[LangID] varchar(10) NULL,
[Title] nvarchar(Max) NULL,
[SubContent] nvarchar(Max) NULL,
[Content] nvarchar(Max) NULL,
)
DROP TABLE[dbo].[Employee]
CREATE TABLE [dbo].[Employee](
[EmployeeID] [int] NOT NULL primary key IDENTITY(1,1),  
[CreatedBy] uniqueidentifier NULL,
[CreatedByDate] datetime NULL,
[ModifyBy] uniqueidentifier NULL,
[ModifyDate] datetime NULL,
ImagePath1 varchar(max) NULL,
ImagePath2 varchar(max) NULL,
LinkTwitter varchar(max) NULL,
LinkFacebook varchar(max) NULL,
LinkInstagram varchar(max) NULL,
LinkLinkedin varchar(max) NULL,
SortOrder int NULL,
[IsActive] bit NULL
)

CREATE TABLE [dbo].[EmployeeLang](
[EmployeeLangID] [int] NOT NULL primary key IDENTITY(1,1),  
[EmployeeID] int NULL,
[LangID] varchar(10) NULL,
[EmployeeName] nvarchar(200) NULL,
[Position] nvarchar(200) NULL,
[Title] nvarchar(Max) NULL,
[Content] nvarchar(Max) NULL
)

CREATE TABLE TextData(
 TextID int primary key IDENTITY(1,1),  
 [LangID] varchar(20) ,
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



	