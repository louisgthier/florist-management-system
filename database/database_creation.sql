-- Database creation
DROP DATABASE IF EXISTS florist;
CREATE DATABASE IF NOT EXISTS florist;

SELECT * FROM mysql.user;

SELECT * FROM mysql.role_edges;

-- Drop all roles
DELETE FROM mysql.role_edges WHERE FROM_HOST != "fuiadhazufegiadzg";

-- Drop all users
DROP USER IF EXISTS 'florist_user'@'localhost';
DROP USER IF EXISTS 'florist_client';

DROP USER IF EXISTS 'louis@gmail.com'@'localhost';
DROP USER IF EXISTS 'marin@gmail.com'@'localhost';

-- Create a user with permission to create other users

CREATE USER IF NOT EXISTS 'florist_user'@'localhost' IDENTIFIED BY 'password';

-- SET DEFAULT ROLE florist_client TO 'florist_user'@'localhost';

-- Schema

USE florist;

DROP TABLE IF EXISTS client;

CREATE TABLE client (
id INT PRIMARY KEY AUTO_INCREMENT,
first_name VARCHAR(50),
name VARCHAR(50),
email VARCHAR(320),
phone_number VARCHAR(20),
address VARCHAR(100),
credit_card VARCHAR(19),
inscription_date DATETIME
);

CREATE TABLE standard_bouquet (
name VARCHAR(50) NOT NULL PRIMARY KEY,
description VARCHAR(100),
price INT,
category VARCHAR(50)
);

CREATE TABLE flower_arrangement (
id INT PRIMARY KEY ,
price INT
);

CREATE TABLE purchase_order (
id INT PRIMARY KEY AUTO_INCREMENT,
delivery_address VARCHAR(50),
message VARCHAR(50),
delivery_date DATETIME,
order_date DATETIME,
order_state VARCHAR(4),
client_id INT,
arrangement_id INT,
bouquet_name VARCHAR(50),
FOREIGN KEY (client_id) REFERENCES client (id),
FOREIGN KEY (arrangement_id) REFERENCES flower_arrangement (id),
FOREIGN KEY (bouquet_name) REFERENCES standard_bouquet (name)
);


CREATE TABLE item (
name VARCHAR(50) PRIMARY KEY,
price INT,
type VARCHAR(1),
availability VARCHAR(50)
);

CREATE TABLE shop (
id INT PRIMARY KEY AUTO_INCREMENT,
address VARCHAR(50)
);

CREATE TABLE arrangement_contains (

arrangement_id INT NOT NULL,
item_name VARCHAR(50) NOT NULL,
PRIMARY KEY (arrangement_id, item_name),
FOREIGN KEY (arrangement_id) REFERENCES flower_arrangement (id),
FOREIGN KEY (item_name) REFERENCES item (name)
);

CREATE TABLE is_stored (
name VARCHAR(50) NOT NULL,
id INT NOT NULL,
quantity INT,
PRIMARY KEY (name, id),
FOREIGN KEY (name) REFERENCES item (name),
FOREIGN KEY (id) REFERENCES shop (id)
);

-- Manage client privileges

CREATE ROLE IF NOT EXISTS florist_client;
GRANT SELECT ON florist.standard_bouquet TO florist_client;
GRANT SELECT ON florist.purchase_order TO florist_client;


