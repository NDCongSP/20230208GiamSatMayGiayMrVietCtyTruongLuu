CREATE DATABASE  IF NOT EXISTS `quanlygiay` /*!40100 DEFAULT CHARACTER SET latin1 */;
USE `quanlygiay`;
-- MySQL dump 10.13  Distrib 8.0.24, for Win64 (x86_64)
--
-- Host: localhost    Database: quanlygiay
-- ------------------------------------------------------
-- Server version	5.7.34-log

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `datamaysong`
--

DROP TABLE IF EXISTS `datamaysong`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `datamaysong` (
  `CreatedDate` datetime DEFAULT CURRENT_TIMESTAMP,
  `MaySong` varchar(5) DEFAULT NULL,
  `GiayMat` float DEFAULT NULL,
  `GiaySong` float DEFAULT NULL,
  `GhepLop` float DEFAULT NULL,
  `TrenDanConLai` float DEFAULT NULL,
  `DoDaiCaiDat` float DEFAULT NULL,
  `SoMetConLai` float DEFAULT NULL,
  `ThaoTacThucHien` int(11) DEFAULT NULL COMMENT '0-xóa từ đầu\n1- chuyển đơn\n2-chuyển khổ'
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `datamaysong`
--

LOCK TABLES `datamaysong` WRITE;
/*!40000 ALTER TABLE `datamaysong` DISABLE KEYS */;
INSERT INTO `datamaysong` VALUES ('2023-02-19 08:54:34','C',NULL,NULL,NULL,NULL,NULL,NULL,0),('2023-02-19 09:04:41','B',NULL,NULL,NULL,NULL,NULL,NULL,0),('2023-02-19 09:04:41','C',22,33,44,55,77,88,2),('2023-02-19 09:04:41','E',NULL,NULL,NULL,NULL,NULL,NULL,0),('2023-02-19 09:06:19','B',NULL,NULL,NULL,NULL,NULL,NULL,0),('2023-02-19 09:06:19','E',NULL,NULL,NULL,NULL,NULL,NULL,0),('2023-02-19 09:06:20','C',22,33,44,55,77,88,2),('2023-02-19 09:11:41','E',NULL,NULL,NULL,NULL,NULL,NULL,1),('2023-02-19 09:11:41','C',NULL,NULL,NULL,NULL,NULL,NULL,1),('2023-02-19 09:11:43','B',NULL,NULL,NULL,NULL,NULL,NULL,1),('2023-02-19 09:22:42','C',22,33,44,0,77,88,1),('2023-02-19 09:22:44','B',0,0,0,0,0,0,1),('2023-02-19 09:22:45','E',0,0,0,0,0,0,1),('2023-02-19 09:24:46','C',22,33,44,55,77,88,2),('2023-02-19 09:24:46','B',0,0,0,0,0,0,2),('2023-02-19 09:24:46','E',0,0,0,0,0,0,2),('2023-02-19 10:34:41','C',1,2,3,4,5,6,1),('2023-02-19 12:16:22','C',22,33,44,55,77,88,2),('2023-02-19 12:16:22','B',0,0,0,0,0,0,2),('2023-02-19 12:16:22','E',0,0,0,0,0,0,2),('2023-02-19 12:16:25','C',22,33,44,55,77,88,2),('2023-02-19 12:16:25','B',0,0,0,0,0,0,2),('2023-02-19 12:16:25','E',0,0,0,0,0,0,2),('2023-02-19 18:18:22','C',1234.23,33,44,55,77,88,2);
/*!40000 ALTER TABLE `datamaysong` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tbldonhang`
--

DROP TABLE IF EXISTS `tbldonhang`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbldonhang` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Stt` int(11) DEFAULT NULL,
  `Status` int(1) DEFAULT '0' COMMENT '0-NewOrder,\\\\\\\\n1-     Processing,\\\\\\\\n2-        Finish\\\\n3-Cancel',
  `Ma` varchar(500) DEFAULT NULL,
  `Song` varchar(45) DEFAULT NULL,
  `SoLop` varchar(10) DEFAULT NULL COMMENT 'Số Lớp của sóng để cọng thêm chênh lệch',
  `Kho` int(10) DEFAULT NULL,
  `ChieuDai` float DEFAULT NULL,
  `SoLuong` int(11) DEFAULT NULL,
  `Pallet` int(11) DEFAULT NULL,
  `Xa` int(11) DEFAULT NULL,
  `Rong` int(11) DEFAULT NULL,
  `Canh` int(11) DEFAULT NULL,
  `Cao` int(11) DEFAULT NULL,
  `Lang` int(11) DEFAULT NULL,
  `GiaySongE` varchar(45) DEFAULT NULL,
  `GiayMatE` varchar(45) DEFAULT NULL,
  `GiaySongB` varchar(45) DEFAULT NULL,
  `GiayMatB` varchar(45) DEFAULT NULL,
  `GiaySongC` varchar(45) DEFAULT NULL,
  `GiayMatC` varchar(45) DEFAULT NULL,
  `GiayMen` varchar(45) DEFAULT NULL,
  `GhiChu` varchar(500) DEFAULT NULL,
  `MayXa` varchar(45) DEFAULT NULL,
  `Line` varchar(45) DEFAULT NULL,
  `KhachHang` varchar(45) DEFAULT NULL,
  `DaiKH` int(11) DEFAULT NULL,
  `RongKH` int(11) DEFAULT NULL,
  `CaoKH` int(11) DEFAULT NULL,
  `DonHang` varchar(45) DEFAULT NULL,
  `PO` varchar(45) DEFAULT NULL,
  `MayIn` varchar(45) DEFAULT NULL,
  `ChapBe` varchar(45) DEFAULT NULL,
  `GhimDan` varchar(45) DEFAULT NULL,
  `CreatedDate` datetime DEFAULT CURRENT_TIMESTAMP,
  `IsActived` int(11) DEFAULT '1',
  `CreatedByHostName` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tbldonhang`
--

LOCK TABLES `tbldonhang` WRITE;
/*!40000 ALTER TABLE `tbldonhang` DISABLE KEYS */;
INSERT INTO `tbldonhang` VALUES (3,3,0,'2312','EBC','5',100,35,450,NULL,1,40,20,50,1,'Song E 1','Mat E 1','Song B 1','Mat B 1','Song C 1','Mat C 1','WT140',NULL,NULL,NULL,'KH1',0,0,0,'DH1','PO1',NULL,NULL,NULL,'2023-03-21 23:36:53',1,'@@hostname'),(4,4,0,'5555','BC','7',170,55,1219,NULL,2,37,19,45,1,'Song E 1','Mat E 1','Song B 1','Mat B 1','Song C 1','Mat C 1','WT140',NULL,NULL,NULL,NULL,0,0,0,NULL,NULL,NULL,NULL,NULL,'2023-07-10 14:16:29',1,NULL);
/*!40000 ALTER TABLE `tbldonhang` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tbldonhangdangchay`
--

DROP TABLE IF EXISTS `tbldonhangdangchay`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbldonhangdangchay` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `DonHangId` int(11) DEFAULT NULL,
  `CreatedDateDonHang` datetime DEFAULT NULL,
  `Ma` varchar(45) DEFAULT NULL,
  `Song` varchar(45) DEFAULT NULL,
  `Kho` int(11) DEFAULT NULL,
  `ChieuDai` float DEFAULT '0',
  `SoLuong` int(11) DEFAULT '0',
  `SLDat` int(11) DEFAULT '0',
  `SLLoi` int(11) DEFAULT '0',
  `SLConLai` int(11) DEFAULT NULL,
  `Pallet` int(11) DEFAULT NULL,
  `Xa` int(11) DEFAULT NULL,
  `ThoiGianBatDau` datetime DEFAULT NULL,
  `ThoiGianKetThuc` datetime DEFAULT NULL,
  `ThoiGianChay` time DEFAULT NULL,
  `ThoiGianDung` time DEFAULT NULL,
  `CreatedDate` datetime DEFAULT CURRENT_TIMESTAMP,
  `IsActived` int(11) DEFAULT '1',
  `HoanTatCutter` float DEFAULT NULL,
  `HoanTatSpliter` float DEFAULT '0',
  `HoanTatMayMen` float DEFAULT '0',
  `HoanTatSongE` float DEFAULT '0',
  `HoanTatGiayMatE` float DEFAULT '0',
  `HoanTatGiaySongE` float DEFAULT '0',
  `HoanTatSongB` float DEFAULT '0',
  `HoanTatGiayMatB` float DEFAULT '0',
  `HoanTatGiaySongB` float DEFAULT '0',
  `HoanTatSongC` float DEFAULT '0',
  `HoanTatGiayMatC` float DEFAULT '0',
  `HoanTatGiaySongC` float DEFAULT '0',
  `Ca` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tbldonhangdangchay`
--

LOCK TABLES `tbldonhangdangchay` WRITE;
/*!40000 ALTER TABLE `tbldonhangdangchay` DISABLE KEYS */;
/*!40000 ALTER TABLE `tbldonhangdangchay` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tblgiaymensettings`
--

DROP TABLE IF EXISTS `tblgiaymensettings`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tblgiaymensettings` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tblgiaymensettings`
--

LOCK TABLES `tblgiaymensettings` WRITE;
/*!40000 ALTER TABLE `tblgiaymensettings` DISABLE KEYS */;
INSERT INTO `tblgiaymensettings` VALUES (1,'WT140'),(2,'WT141'),(3,'WT142');
/*!40000 ALTER TABLE `tblgiaymensettings` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tblmasettings`
--

DROP TABLE IF EXISTS `tblmasettings`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tblmasettings` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tblmasettings`
--

LOCK TABLES `tblmasettings` WRITE;
/*!40000 ALTER TABLE `tblmasettings` DISABLE KEYS */;
INSERT INTO `tblmasettings` VALUES (1,'2312'),(2,'3333'),(3,'4444'),(4,'5555');
/*!40000 ALTER TABLE `tblmasettings` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tblsolop`
--

DROP TABLE IF EXISTS `tblsolop`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tblsolop` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `TenLop` int(11) DEFAULT NULL,
  `CongThem` float DEFAULT NULL,
  `IsActiced` bit(1) DEFAULT b'1',
  `CreatedDate` datetime DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tblsolop`
--

LOCK TABLES `tblsolop` WRITE;
/*!40000 ALTER TABLE `tblsolop` DISABLE KEYS */;
INSERT INTO `tblsolop` VALUES (1,2,0,_binary '','2023-07-10 13:39:34'),(2,3,0.1,_binary '','2023-07-10 13:39:34'),(4,5,0.2,_binary '','2023-07-10 13:39:34'),(6,7,0.3,_binary '','2023-07-10 13:39:34');
/*!40000 ALTER TABLE `tblsolop` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tblsongbsettings`
--

DROP TABLE IF EXISTS `tblsongbsettings`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tblsongbsettings` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `GiaySong` varchar(45) DEFAULT NULL,
  `GiayMat` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tblsongbsettings`
--

LOCK TABLES `tblsongbsettings` WRITE;
/*!40000 ALTER TABLE `tblsongbsettings` DISABLE KEYS */;
INSERT INTO `tblsongbsettings` VALUES (1,'Song B 1','Mat B 1'),(2,'Song B 2','Mat B 2'),(3,'Song B 3','Mat B 3');
/*!40000 ALTER TABLE `tblsongbsettings` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tblsongcsettings`
--

DROP TABLE IF EXISTS `tblsongcsettings`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tblsongcsettings` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `GiaySong` varchar(45) DEFAULT NULL,
  `GiayMat` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tblsongcsettings`
--

LOCK TABLES `tblsongcsettings` WRITE;
/*!40000 ALTER TABLE `tblsongcsettings` DISABLE KEYS */;
INSERT INTO `tblsongcsettings` VALUES (1,'Song C 1','Mat C 1'),(2,'Song C 2','Mat C 2'),(3,'Song C 3','Mat C 3');
/*!40000 ALTER TABLE `tblsongcsettings` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tblsongesettings`
--

DROP TABLE IF EXISTS `tblsongesettings`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tblsongesettings` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `GiaySong` varchar(45) DEFAULT NULL,
  `GiayMat` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tblsongesettings`
--

LOCK TABLES `tblsongesettings` WRITE;
/*!40000 ALTER TABLE `tblsongesettings` DISABLE KEYS */;
INSERT INTO `tblsongesettings` VALUES (1,'Song E 1','Mat E 1'),(2,'Song E 2','Mat E 2'),(3,'Song E 3','Mat E 3');
/*!40000 ALTER TABLE `tblsongesettings` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tblsongsettings`
--

DROP TABLE IF EXISTS `tblsongsettings`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tblsongsettings` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tblsongsettings`
--

LOCK TABLES `tblsongsettings` WRITE;
/*!40000 ALTER TABLE `tblsongsettings` DISABLE KEYS */;
INSERT INTO `tblsongsettings` VALUES (1,'BC'),(2,'B'),(3,'C'),(4,'E');
/*!40000 ALTER TABLE `tblsongsettings` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping events for database 'quanlygiay'
--

--
-- Dumping routines for database 'quanlygiay'
--
/*!50003 DROP PROCEDURE IF EXISTS `sp_dataMaySongGetsFromTo` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_dataMaySongGetsFromTo`(
IN
	_From datetime
	,_To datetime
)
BEGIN
	SELECT *
	FROM datamaysong
	WHERE CreatedDate >= _From
		AND CreatedDate <= _To
    ;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_dataMaySongGetsFromToSong` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_dataMaySongGetsFromToSong`(
IN
	_Song nvarchar(10)
	,_From datetime
	,_To datetime
)
BEGIN
	SELECT *
	FROM datamaysong
	WHERE CreatedDate >= _From
		AND CreatedDate <= _To
        AND MaySong = _Song
    ;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_dataMaySongInsert` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_dataMaySongInsert`(
	IN
		_MaySong nvarchar(5)
		, _GiayMat float
		,_GiaySong float
		,_GhepLop float
		,_TrenDanConLai float
		,_DoDaiCaiDat float
		,_SoMetConLai float
		,_ThaoTacThucHien float
)
BEGIN
INSERT INTO `quanlygiay`.`datamaysong`
(
	`MaySong`,
	`GiayMat`,
	`GiaySong`,
	`GhepLop`,
	`TrenDanConLai`,
	`DoDaiCaiDat`,
	`SoMetConLai`,
	`ThaoTacThucHien`
)
VALUES
(
	_MaySong
	,_GiayMat
	,_GiaySong
	,_GhepLop
	,_TrenDanConLai
	,_DoDaiCaiDat
	,_SoMetConLai
	,_ThaoTacThucHien
);

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_tblDonHangDangChayInsert` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_tblDonHangDangChayInsert`(
	IN
		_donHanagId int,
        _ma nvarchar(45),
        _song nvarchar(45),
        _kho int,
        _chieuDai float,
        _soLuong int,
        _slDat int,
        _slLoi int,
        _slConLai int,
        _pallet int,
        _xa int,
        _thoiGianBatDau datetime,
        _thoiGianKetThuc datetime,
        _thoiGianChay time,
        _thoiGianDung time,
        _hoanTatCutter int,
        _hoanTatSpliter int,
        _hoanTatMayMen int,
        _hoanTatSongE int,
        _hoanTatGiayMatE int,
        _hoanTatGiaySongE int,
        _hoanTatSongB int,
        _hoanTatGiayMatB int,
        _hoanTatGiaySongB int,
        _hoanTatSongC int,
        _hoanTatGiayMatC int,
        _hoanTatGiaySongC int
)
BEGIN
	INSERT INTO `quanlygiay`.`tbldonhangdangchay`
	(
		`DonHangId`,
		`Ma`,
		`Song`,
		`Kho`,
		`ChieuDai`,
		`SoLuong`,
		`SLDat`,
		`SLLoi`,
        SLConLai,
        Pallet,
        Xa,
		`ThoiGianBatDau`,
		`ThoiGianKetThuc`,
		`ThoiGianChay`,
		`ThoiGianDung`,
		`CreatedDate`,
		`IsActived`,
		`HoanTatCutter`,
		`HoanTatSpliter`,
		`HoanTatMayMen`,
		`HoanTatSongE`,
		`HoanTatGiayMatE`,
		`HoanTatGiaySongE`,
		`HoanTatSongB`,
		`HoanTatGiayMatB`,
		`HoanTatGiaySongB`,
		`HoanTatSongC`,
		`HoanTatGiayMatC`,
		`HoanTatGiaySongC`
	)
	VALUES
	(
		__donHanagId ,
        _ma ,
        _song ,
        _kho ,
        _chieuDai ,
        _soLuong ,
        _slDat ,
        _slLoi ,
        _slConLai,
        _pallet,
        _xa,
        _thoiGianBatDau ,
        _thoiGianKetThuc ,
        _thoiGianChay ,
        _thoiGianDung ,
        _hoanTatCutter ,
        _hoanTatSpliter ,
        _hoanTatMayMen ,
        _hoanTatSongE ,
        _hoanTatGiayMatE ,
        _hoanTatGiaySongE ,
        _hoanTatSongB ,
        _hoanTatGiayMatB ,
        _hoanTatGiaySongB ,
        _hoanTatSongC ,
        _hoanTatGiayMatC ,
        _hoanTatGiaySongC 
    );	
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_tblDonHangGetAll` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_tblDonHangGetAll`()
BEGIN
	SELECT *
    FROM tbldonhang
    WHERE
		IsActived = 1
    ORDER BY stt desc;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_tblDonHangGetOnProcess` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_tblDonHangGetOnProcess`()
BEGIN
	SELECT *
    FROM tbldonhang
    WHERE
		IsActived = 1
        and tbldonhang.Status in (0,1)
    ORDER BY stt asc;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_tblDonHangInsert` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_tblDonHangInsert`(
	IN
		_stt int,
		_stattus int,
		_ma VARCHAR(500),
		_song VARCHAR(45),
		_kho int,
        _soLop varchar(10),
        _chieuDai float,
        _soLuong Int,
        _pallet int,
        _xa int,
        _rong int,
        _canh int,
        _cao int,
        _lang int,
        _giaySongE VARCHAR(45),
        _giayMatE VARCHAR(45),
        _giaySongB VARCHAR(45),
        _giayMatB VARCHAR(45),
        _giaySongC VARCHAR(45),
        _giayMatC VARCHAR(45),
        _giayMen VARCHAR(45),
        _ghiChu VARCHAR(500),
        _mayXa VARCHAR(45),
        _line VARCHAR(45),
        _khachHang VARCHAR(45),
        _daiKH int,
        _rongKH int,
        _caoKH int,
        _donHang VARCHAR(45),
        _po VARCHAR(45),
        _mayIn VARCHAR(45),
        _chapBe VARCHAR(45),
        _ghimDan VARCHAR(45)
)
BEGIN
	INSERT INTO `quanlygiay`.`tbldonhang`
	(
		`Stt`,
		`Status`,
		`Ma`,
		`Song`,
		`Kho`,
        `SoLop`,
		`ChieuDai`,
		`SoLuong`,
		`Pallet`,
		`Xa`,
		`Rong`,
		`Canh`,
		`Cao`,
		`Lang`,
		`GiaySongE`,
		`GiayMatE`,
		`GiaySongB`,
		`GiayMatB`,
		`GiaySongC`,
		`GiayMatC`,
        `GiayMen`,
        `GhiChu`,
		`MayXa`,
		`Line`,
		`KhachHang`,
		`DaiKH`,
		`RongKH`,
		`CaoKH`,
		`DonHang`,
		`PO`,
		`MayIn`,
		`ChapBe`,
		`GhimDan`
    )
	VALUES
	(
		_stt,
        _status,
        _ma,
        _song,
        _kho,
        _soLop,
        _chieuDai,
        _soLuong,
        _pallet,
        _xa,
        _rong,
        _canh,
        _cao,
        _lang,
        _giaySongE,
        _giayMatE,
        _giaySongB,
        _giayMatB,
        _giaySongC,
        _giayMatC,
        _giayMen,
        _ghiChu,
        _mayXa,
        _line,
        _khachHang,
        _daiKH,
        _rongKH,
        _caoKH,
        _donHang,
        _po,
        _mayIn,
        _chapBe,
        _ghimDan
    );
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2023-07-14 14:56:50
