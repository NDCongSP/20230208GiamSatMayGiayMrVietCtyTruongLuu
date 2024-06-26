USE [master]
GO
/****** Object:  Database [IDCScaleSystem_Test]    Script Date: 2/13/2023 4:08:32 PM ******/
CREATE DATABASE [IDCScaleSystem_Test]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'IDCScaleSystem', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\IDCScaleSystem_Test.mdf' , SIZE = 204800KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'IDCScaleSystem_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\IDCScaleSystem_Test_log.ldf' , SIZE = 466944KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [IDCScaleSystem_Test] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [IDCScaleSystem_Test].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [IDCScaleSystem_Test] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [IDCScaleSystem_Test] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [IDCScaleSystem_Test] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [IDCScaleSystem_Test] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [IDCScaleSystem_Test] SET ARITHABORT OFF 
GO
ALTER DATABASE [IDCScaleSystem_Test] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [IDCScaleSystem_Test] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [IDCScaleSystem_Test] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [IDCScaleSystem_Test] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [IDCScaleSystem_Test] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [IDCScaleSystem_Test] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [IDCScaleSystem_Test] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [IDCScaleSystem_Test] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [IDCScaleSystem_Test] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [IDCScaleSystem_Test] SET  DISABLE_BROKER 
GO
ALTER DATABASE [IDCScaleSystem_Test] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [IDCScaleSystem_Test] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [IDCScaleSystem_Test] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [IDCScaleSystem_Test] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [IDCScaleSystem_Test] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [IDCScaleSystem_Test] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [IDCScaleSystem_Test] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [IDCScaleSystem_Test] SET RECOVERY FULL 
GO
ALTER DATABASE [IDCScaleSystem_Test] SET  MULTI_USER 
GO
ALTER DATABASE [IDCScaleSystem_Test] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [IDCScaleSystem_Test] SET DB_CHAINING OFF 
GO
ALTER DATABASE [IDCScaleSystem_Test] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [IDCScaleSystem_Test] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [IDCScaleSystem_Test] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [IDCScaleSystem_Test] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [IDCScaleSystem_Test] SET QUERY_STORE = OFF
GO
USE [IDCScaleSystem_Test]
GO
/****** Object:  User [idc_ad_system]    Script Date: 2/13/2023 4:08:32 PM ******/
CREATE USER [idc_ad_system] FOR LOGIN [idc_ad_system] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [idc_ad_system]
GO
/****** Object:  Table [dbo].[tblWinlineProductsInfo]    Script Date: 2/13/2023 4:08:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblWinlineProductsInfo](
	[Id] [uniqueidentifier] NOT NULL,
	[CodeItemSize] [nvarchar](100) NULL,
	[ProductNumber] [nvarchar](500) NULL,
	[ProductName] [nvarchar](500) NULL,
	[ProductCategory] [int] NULL,
	[Brand] [nvarchar](500) NULL,
	[Decoration] [int] NULL,
	[MainProductNo] [nvarchar](100) NULL,
	[MainProductName] [nvarchar](max) NULL,
	[Color] [nvarchar](max) NULL,
	[SizeCode] [int] NULL,
	[SizeName] [nvarchar](100) NULL,
	[Weight] [float] NULL,
	[LeftWeight] [float] NULL,
	[RightWeight] [float] NULL,
	[BoxType] [nvarchar](50) NULL,
	[ToolingNo] [nvarchar](100) NULL,
	[PackingBoxType] [nvarchar](50) NULL,
	[CustomeUsePb] [nvarchar](500) NULL,
	[Actived] [bit] NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedMachine] [nvarchar](100) NULL,
 CONSTRAINT [PK_tblWinlineProductsInfo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblCoreDataCodeItemSize]    Script Date: 2/13/2023 4:08:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblCoreDataCodeItemSize](
	[Id] [uniqueidentifier] NOT NULL,
	[CodeItemSize] [nvarchar](100) NULL,
	[MainItemName] [nvarchar](max) NULL,
	[MetalScan] [int] NULL,
	[Color] [nvarchar](max) NULL,
	[Printing] [int] NULL,
	[Date] [date] NULL,
	[Size] [nvarchar](100) NULL,
	[AveWeight1Prs] [float] NULL,
	[BoxQtyBx1] [int] NULL,
	[BoxQtyBx2] [int] NULL,
	[BoxQtyBx3] [int] NULL,
	[BoxQtyBx4] [int] NULL,
	[BoxWeightBx1] [float] NULL,
	[BoxWeightBx2] [float] NULL,
	[BoxWeightBx3] [float] NULL,
	[BoxWeightBx4] [float] NULL,
	[PartitionQty] [int] NULL,
	[PlasicBag1Qty] [int] NULL,
	[PlasicBag2Qty] [int] NULL,
	[WrapSheetQty] [int] NULL,
	[FoamSheetQty] [int] NULL,
	[PartitionWeight] [float] NULL,
	[PlasicBag1Weight] [float] NULL,
	[PlasicBag2Weight] [float] NULL,
	[WrapSheetWeight] [float] NULL,
	[FoamSheetWeight] [float] NULL,
	[PlasicBoxWeight] [float] NULL,
	[Tolerance] [float] NULL,
	[ToleranceAfterPrint] [float] NULL,
	[CreatedDate] [datetime] NULL,
	[IsActived] [bit] NULL,
 CONSTRAINT [PK_tblCoreDataCodeItemSize] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[v_productItemInfo]    Script Date: 2/13/2023 4:08:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE view [dbo].[v_productItemInfo]
as
SELECT c.CodeItemSize,
    v.ProductNumber,
    v.ProductName,
    v.ProductCategory,
    v.Brand,
    v.Decoration,
    c.MetalScan,
    c.Printing,
    v.MainProductNo,
    v.MainProductName,
    v.Color,
    v.SizeName,
    v.ToolingNo,
    c.MainItemName,
    c.AveWeight1Prs,
    c.BoxQtyBx1,
    c.BoxQtyBx2,
    c.BoxQtyBx3,
    c.BoxQtyBx4,
    c.BoxWeightBx1,
    c.BoxWeightBx2,
    c.BoxWeightBx3,
    c.BoxWeightBx4,
    c.PartitionQty,
    c.PlasicBag1Qty,
	c.PlasicBag2Qty,
    c.WrapSheetQty,
	c.FoamSheetQty,
    c.PartitionWeight,
    c.PlasicBag1Weight,
	c.PlasicBag2Weight,
    c.WrapSheetWeight,
	c.FoamSheetWeight,
    c.PlasicBoxWeight,
    c.Tolerance,
    c.ToleranceAfterPrint
FROM tblWinlineProductsInfo v
    left join tblCoreDataCodeItemSize c 
        on c.CodeItemSize=(CASE
                            WHEN (select COUNT(CodeItemSize) from tblCoreDataCodeItemSize where CodeItemSize = v.ProductNumber) >0 then v.ProductNumber
                            ELSE v.CodeItemSize
                            END
                            )
            and c.Printing = v.Decoration
            and c.IsActived = 1
WHERE v.Actived =1
GO
/****** Object:  Table [dbo].[tbl_KhachHang]    Script Date: 2/13/2023 4:08:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_KhachHang](
	[Id] [uniqueidentifier] NOT NULL,
	[TenKH] [nvarchar](500) NULL,
	[DiaChi] [nvarchar](500) NULL,
	[IsActive] [int] NULL,
	[CreatedDate] [datetime] NULL,
 CONSTRAINT [PK_tbl_KhachHang] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblApprovedPrintLabel]    Script Date: 2/13/2023 4:08:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblApprovedPrintLabel](
	[Id] [uniqueidentifier] NULL,
	[QrCode] [uniqueidentifier] NULL,
	[IdLabel] [nvarchar](max) NULL,
	[OC] [nvarchar](100) NULL,
	[BoxNo] [nvarchar](50) NULL,
	[GrossWeight] [float] NULL,
	[Station] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedMachine] [nvarchar](100) NULL,
	[QRLabel] [nvarchar](500) NULL,
	[ApproveType] [nvarchar](100) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblDanhSachDonHang]    Script Date: 2/13/2023 4:08:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblDanhSachDonHang](
	[Id] [uniqueidentifier] NOT NULL,
	[STT] [int] NULL,
	[Ma] [nvarchar](50) NULL,
	[Song] [nvarchar](50) NULL,
	[Kho] [nvarchar](50) NULL,
	[ChieuDai] [nvarchar](50) NULL,
	[SoLuong] [nvarchar](50) NULL,
	[Pallet] [nvarchar](50) NULL,
	[Xa] [nvarchar](50) NULL,
	[NapC] [nvarchar](50) NULL,
	[Cao] [nvarchar](50) NULL,
	[NapL] [nvarchar](50) NULL,
	[Lang] [nvarchar](50) NULL,
	[GiayMen] [nvarchar](50) NULL,
	[GiaySong1] [nvarchar](50) NULL,
	[GiayMat1] [nvarchar](50) NULL,
	[GiaySong2] [nvarchar](50) NULL,
	[GiayMat2] [nvarchar](50) NULL,
	[GiaySong3] [nvarchar](50) NULL,
	[GiayMat3] [nvarchar](50) NULL,
	[MayXa] [nvarchar](50) NULL,
	[Line] [nvarchar](50) NULL,
	[GhiChu] [nvarchar](500) NULL,
	[KhachHang] [nvarchar](50) NULL,
	[DaiKH] [nvarchar](50) NULL,
	[RongKH] [nvarchar](50) NULL,
	[CaoKH] [nvarchar](50) NULL,
	[DonHang] [nvarchar](50) NULL,
	[PO] [nvarchar](50) NULL,
	[MayIn] [nvarchar](50) NULL,
	[ChapBe] [nvarchar](50) NULL,
	[GhimDan] [nvarchar](50) NULL,
	[CreatedDate] [datetime] NULL,
	[IsActived] [int] NULL,
	[Status] [int] NULL,
 CONSTRAINT [PK_tblDanhSachDonHang] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblItemMissingInfo]    Script Date: 2/13/2023 4:08:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblItemMissingInfo](
	[Id] [uniqueidentifier] NOT NULL,
	[OcNum] [nvarchar](100) NULL,
	[ProductNumber] [nvarchar](500) NULL,
	[ProductName] [nvarchar](500) NULL,
	[CreatedDate] [datetime] NULL,
	[IsActive] [bit] NULL,
	[Note] [nvarchar](max) NULL,
	[QrCode] [nvarchar](500) NULL,
 CONSTRAINT [PK_tblItemMissingInfo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblLog]    Script Date: 2/13/2023 4:08:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblLog](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Message] [nvarchar](max) NULL,
	[MessageTemplate] [nvarchar](max) NULL,
	[Level] [nvarchar](max) NULL,
	[TimeStamp] [datetime] NULL,
	[Exception] [nvarchar](max) NULL,
	[Properties] [nvarchar](max) NULL,
 CONSTRAINT [PK_tblLog] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblScanData]    Script Date: 2/13/2023 4:08:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblScanData](
	[Id] [uniqueidentifier] NULL,
	[BarcodeString] [nvarchar](500) NULL,
	[IdLable] [nvarchar](100) NULL,
	[OcNo] [nvarchar](100) NULL,
	[ProductNumber] [nvarchar](100) NULL,
	[ProductName] [nvarchar](500) NULL,
	[Quantity] [int] NULL,
	[LinePosNo] [nvarchar](50) NULL,
	[Unit] [nvarchar](50) NULL,
	[BoxNo] [nvarchar](50) NULL,
	[CustomerNo] [nvarchar](500) NULL,
	[Location] [int] NULL,
	[BoxPosNo] [nvarchar](50) NULL,
	[Note] [nvarchar](max) NULL,
	[Brand] [nvarchar](50) NULL,
	[Decoration] [int] NULL,
	[MetalScan] [int] NULL,
	[AveWeight1Prs] [float] NULL,
	[StdNetWeight] [float] NULL,
	[Tolerance] [float] NULL,
	[BoxWeight] [float] NULL,
	[PackageWeight] [float] NULL,
	[StdGrossWeight] [float] NULL,
	[GrossWeight] [float] NULL,
	[NetWeight] [float] NULL,
	[Deviation] [float] NULL,
	[Pass] [int] NULL,
	[Status] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[Actived] [int] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[CalculatedPairs] [int] NULL,
	[DeviationPairs] [int] NULL,
	[Station] [int] NULL,
	[ApprovedBy] [uniqueidentifier] NULL,
	[ActualDeviationPairs] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblUsers]    Script Date: 2/13/2023 4:08:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblUsers](
	[Id] [uniqueidentifier] NOT NULL,
	[UserName] [nvarchar](500) NULL,
	[Password] [nvarchar](max) NULL,
	[Role] [int] NULL,
	[Actived] [int] NULL,
	[CreatedBy] [datetime] NULL,
	[Note] [nvarchar](max) NULL,
	[Approved] [int] NULL,
 CONSTRAINT [PK_tblAccount] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[tbl_KhachHang] ADD  CONSTRAINT [DF_tbl_KhachHang_Id]  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[tbl_KhachHang] ADD  CONSTRAINT [DF_tbl_KhachHang_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[tbl_KhachHang] ADD  CONSTRAINT [DF_tbl_KhachHang_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[tblApprovedPrintLabel] ADD  CONSTRAINT [DF_tblApprovedPrintLabel_Id]  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[tblApprovedPrintLabel] ADD  CONSTRAINT [DF_tblApprovedPrintLabel_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[tblApprovedPrintLabel] ADD  CONSTRAINT [DF_tblApprovedPrintLabel_CreatedMachine]  DEFAULT (host_name()) FOR [CreatedMachine]
GO
ALTER TABLE [dbo].[tblCoreDataCodeItemSize] ADD  CONSTRAINT [DF_tblCoreDataCodeItemSize_Id]  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[tblCoreDataCodeItemSize] ADD  CONSTRAINT [DF_tblCoreDataCodeItemSize_MetalScan]  DEFAULT ((0)) FOR [MetalScan]
GO
ALTER TABLE [dbo].[tblCoreDataCodeItemSize] ADD  CONSTRAINT [DF_tblCoreDataCodeItemSize_Printing]  DEFAULT ((0)) FOR [Printing]
GO
ALTER TABLE [dbo].[tblCoreDataCodeItemSize] ADD  CONSTRAINT [DF_tblCoreDataCodeItemSize_AveWeight1Prs]  DEFAULT ((0)) FOR [AveWeight1Prs]
GO
ALTER TABLE [dbo].[tblCoreDataCodeItemSize] ADD  CONSTRAINT [DF_tblCoreDataCodeItemSize_BoxQtyBx1]  DEFAULT ((0)) FOR [BoxQtyBx1]
GO
ALTER TABLE [dbo].[tblCoreDataCodeItemSize] ADD  CONSTRAINT [DF_tblCoreDataCodeItemSize_BoxQtyBx2]  DEFAULT ((0)) FOR [BoxQtyBx2]
GO
ALTER TABLE [dbo].[tblCoreDataCodeItemSize] ADD  CONSTRAINT [DF_tblCoreDataCodeItemSize_BoxQtyBx3]  DEFAULT ((0)) FOR [BoxQtyBx3]
GO
ALTER TABLE [dbo].[tblCoreDataCodeItemSize] ADD  CONSTRAINT [DF_tblCoreDataCodeItemSize_BoxQtyBx4]  DEFAULT ((0)) FOR [BoxQtyBx4]
GO
ALTER TABLE [dbo].[tblCoreDataCodeItemSize] ADD  CONSTRAINT [DF_tblCoreDataCodeItemSize_BoxWeightBx1]  DEFAULT ((0)) FOR [BoxWeightBx1]
GO
ALTER TABLE [dbo].[tblCoreDataCodeItemSize] ADD  CONSTRAINT [DF_tblCoreDataCodeItemSize_BoxWeightBx2]  DEFAULT ((0)) FOR [BoxWeightBx2]
GO
ALTER TABLE [dbo].[tblCoreDataCodeItemSize] ADD  CONSTRAINT [DF_tblCoreDataCodeItemSize_BoxWeightBx3]  DEFAULT ((0)) FOR [BoxWeightBx3]
GO
ALTER TABLE [dbo].[tblCoreDataCodeItemSize] ADD  CONSTRAINT [DF_tblCoreDataCodeItemSize_BoxWeightBx4]  DEFAULT ((0)) FOR [BoxWeightBx4]
GO
ALTER TABLE [dbo].[tblCoreDataCodeItemSize] ADD  CONSTRAINT [DF_tblCoreDataCodeItemSize_PartitionQty]  DEFAULT ((0)) FOR [PartitionQty]
GO
ALTER TABLE [dbo].[tblCoreDataCodeItemSize] ADD  CONSTRAINT [DF_tblCoreDataCodeItemSize_PlasicBagQty]  DEFAULT ((0)) FOR [PlasicBag1Qty]
GO
ALTER TABLE [dbo].[tblCoreDataCodeItemSize] ADD  CONSTRAINT [DF_tblCoreDataCodeItemSize_WrapSheetQty]  DEFAULT ((0)) FOR [WrapSheetQty]
GO
ALTER TABLE [dbo].[tblCoreDataCodeItemSize] ADD  CONSTRAINT [DF_tblCoreDataCodeItemSize_PartitionWeight]  DEFAULT ((60)) FOR [PartitionWeight]
GO
ALTER TABLE [dbo].[tblCoreDataCodeItemSize] ADD  CONSTRAINT [DF_tblCoreDataCodeItemSize_PlasicBagWeight]  DEFAULT ((0)) FOR [PlasicBag1Weight]
GO
ALTER TABLE [dbo].[tblCoreDataCodeItemSize] ADD  CONSTRAINT [DF_tblCoreDataCodeItemSize_WrapSheetWeight]  DEFAULT ((0)) FOR [WrapSheetWeight]
GO
ALTER TABLE [dbo].[tblCoreDataCodeItemSize] ADD  CONSTRAINT [DF_tblCoreDataCodeItemSize_PlasicBoxWeight]  DEFAULT ((1210)) FOR [PlasicBoxWeight]
GO
ALTER TABLE [dbo].[tblCoreDataCodeItemSize] ADD  CONSTRAINT [DF_tblCoreDataCodeItemSize_Tolerance]  DEFAULT ((5)) FOR [Tolerance]
GO
ALTER TABLE [dbo].[tblCoreDataCodeItemSize] ADD  CONSTRAINT [DF_tblCoreDataCodeItemSize_ToleranceAfterPrint]  DEFAULT ((7)) FOR [ToleranceAfterPrint]
GO
ALTER TABLE [dbo].[tblCoreDataCodeItemSize] ADD  CONSTRAINT [DF_tblCoreDataCodeItemSize_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[tblCoreDataCodeItemSize] ADD  CONSTRAINT [DF_tblCoreDataCodeItemSize_IsActived]  DEFAULT ((1)) FOR [IsActived]
GO
ALTER TABLE [dbo].[tblDanhSachDonHang] ADD  CONSTRAINT [DF_tblDanhSachDonHang_Id]  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[tblDanhSachDonHang] ADD  CONSTRAINT [DF_Table_1_Dai]  DEFAULT ((0)) FOR [ChieuDai]
GO
ALTER TABLE [dbo].[tblDanhSachDonHang] ADD  CONSTRAINT [DF_tblDanhSachDonHang_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[tblDanhSachDonHang] ADD  CONSTRAINT [DF_tblDanhSachDonHang_IsActived]  DEFAULT ((1)) FOR [IsActived]
GO
ALTER TABLE [dbo].[tblDanhSachDonHang] ADD  CONSTRAINT [DF_tblDanhSachDonHang_Status]  DEFAULT ((0)) FOR [Status]
GO
ALTER TABLE [dbo].[tblItemMissingInfo] ADD  CONSTRAINT [DF_tblItemMissingInfo_Id]  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[tblItemMissingInfo] ADD  CONSTRAINT [DF_tblItemMissingInfo_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[tblItemMissingInfo] ADD  CONSTRAINT [DF_tblItemMissingInfo_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[tblScanData] ADD  CONSTRAINT [DF_tblScanData_Id]  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[tblScanData] ADD  CONSTRAINT [DF_tblScanData_NetWeight]  DEFAULT ((0)) FOR [StdNetWeight]
GO
ALTER TABLE [dbo].[tblScanData] ADD  CONSTRAINT [DF_tblScanData_BoxWeight]  DEFAULT ((0)) FOR [BoxWeight]
GO
ALTER TABLE [dbo].[tblScanData] ADD  CONSTRAINT [DF_tblScanData_AccessoriesWeight]  DEFAULT ((0)) FOR [PackageWeight]
GO
ALTER TABLE [dbo].[tblScanData] ADD  CONSTRAINT [DF_tblScanData_GrossWeight]  DEFAULT ((0)) FOR [StdGrossWeight]
GO
ALTER TABLE [dbo].[tblScanData] ADD  CONSTRAINT [DF_tblScanData_RealWeight]  DEFAULT ((0)) FOR [GrossWeight]
GO
ALTER TABLE [dbo].[tblScanData] ADD  CONSTRAINT [DF_tblScanData_RealNetWeight]  DEFAULT ((0)) FOR [NetWeight]
GO
ALTER TABLE [dbo].[tblScanData] ADD  CONSTRAINT [DF_tblScanData_Status]  DEFAULT ((0)) FOR [Status]
GO
ALTER TABLE [dbo].[tblScanData] ADD  CONSTRAINT [DF_tblScanData_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[tblScanData] ADD  CONSTRAINT [DF_tblScanData_Actived]  DEFAULT ((1)) FOR [Actived]
GO
ALTER TABLE [dbo].[tblScanData] ADD  CONSTRAINT [DF_tblScanData_CalculatedPairs]  DEFAULT ((0)) FOR [CalculatedPairs]
GO
ALTER TABLE [dbo].[tblScanData] ADD  CONSTRAINT [DF_tblScanData_DeviationPairs]  DEFAULT ((0)) FOR [DeviationPairs]
GO
ALTER TABLE [dbo].[tblScanData] ADD  CONSTRAINT [DF_tblScanData_Station]  DEFAULT ((0)) FOR [Station]
GO
ALTER TABLE [dbo].[tblScanData] ADD  CONSTRAINT [DF_tblScanData_ActualDeviationPairs]  DEFAULT ((0)) FOR [ActualDeviationPairs]
GO
ALTER TABLE [dbo].[tblUsers] ADD  CONSTRAINT [DF_tblAccount_Id]  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[tblUsers] ADD  CONSTRAINT [DF_tblAccount_Role]  DEFAULT ((0)) FOR [Role]
GO
ALTER TABLE [dbo].[tblUsers] ADD  CONSTRAINT [DF_tblUsers_Actived]  DEFAULT ((1)) FOR [Actived]
GO
ALTER TABLE [dbo].[tblUsers] ADD  CONSTRAINT [DF_tblAccount_CreatedBy]  DEFAULT (getdate()) FOR [CreatedBy]
GO
ALTER TABLE [dbo].[tblUsers] ADD  CONSTRAINT [DF_tblUsers_Approved]  DEFAULT ((0)) FOR [Approved]
GO
ALTER TABLE [dbo].[tblWinlineProductsInfo] ADD  CONSTRAINT [DF_tblWinlineProductsInfo_Id]  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[tblWinlineProductsInfo] ADD  CONSTRAINT [DF_tblWinlineProductsInfo_Actived]  DEFAULT ((1)) FOR [Actived]
GO
ALTER TABLE [dbo].[tblWinlineProductsInfo] ADD  CONSTRAINT [DF_tblWinlineProductsInfo_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[tblWinlineProductsInfo] ADD  CONSTRAINT [DF_tblWinlineProductsInfo_CreatedMachine]  DEFAULT (host_name()) FOR [CreatedMachine]
GO
/****** Object:  StoredProcedure [dbo].[sp_MissingInfoGets]    Script Date: 2/13/2023 4:08:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_MissingInfoGets]
	-- Add the parameters for the stored procedure here
	@FromDate DATETIME,
	@ToDate DATETIME
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT OcNum
		,ProductNumber
		,ProductName
		,QrCode
		,CreatedDate
	FROM [IDCScaleSystem].[dbo].[tblItemMissingInfo] 
	WHERE CreatedDate >= @FromDate 
				and CreatedDate <= @ToDate
	ORDER by CreatedDate desc;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_ProductItemGetSpecialCase]    Script Date: 2/13/2023 4:08:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_ProductItemGetSpecialCase]
	-- Add the parameters for the stored procedure here
	@ProductNumber nvarchar(100),
	@Printing int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT c.CodeItemSize,
		v.ProductNumber,
		v.ProductName,
		v.ProductCategory,
		v.Brand,
		v.Decoration,
		c.MetalScan,
		c.Printing,
		v.MainProductNo,
		v.MainProductName,
		v.Color,
		v.SizeName,
		v.ToolingNo,
		c.MainItemName,
		c.AveWeight1Prs,
		c.BoxQtyBx1,
		c.BoxQtyBx2,
		c.BoxQtyBx3,
		c.BoxQtyBx4,
		c.BoxWeightBx1,
		c.BoxWeightBx2,
		c.BoxWeightBx3,
		c.BoxWeightBx4,
		c.PartitionQty,
		c.PlasicBag1Qty,
		c.PlasicBag2Qty,
		c.WrapSheetQty,
		c.FoamSheetQty,
		c.PartitionWeight,
		c.PlasicBag1Weight,
		c.PlasicBag2Weight,
		c.WrapSheetWeight,
		c.FoamSheetWeight,
		c.PlasicBoxWeight,
		c.Tolerance,
		c.ToleranceAfterPrint
	FROM tblWinlineProductsInfo v
		left join tblCoreDataCodeItemSize c 
			on c.CodeItemSize=(CASE
								WHEN (select COUNT(CodeItemSize) from tblCoreDataCodeItemSize where CodeItemSize = v.ProductNumber) >0 then v.ProductNumber
								ELSE v.CodeItemSize
								END
								)
				and c.Printing = @Printing
				and c.IsActived = 1
	WHERE v.Actived =1 and v.ProductNumber = @ProductNumber
END
GO
/****** Object:  StoredProcedure [dbo].[sp_tblApprovedPrintLabelInsert]    Script Date: 2/13/2023 4:08:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_tblApprovedPrintLabelInsert]
	-- Add the parameters for the stored procedure here
	@QrCode nvarchar(max),
	@IdLabel nvarchar(100),
	@OC nvarchar(100),
	@BoxNo Nvarchar(100),
	@GrossWeight float,
	@Station int,
	@QRLabel nvarchar(500)
	,@ApproveType nvarchar(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	--SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO [dbo].[tblApprovedPrintLabel]
           (
           [QrCode]
           ,[IdLabel]
           ,[OC]
           ,[BoxNo]
           ,[GrossWeight]
		   ,[Station]
		   ,[QRLabel]
		   ,[ApproveType]
		   )
     VALUES
           (
           @QrCode,
		   @IdLabel,
		   @OC,
		   @BoxNo,
		   @GrossWeight,
		   @Station,
		   @QRLabel
		   ,@ApproveType
		   )
END
GO
/****** Object:  StoredProcedure [dbo].[sp_tblApprovedPrintLableSelect]    Script Date: 2/13/2023 4:08:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_tblApprovedPrintLableSelect]
	-- Add the parameters for the stored procedure here
	@FromDate DATETIME,
	@ToDate DATETIME,
	@Station nvarchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	IF (@Station = 'All')
		BEGIN
			SELECT _approved.*,
				_user.UserName
			FROM [dbo].[tblApprovedPrintLabel] _approved
				LEFT JOIN tblUsers _user on _user.Id = _approved.QrCode
			WHERE _approved.CreatedDate >= @FromDate 
				and _approved.CreatedDate <= @ToDate
			ORDER BY _approved.CreatedDate desc;
		END
	ELSE
		BEGIN
			SELECT _approved.*,
				_user.UserName
			FROM [dbo].[tblApprovedPrintLabel] _approved
				LEFT JOIN tblUsers _user on _user.Id = _approved.QrCode
			WHERE _approved.CreatedDate >= @FromDate 
				and _approved.CreatedDate <= @ToDate
				and _approved.Station = (
											CASE
											WHEN @Station ='IDC_1' THEN 0
											WHEN @Station ='IDC_2' THEN 1
											ELSE 2--fVN Kerry
											END
										)
			ORDER BY _approved.CreatedDate desc;
		END
END
GO
/****** Object:  StoredProcedure [dbo].[sp_tblCoreDataCodeitemSizeGets]    Script Date: 2/13/2023 4:08:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_tblCoreDataCodeitemSizeGets]
	-- Add the parameters for the stored procedure here
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * from tblCoreDataCodeItemSize order by CreatedDate asc
END
GO
/****** Object:  StoredProcedure [dbo].[sp_tblCoreDataCodeitemSizeInsert]    Script Date: 2/13/2023 4:08:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_tblCoreDataCodeitemSizeInsert]
	-- Add the parameters for the stored procedure here
	@CodeItemSize nvarchar(100),
	@MainItemName nvarchar(max),
	@MetalScan int,
	@Color nvarchar(max),
	@Printing int,
	@Date date,
	@Size nvarchar(50),
	@AveWeight1Prs float,
	@BoxQtyBx1 int,
	@BoxQtyBx2 int,
	@BoxQtyBx3 int,
	@BoxQtyBx4 int,
	@BoxWeightBx1 float,
	@BoxWeightBx2 float,
	@BoxWeightBx3 float,
	@BoxWeightBx4 float,
	@PartitionQty int,
	@PlasicBag1Qty int,
	@PlasicBag2Qty int,
	@WrapSheetQty int,
	@FoamSheetQty int,
	@PartitionWeight float,
	@PlasicBag1Weight float,
	@PlasicBag2Weight float,
	@WrapSheetWeight float,
	@FoamSheetWeight float,
	@PlasicBoxWeight float,
	@Tolerance float,
	@ToleranceAfterPrint float
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.

    -- Insert statements for procedure here
	INSERT INTO [dbo].[tblCoreDataCodeItemSize]
           (
           [CodeItemSize]
           ,[MainItemName]
		   ,[MetalScan]
		   ,[Color]
		   ,[Printing]
           ,[Date]
           ,[Size]
           ,[AveWeight1Prs]
           ,[BoxQtyBx1]
           ,[BoxQtyBx2]
           ,[BoxQtyBx3]
           ,[BoxQtyBx4]
           ,[BoxWeightBx1]
           ,[BoxWeightBx2]
           ,[BoxWeightBx3]
           ,[BoxWeightBx4]
           ,[PartitionQty]
           ,[PlasicBag1Qty]
		   ,[PlasicBag2Qty]
           ,[WrapSheetQty]
		   ,[FoamSheetQty]
           ,[PartitionWeight]
           ,[PlasicBag1Weight]
		   ,[PlasicBag2Weight]
           ,[WrapSheetWeight]
		   ,[FoamSheetWeight]
           ,[PlasicBoxWeight]
           ,[Tolerance]
           ,[ToleranceAfterPrint])
     VALUES
           (
				@CodeItemSize,
				@MainItemName,
				@MetalScan,
				@Color,
				@Printing,
				@Date,
				@Size,
				@AveWeight1Prs,
				@BoxQtyBx1,
				@BoxQtyBx2,
				@BoxQtyBx3,
				@BoxQtyBx4,
				@BoxWeightBx1,
				@BoxWeightBx2,
				@BoxWeightBx3,
				@BoxWeightBx4,
				@PartitionQty,
				@PlasicBag1Qty,
				@PlasicBag2Qty,
				@WrapSheetQty,
				@FoamSheetQty,
				@PartitionWeight,
				@PlasicBag1Weight,
				@PlasicBag2Weight,
				@WrapSheetWeight,
				@FoamSheetWeight,
				@PlasicBoxWeight,
				@Tolerance,
				@ToleranceAfterPrint
		   )
END
GO
/****** Object:  StoredProcedure [dbo].[sp_tblCoreDataCodeItemSizeUpdate]    Script Date: 2/13/2023 4:08:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_tblCoreDataCodeItemSizeUpdate]
	-- Add the parameters for the stored procedure here
	@CodeItemSize nvarchar(100),
	@MainItemName nvarchar(max),
	@MetalScan int,
	@Color nvarchar(max),
	@Printing int,
	--@Date date,
	@Size nvarchar(50),
	@AveWeight1Prs float,
	@BoxQtyBx1 int,
	@BoxQtyBx2 int,
	@BoxQtyBx3 int,
	@BoxQtyBx4 int,
	@BoxWeightBx1 float,
	@BoxWeightBx2 float,
	@BoxWeightBx3 float,
	@BoxWeightBx4 float,
	@PartitionQty int,
	@PlasicBag1Qty int,
	@PlasicBag2Qty int,
	@WrapSheetQty int,
	@FoamSheetQty int,
	@PartitionWeight float,
	@PlasicBag1Weight float,
	@PlasicBag2Weight float,
	@WrapSheetWeight float,
	@FoamSheetWeight float,
	@PlasicBoxWeight float,
	@Tolerance float,
	@ToleranceAfterPrint float
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	
    -- Insert statements for procedure here
	UPDATE tblCoreDataCodeItemSize
	SET
		MainItemName=@MainItemName,
		MetalScan = @MetalScan,
		Color = @Color,
		Printing = @Printing,
		--Date=@Date,
		Size=@Size,
		AveWeight1Prs=@AveWeight1Prs,
		BoxQtyBx1=@BoxQtyBx1,
		BoxQtyBx2=@BoxQtyBx2,
		BoxQtyBx3=@BoxQtyBx3,
		BoxQtyBx4=@BoxQtyBx4,
		BoxWeightBx1=@BoxWeightBx1,
		BoxWeightBx2=@BoxWeightBx2,
		BoxWeightBx3=@BoxWeightBx3,
		BoxWeightBx4=@BoxWeightBx4,
		PartitionQty=@PartitionQty,
		PlasicBag1Qty=@PlasicBag1Qty,
		PlasicBag2Qty=@PlasicBag2Qty,
		WrapSheetQty=@WrapSheetQty,
		FoamSheetQty=@FoamSheetQty,
		PartitionWeight=@PartitionWeight,
		PlasicBag1Weight=@PlasicBag1Weight,
		PlasicBag2Weight=@PlasicBag2Weight,
		WrapSheetWeight=@WrapSheetWeight,
		FoamSheetWeight=@FoamSheetWeight,
		PlasicBoxWeight=@PlasicBoxWeight,
		Tolerance=@Tolerance,
		ToleranceAfterPrint=@ToleranceAfterPrint
	WHERE
		CodeItemSize = @CodeItemSize
		and Printing = @Printing
END
GO
/****** Object:  StoredProcedure [dbo].[sp_tblDanhSachDonHangGets]    Script Date: 2/13/2023 4:08:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_tblDanhSachDonHangGets]
	-- Add the parameters for the stored procedure here
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT *
	FROM tblDanhSachDonHang
	ORDER BY STT ASC
END
GO
/****** Object:  StoredProcedure [dbo].[sp_tblDanhSachDonHangInsert]    Script Date: 2/13/2023 4:08:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_tblDanhSachDonHangInsert]
	-- Add the parameters for the stored procedure here
	@STT int,
	@Ma nvarchar(100),
	@Song nvarchar(100),
	@Kho nvarchar(100),
	@ChieuDai nvarchar(100),
	@SoLuong nvarchar(100),
	@Pallet nvarchar(100) = null,
	@Xa nvarchar(100) = null,
	@NapC nvarchar(100) = null,
	@Cao nvarchar(100) = null,
	@NapL nvarchar(100) = null,
	@Lang nvarchar(100) = null,
	@GiayMen nvarchar(100) = null,
	@GiaySong1 nvarchar(100) = null,
	@GiayMat1 nvarchar(100) = null,
	@GiaySong2 nvarchar(100) = null,
	@GiayMat2 nvarchar(100) = null,
	@GiaySong3 nvarchar(100) = null,
	@GiayMat3 nvarchar(100) = null,
	@MayXa nvarchar(100) = null,
	@Line nvarchar(100) = null,
	@GhiChu nvarchar(100) = null,
	@KhachHang nvarchar(100) = null,
	@DaiKH nvarchar(100) = null,
	@RongKH nvarchar(100) = null,
	@CaoKH nvarchar(100) = null,
	@DonHang nvarchar(100) = null,
	@PO nvarchar(100) = null,
	@MayIn nvarchar(100) = null,
	@ChapBe nvarchar(100) = null,
	@GhimDan nvarchar(100) = null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	--SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO [dbo].[tblDanhSachDonHang]
           (
           [STT]
           ,[Ma]
           ,[Song]
           ,[Kho]
           ,[ChieuDai]
           ,[SoLuong]
           ,[Pallet]
           ,[Xa]
           ,[NapC]
           ,[Cao]
           ,[NapL]
           ,[Lang]
           ,[GiayMen]
           ,[GiaySong1]
           ,[GiayMat1]
           ,[GiaySong2]
           ,[GiayMat2]
           ,[GiaySong3]
           ,[GiayMat3]
           ,[MayXa]
           ,[Line]
           ,[GhiChu]
           ,[KhachHang]
           ,[DaiKH]
           ,[RongKH]
           ,[CaoKH]
           ,[DonHang]
           ,[PO]
           ,[MayIn]
           ,[ChapBe]
           ,[GhimDan])
     VALUES
           (
		   @STT,
		   @Ma,
		   @Song,
		   @Kho,
		   @ChieuDai,
		   @SoLuong,
		   @Pallet,
		   @Xa,
		   @NapC,
		   @Cao,
		   @NapL,
		   @Lang,
		   @GiayMen,
		   @GiaySong1,
		   @GiayMat1,
		   @GiaySong2,
		   @GiayMat2,
		   @GiaySong3,
		   @GiayMat3,
		   @MayXa,
		   @Line,
		   @GhiChu,
		   @KhachHang,
		   @DaiKH,
		   @RongKH,
		   @CaoKH,
		   @DonHang,
		   @PO,
		   @MayIn,
		   @ChapBe,
		   @GhimDan
		   )
END
GO
/****** Object:  StoredProcedure [dbo].[sp_tblItemMissingInfoInsert]    Script Date: 2/13/2023 4:08:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_tblItemMissingInfoInsert]
	-- Add the parameters for the stored procedure here
	@ProductNumber nvarchar(100),
	@ProductName nvarchar(500),
	@OcNum nvarchar(100),
	@Note nvarchar(max),
	@QrCode nvarchar(500)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.


    -- Insert statements for procedure here
	INSERT INTO [dbo].[tblItemMissingInfo]
           (
           [ProductNumber]
           ,[ProductName]
		   ,[OcNum]
		   ,[Note]
		   ,[QrCode])
     VALUES
           (
				@ProductNumber,
				@ProductName,
				@OcNum,
				@Note,
				@QrCode
		   )
END
GO
/****** Object:  StoredProcedure [dbo].[sp_tblScanDataCheck]    Script Date: 2/13/2023 4:08:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_tblScanDataCheck]
	-- Add the parameters for the stored procedure here
	@QRLabel nvarchar(max)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT Pass ,ApprovedBy,ActualDeviationPairs,Status
	FROM tblScanData
	WHERE BarcodeString = @QRLabel 
			AND Actived = 1
	GROUP BY Pass,ApprovedBy,ActualDeviationPairs, Status
END
GO
/****** Object:  StoredProcedure [dbo].[sp_tblScanDataGetForApprovedPrint]    Script Date: 2/13/2023 4:08:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_tblScanDataGetForApprovedPrint]
	-- Add the parameters for the stored procedure here
	@QRLabel nvarchar(max)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT TOP (1) * 
	FROM tblScanData
	WHERE BarcodeString = @QRLabel 
			AND Actived = 1
			AND Pass = 0
	ORDER BY CreatedDate desc
END
GO
/****** Object:  StoredProcedure [dbo].[sp_tblScanDataGets]    Script Date: 2/13/2023 4:08:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_tblScanDataGets]
	-- Add the parameters for the stored procedure here
	@FromDate DATETIME,
	@ToDate DATETIME,
	@Station nvarchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	IF (@Station = 'All')
		BEGIN
			SELECT _scanData.*,
				_user.UserName
			FROM tblScanData _scanData
				left join tblUsers _user on _user.Id = _scanData.CreatedBy
			WHERE _scanData.CreatedDate >= @FromDate 
				and _scanData.CreatedDate <= @ToDate
				and _scanData.Actived = 1
			ORDER BY CreatedDate desc;
		END
	ELSE
		BEGIN
			SELECT _scanData.*,
			_user.UserName
			FROM tblScanData _scanData
				left join tblUsers _user on _user.Id = _scanData.CreatedBy
			WHERE _scanData.CreatedDate >= @FromDate 
				and _scanData.CreatedDate <= @ToDate
				and _scanData.Station = (
											CASE
											WHEN @Station ='IDC_1' THEN 0
											WHEN @Station ='IDC_2' THEN 1
											ELSE 2--fVN Kerry
											END
										)
				and _scanData.Actived = 1
			ORDER BY CreatedDate desc;
		END
END
GO
/****** Object:  StoredProcedure [dbo].[sp_tblScanDataInsert]    Script Date: 2/13/2023 4:08:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_tblScanDataInsert]
	-- Add the parameters for the stored procedure here
	@BarcodeString nvarchar(100),
	@IdLable nvarchar(100),
	@OcNo nvarchar(50),
	@ProductNumber nvarchar(100),
	@ProductName nvarchar(max),
	@Quantity int,
	@LinePosNo int,
	@Unit nvarchar(10),
	@BoxNo nvarchar(10),
	@CustomerNo nvarchar(50),
	@Location int,
	@BoxPosNo nvarchar(10),
	@Note nvarchar(Max) = null,
	@Brand nvarchar(100),
	@Decoration int,
	@MetalScan int,
	@AveWeight1Prs float,
	@StdNetWeight float,
	@Tolerance float,
	@Boxweight float,
	@PackageWeight float,
	@StdGrossweight float,
	@GrossWeight float,
	@NetWeight float,
	@Deviation float,
	@Pass int,
	@Status int,
	@CalculatedPairs int,
	@DeviationPairs int,
	@CreatedBy uniqueidentifier,
	@Station int,
	@CreatedDate datetime = null,
	@ApprovedBy uniqueidentifier =null
	,@ActualDeviationPairs int
	--@Id nvarchar(500) OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	--SET NOCOUNT ON;
    -- Insert statements for procedure here
	INSERT INTO [dbo].[tblScanData]
           (
			   [BarcodeString]
			   ,[IdLable]
			   ,[OcNo]
			   ,[ProductNumber]
			   ,[ProductName]
			   ,[Quantity]
			   ,[LinePosNo]
			   ,[Unit]
			   ,[BoxNo]
			   ,[CustomerNo]
			   ,[Location]
			   ,[BoxPosNo]
			   ,[Note]
			   ,[Brand]
			   ,[Decoration]
			   ,[MetalScan]
			   ,[AveWeight1Prs]
			   ,[Tolerance]
			   ,[StdNetWeight]
			   ,[BoxWeight]
			   ,[PackageWeight]
			   ,[StdGrossWeight]
			   ,[GrossWeight]
			   ,[NetWeight]
			   ,[Deviation]
			   ,[Pass]
			   ,[Status]
			   ,[CalculatedPairs]
			   ,[DeviationPairs]
			   ,[CreatedBy]
			   ,[Station]
			   ,[CreatedDate]
			   ,[ApprovedBy]
			   ,[ActualDeviationPairs]
		   )
     VALUES
           (
				@BarcodeString,
				@IdLable,
				@OcNo,
				@ProductNumber,
				@ProductName,
				@Quantity,
				@LinePosNo,
				@Unit,
				@BoxNo,
				@CustomerNo,
				@Location,
				@BoxPosNo,
				@Note,
				@Brand,
				@Decoration,
				@MetalScan,
				@AveWeight1Prs,
				@Tolerance,
				@StdNetWeight,
				@Boxweight,
				@PackageWeight,
				@StdGrossweight,
				@GrossWeight,
				@NetWeight,
				@Deviation,
				@Pass,
				@Status,
				@CalculatedPairs,
				@DeviationPairs,
				@CreatedBy,
				@Station,
				@CreatedDate,
				@ApprovedBy,
				@ActualDeviationPairs
		   )

	--SET @Id = convert(nvarchar(500), (SELECT TOP (1) Id FROM tblScanData order by CreatedDate desc))
END
GO
/****** Object:  StoredProcedure [dbo].[sp_tblScanDataUpdateApproveBy]    Script Date: 2/13/2023 4:08:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_tblScanDataUpdateApproveBy]
	-- Add the parameters for the stored procedure here
	@Id uniqueidentifier
	,@ApproveBy uniqueidentifier
	,@ActualDeviationPairs int =0
	,@GrossWeight float
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	--SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE tblScanData SET ApprovedBy = @ApproveBy, ActualDeviationPairs = @ActualDeviationPairs, GrossWeight = @GrossWeight
	WHERE Id = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[sp_tblUserGet]    Script Date: 2/13/2023 4:08:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_tblUserGet]
	-- Add the parameters for the stored procedure here
	@Id uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * from tblUsers where id = @id
END
GO
/****** Object:  StoredProcedure [dbo].[sp_tblWinlineProductsInfoGet]    Script Date: 2/13/2023 4:08:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_tblWinlineProductsInfoGet] 
	-- Add the parameters for the stored procedure here
	@productCode nvarchar(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT *
	FROM tblWinlineProductsInfo
	WHERE ProductNunmber = @productCode
END
GO
/****** Object:  StoredProcedure [dbo].[sp_tblWinlineProductsInfoGetId]    Script Date: 2/13/2023 4:08:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
create PROCEDURE [dbo].[sp_tblWinlineProductsInfoGetId] 
	-- Add the parameters for the stored procedure here
	@Id uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT *
	FROM tblWinlineProductsInfo
	WHERE Id = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[sp_tblWinlineProductsInfoGets]    Script Date: 2/13/2023 4:08:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_tblWinlineProductsInfoGets]
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT *
	FROM tblWinlineProductsInfo
END
GO
/****** Object:  StoredProcedure [dbo].[sp_tblWinlineProductsInfoInsert]    Script Date: 2/13/2023 4:08:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_tblWinlineProductsInfoInsert]
	-- Add the parameters for the stored procedure here
	@ProductNunmber nvarchar(500),
	@ProductName nvarchar(500),
	@ProductCategory int,
	@Brand nvarchar(100),
	@Decoration int,
	@MainProductNo nvarchar(100),
	@MainProductName nvarchar(max),
	@Color nvarchar(100),
	@SizeCode nvarchar(100),
	@SizeName nvarchar(100),
	@Weight float,
	@LeftWeight float,
	@RightWeight float,
	@BoxType nvarchar(100),
	@ToolingNo nvarchar(100),
	@PackingBoxType nvarchar(100),
	@CustomeUsePb nvarchar(100),
	@CodeItemSize nvarchar(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.

    -- Insert statements for procedure here
	INSERT INTO [dbo].[tblWinlineProductsInfo]
           (
           [CodeItemSize]
           ,[ProductNumber]
           ,[ProductName]
           ,[ProductCategory]
           ,[Brand]
           ,[Decoration]
           ,[MainProductNo]
		   ,[MainProductName]
           ,[Color]
           ,[SizeCode]
           ,[SizeName]
           ,[Weight]
           ,[LeftWeight]
           ,[RightWeight]
           ,[BoxType]
           ,[ToolingNo]
           ,[PackingBoxType]
           ,[CustomeUsePb]
           )
     VALUES
           (
			@CodeItemSize,
			@ProductNunmber,
			@ProductName ,
			@ProductCategory,
			@Brand ,
			@Decoration ,
			@MainProductNo ,
			@MainProductName,
			@Color ,
			@SizeCode ,
			@SizeName,
			@Weight ,
			@LeftWeight ,
			@RightWeight ,
			@BoxType ,
			@ToolingNo ,
			@PackingBoxType ,
			@CustomeUsePb
		   )
END
GO
/****** Object:  StoredProcedure [dbo].[sp_tblWinlineProductsInfoUpdateTolerance]    Script Date: 2/13/2023 4:08:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_tblWinlineProductsInfoUpdateTolerance]
	-- Add the parameters for the stored procedure here
	@Id uniqueidentifier,
	@Tolerance float,
	@ToleranceBeforePrint float,
	@ToleranceAfterPrint float
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	
    -- Insert statements for procedure here
	update tblWinlineProductsInfo set Tolerance = @Tolerance, ToleranceBeforePrint = @ToleranceBeforePrint, ToleranceAfterPrint = @ToleranceAfterPrint
	where Id = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[sp_UsersLogin]    Script Date: 2/13/2023 4:08:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_UsersLogin]
	-- Add the parameters for the stored procedure here
	@userName nvarchar(500)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * from tblUsers where Actived = 1 and UserName = @userName
END
GO
/****** Object:  StoredProcedure [dbo].[sp_vProductItemInfoGet]    Script Date: 2/13/2023 4:08:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_vProductItemInfoGet]
	-- Add the parameters for the stored procedure here
	@ProductNumber nvarchar(100),
	@SpecialCase int,
	@Printing int = null--chi dung cho truong hop SpecialCase = true
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	IF (@SpecialCase = 0)
		BEGIN
			--print('Binh thuong');
			--SELECT * from v_productItemInfo where ProductNumber = @ProductNumber
			--ORDER BY CodeItemSize ASC

			SELECT c.CodeItemSize,
				v.ProductNumber,
				v.ProductName,
				v.ProductCategory,
				v.Brand,
				v.Decoration,
				c.MetalScan,
				c.Printing,
				v.MainProductNo,
				v.MainProductName,
				v.Color,
				v.SizeName,
				v.ToolingNo,
				c.MainItemName,
				c.AveWeight1Prs,
				c.BoxQtyBx1,
				c.BoxQtyBx2,
				c.BoxQtyBx3,
				c.BoxQtyBx4,
				c.BoxWeightBx1,
				c.BoxWeightBx2,
				c.BoxWeightBx3,
				c.BoxWeightBx4,
				c.PartitionQty,
				c.PlasicBag1Qty,
				c.PlasicBag2Qty,
				c.WrapSheetQty,
				c.FoamSheetQty,
				c.PartitionWeight,
				c.PlasicBag1Weight,
				c.PlasicBag2Weight,
				c.WrapSheetWeight,
				c.FoamSheetWeight,
				c.PlasicBoxWeight,
				c.Tolerance,
				c.ToleranceAfterPrint
			FROM tblWinlineProductsInfo v
				left join tblCoreDataCodeItemSize c 
					on c.CodeItemSize=(CASE
										WHEN (select COUNT(CodeItemSize) from tblCoreDataCodeItemSize where CodeItemSize = v.ProductNumber) >0 then v.ProductNumber
										ELSE v.CodeItemSize
										END
										)
						and c.Printing = v.Decoration
						and c.IsActived = 1
			WHERE v.Actived =1 and v.ProductNumber = @ProductNumber
		END
	ELSE
		BEGIN
			--print('Special case');

			SELECT c.CodeItemSize,
				v.ProductNumber,
				v.ProductName,
				v.ProductCategory,
				v.Brand,
				v.Decoration,
				c.MetalScan,
				c.Printing,
				v.MainProductNo,
				v.MainProductName,
				v.Color,
				v.SizeName,
				v.ToolingNo,
				c.MainItemName,
				c.AveWeight1Prs,
				c.BoxQtyBx1,
				c.BoxQtyBx2,
				c.BoxQtyBx3,
				c.BoxQtyBx4,
				c.BoxWeightBx1,
				c.BoxWeightBx2,
				c.BoxWeightBx3,
				c.BoxWeightBx4,
				c.PartitionQty,
				c.PlasicBag1Qty,
				c.PlasicBag2Qty,
				c.WrapSheetQty,
				c.FoamSheetQty,
				c.PartitionWeight,
				c.PlasicBag1Weight,
				c.PlasicBag2Weight,
				c.WrapSheetWeight,
				c.FoamSheetWeight,
				c.PlasicBoxWeight,
				c.Tolerance,
				c.ToleranceAfterPrint
			FROM tblWinlineProductsInfo v
				left join tblCoreDataCodeItemSize c 
					on c.CodeItemSize=(CASE
										WHEN (select COUNT(CodeItemSize) from tblCoreDataCodeItemSize where CodeItemSize = v.ProductNumber) >0 then v.ProductNumber
										ELSE v.CodeItemSize
										END
										)
						and c.Printing = @Printing
						and c.IsActived = 1
			WHERE v.Actived =1 and v.ProductNumber = @ProductNumber
		END
END
GO
/****** Object:  StoredProcedure [dbo].[sp_vProductItemInfoGets]    Script Date: 2/13/2023 4:08:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_vProductItemInfoGets]
	-- Add the parameters for the stored procedure here
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * from v_productItemInfo ORDER BY ProductNumber asc
END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'UserId' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblApprovedPrintLabel', @level2type=N'COLUMN',@level2name=N'QrCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Approve cho 3 tường hợp. in lại tem do fall alarm, reprint, ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblApprovedPrintLabel', @level2type=N'COLUMN',@level2name=N'ApproveType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1-Scan; 0-No scan' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblCoreDataCodeItemSize', @level2type=N'COLUMN',@level2name=N'MetalScan'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0-ko in; 1-in' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblCoreDataCodeItemSize', @level2type=N'COLUMN',@level2name=N'Printing'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Gram. Khối lượng của 1 đôi.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblCoreDataCodeItemSize', @level2type=N'COLUMN',@level2name=N'AveWeight1Prs'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Prs' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblCoreDataCodeItemSize', @level2type=N'COLUMN',@level2name=N'BoxQtyBx1'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Prs' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblCoreDataCodeItemSize', @level2type=N'COLUMN',@level2name=N'BoxQtyBx2'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Prs' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblCoreDataCodeItemSize', @level2type=N'COLUMN',@level2name=N'BoxQtyBx3'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Prs' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblCoreDataCodeItemSize', @level2type=N'COLUMN',@level2name=N'BoxQtyBx4'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Gram' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblCoreDataCodeItemSize', @level2type=N'COLUMN',@level2name=N'BoxWeightBx1'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Gram' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblCoreDataCodeItemSize', @level2type=N'COLUMN',@level2name=N'BoxWeightBx2'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Gram' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblCoreDataCodeItemSize', @level2type=N'COLUMN',@level2name=N'BoxWeightBx3'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Gram' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblCoreDataCodeItemSize', @level2type=N'COLUMN',@level2name=N'BoxWeightBx4'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'số đôi (prs) partition này có thể chứa' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblCoreDataCodeItemSize', @level2type=N'COLUMN',@level2name=N'PartitionQty'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'số đôi (prs) plasic này có thể chứa' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblCoreDataCodeItemSize', @level2type=N'COLUMN',@level2name=N'PlasicBag1Qty'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'số đôi (prs) wrapSheet này có thể chứa' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblCoreDataCodeItemSize', @level2type=N'COLUMN',@level2name=N'WrapSheetQty'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'gram. 38x49 ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblCoreDataCodeItemSize', @level2type=N'COLUMN',@level2name=N'PartitionWeight'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Nếu OC có chứa ''PRT%'', thì phải dùng khối lượng thùng nhựa. Gram' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblCoreDataCodeItemSize', @level2type=N'COLUMN',@level2name=N'PlasicBoxWeight'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default +-5%' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblCoreDataCodeItemSize', @level2type=N'COLUMN',@level2name=N'Tolerance'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default +-7%' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblCoreDataCodeItemSize', @level2type=N'COLUMN',@level2name=N'ToleranceAfterPrint'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Mã. chọn' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblDanhSachDonHang', @level2type=N'COLUMN',@level2name=N'Ma'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sóng, chọn' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblDanhSachDonHang', @level2type=N'COLUMN',@level2name=N'Song'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Khổ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblDanhSachDonHang', @level2type=N'COLUMN',@level2name=N'Kho'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Dài' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblDanhSachDonHang', @level2type=N'COLUMN',@level2name=N'ChieuDai'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Xả' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblDanhSachDonHang', @level2type=N'COLUMN',@level2name=N'Xa'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Chạp/Bế' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblDanhSachDonHang', @level2type=N'COLUMN',@level2name=N'ChapBe'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Ghim/Dán' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblDanhSachDonHang', @level2type=N'COLUMN',@level2name=N'GhimDan'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Trạng thái của đơn hàng. 0-mới tạo, chờ sản xuất; 1- đang sản xuất; 2-đã sản xuất xong.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblDanhSachDonHang', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'P/L/R' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblScanData', @level2type=N'COLUMN',@level2name=N'Unit'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1-fVN; 2-fFT' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblScanData', @level2type=N'COLUMN',@level2name=N'Location'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1-Yes; 0-No' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblScanData', @level2type=N'COLUMN',@level2name=N'Decoration'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1- khong quet; 0- quet' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblScanData', @level2type=N'COLUMN',@level2name=N'MetalScan'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Weight*Qtyprs. khối lượng của item.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblScanData', @level2type=N'COLUMN',@level2name=N'StdNetWeight'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'khối luong cho phép lệch. gam' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblScanData', @level2type=N'COLUMN',@level2name=N'Tolerance'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Phụ kiện đóng gói: Partition + PlasicBag + WapSheet' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblScanData', @level2type=N'COLUMN',@level2name=N'PackageWeight'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'NetWeight+BoxWeight+AccessoriesWeight' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblScanData', @level2type=N'COLUMN',@level2name=N'StdGrossWeight'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Gam' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblScanData', @level2type=N'COLUMN',@level2name=N'Deviation'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1- pass; 0-fail' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblScanData', @level2type=N'COLUMN',@level2name=N'Pass'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Báo trạng thái: 0- thùng fail; 1- chờ đi sơn; 2- Done-hàng FG qua kho Kerry. Ở trạm IDC check nêu hàng noPrinting thì set =2. nếu printing set =1. Khi hàng đi sơn về, vào trạm check afterPrinting, quét OK set =2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblScanData', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'chỉ có giá trị khi thùng fail, lưu ID của người approved cho in tem. và actual deviation' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblScanData', @level2type=N'COLUMN',@level2name=N'ApprovedBy'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'số lượng đôi chênh lệch thực tế' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblScanData', @level2type=N'COLUMN',@level2name=N'ActualDeviationPairs'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0-Operator; 1-Admin' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblUsers', @level2type=N'COLUMN',@level2name=N'Role'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Dùng để duyệt in bằng tay với các trường hợp báo Fail mà sau khi kiểm lại cho qua.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblUsers', @level2type=N'COLUMN',@level2name=N'Approved'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'=1 là heelCounter' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblWinlineProductsInfo', @level2type=N'COLUMN',@level2name=N'ProductCategory'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Printing. 0-No; 1-Yes' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblWinlineProductsInfo', @level2type=N'COLUMN',@level2name=N'Decoration'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'gram' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblWinlineProductsInfo', @level2type=N'COLUMN',@level2name=N'LeftWeight'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'gram' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblWinlineProductsInfo', @level2type=N'COLUMN',@level2name=N'RightWeight'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'PackingBox Type(plastic box)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblWinlineProductsInfo', @level2type=N'COLUMN',@level2name=N'PackingBoxType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Pb: plactic box' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblWinlineProductsInfo', @level2type=N'COLUMN',@level2name=N'CustomeUsePb'
GO
USE [master]
GO
ALTER DATABASE [IDCScaleSystem_Test] SET  READ_WRITE 
GO
