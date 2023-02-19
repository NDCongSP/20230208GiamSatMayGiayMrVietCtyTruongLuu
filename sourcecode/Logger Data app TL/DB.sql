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
INSERT INTO `datamaysong` VALUES ('2023-02-19 08:54:34','C',NULL,NULL,NULL,NULL,NULL,NULL,0),('2023-02-19 09:04:41','B',NULL,NULL,NULL,NULL,NULL,NULL,0),('2023-02-19 09:04:41','C',22,33,44,55,77,88,2),('2023-02-19 09:04:41','E',NULL,NULL,NULL,NULL,NULL,NULL,0),('2023-02-19 09:06:19','B',NULL,NULL,NULL,NULL,NULL,NULL,0),('2023-02-19 09:06:19','E',NULL,NULL,NULL,NULL,NULL,NULL,0),('2023-02-19 09:06:20','C',22,33,44,55,77,88,2),('2023-02-19 09:11:41','E',NULL,NULL,NULL,NULL,NULL,NULL,1),('2023-02-19 09:11:41','C',NULL,NULL,NULL,NULL,NULL,NULL,1),('2023-02-19 09:11:43','B',NULL,NULL,NULL,NULL,NULL,NULL,1),('2023-02-19 09:22:42','C',22,33,44,0,77,88,1),('2023-02-19 09:22:44','B',0,0,0,0,0,0,1),('2023-02-19 09:22:45','E',0,0,0,0,0,0,1),('2023-02-19 09:24:46','C',22,33,44,55,77,88,2),('2023-02-19 09:24:46','B',0,0,0,0,0,0,2),('2023-02-19 09:24:46','E',0,0,0,0,0,0,2),('2023-02-19 10:34:41','C',1,2,3,4,5,6,1),('2023-02-19 12:16:22','C',22,33,44,55,77,88,2),('2023-02-19 12:16:22','B',0,0,0,0,0,0,2),('2023-02-19 12:16:22','E',0,0,0,0,0,0,2),('2023-02-19 12:16:25','C',22,33,44,55,77,88,2),('2023-02-19 12:16:25','B',0,0,0,0,0,0,2),('2023-02-19 12:16:25','E',0,0,0,0,0,0,2);
/*!40000 ALTER TABLE `datamaysong` ENABLE KEYS */;
UNLOCK TABLES;

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
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2023-02-19 16:51:24
