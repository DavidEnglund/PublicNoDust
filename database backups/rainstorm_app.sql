-- phpMyAdmin SQL Dump
-- version 4.6.4
-- https://www.phpmyadmin.net/
--
-- Host: localhost
-- Generation Time: Nov 18, 2016 at 09:33 PM
-- Server version: 10.0.28-MariaDB
-- PHP Version: 5.5.38

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `rainstorm_app`
--

-- --------------------------------------------------------

--
-- Table structure for table `AreaType`
--

CREATE TABLE `AreaType` (
  `ID` int(11) NOT NULL,
  `Text` varchar(20) COLLATE utf8_unicode_ci DEFAULT NULL
) ENGINE=MyISAM DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- Dumping data for table `AreaType`
--

INSERT INTO `AreaType` (`ID`, `Text`) VALUES
(0, 'Open Area'),
(1, 'Road');

-- --------------------------------------------------------

--
-- Table structure for table `contactRequests`
--

CREATE TABLE `contactRequests` (
  `id` int(11) NOT NULL,
  `createdDate` datetime NOT NULL,
  `name` varchar(50) COLLATE utf8_unicode_ci NOT NULL,
  `jobLocation` varchar(50) COLLATE utf8_unicode_ci NOT NULL,
  `areaType` varchar(50) COLLATE utf8_unicode_ci NOT NULL,
  `areaSize` int(11) NOT NULL,
  `contactDate` datetime NOT NULL,
  `contactDetails` varchar(100) COLLATE utf8_unicode_ci NOT NULL,
  `jobDuration` int(11) NOT NULL,
  `contactType` varchar(50) COLLATE utf8_unicode_ci NOT NULL,
  `industry` varchar(50) COLLATE utf8_unicode_ci NOT NULL
) ENGINE=MyISAM DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- Dumping data for table `contactRequests`
--

INSERT INTO `contactRequests` (`id`, `createdDate`, `name`, `jobLocation`, `areaType`, `areaSize`, `contactDate`, `contactDetails`, `jobDuration`, `contactType`, `industry`) VALUES
(1, '2016-11-09 17:18:35', 'Sorry about all the emails', 'its not adding to the db', 'Open Area', 1000, '2016-11-09 18:02:00', 'fake@email.com', 30, 'phone', 'Rainstorm'),
(2, '2016-11-09 17:20:55', 'hopfully the last one', 'cleaning up errors and checking JSON', 'Open Area', 1000, '2016-11-09 18:02:00', 'fake@email.com', 30, 'email', 'Rainstorm'),
(3, '2016-11-09 17:21:06', 'hopfully the last one', 'cleaning up errors and checking JSON', 'Open Area', 1000, '2016-11-09 18:02:00', 'fake@email.com', 30, 'email', 'Rainstorm'),
(4, '2016-11-09 17:21:33', 'NOW hopfully the last one', 'cleaning up errors and checking JSON', 'Open Area', 1000, '2016-11-09 18:02:00', 'fake@email.com', 30, 'email', 'Rainstorm'),
(5, '2016-11-13 17:07:41', 'John Doe', 'Northbridge', 'openArea', 10, '0000-00-00 00:00:00', 'test@localhost', 10, 'email', 'Sunhawk'),
(7, '2016-11-13 17:19:22', 'test', 'Northbridge', 'openArea', 10, '0000-00-00 00:00:00', 'test@localhost', 10, 'email', 'Sunhawk'),
(8, '2016-11-13 17:33:04', '', 'Northbridge', 'openArea', 10, '0000-00-00 00:00:00', '', 0, '', ''),
(9, '2016-11-14 01:48:02', 'Lucifer morningstar', '', 'unknown area', 0, '0000-00-00 00:00:00', '666@hell.com', 0, 'email', 'Rainstorm'),
(10, '2016-11-14 02:09:28', 'jon snow', 'perth', 'unknown area', 0, '0000-00-00 00:00:00', 'jon.snow@got.com.au', 0, 'Email', 'Rainstorm'),
(11, '2016-11-14 03:36:18', 'arya stark', ' ', 'unknown area', 0, '0000-00-00 00:00:00', 'arya.stark@got.com.au', 0, 'Email', 'Rainstorm'),
(12, '2016-11-14 12:26:32', 'arya stark', 'perth', 'unknown area', 0, '0000-00-00 00:00:00', 'arya.stark@got.com.au', 0, 'Email', 'Rainstorm'),
(13, '2016-11-14 12:30:34', 'jon snow', 'perth', 'unknown area', 0, '0000-00-00 00:00:00', 'jon.snow@got.com.au', 0, 'Email', 'Rainstorm'),
(14, '2016-11-14 12:49:04', 'jon snow', 'perth', 'unknown area', 0, '0000-00-00 00:00:00', 'jon.snow@got.com.au', 0, 'Email', 'Rainstorm'),
(15, '2016-11-14 13:51:08', 'sansa stark', 'perth', 'unknown area', 0, '0000-00-00 00:00:00', 'sansa.stark@got.com.au', 0, 'Email', 'Rainstorm'),
(16, '2016-11-14 14:01:31', 'sansa stark', 'northbridge', 'unknown area', 0, '0000-00-00 00:00:00', 'sansa.stark@got.com.au', 0, 'Email', 'Rainstorm'),
(17, '2016-11-14 14:05:33', 'sansa stark', 'northbridge', 'unknown area', 0, '0000-00-00 00:00:00', 'sansa.stark@got.com.au', 0, 'Email', 'Rainstorm'),
(18, '2016-11-14 14:10:34', 'sansa stark', 'northbridge', 'unknown area', 0, '0000-00-00 00:00:00', 'sansa.stark@got.com.au', 0, 'Email', 'Rainstorm'),
(19, '2016-11-14 14:17:51', 'sansa stark', 'northbridge', 'unknown area', 0, '0000-00-00 00:00:00', 'sansa.stark@got.com.au', 0, 'Email', 'Rainstorm'),
(20, '2016-11-14 14:20:23', 'jon snow', 'perth', 'unknown area', 0, '0000-00-00 00:00:00', 'jon.snow@got.com.au', 0, 'Email', 'Rainstorm'),
(21, '2016-11-14 14:22:11', 'arya stark', 'northbridge', 'unknown area', 0, '0000-00-00 00:00:00', 'arya.stark@got.com.au', 0, 'Email', 'Rainstorm'),
(22, '2016-11-14 14:22:13', 'arya stark', 'northbridge', 'unknown area', 0, '0000-00-00 00:00:00', 'arya.stark@got.com.au', 0, 'Email', 'Rainstorm'),
(23, '2016-11-14 14:25:59', 'da', 'perth', 'unknown area', 0, '0000-00-00 00:00:00', '666@hell.com', 0, 'Email', 'Rainstorm'),
(24, '2016-11-14 14:28:42', 'jon snow', 'perth', 'unknown area', 0, '0000-00-00 00:00:00', 'jon.snow@got.com.au', 0, 'Email', 'Rainstorm'),
(25, '2016-11-14 14:31:12', 's', 'perth', 'unknown area', 0, '0000-00-00 00:00:00', 's@s.s', 0, 'Email', 'Rainstorm'),
(26, '2016-11-14 14:33:33', 'e', 'perth', 'unknown area', 0, '0000-00-00 00:00:00', '6@6.6', 0, 'Email', 'Rainstorm'),
(27, '2016-11-14 14:40:28', 'd', 'perth', 'unknown area', 0, '0000-00-00 00:00:00', 'd@d.d', 0, 'Email', 'Rainstorm'),
(28, '2016-11-14 14:42:48', 'd', 'perth', 'unknown area', 0, '0000-00-00 00:00:00', 'd@d.d', 0, 'Email', 'Rainstorm'),
(29, '2016-11-14 14:45:48', 'd', 'perth', 'unknown area', 0, '0000-00-00 00:00:00', '6@6.6', 0, 'Email', 'Rainstorm'),
(30, '2016-11-14 14:51:05', 'jon snow', 'northbridge', 'unknown area', 0, '0000-00-00 00:00:00', 'jon.snow@got.com.au', 0, 'Email', 'Rainstorm'),
(31, '2016-11-14 15:24:38', 'jon snow', 'perth', 'unknown area', 0, '0000-00-00 00:00:00', 'jon.snow@got.com.au', 0, 'Email', 'Rainstorm'),
(32, '2016-11-14 17:18:10', 'jon snow', 'perth', 'unknown area', 0, '0000-00-00 00:00:00', 'jon.snow@got.com.au', 0, 'Email', 'Rainstorm'),
(33, '2016-11-15 17:26:46', 'sansa stark', '6000', 'unknown area', 0, '0000-00-00 00:00:00', 'sansa.stark@got.com.au', 0, 'Email', 'Rainstorm');

-- --------------------------------------------------------

--
-- Table structure for table `DBMetaData`
--

CREATE TABLE `DBMetaData` (
  `VersionKey` int(11) NOT NULL,
  `DBVersion` int(11) NOT NULL
) ENGINE=MyISAM DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- Dumping data for table `DBMetaData`
--

INSERT INTO `DBMetaData` (`VersionKey`, `DBVersion`) VALUES
(1, 4);

-- --------------------------------------------------------

--
-- Table structure for table `helpLinks`
--

CREATE TABLE `helpLinks` (
  `id` int(11) NOT NULL,
  `link` varchar(200) COLLATE utf8_unicode_ci NOT NULL,
  `title` varchar(50) COLLATE utf8_unicode_ci NOT NULL
) ENGINE=MyISAM DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- Dumping data for table `helpLinks`
--

INSERT INTO `helpLinks` (`id`, `link`, `title`) VALUES
(1, 'https://rainstorm.com.au/app/userHelp/mainpage.html', 'Main page'),
(2, 'https://rainstorm.com.au/app/userHelp/selectionPage.html', 'Selection menu');

-- --------------------------------------------------------

--
-- Table structure for table `Product`
--

CREATE TABLE `Product` (
  `Id` int(11) NOT NULL,
  `ProductName` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL
) ENGINE=MyISAM DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- Dumping data for table `Product`
--

INSERT INTO `Product` (`Id`, `ProductName`) VALUES
(1, 'Gluon'),
(2, 'DustLig'),
(3, 'DustJel'),
(4, 'DustMag'),
(5, 'Hydromulch');

-- --------------------------------------------------------

--
-- Table structure for table `ProductDescription`
--

CREATE TABLE `ProductDescription` (
  `Id` int(11) NOT NULL,
  `ProductId` int(11) DEFAULT NULL,
  `DurationMaxDays` int(11) DEFAULT NULL,
  `ProductName` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  `ProductDesc` varchar(500) COLLATE utf8_unicode_ci DEFAULT NULL,
  `ApplicationsRequired` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Concentration` int(11) DEFAULT NULL
) ENGINE=MyISAM DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- Dumping data for table `ProductDescription`
--

INSERT INTO `ProductDescription` (`Id`, `ProductId`, `DurationMaxDays`, `ProductName`, `ProductDesc`, `ApplicationsRequired`, `Concentration`) VALUES
(1, 1, 30, 'Gluon', 'Gluon is a high performance polymer that can be sprayed onto soil surfaces to form a "veneer" or "crust" which can withstand wind speeds up to 120km/hr. Its crosslinking nature allows rain to seep through the crust instead of washing it away. A light dosage rate will give you enough protection from the elements for up to a month.', '1 Application only', 35),
(2, 1, 180, 'Gluon', 'Gluon is a high performance polymer that can be sprayed onto soil surfaces to form a "veneer" or "crust" which can withstand wind speeds up to 120km/hr. Its crosslinking nature allows rain to seep through the crust instead of washing it away. A medium dosage rate will give your site protection for up to 6 months.', '1 Application only', 75),
(3, 1, 360, 'Gluon', 'Gluon is a high performance polymer that can be sprayed onto soil surfaces to form a "veneer" or "crust" which can withstand wind speeds up to 120km/hr. Its crosslinking nature allows rain to seep through the crust instead of washing it away. To achieve all year round protection a high dosage rate will be required. Gluon can be used in combination with seed to achieve even longer lasting results.', '1 Application only', 125),
(4, 2, 30, 'DustLig', 'DustLig is a sodium lignosulphonate derived from the wood pulp industry. It is a cheap, short term dust suppression, mainly used to achieve protection from wind erosion during weekends. Because it is water soluble re-application will be required after any rain event.', '1 Application/3 weeks', 150),
(5, 2, 180, 'DustLig', 'DustLig is a sodium lignosulphonate derived from the wood pulp industry. It is a cheap, short term dust suppression, mainly used to achieve protection from wind erosion. Because it is water soluble re-application will be required after any rain event. Without rain it is suggested to re-apply every 2 to 3 weeks for optimal protection.', '1 Application/3 weeks', 150),
(6, 5, 30, 'HydroMulch', 'HydroMulch is a cheap and environmentally friendly product to easily prevent water and wind erosion. Our proprietary blend comes as a full service application and will easily withstand any wind or rain event for up to 3 months. Protection from vehicles and pedestrians is required.', '1 Application only', 4000),
(7, 5, 180, 'HydroMulch with Gluon', 'HydroMulch is a cheap and environmentally friendly product to easily prevent water and wind erosion. Our proprietary blend comes as a full service application. Mixed with Gluon will ensure extra strength and durability on the ground for up to 6 months. Protection from vehicles and pedestrians is required.', '1 Application only', 4000),
(8, 5, 360, 'Hydroseeding', 'HydroSeeding is the best possible option to ensure long term erosion control. Our proprietary blend includes soil enhancers and fast germinating seeds to establish vegetation as soon as possible. Very popular on mine sites and waste treatment plant to complete revegetation requirements. We can add any type of seed or other additives to the product if requested.', '1 Application only', 4000),
(9, 3, 30, 'DustJel', 'DustJel is a concentrated liquid polyacrylamide designated to extend the duration of water on a soil surface before evaporating. Ideally it is used on roads with a regular maintenance schedule or when rain is expected. Best used multiple times a day to reduce regular water application. An automated dosage system is available to avoid manual handling.', '3 Applications/day', 10),
(10, 3, 90, 'DustJel', 'DustJel is a concentrated liquid polyacrylamide designated to extend the duration of water on a soil surface before evaporating. Ideally it is used on roads with a regular maintenance schedule or when rain is expected. Best used multiple times a day to reduce regular water application. An automated dosage system is available to avoid manual handling.', '3 Applications/day', 10),
(11, 4, 30, 'DustMag', 'DustMag is a hygroscopic solution which attracts and retains moisture from the air reducing the need for watering the road for up to 3 months. If the road is properly graded before application, no maintenance will be required.', '1 Application only', 1500),
(12, 4, 90, 'DustMag', 'DustMag is a hygroscopic solution which attracts and retains moisture from the air reducing the need for watering the road for up to 3 months. If the road is properly graded before application, no maintenance will be required.', '1 Application/2 months', 1500);

-- --------------------------------------------------------

--
-- Table structure for table `ProductDuration`
--

CREATE TABLE `ProductDuration` (
  `MaxDays` int(11) DEFAULT NULL,
  `Text` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL
) ENGINE=MyISAM DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- Dumping data for table `ProductDuration`
--

INSERT INTO `ProductDuration` (`MaxDays`, `Text`) VALUES
(30, 'Short Term'),
(90, 'Medium To Long Term'),
(180, 'Medium Term'),
(360, 'Long Term');

-- --------------------------------------------------------

--
-- Table structure for table `ProductMatrix`
--

CREATE TABLE `ProductMatrix` (
  `ID` int(11) NOT NULL,
  `DurationMaxDays` int(11) DEFAULT NULL,
  `AreaTypeId` int(11) DEFAULT NULL,
  `WillRain` tinyint(1) NOT NULL,
  `ProductId` int(11) DEFAULT NULL
) ENGINE=MyISAM DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- Dumping data for table `ProductMatrix`
--

INSERT INTO `ProductMatrix` (`ID`, `DurationMaxDays`, `AreaTypeId`, `WillRain`, `ProductId`) VALUES
(1, 30, 0, 0, 1),
(2, 30, 0, 1, 1),
(3, 30, 0, 0, 2),
(4, 30, 0, 0, 5),
(5, 30, 0, 1, 5),
(6, 180, 0, 0, 1),
(7, 180, 0, 1, 1),
(8, 180, 0, 0, 2),
(9, 180, 0, 0, 5),
(10, 180, 0, 1, 5),
(11, 360, 0, 0, 1),
(12, 360, 0, 0, 5),
(13, 30, 1, 1, 4),
(14, 30, 1, 0, 3),
(15, 90, 1, 0, 4),
(16, 90, 1, 1, 3);

-- --------------------------------------------------------

--
-- Table structure for table `Supplier`
--

CREATE TABLE `Supplier` (
  `Id` int(11) NOT NULL,
  `Name` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  `ContactPhone` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  `ContactEmail` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL
) ENGINE=MyISAM DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- Dumping data for table `Supplier`
--

INSERT INTO `Supplier` (`Id`, `Name`, `ContactPhone`, `ContactEmail`) VALUES
(1, 'Sunhawk', 'Sunhawk Phone', 'Sunhawk Email'),
(2, 'Rainstorm', 'Rainstorm Phone', 'Rainstorm Email');

-- --------------------------------------------------------

--
-- Table structure for table `UserInfo`
--

CREATE TABLE `UserInfo` (
  `UserId` int(11) NOT NULL,
  `Name` varchar(80) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Email` varchar(80) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Phone` varchar(30) COLLATE utf8_unicode_ci DEFAULT NULL
) ENGINE=MyISAM DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- Indexes for dumped tables
--

--
-- Indexes for table `AreaType`
--
ALTER TABLE `AreaType`
  ADD PRIMARY KEY (`ID`),
  ADD UNIQUE KEY `AreaType_ID` (`ID`);

--
-- Indexes for table `contactRequests`
--
ALTER TABLE `contactRequests`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `DBMetaData`
--
ALTER TABLE `DBMetaData`
  ADD PRIMARY KEY (`VersionKey`);

--
-- Indexes for table `helpLinks`
--
ALTER TABLE `helpLinks`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `Product`
--
ALTER TABLE `Product`
  ADD PRIMARY KEY (`Id`),
  ADD UNIQUE KEY `Product_Id` (`Id`);

--
-- Indexes for table `ProductDescription`
--
ALTER TABLE `ProductDescription`
  ADD PRIMARY KEY (`Id`);

--
-- Indexes for table `ProductMatrix`
--
ALTER TABLE `ProductMatrix`
  ADD PRIMARY KEY (`ID`);

--
-- Indexes for table `Supplier`
--
ALTER TABLE `Supplier`
  ADD PRIMARY KEY (`Id`),
  ADD UNIQUE KEY `Supplier_Id` (`Id`);

--
-- Indexes for table `UserInfo`
--
ALTER TABLE `UserInfo`
  ADD PRIMARY KEY (`UserId`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `contactRequests`
--
ALTER TABLE `contactRequests`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=34;
--
-- AUTO_INCREMENT for table `helpLinks`
--
ALTER TABLE `helpLinks`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;
--
-- AUTO_INCREMENT for table `ProductDescription`
--
ALTER TABLE `ProductDescription`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=13;
--
-- AUTO_INCREMENT for table `ProductMatrix`
--
ALTER TABLE `ProductMatrix`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=17;
--
-- AUTO_INCREMENT for table `UserInfo`
--
ALTER TABLE `UserInfo`
  MODIFY `UserId` int(11) NOT NULL AUTO_INCREMENT;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
