-- Creation base de données:
DROP DATABASE IF EXISTS Fleuriste;
CREATE DATABASE IF NOT EXISTS Fleuriste;
USE Fleuriste;

CREATE TABLE client (
id_client INT PRIMARY KEY AUTO_INCREMENT,
nom varchar(50),
prenom varchar(50),
email varchar(50),
mot_de_passe varchar(50),
num_tel int,
addresse varchar(50),
num_cb int,
date_inscription datetime
);

CREATE TABLE bouquet_standard (
nom_bouquet varchar(50) NOT NULL PRIMARY KEY,
description varchar(100),
prix int,
categorie varchar(50)
);

CREATE TABLE arrangement_floral (
num_bouquet INT PRIMARY KEY AUTO_INCREMENT,
prix_donne int
);

CREATE TABLE commande (
num_commande INT PRIMARY KEY AUTO_INCREMENT,
adresse_livraison varchar(50),
message varchar(50),
date_livraison datetime,
date_commande datetime,
etat_commande varchar(4),
id_client int,
num_bouquet int,
nom_bouquet varchar(50),
FOREIGN KEY (id_client) REFERENCES client (id_client),
FOREIGN KEY (num_bouquet) REFERENCES arrangement_floral (num_bouquet),
FOREIGN KEY (nom_bouquet) REFERENCES bouquet_standard (nom_bouquet)
);

CREATE TABLE produit (
nom_produit varchar(50) NOT NULL PRIMARY KEY,
prix_produit int,
type_produit varchar(1),
disponibilite varchar(50)
);

CREATE TABLE magasin (
num_magasin INT PRIMARY KEY AUTO_INCREMENT,
ville varchar(50),
adresse_magasin varchar(50)
);

CREATE TABLE arrangement_contient (
num_bouquet int NOT NULL,
nom_produit varchar(50) NOT NULL,
PRIMARY KEY (num_bouquet, nom_produit),
FOREIGN KEY (num_bouquet) REFERENCES arrangement_floral (num_bouquet),
FOREIGN KEY (nom_produit) REFERENCES produit (nom_produit)
);

CREATE TABLE est_en_stockage (
nom_produit varchar(50) NOT NULL,
num_magasin int NOT NULL,
quantite int,
PRIMARY KEY (nom_produit, num_magasin),
FOREIGN KEY (nom_produit) REFERENCES produit (nom_produit),
FOREIGN KEY (num_magasin) REFERENCES magasin (num_magasin)
);