-- --------------------------------------------------------
-- Host:                         127.0.0.1
-- Server version:               10.6.7-MariaDB - mariadb.org binary distribution
-- Server OS:                    Win64
-- HeidiSQL Version:             11.3.0.6295
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;


-- Dumping database structure for items
CREATE DATABASE IF NOT EXISTS `items` /*!40100 DEFAULT CHARACTER SET utf8mb4 */;
USE `items`;

-- Dumping structure for table items.contacts
CREATE TABLE IF NOT EXISTS `contacts` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `ContactId` longtext DEFAULT NULL,
  `Name` longtext DEFAULT NULL,
  `Last` longtext DEFAULT NULL,
  `Server` longtext DEFAULT NULL,
  `Image` longtext DEFAULT NULL,
  `LastDate` datetime(6) DEFAULT NULL,
  `IsContactOf` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Contacts_IsContactOf` (`IsContactOf`),
  CONSTRAINT `FK_Contacts_Users_IsContactOf` FOREIGN KEY (`IsContactOf`) REFERENCES `users` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4;

-- Dumping data for table items.contacts: ~2 rows (approximately)
DELETE FROM `contacts`;
/*!40000 ALTER TABLE `contacts` DISABLE KEYS */;
INSERT INTO `contacts` (`Id`, `ContactId`, `Name`, `Last`, `Server`, `Image`, `LastDate`, `IsContactOf`) VALUES
	(1, 'avital', 'Avital Aviv', 'Bye', 'localhost:7030', 'bla', '2022-06-02 19:26:03.000000', 'omer'),
	(2, 'omer', 'Omer', 'Ok', 'localhost:7030', 'bla', '2022-06-02 19:26:29.000000', 'avital');
/*!40000 ALTER TABLE `contacts` ENABLE KEYS */;

-- Dumping structure for table items.conversations
CREATE TABLE IF NOT EXISTS `conversations` (
  `Id` varchar(255) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Dumping data for table items.conversations: ~4 rows (approximately)
DELETE FROM `conversations`;
/*!40000 ALTER TABLE `conversations` DISABLE KEYS */;
INSERT INTO `conversations` (`Id`) VALUES
	('1'),
	('2'),
	('3'),
	('4');
/*!40000 ALTER TABLE `conversations` ENABLE KEYS */;

-- Dumping structure for table items.conversationuser
CREATE TABLE IF NOT EXISTS `conversationuser` (
  `ContactsId` varchar(255) NOT NULL,
  `ConversationsId` varchar(255) NOT NULL,
  PRIMARY KEY (`ContactsId`,`ConversationsId`),
  KEY `IX_ConversationUser_ConversationsId` (`ConversationsId`),
  CONSTRAINT `FK_ConversationUser_Conversations_ConversationsId` FOREIGN KEY (`ConversationsId`) REFERENCES `conversations` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_ConversationUser_Users_ContactsId` FOREIGN KEY (`ContactsId`) REFERENCES `users` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Dumping data for table items.conversationuser: ~2 rows (approximately)
DELETE FROM `conversationuser`;
/*!40000 ALTER TABLE `conversationuser` DISABLE KEYS */;
INSERT INTO `conversationuser` (`ContactsId`, `ConversationsId`) VALUES
	('avital', '1'),
	('omer', '1');
/*!40000 ALTER TABLE `conversationuser` ENABLE KEYS */;

-- Dumping structure for table items.messages
CREATE TABLE IF NOT EXISTS `messages` (
  `Id` varchar(255) NOT NULL,
  `UserId` varchar(255) DEFAULT NULL,
  `Content` longtext DEFAULT NULL,
  `Created` longtext DEFAULT NULL,
  `ConversationId` varchar(255) DEFAULT NULL,
  `Sent` tinyint(1) NOT NULL,
  `ConversationId1` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `IX_Messages_ConversationId1` (`ConversationId1`),
  KEY `IX_Messages_ConversationId` (`ConversationId`),
  KEY `IX_Messages_UserId` (`UserId`),
  CONSTRAINT `FK_Messages_Conversations_ConversationId` FOREIGN KEY (`ConversationId`) REFERENCES `conversations` (`Id`),
  CONSTRAINT `FK_Messages_Conversations_ConversationId1` FOREIGN KEY (`ConversationId1`) REFERENCES `conversations` (`Id`),
  CONSTRAINT `FK_Messages_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `users` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Dumping data for table items.messages: ~2 rows (approximately)
DELETE FROM `messages`;
/*!40000 ALTER TABLE `messages` DISABLE KEYS */;
INSERT INTO `messages` (`Id`, `UserId`, `Content`, `Created`, `ConversationId`, `Sent`, `ConversationId1`) VALUES
	('1', 'avital', 'Bye', '12:34', '1', 1, '1'),
	('2', 'omer', 'Ok', '12:34', '1', 1, '2');
/*!40000 ALTER TABLE `messages` ENABLE KEYS */;

-- Dumping structure for table items.users
CREATE TABLE IF NOT EXISTS `users` (
  `Id` varchar(255) NOT NULL,
  `LastDate` datetime(6) DEFAULT NULL,
  `RefContactId` varchar(255) DEFAULT NULL,
  `Name` longtext DEFAULT NULL,
  `ContactId` longtext DEFAULT NULL,
  `CreatedDate` longtext DEFAULT NULL,
  `Password` longtext NOT NULL,
  `Server` longtext DEFAULT NULL,
  `Last` longtext DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Users_RefContactId` (`RefContactId`),
  CONSTRAINT `FK_Users_Users_RefContactId` FOREIGN KEY (`RefContactId`) REFERENCES `users` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Dumping data for table items.users: ~2 rows (approximately)
DELETE FROM `users`;
/*!40000 ALTER TABLE `users` DISABLE KEYS */;
INSERT INTO `users` (`Id`, `LastDate`, `RefContactId`, `Name`, `ContactId`, `CreatedDate`, `Password`, `Server`, `Last`) VALUES
	('avital', '2022-06-02 19:22:23.000000', NULL, 'Avital Aviv', NULL, '12:34', '123456', 'localhost:7030', 'Bye'),
	('omer', '2022-06-02 19:22:23.000000', NULL, 'Omer', NULL, '12:34', '123456', 'localhost:7030', 'Ok');
/*!40000 ALTER TABLE `users` ENABLE KEYS */;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
