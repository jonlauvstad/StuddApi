CREATE DATABASE  IF NOT EXISTS `studd_gok_api` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `studd_gok_api`;
-- MySQL dump 10.13  Distrib 8.0.30, for Win64 (x86_64)
--
-- Host: localhost    Database: studd_gok_api
-- ------------------------------------------------------
-- Server version	8.0.30

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
-- Table structure for table `__efmigrationshistory`
--

DROP TABLE IF EXISTS `__efmigrationshistory`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `__efmigrationshistory` (
  `MigrationId` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ProductVersion` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`MigrationId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `__efmigrationshistory`
--

LOCK TABLES `__efmigrationshistory` WRITE;
/*!40000 ALTER TABLE `__efmigrationshistory` DISABLE KEYS */;
INSERT INTO `__efmigrationshistory` VALUES ('20240119163642_initialization','7.0.12'),('20240120045913_ProgImpPropStudyProgram','7.0.12'),('20240120050516_ProgImpPropStudyProgram','7.0.12'),('20240127114053_VenueCapacity','7.0.12');
/*!40000 ALTER TABLE `__efmigrationshistory` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `assignmentresults`
--

DROP TABLE IF EXISTS `assignmentresults`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `assignmentresults` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `AssignmentId` int NOT NULL,
  `UserId` int NOT NULL,
  `Grade` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_AssignmentResults_AssignmentId` (`AssignmentId`),
  KEY `IX_AssignmentResults_UserId` (`UserId`),
  CONSTRAINT `FK_AssignmentResults_Assignments_AssignmentId` FOREIGN KEY (`AssignmentId`) REFERENCES `assignments` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_AssignmentResults_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `users` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `assignmentresults`
--

LOCK TABLES `assignmentresults` WRITE;
/*!40000 ALTER TABLE `assignmentresults` DISABLE KEYS */;
/*!40000 ALTER TABLE `assignmentresults` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `assignments`
--

DROP TABLE IF EXISTS `assignments`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `assignments` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `CourseImplementationId` int NOT NULL,
  `Name` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Description` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Deadline` datetime(6) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Assignments_CourseImplementationId` (`CourseImplementationId`),
  CONSTRAINT `FK_Assignments_CourseImplementations_CourseImplementationId` FOREIGN KEY (`CourseImplementationId`) REFERENCES `courseimplementations` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `assignments`
--

LOCK TABLES `assignments` WRITE;
/*!40000 ALTER TABLE `assignments` DISABLE KEYS */;
INSERT INTO `assignments` VALUES (1,8,'Dockerisert Applikasjon','Se klassenotatblokk','2024-02-09 23:59:00.000000');
/*!40000 ALTER TABLE `assignments` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `attendances`
--

DROP TABLE IF EXISTS `attendances`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `attendances` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `LectureId` int NOT NULL,
  `UserId` int NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Attendances_LectureId` (`LectureId`),
  KEY `IX_Attendances_UserId` (`UserId`),
  CONSTRAINT `FK_Attendances_Lectures_LectureId` FOREIGN KEY (`LectureId`) REFERENCES `lectures` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_Attendances_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `users` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `attendances`
--

LOCK TABLES `attendances` WRITE;
/*!40000 ALTER TABLE `attendances` DISABLE KEYS */;
/*!40000 ALTER TABLE `attendances` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `courseimplementations`
--

DROP TABLE IF EXISTS `courseimplementations`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `courseimplementations` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Code` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Name` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `CourseId` int NOT NULL,
  `StartDate` datetime(6) NOT NULL,
  `EndDate` datetime(6) NOT NULL,
  `Semester` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `EndSemester` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Year` int NOT NULL,
  `EndYear` int NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_CourseImplementations_CourseId` (`CourseId`),
  CONSTRAINT `FK_CourseImplementations_Courses_CourseId` FOREIGN KEY (`CourseId`) REFERENCES `courses` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=68 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `courseimplementations`
--

LOCK TABLES `courseimplementations` WRITE;
/*!40000 ALTER TABLE `courseimplementations` DISABLE KEYS */;
INSERT INTO `courseimplementations` VALUES (1,'GRLPH22SA','Grunnleggende programmering H22 Sandefjord',1,'2022-07-01 00:00:00.000000','2022-12-31 00:00:00.000000','H','H',2022,2022),(2,'DBSQH22SA','Databaser og SQL H22 Sandefjord',2,'2022-07-01 00:00:00.000000','2022-12-31 00:00:00.000000','H','H',2022,2022),(3,'OBJPV23SA','Objektorientert programmering V23 Sandefjord',3,'2023-01-01 00:00:00.000000','2023-06-30 00:00:00.000000','V','V',2023,2023),(4,'PRUMV23SA','Programvareutviklingsmetoder V23 Sandefjord',4,'2023-01-01 00:00:00.000000','2023-06-30 00:00:00.000000','V','V',2023,2023),(5,'AVAPH23SA','Avansert programmering H23 Sandefjord',5,'2023-07-01 00:00:00.000000','2023-12-31 00:00:00.000000','H','H',2023,2023),(6,'ALGOH23SA','Algoritmiske metoder H23 Sandefjord',6,'2023-07-01 00:00:00.000000','2023-12-31 00:00:00.000000','H','H',2023,2023),(7,'SOFDH23SA','Software design H23 Sandefjord',7,'2023-07-01 00:00:00.000000','2023-12-31 00:00:00.000000','H','H',2023,2023),(8,'CWAKV24SA','Cloud-WebArkitektur-Container V24 Sandefjord',8,'2024-01-01 00:00:00.000000','2024-06-30 00:00:00.000000','V','V',2024,2024),(9,'PROJV24SA','Prosjektoppgave V24 Sandefjord',9,'2024-01-01 00:00:00.000000','2024-06-30 00:00:00.000000','V','V',2024,2024),(10,'GRLPH23SA','Grunnleggende programmering H23 Sandefjord',1,'2023-07-01 00:00:00.000000','2023-12-31 00:00:00.000000','H','H',2023,2023),(11,'DBSQH23SA','Databaser og SQL H23 Sandefjord',2,'2023-07-01 00:00:00.000000','2023-12-31 00:00:00.000000','H','H',2023,2023),(12,'OBJPV24SA','Objektorientert programmering V24 Sandefjord',3,'2024-01-01 00:00:00.000000','2024-06-30 00:00:00.000000','V','V',2024,2024),(13,'PRUMV24SA','Programvareutviklingsmetoder V24 Sandefjord',4,'2024-01-01 00:00:00.000000','2024-06-30 00:00:00.000000','V','V',2024,2024),(14,'AVAPH24SA','Avansert programmering H24 Sandefjord',5,'2024-07-01 00:00:00.000000','2024-12-31 00:00:00.000000','H','H',2024,2024),(15,'ALGOH24SA','Algoritmiske metoder H24 Sandefjord',6,'2024-07-01 00:00:00.000000','2024-12-31 00:00:00.000000','H','H',2024,2024),(16,'SOFDH24SA','Software design H24 Sandefjord',7,'2024-07-01 00:00:00.000000','2024-12-31 00:00:00.000000','H','H',2024,2024),(17,'CWAKV25SA','Cloud-WebArkitektur-Container V25 Sandefjord',8,'2025-01-01 00:00:00.000000','2025-06-30 00:00:00.000000','V','V',2025,2025),(18,'PROJV25SA','Prosjektoppgave V25 Sandefjord',9,'2025-01-01 00:00:00.000000','2025-06-30 00:00:00.000000','V','V',2025,2025),(19,'GRLPH23IN','Grunnleggende programmering H23 Internett',1,'2023-07-01 00:00:00.000000','2023-12-31 00:00:00.000000','H','H',2023,2023),(20,'DBSQH23IN','Databaser og SQL H23 Internett',2,'2023-07-01 00:00:00.000000','2023-12-31 00:00:00.000000','H','H',2023,2023),(21,'OBJPV24IN','Objektorientert programmering V24 Internett',3,'2024-01-01 00:00:00.000000','2024-06-30 00:00:00.000000','V','V',2024,2024),(22,'PRUMV24IN','Programvareutviklingsmetoder V24 Internett',4,'2024-01-01 00:00:00.000000','2024-06-30 00:00:00.000000','V','V',2024,2024),(23,'AVAPH24IN','Avansert programmering H24 Internett',5,'2024-07-01 00:00:00.000000','2024-12-31 00:00:00.000000','H','H',2024,2024),(24,'ALGOH24IN','Algoritmiske metoder H24 Internett',6,'2024-07-01 00:00:00.000000','2024-12-31 00:00:00.000000','H','H',2024,2024),(25,'SOFDH24IN','Software design H24 Internett',7,'2024-07-01 00:00:00.000000','2024-12-31 00:00:00.000000','H','H',2024,2024),(26,'CWAKV25IN','Cloud-WebArkitektur-Container V25 Internett',8,'2025-01-01 00:00:00.000000','2025-06-30 00:00:00.000000','V','V',2025,2025),(27,'PROJV25IN','Prosjektoppgave V25 Internett',9,'2025-01-01 00:00:00.000000','2025-06-30 00:00:00.000000','V','V',2025,2025),(28,'GCYSH23SA','Grunnleggende Cybersikkerhet H23 Sandefjord',10,'2023-07-01 00:00:00.000000','2023-12-31 00:00:00.000000','H','H',2023,2023),(29,'NETSH23SA','Nettverkssikkerhet H23 Sandefjord',11,'2023-07-01 00:00:00.000000','2023-12-31 00:00:00.000000','H','H',2023,2023),(30,'KRYFV24SA','Kryptografi Fundamentals V24 Sandefjord',12,'2024-01-01 00:00:00.000000','2024-06-30 00:00:00.000000','V','V',2024,2024),(31,'SIOSV24SA','Sikkerhet i Operativsystemer V24 Sandefjord',13,'2024-01-01 00:00:00.000000','2024-06-30 00:00:00.000000','V','V',2024,2024),(32,'IRMGH24SA','Incident Response Management H24 Sandefjord',14,'2024-07-01 00:00:00.000000','2024-12-31 00:00:00.000000','H','H',2024,2024),(33,'AETHH24SA','Anvendt Ethical Hacking H24 Sandefjord',15,'2024-07-01 00:00:00.000000','2024-12-31 00:00:00.000000','H','H',2024,2024),(34,'SICCV25SA','Sikkerhet i Cloud Computing V25 Sandefjord',16,'2025-01-01 00:00:00.000000','2025-06-30 00:00:00.000000','V','V',2025,2025),(35,'CTINV25SA','Cyber Threat Intelligence V25 Sandefjord',17,'2025-01-01 00:00:00.000000','2025-06-30 00:00:00.000000','V','V',2025,2025),(36,'GWUDH22SA','Grunnleggende Webutvikling H22 Sandefjord',18,'2022-07-01 00:00:00.000000','2022-12-31 00:00:00.000000','H','H',2022,2022),(37,'RSDNH22SA','Responsiv Design H22 Sandefjord',19,'2022-07-01 00:00:00.000000','2022-12-31 00:00:00.000000','H','H',2022,2022),(38,'JSFNV23SA','JavaScript Fundamentals V23 Sandefjord',20,'2023-01-01 00:00:00.000000','2023-06-30 00:00:00.000000','V','V',2023,2023),(39,'FRAWV23SA','Frontend-rammeverk Workshop V23 Sandefjord',21,'2023-01-01 00:00:00.000000','2023-06-30 00:00:00.000000','V','V',2023,2023),(40,'UIGDH23SA','Brukergrensesnitt Design H23 Sandefjord',22,'2023-07-01 00:00:00.000000','2023-12-31 00:00:00.000000','H','H',2023,2023),(41,'SPAPH23SA','Single Page Applications (SPA) H23 Sandefjord',23,'2023-07-01 00:00:00.000000','2023-12-31 00:00:00.000000','H','H',2023,2023),(42,'FETSV24SA','Frontend Testing Strategies V24 Sandefjord',24,'2024-01-01 00:00:00.000000','2024-06-30 00:00:00.000000','V','V',2024,2024),(43,'MCSSV24SA','Modern CSS Techniques V24 Sandefjord',25,'2024-01-01 00:00:00.000000','2024-06-30 00:00:00.000000','V','V',2024,2024),(44,'GWUDH23SA','Grunnleggende Webutvikling H23 Sandefjord',18,'2023-07-01 00:00:00.000000','2023-12-31 00:00:00.000000','H','H',2023,2023),(45,'RSDNH23SA','Responsiv Design H23 Sandefjord',19,'2023-07-01 00:00:00.000000','2023-12-31 00:00:00.000000','H','H',2023,2023),(46,'JSFNV24SA','JavaScript Fundamentals V24 Sandefjord',20,'2024-01-01 00:00:00.000000','2024-06-30 00:00:00.000000','V','V',2024,2024),(47,'FRAWV24SA','Frontend-rammeverk Workshop V24 Sandefjord',21,'2024-01-01 00:00:00.000000','2024-06-30 00:00:00.000000','V','V',2024,2024),(48,'UIGDH24SA','Brukergrensesnitt Design H24 Sandefjord',22,'2024-07-01 00:00:00.000000','2024-12-31 00:00:00.000000','H','H',2024,2024),(49,'SPAPH24SA','Single Page Applications (SPA) H24 Sandefjord',23,'2024-07-01 00:00:00.000000','2024-12-31 00:00:00.000000','H','H',2024,2024),(50,'FETSV25SA','Frontend Testing Strategies V25 Sandefjord',24,'2025-01-01 00:00:00.000000','2025-06-30 00:00:00.000000','V','V',2025,2025),(51,'MCSSV25SA','Modern CSS Techniques V25 Sandefjord',25,'2025-01-01 00:00:00.000000','2025-06-30 00:00:00.000000','V','V',2025,2025),(52,'BFMNH23SA','Bærekraftig Forretningsmodell H23 Sandefjord',26,'2023-07-01 00:00:00.000000','2023-12-31 00:00:00.000000','H','H',2023,2023),(53,'INNKH23SA','Innovasjon og Kreativitet H23 Sandefjord',27,'2023-07-01 00:00:00.000000','2023-12-31 00:00:00.000000','H','H',2023,2023),(54,'SSENV24SA','Sosialt Entreprenørskap V24 Sandefjord',28,'2024-01-01 00:00:00.000000','2024-06-30 00:00:00.000000','V','V',2024,2024),(55,'BPUVV24SA','Bærekraftig Produktutvikling V24 Sandefjord',29,'2024-01-01 00:00:00.000000','2024-06-30 00:00:00.000000','V','V',2024,2024),(56,'EGBIH24SA','Etablering av Grønne Bedrifter H24 Sandefjord',30,'2024-07-01 00:00:00.000000','2024-12-31 00:00:00.000000','H','H',2024,2024),(57,'INNSH24SA','Innovasjonsstrategier H24 Sandefjord',31,'2024-07-01 00:00:00.000000','2024-12-31 00:00:00.000000','H','H',2024,2024),(58,'LBKTV25SA','Ledelse for Bærekraft V25 Sandefjord',32,'2025-01-01 00:00:00.000000','2025-06-30 00:00:00.000000','V','V',2025,2025),(59,'TSENV25SA','Teknologi for Samfunnsendring V25 Sandefjord',33,'2025-01-01 00:00:00.000000','2025-06-30 00:00:00.000000','V','V',2025,2025),(60,'GQAPH23SA','Grunnleggende QA-prinsipper H23 Sandefjord',34,'2023-07-01 00:00:00.000000','2023-12-31 00:00:00.000000','H','H',2023,2023),(61,'TPDOH23SA','Testplanlegging og Dokumentasjon H23 Sandefjord',35,'2023-07-01 00:00:00.000000','2023-12-31 00:00:00.000000','H','H',2023,2023),(62,'ATMSV24SA','Automatisert Testing med Selenium V24 Sandefjord',36,'2024-01-01 00:00:00.000000','2024-06-30 00:00:00.000000','V','V',2024,2024),(63,'PFTSV24SA','Performans Testing V24 Sandefjord',37,'2024-01-01 00:00:00.000000','2024-06-30 00:00:00.000000','V','V',2024,2024),(64,'SISPH24SA','Sikkerhetstesting i Programvare H24 Sandefjord',38,'2024-07-01 00:00:00.000000','2024-12-31 00:00:00.000000','H','H',2024,2024),(65,'UTSCH24SA','Utvikling av Testscenarier H24 Sandefjord',39,'2024-07-01 00:00:00.000000','2024-12-31 00:00:00.000000','H','H',2024,2024),(66,'CIFTV25SA','Kontinuerlig Integrering for Testing V25 Sandefjord',40,'2025-01-01 00:00:00.000000','2025-06-30 00:00:00.000000','V','V',2025,2025),(67,'TDMHV25SA','Testdata og Miljøhåndtering V25 Sandefjord',41,'2025-01-01 00:00:00.000000','2025-06-30 00:00:00.000000','V','V',2025,2025);
/*!40000 ALTER TABLE `courseimplementations` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `courses`
--

DROP TABLE IF EXISTS `courses`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `courses` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Code` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Name` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Points` decimal(65,30) NOT NULL,
  `Category` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `TeachCours` tinyint(1) NOT NULL,
  `DiplomaCours` tinyint(1) NOT NULL,
  `ExamCours` tinyint(1) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=42 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `courses`
--

LOCK TABLES `courses` WRITE;
/*!40000 ALTER TABLE `courses` DISABLE KEYS */;
INSERT INTO `courses` VALUES (1,'GRLP','Grunnleggende programmering',15.000000000000000000000000000000,'course',1,1,1),(2,'DBSQ','Databaser og SQL',15.000000000000000000000000000000,'course',1,1,1),(3,'OBJP','Objektorientert programmering',20.000000000000000000000000000000,'course',1,1,1),(4,'PRUM','Programvareutviklingsmetoder',10.000000000000000000000000000000,'course',1,1,1),(5,'AVAP','Avansert programmering',15.000000000000000000000000000000,'course',1,1,1),(6,'ALGO','Algoritmiske metoder',7.500000000000000000000000000000,'course',1,1,1),(7,'SOFD','Software design',7.500000000000000000000000000000,'course',1,1,1),(8,'CWAK','Cloud-WebArkitektur-Container',15.000000000000000000000000000000,'course',1,1,1),(9,'PROJ','Prosjektoppgave',15.000000000000000000000000000000,'course',1,1,1),(10,'GCYS','Grunnleggende Cybersikkerhet',15.000000000000000000000000000000,'course',1,1,1),(11,'NETS','Nettverkssikkerhet',15.000000000000000000000000000000,'course',1,1,1),(12,'KRYF','Kryptografi Fundamentals',15.000000000000000000000000000000,'course',1,1,1),(13,'SIOS','Sikkerhet i Operativsystemer',15.000000000000000000000000000000,'course',1,1,1),(14,'IRMG','Incident Response Management',15.000000000000000000000000000000,'course',1,1,1),(15,'AETH','Anvendt Ethical Hacking',15.000000000000000000000000000000,'course',1,1,1),(16,'SICC','Sikkerhet i Cloud Computing',15.000000000000000000000000000000,'course',1,1,1),(17,'CTIN','Cyber Threat Intelligence',15.000000000000000000000000000000,'course',1,1,1),(18,'GWUD','Grunnleggende Webutvikling',15.000000000000000000000000000000,'course',1,1,1),(19,'RSDN','Responsiv Design',15.000000000000000000000000000000,'course',1,1,1),(20,'JSFN','JavaScript Fundamentals',15.000000000000000000000000000000,'course',1,1,1),(21,'FRAW','Frontend-rammeverk Workshop',15.000000000000000000000000000000,'course',1,1,1),(22,'UIGD','Brukergrensesnitt Design',15.000000000000000000000000000000,'course',1,1,1),(23,'SPAP','Single Page Applications (SPA)',15.000000000000000000000000000000,'course',1,1,1),(24,'FETS','Frontend Testing Strategies',15.000000000000000000000000000000,'course',1,1,1),(25,'MCSS','Modern CSS Techniques',15.000000000000000000000000000000,'course',1,1,1),(26,'BFMN','Bærekraftig Forretningsmodell',15.000000000000000000000000000000,'course',1,1,1),(27,'INNK','Innovasjon og Kreativitet',15.000000000000000000000000000000,'course',1,1,1),(28,'SSEN','Sosialt Entreprenørskap',15.000000000000000000000000000000,'course',1,1,1),(29,'BPUV','Bærekraftig Produktutvikling',15.000000000000000000000000000000,'course',1,1,1),(30,'EGBI','Etablering av Grønne Bedrifter',15.000000000000000000000000000000,'course',1,1,1),(31,'INNS','Innovasjonsstrategier',15.000000000000000000000000000000,'course',1,1,1),(32,'LBKT','Ledelse for Bærekraft',15.000000000000000000000000000000,'course',1,1,1),(33,'TSEN','Teknologi for Samfunnsendring',15.000000000000000000000000000000,'course',1,1,1),(34,'GQAP','Grunnleggende QA-prinsipper',15.000000000000000000000000000000,'course',1,1,1),(35,'TPDO','Testplanlegging og Dokumentasjon',15.000000000000000000000000000000,'course',1,1,1),(36,'ATMS','Automatisert Testing med Selenium',15.000000000000000000000000000000,'course',1,1,1),(37,'PFTS','Performans Testing',15.000000000000000000000000000000,'course',1,1,1),(38,'SISP','Sikkerhetstesting i Programvare',15.000000000000000000000000000000,'course',1,1,1),(39,'UTSC','Utvikling av Testscenarier',15.000000000000000000000000000000,'course',1,1,1),(40,'CIFT','Kontinuerlig Integrering for Testing',15.000000000000000000000000000000,'course',1,1,1),(41,'TDMH','Testdata og Miljøhåndtering',15.000000000000000000000000000000,'course',1,1,1);
/*!40000 ALTER TABLE `courses` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `examimplementations`
--

DROP TABLE IF EXISTS `examimplementations`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `examimplementations` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `ExamId` int NOT NULL,
  `VenueId` int NOT NULL,
  `StartTime` datetime(6) NOT NULL,
  `EndTime` datetime(6) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_ExamImplementations_ExamId` (`ExamId`),
  KEY `IX_ExamImplementations_VenueId` (`VenueId`),
  CONSTRAINT `FK_ExamImplementations_Exams_ExamId` FOREIGN KEY (`ExamId`) REFERENCES `exams` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_ExamImplementations_Venues_VenueId` FOREIGN KEY (`VenueId`) REFERENCES `venues` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `examimplementations`
--

LOCK TABLES `examimplementations` WRITE;
/*!40000 ALTER TABLE `examimplementations` DISABLE KEYS */;
INSERT INTO `examimplementations` VALUES (1,8,2,'2024-05-22 09:00:00.000000','2024-05-22 14:00:00.000000'),(2,8,5,'2024-05-22 09:00:00.000000','2024-05-22 14:00:00.000000');
/*!40000 ALTER TABLE `examimplementations` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `examresults`
--

DROP TABLE IF EXISTS `examresults`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `examresults` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `ExamId` int NOT NULL,
  `UserId` int NOT NULL,
  `Grade` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_ExamResults_ExamId` (`ExamId`),
  KEY `IX_ExamResults_UserId` (`UserId`),
  CONSTRAINT `FK_ExamResults_Exams_ExamId` FOREIGN KEY (`ExamId`) REFERENCES `exams` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_ExamResults_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `users` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `examresults`
--

LOCK TABLES `examresults` WRITE;
/*!40000 ALTER TABLE `examresults` DISABLE KEYS */;
/*!40000 ALTER TABLE `examresults` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `exams`
--

DROP TABLE IF EXISTS `exams`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `exams` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `CourseImplementationId` int NOT NULL,
  `Category` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `DurationHours` decimal(65,30) NOT NULL,
  `PeriodStart` datetime(6) NOT NULL,
  `PeriodEnd` datetime(6) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Exams_CourseImplementationId` (`CourseImplementationId`),
  CONSTRAINT `FK_Exams_CourseImplementations_CourseImplementationId` FOREIGN KEY (`CourseImplementationId`) REFERENCES `courseimplementations` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=68 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `exams`
--

LOCK TABLES `exams` WRITE;
/*!40000 ALTER TABLE `exams` DISABLE KEYS */;
INSERT INTO `exams` VALUES (1,1,'hjemme',6.000000000000000000000000000000,'2022-11-10 00:00:00.000000','2022-12-20 00:00:00.000000'),(2,2,'muntlig',0.500000000000000000000000000000,'2022-11-10 00:00:00.000000','2022-12-20 00:00:00.000000'),(3,3,'skriftlig',5.000000000000000000000000000000,'2023-05-10 00:00:00.000000','2023-06-20 00:00:00.000000'),(4,4,'hjemme',168.000000000000000000000000000000,'2023-05-10 00:00:00.000000','2023-06-20 00:00:00.000000'),(5,5,'skriftlig',5.000000000000000000000000000000,'2023-11-10 00:00:00.000000','2023-12-20 00:00:00.000000'),(6,6,'hjemme',6.000000000000000000000000000000,'2023-11-10 00:00:00.000000','2023-12-20 00:00:00.000000'),(7,7,'hjemme',6.000000000000000000000000000000,'2023-11-10 00:00:00.000000','2023-12-20 00:00:00.000000'),(8,8,'skriftlig',5.000000000000000000000000000000,'2024-05-10 00:00:00.000000','2024-06-20 00:00:00.000000'),(9,9,'hjemme',168.000000000000000000000000000000,'2024-05-10 00:00:00.000000','2024-06-20 00:00:00.000000'),(10,10,'skriftlig',5.000000000000000000000000000000,'2023-11-10 00:00:00.000000','2023-12-20 00:00:00.000000'),(11,11,'hjemme',6.000000000000000000000000000000,'2023-11-10 00:00:00.000000','2023-12-20 00:00:00.000000'),(12,12,'hjemme',6.000000000000000000000000000000,'2024-05-10 00:00:00.000000','2024-06-20 00:00:00.000000'),(13,13,'hjemme',168.000000000000000000000000000000,'2024-05-10 00:00:00.000000','2024-06-20 00:00:00.000000'),(14,14,'skriftlig',5.000000000000000000000000000000,'2024-11-10 00:00:00.000000','2024-12-20 00:00:00.000000'),(15,15,'hjemme',6.000000000000000000000000000000,'2024-11-10 00:00:00.000000','2024-12-20 00:00:00.000000'),(16,16,'muntlig',0.500000000000000000000000000000,'2024-11-10 00:00:00.000000','2024-12-20 00:00:00.000000'),(17,17,'hjemme',6.000000000000000000000000000000,'2025-05-10 00:00:00.000000','2025-06-20 00:00:00.000000'),(18,18,'hjemme',6.000000000000000000000000000000,'2025-05-10 00:00:00.000000','2025-06-20 00:00:00.000000'),(19,19,'hjemme',168.000000000000000000000000000000,'2023-11-10 00:00:00.000000','2023-12-20 00:00:00.000000'),(20,20,'hjemme',168.000000000000000000000000000000,'2023-11-10 00:00:00.000000','2023-12-20 00:00:00.000000'),(21,21,'hjemme',6.000000000000000000000000000000,'2024-05-10 00:00:00.000000','2024-06-20 00:00:00.000000'),(22,22,'hjemme',6.000000000000000000000000000000,'2024-05-10 00:00:00.000000','2024-06-20 00:00:00.000000'),(23,23,'hjemme',168.000000000000000000000000000000,'2024-11-10 00:00:00.000000','2024-12-20 00:00:00.000000'),(24,24,'hjemme',6.000000000000000000000000000000,'2024-11-10 00:00:00.000000','2024-12-20 00:00:00.000000'),(25,25,'skriftlig',5.000000000000000000000000000000,'2024-11-10 00:00:00.000000','2024-12-20 00:00:00.000000'),(26,26,'hjemme',6.000000000000000000000000000000,'2025-05-10 00:00:00.000000','2025-06-20 00:00:00.000000'),(27,27,'muntlig',0.500000000000000000000000000000,'2025-05-10 00:00:00.000000','2025-06-20 00:00:00.000000'),(28,28,'muntlig',0.500000000000000000000000000000,'2023-11-10 00:00:00.000000','2023-12-20 00:00:00.000000'),(29,29,'hjemme',168.000000000000000000000000000000,'2023-11-10 00:00:00.000000','2023-12-20 00:00:00.000000'),(30,30,'skriftlig',5.000000000000000000000000000000,'2024-05-10 00:00:00.000000','2024-06-20 00:00:00.000000'),(31,31,'skriftlig',5.000000000000000000000000000000,'2024-05-10 00:00:00.000000','2024-06-20 00:00:00.000000'),(32,32,'skriftlig',5.000000000000000000000000000000,'2024-11-10 00:00:00.000000','2024-12-20 00:00:00.000000'),(33,33,'hjemme',6.000000000000000000000000000000,'2024-11-10 00:00:00.000000','2024-12-20 00:00:00.000000'),(34,34,'hjemme',6.000000000000000000000000000000,'2025-05-10 00:00:00.000000','2025-06-20 00:00:00.000000'),(35,35,'hjemme',6.000000000000000000000000000000,'2025-05-10 00:00:00.000000','2025-06-20 00:00:00.000000'),(36,36,'muntlig',0.500000000000000000000000000000,'2022-11-10 00:00:00.000000','2022-12-20 00:00:00.000000'),(37,37,'hjemme',6.000000000000000000000000000000,'2022-11-10 00:00:00.000000','2022-12-20 00:00:00.000000'),(38,38,'hjemme',168.000000000000000000000000000000,'2023-05-10 00:00:00.000000','2023-06-20 00:00:00.000000'),(39,39,'hjemme',168.000000000000000000000000000000,'2023-05-10 00:00:00.000000','2023-06-20 00:00:00.000000'),(40,40,'hjemme',6.000000000000000000000000000000,'2023-11-10 00:00:00.000000','2023-12-20 00:00:00.000000'),(41,41,'hjemme',6.000000000000000000000000000000,'2023-11-10 00:00:00.000000','2023-12-20 00:00:00.000000'),(42,42,'skriftlig',5.000000000000000000000000000000,'2024-05-10 00:00:00.000000','2024-06-20 00:00:00.000000'),(43,43,'hjemme',6.000000000000000000000000000000,'2024-05-10 00:00:00.000000','2024-06-20 00:00:00.000000'),(44,44,'hjemme',6.000000000000000000000000000000,'2023-11-10 00:00:00.000000','2023-12-20 00:00:00.000000'),(45,45,'muntlig',0.500000000000000000000000000000,'2023-11-10 00:00:00.000000','2023-12-20 00:00:00.000000'),(46,46,'muntlig',0.500000000000000000000000000000,'2024-05-10 00:00:00.000000','2024-06-20 00:00:00.000000'),(47,47,'skriftlig',5.000000000000000000000000000000,'2024-05-10 00:00:00.000000','2024-06-20 00:00:00.000000'),(48,48,'skriftlig',5.000000000000000000000000000000,'2024-11-10 00:00:00.000000','2024-12-20 00:00:00.000000'),(49,49,'hjemme',6.000000000000000000000000000000,'2024-11-10 00:00:00.000000','2024-12-20 00:00:00.000000'),(50,50,'skriftlig',5.000000000000000000000000000000,'2025-05-10 00:00:00.000000','2025-06-20 00:00:00.000000'),(51,51,'muntlig',0.500000000000000000000000000000,'2025-05-10 00:00:00.000000','2025-06-20 00:00:00.000000'),(52,52,'muntlig',0.500000000000000000000000000000,'2023-11-10 00:00:00.000000','2023-12-20 00:00:00.000000'),(53,53,'muntlig',0.500000000000000000000000000000,'2023-11-10 00:00:00.000000','2023-12-20 00:00:00.000000'),(54,54,'skriftlig',5.000000000000000000000000000000,'2024-05-10 00:00:00.000000','2024-06-20 00:00:00.000000'),(55,55,'hjemme',6.000000000000000000000000000000,'2024-05-10 00:00:00.000000','2024-06-20 00:00:00.000000'),(56,56,'skriftlig',5.000000000000000000000000000000,'2024-11-10 00:00:00.000000','2024-12-20 00:00:00.000000'),(57,57,'hjemme',168.000000000000000000000000000000,'2024-11-10 00:00:00.000000','2024-12-20 00:00:00.000000'),(58,58,'hjemme',6.000000000000000000000000000000,'2025-05-10 00:00:00.000000','2025-06-20 00:00:00.000000'),(59,59,'muntlig',0.500000000000000000000000000000,'2025-05-10 00:00:00.000000','2025-06-20 00:00:00.000000'),(60,60,'skriftlig',5.000000000000000000000000000000,'2023-11-10 00:00:00.000000','2023-12-20 00:00:00.000000'),(61,61,'hjemme',6.000000000000000000000000000000,'2023-11-10 00:00:00.000000','2023-12-20 00:00:00.000000'),(62,62,'hjemme',168.000000000000000000000000000000,'2024-05-10 00:00:00.000000','2024-06-20 00:00:00.000000'),(63,63,'hjemme',6.000000000000000000000000000000,'2024-05-10 00:00:00.000000','2024-06-20 00:00:00.000000'),(64,64,'skriftlig',5.000000000000000000000000000000,'2024-11-10 00:00:00.000000','2024-12-20 00:00:00.000000'),(65,65,'hjemme',6.000000000000000000000000000000,'2024-11-10 00:00:00.000000','2024-12-20 00:00:00.000000'),(66,66,'muntlig',0.500000000000000000000000000000,'2025-05-10 00:00:00.000000','2025-06-20 00:00:00.000000'),(67,67,'skriftlig',5.000000000000000000000000000000,'2025-05-10 00:00:00.000000','2025-06-20 00:00:00.000000');
/*!40000 ALTER TABLE `exams` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `lectures`
--

DROP TABLE IF EXISTS `lectures`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `lectures` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `CourseImplementationId` int NOT NULL,
  `Theme` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Description` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `StartTime` datetime(6) NOT NULL,
  `EndTime` datetime(6) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Lectures_CourseImplementationId` (`CourseImplementationId`),
  CONSTRAINT `FK_Lectures_CourseImplementations_CourseImplementationId` FOREIGN KEY (`CourseImplementationId`) REFERENCES `courseimplementations` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=19 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `lectures`
--

LOCK TABLES `lectures` WRITE;
/*!40000 ALTER TABLE `lectures` DISABLE KEYS */;
INSERT INTO `lectures` VALUES (1,8,'Forelesning 1','TBA','2024-01-08 09:00:00.000000','2024-01-08 14:00:00.000000'),(2,8,'Forelesning 2','TBA','2024-01-09 09:00:00.000000','2024-01-09 14:00:00.000000'),(3,8,'Arbeidsdag','TBA','2024-01-10 09:00:00.000000','2024-01-10 14:00:00.000000'),(4,8,'Forelesning 3','TBA','2024-01-15 09:00:00.000000','2024-01-15 14:00:00.000000'),(5,8,'Forelesning 4','TBA','2024-01-16 09:00:00.000000','2024-01-16 14:00:00.000000'),(6,8,'Arbeidsdag','TBA','2024-01-17 09:00:00.000000','2024-01-17 14:00:00.000000'),(7,8,'Forelesning 5','TBA','2024-01-22 09:00:00.000000','2024-01-22 14:00:00.000000'),(8,8,'Forelesning 6','TBA','2024-01-23 09:00:00.000000','2024-01-23 14:00:00.000000'),(9,8,'Arbeidsdag','TBA','2024-01-24 09:00:00.000000','2024-01-24 14:00:00.000000'),(10,8,'Forelesning 7','TBA','2024-01-29 09:00:00.000000','2024-01-29 14:00:00.000000'),(11,8,'Forelesning 8','TBA','2024-01-30 09:00:00.000000','2024-01-30 14:00:00.000000'),(12,8,'Arbeidsdag','TBA','2024-01-31 09:00:00.000000','2024-01-31 14:00:00.000000'),(13,8,'Forelesning 9','TBA','2024-02-05 09:00:00.000000','2024-02-05 14:00:00.000000'),(14,8,'Forelesning 10','TBA','2024-02-06 09:00:00.000000','2024-02-06 14:00:00.000000'),(15,8,'Arbeidsdag','TBA','2024-02-07 09:00:00.000000','2024-02-07 14:00:00.000000'),(16,8,'Forelesning 11','TBA','2024-02-12 09:00:00.000000','2024-02-12 14:00:00.000000'),(17,8,'Forelesning 12','TBA','2024-02-13 09:00:00.000000','2024-02-13 14:00:00.000000'),(18,8,'Arbeidsdag','TBA','2024-02-14 09:00:00.000000','2024-02-14 14:00:00.000000');
/*!40000 ALTER TABLE `lectures` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `lecturevenues`
--

DROP TABLE IF EXISTS `lecturevenues`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `lecturevenues` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `LectureId` int NOT NULL,
  `VenueId` int NOT NULL,
  `UserId` int DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_LectureVenues_LectureId` (`LectureId`),
  KEY `IX_LectureVenues_UserId` (`UserId`),
  KEY `IX_LectureVenues_VenueId` (`VenueId`),
  CONSTRAINT `FK_LectureVenues_Lectures_LectureId` FOREIGN KEY (`LectureId`) REFERENCES `lectures` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_LectureVenues_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `users` (`Id`),
  CONSTRAINT `FK_LectureVenues_Venues_VenueId` FOREIGN KEY (`VenueId`) REFERENCES `venues` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `lecturevenues`
--

LOCK TABLES `lecturevenues` WRITE;
/*!40000 ALTER TABLE `lecturevenues` DISABLE KEYS */;
/*!40000 ALTER TABLE `lecturevenues` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `locations`
--

DROP TABLE IF EXISTS `locations`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `locations` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Name` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `locations`
--

LOCK TABLES `locations` WRITE;
/*!40000 ALTER TABLE `locations` DISABLE KEYS */;
INSERT INTO `locations` VALUES (1,'Sandefjord'),(2,'Internett');
/*!40000 ALTER TABLE `locations` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `programcourses`
--

DROP TABLE IF EXISTS `programcourses`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `programcourses` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `ProgramImplementationId` int NOT NULL,
  `CourseImplementationId` int NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_ProgramCourses_CourseImplementationId` (`CourseImplementationId`),
  KEY `IX_ProgramCourses_ProgramImplementationId` (`ProgramImplementationId`),
  CONSTRAINT `FK_ProgramCourses_CourseImplementations_CourseImplementationId` FOREIGN KEY (`CourseImplementationId`) REFERENCES `courseimplementations` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_ProgramCourses_ProgramImplementations_ProgramImplementationId` FOREIGN KEY (`ProgramImplementationId`) REFERENCES `programimplementations` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=68 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `programcourses`
--

LOCK TABLES `programcourses` WRITE;
/*!40000 ALTER TABLE `programcourses` DISABLE KEYS */;
INSERT INTO `programcourses` VALUES (1,1,1),(2,1,2),(3,1,3),(4,1,4),(5,1,5),(6,1,6),(7,1,7),(8,1,8),(9,1,9),(10,2,10),(11,2,11),(12,2,12),(13,2,13),(14,2,14),(15,2,15),(16,2,16),(17,2,17),(18,2,18),(19,4,19),(20,4,20),(21,4,21),(22,4,22),(23,4,23),(24,4,24),(25,4,25),(26,4,26),(27,4,27),(28,8,28),(29,8,29),(30,8,30),(31,8,31),(32,8,32),(33,8,33),(34,8,34),(35,8,35),(36,6,36),(37,6,37),(38,6,38),(39,6,39),(40,6,40),(41,6,41),(42,6,42),(43,6,43),(44,7,44),(45,7,45),(46,7,46),(47,7,47),(48,7,48),(49,7,49),(50,7,50),(51,7,51),(52,3,52),(53,3,53),(54,3,54),(55,3,55),(56,3,56),(57,3,57),(58,3,58),(59,3,59),(60,5,60),(61,5,61),(62,5,62),(63,5,63),(64,5,64),(65,5,65),(66,5,66),(67,5,67);
/*!40000 ALTER TABLE `programcourses` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `programimplementations`
--

DROP TABLE IF EXISTS `programimplementations`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `programimplementations` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Code` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Name` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `StartDate` datetime(6) NOT NULL,
  `EndDate` datetime(6) NOT NULL,
  `Semester` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `EndSemester` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Year` int NOT NULL,
  `EndYear` int NOT NULL,
  `StudyProgramId` int NOT NULL DEFAULT '0',
  PRIMARY KEY (`Id`),
  KEY `IX_ProgramImplementations_StudyProgramId` (`StudyProgramId`),
  CONSTRAINT `FK_ProgramImplementations_StudyPrograms_StudyProgramId` FOREIGN KEY (`StudyProgramId`) REFERENCES `studyprograms` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `programimplementations`
--

LOCK TABLES `programimplementations` WRITE;
/*!40000 ALTER TABLE `programimplementations` DISABLE KEYS */;
INSERT INTO `programimplementations` VALUES (1,'HS-BEPH22','Backend-programmering H22','2022-07-01 00:00:00.000000','2024-06-30 00:00:00.000000','H','V',2022,2024,1),(2,'HS-BEPH23','Backend-programmering H23','2023-07-01 00:00:00.000000','2025-06-30 00:00:00.000000','H','V',2023,2025,1),(3,'HS-BEIH23','Bærekraftig entrepenørskap og innovasjon H23','2023-07-01 00:00:00.000000','2025-06-30 00:00:00.000000','H','V',2023,2025,2),(4,'HS-BEPNH23','Backend-programmering Nett H23','2023-07-01 00:00:00.000000','2025-06-30 00:00:00.000000','H','V',2023,2025,3),(5,'HD-QAH23','QA Programvaretesting H23','2023-07-01 00:00:00.000000','2025-06-30 00:00:00.000000','H','V',2023,2025,4),(6,'HS-FEPH22','Frontend-programmering H22','2022-07-01 00:00:00.000000','2024-06-30 00:00:00.000000','H','V',2022,2024,5),(7,'HS-FEPH23','Frontend-programmering H23','2023-07-01 00:00:00.000000','2025-06-30 00:00:00.000000','H','V',2023,2025,5),(8,'HS-CYBH23','Cyber-sikkerhet H23','2023-07-01 00:00:00.000000','2025-06-30 00:00:00.000000','H','V',2023,2025,6);
/*!40000 ALTER TABLE `programimplementations` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `programlocations`
--

DROP TABLE IF EXISTS `programlocations`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `programlocations` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `ProgramImplementationId` int NOT NULL,
  `LocationId` int NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_ProgramLocations_LocationId` (`LocationId`),
  KEY `IX_ProgramLocations_ProgramImplementationId` (`ProgramImplementationId`),
  CONSTRAINT `FK_ProgramLocations_Locations_LocationId` FOREIGN KEY (`LocationId`) REFERENCES `locations` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_ProgramLocations_ProgramImplementations_ProgramImplementatio~` FOREIGN KEY (`ProgramImplementationId`) REFERENCES `programimplementations` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `programlocations`
--

LOCK TABLES `programlocations` WRITE;
/*!40000 ALTER TABLE `programlocations` DISABLE KEYS */;
/*!40000 ALTER TABLE `programlocations` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `studentprograms`
--

DROP TABLE IF EXISTS `studentprograms`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `studentprograms` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `ProgramImplementationId` int NOT NULL,
  `UserId` int NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_StudentPrograms_ProgramImplementationId` (`ProgramImplementationId`),
  KEY `IX_StudentPrograms_UserId` (`UserId`),
  CONSTRAINT `FK_StudentPrograms_ProgramImplementations_ProgramImplementation~` FOREIGN KEY (`ProgramImplementationId`) REFERENCES `programimplementations` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_StudentPrograms_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `users` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=276 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `studentprograms`
--

LOCK TABLES `studentprograms` WRITE;
/*!40000 ALTER TABLE `studentprograms` DISABLE KEYS */;
INSERT INTO `studentprograms` VALUES (1,1,26),(2,1,27),(3,1,28),(4,1,29),(5,1,30),(6,1,31),(7,1,32),(8,1,33),(9,1,34),(10,1,35),(11,1,36),(12,1,37),(13,1,38),(14,1,39),(15,1,40),(16,1,41),(17,1,42),(18,1,43),(19,1,44),(20,1,45),(21,1,46),(22,1,47),(23,1,48),(24,1,49),(25,1,50),(26,1,51),(27,1,52),(28,1,53),(29,1,54),(30,1,55),(31,1,56),(32,1,57),(33,1,58),(34,1,59),(35,1,60),(36,1,61),(37,1,62),(38,1,63),(39,1,64),(40,1,65),(41,1,66),(42,1,67),(43,1,68),(44,1,69),(45,1,70),(46,1,71),(47,1,72),(48,1,73),(49,1,74),(50,1,75),(51,2,76),(52,2,77),(53,2,78),(54,2,79),(55,2,80),(56,2,81),(57,2,82),(58,2,83),(59,2,84),(60,2,85),(61,2,86),(62,2,87),(63,2,88),(64,2,89),(65,2,90),(66,2,91),(67,2,92),(68,2,93),(69,2,94),(70,2,95),(71,2,96),(72,2,97),(73,2,98),(74,2,99),(75,2,100),(76,2,101),(77,2,102),(78,2,103),(79,2,104),(80,2,105),(81,2,106),(82,2,107),(83,2,108),(84,2,109),(85,2,110),(86,2,111),(87,2,112),(88,2,113),(89,2,114),(90,2,115),(91,3,116),(92,3,117),(93,3,118),(94,3,119),(95,3,120),(96,3,121),(97,3,122),(98,3,123),(99,3,124),(100,3,125),(101,3,126),(102,3,127),(103,3,128),(104,3,129),(105,3,130),(106,3,131),(107,3,132),(108,3,133),(109,3,134),(110,3,135),(111,4,136),(112,4,137),(113,4,138),(114,4,139),(115,4,140),(116,4,141),(117,4,142),(118,4,143),(119,4,144),(120,4,145),(121,4,146),(122,4,147),(123,4,148),(124,4,149),(125,4,150),(126,4,151),(127,4,152),(128,4,153),(129,4,154),(130,4,155),(131,4,156),(132,4,157),(133,4,158),(134,4,159),(135,4,160),(136,4,161),(137,4,162),(138,4,163),(139,4,164),(140,4,165),(141,4,166),(142,4,167),(143,4,168),(144,4,169),(145,4,170),(146,4,171),(147,4,172),(148,4,173),(149,4,174),(150,4,175),(151,4,176),(152,4,177),(153,4,178),(154,4,179),(155,4,180),(156,4,181),(157,4,182),(158,4,183),(159,4,184),(160,4,185),(161,4,186),(162,4,187),(163,4,188),(164,4,189),(165,4,190),(166,4,191),(167,4,192),(168,4,193),(169,4,194),(170,4,195),(171,5,196),(172,5,197),(173,5,198),(174,5,199),(175,5,200),(176,5,201),(177,5,202),(178,5,203),(179,5,204),(180,5,205),(181,5,206),(182,5,207),(183,5,208),(184,5,209),(185,5,210),(186,5,211),(187,5,212),(188,5,213),(189,5,214),(190,5,215),(191,5,216),(192,5,217),(193,5,218),(194,5,219),(195,5,220),(196,6,221),(197,6,222),(198,6,223),(199,6,224),(200,6,225),(201,6,226),(202,6,227),(203,6,228),(204,6,229),(205,6,230),(206,6,231),(207,6,232),(208,6,233),(209,6,234),(210,6,235),(211,6,236),(212,6,237),(213,6,238),(214,6,239),(215,6,240),(216,6,241),(217,6,242),(218,6,243),(219,6,244),(220,6,245),(221,7,246),(222,7,247),(223,7,248),(224,7,249),(225,7,250),(226,7,251),(227,7,252),(228,7,253),(229,7,254),(230,7,255),(231,7,256),(232,7,257),(233,7,258),(234,7,259),(235,7,260),(236,7,261),(237,7,262),(238,7,263),(239,7,264),(240,7,265),(241,7,266),(242,7,267),(243,7,268),(244,7,269),(245,7,270),(246,8,271),(247,8,272),(248,8,273),(249,8,274),(250,8,275),(251,8,276),(252,8,277),(253,8,278),(254,8,279),(255,8,280),(256,8,281),(257,8,282),(258,8,283),(259,8,284),(260,8,285),(261,8,286),(262,8,287),(263,8,288),(264,8,289),(265,8,290),(266,8,291),(267,8,292),(268,8,293),(269,8,294),(270,8,295),(271,8,296),(272,8,297),(273,8,298),(274,8,299),(275,8,300);
/*!40000 ALTER TABLE `studentprograms` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `studyprograms`
--

DROP TABLE IF EXISTS `studyprograms`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `studyprograms` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Code` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Name` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Points` decimal(65,30) NOT NULL,
  `Level` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `NUS_code` int NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `studyprograms`
--

LOCK TABLES `studyprograms` WRITE;
/*!40000 ALTER TABLE `studyprograms` DISABLE KEYS */;
INSERT INTO `studyprograms` VALUES (1,'HS-BEP','Backend-programmering',120.000000000000000000000000000000,'Toårig fagskolestudium',554151),(2,'HS-BEI','Bærekraftig entrepenørskap og innovasjon',120.000000000000000000000000000000,'Toårig fagskolestudium',541112),(3,'HS-BEPN','Backend-programmering',120.000000000000000000000000000000,'Toårig fagskolestudium',554151),(4,'HD-QA','QA Programvaretesting',120.000000000000000000000000000000,'Toårig fagskolestudium',554151),(5,'HS-FEP','Frontend-programmering',120.000000000000000000000000000000,'Toårig fagskolestudium',554151),(6,'HS-CYB','Cyber-sikkerhet',120.000000000000000000000000000000,'Toårig fagskolestudium',554151);
/*!40000 ALTER TABLE `studyprograms` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `teachercourses`
--

DROP TABLE IF EXISTS `teachercourses`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `teachercourses` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `CourseImplementationId` int NOT NULL,
  `UserId` int NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_TeacherCourses_CourseImplementationId` (`CourseImplementationId`),
  KEY `IX_TeacherCourses_UserId` (`UserId`),
  CONSTRAINT `FK_TeacherCourses_CourseImplementations_CourseImplementationId` FOREIGN KEY (`CourseImplementationId`) REFERENCES `courseimplementations` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_TeacherCourses_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `users` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=135 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `teachercourses`
--

LOCK TABLES `teachercourses` WRITE;
/*!40000 ALTER TABLE `teachercourses` DISABLE KEYS */;
INSERT INTO `teachercourses` VALUES (68,1,6),(69,2,6),(70,3,6),(71,4,6),(72,10,6),(73,11,6),(74,12,6),(75,13,6),(76,5,7),(77,6,7),(78,7,7),(79,8,7),(80,9,7),(81,14,7),(82,15,7),(83,16,7),(84,17,7),(85,18,7),(86,19,8),(87,20,8),(88,21,8),(89,22,8),(90,23,9),(91,24,9),(92,25,9),(93,26,9),(94,27,9),(95,28,10),(96,29,10),(97,30,10),(98,31,10),(99,32,10),(100,33,10),(101,34,10),(102,35,10),(103,36,11),(104,37,11),(105,38,11),(106,39,11),(107,40,11),(108,41,11),(109,42,11),(110,43,11),(111,44,12),(112,45,12),(113,46,12),(114,47,12),(115,48,12),(116,49,12),(117,50,12),(118,51,12),(119,52,13),(120,53,13),(121,54,13),(122,55,13),(123,56,13),(124,57,13),(125,58,13),(126,59,13),(127,60,14),(128,61,14),(129,62,14),(130,63,14),(131,64,14),(132,65,14),(133,66,14),(134,67,14);
/*!40000 ALTER TABLE `teachercourses` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `teacherprograms`
--

DROP TABLE IF EXISTS `teacherprograms`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `teacherprograms` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `ProgramImplementationId` int NOT NULL,
  `UserId` int NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_TeacherPrograms_ProgramImplementationId` (`ProgramImplementationId`),
  KEY `IX_TeacherPrograms_UserId` (`UserId`),
  CONSTRAINT `FK_TeacherPrograms_ProgramImplementations_ProgramImplementation~` FOREIGN KEY (`ProgramImplementationId`) REFERENCES `programimplementations` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_TeacherPrograms_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `users` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `teacherprograms`
--

LOCK TABLES `teacherprograms` WRITE;
/*!40000 ALTER TABLE `teacherprograms` DISABLE KEYS */;
INSERT INTO `teacherprograms` VALUES (1,1,7),(2,2,7),(3,3,13),(4,4,7),(5,5,14),(6,6,11),(7,7,11),(8,8,10);
/*!40000 ALTER TABLE `teacherprograms` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `userexamimplementations`
--

DROP TABLE IF EXISTS `userexamimplementations`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `userexamimplementations` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `ExamImplementationId` int NOT NULL,
  `UserId` int NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_UserExamImplementations_ExamImplementationId` (`ExamImplementationId`),
  KEY `IX_UserExamImplementations_UserId` (`UserId`),
  CONSTRAINT `FK_UserExamImplementations_ExamImplementations_ExamImplementati~` FOREIGN KEY (`ExamImplementationId`) REFERENCES `examimplementations` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_UserExamImplementations_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `users` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=51 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `userexamimplementations`
--

LOCK TABLES `userexamimplementations` WRITE;
/*!40000 ALTER TABLE `userexamimplementations` DISABLE KEYS */;
INSERT INTO `userexamimplementations` VALUES (1,1,26),(2,1,27),(3,1,28),(4,1,29),(5,1,30),(6,1,31),(7,1,32),(8,1,33),(9,1,34),(10,1,35),(11,1,36),(12,1,37),(13,1,38),(14,1,39),(15,1,40),(16,1,41),(17,1,42),(18,1,43),(19,1,44),(20,1,45),(21,1,46),(22,1,47),(23,1,48),(24,1,49),(25,1,50),(26,2,51),(27,2,52),(28,2,53),(29,2,54),(30,2,55),(31,2,56),(32,2,57),(33,2,58),(34,2,59),(35,2,60),(36,2,61),(37,2,62),(38,2,63),(39,2,64),(40,2,65),(41,2,66),(42,2,67),(43,2,68),(44,2,69),(45,2,70),(46,2,71),(47,2,72),(48,2,73),(49,2,74),(50,2,75);
/*!40000 ALTER TABLE `userexamimplementations` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `users`
--

DROP TABLE IF EXISTS `users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `users` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `GokstadEmail` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `FirstName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `LastName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Email2` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Email3` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Role` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Salt` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `HashedPassword` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=301 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `users`
--

LOCK TABLES `users` WRITE;
/*!40000 ALTER TABLE `users` DISABLE KEYS */;
INSERT INTO `users` VALUES (1,'johannes.andersen@gokstadakademiet.no','Johannes','Andersen','','','admin','$2b$12$oRR5N1.NQGQAHcSZlycJOu','$2b$12$oRR5N1.NQGQAHcSZlycJOuR4sfTc2XadUAi.3tKOZi/JVIC/zNNBi'),(2,'markus.andersen@gokstadakademiet.no','Markus','Andersen','','','admin','$2b$12$4YSeZsQHJyt13LhBsJrx6u','$2b$12$4YSeZsQHJyt13LhBsJrx6uEb5MBTP8zcx9sMjgm5QsJWAC6FxPWXO'),(3,'anders.magnussen@gokstadakademiet.no','Anders','Magnussen','','','admin','$2b$12$4dBI16bq2jymH1qedCw6ZO','$2b$12$4dBI16bq2jymH1qedCw6ZOP7PfrE5k5hGAHuy8W3goekAZ/Azi.r6'),(4,'lars.hellum@gokstadakademiet.no','Lars','Hellum','','','admin','$2b$12$XLvOae.n7M4/gGrDlmJcX.','$2b$12$XLvOae.n7M4/gGrDlmJcX.Zp9k70EPvE6E5GLSpy2ovsNiUZgiab2'),(5,'mari.moen@gokstadakademiet.no','Mari','Moen','','','admin','$2b$12$B4UEB7mZdx.OI5vjMxm/uu','$2b$12$B4UEB7mZdx.OI5vjMxm/uuIPNuhDzEBV8d0EIrFzFABm7A6.Mvgca'),(6,'kamilla.hellum@gokstadakademiet.no','Kamilla','Hellum','','','teacher','$2b$12$szobSbgh.O4aCnO27I6EWu','$2b$12$szobSbgh.O4aCnO27I6EWuXHyYptSaN4q7hDqidXvr1ManZP.K2gu'),(7,'johannes.hansen@gokstadakademiet.no','Johannes','Hansen','','','teacher','$2b$12$mAY4bkmGf2GokS71JNoWre','$2b$12$mAY4bkmGf2GokS71JNoWreKKHMK3Iug4tiXBu8lGHaSyltohOsUai'),(8,'beate.olsen@gokstadakademiet.no','Beate','Olsen','','','teacher','$2b$12$6Y0pIrUlYe8sAf56B2QGFu','$2b$12$6Y0pIrUlYe8sAf56B2QGFumYMyHMyQWlvLSZ3R4Vcn4E11Pw1Qvt2'),(9,'ella.magnussen@gokstadakademiet.no','Ella','Magnussen','','','teacher','$2b$12$1eJkyIP82s.G68QxXQM4Ye','$2b$12$1eJkyIP82s.G68QxXQM4YeRnR5BZZbCkuASOxEdBcHwhwBK5CHlZm'),(10,'ole.larsen@gokstadakademiet.no','Ole','Larsen','','','teacher','$2b$12$IWcA3XBqTnL7u6opfO.PQu','$2b$12$IWcA3XBqTnL7u6opfO.PQu5Mc27RSNekvHbDiFm3GtA/LDqQCuUsS'),(11,'sara.eriksen@gokstadakademiet.no','Sara','Eriksen','','','teacher','$2b$12$uN2DDJmK3IxfRtefTTNEpe','$2b$12$uN2DDJmK3IxfRtefTTNEpeBL7U.9uGB6Gc0V27UAETQ4XrG2ufIgC'),(12,'otto.hellum@gokstadakademiet.no','Otto','Hellum','','','teacher','$2b$12$YKonR7cKz9WVPB6N.w.Ca.','$2b$12$YKonR7cKz9WVPB6N.w.Ca.rCbBamgq9gtJAp5xEXZrsKeTqJoKXdm'),(13,'nora.larsen@gokstadakademiet.no','Nora','Larsen','','','teacher','$2b$12$254SwcHUVUiIh8JuZnqx8.','$2b$12$254SwcHUVUiIh8JuZnqx8.GNKl2pq3/9/drB88RMEoAoiTASuKC9W'),(14,'anders.andersen@gokstadakademiet.no','Anders','Andersen','','','teacher','$2b$12$2q2fANb85oDyvaFbrg.Iz.','$2b$12$2q2fANb85oDyvaFbrg.Iz.hGry06/ks43vnZrlOy5gXvgBrmt.pDu'),(15,'beate.pettersen@gokstadakademiet.no','Beate','Pettersen','','','teacher','$2b$12$UpwKuWo3tDH/MzMz266CAu','$2b$12$UpwKuWo3tDH/MzMz266CAuFx0CCTSoaDiuhkMFhfik22vXlMxG1Va'),(16,'ella.hansen@gokstadakademiet.no','Ella','Hansen','','','teacher','$2b$12$m1Wwqj6FuK.vHc0tVknfAe','$2b$12$m1Wwqj6FuK.vHc0tVknfAeOh7dI.heEFrm0mnOyxFflEU5mS9jRjK'),(17,'lukas.hellum@gokstadakademiet.no','Lukas','Hellum','','','teacher','$2b$12$j1V0EXV.Kl2U4jgXhpR0KO','$2b$12$j1V0EXV.Kl2U4jgXhpR0KOAHQVlXTe8GMhOgjSu1filBlFOkpRRne'),(18,'amalie.eriksen@gokstadakademiet.no','Amalie','Eriksen','','','teacher','$2b$12$.W0Qg5EZrJC4TzmQ37cRR.','$2b$12$.W0Qg5EZrJC4TzmQ37cRR.KxCbLOduJpzNoREnwmcNtn0Cpg/V61i'),(19,'ole.hansen@gokstadakademiet.no','Ole','Hansen','','','teacher','$2b$12$TaziLc3ZcJBjQth4214cCe','$2b$12$TaziLc3ZcJBjQth4214cCertwy1eQriXC4HaFNhpgb44o3rl0ilWS'),(20,'beate.sørensen@gokstadakademiet.no','Beate','Sørensen','','','teacher','$2b$12$SDfoZ1S80h8QET6IY5TinO','$2b$12$SDfoZ1S80h8QET6IY5TinOJ.BCYQhw.p886jvcUQlTP49WcMJnnGy'),(21,'ella.magnussen1@gokstadakademiet.no','Ella','Magnussen','','','teacher','$2b$12$UoV8f6XhazEiEt3zB1eQwu','$2b$12$UoV8f6XhazEiEt3zB1eQwuoyaarI8nKk8WCOFpsh3j.f3tsVSQgzy'),(22,'helene.andersen@gokstadakademiet.no','Helene','Andersen','','','teacher','$2b$12$XLaur8ineQ0yzMMksPbJ1.','$2b$12$XLaur8ineQ0yzMMksPbJ1.zKfmHUBUVAgP9T54Q./xlOXDF8YWK8m'),(23,'jonas.moen@gokstadakademiet.no','Jonas','Moen','','','teacher','$2b$12$QZVc1CR2e03mOPsVy3ebN.','$2b$12$QZVc1CR2e03mOPsVy3ebN.E2KP4AP4S0v/bradk5WlFt7oVmBAzlK'),(24,'johannes.olsen@gokstadakademiet.no','Johannes','Olsen','','','teacher','$2b$12$jw8t1UXfsAG4cP7qd6QN/e','$2b$12$jw8t1UXfsAG4cP7qd6QN/eMnW/FxsUMC9glDhK8FY1F8IHNv0aE7K'),(25,'johannes.hellum@gokstadakademiet.no','Johannes','Hellum','','','teacher','$2b$12$skrt7efdugX47/DC8rMJpe','$2b$12$skrt7efdugX47/DC8rMJpexS.DYc7n7mYJ361JvEUCYJoZpHUwvgG'),(26,'anders.olsen@gokstadakademiet.no','Anders','Olsen','','','student','$2b$12$Db8cq4/jNiMD/16x.h8Vje','$2b$12$Db8cq4/jNiMD/16x.h8VjeuF43sE.zQt0SliPSYwCXCRpHwQCAXwC'),(27,'aleksander.hellum@gokstadakademiet.no','Aleksander','Hellum','','','student','$2b$12$Z5EsOZA9bzwfxI.CbeHLz.','$2b$12$Z5EsOZA9bzwfxI.CbeHLz.o84L1mx9Q0lXkBCigGYr4sEUq1IuUTC'),(28,'jonas.andersen@gokstadakademiet.no','Jonas','Andersen','','','student','$2b$12$MJtJ2WTH8DBT0RC4DUh0mO','$2b$12$MJtJ2WTH8DBT0RC4DUh0mOaK2UFeb2hPg5GulW0J43lbuUCpkp31a'),(29,'nora.andreassen@gokstadakademiet.no','Nora','Andreassen','','','student','$2b$12$L72dGsqS1A/MKDvJimq.EO','$2b$12$L72dGsqS1A/MKDvJimq.EObyoX.8xrGgVWmJ6PLqliAcDfG/affc6'),(30,'nora.moe@gokstadakademiet.no','Nora','Moe','','','student','$2b$12$zDn7uQvJ1FNHQ/uYU9Foh.','$2b$12$zDn7uQvJ1FNHQ/uYU9Foh.jEdgCVExfNJWCjMDg5HlPktOmvdoGNm'),(31,'ella.andersen@gokstadakademiet.no','Ella','Andersen','','','student','$2b$12$7YByIm2c97UPU34UqEfNxu','$2b$12$7YByIm2c97UPU34UqEfNxu.dqfT1YhFD.Cc3uJmcZtnXj5cAjZYsa'),(32,'mari.pettersen@gokstadakademiet.no','Mari','Pettersen','','','student','$2b$12$XIj78dKDFYUiaJmSMu3.8u','$2b$12$XIj78dKDFYUiaJmSMu3.8ux/51/b4ESlpwAOV2NTTStpWOG40aPfW'),(33,'markus.hansen@gokstadakademiet.no','Markus','Hansen','','','student','$2b$12$eaZQbl0TJQiLd2.K.N7xie','$2b$12$eaZQbl0TJQiLd2.K.N7xieLMDn0RjSl684YpVtzBTCMUEiIPCMVkO'),(34,'ingrid.olsen@gokstadakademiet.no','Ingrid','Olsen','','','student','$2b$12$i862hS06fJjnaGBkN1z/hO','$2b$12$i862hS06fJjnaGBkN1z/hO5IvTigJ5EtIbK2Z3am/qnW5BJTrbdhm'),(35,'anders.berg@gokstadakademiet.no','Anders','Berg','','','student','$2b$12$lR1EeIdsJ.7GL2cevEAETu','$2b$12$lR1EeIdsJ.7GL2cevEAETuBa.djX6DGdkSqlQPoaiRWSGA2nMJItG'),(36,'nora.moen@gokstadakademiet.no','Nora','Moen','','','student','$2b$12$NAZcAwfHO3sHAKIViFykGO','$2b$12$NAZcAwfHO3sHAKIViFykGOqYMFrNAfcVuhTWPHrLTtHqu69vVzE3y'),(37,'nora.andersen@gokstadakademiet.no','Nora','Andersen','','','student','$2b$12$q9bNUbJNSEISNkwNGrrFZO','$2b$12$q9bNUbJNSEISNkwNGrrFZOq4k4bG5a4VO9ojeuDSt/XvYKudiHNHi'),(38,'ella.olsen@gokstadakademiet.no','Ella','Olsen','','','student','$2b$12$YETn.LfEDJZ6W96u40AgV.','$2b$12$YETn.LfEDJZ6W96u40AgV.x0bxH/kyKYlCT3GHEt8JcMnc1GfYzy2'),(39,'otto.moen@gokstadakademiet.no','Otto','Moen','','','student','$2b$12$tWWlMiJn1L.rEy6tTHvgu.','$2b$12$tWWlMiJn1L.rEy6tTHvgu.Id606FiCA7cYBs6KazbhBUxgfkAjVFu'),(40,'jonas.olsen@gokstadakademiet.no','Jonas','Olsen','','','student','$2b$12$qI8Wm81rRBV5FhVD8o6si.','$2b$12$qI8Wm81rRBV5FhVD8o6si..GKfWQnMQhgWLRpbaVcI1j5Pw/e2Nqq'),(41,'ella.hansen1@gokstadakademiet.no','Ella','Hansen','','','student','$2b$12$atIjWLYVrn5Lno1LCXzVaO','$2b$12$atIjWLYVrn5Lno1LCXzVaOsRKbyOC09wzngX7AtfOme.ah1HRzq.u'),(42,'anders.eriksen@gokstadakademiet.no','Anders','Eriksen','','','student','$2b$12$kjpZRN4C/irP52NFsVOLGO','$2b$12$kjpZRN4C/irP52NFsVOLGO1OVhGS0jRyeeNQ11vtnAki93t0fZcpq'),(43,'kamilla.moe@gokstadakademiet.no','Kamilla','Moe','','','student','$2b$12$lF.b.urnEiYrVYtRyccNAu','$2b$12$lF.b.urnEiYrVYtRyccNAuCAssHZpdY81yURqqri6nbEb52Xpt9Pe'),(44,'mette.andreassen@gokstadakademiet.no','Mette','Andreassen','','','student','$2b$12$WiXgq5VNqGovHL0GCGgxoO','$2b$12$WiXgq5VNqGovHL0GCGgxoOz5uXlQjiQ6TXT1CJw.zSiNtdEG/kQLy'),(45,'otto.hansen@gokstadakademiet.no','Otto','Hansen','','','student','$2b$12$sVdh8NExAGq28ynKDfq3Xu','$2b$12$sVdh8NExAGq28ynKDfq3Xulz9Jp/Zpk0CVgZ9CoE2kyvFkh5vckTS'),(46,'johannes.andreassen@gokstadakademiet.no','Johannes','Andreassen','','','student','$2b$12$ECAwNwFXoWucclMP2SUX2u','$2b$12$ECAwNwFXoWucclMP2SUX2uSrXWZn1VBqDn3XWS9J6NhxwXmID.Eem'),(47,'jonas.pettersen@gokstadakademiet.no','Jonas','Pettersen','','','student','$2b$12$NaTpwIXo4soAXJvbWw68he','$2b$12$NaTpwIXo4soAXJvbWw68heGUYTZrLtvTLuaaJaW0nJGyWSce7D8Ci'),(48,'lukas.olsen@gokstadakademiet.no','Lukas','Olsen','','','student','$2b$12$Jb6Y6GY7Dx1muuPZtj.cUe','$2b$12$Jb6Y6GY7Dx1muuPZtj.cUeR2jLOLE5uL213..XVK2QyqnPuDEEkRO'),(49,'mette.eriksen@gokstadakademiet.no','Mette','Eriksen','','','student','$2b$12$0fNrYblba3I7g7AW/3Lfk.','$2b$12$0fNrYblba3I7g7AW/3Lfk.QqR4vnWZN5Xvg.MUMNBIRo9LKi1YkoW'),(50,'lukas.larsen@gokstadakademiet.no','Lukas','Larsen','','','student','$2b$12$.840RhBZKXCQgQ.vdlNKXe','$2b$12$.840RhBZKXCQgQ.vdlNKXeiNWRnmP7oAq9Ck4/vtXehWBIZ6FlEfy'),(51,'aleksander.andreassen@gokstadakademiet.no','Aleksander','Andreassen','','','student','$2b$12$q0kj/AI8m9meUMUDB5A.1u','$2b$12$q0kj/AI8m9meUMUDB5A.1uM7LLezyd/ImZJJu8A/qCqMVVSYhoMPe'),(52,'jonas.larsen@gokstadakademiet.no','Jonas','Larsen','','','student','$2b$12$rcYbSaQpwJn.Z8XWEKQGoO','$2b$12$rcYbSaQpwJn.Z8XWEKQGoOY0Roc0n7bLttCfwdp5emS4C9DRFYBWu'),(53,'jonas.johansen@gokstadakademiet.no','Jonas','Johansen','','','student','$2b$12$ProOFV/Sg3GEcu2scdGU3e','$2b$12$ProOFV/Sg3GEcu2scdGU3elXV6vhuwE6JpmTyHPMzVCxfIZ9n5pp6'),(54,'ella.berg@gokstadakademiet.no','Ella','Berg','','','student','$2b$12$zPU6hYANtUn5M8Ym6vcKQu','$2b$12$zPU6hYANtUn5M8Ym6vcKQuBf5lqiOwGAt1VqTdEOzdcoF/qm0qtPa'),(55,'markus.eriksen@gokstadakademiet.no','Markus','Eriksen','','','student','$2b$12$n5xmIBBFiZUAJXM/yIO/lO','$2b$12$n5xmIBBFiZUAJXM/yIO/lO9vaRkj/mv4bcJvDEWvzuxIatfXruFWW'),(56,'beate.larsen@gokstadakademiet.no','Beate','Larsen','','','student','$2b$12$6qfMA0/.ojeznlqVipr.Ee','$2b$12$6qfMA0/.ojeznlqVipr.EesCzFn3xaKzgAw8yNF95aDso8F4iw1Bi'),(57,'aleksander.larsen@gokstadakademiet.no','Aleksander','Larsen','','','student','$2b$12$wJeY6EXyXK6nuVfmhnT4hO','$2b$12$wJeY6EXyXK6nuVfmhnT4hOs72W4IUFuALhzKog0IbRlic7DJA8UIW'),(58,'kristian.johansen@gokstadakademiet.no','Kristian','Johansen','','','student','$2b$12$ZOF6rFuP6ScL3ILEXaooEu','$2b$12$ZOF6rFuP6ScL3ILEXaooEuTT.JJ11zgqmbb2vRXVyHMJiQK76BN/K'),(59,'johannes.pettersen@gokstadakademiet.no','Johannes','Pettersen','','','student','$2b$12$GoaaOt0MKxJIyhSV3is7wu','$2b$12$GoaaOt0MKxJIyhSV3is7wupXb8y9Uo5623kaxdJwxr4OBDmnJbq..'),(60,'mette.andreassen1@gokstadakademiet.no','Mette','Andreassen','','','student','$2b$12$M.3szZLx5.wJoOMsiuCU9O','$2b$12$M.3szZLx5.wJoOMsiuCU9OuGARU7ldmg39Pp.5c/Bkoaez9d0ZvP.'),(61,'otto.johansen@gokstadakademiet.no','Otto','Johansen','','','student','$2b$12$ErQKQaBajuIpmPrRg/Pq9.','$2b$12$ErQKQaBajuIpmPrRg/Pq9.gOvKs6CJ7SPgxKsahqJZ3tR3vX3CB0.'),(62,'ole.berg@gokstadakademiet.no','Ole','Berg','','','student','$2b$12$QBJaLgHLA1jsI6jYoerxIe','$2b$12$QBJaLgHLA1jsI6jYoerxIeZiooXdvAKJ8dJjhDnlOMIbjKxuBcfq2'),(63,'ella.moen@gokstadakademiet.no','Ella','Moen','','','student','$2b$12$OKenbFL2XKFNfnVKElbpOO','$2b$12$OKenbFL2XKFNfnVKElbpOOEh4ch69SL3PLE8Cm.b/vRMGhENvyF7C'),(64,'nils.johansen@gokstadakademiet.no','Nils','Johansen','','','student','$2b$12$4ovc7s/sW0dvjNQS13XDoe','$2b$12$4ovc7s/sW0dvjNQS13XDoelM62K84ZouEaTjpBVIJ9BrpE4CGlMda'),(65,'mette.andersen@gokstadakademiet.no','Mette','Andersen','','','student','$2b$12$MOf4Kvervy3exx9lOdERp.','$2b$12$MOf4Kvervy3exx9lOdERp..cBC8f6TibXGTzNBK3YJH1OM.2y/z6O'),(66,'mari.pettersen1@gokstadakademiet.no','Mari','Pettersen','','','student','$2b$12$ZFLTGrq/OuND6jgsxsaP8e','$2b$12$ZFLTGrq/OuND6jgsxsaP8ek2u6l9ngGyPnykRycL.ZeVByoJCFKvu'),(67,'aleksander.magnussen@gokstadakademiet.no','Aleksander','Magnussen','','','student','$2b$12$1CZbfE2ugDBiktvPqNl8W.','$2b$12$1CZbfE2ugDBiktvPqNl8W.2tR6pzovppGXT5wYp9sjcGZnsokn38C'),(68,'nora.johansen@gokstadakademiet.no','Nora','Johansen','','','student','$2b$12$LRGnds51DjRrrrkAByPgau','$2b$12$LRGnds51DjRrrrkAByPgau0Asqy7.xtLTzyqenbHWAxiYYD6ggXRm'),(69,'amalie.magnussen@gokstadakademiet.no','Amalie','Magnussen','','','student','$2b$12$eo2Hc70i1U433HaP7xKIku','$2b$12$eo2Hc70i1U433HaP7xKIkuFlvsyYnvkFnCyMf9yFS9nVD3hiiSgPO'),(70,'otto.magnussen@gokstadakademiet.no','Otto','Magnussen','','','student','$2b$12$46qjmGmVJCYd7vQUDaXxfe','$2b$12$46qjmGmVJCYd7vQUDaXxfe/qwS94hWVVhyZDOPzvcRmQz4RT/OeK.'),(71,'sara.johansen@gokstadakademiet.no','Sara','Johansen','','','student','$2b$12$iLF/J2VStGjdOglafpWI4e','$2b$12$iLF/J2VStGjdOglafpWI4ezglhFhkUHpoZbNJ9m9eD9snH7sCa.Bq'),(72,'helene.eriksen@gokstadakademiet.no','Helene','Eriksen','','','student','$2b$12$ZExK9WeMKZx3wsfD4c9yFu','$2b$12$ZExK9WeMKZx3wsfD4c9yFuGFjXhwWOG8zsLfC4WGt5RoeCRVp0Cdu'),(73,'ella.hansen2@gokstadakademiet.no','Ella','Hansen','','','student','$2b$12$SA1U/b9ERSUWjlh2mpgyW.','$2b$12$SA1U/b9ERSUWjlh2mpgyW.DWqP6b4lPhwg1WGbX4HiQANYd2TP2Xq'),(74,'jonas.larsen1@gokstadakademiet.no','Jonas','Larsen','','','student','$2b$12$ITqjt7UGDwzWXepmBB/Nd.','$2b$12$ITqjt7UGDwzWXepmBB/Nd.VYJZfHWDsvwkRPFDfVQWPcWfAJnVuIC'),(75,'nora.andreassen1@gokstadakademiet.no','Nora','Andreassen','','','student','$2b$12$4Zx5MiM7dvjbsIw/1bX1Du','$2b$12$4Zx5MiM7dvjbsIw/1bX1DumMMeKaL5.Gohw9fFwdonvxik5GOLfPC'),(76,'aleksander.pedersen@gokstadakademiet.no','Aleksander','Pedersen','','','student','$2b$12$ppm/PBtSAG1yagASELWz3e','$2b$12$ppm/PBtSAG1yagASELWz3eyWMpNBtGdv9859gq3XTucYXIAGLVXH6'),(77,'lars.moen@gokstadakademiet.no','Lars','Moen','','','student','$2b$12$PsP7fYjVTh9ZnOM/CWrCdu','$2b$12$PsP7fYjVTh9ZnOM/CWrCduAy2Ww33uHCSNAHT24mEihJ074G.G/m6'),(78,'nora.magnussen@gokstadakademiet.no','Nora','Magnussen','','','student','$2b$12$PAQhsOUarlF3Qct47RAD9u','$2b$12$PAQhsOUarlF3Qct47RAD9uJvE6LxmWXirLwHmC5NWilTN6T2tJcy6'),(79,'lukas.pedersen@gokstadakademiet.no','Lukas','Pedersen','','','student','$2b$12$MUt8yTR2CpsF/hauaah4pu','$2b$12$MUt8yTR2CpsF/hauaah4puVbITDG6dTcs6c0y/ytESIwEIK3l.Lcq'),(80,'lars.andersen@gokstadakademiet.no','Lars','Andersen','','','student','$2b$12$XFtr8sM367AOpjlTmTgkBu','$2b$12$XFtr8sM367AOpjlTmTgkBu78cCuWHJbeZbqwjGQb7VbMbi4PWs6lu'),(81,'kamilla.andersen@gokstadakademiet.no','Kamilla','Andersen','','','student','$2b$12$yhs7e/Z.96mNSIHUI7pPO.','$2b$12$yhs7e/Z.96mNSIHUI7pPO.g4mqH6LNzdNQbfDMgeEZpVk01wvW.zG'),(82,'sara.larsen@gokstadakademiet.no','Sara','Larsen','','','student','$2b$12$PxBIbXsrp5BOx4wm.3lSVe','$2b$12$PxBIbXsrp5BOx4wm.3lSVePk2/MZmAyK7URcJMGoIQBZMrKZN6KGK'),(83,'sara.magnussen@gokstadakademiet.no','Sara','Magnussen','','','student','$2b$12$pq0akpYWIzkq2SibfoTiee','$2b$12$pq0akpYWIzkq2SibfoTieepZ75C/D/zgTe1x0Vr4vHWNnZlLtIelS'),(84,'lars.johansen@gokstadakademiet.no','Lars','Johansen','','','student','$2b$12$.snB6qfzaUTrQUvwqY8b3e','$2b$12$.snB6qfzaUTrQUvwqY8b3efas09AtxsfpP.Ktpemn9eZGnsKUTOAG'),(85,'lars.johansen1@gokstadakademiet.no','Lars','Johansen','','','student','$2b$12$VOcVtifn2xaOe1kKBSuVpu','$2b$12$VOcVtifn2xaOe1kKBSuVpu7efHYR4jGcAScdf6IHKuNDTdxKXXFZe'),(86,'aleksander.larsen1@gokstadakademiet.no','Aleksander','Larsen','','','student','$2b$12$sCyfTF8YrooV6oy7oDK9gO','$2b$12$sCyfTF8YrooV6oy7oDK9gOt5PiGPiqBVXCS44LoLKBiuTXzY2wE7.'),(87,'amalie.moe@gokstadakademiet.no','Amalie','Moe','','','student','$2b$12$Y1XbVbnM22aEI9Mgxh4YQe','$2b$12$Y1XbVbnM22aEI9Mgxh4YQeSpUY63xoi2cPnwqUMVsHpEL64I4TYtC'),(88,'johannes.olsen1@gokstadakademiet.no','Johannes','Olsen','','','student','$2b$12$4iyv8D.1OHw3kSGenKXVaO','$2b$12$4iyv8D.1OHw3kSGenKXVaODBnXVuKJAy7HiO.DqWUh/WMil9id8Va'),(89,'ole.moen@gokstadakademiet.no','Ole','Moen','','','student','$2b$12$MB/b0xb8wPdE7bCS1os9Vu','$2b$12$MB/b0xb8wPdE7bCS1os9VuCQOkxatHGerj8F8i63GxXDSjA05w/F2'),(90,'aleksander.olsen@gokstadakademiet.no','Aleksander','Olsen','','','student','$2b$12$XUUPqt1oF1Kv7NJiO2HPc.','$2b$12$XUUPqt1oF1Kv7NJiO2HPc.GxPdDIl3GkUOyjBvf1L3.P5gleHleG2'),(91,'lars.pettersen@gokstadakademiet.no','Lars','Pettersen','','','student','$2b$12$jU55/NN/sKQ7t3vcI.Dose','$2b$12$jU55/NN/sKQ7t3vcI.Doseom5gQEfN6E73DetTgq6ofgBrzaddp6q'),(92,'lukas.hansen@gokstadakademiet.no','Lukas','Hansen','','','student','$2b$12$7UVlStGHv13uBzYkO2d85O','$2b$12$7UVlStGHv13uBzYkO2d85OSiS/vhN93445t0vd7riLWnDOdjJ3DJe'),(93,'kristian.magnussen@gokstadakademiet.no','Kristian','Magnussen','','','student','$2b$12$rFvyScJragYP9PXT3MvD7.','$2b$12$rFvyScJragYP9PXT3MvD7.eyvGsfdFQE5HDcqcEIYximIDVE7rAq.'),(94,'lukas.andersen@gokstadakademiet.no','Lukas','Andersen','','','student','$2b$12$kmDCLqONt5pHp4ZtY9KMZO','$2b$12$kmDCLqONt5pHp4ZtY9KMZOuWhqGRfStMosy2LOXuGdlEorFN2ZZtG'),(95,'ella.berg1@gokstadakademiet.no','Ella','Berg','','','student','$2b$12$FLaiJFgDWFDNPWIz6s5dLe','$2b$12$FLaiJFgDWFDNPWIz6s5dLe9/hH2L4Wyov3hELkzrRz.CzLfYHG0E.'),(96,'markus.sørensen@gokstadakademiet.no','Markus','Sørensen','','','student','$2b$12$Hpd82Lk5Lx4hBYXEYs1Q9O','$2b$12$Hpd82Lk5Lx4hBYXEYs1Q9ONfnm9ULokA7tdELMcJP/JRUYhOKiUsi'),(97,'nora.eriksen@gokstadakademiet.no','Nora','Eriksen','','','student','$2b$12$LJtD/m16n6K3mmfnvEpjqu','$2b$12$LJtD/m16n6K3mmfnvEpjqubgNmqsfJ11NsgQjIpdxwNBfvijdVqzi'),(98,'ole.hansen1@gokstadakademiet.no','Ole','Hansen','','','student','$2b$12$vFZ/UntF9gpOhh3.g.VGje','$2b$12$vFZ/UntF9gpOhh3.g.VGjezVbQzhgToEYBMrOzbRWIr/B6m94xOte'),(99,'amalie.moen@gokstadakademiet.no','Amalie','Moen','','','student','$2b$12$8JUqwf28lMl/2/cpGzL8O.','$2b$12$8JUqwf28lMl/2/cpGzL8O.D69NktqBm4VBOR5z0zMpQCmBABX/t6W'),(100,'ole.sørensen@gokstadakademiet.no','Ole','Sørensen','','','student','$2b$12$UEsH34ZdHd3XjGGhEqMPXe','$2b$12$UEsH34ZdHd3XjGGhEqMPXeZaxMIpXFLGpLIIj9V26VluGr8mR80XG'),(101,'nora.hellum@gokstadakademiet.no','Nora','Hellum','','','student','$2b$12$8ocF62Ams/pm4dWXmnayL.','$2b$12$8ocF62Ams/pm4dWXmnayL.vaZj0s.RRxV1OxW4MvHZFj8.m0dJbVa'),(102,'otto.olsen@gokstadakademiet.no','Otto','Olsen','','','student','$2b$12$7wT6tuX/ijq.J10os4Zq8e','$2b$12$7wT6tuX/ijq.J10os4Zq8e477WBzHVq6Cu2Bbhc5uZbrozipUJ.da'),(103,'markus.berg@gokstadakademiet.no','Markus','Berg','','','student','$2b$12$7oWNLsJ9yCB2F6FmQckHuu','$2b$12$7oWNLsJ9yCB2F6FmQckHuuB4NZbPO/dLTPH81jzTtz/fCds9Tvclm'),(104,'ole.johansen@gokstadakademiet.no','Ole','Johansen','','','student','$2b$12$wVP2G9LK.UmZYfF6nkdbHO','$2b$12$wVP2G9LK.UmZYfF6nkdbHOZV4ocqqcj8DuKiQ2IeiEff6f4sRb6h2'),(105,'lars.magnussen@gokstadakademiet.no','Lars','Magnussen','','','student','$2b$12$DsBP13EWtc6Sw734MTOMDu','$2b$12$DsBP13EWtc6Sw734MTOMDuKtQAWmLVJSeci3re1rWB2s3cCjfe/gy'),(106,'kristian.pettersen@gokstadakademiet.no','Kristian','Pettersen','','','student','$2b$12$1/wndWCZuXyIfA0N8fr0Ou','$2b$12$1/wndWCZuXyIfA0N8fr0Our8sM0oC8krqHsl0RsIOeJf5V4no6UA6'),(107,'aleksander.eriksen@gokstadakademiet.no','Aleksander','Eriksen','','','student','$2b$12$NM51VSTfIn6oUH.JxS2Ofe','$2b$12$NM51VSTfIn6oUH.JxS2OfeK/5Mbru28i/lEJEdJR/Tk14Ln6FMN0O'),(108,'ole.pettersen@gokstadakademiet.no','Ole','Pettersen','','','student','$2b$12$SVLZgnN/Zd4/opE.wrElje','$2b$12$SVLZgnN/Zd4/opE.wrEljerWDAyjqgYluRnT6v5YQYbc5mec0CJj2'),(109,'aleksander.larsen2@gokstadakademiet.no','Aleksander','Larsen','','','student','$2b$12$9klyL6JduaWa6fSzzsifkO','$2b$12$9klyL6JduaWa6fSzzsifkOgOp2AYKcwiPonb025n6K4BNA1qNFGB2'),(110,'mari.eriksen@gokstadakademiet.no','Mari','Eriksen','','','student','$2b$12$rtcwmJlrC0ppsihQRHkpr.','$2b$12$rtcwmJlrC0ppsihQRHkpr.KIeGnYpKycU4b38XQ/04k70Ita3ftIG'),(111,'nils.pedersen@gokstadakademiet.no','Nils','Pedersen','','','student','$2b$12$bkm0wTKpA1E04teNxpsh1O','$2b$12$bkm0wTKpA1E04teNxpsh1OYQf628gC79cHD/LwCOobFGdAllxzjSC'),(112,'amalie.sørensen@gokstadakademiet.no','Amalie','Sørensen','','','student','$2b$12$8xHJBeyin615mY.JDYDNYu','$2b$12$8xHJBeyin615mY.JDYDNYuNp36SbOCltFBGBbXTq9pYKuTCpRm63y'),(113,'ole.berg1@gokstadakademiet.no','Ole','Berg','','','student','$2b$12$DDWntO/2iQgMj/6/MwzEEe','$2b$12$DDWntO/2iQgMj/6/MwzEEeWfBE947BIT5pwpujQVU2uMdmnGpLPrS'),(114,'sara.eriksen1@gokstadakademiet.no','Sara','Eriksen','','','student','$2b$12$C.afxOvGLLV5YOLmTHA6Au','$2b$12$C.afxOvGLLV5YOLmTHA6Au5.bRPnHrJkQPPOQHEadxyDFeXCyuYb.'),(115,'helene.hansen@gokstadakademiet.no','Helene','Hansen','','','student','$2b$12$tOSAk/Xy3onxmU88pYWUdu','$2b$12$tOSAk/Xy3onxmU88pYWUduM4Ra2w3X0ggY81Rh3AM9TykHbx9t5NC'),(116,'anders.andreassen@gokstadakademiet.no','Anders','Andreassen','','','student','$2b$12$2pJtXNlmWwR0KQPx9z0p5.','$2b$12$2pJtXNlmWwR0KQPx9z0p5.6/PSBM.N1lbN8492Z0X4MkNoyih6qDi'),(117,'aleksander.hansen@gokstadakademiet.no','Aleksander','Hansen','','','student','$2b$12$/VQoY.HhnOcWLbpz48LWUO','$2b$12$/VQoY.HhnOcWLbpz48LWUOBbnvSYrdQ2ZgxC02E2D3oynjilldfwO'),(118,'sara.andersen@gokstadakademiet.no','Sara','Andersen','','','student','$2b$12$x1DYiYJWm9u0v7RIydeLu.','$2b$12$x1DYiYJWm9u0v7RIydeLu.dwOAeFYKJy3xYAlw3ChVizhzAtEK3DG'),(119,'nils.johansen1@gokstadakademiet.no','Nils','Johansen','','','student','$2b$12$O7jtrmib3ZUOOcMiZbIWre','$2b$12$O7jtrmib3ZUOOcMiZbIWrevwpEY9kSUh16csHP40ARYvQnniUkUny'),(120,'ole.magnussen@gokstadakademiet.no','Ole','Magnussen','','','student','$2b$12$6MugvydpDZoDAYf4JEDQAO','$2b$12$6MugvydpDZoDAYf4JEDQAO4Wa0sxEtsjZe9vzMB5083DW.5lC1l8.'),(121,'markus.larsen@gokstadakademiet.no','Markus','Larsen','','','student','$2b$12$AQXFKBoHnVz/gXHGRBEcWO','$2b$12$AQXFKBoHnVz/gXHGRBEcWO0yJ/dtVZh3PF//u44dSNkAm6biCzO9O'),(122,'kristian.olsen@gokstadakademiet.no','Kristian','Olsen','','','student','$2b$12$Rp8Sxlpe0/2LpCcnW4GZRu','$2b$12$Rp8Sxlpe0/2LpCcnW4GZRulj2Brx/ogchKZ9PtLGkZzY3uTlrGF5i'),(123,'sara.magnussen1@gokstadakademiet.no','Sara','Magnussen','','','student','$2b$12$8Szpi7xIlwuzPK/XCQ2BXO','$2b$12$8Szpi7xIlwuzPK/XCQ2BXObiOXzy860FJhRwUY5MT.QWBMHCIgnL6'),(124,'markus.eriksen1@gokstadakademiet.no','Markus','Eriksen','','','student','$2b$12$08.BDVyySjwnhL5UZKUWJO','$2b$12$08.BDVyySjwnhL5UZKUWJOz1telArBwJc9kLxCSEzMTUvPBTh8lRu'),(125,'johannes.hansen1@gokstadakademiet.no','Johannes','Hansen','','','student','$2b$12$DP1UPfQtWd9guQdW5Peecu','$2b$12$DP1UPfQtWd9guQdW5PeecuenEr1a/Gz5GHjypcGtXv857UvBChBvy'),(126,'ingrid.olsen1@gokstadakademiet.no','Ingrid','Olsen','','','student','$2b$12$RbiLTv2ZVB.w2mmm0pBwmu','$2b$12$RbiLTv2ZVB.w2mmm0pBwmuCSP9WbCJR38qZoqye3BHnhHHqzokewu'),(127,'mette.moen@gokstadakademiet.no','Mette','Moen','','','student','$2b$12$jMHw8qDfOM01rQwTiTUmou','$2b$12$jMHw8qDfOM01rQwTiTUmoumbHNmbJJ86N4aLjbjNzVCnx67uGPld2'),(128,'aleksander.sørensen@gokstadakademiet.no','Aleksander','Sørensen','','','student','$2b$12$.aa1MfxVixJHXoDAw5xGYu','$2b$12$.aa1MfxVixJHXoDAw5xGYu5bP1.GKN4.wml0lP93cPxShZkd48E8.'),(129,'markus.pettersen@gokstadakademiet.no','Markus','Pettersen','','','student','$2b$12$9emwfwuqu3x9oE7.m93BVu','$2b$12$9emwfwuqu3x9oE7.m93BVuwCm4ltO9Tus8NzOznzfcky4Em7LFJCK'),(130,'mari.eriksen1@gokstadakademiet.no','Mari','Eriksen','','','student','$2b$12$T5fWhPbkkJdS1WiLkahIje','$2b$12$T5fWhPbkkJdS1WiLkahIjew.YahCL39pWt8jiKRMCJAJ.hj0ZGLh2'),(131,'anders.moe@gokstadakademiet.no','Anders','Moe','','','student','$2b$12$bhA9jARuiDbo/CpT06t/Fe','$2b$12$bhA9jARuiDbo/CpT06t/Fe2.V9FsJ6nzX9WW6t9G45FOYpgpxZDcW'),(132,'helene.moe@gokstadakademiet.no','Helene','Moe','','','student','$2b$12$aJZ8D9dT2Cz/mPM9fqbpQe','$2b$12$aJZ8D9dT2Cz/mPM9fqbpQel1ddrNI7M1ZmPOBDXOqwT0dHfUTkqIC'),(133,'sara.eriksen2@gokstadakademiet.no','Sara','Eriksen','','','student','$2b$12$2cSCjr0b9Y5F1Wuvq7SPcu','$2b$12$2cSCjr0b9Y5F1Wuvq7SPcuNp2B7u5y/.VyFBQoagQgViYssBGTSOa'),(134,'amalie.berg@gokstadakademiet.no','Amalie','Berg','','','student','$2b$12$PG8AxL.qSmNg1w2T20kfRO','$2b$12$PG8AxL.qSmNg1w2T20kfRO4TqtVGKJLLnDITQDLik5d5dw3p8UAU.'),(135,'kristian.olsen1@gokstadakademiet.no','Kristian','Olsen','','','student','$2b$12$/4ftYvTujLjweqCh2gIuTu','$2b$12$/4ftYvTujLjweqCh2gIuTuKTxX/3/bXJIth3zZjCgfsvxsGBEzL1a'),(136,'kristian.magnussen1@gokstadakademiet.no','Kristian','Magnussen','','','student','$2b$12$GAMsUH0nq2qg3rxKHlDore','$2b$12$GAMsUH0nq2qg3rxKHlDoreOEURrbouN5a7L0ya4drCgf7dewjBeO.'),(137,'otto.johansen1@gokstadakademiet.no','Otto','Johansen','','','student','$2b$12$5nhUH0bcZNnGTxQoodWATO','$2b$12$5nhUH0bcZNnGTxQoodWATOC.vcv5U0pFAP0tdGBnWNV3QgFXNL9tm'),(138,'sara.pettersen@gokstadakademiet.no','Sara','Pettersen','','','student','$2b$12$h16vmCmRdurQKiNCc/eYA.','$2b$12$h16vmCmRdurQKiNCc/eYA.wuBtizCKyqDh/hg/V38lrvUSLPT2Eea'),(139,'aleksander.hellum1@gokstadakademiet.no','Aleksander','Hellum','','','student','$2b$12$fjQtnBEjnMhvJDvSVzIGh.','$2b$12$fjQtnBEjnMhvJDvSVzIGh.YGvPBmkDZT4u47qJELd72e6m/Rerxg.'),(140,'otto.hansen1@gokstadakademiet.no','Otto','Hansen','','','student','$2b$12$HxDzMi/0VhK2jJC6juvFru','$2b$12$HxDzMi/0VhK2jJC6juvFruJkR5MlEe76OBcemJlJ0RHETXVuSTXpS'),(141,'lars.berg@gokstadakademiet.no','Lars','Berg','','','student','$2b$12$yNkYe0orsIouFl99/RMye.','$2b$12$yNkYe0orsIouFl99/RMye.HsxhZwhTSRFczeGSwYOhhNfb7JX50QW'),(142,'jonas.pettersen1@gokstadakademiet.no','Jonas','Pettersen','','','student','$2b$12$TeO8hc4m7saOohFw/wOd.O','$2b$12$TeO8hc4m7saOohFw/wOd.Oebz/lfuOVBuuRrcGlqkBg1iSr0Rfq6a'),(143,'aleksander.magnussen1@gokstadakademiet.no','Aleksander','Magnussen','','','student','$2b$12$l9qwrERq1l026EUSRqyeye','$2b$12$l9qwrERq1l026EUSRqyeyeW9REJZxGVZ95HuswXJR5/36hCJDaAIu'),(144,'anders.olsen1@gokstadakademiet.no','Anders','Olsen','','','student','$2b$12$Ki.NP94SOUywlhYRs05O2.','$2b$12$Ki.NP94SOUywlhYRs05O2.GOnZgtgKzL0q/bpHDQriHqdkxUmmoQ2'),(145,'lars.andersen1@gokstadakademiet.no','Lars','Andersen','','','student','$2b$12$bqnz8KmjJjxxCHWvEJ1zr.','$2b$12$bqnz8KmjJjxxCHWvEJ1zr.EmCfL72R9PrfnUK1/HvtM272aP1RxT6'),(146,'ingrid.larsen@gokstadakademiet.no','Ingrid','Larsen','','','student','$2b$12$WJMrwb1Fr.vdDtRDiSxYtO','$2b$12$WJMrwb1Fr.vdDtRDiSxYtOrkaDy/2QD/1L8N8QcsBSwUHOe7z0Adq'),(147,'johannes.hansen2@gokstadakademiet.no','Johannes','Hansen','','','student','$2b$12$PbSR6dsvYAZw1A6guZg91u','$2b$12$PbSR6dsvYAZw1A6guZg91ux./nyLs/wRrP1GditakL5fFwvZ7i/m6'),(148,'sara.berg@gokstadakademiet.no','Sara','Berg','','','student','$2b$12$hUZwVAirhUSPQRyWwBoqE.','$2b$12$hUZwVAirhUSPQRyWwBoqE..u0eVMI5.P8muhFd0N2miu.06LRvSnC'),(149,'anders.moe1@gokstadakademiet.no','Anders','Moe','','','student','$2b$12$2tKeeJcpDT3AurCKHt/Amu','$2b$12$2tKeeJcpDT3AurCKHt/AmuJ6L2z5T93ErPMufm1.3KTRlpOfK5a4O'),(150,'beate.moe@gokstadakademiet.no','Beate','Moe','','','student','$2b$12$CcQuYLc9e86ak2KAwzsVvO','$2b$12$CcQuYLc9e86ak2KAwzsVvOajW3URVMM3da9NcsZc2syd10E0QKMU.'),(151,'mette.johansen@gokstadakademiet.no','Mette','Johansen','','','student','$2b$12$c61aFN85RbePpuqfipKvzu','$2b$12$c61aFN85RbePpuqfipKvzuYFEq8aUpR49tTlKM/4RakIzPaIITCf2'),(152,'nils.andersen@gokstadakademiet.no','Nils','Andersen','','','student','$2b$12$B1ndy2g6FVJdv1oNcp1ihO','$2b$12$B1ndy2g6FVJdv1oNcp1ihOXN/c2zNozVO461P3jCC6ERdKDp5S0Li'),(153,'lukas.hellum1@gokstadakademiet.no','Lukas','Hellum','','','student','$2b$12$P5QJJjnEEUpF1uidPs00NO','$2b$12$P5QJJjnEEUpF1uidPs00NOFYXwHsbhZiYgGoRexSLp74IqrhjI7oi'),(154,'kristian.andersen@gokstadakademiet.no','Kristian','Andersen','','','student','$2b$12$Wexhz0AwFXzXW/JDfTvfS.','$2b$12$Wexhz0AwFXzXW/JDfTvfS.YBzJ.hFdjzgaOKiXDeZSHA21Ep9yq5i'),(155,'mette.sørensen@gokstadakademiet.no','Mette','Sørensen','','','student','$2b$12$BAIaz0pWA7O3iAa.0e2kzO','$2b$12$BAIaz0pWA7O3iAa.0e2kzOE7.3Q7Xt7lRx9ZYHP2uZQfyzfchQe5m'),(156,'amalie.berg1@gokstadakademiet.no','Amalie','Berg','','','student','$2b$12$txZShpnhDCXFYZMFFhy3.O','$2b$12$txZShpnhDCXFYZMFFhy3.OheF1kbyYbEO3l0dwsFt.ruFwXXLMxNe'),(157,'mette.olsen@gokstadakademiet.no','Mette','Olsen','','','student','$2b$12$VEunlxtsU5ScUmBilPnh1.','$2b$12$VEunlxtsU5ScUmBilPnh1.GQCpasULY3XYkRN4pW5b/BKz8cC8X22'),(158,'kristian.larsen@gokstadakademiet.no','Kristian','Larsen','','','student','$2b$12$r8MD6hSIdtU7fBLap0vY8O','$2b$12$r8MD6hSIdtU7fBLap0vY8O1.nVrMZEQJtz3m3r0vRG6Hspb.JpUi2'),(159,'beate.hellum@gokstadakademiet.no','Beate','Hellum','','','student','$2b$12$LfcUI27nXva7nlyPTExhA.','$2b$12$LfcUI27nXva7nlyPTExhA.AICXhBUeK2riQl6iHi8udKieELTVlzq'),(160,'amalie.hansen@gokstadakademiet.no','Amalie','Hansen','','','student','$2b$12$hZQDNCOnew4NNNJhvrHA2u','$2b$12$hZQDNCOnew4NNNJhvrHA2ugiAR0RNzEwKlRDyxyrXnhH9GD4jS3Uq'),(161,'mette.hansen@gokstadakademiet.no','Mette','Hansen','','','student','$2b$12$SqplhT2UWaAyhm8rRX1eSO','$2b$12$SqplhT2UWaAyhm8rRX1eSO..Jk/ioDJ4nv48fscrTNi6dCO7OvSie'),(162,'sara.sørensen@gokstadakademiet.no','Sara','Sørensen','','','student','$2b$12$SThF06gN3HxXJb4ptRpQGO','$2b$12$SThF06gN3HxXJb4ptRpQGO99H9CpCPHz5JmeZGaVnPLTiexePWBIC'),(163,'helene.hellum@gokstadakademiet.no','Helene','Hellum','','','student','$2b$12$RH4kxPGx0KuzoVQaij9S9.','$2b$12$RH4kxPGx0KuzoVQaij9S9.3O0aKBZEcai9etiyACvNBnduMwZVnc6'),(164,'kristian.moen@gokstadakademiet.no','Kristian','Moen','','','student','$2b$12$KdxUzyuEjeHzvtBqjWpA2.','$2b$12$KdxUzyuEjeHzvtBqjWpA2.3dOGNCfmTe5HPZSCcwfXuksQBbBm6c6'),(165,'nora.berg@gokstadakademiet.no','Nora','Berg','','','student','$2b$12$y5bPtT.IseW71GWGUfhAf.','$2b$12$y5bPtT.IseW71GWGUfhAf.ksi9.6zvJqgYO5qvF.EnppoUCMcfiai'),(166,'sara.olsen@gokstadakademiet.no','Sara','Olsen','','','student','$2b$12$n/6xdqyHOiahxGiI2M7Hhu','$2b$12$n/6xdqyHOiahxGiI2M7HhuhLH2QJWF2kVYddHB8EZye3M0q3GBi/.'),(167,'anders.johansen@gokstadakademiet.no','Anders','Johansen','','','student','$2b$12$r.UyFIEz7dmikstaw11ABe','$2b$12$r.UyFIEz7dmikstaw11ABeDlsM54CXS12TJibHhTz99svjzNvxMdW'),(168,'amalie.johansen@gokstadakademiet.no','Amalie','Johansen','','','student','$2b$12$tOF8mMqMpva3RWeXBb.vbO','$2b$12$tOF8mMqMpva3RWeXBb.vbOc6ObVyZbwetdp2.XLi5ho67rWKyB7xi'),(169,'sara.johansen1@gokstadakademiet.no','Sara','Johansen','','','student','$2b$12$558BBtRyzlTPDCFqfblZPu','$2b$12$558BBtRyzlTPDCFqfblZPu90R2KZa5FzPIklVd24e9KRb/K9CffD6'),(170,'beate.johansen@gokstadakademiet.no','Beate','Johansen','','','student','$2b$12$IAeTHkc7Z2ZGQYwEousd9O','$2b$12$IAeTHkc7Z2ZGQYwEousd9ORcJ.LSAp/YJ56x8mAA3325WBlCgNKfC'),(171,'lars.berg1@gokstadakademiet.no','Lars','Berg','','','student','$2b$12$HOjUkH51UTMfGjpnNzaVN.','$2b$12$HOjUkH51UTMfGjpnNzaVN.1uEafkLi3OuJ9jl38w.Sacr9VUkEJsy'),(172,'kristian.pedersen@gokstadakademiet.no','Kristian','Pedersen','','','student','$2b$12$2plieYa.zYGv1..nnSsmxO','$2b$12$2plieYa.zYGv1..nnSsmxOHnExoifDFgNcqP0834clT4AJPE9TQgq'),(173,'anders.pettersen@gokstadakademiet.no','Anders','Pettersen','','','student','$2b$12$k91VJv3dz43CZi5gA1/ofe','$2b$12$k91VJv3dz43CZi5gA1/ofeYaQ1QlE89KFayTxM3f5ktjHu8myPLLm'),(174,'otto.larsen@gokstadakademiet.no','Otto','Larsen','','','student','$2b$12$2uEuKH5Hu26PXj.MgOG4n.','$2b$12$2uEuKH5Hu26PXj.MgOG4n.isJ1m5Mbilandam2jW/oBFad/WKljP6'),(175,'amalie.moen1@gokstadakademiet.no','Amalie','Moen','','','student','$2b$12$tc.C4FUYyWID0.t.RbVvOe','$2b$12$tc.C4FUYyWID0.t.RbVvOexSIXEsTzqbQDa6HC07qVfquPLvWhYgW'),(176,'markus.pedersen@gokstadakademiet.no','Markus','Pedersen','','','student','$2b$12$SCle.i2wS.ySgaoxXr5LS.','$2b$12$SCle.i2wS.ySgaoxXr5LS.swch3nu9BvukWLq.cpcfzhdWWTt3dXy'),(177,'mette.hellum@gokstadakademiet.no','Mette','Hellum','','','student','$2b$12$U3AOeVJungP.PwldoZOJAe','$2b$12$U3AOeVJungP.PwldoZOJAeiJm4CQ.KtPmE8oRMdG9LDagrCtliqma'),(178,'kamilla.pedersen@gokstadakademiet.no','Kamilla','Pedersen','','','student','$2b$12$bl2H85Q5INdcooXZ9kD3Ve','$2b$12$bl2H85Q5INdcooXZ9kD3VeedRPKw0MU8CQSa24hQzEMOhbSpzTaju'),(179,'sara.larsen1@gokstadakademiet.no','Sara','Larsen','','','student','$2b$12$wE2EOGZkX8vKjC8R/UaUe.','$2b$12$wE2EOGZkX8vKjC8R/UaUe.x.yYd1ThDRVXSGG4y9T8wUDtBOpX08S'),(180,'nils.johansen2@gokstadakademiet.no','Nils','Johansen','','','student','$2b$12$N16vE4RGIkGxKrgacK953O','$2b$12$N16vE4RGIkGxKrgacK953OtpXeMMm96gFuOgWTwRF7A7djxhClDE2'),(181,'sara.pedersen@gokstadakademiet.no','Sara','Pedersen','','','student','$2b$12$3nRO5hK5pV5cuRxjE/T4ie','$2b$12$3nRO5hK5pV5cuRxjE/T4ieI6lbkK4KCi76X.b9ivtHgyx8jSDyNMW'),(182,'johannes.moen@gokstadakademiet.no','Johannes','Moen','','','student','$2b$12$YsgnRkQwe6x9vSf.K7OIJe','$2b$12$YsgnRkQwe6x9vSf.K7OIJeaQull9r.JG2sZGtIAMMs1YqeBx6EcPW'),(183,'johannes.berg@gokstadakademiet.no','Johannes','Berg','','','student','$2b$12$RGs1WWemzec6cqVToFIyxO','$2b$12$RGs1WWemzec6cqVToFIyxOKrOsRAE0PP/Qnom0Oj13LMK2C9zpgr.'),(184,'nora.andersen1@gokstadakademiet.no','Nora','Andersen','','','student','$2b$12$c9vz9090KKxiMZQBJ8H0ru','$2b$12$c9vz9090KKxiMZQBJ8H0ru.yEf01h5fFID37O/np763.YLzTHb.8a'),(185,'lukas.pettersen@gokstadakademiet.no','Lukas','Pettersen','','','student','$2b$12$e0UQ8UedKDQGBDTgzBdULe','$2b$12$e0UQ8UedKDQGBDTgzBdULeUdveiZV9vK9tKFBm8rFKYidNDg3NcOm'),(186,'lars.pettersen1@gokstadakademiet.no','Lars','Pettersen','','','student','$2b$12$5/ukivRyNj8nqG3hZdRNsu','$2b$12$5/ukivRyNj8nqG3hZdRNsudvvNV6fXAn./6/gMm.aL5hXSkpeLe0i'),(187,'otto.andersen@gokstadakademiet.no','Otto','Andersen','','','student','$2b$12$kOF1DUKczwrcbuFGQ8eKue','$2b$12$kOF1DUKczwrcbuFGQ8eKueoiWa3ztdokgjupXPmrS47P0HAh9zL1e'),(188,'sara.pedersen1@gokstadakademiet.no','Sara','Pedersen','','','student','$2b$12$dgN.Y3af2D5IzMmctcgEGu','$2b$12$dgN.Y3af2D5IzMmctcgEGuG07u.Uapks/5raNYSxBsFR0/t8Le4/O'),(189,'helene.olsen@gokstadakademiet.no','Helene','Olsen','','','student','$2b$12$StkxfuK/7Bvn2b9NmGdgcu','$2b$12$StkxfuK/7Bvn2b9NmGdgcujqXX2V7m6pp4LtFw8/MaFHZ4V1fdY16'),(190,'mari.magnussen@gokstadakademiet.no','Mari','Magnussen','','','student','$2b$12$55uruudpeN3.lkvh5QLjnu','$2b$12$55uruudpeN3.lkvh5QLjnuzmOWi1uePK1tLHqYwjy08ROw7ZQNEby'),(191,'anders.hansen@gokstadakademiet.no','Anders','Hansen','','','student','$2b$12$nfR82dZGoYeSrO.XHyjdMe','$2b$12$nfR82dZGoYeSrO.XHyjdMegmtjTeJhPUFite9KyI/CoDT/spCj3da'),(192,'beate.pettersen1@gokstadakademiet.no','Beate','Pettersen','','','student','$2b$12$NUN1G/.R6qPBQwjZ8LesNe','$2b$12$NUN1G/.R6qPBQwjZ8LesNeZ4uMCuendb.Ek9CsdFDVGMwPhji1WJ6'),(193,'ole.johansen1@gokstadakademiet.no','Ole','Johansen','','','student','$2b$12$NnqCM8AYQZnuergf1wW0GO','$2b$12$NnqCM8AYQZnuergf1wW0GOPLEQOiMJZrPHK3/zaMLp2RVLcTsa59i'),(194,'ingrid.hellum@gokstadakademiet.no','Ingrid','Hellum','','','student','$2b$12$4k93KIrps/iZfFT3SR.adO','$2b$12$4k93KIrps/iZfFT3SR.adOrdDbLc5lCEPXh9QWoT5jeiSEZfoMYN.'),(195,'amalie.pettersen@gokstadakademiet.no','Amalie','Pettersen','','','student','$2b$12$KacVEhR7/6yOekcH0sxiT.','$2b$12$KacVEhR7/6yOekcH0sxiT.6Wv42vIK.3hnChYggF35L.KqF64rGse'),(196,'nils.olsen@gokstadakademiet.no','Nils','Olsen','','','student','$2b$12$PxATTCNxq.DhcsHwV5Bpm.','$2b$12$PxATTCNxq.DhcsHwV5Bpm.3PqoPtq1ePo7eCUGl.hUBPF1669mNMG'),(197,'johannes.berg1@gokstadakademiet.no','Johannes','Berg','','','student','$2b$12$Bmu43yJcKQ/gTM7fQ8Vgn.','$2b$12$Bmu43yJcKQ/gTM7fQ8Vgn.25k4xJqlossjGDxYkwmlWUXF7NI9iG.'),(198,'ella.larsen@gokstadakademiet.no','Ella','Larsen','','','student','$2b$12$6/0oBNJkQd2.GuP/UMkdKe','$2b$12$6/0oBNJkQd2.GuP/UMkdKesbqwcLA/.VSulebxSFSyo.WN9Lb7DNO'),(199,'ole.hansen2@gokstadakademiet.no','Ole','Hansen','','','student','$2b$12$GdehGqCXkzlV2NGomyxagO','$2b$12$GdehGqCXkzlV2NGomyxagO.AO.QB5YdCp0qDxOx5QyNGgwsGnz8yi'),(200,'jonas.magnussen@gokstadakademiet.no','Jonas','Magnussen','','','student','$2b$12$gVXnrKCLiuYUp1q/LunoGu','$2b$12$gVXnrKCLiuYUp1q/LunoGu1YlhJPmCjaF470EHyyChCduS.R1NCzK'),(201,'ingrid.eriksen@gokstadakademiet.no','Ingrid','Eriksen','','','student','$2b$12$x6OLkPKudsgOv.zKkSBKIO','$2b$12$x6OLkPKudsgOv.zKkSBKIOAhiq7sotKc9/2Ww0WnPbdB73Qz8LzHS'),(202,'mette.berg@gokstadakademiet.no','Mette','Berg','','','student','$2b$12$BORV.URaEznGMyGFD6Y1Du','$2b$12$BORV.URaEznGMyGFD6Y1Du9ttLUSA386sIJZuAOiZ7GGqhKJvbYja'),(203,'kristian.sørensen@gokstadakademiet.no','Kristian','Sørensen','','','student','$2b$12$FYiehqfjo6bNziZ5nSXjIO','$2b$12$FYiehqfjo6bNziZ5nSXjIOM2uS1dt/Kz19VIOr1ZZ1O5cM/NapBZe'),(204,'sara.pettersen1@gokstadakademiet.no','Sara','Pettersen','','','student','$2b$12$BjGCJedTOEbyZ300wuMw3u','$2b$12$BjGCJedTOEbyZ300wuMw3uiR5HS5fHneagkTisn3JdQsRJrmD/8XO'),(205,'mari.moen1@gokstadakademiet.no','Mari','Moen','','','student','$2b$12$uuzmgRMImIOrHGzl6a1CUe','$2b$12$uuzmgRMImIOrHGzl6a1CUew8lOoNIHGCV5pbvQamvuy82oEnL7BWW'),(206,'mette.magnussen@gokstadakademiet.no','Mette','Magnussen','','','student','$2b$12$1h6glTu7vcCTnLAvfWfRDO','$2b$12$1h6glTu7vcCTnLAvfWfRDOir01GI7EClk7oUUeGwy2Tkq.i102Snm'),(207,'amalie.sørensen1@gokstadakademiet.no','Amalie','Sørensen','','','student','$2b$12$J7vLofmB2fl/IeeDNfKvgO','$2b$12$J7vLofmB2fl/IeeDNfKvgO4t7HVJkKG3iog0HtA5XGtSs9yRYnM96'),(208,'johannes.hellum1@gokstadakademiet.no','Johannes','Hellum','','','student','$2b$12$z6IkHVfvcdG98zBOIpTz4u','$2b$12$z6IkHVfvcdG98zBOIpTz4u3Y0UEfLdvt1fdij3Ofsq3s8oyFyg..q'),(209,'ole.hellum@gokstadakademiet.no','Ole','Hellum','','','student','$2b$12$21QCWRu2UD60apGoR5np0.','$2b$12$21QCWRu2UD60apGoR5np0.iIn2xWVJZoJOZRRgSpzVRAxbF1cURP2'),(210,'jonas.magnussen1@gokstadakademiet.no','Jonas','Magnussen','','','student','$2b$12$AgX..9LctYOD024a2BSM5e','$2b$12$AgX..9LctYOD024a2BSM5e30Hhazh6VQ6APx2zYe1u9BjoIVBqqWC'),(211,'helene.eriksen1@gokstadakademiet.no','Helene','Eriksen','','','student','$2b$12$.8lrhJWcMVk13CuUxZXbxO','$2b$12$.8lrhJWcMVk13CuUxZXbxOO75TxNgH0kef29fF.3qtbr7OeOkwiEm'),(212,'aleksander.moe@gokstadakademiet.no','Aleksander','Moe','','','student','$2b$12$xIY3FiWs0t2Fb8LavzKwSe','$2b$12$xIY3FiWs0t2Fb8LavzKwSeCDNFKtbAH3b4h9CRWKxX1P0tY4TylUS'),(213,'amalie.hellum@gokstadakademiet.no','Amalie','Hellum','','','student','$2b$12$EKqnowKNGUkRVYndOGJlq.','$2b$12$EKqnowKNGUkRVYndOGJlq.RMGAZLm1kxao2IDsiRSoWkLVzEuArtW'),(214,'ingrid.larsen1@gokstadakademiet.no','Ingrid','Larsen','','','student','$2b$12$r.I9h569yKWOenAWTIWaaO','$2b$12$r.I9h569yKWOenAWTIWaaOqHcQ3LL8hHxyiC1BMsUvHBaVj85q9Gm'),(215,'nora.eriksen1@gokstadakademiet.no','Nora','Eriksen','','','student','$2b$12$B05GCEFWqX6UwnJJYrTW8O','$2b$12$B05GCEFWqX6UwnJJYrTW8O4s.RiriDqmACVm0ss9sihIgriuLyhoS'),(216,'beate.andersen@gokstadakademiet.no','Beate','Andersen','','','student','$2b$12$agRhcKOIOXBm6mtG77PIiu','$2b$12$agRhcKOIOXBm6mtG77PIiuA/qs.0I9ak6N.vIwYxigr2USN6CP7Tm'),(217,'anders.moe2@gokstadakademiet.no','Anders','Moe','','','student','$2b$12$qUijESHmz/G04kpE21ru3O','$2b$12$qUijESHmz/G04kpE21ru3OVQ./aF4TuDHPdcHioU.bPYIZXEGo3Aa'),(218,'beate.eriksen@gokstadakademiet.no','Beate','Eriksen','','','student','$2b$12$w38tCzJqiNXweYJo7ei8g.','$2b$12$w38tCzJqiNXweYJo7ei8g.18iJKZH3An3v7h13Tt.2tRvsIzkorcC'),(219,'otto.olsen1@gokstadakademiet.no','Otto','Olsen','','','student','$2b$12$WnFh3Br29gp2H9xowTCuRu','$2b$12$WnFh3Br29gp2H9xowTCuRuFcJ4PUPMPeFFoykjls7iLwuOPMi4d1q'),(220,'lars.berg2@gokstadakademiet.no','Lars','Berg','','','student','$2b$12$QF7xFyWuAZoM0mBWKvrWz.','$2b$12$QF7xFyWuAZoM0mBWKvrWz.z4Vgz7UOlJ1N/M6netrMxfIRhWhh70y'),(221,'kristian.pettersen1@gokstadakademiet.no','Kristian','Pettersen','','','student','$2b$12$44HPNMRm5gycQ36i3kRfNe','$2b$12$44HPNMRm5gycQ36i3kRfNey.FN1XzuDNmW9r4c5ml0yTHSU7SckAG'),(222,'mette.berg1@gokstadakademiet.no','Mette','Berg','','','student','$2b$12$pylrgJC22i/nkoRUTYLJju','$2b$12$pylrgJC22i/nkoRUTYLJjuGug8r6OxGbQnMQB6swZZc9W1C5HaKG2'),(223,'helene.pettersen@gokstadakademiet.no','Helene','Pettersen','','','student','$2b$12$G//hmFAMrathbA3TC6YoAe','$2b$12$G//hmFAMrathbA3TC6YoAe9IbrbV0PtSvRMm.c2mqAOgfcgqkfpdG'),(224,'aleksander.moe1@gokstadakademiet.no','Aleksander','Moe','','','student','$2b$12$tfZmRkv4Yh6tZlAg7H/Xn.','$2b$12$tfZmRkv4Yh6tZlAg7H/Xn.2fZBO0nOgTWGWGeBG6ifUB8YpII20jW'),(225,'sara.sørensen1@gokstadakademiet.no','Sara','Sørensen','','','student','$2b$12$uH2AyKAMq9headILbjMQtu','$2b$12$uH2AyKAMq9headILbjMQtuvDlyJjFO2cTLK9wlBocpfA0DDSkaz4i'),(226,'nora.johansen1@gokstadakademiet.no','Nora','Johansen','','','student','$2b$12$zacJWMKu70m9Z/lq9Oh3cO','$2b$12$zacJWMKu70m9Z/lq9Oh3cOR4hT9dgYvD793U/9I/mQka35YkU4kH6'),(227,'helene.pettersen1@gokstadakademiet.no','Helene','Pettersen','','','student','$2b$12$9ENuX4FuT8ltowLYH9U7o.','$2b$12$9ENuX4FuT8ltowLYH9U7o.DpWZC6Oz5BOPg9fxeYQS7wdVd/PEnfi'),(228,'ole.larsen1@gokstadakademiet.no','Ole','Larsen','','','student','$2b$12$jzbKvRAmtsfOwQcTMu0tq.','$2b$12$jzbKvRAmtsfOwQcTMu0tq.lh4ebKhCQ9tlAWBy7KYJ2lum64jkdZW'),(229,'aleksander.johansen@gokstadakademiet.no','Aleksander','Johansen','','','student','$2b$12$Xvy/Aze4w1gIMd0KVTRgQ.','$2b$12$Xvy/Aze4w1gIMd0KVTRgQ.96qIObYxAN8qsKvLckpr36Bg5aKHn9e'),(230,'helene.andersen1@gokstadakademiet.no','Helene','Andersen','','','student','$2b$12$fZzkgJPbBuXf.XkHIhLL4e','$2b$12$fZzkgJPbBuXf.XkHIhLL4eUIdON6x285/HvWfw8JJVcZQjFOZMRpW'),(231,'lars.andreassen@gokstadakademiet.no','Lars','Andreassen','','','student','$2b$12$Bkk62YL5d7WDhjahgQwgvO','$2b$12$Bkk62YL5d7WDhjahgQwgvObs.R/N.2weyNUm3ovg7fr56589wu7aC'),(232,'kristian.hellum@gokstadakademiet.no','Kristian','Hellum','','','student','$2b$12$Gd2IL2kfV79wVHYBGyma1.','$2b$12$Gd2IL2kfV79wVHYBGyma1.P3YSfhvWWwJPrh8egL423i/c.vzBgmC'),(233,'lars.pettersen2@gokstadakademiet.no','Lars','Pettersen','','','student','$2b$12$5baggF9OR/YnAYyidMMjEu','$2b$12$5baggF9OR/YnAYyidMMjEuobV8dk6owSIzffASp2Co/emxpNgLJ9O'),(234,'sara.sørensen2@gokstadakademiet.no','Sara','Sørensen','','','student','$2b$12$zqdoJWsK3uSgnMIVRk78iu','$2b$12$zqdoJWsK3uSgnMIVRk78iuWS7WFRGIjBrO3pJ9HEUElGMWZ.EUcoa'),(235,'amalie.eriksen1@gokstadakademiet.no','Amalie','Eriksen','','','student','$2b$12$eGkKxzmQ6hJedB8gykXEHu','$2b$12$eGkKxzmQ6hJedB8gykXEHurCCOoYdRUv3E1vpx54pPGYYE6xfcN8K'),(236,'sara.hansen@gokstadakademiet.no','Sara','Hansen','','','student','$2b$12$riRyU.Bstd0dezV09V3Px.','$2b$12$riRyU.Bstd0dezV09V3Px.TZ/CGV3dS/lXBY3RmyYdodkXn5xJ.sa'),(237,'lukas.moen@gokstadakademiet.no','Lukas','Moen','','','student','$2b$12$i2XOj0I4SbXAjnBrm44U3.','$2b$12$i2XOj0I4SbXAjnBrm44U3.cUPCei5so2eHX8oo1czIMjn3K3cVlOK'),(238,'mette.larsen@gokstadakademiet.no','Mette','Larsen','','','student','$2b$12$auI6jIYLCQj175EL/vs2mu','$2b$12$auI6jIYLCQj175EL/vs2mu6TmPzEfmGeZsjv6/caWQrmE9vlszuu.'),(239,'ingrid.pedersen@gokstadakademiet.no','Ingrid','Pedersen','','','student','$2b$12$4.vB2ZIjjwIQ2tC3Wz2Doe','$2b$12$4.vB2ZIjjwIQ2tC3Wz2DoejiRGw5H2EnWR01LHE3VszS0ge6FAQKm'),(240,'ingrid.andersen@gokstadakademiet.no','Ingrid','Andersen','','','student','$2b$12$n5MiCTuf4PvdtsmMAkGNS.','$2b$12$n5MiCTuf4PvdtsmMAkGNS.mO5ngT2ygBJl3.sO5ASnGkRDW2jFDQC'),(241,'jonas.andreassen@gokstadakademiet.no','Jonas','Andreassen','','','student','$2b$12$YBo9u.wl4cynQWodAGzHku','$2b$12$YBo9u.wl4cynQWodAGzHkuQH3QfPiB8XVqosthOhOMUpCKSUHVAW.'),(242,'otto.eriksen@gokstadakademiet.no','Otto','Eriksen','','','student','$2b$12$AF3CIwlc.G4NJKbFObH81O','$2b$12$AF3CIwlc.G4NJKbFObH81OjXP7KUPduIngGe.EBSoXx1toCpcGrfO'),(243,'helene.sørensen@gokstadakademiet.no','Helene','Sørensen','','','student','$2b$12$4nwraZFPZ0ZdOyuBcL0Mh.','$2b$12$4nwraZFPZ0ZdOyuBcL0Mh.QGt21cKSRS.HWmFHSn0GbnWCG4QPcXW'),(244,'kamilla.moen@gokstadakademiet.no','Kamilla','Moen','','','student','$2b$12$bcauGSc0yFM5wXnspGu7Yu','$2b$12$bcauGSc0yFM5wXnspGu7Yus8CtUPVKbNoFzI5u0D9VqevYPMO.8WG'),(245,'amalie.eriksen2@gokstadakademiet.no','Amalie','Eriksen','','','student','$2b$12$wqochN4uJpvhNM9goIN15O','$2b$12$wqochN4uJpvhNM9goIN15OrXzwLHrawIXyZ1lsj4VJZsQznl.cpzK'),(246,'mette.hellum1@gokstadakademiet.no','Mette','Hellum','','','student','$2b$12$N7HbM8tkaBfdguS0XaFxG.','$2b$12$N7HbM8tkaBfdguS0XaFxG.OCHbFQA6SUuUq0s2Pr3zesHJnGwvqfm'),(247,'nora.pettersen@gokstadakademiet.no','Nora','Pettersen','','','student','$2b$12$vKRY3KG3UsJKt6bJdjqC/u','$2b$12$vKRY3KG3UsJKt6bJdjqC/upuWgZzoObW7rSZCj6M/hQRIraCxUwwK'),(248,'ole.andersen@gokstadakademiet.no','Ole','Andersen','','','student','$2b$12$UPHQyu8jw1L2buP2F1A.VO','$2b$12$UPHQyu8jw1L2buP2F1A.VOSBh/5KYEeH5CpwS6XTG4rJiZSQTLbEW'),(249,'sara.sørensen3@gokstadakademiet.no','Sara','Sørensen','','','student','$2b$12$uDIDZkiWw47f0pZZHe.dB.','$2b$12$uDIDZkiWw47f0pZZHe.dB.89oY26X2pdGPojBJ8.LATNAQDlz/cRu'),(250,'ingrid.hellum1@gokstadakademiet.no','Ingrid','Hellum','','','student','$2b$12$x7q32Dxt4a2BbYNbdMURT.','$2b$12$x7q32Dxt4a2BbYNbdMURT.zETznwh7kh2HX8bgiCSSEbyeBYBzr.G'),(251,'kamilla.pettersen@gokstadakademiet.no','Kamilla','Pettersen','','','student','$2b$12$iyheP3z2WrXxaheWi65f2O','$2b$12$iyheP3z2WrXxaheWi65f2O/mkpOrUPWzBEwzNOm02wZ8gYVnOU/9e'),(252,'ella.pettersen@gokstadakademiet.no','Ella','Pettersen','','','student','$2b$12$y3DEtBZqyuYT9.GpwzQaQu','$2b$12$y3DEtBZqyuYT9.GpwzQaQu5QejtszsaS3q7zBOilMC3948i32l4ja'),(253,'markus.magnussen@gokstadakademiet.no','Markus','Magnussen','','','student','$2b$12$thbSa9Bk3dsGUaTat8AxKO','$2b$12$thbSa9Bk3dsGUaTat8AxKOEgqsJb4OPrS33SxI6A3b8.RjpwPnqyq'),(254,'otto.olsen2@gokstadakademiet.no','Otto','Olsen','','','student','$2b$12$mB179qWWdZX065RavBGxKO','$2b$12$mB179qWWdZX065RavBGxKOuM6wBjOjuiKE10K18a4QcUOws5INzpS'),(255,'beate.moen@gokstadakademiet.no','Beate','Moen','','','student','$2b$12$/Rz71IcX3fLjuYJTHQBY7O','$2b$12$/Rz71IcX3fLjuYJTHQBY7O5fQrX5LhkzLyV1K8rFMPhXuxYRsfWaS'),(256,'mari.hellum@gokstadakademiet.no','Mari','Hellum','','','student','$2b$12$H2BBpXmbvnRDT3QSpGYw6e','$2b$12$H2BBpXmbvnRDT3QSpGYw6e0aQ7HnwfO/lFd9.3aN1/FOHl4FgrsOC'),(257,'nils.moe@gokstadakademiet.no','Nils','Moe','','','student','$2b$12$L2Q4JEASFg260xSN/r1xe.','$2b$12$L2Q4JEASFg260xSN/r1xe.H6u.OOpLEJIjr0rgh1X7MEIapE/Ftae'),(258,'ole.olsen@gokstadakademiet.no','Ole','Olsen','','','student','$2b$12$ww988yMXrOvUiv1keOiIk.','$2b$12$ww988yMXrOvUiv1keOiIk.XTLakh34YxTuNlwW2hD0yEbdBYy3JDW'),(259,'johannes.andreassen1@gokstadakademiet.no','Johannes','Andreassen','','','student','$2b$12$p6jS65FhtX3o0P2xNhFjb.','$2b$12$p6jS65FhtX3o0P2xNhFjb.1QPeaw7ttwODPEOfkYeL/6vUw3IeXgy'),(260,'lars.moe@gokstadakademiet.no','Lars','Moe','','','student','$2b$12$pXxNLrpu.t6lSOHb7QIPRe','$2b$12$pXxNLrpu.t6lSOHb7QIPRenUYLs6g4LHWEziPhr3V4TzZuezqxrvG'),(261,'kristian.larsen1@gokstadakademiet.no','Kristian','Larsen','','','student','$2b$12$q36OgdFNpyv1DSTQ6xKuZu','$2b$12$q36OgdFNpyv1DSTQ6xKuZuvoJ9QodsMPsOZ6UAh1kww2OHvKi3AN.'),(262,'nora.magnussen1@gokstadakademiet.no','Nora','Magnussen','','','student','$2b$12$5SIRuwtxQiJ.MFe1XpQkBe','$2b$12$5SIRuwtxQiJ.MFe1XpQkBeRaiZKVha3lRpgNB0LduFEk0aAoc17HK'),(263,'mari.andersen@gokstadakademiet.no','Mari','Andersen','','','student','$2b$12$jQbbewgin9HcyK6mWoMyx.','$2b$12$jQbbewgin9HcyK6mWoMyx.K270Elunjq4lE14qEK3fqrOXF4iy/U6'),(264,'jonas.andreassen1@gokstadakademiet.no','Jonas','Andreassen','','','student','$2b$12$NG7mawbOa1tHsc7Ch7nIeu','$2b$12$NG7mawbOa1tHsc7Ch7nIeuqz2OZjTIVekxjSTco/0uAZr1q1QMT1m'),(265,'markus.larsen1@gokstadakademiet.no','Markus','Larsen','','','student','$2b$12$6EIDqlwpxm0DPKuZRnQ5Cu','$2b$12$6EIDqlwpxm0DPKuZRnQ5Cu4LaqGi65pouvwADi7vvb.FMp8/zJZ7m'),(266,'helene.hansen1@gokstadakademiet.no','Helene','Hansen','','','student','$2b$12$IRSpjroM4lLJZhWk75xQX.','$2b$12$IRSpjroM4lLJZhWk75xQX.o1zddlAB5TxL8Q8JVFjsnG51dDopKHO'),(267,'mari.olsen@gokstadakademiet.no','Mari','Olsen','','','student','$2b$12$Rvs1QBnXpuKBc5i2wcYK0O','$2b$12$Rvs1QBnXpuKBc5i2wcYK0OeYvIU76QaKaVv7G1kUKQyMtNCrunT6m'),(268,'lukas.andreassen@gokstadakademiet.no','Lukas','Andreassen','','','student','$2b$12$2BfTkFES4sBRkpVCQ7CwYe','$2b$12$2BfTkFES4sBRkpVCQ7CwYeGaGN5o6u/scPlIpLEAxCoj5sk7FNaiC'),(269,'kamilla.pedersen1@gokstadakademiet.no','Kamilla','Pedersen','','','student','$2b$12$20V4.bW1EW/DM8YUqppkve','$2b$12$20V4.bW1EW/DM8YUqppkvepK75jJRv5tngIX11k7YtcyDIlvYKrvW'),(270,'otto.johansen2@gokstadakademiet.no','Otto','Johansen','','','student','$2b$12$cGbfNkp.CSpU52JMuT4DB.','$2b$12$cGbfNkp.CSpU52JMuT4DB.1QQIC0oiQrfSsLS.L7AahuINuKgssT6'),(271,'ingrid.hansen@gokstadakademiet.no','Ingrid','Hansen','','','student','$2b$12$HK6gsCQfNlSYnIyTTLu4RO','$2b$12$HK6gsCQfNlSYnIyTTLu4ROV9/iFrOW40YlJbEC5tQ3HjxDzyqX8bO'),(272,'lukas.andersen1@gokstadakademiet.no','Lukas','Andersen','','','student','$2b$12$YR4CnI5ZMBF4NjkPQnOJP.','$2b$12$YR4CnI5ZMBF4NjkPQnOJP.cWNWbEhXIy/aX1AhJGlZvcB.5cvpPQq'),(273,'mari.andreassen@gokstadakademiet.no','Mari','Andreassen','','','student','$2b$12$uqFrP/75JWGZOz2a3R5wGe','$2b$12$uqFrP/75JWGZOz2a3R5wGeaYXPHj11BPks39SUuD2/55sCT05GVey'),(274,'helene.sørensen1@gokstadakademiet.no','Helene','Sørensen','','','student','$2b$12$oAeJDZI0MPNw3I4SWoyoSe','$2b$12$oAeJDZI0MPNw3I4SWoyoSeSsmbL8nRByPZRNiX2svxZ5/erEr6ykS'),(275,'mari.sørensen@gokstadakademiet.no','Mari','Sørensen','','','student','$2b$12$2dEebyi44ljRfME0SsY6Pu','$2b$12$2dEebyi44ljRfME0SsY6PupP/Hhml9S4vEShkXh3zHBm40tBaigIu'),(276,'nora.moe1@gokstadakademiet.no','Nora','Moe','','','student','$2b$12$7d0JIgXVS5pSrdGABsP5Ve','$2b$12$7d0JIgXVS5pSrdGABsP5VejLIZwPlNe17DLBM.UWi9X/z4C4VACoq'),(277,'lukas.hellum2@gokstadakademiet.no','Lukas','Hellum','','','student','$2b$12$VUPRi/codlxvkqJxBbmh8.','$2b$12$VUPRi/codlxvkqJxBbmh8.rpaI1FOLpUIqfVcslBw4lTEI9mF4H7S'),(278,'sara.berg1@gokstadakademiet.no','Sara','Berg','','','student','$2b$12$tL5RRkMncgP1Gi1M82IdXu','$2b$12$tL5RRkMncgP1Gi1M82IdXuF62MB9xK2wQkvG5NcM7hewwIBop/.vW'),(279,'jonas.andreassen2@gokstadakademiet.no','Jonas','Andreassen','','','student','$2b$12$SaWodCPfRLpfvhYNrs7Hk.','$2b$12$SaWodCPfRLpfvhYNrs7Hk.5QlouoF8c5y52.8VL1kCj2.vsh8kltW'),(280,'aleksander.olsen1@gokstadakademiet.no','Aleksander','Olsen','','','student','$2b$12$6rV7etzo3/9XGLL52LtpK.','$2b$12$6rV7etzo3/9XGLL52LtpK.EQFUJ3LyzNkctJECutefx9.ThPBgY4C'),(281,'lars.andersen2@gokstadakademiet.no','Lars','Andersen','','','student','$2b$12$AgryT0dj6UcgjVMaAkB0te','$2b$12$AgryT0dj6UcgjVMaAkB0teVx6yZBhPFvECIzn6aDkH9v4UTxHo0iK'),(282,'mette.eriksen1@gokstadakademiet.no','Mette','Eriksen','','','student','$2b$12$x7Ghco5i9K8WvTdOgQex4e','$2b$12$x7Ghco5i9K8WvTdOgQex4emmtcTrEh7x/uY76VinQlTsQ0Oe.eYpu'),(283,'mette.pedersen@gokstadakademiet.no','Mette','Pedersen','','','student','$2b$12$Crk9ctrTwFVL4IK4jq5Gf.','$2b$12$Crk9ctrTwFVL4IK4jq5Gf.cYr8mKvmzeLeMT/U9Qd1KTvnQSIRQRy'),(284,'lars.olsen@gokstadakademiet.no','Lars','Olsen','','','student','$2b$12$6yect9ijMNcTGUQvijTjT.','$2b$12$6yect9ijMNcTGUQvijTjT.8SoT.PxuB3YFlinxK5gsakAuNNQpt0C'),(285,'helene.hansen2@gokstadakademiet.no','Helene','Hansen','','','student','$2b$12$fbM8Ncsha2.8LQ8yPI6RIe','$2b$12$fbM8Ncsha2.8LQ8yPI6RIeW9GJqq5aZEc0LhPpxCH2ecAJPJx/jCy'),(286,'helene.eriksen2@gokstadakademiet.no','Helene','Eriksen','','','student','$2b$12$aIQBPOlCMWtiqkjNLcJM.O','$2b$12$aIQBPOlCMWtiqkjNLcJM.O6T6Jejcxd2WeM59gUEHh10ol0PvQGmS'),(287,'jonas.pedersen@gokstadakademiet.no','Jonas','Pedersen','','','student','$2b$12$Fdvj95X/NbdWMIh1x29sTe','$2b$12$Fdvj95X/NbdWMIh1x29sTeljvu1moEiK6MADCAwIUIFWSX.eXvt3q'),(288,'amalie.hansen1@gokstadakademiet.no','Amalie','Hansen','','','student','$2b$12$k3f3qLriPdSgZNN6yV4o6.','$2b$12$k3f3qLriPdSgZNN6yV4o6.v1W8jK5bIB2tciEHT7Jo/d6kD9yoSjG'),(289,'helene.moe1@gokstadakademiet.no','Helene','Moe','','','student','$2b$12$/nAUfxkssrCMK8EUDFcmqe','$2b$12$/nAUfxkssrCMK8EUDFcmqex2hpgJ0dLeU.5Ge2xyXJxDYn5sfc3YK'),(290,'aleksander.magnussen2@gokstadakademiet.no','Aleksander','Magnussen','','','student','$2b$12$3P2N2iV9rHxV3NHba5OzEu','$2b$12$3P2N2iV9rHxV3NHba5OzEuqmlWSuCZZd2L471tvhH670.URq3.ugm'),(291,'sara.eriksen3@gokstadakademiet.no','Sara','Eriksen','','','student','$2b$12$/2BnvQ0c1tWC4rEKZUnag.','$2b$12$/2BnvQ0c1tWC4rEKZUnag.6yG3WrIDwM5LkfODBcwzmBaO.tpzzVa'),(292,'kristian.hellum1@gokstadakademiet.no','Kristian','Hellum','','','student','$2b$12$aOxDFkDgrJ.WoqwCiJ0AE.','$2b$12$aOxDFkDgrJ.WoqwCiJ0AE.J8WpiteMZC/3zlHTBRcpeZbg0XOADxK'),(293,'lars.hellum1@gokstadakademiet.no','Lars','Hellum','','','student','$2b$12$Xr7Or0fEewQyZ1TjxqExve','$2b$12$Xr7Or0fEewQyZ1TjxqExven9K5RnxrkjnwUVkuxKo5LmLsNNHdQ4G'),(294,'otto.hansen2@gokstadakademiet.no','Otto','Hansen','','','student','$2b$12$sNYJu5XxD2FR/XASlqHwbu','$2b$12$sNYJu5XxD2FR/XASlqHwbuagVbfxNmQTTZqtHBTwOhWtQ9Go7uCLC'),(295,'ella.olsen1@gokstadakademiet.no','Ella','Olsen','','','student','$2b$12$9M3EvS6Sv8GS.fADlqQWHO','$2b$12$9M3EvS6Sv8GS.fADlqQWHOb9sCYlfy5A7jAxJRHJxOujZoprL7uyu'),(296,'mette.eriksen2@gokstadakademiet.no','Mette','Eriksen','','','student','$2b$12$XGIE1brmvl0J9VK5vEl3Ae','$2b$12$XGIE1brmvl0J9VK5vEl3AeEn8LjaZ74w009rjJyO7L1KRcwNGZyjq'),(297,'aleksander.andersen@gokstadakademiet.no','Aleksander','Andersen','','','student','$2b$12$r97vHbnXFuScljRgMVn1Ou','$2b$12$r97vHbnXFuScljRgMVn1OuO4yGpOrrKFmuCg44edVQE8eJcOs5AmO'),(298,'kamilla.moen1@gokstadakademiet.no','Kamilla','Moen','','','student','$2b$12$MTtDTIbpvx/NiWpehSX5re','$2b$12$MTtDTIbpvx/NiWpehSX5reyXO6gxu79OQwQvjietzUasNNLjLL9xW'),(299,'lars.moe1@gokstadakademiet.no','Lars','Moe','','','student','$2b$12$nVcyPNfsUoNBvr7jzU7w1O','$2b$12$nVcyPNfsUoNBvr7jzU7w1OYFxZdWWHRj6Z4OlaHATPGLnNyS8YcP.'),(300,'johannes.moen1@gokstadakademiet.no','Johannes','Moen','','','student','$2b$12$tjC9xVKONF0D8hmZ1ufJb.','$2b$12$tjC9xVKONF0D8hmZ1ufJb.S.bDkTpQTilEGkRyxZWgsDAZLTW.Ugq');
/*!40000 ALTER TABLE `users` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `venues`
--

DROP TABLE IF EXISTS `venues`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `venues` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Name` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Description` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `LocationId` int NOT NULL,
  `StreetAddress` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `PostCode` int NOT NULL,
  `City` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Capacity` int NOT NULL DEFAULT '0',
  PRIMARY KEY (`Id`),
  KEY `IX_Venues_LocationId` (`LocationId`),
  CONSTRAINT `FK_Venues_Locations_LocationId` FOREIGN KEY (`LocationId`) REFERENCES `locations` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `venues`
--

LOCK TABLES `venues` WRITE;
/*!40000 ALTER TABLE `venues` DISABLE KEYS */;
INSERT INTO `venues` VALUES (1,'Fjorden','Auditorium',1,'Torget 7, 4. etg.',3210,'Sandefjord',90),(2,'Verdens Ende','Klasserom',1,'Torget 7, 4. etg.',3210,'Sandefjord',34),(3,'Svenner','Klasserom',1,'Torget 7, 4. etg.',3210,'Sandefjord',36),(4,'Tvistein','Klasserom',1,'Torget 7, 4. etg.',3210,'Sandefjord',26),(5,'Færder','Klasserom',1,'Torget 7, 4. etg.',3210,'Sandefjord',34);
/*!40000 ALTER TABLE `venues` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping events for database 'studd_gok_api'
--
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-02-06 15:59:50
