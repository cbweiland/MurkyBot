CREATE TABLE `__EFMigrationsHistory` (
    `MigrationId` varchar(150) NOT NULL,
    `ProductVersion` varchar(32) NOT NULL,
    PRIMARY KEY (`MigrationId`)
);

START TRANSACTION;

CREATE TABLE `ChannelConfigs` (
    `ChannelConfigId` int NOT NULL AUTO_INCREMENT,
    `ChannelId` text NULL,
    `ChannelType` int NOT NULL,
    PRIMARY KEY (`ChannelConfigId`)
);

CREATE TABLE `EventMeetings` (
    `EventMeetingId` int NOT NULL AUTO_INCREMENT,
    `Title` text NULL,
    `Date` text NULL,
    `Time` text NULL,
    `CheckIn` text NULL,
    `Description` text NULL,
    `EventType` text NULL,
    `DiscordMessageId` text NULL,
    PRIMARY KEY (`EventMeetingId`)
);

CREATE TABLE `SignUp` (
    `SignUpId` int NOT NULL AUTO_INCREMENT,
    `PersonDiscordId` text NULL,
    `EmoteClicked` text NULL,
    `DateTimeSignedUp` datetime NOT NULL,
    `EventMeetingId` int NOT NULL,
    PRIMARY KEY (`SignUpId`),
    CONSTRAINT `FK_SignUp_EventMeetings_EventMeetingId` FOREIGN KEY (`EventMeetingId`) REFERENCES `EventMeetings` (`EventMeetingId`) ON DELETE CASCADE
);

CREATE INDEX `IX_SignUp_EventMeetingId` ON `SignUp` (`EventMeetingId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20210829164217_init', '5.0.8');

COMMIT;

