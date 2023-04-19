CREATE TABLE [Company](
CompanyId [int] Primary key IDENTITY(1,1) NOT NULL,
Gid varchar(500) NOT NULL,
Name VARCHAR(50) NOT NULL,
[Address] varchar(400) NULL,
City varchar(400) NULL,
Parish varchar(400) NULL,
Country varchar(400) NULL,
[State] varchar(400) NULL,
Tele varchar(400) NULL,
Fax varchar(400) NULL,
Email varchar(400) NULL,
Website varchar(400) NULL,
Blog varchar(400) NULL,
Facebook varchar(400) NULL,
Instagram varchar(400) NULL,
Twitter varchar(400) NULL,
Youtube varchar(400) NULL,
GoogleP varchar(400) NULL,
[Image] varchar(400) NULL,
APIKey varchar(MAX) NULL,
URL varchar(400) NULL,
[Username] varchar(400) NULL,
[Password] varchar(400) NULL,
[Published] [bit] NOT NULL,
DateCreated [datetime] NOT NULL
)

CREATE TABLE [dbo].[Country](
CountryId [int] Primary key IDENTITY(1,1) NOT NULL,
[Description] [varchar](100) NOT NULL,
[TwoLetterIsoCode] [nvarchar](2) NULL,
[ThreeLetterIsoCode] [nvarchar](3) NULL,
[AllowsBilling] [bit] NOT NULL,
[AllowsShipping] [bit] NOT NULL,
[NumericIsoCode] [int] NOT NULL,
[Published] [bit] NOT NULL,
[DisplayOrder] [int] NOT NULL,
)

CREATE TABLE [dbo].[PermissionGroup] (
 PermissionGroupId INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
 [Description] [varchar](200) NOT NULL,
 [Published] [bit] NOT NULL,
 Deleted BIT NOT NULL,
 DateCreated DATETIME NOT NULL,
)

CREATE TABLE [dbo].[Permission] (
PermissionId INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
AccessType INT NOT NULL,
AccessScope INT NOT NULL,
[Description] [varchar](200) NOT NULL,
)

CREATE TABLE Location
(
LocationId INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
Gid VARCHAR(MAX) NOT NULL,
Description VARCHAR(50) NOT NULL,
[Address] [varchar](140) NULL,
CountryId INT FOREIGN KEY REFERENCES Country(CountryId) NOT NULL,
Telehpone VARCHAR(200) NULL,
--UserId INT FOREIGN KEY REFERENCES [User](UserId) NULL,
DateCreated DATETIME NOT NULL,
Deleted BIT NOT NULL
)

CREATE TABLE [dbo].[User]
(
UserId INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
Gid varchar(500) NOT NULL,
Firstname VARCHAR(250) NOT NULL,
Lastname varchar(250)NOT NULL,
Username varchar(250)NOT NULL,
Password varchar(250)NOT NULL,
Email VARCHAR(250) NOT NULL,
Image VARCHAR(500) NULL,
PermissionGroupId INT FOREIGN KEY REFERENCES PermissionGroup(PermissionGroupId) NOT NULL,
LocationId INT FOREIGN KEY REFERENCES [Location] (LocationId) NOT NULL,
[Status] BIT NOT NULL, --Active or inactive
Islocked BIT NOT NULL,
LastlockedDate DATETIME,
LoginStatus BIT NOT NULL,
FailedLoginAttempts INT NULL,
LastDateLogin DATETIME,
LastPasswordChangedDate DATETIME,
StartDate DATETIME NULL,
EndDate DATETIME NULL,
IPAddress VARCHAR(150) NULL,
Deleted BIT NOT NULL,
DateCreated DATETIME NOT NULL,
)


CREATE TABLE [dbo].[PermissionRf](
PermissionRfId INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
PermissionGroupId INT FOREIGN KEY REFERENCES PermissionGroup(PermissionGroupId) NOT NULL,
PermissionId INT FOREIGN KEY REFERENCES Permission (PermissionId) NOT NULL,
UserId INT FOREIGN KEY REFERENCES [User](UserId) NULL,
DateCreated DATETIME NOT NULL,
Deleted BIT NOT NULL
)

CREATE TABLE [Loglevel](
LoglevelId [int] Primary key IDENTITY(1,1) NOT NULL,
Name VARCHAR(50) NOT NULL,
[Published] [bit] NOT NULL
)

CREATE TABLE [Log](
[Id] [int] Primary key IDENTITY(1,1) NOT NULL,
Gid varchar(500) NOT NULL,
UserId INT FOREIGN KEY REFERENCES [User](UserId) NULL,
LoglevelId INT FOREIGN KEY REFERENCES Loglevel (LoglevelId) NULL,
LocationId INT FOREIGN KEY REFERENCES Location (LocationId) NOT NULL,
Device varchar(MAX) NULL,
Shortmessage varchar(MAX) NULL,
Fullmessage varchar(MAX) NULL,
IPaddress varchar(MAX) NULL,
PageURL varchar(MAX) NULL,
ReferrerURL varchar(MAX) NULL,
DateCreated DATETIME NOT NULL
)

CREATE TABLE [dbo].[Setting](
SettingId [int] Primary key IDENTITY(1,1) NOT NULL,
Gid varchar(500) NOT NULL,
DisplayOrder INT,
Label [varchar](MAX) NULL,
ControlId INT NOT NULL,
[Name] [varchar](300) NULL,
[Value] [varchar](2000) NULL,
ToolTip [varchar](MAX) NULL,
UserId INT FOREIGN KEY REFERENCES [User](UserId) NOT NULL,
DateCreated DATETIME NOT NULL,
)


CREATE TABLE NotificationType(
NotificationTypeId INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
[Description] [varchar](200) NOT NULL,
Active BIT NOT NULL
)

CREATE TABLE [Notification](
NotificationId INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
NotificationTypeId INT FOREIGN KEY REFERENCES [NotificationType](NotificationTypeId) NOT NULL,
[Description] [varchar](200) NOT NULL,
DateCreate DATETIME NOT NULL,
[Delete] BIT NOT NULL
)

CREATE TABLE NotificationRF(
NotificationRFId INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
UserId INT FOREIGN KEY REFERENCES [User](UserId) NOT NULL,
NotificationId INT FOREIGN KEY REFERENCES [Notification](NotificationId) NOT NULL,
IsRead BIT NOT NULL,
DateRead DATETIME NULL,
[Delete] BIT NOT NULL
)

CREATE TABLE AuditType(
AuditTypeId INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
[Description] [varchar](200) NOT NULL
)

CREATE TABLE AuditTrail(
AuditTrailId INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
UserId INT FOREIGN KEY REFERENCES [User](UserId) NULL,
AuditTypeId INT FOREIGN KEY REFERENCES AuditType (AuditTypeId)NOT NULL,
[Date] DATETIME,
IPAddress [VARCHAR](200) NOT NULL,
Description [varchar](MAX) NOT NULL
)

CREATE TABLE [Status](
StatusId INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
[Description] [varchar](200) NOT NULL,
[Published] [bit] NOT NULL
)


CREATE TABLE SeqControlNumber(
SeqControlId INT IDENTITY(1,1)PRIMARY KEY NOT NULL,
Gid VARCHAR(MAX) NOT NULL,
RunNo INT NOT NULL,
)

CREATE TABLE Tier
(
TierId INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
[Description] [varchar](200) NOT NULL,
[Published] [bit] NOT NULL
)

CREATE TABLE MatchTable(
MatchTableId INT Primary key IDENTITY(1,1) NOT NULL,
Gid VARCHAR(MAX) NOT NULL,
ProductNumber INT NOT NULL,
Name VARCHAR(MAX) NOT NULL,
PickupZone VARCHAR(MAX) NOT NULL,
Supplier varchar(MAX) NOT NULL,
TierId INT FOREIGN KEY REFERENCES [Tier](TierId) NOT NULL,
Cost DECIMAL(18,2) NOT NULL,
CountryId INT FOREIGN KEY REFERENCES Country(CountryId) NOT NULL,
UserId INT FOREIGN KEY REFERENCES [User](UserId) NOT NULL,
Deleted BIT NOT NULL,
DateCreated DATETIME NOT NULL
)

CREATE TABLE RunResult
(
RunResultId INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
Gid VARCHAR(MAX) NOT NULL,
RunNo VARCHAR INT NOT NULL,
StatusId INT FOREIGN KEY REFERENCES [Status](StatusId) NOT NULL,
[Filename] VARCHAR(MAX) NOT NULL,
Comments VARCHAR(MAX) NULL,
DateCreated DATETIME NOT NULL,
[Deleted] BIT NOT NULL
)

