CREATE TABLE `match` (
	`id` BIGINT UNSIGNED NOT NULL AUTO_INCREMENT,
    `event_id` BIGINT UNSIGNED,
    `match_start` DATETIME,
    `client_name` VARCHAR(100),
    `filestamp` VARCHAR(100),
    `game_directory` VARCHAR(100),
    `map_name` VARCHAR(100),
    `network_protocol` SMALLINT UNSIGNED,
    `playback_frames` MEDIUMINT UNSIGNED,
    `playback_ticks` MEDIUMINT UNSIGNED,
    `playback_time` DECIMAL(12, 4) UNSIGNED,
    `protocol` SMALLINT UNSIGNED,
    `server_name` VARCHAR(100),
    `signon_length` MEDIUMINT UNSIGNED,
    PRIMARY KEY (`id`)
);
CREATE TABLE `frags` (
	`match_id` BIGINT UNSIGNED,
    `round` TINYINT UNSIGNED,
    `tick` MEDIUMINT UNSIGNED,
    `victim_steam_id` BIGINT UNSIGNED,
    `victim_weapon` VARCHAR(30),
    `victim_position_x` DECIMAL(20,10),
    `victim_position_y` DECIMAL(20,10),
    `victim_position_z` DECIMAL(20,10),
    `victim_hp` TINYINT UNSIGNED,
    `killer_steam_id` BIGINT UNSIGNED,
    `killer_weapon` VARCHAR(30),
    `killer_position_x` DECIMAL(20,10),
    `killer_position_y` DECIMAL(20,10),
    `killer_position_z` DECIMAL(20,10),
    `killer_hp` TINYINT UNSIGNED,
    `assister_steam_id` BIGINT UNSIGNED,
    `is_headshot` BIT
);
CREATE TABLE `damage` (
	`match_id` BIGINT UNSIGNED,
    `round` TINYINT UNSIGNED,
    `tick` MEDIUMINT UNSIGNED,
    `victim_steam_id` BIGINT UNSIGNED,
    `victim_weapon` VARCHAR(30),
    `victim_position_x` DECIMAL(20,10),
    `victim_position_y` DECIMAL(20,10),
    `victim_position_z` DECIMAL(20,10),
    `victim_hp` TINYINT UNSIGNED,
    `victim_health` SMALLINT UNSIGNED,
    `victim_health_damage` SMALLINT UNSIGNED,
    `victim_armor` SMALLINT UNSIGNED,
    `victim_armor_damage` SMALLINT UNSIGNED,
    `attacker_steam_id` BIGINT UNSIGNED,
    `attacker_weapon` VARCHAR(30),
    `attacker_position_x` DECIMAL(20,10),
    `attacker_position_y` DECIMAL(20,10),
    `attacker_position_z` DECIMAL(20,10),
    `attacker_hp` TINYINT UNSIGNED,
    `hitgroup` VARCHAR(30)
);
CREATE TABLE `teams` (
	`id` BIGINT UNSIGNED AUTO_INCREMENT,
    `name` VARCHAR(30),
    PRIMARY KEY (`id`)
);
CREATE TABLE `players` (
	`steam_id` BIGINT UNSIGNED,
    `name` VARCHAR(50),
    PRIMARY KEY (`steam_id`)
);
CREATE TABLE `match_players` (
	`match_id` BIGINT UNSIGNED,
    `steam_id` BIGINT UNSIGNED,
    `team_id` BIGINT UNSIGNED
);