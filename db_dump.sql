/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

CREATE DATABASE IF NOT EXISTS `hostman` /*!40100 DEFAULT CHARACTER SET utf8mb4 */;
USE `hostman`;

CREATE TABLE IF NOT EXISTS `authentication` (
  `UserId` int(10) unsigned NOT NULL,
  `Issuer` varchar(255) NOT NULL,
  `Subject` varchar(255) NOT NULL,
  UNIQUE KEY `User_And_Issuer` (`UserId`,`Issuer`) USING BTREE,
  UNIQUE KEY `Issuer_And_Subject` (`Issuer`,`Subject`),
  CONSTRAINT `User` FOREIGN KEY (`UserId`) REFERENCES `user` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE TABLE IF NOT EXISTS `host` (
  `Id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `Owner` int(10) unsigned NOT NULL,
  `Name` varchar(16) NOT NULL,
  `IPMode` enum('Static','Dynamic') NOT NULL DEFAULT 'Dynamic',
  `AssignedIP` int(10) unsigned DEFAULT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Owner_Name` (`Owner`,`Name`),
  UNIQUE KEY `AssignedIP` (`AssignedIP`),
  CONSTRAINT `Owner` FOREIGN KEY (`Owner`) REFERENCES `user` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4;

CREATE TABLE IF NOT EXISTS `user` (
  `Id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `Nickname` varchar(16) NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Nickname` (`Nickname`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4;

CREATE TABLE IF NOT EXISTS `vpn_authentication` (
  `HostId` int(10) unsigned NOT NULL,
  `Username` varchar(32) NOT NULL,
  `Password` varchar(32) NOT NULL COMMENT 'Does not need to be hashed/salted;\r\nonly temporary and for machine-only\r\ncommunication.',
  `Expiration` timestamp NOT NULL DEFAULT addtime(current_timestamp(),'0:10:0'),
  UNIQUE KEY `HostId` (`HostId`),
  CONSTRAINT `HostId` FOREIGN KEY (`HostId`) REFERENCES `host` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
