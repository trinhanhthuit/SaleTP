CREATE TABLE [dbo].[Contact](
	[ContactID] [int] IDENTITY(1,1) primary key NOT NULL,
	[Subject] [nvarchar](250) NOT NULL,
	[FullName] [nvarchar](250) NOT NULL,
	[Email] [varchar](100) NOT NULL,
	[PhoneNumber] [varchar](50) NULL,
	[Contents] [nvarchar](4000) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[Status] [int] NOT NULL,
	[SearchString] [varchar](4000) NULL
)