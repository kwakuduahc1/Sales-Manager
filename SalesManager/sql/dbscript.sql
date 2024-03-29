USE [bs_stores]
GO
IF  EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_DiagramPaneCount' , N'SCHEMA',N'dbo', N'VIEW',N'vwItemPrices', NULL,NULL))
EXEC sys.sp_dropextendedproperty @name=N'MS_DiagramPaneCount' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vwItemPrices'
GO
IF  EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_DiagramPane1' , N'SCHEMA',N'dbo', N'VIEW',N'vwItemPrices', NULL,NULL))
EXEC sys.sp_dropextendedproperty @name=N'MS_DiagramPane1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vwItemPrices'
GO
/****** Object:  StoredProcedure [dbo].[spSalesLedger]    Script Date: 01-Feb-24 09:03:43 ******/
DROP PROCEDURE IF EXISTS [dbo].[spSalesLedger]
GO
/****** Object:  StoredProcedure [dbo].[spReceipts]    Script Date: 01-Feb-24 09:03:43 ******/
DROP PROCEDURE IF EXISTS [dbo].[spReceipts]
GO
/****** Object:  StoredProcedure [dbo].[spItemsToSell]    Script Date: 01-Feb-24 09:03:43 ******/
DROP PROCEDURE IF EXISTS [dbo].[spItemsToSell]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Units]') AND type in (N'U'))
ALTER TABLE [dbo].[Units] DROP CONSTRAINT IF EXISTS [FK_Units_Items_ItemsID]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SupplierPayments]') AND type in (N'U'))
ALTER TABLE [dbo].[SupplierPayments] DROP CONSTRAINT IF EXISTS [FK_SupplierPayments_Suppliers_SuppliersID]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SupplierPayments]') AND type in (N'U'))
ALTER TABLE [dbo].[SupplierPayments] DROP CONSTRAINT IF EXISTS [FK_SupplierPayments_PaymentTypes_PaymentTypesID]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Stockings]') AND type in (N'U'))
ALTER TABLE [dbo].[Stockings] DROP CONSTRAINT IF EXISTS [FK_Stockings_Suppliers_SuppliersID]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Stockings]') AND type in (N'U'))
ALTER TABLE [dbo].[Stockings] DROP CONSTRAINT IF EXISTS [FK_Stockings_Items_ItemsID]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Sales]') AND type in (N'U'))
ALTER TABLE [dbo].[Sales] DROP CONSTRAINT IF EXISTS [FK_Sales_Prices_PricesID]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Sales]') AND type in (N'U'))
ALTER TABLE [dbo].[Sales] DROP CONSTRAINT IF EXISTS [FK_Sales_Payments_Receipt]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Sales]') AND type in (N'U'))
ALTER TABLE [dbo].[Sales] DROP CONSTRAINT IF EXISTS [FK_Sales_Items_ItemsID]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Prices]') AND type in (N'U'))
ALTER TABLE [dbo].[Prices] DROP CONSTRAINT IF EXISTS [FK_Prices_Units_UnitsID]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AspNetUserTokens]') AND type in (N'U'))
ALTER TABLE [dbo].[AspNetUserTokens] DROP CONSTRAINT IF EXISTS [FK_AspNetUserTokens_AspNetUsers_UserId]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AspNetUserRoles]') AND type in (N'U'))
ALTER TABLE [dbo].[AspNetUserRoles] DROP CONSTRAINT IF EXISTS [FK_AspNetUserRoles_AspNetUsers_UserId]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AspNetUserRoles]') AND type in (N'U'))
ALTER TABLE [dbo].[AspNetUserRoles] DROP CONSTRAINT IF EXISTS [FK_AspNetUserRoles_AspNetRoles_RoleId]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AspNetUserLogins]') AND type in (N'U'))
ALTER TABLE [dbo].[AspNetUserLogins] DROP CONSTRAINT IF EXISTS [FK_AspNetUserLogins_AspNetUsers_UserId]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AspNetUserClaims]') AND type in (N'U'))
ALTER TABLE [dbo].[AspNetUserClaims] DROP CONSTRAINT IF EXISTS [FK_AspNetUserClaims_AspNetUsers_UserId]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AspNetRoleClaims]') AND type in (N'U'))
ALTER TABLE [dbo].[AspNetRoleClaims] DROP CONSTRAINT IF EXISTS [FK_AspNetRoleClaims_AspNetRoles_RoleId]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Units]') AND type in (N'U'))
ALTER TABLE [dbo].[Units] DROP CONSTRAINT IF EXISTS [DF__Units__Quantity__5FB337D6]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Units]') AND type in (N'U'))
ALTER TABLE [dbo].[Units] DROP CONSTRAINT IF EXISTS [DF__Units__Active__5DCAEF64]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Units]') AND type in (N'U'))
ALTER TABLE [dbo].[Units] DROP CONSTRAINT IF EXISTS [DF__Units__ItemsID__5CD6CB2B]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Suppliers]') AND type in (N'U'))
ALTER TABLE [dbo].[Suppliers] DROP CONSTRAINT IF EXISTS [DF__Suppliers__Suppl__6477ECF3]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Stockings]') AND type in (N'U'))
ALTER TABLE [dbo].[Stockings] DROP CONSTRAINT IF EXISTS [DF__Stockings__Suppl__60A75C0F]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Sales]') AND type in (N'U'))
ALTER TABLE [dbo].[Sales] DROP CONSTRAINT IF EXISTS [DF__Sales__PricesID__693CA210]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AspNetUsers]') AND type in (N'U'))
ALTER TABLE [dbo].[AspNetUsers] DROP CONSTRAINT IF EXISTS [DF__AspNetUse__FullN__6C190EBB]
GO
/****** Object:  Index [IX_Units_ItemsID]    Script Date: 01-Feb-24 09:03:43 ******/
DROP INDEX IF EXISTS [IX_Units_ItemsID] ON [dbo].[Units]
GO
/****** Object:  Index [IX_SupplierPayments_SuppliersID]    Script Date: 01-Feb-24 09:03:43 ******/
DROP INDEX IF EXISTS [IX_SupplierPayments_SuppliersID] ON [dbo].[SupplierPayments]
GO
/****** Object:  Index [IX_SupplierPayments_PaymentTypesID]    Script Date: 01-Feb-24 09:03:43 ******/
DROP INDEX IF EXISTS [IX_SupplierPayments_PaymentTypesID] ON [dbo].[SupplierPayments]
GO
/****** Object:  Index [IX_Stockings_SuppliersID]    Script Date: 01-Feb-24 09:03:43 ******/
DROP INDEX IF EXISTS [IX_Stockings_SuppliersID] ON [dbo].[Stockings]
GO
/****** Object:  Index [IX_Stockings_ItemsID]    Script Date: 01-Feb-24 09:03:43 ******/
DROP INDEX IF EXISTS [IX_Stockings_ItemsID] ON [dbo].[Stockings]
GO
/****** Object:  Index [IX_Sales_Receipt]    Script Date: 01-Feb-24 09:03:43 ******/
DROP INDEX IF EXISTS [IX_Sales_Receipt] ON [dbo].[Sales]
GO
/****** Object:  Index [IX_Sales_PricesID]    Script Date: 01-Feb-24 09:03:43 ******/
DROP INDEX IF EXISTS [IX_Sales_PricesID] ON [dbo].[Sales]
GO
/****** Object:  Index [IX_Sales_ItemsID]    Script Date: 01-Feb-24 09:03:43 ******/
DROP INDEX IF EXISTS [IX_Sales_ItemsID] ON [dbo].[Sales]
GO
/****** Object:  Index [IX_Prices_UnitsID]    Script Date: 01-Feb-24 09:03:43 ******/
DROP INDEX IF EXISTS [IX_Prices_UnitsID] ON [dbo].[Prices]
GO
/****** Object:  Index [UserNameIndex]    Script Date: 01-Feb-24 09:03:43 ******/
DROP INDEX IF EXISTS [UserNameIndex] ON [dbo].[AspNetUsers]
GO
/****** Object:  Index [EmailIndex]    Script Date: 01-Feb-24 09:03:43 ******/
DROP INDEX IF EXISTS [EmailIndex] ON [dbo].[AspNetUsers]
GO
/****** Object:  Index [IX_AspNetUserRoles_RoleId]    Script Date: 01-Feb-24 09:03:43 ******/
DROP INDEX IF EXISTS [IX_AspNetUserRoles_RoleId] ON [dbo].[AspNetUserRoles]
GO
/****** Object:  Index [IX_AspNetUserLogins_UserId]    Script Date: 01-Feb-24 09:03:43 ******/
DROP INDEX IF EXISTS [IX_AspNetUserLogins_UserId] ON [dbo].[AspNetUserLogins]
GO
/****** Object:  Index [IX_AspNetUserClaims_UserId]    Script Date: 01-Feb-24 09:03:43 ******/
DROP INDEX IF EXISTS [IX_AspNetUserClaims_UserId] ON [dbo].[AspNetUserClaims]
GO
/****** Object:  Index [RoleNameIndex]    Script Date: 01-Feb-24 09:03:43 ******/
DROP INDEX IF EXISTS [RoleNameIndex] ON [dbo].[AspNetRoles]
GO
/****** Object:  Index [IX_AspNetRoleClaims_RoleId]    Script Date: 01-Feb-24 09:03:43 ******/
DROP INDEX IF EXISTS [IX_AspNetRoleClaims_RoleId] ON [dbo].[AspNetRoleClaims]
GO
/****** Object:  Table [dbo].[Suppliers]    Script Date: 01-Feb-24 09:03:43 ******/
DROP TABLE IF EXISTS [dbo].[Suppliers]
GO
/****** Object:  Table [dbo].[SupplierPayments]    Script Date: 01-Feb-24 09:03:43 ******/
DROP TABLE IF EXISTS [dbo].[SupplierPayments]
GO
/****** Object:  Table [dbo].[PaymentTypes]    Script Date: 01-Feb-24 09:03:43 ******/
DROP TABLE IF EXISTS [dbo].[PaymentTypes]
GO
/****** Object:  Table [dbo].[Payments]    Script Date: 01-Feb-24 09:03:43 ******/
DROP TABLE IF EXISTS [dbo].[Payments]
GO
/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 01-Feb-24 09:03:43 ******/
DROP TABLE IF EXISTS [dbo].[AspNetUserTokens]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 01-Feb-24 09:03:43 ******/
DROP TABLE IF EXISTS [dbo].[AspNetUsers]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 01-Feb-24 09:03:43 ******/
DROP TABLE IF EXISTS [dbo].[AspNetUserRoles]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 01-Feb-24 09:03:43 ******/
DROP TABLE IF EXISTS [dbo].[AspNetUserLogins]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 01-Feb-24 09:03:43 ******/
DROP TABLE IF EXISTS [dbo].[AspNetUserClaims]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 01-Feb-24 09:03:43 ******/
DROP TABLE IF EXISTS [dbo].[AspNetRoles]
GO
/****** Object:  Table [dbo].[AspNetRoleClaims]    Script Date: 01-Feb-24 09:03:43 ******/
DROP TABLE IF EXISTS [dbo].[AspNetRoleClaims]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 01-Feb-24 09:03:43 ******/
DROP TABLE IF EXISTS [dbo].[__EFMigrationsHistory]
GO
/****** Object:  View [dbo].[ItemBalances]    Script Date: 01-Feb-24 09:03:43 ******/
DROP VIEW IF EXISTS [dbo].[ItemBalances]
GO
/****** Object:  View [dbo].[vwItemPrices]    Script Date: 01-Feb-24 09:03:43 ******/
DROP VIEW IF EXISTS [dbo].[vwItemPrices]
GO
/****** Object:  UserDefinedFunction [dbo].[fnItemPrices]    Script Date: 01-Feb-24 09:03:43 ******/
DROP FUNCTION IF EXISTS [dbo].[fnItemPrices]
GO
/****** Object:  Table [dbo].[Prices]    Script Date: 01-Feb-24 09:03:43 ******/
DROP TABLE IF EXISTS [dbo].[Prices]
GO
/****** Object:  Table [dbo].[Sales]    Script Date: 01-Feb-24 09:03:43 ******/
DROP TABLE IF EXISTS [dbo].[Sales]
GO
/****** Object:  Table [dbo].[Units]    Script Date: 01-Feb-24 09:03:43 ******/
DROP TABLE IF EXISTS [dbo].[Units]
GO
/****** Object:  Table [dbo].[Stockings]    Script Date: 01-Feb-24 09:03:43 ******/
DROP TABLE IF EXISTS [dbo].[Stockings]
GO
/****** Object:  Table [dbo].[Items]    Script Date: 01-Feb-24 09:03:43 ******/
DROP TABLE IF EXISTS [dbo].[Items]
GO
USE [master]
GO
/****** Object:  Database [bs_stores]    Script Date: 01-Feb-24 09:03:43 ******/
DROP DATABASE IF EXISTS [bs_stores]
GO
/****** Object:  Database [bs_stores]    Script Date: 01-Feb-24 09:03:43 ******/
CREATE DATABASE [bs_stores]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'bs_stores', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\bs_stores.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'bs_stores_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\bs_stores_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [bs_stores] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [bs_stores].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [bs_stores] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [bs_stores] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [bs_stores] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [bs_stores] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [bs_stores] SET ARITHABORT OFF 
GO
ALTER DATABASE [bs_stores] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [bs_stores] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [bs_stores] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [bs_stores] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [bs_stores] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [bs_stores] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [bs_stores] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [bs_stores] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [bs_stores] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [bs_stores] SET  ENABLE_BROKER 
GO
ALTER DATABASE [bs_stores] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [bs_stores] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [bs_stores] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [bs_stores] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [bs_stores] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [bs_stores] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [bs_stores] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [bs_stores] SET RECOVERY FULL 
GO
ALTER DATABASE [bs_stores] SET  MULTI_USER 
GO
ALTER DATABASE [bs_stores] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [bs_stores] SET DB_CHAINING OFF 
GO
ALTER DATABASE [bs_stores] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [bs_stores] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [bs_stores] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [bs_stores] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'bs_stores', N'ON'
GO
ALTER DATABASE [bs_stores] SET QUERY_STORE = ON
GO
ALTER DATABASE [bs_stores] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [bs_stores]
GO
/****** Object:  Table [dbo].[Items]    Script Date: 01-Feb-24 09:03:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Items](
	[ItemsID] [int] IDENTITY(1,1) NOT NULL,
	[ItemName] [nvarchar](50) NOT NULL,
	[Group] [nvarchar](50) NOT NULL,
	[MinimumStock] [int] NOT NULL,
	[DateAdded] [datetime2](7) NOT NULL,
	[Concurrency] [timestamp] NULL,
 CONSTRAINT [PK_Items] PRIMARY KEY CLUSTERED 
(
	[ItemsID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Stockings]    Script Date: 01-Feb-24 09:03:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Stockings](
	[StockingsID] [bigint] IDENTITY(1,1) NOT NULL,
	[ItemsID] [int] NOT NULL,
	[DateAdded] [datetime2](7) NOT NULL,
	[DateBought] [datetime2](7) NOT NULL,
	[Quantity] [smallint] NOT NULL,
	[Receipt] [nvarchar](30) NOT NULL,
	[UnitCost] [float] NOT NULL,
	[UserName] [nvarchar](30) NULL,
	[Concurrency] [timestamp] NULL,
	[SuppliersID] [int] NOT NULL,
 CONSTRAINT [PK_Stockings] PRIMARY KEY CLUSTERED 
(
	[StockingsID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Units]    Script Date: 01-Feb-24 09:03:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Units](
	[UnitsID] [int] IDENTITY(1,1) NOT NULL,
	[Unit] [nvarchar](50) NOT NULL,
	[ItemsID] [int] NOT NULL,
	[Active] [bit] NOT NULL,
	[Quantity] [real] NOT NULL,
 CONSTRAINT [PK_Units] PRIMARY KEY CLUSTERED 
(
	[UnitsID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sales]    Script Date: 01-Feb-24 09:03:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sales](
	[SalesID] [bigint] IDENTITY(1,1) NOT NULL,
	[Quantity] [smallint] NOT NULL,
	[ItemsID] [int] NULL,
	[Cost] [money] NOT NULL,
	[UserName] [nvarchar](30) NULL,
	[Receipt] [nvarchar](20) NOT NULL,
	[DateAdded] [datetime2](7) NOT NULL,
	[Concurrency] [timestamp] NULL,
	[PricesID] [int] NOT NULL,
	[UnitsID] [int] NULL,
 CONSTRAINT [PK_Sales] PRIMARY KEY CLUSTERED 
(
	[SalesID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Prices]    Script Date: 01-Feb-24 09:03:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Prices](
	[PricesID] [int] IDENTITY(1,1) NOT NULL,
	[Price] [money] NOT NULL,
	[UnitsID] [int] NOT NULL,
	[DateSet] [datetime2](7) NOT NULL,
	[Setter] [nvarchar](30) NULL,
	[Concurrency] [timestamp] NULL,
 CONSTRAINT [PK_Prices] PRIMARY KEY CLUSTERED 
(
	[PricesID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  UserDefinedFunction [dbo].[fnItemPrices]    Script Date: 01-Feb-24 09:03:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE FUNCTION [dbo].[fnItemPrices]
(	
)
RETURNS TABLE 
AS
RETURN 
(
	-- Add the SELECT statement with parameter references here
with pcs as (select u.itemsid, p.Price, p.PricesID, p.UnitsID, u.Unit, RANK() over(partition by u.itemsid, u.unitsid order by p.dateset desc) Lev
from Prices p 
inner join Units u on u.UnitsID = p.UnitsID
),
stk as (
SELECT ItemsID, SUM(Quantity) AS Stock
FROM   Stockings s
where s.ItemsID in (select ItemsID from pcs)
GROUP BY ItemsID
),
sls as (
SELECT ItemsID, SUM(Quantity) AS Sales
FROM   Sales s
where s.ItemsID in (select ItemsID from pcs)
GROUP BY ItemsID
)
select p.ItemsID, i.ItemName, p.PricesID, i.[Group], p.Price, p.Unit, p.UnitsID, isnull(s.Stock, 0) - isnull(l.Sales, 0) [Balance]
from pcs p
inner join Items i on i.ItemsID  = p.ItemsID
inner join stk s on s.ItemsID = p.ItemsID and s.ItemsID = i.ItemsID
full join sls l on l.ItemsID = i.ItemsID and l.ItemsID = p.ItemsID
where Lev = 1
)
GO
/****** Object:  View [dbo].[vwItemPrices]    Script Date: 01-Feb-24 09:03:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vwItemPrices]
AS
with pcs as (select u.itemsid, p.Price, p.PricesID, p.UnitsID, u.Unit, RANK() over(partition by u.itemsid, u.unitsid order by p.dateset desc) Lev
from Prices p 
inner join Units u on u.UnitsID = p.UnitsID
),
stk as (
SELECT ItemsID, SUM(Quantity) AS Stock
FROM   Stockings s
where s.ItemsID in (select ItemsID from pcs)
GROUP BY ItemsID
),
sls as (
SELECT ItemsID, SUM(Quantity) AS Sales
FROM   Sales s
where s.ItemsID in (select ItemsID from pcs)
GROUP BY ItemsID
)
select p.ItemsID, i.ItemName, p.PricesID, i.[Group], p.Price, p.Unit, p.UnitsID, isnull(s.Stock, 0) - isnull(l.Sales, 0) [Balance]
from pcs p
inner join Items i on i.ItemsID  = p.ItemsID
inner join stk s on s.ItemsID = p.ItemsID and s.ItemsID = i.ItemsID
full join sls l on l.ItemsID = i.ItemsID and l.ItemsID = p.ItemsID
where Lev = 1
GO
/****** Object:  View [dbo].[ItemBalances]    Script Date: 01-Feb-24 09:03:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create   view [dbo].[ItemBalances] as (
select i.ItemName, i.MinimumStock, i.[Group], st.Received, sl.Issued, st.Received + sl.Issued Total
from Items i
cross apply(
select ISNULL(SUM(s.Quantity), 0) Received
from Stockings s where s.ItemsID = i.itemsID
)st
cross apply(
select ISNULL(SUM(s.Quantity), 0) Issued
from Sales s where s.ItemsID = i.ItemsID
)sl
)
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 01-Feb-24 09:03:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoleClaims]    Script Date: 01-Feb-24 09:03:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 01-Feb-24 09:03:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 01-Feb-24 09:03:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 01-Feb-24 09:03:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](450) NOT NULL,
	[ProviderKey] [nvarchar](450) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 01-Feb-24 09:03:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](450) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 01-Feb-24 09:03:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](450) NOT NULL,
	[UserName] [nvarchar](256) NOT NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[Email] [nvarchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[FullName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 01-Feb-24 09:03:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserTokens](
	[UserId] [nvarchar](450) NOT NULL,
	[LoginProvider] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](450) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Payments]    Script Date: 01-Feb-24 09:03:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Payments](
	[Receipt] [nvarchar](20) NOT NULL,
	[Cash] [money] NOT NULL,
	[MobileMoney] [money] NOT NULL,
	[Customer] [nvarchar](75) NOT NULL,
	[Telephone] [nvarchar](10) NULL,
	[CanContact] [bit] NOT NULL,
	[Total] [money] NOT NULL,
	[SalesType] [varchar](30) NOT NULL,
	[ExpectedDate] [date] NULL,
	[DatePaid] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Payments] PRIMARY KEY CLUSTERED 
(
	[Receipt] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PaymentTypes]    Script Date: 01-Feb-24 09:03:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PaymentTypes](
	[PaymentTypesID] [tinyint] IDENTITY(1,1) NOT NULL,
	[PaymentType] [nvarchar](15) NOT NULL,
 CONSTRAINT [PK_PaymentTypes] PRIMARY KEY CLUSTERED 
(
	[PaymentTypesID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SupplierPayments]    Script Date: 01-Feb-24 09:03:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SupplierPayments](
	[SupplierPaymentsID] [int] IDENTITY(1,1) NOT NULL,
	[SuppliersID] [int] NOT NULL,
	[DatePaid] [datetime2](7) NOT NULL,
	[Amount] [money] NOT NULL,
	[PaymentTypesID] [tinyint] NOT NULL,
	[Reference] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_SupplierPayments] PRIMARY KEY CLUSTERED 
(
	[SupplierPaymentsID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Suppliers]    Script Date: 01-Feb-24 09:03:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Suppliers](
	[SuppliersID] [int] IDENTITY(1,1) NOT NULL,
	[DateAdded] [datetime2](7) NOT NULL,
	[Address] [nvarchar](50) NULL,
	[IsActive] [bit] NOT NULL,
	[SupplierName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Suppliers] PRIMARY KEY CLUSTERED 
(
	[SuppliersID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230508162725_Initial', N'8.0.1')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230508170623_UnitVal', N'8.0.1')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230508172533_UnitQuantity', N'8.0.1')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230508221033_Supps', N'8.0.1')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230509060749_SuppsFix', N'8.0.1')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230509154405_SupPay', N'8.0.1')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230510063501_PayPrice', N'8.0.1')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240126211248_FullNameMig', N'8.0.1')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240201063205_SalesItems', N'8.0.1')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240201065131_SalesUnits', N'8.0.1')
GO
SET IDENTITY_INSERT [dbo].[AspNetUserClaims] ON 

INSERT [dbo].[AspNetUserClaims] ([Id], [UserId], [ClaimType], [ClaimValue]) VALUES (1, N'c4468d65-ac4d-4f8d-b3c2-cd77f15ef025', N'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name', N'kwakuduah')
INSERT [dbo].[AspNetUserClaims] ([Id], [UserId], [ClaimType], [ClaimValue]) VALUES (2, N'c4468d65-ac4d-4f8d-b3c2-cd77f15ef025', N'http://schemas.microsoft.com/ws/2008/06/identity/claims/role', N'User')
INSERT [dbo].[AspNetUserClaims] ([Id], [UserId], [ClaimType], [ClaimValue]) VALUES (3, N'c4468d65-ac4d-4f8d-b3c2-cd77f15ef025', N'http://schemas.microsoft.com/ws/2008/06/identity/claims/role', N'Power')
SET IDENTITY_INSERT [dbo].[AspNetUserClaims] OFF
GO
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FullName]) VALUES (N'c4468d65-ac4d-4f8d-b3c2-cd77f15ef025', N'kwakuduah', N'KWAKUDUAH', NULL, NULL, 0, N'AQAAAAIAAYagAAAAEOHZghg+HcOCNfWl18YgX6WVl2VGII6RCjDvhF6GNYD6nW5KyYjRZo1riRMVaEYiJQ==', N'FXYSVEOQ3ZOTGBCHZUCT7O76RDP4EWRD', N'ea085af6-f144-4a63-b1c4-81d555b7440b', NULL, 0, 0, NULL, 1, 0, N'Kwaku Duah')
GO
SET IDENTITY_INSERT [dbo].[Items] ON 

INSERT [dbo].[Items] ([ItemsID], [ItemName], [Group], [MinimumStock], [DateAdded]) VALUES (1, N'Playstation 2', N'Consoles', 20, CAST(N'2024-02-01T06:51:29.1072326' AS DateTime2))
INSERT [dbo].[Items] ([ItemsID], [ItemName], [Group], [MinimumStock], [DateAdded]) VALUES (2, N'XBox One', N'Consoles', 10, CAST(N'2024-02-01T06:51:29.1072358' AS DateTime2))
INSERT [dbo].[Items] ([ItemsID], [ItemName], [Group], [MinimumStock], [DateAdded]) VALUES (3, N'XBox 360', N'Consoles', 15, CAST(N'2024-02-01T06:51:29.1072367' AS DateTime2))
INSERT [dbo].[Items] ([ItemsID], [ItemName], [Group], [MinimumStock], [DateAdded]) VALUES (4, N'XBox', N'Consoles', 5, CAST(N'2024-02-01T06:51:29.1072373' AS DateTime2))
INSERT [dbo].[Items] ([ItemsID], [ItemName], [Group], [MinimumStock], [DateAdded]) VALUES (5, N'XBox One Wired Controller', N'Contollers', 10, CAST(N'2024-02-01T06:51:29.1072379' AS DateTime2))
INSERT [dbo].[Items] ([ItemsID], [ItemName], [Group], [MinimumStock], [DateAdded]) VALUES (6, N'XBox 360 Wireless Controller', N'Contollers', 10, CAST(N'2024-02-01T06:51:29.1072399' AS DateTime2))
INSERT [dbo].[Items] ([ItemsID], [ItemName], [Group], [MinimumStock], [DateAdded]) VALUES (7, N'My item', N'My item group', 10, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Items] ([ItemsID], [ItemName], [Group], [MinimumStock], [DateAdded]) VALUES (8, N'My items', N'My item group', 10, CAST(N'2024-01-28T19:24:35.5773990' AS DateTime2))
INSERT [dbo].[Items] ([ItemsID], [ItemName], [Group], [MinimumStock], [DateAdded]) VALUES (9, N'My itemss', N'My item group', 10, CAST(N'2024-01-28T19:26:26.3795461' AS DateTime2))
INSERT [dbo].[Items] ([ItemsID], [ItemName], [Group], [MinimumStock], [DateAdded]) VALUES (10, N'Only item', N'My item group', 10, CAST(N'2024-01-28T19:31:32.5390084' AS DateTime2))
INSERT [dbo].[Items] ([ItemsID], [ItemName], [Group], [MinimumStock], [DateAdded]) VALUES (11, N'Best', N'My item group', 10, CAST(N'2024-01-29T13:11:54.5690214' AS DateTime2))
SET IDENTITY_INSERT [dbo].[Items] OFF
GO
INSERT [dbo].[Payments] ([Receipt], [Cash], [MobileMoney], [Customer], [Telephone], [CanContact], [Total], [SalesType], [ExpectedDate], [DatePaid]) VALUES (N'2T9XXE', 6387.0000, 0.0000, N'Bernice', N'0540937884', 0, 6387.0000, N'Outright purchase', NULL, CAST(N'2024-02-01T06:34:16.9841802' AS DateTime2))
INSERT [dbo].[Payments] ([Receipt], [Cash], [MobileMoney], [Customer], [Telephone], [CanContact], [Total], [SalesType], [ExpectedDate], [DatePaid]) VALUES (N'432FQY9', 17250.0000, 0.0000, N'Kwaku Duah', N'0540937884', 0, 17250.0000, N'Outright purchase', NULL, CAST(N'2024-02-01T07:04:49.1428661' AS DateTime2))
GO
SET IDENTITY_INSERT [dbo].[PaymentTypes] ON 

INSERT [dbo].[PaymentTypes] ([PaymentTypesID], [PaymentType]) VALUES (1, N'Cheque')
INSERT [dbo].[PaymentTypes] ([PaymentTypesID], [PaymentType]) VALUES (2, N'Cash')
INSERT [dbo].[PaymentTypes] ([PaymentTypesID], [PaymentType]) VALUES (3, N'Mobile Money')
SET IDENTITY_INSERT [dbo].[PaymentTypes] OFF
GO
SET IDENTITY_INSERT [dbo].[Prices] ON 

INSERT [dbo].[Prices] ([PricesID], [Price], [UnitsID], [DateSet], [Setter]) VALUES (2, 120.0000, 4, CAST(N'2024-01-28T17:56:39.0032190' AS DateTime2), NULL)
INSERT [dbo].[Prices] ([PricesID], [Price], [UnitsID], [DateSet], [Setter]) VALUES (3, 125.0000, 4, CAST(N'2024-01-28T18:28:21.9510484' AS DateTime2), N'kwakuduah')
INSERT [dbo].[Prices] ([PricesID], [Price], [UnitsID], [DateSet], [Setter]) VALUES (4, 127.0000, 4, CAST(N'2024-01-28T18:29:48.6154511' AS DateTime2), N'kwakuduah')
INSERT [dbo].[Prices] ([PricesID], [Price], [UnitsID], [DateSet], [Setter]) VALUES (5, 129.0000, 4, CAST(N'2024-01-28T18:37:23.5371651' AS DateTime2), N'kwakuduah')
INSERT [dbo].[Prices] ([PricesID], [Price], [UnitsID], [DateSet], [Setter]) VALUES (6, 120.0000, 5, CAST(N'2024-01-29T13:14:26.2035608' AS DateTime2), NULL)
INSERT [dbo].[Prices] ([PricesID], [Price], [UnitsID], [DateSet], [Setter]) VALUES (7, 127.0000, 5, CAST(N'2024-01-29T13:18:22.6198401' AS DateTime2), N'kwakuduah')
INSERT [dbo].[Prices] ([PricesID], [Price], [UnitsID], [DateSet], [Setter]) VALUES (8, 570.0000, 6, CAST(N'2024-01-30T22:35:50.8562211' AS DateTime2), NULL)
INSERT [dbo].[Prices] ([PricesID], [Price], [UnitsID], [DateSet], [Setter]) VALUES (9, 575.0000, 6, CAST(N'2024-01-30T22:36:06.3336048' AS DateTime2), N'kwakuduah')
INSERT [dbo].[Prices] ([PricesID], [Price], [UnitsID], [DateSet], [Setter]) VALUES (10, 1200.0000, 7, CAST(N'2024-01-30T22:45:21.6476955' AS DateTime2), NULL)
SET IDENTITY_INSERT [dbo].[Prices] OFF
GO
SET IDENTITY_INSERT [dbo].[Sales] ON 

INSERT [dbo].[Sales] ([SalesID], [Quantity], [ItemsID], [Cost], [UserName], [Receipt], [DateAdded], [PricesID], [UnitsID]) VALUES (7, 3, 1, 387.0000, N'kwakuduah', N'2T9XXE', CAST(N'2024-02-01T06:34:16.9841802' AS DateTime2), 5, NULL)
INSERT [dbo].[Sales] ([SalesID], [Quantity], [ItemsID], [Cost], [UserName], [Receipt], [DateAdded], [PricesID], [UnitsID]) VALUES (8, 5, 1, 6000.0000, N'kwakuduah', N'2T9XXE', CAST(N'2024-02-01T06:34:16.9841802' AS DateTime2), 10, NULL)
INSERT [dbo].[Sales] ([SalesID], [Quantity], [ItemsID], [Cost], [UserName], [Receipt], [DateAdded], [PricesID], [UnitsID]) VALUES (9, 30, 2, 17250.0000, N'kwakuduah', N'432FQY9', CAST(N'2024-02-01T07:04:49.1428661' AS DateTime2), 9, 6)
SET IDENTITY_INSERT [dbo].[Sales] OFF
GO
SET IDENTITY_INSERT [dbo].[Stockings] ON 

INSERT [dbo].[Stockings] ([StockingsID], [ItemsID], [DateAdded], [DateBought], [Quantity], [Receipt], [UnitCost], [UserName], [SuppliersID]) VALUES (1, 1, CAST(N'2024-01-29T22:06:09.1514766' AS DateTime2), CAST(N'2024-01-29T22:05:52.7630000' AS DateTime2), 10, N'Test-Unit', 100, N'kwakuduah', 3)
INSERT [dbo].[Stockings] ([StockingsID], [ItemsID], [DateAdded], [DateBought], [Quantity], [Receipt], [UnitCost], [UserName], [SuppliersID]) VALUES (2, 4, CAST(N'2024-01-29T22:09:11.9381989' AS DateTime2), CAST(N'2024-01-29T22:08:37.3770000' AS DateTime2), 56, N'bars', 78, N'kwakuduah', 3)
INSERT [dbo].[Stockings] ([StockingsID], [ItemsID], [DateAdded], [DateBought], [Quantity], [Receipt], [UnitCost], [UserName], [SuppliersID]) VALUES (3, 3, CAST(N'2024-01-29T22:09:11.9383239' AS DateTime2), CAST(N'2024-01-29T22:08:37.3770000' AS DateTime2), 10, N'bars', 130, N'kwakuduah', 3)
INSERT [dbo].[Stockings] ([StockingsID], [ItemsID], [DateAdded], [DateBought], [Quantity], [Receipt], [UnitCost], [UserName], [SuppliersID]) VALUES (4, 2, CAST(N'2024-01-29T22:09:11.9383249' AS DateTime2), CAST(N'2024-01-29T22:08:37.3770000' AS DateTime2), 1, N'bars', 78, N'kwakuduah', 3)
INSERT [dbo].[Stockings] ([StockingsID], [ItemsID], [DateAdded], [DateBought], [Quantity], [Receipt], [UnitCost], [UserName], [SuppliersID]) VALUES (5, 6, CAST(N'2024-01-29T22:10:47.0344232' AS DateTime2), CAST(N'2024-01-29T22:10:20.4970000' AS DateTime2), 23, N'guy guy', 78, N'kwakuduah', 3)
INSERT [dbo].[Stockings] ([StockingsID], [ItemsID], [DateAdded], [DateBought], [Quantity], [Receipt], [UnitCost], [UserName], [SuppliersID]) VALUES (6, 5, CAST(N'2024-01-29T22:10:47.0344347' AS DateTime2), CAST(N'2024-01-29T22:10:20.4970000' AS DateTime2), 30, N'guy guy', 370, N'kwakuduah', 3)
INSERT [dbo].[Stockings] ([StockingsID], [ItemsID], [DateAdded], [DateBought], [Quantity], [Receipt], [UnitCost], [UserName], [SuppliersID]) VALUES (7, 1, CAST(N'2024-01-29T22:13:03.0068272' AS DateTime2), CAST(N'2024-01-29T22:12:44.2230000' AS DateTime2), 10, N'two sure', 100, N'kwakuduah', 3)
INSERT [dbo].[Stockings] ([StockingsID], [ItemsID], [DateAdded], [DateBought], [Quantity], [Receipt], [UnitCost], [UserName], [SuppliersID]) VALUES (8, 1, CAST(N'2024-01-30T22:06:40.3354402' AS DateTime2), CAST(N'2024-01-30T22:05:43.6420000' AS DateTime2), 90, N'jfgfdg', 9000, N'kwakuduah', 3)
INSERT [dbo].[Stockings] ([StockingsID], [ItemsID], [DateAdded], [DateBought], [Quantity], [Receipt], [UnitCost], [UserName], [SuppliersID]) VALUES (9, 2, CAST(N'2024-01-30T22:08:50.7973184' AS DateTime2), CAST(N'2024-01-30T22:08:28.1860000' AS DateTime2), 9, N'sfjs', 2000, N'kwakuduah', 3)
SET IDENTITY_INSERT [dbo].[Stockings] OFF
GO
SET IDENTITY_INSERT [dbo].[SupplierPayments] ON 

INSERT [dbo].[SupplierPayments] ([SupplierPaymentsID], [SuppliersID], [DatePaid], [Amount], [PaymentTypesID], [Reference]) VALUES (2, 3, CAST(N'2024-01-30T17:19:51.4035766' AS DateTime2), 2000.0000, 3, N'000')
INSERT [dbo].[SupplierPayments] ([SupplierPaymentsID], [SuppliersID], [DatePaid], [Amount], [PaymentTypesID], [Reference]) VALUES (3, 3, CAST(N'2024-01-30T21:37:34.6513850' AS DateTime2), 8000.0000, 1, N'sjflfjs')
INSERT [dbo].[SupplierPayments] ([SupplierPaymentsID], [SuppliersID], [DatePaid], [Amount], [PaymentTypesID], [Reference]) VALUES (4, 3, CAST(N'2024-01-30T21:48:17.9643894' AS DateTime2), 9000.0000, 1, N'Dererere')
INSERT [dbo].[SupplierPayments] ([SupplierPaymentsID], [SuppliersID], [DatePaid], [Amount], [PaymentTypesID], [Reference]) VALUES (5, 3, CAST(N'2024-01-30T21:54:37.0953019' AS DateTime2), 140.0000, 1, N'final pay')
INSERT [dbo].[SupplierPayments] ([SupplierPaymentsID], [SuppliersID], [DatePaid], [Amount], [PaymentTypesID], [Reference]) VALUES (6, 3, CAST(N'2024-01-30T22:02:01.1192770' AS DateTime2), 1000.0000, 1, N'Ghana')
INSERT [dbo].[SupplierPayments] ([SupplierPaymentsID], [SuppliersID], [DatePaid], [Amount], [PaymentTypesID], [Reference]) VALUES (7, 3, CAST(N'2024-01-30T22:04:50.4765958' AS DateTime2), 200.0000, 3, N'sjflsjf')
INSERT [dbo].[SupplierPayments] ([SupplierPaymentsID], [SuppliersID], [DatePaid], [Amount], [PaymentTypesID], [Reference]) VALUES (8, 3, CAST(N'2024-01-30T22:06:02.3494310' AS DateTime2), 100.0000, 3, N'lklji')
INSERT [dbo].[SupplierPayments] ([SupplierPaymentsID], [SuppliersID], [DatePaid], [Amount], [PaymentTypesID], [Reference]) VALUES (9, 3, CAST(N'2024-01-30T22:10:42.9635171' AS DateTime2), 20000.0000, 1, N'djfs00')
SET IDENTITY_INSERT [dbo].[SupplierPayments] OFF
GO
SET IDENTITY_INSERT [dbo].[Suppliers] ON 

INSERT [dbo].[Suppliers] ([SuppliersID], [DateAdded], [Address], [IsActive], [SupplierName]) VALUES (1, CAST(N'2024-01-28T22:39:34.6604438' AS DateTime2), N'+233245266188', 0, N'My Stores')
INSERT [dbo].[Suppliers] ([SuppliersID], [DateAdded], [Address], [IsActive], [SupplierName]) VALUES (2, CAST(N'2024-01-28T22:45:52.3842913' AS DateTime2), N'gsdgsdf', 0, N'bAD stores')
INSERT [dbo].[Suppliers] ([SuppliersID], [DateAdded], [Address], [IsActive], [SupplierName]) VALUES (3, CAST(N'2024-01-28T22:49:06.9776709' AS DateTime2), N'+233207531532', 1, N'Frimaa Technologies')
INSERT [dbo].[Suppliers] ([SuppliersID], [DateAdded], [Address], [IsActive], [SupplierName]) VALUES (4, CAST(N'2024-01-28T22:50:37.9730304' AS DateTime2), N'+233244993919', 1, N'Bern Tech')
INSERT [dbo].[Suppliers] ([SuppliersID], [DateAdded], [Address], [IsActive], [SupplierName]) VALUES (5, CAST(N'2024-01-29T06:53:10.9971452' AS DateTime2), N'03039394848', 0, N'Fish supplier')
INSERT [dbo].[Suppliers] ([SuppliersID], [DateAdded], [Address], [IsActive], [SupplierName]) VALUES (6, CAST(N'2024-01-29T13:24:37.7852984' AS DateTime2), N'sdfjsljfla', 1, N'My supplier')
INSERT [dbo].[Suppliers] ([SuppliersID], [DateAdded], [Address], [IsActive], [SupplierName]) VALUES (7, CAST(N'2024-01-29T13:34:41.9326590' AS DateTime2), N'gsdgsdf', 0, N'Good stores')
SET IDENTITY_INSERT [dbo].[Suppliers] OFF
GO
SET IDENTITY_INSERT [dbo].[Units] ON 

INSERT [dbo].[Units] ([UnitsID], [Unit], [ItemsID], [Active], [Quantity]) VALUES (4, N'Piece', 1, 1, 1)
INSERT [dbo].[Units] ([UnitsID], [Unit], [ItemsID], [Active], [Quantity]) VALUES (5, N'Piece', 11, 1, 1)
INSERT [dbo].[Units] ([UnitsID], [Unit], [ItemsID], [Active], [Quantity]) VALUES (6, N'Piece', 2, 1, 1)
INSERT [dbo].[Units] ([UnitsID], [Unit], [ItemsID], [Active], [Quantity]) VALUES (7, N'Bundle', 1, 1, 12)
SET IDENTITY_INSERT [dbo].[Units] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetRoleClaims_RoleId]    Script Date: 01-Feb-24 09:03:45 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetRoleClaims_RoleId] ON [dbo].[AspNetRoleClaims]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [RoleNameIndex]    Script Date: 01-Feb-24 09:03:45 ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
(
	[NormalizedName] ASC
)
WHERE ([NormalizedName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserClaims_UserId]    Script Date: 01-Feb-24 09:03:45 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserClaims_UserId] ON [dbo].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserLogins_UserId]    Script Date: 01-Feb-24 09:03:45 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserLogins_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserRoles_RoleId]    Script Date: 01-Feb-24 09:03:45 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserRoles_RoleId] ON [dbo].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [EmailIndex]    Script Date: 01-Feb-24 09:03:45 ******/
CREATE NONCLUSTERED INDEX [EmailIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedEmail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UserNameIndex]    Script Date: 01-Feb-24 09:03:45 ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedUserName] ASC
)
WHERE ([NormalizedUserName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Prices_UnitsID]    Script Date: 01-Feb-24 09:03:45 ******/
CREATE NONCLUSTERED INDEX [IX_Prices_UnitsID] ON [dbo].[Prices]
(
	[UnitsID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Sales_ItemsID]    Script Date: 01-Feb-24 09:03:45 ******/
CREATE NONCLUSTERED INDEX [IX_Sales_ItemsID] ON [dbo].[Sales]
(
	[ItemsID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Sales_PricesID]    Script Date: 01-Feb-24 09:03:45 ******/
CREATE NONCLUSTERED INDEX [IX_Sales_PricesID] ON [dbo].[Sales]
(
	[PricesID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Sales_Receipt]    Script Date: 01-Feb-24 09:03:45 ******/
CREATE NONCLUSTERED INDEX [IX_Sales_Receipt] ON [dbo].[Sales]
(
	[Receipt] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Stockings_ItemsID]    Script Date: 01-Feb-24 09:03:45 ******/
CREATE NONCLUSTERED INDEX [IX_Stockings_ItemsID] ON [dbo].[Stockings]
(
	[ItemsID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Stockings_SuppliersID]    Script Date: 01-Feb-24 09:03:45 ******/
CREATE NONCLUSTERED INDEX [IX_Stockings_SuppliersID] ON [dbo].[Stockings]
(
	[SuppliersID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_SupplierPayments_PaymentTypesID]    Script Date: 01-Feb-24 09:03:45 ******/
CREATE NONCLUSTERED INDEX [IX_SupplierPayments_PaymentTypesID] ON [dbo].[SupplierPayments]
(
	[PaymentTypesID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_SupplierPayments_SuppliersID]    Script Date: 01-Feb-24 09:03:45 ******/
CREATE NONCLUSTERED INDEX [IX_SupplierPayments_SuppliersID] ON [dbo].[SupplierPayments]
(
	[SuppliersID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Units_ItemsID]    Script Date: 01-Feb-24 09:03:45 ******/
CREATE NONCLUSTERED INDEX [IX_Units_ItemsID] ON [dbo].[Units]
(
	[ItemsID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AspNetUsers] ADD  DEFAULT (N'') FOR [FullName]
GO
ALTER TABLE [dbo].[Sales] ADD  DEFAULT ((0)) FOR [PricesID]
GO
ALTER TABLE [dbo].[Stockings] ADD  DEFAULT ((0)) FOR [SuppliersID]
GO
ALTER TABLE [dbo].[Suppliers] ADD  DEFAULT (N'') FOR [SupplierName]
GO
ALTER TABLE [dbo].[Units] ADD  DEFAULT ((0)) FOR [ItemsID]
GO
ALTER TABLE [dbo].[Units] ADD  DEFAULT (CONVERT([bit],(0))) FOR [Active]
GO
ALTER TABLE [dbo].[Units] ADD  DEFAULT (CONVERT([real],(0))) FOR [Quantity]
GO
ALTER TABLE [dbo].[AspNetRoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetRoleClaims] CHECK CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserTokens]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserTokens] CHECK CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[Prices]  WITH CHECK ADD  CONSTRAINT [FK_Prices_Units_UnitsID] FOREIGN KEY([UnitsID])
REFERENCES [dbo].[Units] ([UnitsID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Prices] CHECK CONSTRAINT [FK_Prices_Units_UnitsID]
GO
ALTER TABLE [dbo].[Sales]  WITH CHECK ADD  CONSTRAINT [FK_Sales_Items_ItemsID] FOREIGN KEY([ItemsID])
REFERENCES [dbo].[Items] ([ItemsID])
GO
ALTER TABLE [dbo].[Sales] CHECK CONSTRAINT [FK_Sales_Items_ItemsID]
GO
ALTER TABLE [dbo].[Sales]  WITH CHECK ADD  CONSTRAINT [FK_Sales_Payments_Receipt] FOREIGN KEY([Receipt])
REFERENCES [dbo].[Payments] ([Receipt])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Sales] CHECK CONSTRAINT [FK_Sales_Payments_Receipt]
GO
ALTER TABLE [dbo].[Sales]  WITH CHECK ADD  CONSTRAINT [FK_Sales_Prices_PricesID] FOREIGN KEY([PricesID])
REFERENCES [dbo].[Prices] ([PricesID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Sales] CHECK CONSTRAINT [FK_Sales_Prices_PricesID]
GO
ALTER TABLE [dbo].[Stockings]  WITH CHECK ADD  CONSTRAINT [FK_Stockings_Items_ItemsID] FOREIGN KEY([ItemsID])
REFERENCES [dbo].[Items] ([ItemsID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Stockings] CHECK CONSTRAINT [FK_Stockings_Items_ItemsID]
GO
ALTER TABLE [dbo].[Stockings]  WITH CHECK ADD  CONSTRAINT [FK_Stockings_Suppliers_SuppliersID] FOREIGN KEY([SuppliersID])
REFERENCES [dbo].[Suppliers] ([SuppliersID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Stockings] CHECK CONSTRAINT [FK_Stockings_Suppliers_SuppliersID]
GO
ALTER TABLE [dbo].[SupplierPayments]  WITH CHECK ADD  CONSTRAINT [FK_SupplierPayments_PaymentTypes_PaymentTypesID] FOREIGN KEY([PaymentTypesID])
REFERENCES [dbo].[PaymentTypes] ([PaymentTypesID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SupplierPayments] CHECK CONSTRAINT [FK_SupplierPayments_PaymentTypes_PaymentTypesID]
GO
ALTER TABLE [dbo].[SupplierPayments]  WITH CHECK ADD  CONSTRAINT [FK_SupplierPayments_Suppliers_SuppliersID] FOREIGN KEY([SuppliersID])
REFERENCES [dbo].[Suppliers] ([SuppliersID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SupplierPayments] CHECK CONSTRAINT [FK_SupplierPayments_Suppliers_SuppliersID]
GO
ALTER TABLE [dbo].[Units]  WITH CHECK ADD  CONSTRAINT [FK_Units_Items_ItemsID] FOREIGN KEY([ItemsID])
REFERENCES [dbo].[Items] ([ItemsID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Units] CHECK CONSTRAINT [FK_Units_Items_ItemsID]
GO
/****** Object:  StoredProcedure [dbo].[spItemsToSell]    Script Date: 01-Feb-24 09:03:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE   PROCEDURE [dbo].[spItemsToSell]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
with pcs as (
select u.itemsid, p.Price, p.PricesID, p.UnitsID, u.Unit, RANK() over(partition by u.itemsid, u.unitsid order by p.dateset desc) Lev
from Prices p 
inner join Units u on u.UnitsID = p.UnitsID
),
stk as (
SELECT ItemsID, SUM(Quantity) AS Stock
FROM   Stockings s
where s.ItemsID in (select ItemsID from pcs)
GROUP BY ItemsID
),
sls as (
SELECT ItemsID, SUM(Quantity) AS Sales
FROM   Sales s
where s.ItemsID in (select ItemsID from pcs)
GROUP BY ItemsID
)
select p.ItemsID, i.ItemName, p.PricesID, i.[Group], p.Price, p.Unit, p.UnitsID, isnull(s.Stock, 0) - isnull(l.Sales, 0) [Balance]
from pcs p
inner join Items i on i.ItemsID  = p.ItemsID
inner join stk s on s.ItemsID = p.ItemsID and s.ItemsID = i.ItemsID
full join sls l on l.ItemsID = i.ItemsID and l.ItemsID = p.ItemsID
where Lev = 1
order by p.ItemsID, p.Unit
END
GO
/****** Object:  StoredProcedure [dbo].[spReceipts]    Script Date: 01-Feb-24 09:03:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--spReceipts 'G19A5EN'


CREATE   procedure [dbo].[spReceipts]
(
	@receipt varchar(15)
)
as
begin
select p.Customer, p.Telephone, s.Receipt, s.Cost, CONCAT(vip.ItemName, ' (',vip.Unit,')') ItemName, p.SalesType, s.Quantity, vip.Unit
from sales s 
inner join Payments p on p.Receipt = s.Receipt
inner join vwItemPrices vip on vip.PricesID = s.PricesID and s.ItemsID = vip.ItemsID
where s.receipt = @receipt;
end
GO
/****** Object:  StoredProcedure [dbo].[spSalesLedger]    Script Date: 01-Feb-24 09:03:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE     PROCEDURE [dbo].[spSalesLedger]
		@start date,
		@end date
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
SELECT   p.Receipt, p.Cash, p.MobileMoney, p.Customer, p.Total, p.SalesType, p.DatePaid, i.ItemName + ' (' + u.Unit + ')' ItemName, pr.PricesID, p.Telephone, u.Unit, s.Cost, u.UnitsID, 
                         pr.Price, s.SalesID, s.Quantity
FROM         Payments AS p INNER JOIN
                         Sales AS s ON s.Receipt = p.Receipt INNER JOIN
                         Prices AS pr ON pr.PricesID = s.PricesID INNER JOIN
                         Units AS u ON u.UnitsID = pr.UnitsID INNER JOIN
                         Items AS i ON i.ItemsID = u.ItemsID
WHERE     (CAST(p.DatePaid AS date) BETWEEN @start AND @end)
END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vwItemPrices'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vwItemPrices'
GO
USE [master]
GO
ALTER DATABASE [bs_stores] SET  READ_WRITE 
GO
