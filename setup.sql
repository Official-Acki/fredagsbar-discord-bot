-- SQLBook: Code
CREATE DATABASE IF NOT EXISTS beerbotdb;

USE beerbotdb;

CREATE TABLE Channel (
    id INT PRIMARY KEY NOT NULL AUTO_INCREMENT,
    discord_id varchar(255),
    name VARCHAR(255)
    -- event_id INT,
    -- FOREIGN KEY (event_id) REFERENCES Event(id)
);

CREATE TABLE Person (
    id INT PRIMARY KEY NOT NULL AUTO_INCREMENT,
    discord_id varchar(255),
    name VARCHAR(255)
);

CREATE TABLE Event (
    id INT PRIMARY KEY NOT NULL AUTO_INCREMENT,
    name VARCHAR(255),
    description TEXT,
    location VARCHAR(255),
    start_date DATETIME,
    end_date DATETIME,
    -- created_at DATETIME,
    going_emoji VARCHAR(255),
    not_going_emoji VARCHAR(255),
    channel_id INT,
    author_person_id INT,
    FOREIGN KEY (channel_id) REFERENCES Channel(id),
    FOREIGN KEY (author_person_id) REFERENCES Person(id)
);

CREATE TABLE EventPerson (
    id INT PRIMARY KEY NOT NULL AUTO_INCREMENT,
    event_id INT,
    person_id INT,
    going BOOLEAN,
    FOREIGN KEY (event_id) REFERENCES Event(id),
    FOREIGN KEY (person_id) REFERENCES Person(id)
);

CREATE TABLE BeerCrate (
    id int PRIMARY KEY NOT NULL AUTO_INCREMENT,
    created DATETIME,
    amount float,
    ower_person_id INT,
    FOREIGN KEY (ower_person_id) REFERENCES Person(id)
);

CREATE TABLE Message (
    id INT PRIMARY KEY NOT NULL AUTO_INCREMENT,
    discord_id varchar(255),
    text TEXT,
    time DATETIME,
    channel_id INT,
    person_id INT,
    FOREIGN KEY (channel_id) REFERENCES Channel(id),
    FOREIGN KEY (person_id) REFERENCES Person(id)
);