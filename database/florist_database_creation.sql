-- Database creation
DROP DATABASE IF EXISTS florist;
CREATE DATABASE IF NOT EXISTS florist;

SELECT * FROM mysql.user;

-- Drop all roles
DELETE FROM mysql.role_edges;

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
id INT PRIMARY KEY AUTO_INCREMENT,
price INT
);

CREATE TABLE order (
id INT PRIMARY KEY AUTO_INCREMENT,
delivery_adress VARCHAR(50),
message VARCHAR(50),
delivery_date DATETIME,
order_date DATETIME,
order_state VARCHAR(4),
client_id INT,
arrangement_id INT,
bouquet_name VARCHAR(50),
FOREIGN KEY (client_id) REFERENCES client (client_id),
FOREIGN KEY (arrangement_id) REFERENCES flower_arrangement (arrangement_id),
FOREIGN KEY (bouquet_name) REFERENCES standard_bouquet (bouquet_name)
);

CREATE TABLE item (
name VARCHAR(50) NOT NULL PRIMARY KEY,
price INT,
type VARCHAR(1),
availability VARCHAR(50)
);

CREATE TABLE shop (
id INT PRIMARY KEY AUTO_INCREMENT,
address VARCHAR(50)
);

CREATE TABLE arrangement_contains (

num_bouquet INT NOT NULL,
nom_produit VARCHAR(50) NOT NULL,
PRIMARY KEY (num_bouquet, nom_produit),
FOREIGN KEY (num_bouquet) REFERENCES flower_arrangement (num_bouquet),
FOREIGN KEY (nom_produit) REFERENCES item (nom_produit)
);

CREATE TABLE is_stored (
nom_produit VARCHAR(50) NOT NULL,
num_magasin INT NOT NULL,
quantite INT,
PRIMARY KEY (nom_produit, num_magasin),
FOREIGN KEY (nom_produit) REFERENCES produit (nom_produit),
FOREIGN KEY (num_magasin) REFERENCES magasin (num_magasin)
);



-- Manage client privileges

CREATE ROLE IF NOT EXISTS florist_client;
GRANT SELECT ON florist.standard_bouquet TO florist_client;


