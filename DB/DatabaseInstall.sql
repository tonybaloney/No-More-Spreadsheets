USE [master]
GO
/****** Object:  Database [pricing]    Script Date: 8/27/2012 10:32:08 AM ******/
CREATE DATABASE [pricing]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'pricing', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\pricing.mdf' , SIZE = 3136KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'pricing_log', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\pricing_log.ldf' , SIZE = 832KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [pricing] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [pricing].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [pricing] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [pricing] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [pricing] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [pricing] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [pricing] SET ARITHABORT OFF 
GO
ALTER DATABASE [pricing] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [pricing] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [pricing] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [pricing] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [pricing] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [pricing] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [pricing] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [pricing] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [pricing] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [pricing] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [pricing] SET  ENABLE_BROKER 
GO
ALTER DATABASE [pricing] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [pricing] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [pricing] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [pricing] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [pricing] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [pricing] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [pricing] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [pricing] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [pricing] SET  MULTI_USER 
GO
ALTER DATABASE [pricing] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [pricing] SET DB_CHAINING OFF 
GO
ALTER DATABASE [pricing] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [pricing] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [pricing]
GO
/****** Object:  StoredProcedure [dbo].[ClearItems]    Script Date: 8/27/2012 10:32:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ClearItems]
	@QuoteId int
AS
	DELETE FROM [QuoteItems] WHERE [QuoteItems].[QuoteId] = @QuoteId;
RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[CreateQuote]    Script Date: 8/27/2012 10:32:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CreateQuote]
	@OwnerId int,
	@CustomerId NCHAR(255),
	@CustomerName NCHAR(255),
	@PricelistId int,
	@QuoteTitle NCHAR(255)
AS
	INSERT INTO [Quotes] (
		[OwnerId], 
		[CustomerId],
		[CustomerName],
		[PricelistId], 
		[Title], 
		[Status],
		[CreatedDate],
		[LastChanged]) 
		VALUES ( 
			@OwnerId, 
			@CustomerId, 
			@CustomerName,
			@PricelistId, 
			@QuoteTitle, 
			'draft',
			CURRENT_TIMESTAMP,
			CURRENT_TIMESTAMP
			);
RETURN @@IDENTITY
GO
/****** Object:  StoredProcedure [dbo].[GetAllPricelists]    Script Date: 8/27/2012 10:32:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetAllPricelists]

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM Pricelists
END

GO
/****** Object:  StoredProcedure [dbo].[GetAllQuotesForUser]    Script Date: 8/27/2012 10:32:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllQuotesForUser]
	@UserId int
AS
	SELECT * FROM Quotes WHERE OwnerId = @UserId AND Status IN ('draft','live') ; 
RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[GetAllUsers]    Script Date: 8/27/2012 10:32:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllUsers]

AS
	SELECT * FROM Users
RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[GetPricelistsForUser]    Script Date: 8/27/2012 10:32:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Anthony Shaw
-- Create date: 21-08-12
-- Description:	Get the quotes for a user
-- =============================================
CREATE PROCEDURE [dbo].[GetPricelistsForUser]
	-- Add the parameters for the stored procedure here
	@UserId int 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM Pricelists WHERE OwnerId = @UserId ;
END

GO
/****** Object:  StoredProcedure [dbo].[GetProductsAvailableToQuote]    Script Date: 8/27/2012 10:32:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetProductsAvailableToQuote]
	@QuoteId int 
AS
	DECLARE @PricelistId int
	SELECT @PricelistId = [Quotes].[PricelistId] FROM [Quotes] WHERE [Quotes].[Id] = @QuoteId 
	SELECT * FROM [PricelistItems] 
		INNER JOIN [Products] ON [PricelistItems].[ProductId] = [Products].[Id]
		 WHERE [PricelistItems].[PricelistId] = @PricelistId 
		ORDER BY [Products].[Group] ASC,[Products].[Subgroup] ASC
RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[GetQuote]    Script Date: 8/27/2012 10:32:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetQuote]
	@Id int = 0
AS
	SELECT TOP 1 * FROM  [Quotes] WHERE [Id] = @Id ;
RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[GetQuoteItems]    Script Date: 8/27/2012 10:32:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetQuoteItems]
	@QuoteId int 
AS
	SELECT 
		[QuoteItems].*, 
		([QuoteItems].[RecurringPrice]*[QuoteItems].[Quantity]) AS [TotalRecurringPrice], 
		([QuoteItems].[SetupPrice]*[QuoteItems].[Quantity]) AS [TotalSetupPrice]
	FROM [QuoteItems] 
		LEFT OUTER JOIN 
			[Products] ON [Products].[Id] = [QuoteItems].[ProductId]
		WHERE 
			[QuoteItems].[QuoteId] = @QuoteId 
		ORDER BY 
			[QuoteItems].[GroupName] ASC, 
			[QuoteItems].[Index] ASC,
			[QuoteItems].[Title] ASC
RETURN 0
GO
/****** Object:  Table [dbo].[PricelistItems]    Script Date: 8/27/2012 10:32:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PricelistItems](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PricelistId] [int] NULL,
	[ProductId] [int] NULL,
	[SetupPrice] [float] NULL,
	[RecurringPrice] [float] NULL,
	[SetupCost] [float] NULL,
	[RecurringCost] [float] NULL,
 CONSTRAINT [PK_PricelistItems] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Pricelists]    Script Date: 8/27/2012 10:32:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Pricelists](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Date] [datetime] NOT NULL,
	[IsPrivate] [bit] NOT NULL,
	[OwnerId] [int] NULL,
	[IsDefault] [bit] NOT NULL,
	[Currency] [nchar](10) NULL,
 CONSTRAINT [PK__Table__3214EC07057480B0] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Products]    Script Date: 8/27/2012 10:32:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nchar](255) NULL,
	[Description] [ntext] NULL,
	[Group] [nchar](255) NULL,
	[Partcode] [nchar](255) NULL,
	[Subgroup] [nchar](255) NULL,
	[Availability] [nchar](32) NULL,
	[InternalNotes] [ntext] NULL,
	[SizeU] [tinyint] NULL,
	[Power] [smallint] NULL,
	[Manufacturer] [nchar](255) NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[QuoteItems]    Script Date: 8/27/2012 10:32:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuoteItems](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[QuoteId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[Title] [nchar](255) NULL,
	[Description] [ntext] NULL,
	[Quantity] [smallint] NOT NULL,
	[SetupPrice] [float] NOT NULL,
	[RecurringPrice] [float] NOT NULL,
	[SetupCost] [float] NOT NULL,
	[RecurringCost] [float] NOT NULL,
	[GroupName] [nchar](255) NULL,
	[SubGroupName] [nchar](255) NULL,
	[Partcode] [nchar](255) NULL,
	[Notes] [ntext] NULL,
	[Index] [smallint] NULL,
	[IsBundle] [bit] NOT NULL,
	[IsPart] [bit] NOT NULL,
	[BundleId] [int] NULL,
 CONSTRAINT [PK_QuoteItems] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[QuoteItemsHistory]    Script Date: 8/27/2012 10:32:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuoteItemsHistory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[QuoteId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[Title] [nchar](255) NULL,
	[Description] [ntext] NULL,
	[Quantity] [smallint] NOT NULL,
	[SetupPrice] [float] NOT NULL,
	[RecurringPrice] [float] NOT NULL,
	[SetupCost] [float] NOT NULL,
	[RecurringCost] [float] NOT NULL,
	[GroupName] [nchar](255) NULL,
	[SubGroupName] [nchar](255) NULL,
	[Partcode] [nchar](255) NULL,
	[Notes] [ntext] NULL,
	[Index] [smallint] NULL,
	[IsBundle] [bit] NOT NULL,
	[IsPart] [bit] NOT NULL,
	[BundleId] [int] NULL,
	[Revision] [int] NULL,
 CONSTRAINT [PK_QuoteItemsHistory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Quotes]    Script Date: 8/27/2012 10:32:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Quotes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OwnerId] [int] NULL,
	[CustomerId] [nchar](255) NULL,
	[CustomerName] [nchar](255) NULL,
	[PricelistId] [int] NULL,
	[Title] [nchar](255) NULL,
	[Status] [nvarchar](50) NOT NULL,
	[LastChanged] [datetime] NULL,
	[DiscountPercent] [float] NOT NULL,
	[DiscountPercent24] [float] NOT NULL,
	[DiscountPercent36] [float] NOT NULL,
	[DiscountWritein] [float] NOT NULL,
	[DiscountPercentSetup] [float] NOT NULL,
	[TotalValue] [float] NOT NULL,
	[TotalSetupValue] [float] NOT NULL,
	[ClosedDate] [datetime] NULL,
	[ContractLengthMonths] [smallint] NOT NULL,
	[Revision] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK__Quotes__3214EC078CC1B559] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Users]    Script Date: 8/27/2012 10:32:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RealName] [nchar](255) NULL,
	[Email] [nchar](255) NULL,
	[Password] [nchar](255) NULL,
	[Team] [nchar](10) NULL,
	[Permissions] [nchar](10) NULL,
 CONSTRAINT [PK__Table__3214EC0700D8C066] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Pricelists] ADD  CONSTRAINT [DF__Table__Pricelist__2F10007B]  DEFAULT ((0)) FOR [IsPrivate]
GO
ALTER TABLE [dbo].[Pricelists] ADD  CONSTRAINT [DF__Table__Pricelist__300424B4]  DEFAULT ((0)) FOR [IsDefault]
GO
ALTER TABLE [dbo].[QuoteItems] ADD  CONSTRAINT [DF_QuoteItems_Quantity]  DEFAULT ((1)) FOR [Quantity]
GO
ALTER TABLE [dbo].[QuoteItems] ADD  CONSTRAINT [DF_QuoteItems_SetupPrice]  DEFAULT ((0)) FOR [SetupPrice]
GO
ALTER TABLE [dbo].[QuoteItems] ADD  CONSTRAINT [DF_QuoteItems_RecurringPrice]  DEFAULT ((0)) FOR [RecurringPrice]
GO
ALTER TABLE [dbo].[QuoteItems] ADD  CONSTRAINT [DF_QuoteItems_SetupCost]  DEFAULT ((0)) FOR [SetupCost]
GO
ALTER TABLE [dbo].[QuoteItems] ADD  CONSTRAINT [DF_QuoteItems_RecurringCost]  DEFAULT ((0)) FOR [RecurringCost]
GO
ALTER TABLE [dbo].[QuoteItems] ADD  CONSTRAINT [DF_QuoteItems_IsBundle]  DEFAULT ((0)) FOR [IsBundle]
GO
ALTER TABLE [dbo].[QuoteItems] ADD  CONSTRAINT [DF_QuoteItems_IsPart]  DEFAULT ((0)) FOR [IsPart]
GO
ALTER TABLE [dbo].[QuoteItemsHistory] ADD  CONSTRAINT [DF_QuoteItemsHistory_Quantity]  DEFAULT ((1)) FOR [Quantity]
GO
ALTER TABLE [dbo].[QuoteItemsHistory] ADD  CONSTRAINT [DF_QuoteItemsHistory_SetupPrice]  DEFAULT ((0)) FOR [SetupPrice]
GO
ALTER TABLE [dbo].[QuoteItemsHistory] ADD  CONSTRAINT [DF_QuoteItemsHistory_RecurringPrice]  DEFAULT ((0)) FOR [RecurringPrice]
GO
ALTER TABLE [dbo].[QuoteItemsHistory] ADD  CONSTRAINT [DF_QuoteItemsHistory_SetupCost]  DEFAULT ((0)) FOR [SetupCost]
GO
ALTER TABLE [dbo].[QuoteItemsHistory] ADD  CONSTRAINT [DF_QuoteItemsHistory_RecurringCost]  DEFAULT ((0)) FOR [RecurringCost]
GO
ALTER TABLE [dbo].[QuoteItemsHistory] ADD  CONSTRAINT [DF_QuoteItemsHistory_IsBundle]  DEFAULT ((0)) FOR [IsBundle]
GO
ALTER TABLE [dbo].[QuoteItemsHistory] ADD  CONSTRAINT [DF_QuoteItemsHistory_IsPart]  DEFAULT ((0)) FOR [IsPart]
GO
ALTER TABLE [dbo].[Quotes] ADD  CONSTRAINT [DF__Quotes__QuoteSta__239E4DCF]  DEFAULT ('draft') FOR [Status]
GO
ALTER TABLE [dbo].[Quotes] ADD  CONSTRAINT [DF__Quotes__QuoteDis__24927208]  DEFAULT ((0)) FOR [DiscountPercent]
GO
ALTER TABLE [dbo].[Quotes] ADD  CONSTRAINT [DF__Quotes__QuoteDis__25869641]  DEFAULT ((0)) FOR [DiscountPercent24]
GO
ALTER TABLE [dbo].[Quotes] ADD  CONSTRAINT [DF__Quotes__QuoteDis__267ABA7A]  DEFAULT ((0)) FOR [DiscountPercent36]
GO
ALTER TABLE [dbo].[Quotes] ADD  CONSTRAINT [DF__Quotes__QuoteDis__276EDEB3]  DEFAULT ((0)) FOR [DiscountWritein]
GO
ALTER TABLE [dbo].[Quotes] ADD  CONSTRAINT [DF__Quotes__QuoteDis__286302EC]  DEFAULT ((0)) FOR [DiscountPercentSetup]
GO
ALTER TABLE [dbo].[Quotes] ADD  CONSTRAINT [DF__Quotes__QuoteTot__29572725]  DEFAULT ((0)) FOR [TotalValue]
GO
ALTER TABLE [dbo].[Quotes] ADD  CONSTRAINT [DF__Quotes__QuoteTot__2A4B4B5E]  DEFAULT ((0)) FOR [TotalSetupValue]
GO
ALTER TABLE [dbo].[Quotes] ADD  CONSTRAINT [DF__Quotes__QuoteCon__2B3F6F97]  DEFAULT ((12)) FOR [ContractLengthMonths]
GO
ALTER TABLE [dbo].[Quotes] ADD  CONSTRAINT [DF__Quotes__QuoteRev__2C3393D0]  DEFAULT ((1)) FOR [Revision]
GO
USE [master]
GO
ALTER DATABASE [pricing] SET  READ_WRITE 
GO
