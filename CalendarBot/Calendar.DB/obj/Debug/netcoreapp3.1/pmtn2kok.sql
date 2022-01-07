START TRANSACTION;

ALTER TABLE `ChannelConfigs` ADD `GuildId` text NULL;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20210904162156_GuildID', '5.0.8');

COMMIT;

