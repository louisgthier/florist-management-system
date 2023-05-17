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
id INT PRIMARY KEY AUTO_INCREMENT,
price INT
);

CREATE TABLE shop (
id INT PRIMARY KEY AUTO_INCREMENT,
address VARCHAR(50)
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
shop_id INT,
FOREIGN KEY (client_id) REFERENCES client (id),
FOREIGN KEY (arrangement_id) REFERENCES flower_arrangement (id),
FOREIGN KEY (bouquet_name) REFERENCES standard_bouquet (name),
FOREIGN KEY (shop_id) REFERENCES shop(id)
);


CREATE TABLE item (
name VARCHAR(50) PRIMARY KEY,
price INT,
type VARCHAR(1),
availability VARCHAR(50)
);


CREATE TABLE arrangement_contains (
arrangement_id INT NOT NULL AUTO_INCREMENT,
item_name VARCHAR(50) NOT NULL,
quantity INT,
PRIMARY KEY (arrangement_id, item_name),
FOREIGN KEY (arrangement_id) REFERENCES flower_arrangement (id),
FOREIGN KEY (item_name) REFERENCES item (name)
);

CREATE TABLE is_stored (
item_name VARCHAR(50) NOT NULL,
shop_id INT NOT NULL,
quantity INT,
PRIMARY KEY (item_name,shop_id),
FOREIGN KEY (item_name) REFERENCES item (name),
FOREIGN KEY (shop_id) REFERENCES shop (id)
);

-- Manage client privileges

CREATE ROLE IF NOT EXISTS florist_client;
GRANT SELECT ON florist.standard_bouquet TO florist_client;
GRANT SELECT ON florist.purchase_order TO florist_client;
GRANT SELECT ON florist.arrangement_contains TO florist_client;
GRANT SELECT ON florist.item TO florist_client;


USE florist;

INSERT INTO client (first_name, name, email, phone_number, address, credit_card, inscription_date)
VALUES
('Martin', 'Sophie', 'sophie.martin@gmail.com', 0123456789, '10 Rue du Bac, Paris', 1234567890123456, '2017-03-14'),
('Durand', 'Luc', 'luc.durand@yahoo.fr', 0234567890, '28 Rue des Champs, Lyon', 2345678901234567, '2018-08-02'),
('Petit', 'Charlotte', 'charlotte.petit@hotmail.com', 0345678901, '17 Rue de la Paix, Marseille', 3456789012345678, '2016-05-21'),
('Lefebvre', 'Maxime', 'maxime.lefebvre@gmail.com', 0456789012, '11 Rue de la Gare, Lille', 4567890123456789, '2021-02-10'),
('Moreau', 'Emma', 'emma.moreau@gmail.com', 0567890123, '9 Rue de la Liberté, Bordeaux', 5678901234567890, '2022-11-30'),
('Girard', 'Hugo', 'hugo.girard@yahoo.fr', 0678901234, '13 Rue des Fleurs, Toulouse', 6789012345678901, '2019-10-15'),
('Robin', 'Léa', 'lea.robin@hotmail.com', 0789012345, '21 Rue des Lilas, Nice', 7890123456789012, '2017-07-07'),
('Rousseau', 'Ethan', 'ethan.rousseau@gmail.com', 0890123456, '6 Rue du Marché, Nantes', 8901234567890123, '2023-01-05'),
('Jean', 'Inès', 'ines.jean@yahoo.fr', 0901234567, '2 Rue des Écoles, Strasbourg', 9012345678901234, '2016-09-18'),
('Dupont', 'Léo', 'leo.dupont@hotmail.com', 0912345678, '12 Rue de la Poste, Grenoble', 0123456789012345, '2018-06-12'),
('Benoit', 'Amandine', 'amandine.benoit@gmail.com', 0123456789, '14 Rue du Palais, Montpellier', 1234567890123456, '2022-04-29'),
('Dubois', 'Antoine', 'antoine.dubois@yahoo.fr', 0234567890, '19 Rue de la Mairie, Rennes', 2345678901234567, '2020-11-22');

INSERT INTO standard_bouquet (name, description, price, category) VALUES 
-- (null,null,null,null),
('Gros Merci', 'Arrangement floral avec marguerites et verdure', 45, 'Toute occasion'),
('L’amoureux', 'Arrangement floral avec roses blanches et roses rouges', 65, 'St-Valentin'),
('L’Exotique', 'Arrangement floral avec ginger oiseaux du paradis roses et genet', 40, 'Toute occasion'),
('Maman', 'Arrangement floral avec gerbera roses blanches lys et alstroméria', 80, 'Fête des mères'),
('Vive la mariée', 'Arrangement floral avec lys et orchidées', 120, 'Mariage'),
('Le Printanier', 'Arrangement floral avec tulipes et jonquilles', 35, 'Toute occasion'),
('Belle Epoque', 'Arrangement floral avec pivoines et roses', 90, 'Anniversaire'),
('Le Jardin Anglais', 'Arrangement floral avec roses anglaises et lavande', 55, 'Toute occasion'),
('Fleurs du Sud', 'Arrangement floral avec lavande et tournesols', 50, 'Toute occasion'),
('Le Sophistiqué', 'Arrangement floral avec calla et roses', 75, 'Mariage');

-- Fleurs
INSERT INTO item (name, price, type, availability) VALUES 
('Gerbera', 5.00, 'f', 'à lannée'),
('Ginger', 4.00, 'f', 'à lannée'),
('Glaïeul', 1.00, 'f', 'mai à novembre'),
('Marguerite', 2.25, 'f', 'à lannée'),
('Rose rouge', 2.50, 'f', 'à lannée'),
('Tulipe', 3.50, 'f', 'février à mai'),
('Orchidée', 12.00, 'f', 'à lannée'),
('Lys', 7.50, 'f', 'à l''année'),
('Bouquet de pivoines', 20.00, 'f', 'mai à juin'),
('Chrysanthème', 1.50, 'f', 'septembre à novembre'),
('Oeillet', 1.75, 'f', 'à lannée'),
('Lavande', 4.50, 'f', 'juin à septembre'),
('Anthurium', 8.00, 'f', 'à lannée'),
('Dahlia', 3.00, 'f', 'juillet à septembre'),
('Edelweiss', 6.00, 'f', 'juin à août'),
('Œillet d''Inde', 2.00, 'f', 'juin à octobre'),
('Freesia', 4.50, 'f', 'mars à juin'),
('Iris', 5.50, 'f', 'mars à juin'),
('Lilas', 6.50, 'f', 'avril à juin'),
('Pensée', 2.50, 'f', 'à lannée');

-- Items
INSERT INTO item (name, price, type, availability) VALUES 
('Pot en terre cuite', 6.00, 'a', 'à lannée'),
('Vase en verre', 8.00, 'a', 'à lannée'),
('Cache-pot en osier', 10.00, 'a', 'à lannée'),
('Panier en rotin', 12.00, 'a', 'à lannée'),
('Tuteur pour plante', 4.50, 'a', 'à lannée'),
('Arrosoir en métal', 15.00, 'a', 'à lannée'),
('Pince à élaguer', 7.50, 'a', 'à lannée'),
('Griffe à main', 3.50, 'a', 'à lannée'),
('Gants de jardinage', 6.00, 'a', 'à lannée'),
('Sécateur de jardin', 10.00, 'a', 'à lannée');

-- INSERT INTO flower_arrangement (id,price) VALUES (0,null);
INSERT INTO flower_arrangement (id,price) VALUES (1,15);
INSERT INTO flower_arrangement (id,price) VALUES (2,60);
INSERT INTO flower_arrangement (id,price) VALUES (3,80);
INSERT INTO flower_arrangement (id,price) VALUES (4,25);
INSERT INTO flower_arrangement (id,price) VALUES (5,65);
INSERT INTO flower_arrangement (id,price) VALUES (6,90);
INSERT INTO flower_arrangement (id,price) VALUES (7,20);
INSERT INTO flower_arrangement (id,price) VALUES (8,75);
INSERT INTO flower_arrangement (id,price) VALUES (9,12);
INSERT INTO flower_arrangement (id,price) VALUES (10,30);
INSERT INTO flower_arrangement (id,price) VALUES (11,70);
INSERT INTO flower_arrangement (id,price) VALUES (12,50);
INSERT INTO flower_arrangement (id,price) VALUES (13,85);
INSERT INTO flower_arrangement (id,price) VALUES (14,95);
INSERT INTO flower_arrangement (id,price) VALUES (15,18);
INSERT INTO flower_arrangement (id,price) VALUES (16,45);
INSERT INTO flower_arrangement (id,price) VALUES (17,80);
INSERT INTO flower_arrangement (id,price) VALUES (18,60);
INSERT INTO flower_arrangement (id,price) VALUES (19,35);

INSERT INTO shop (address) VALUES
('Paris'),('Lyon'),('Marseille'),('Lille'),('Bordeaux'),('Toulouse'),('Nice'),('Nantes'),('Strasbourg'),('Grenoble'),( 'Montpellier');


INSERT INTO purchase_order (delivery_address, message, delivery_date, order_date, order_state, client_id, arrangement_id, bouquet_name,shop_id)
VALUES
('10 Rue du Bac, Paris', 'Merci pour tout', '2022-12-01 12:00:00', '2022-11-01 12:00:00', 'CL', 1, null, 'Gros Merci',1),
('28 Rue des Champs, Lyon', 'Joyeux anniversaire', '2022-12-05 12:00:00', '2022-11-05 12:00:00', 'CL', 2, null, 'Le Printanier',2),
('17 Rue de la Paix, Marseille', 'Félicitations', '2022-12-09 12:00:00', '2022-11-09 12:00:00', 'CL', 3, null, 'Fleurs du Sud',3),
('11 Rue de la Gare, Lille', 'Joyeuses fêtes', '2022-12-13 12:00:00', '2022-11-13 12:00:00', 'CL', 4, null, 'Belle Epoque',4),
('9 Rue de la Liberté, Bordeaux', 'Bon rétablissement', '2022-12-17 12:00:00', '2022-11-17 12:00:00', 'CL', 5, null, 'L’Exotique',5),
('13 Rue des Fleurs, Toulouse', 'Félicitations pour le bébé', '2022-12-21 12:00:00', '2022-11-21 12:00:00', 'CL', 6, null, 'Maman',6),
('21 Rue des Lilas, Nice', 'Je t’aime', '2022-12-25 12:00:00', '2022-11-25 12:00:00', 'CL', 7, null, 'L’amoureux',7),
('6 Rue du Marché, Nantes', 'Bon courage', '2022-12-29 12:00:00', '2022-11-29 12:00:00', 'CL', 8, null, 'Le Jardin Anglais',8),
('2 Rue des Écoles, Strasbourg', 'Félicitations pour le diplôme', '2023-01-02 12:00:00', '2022-12-02 12:00:00', 'CL', 9, null, 'Fleurs du Sud',9),
('12 Rue de la Poste, Grenoble', 'Joyeux Noël', '2023-01-05 12:00:00', '2022-12-05 12:00:00', 'CL', 10, null, 'Le Jardin Anglais',10),
('14 Rue du Palais, Montpellier', 'Bonne fête maman', '2023-01-04 12:00:00', '2022-12-04 12:00:00', 'CL', 11, null, 'L’Exotique',11),
('12 Rue du Moulin,Paris', 'Joyeux anniversaire', '2022-06-15 10:00:00', '2022-05-15 10:00:00', 'CL', 1, null, 'Gros Merci',1),
('6 Rue des Fleurs,Lyon', 'Félicitations !', '2022-07-01 12:00:00', '2022-06-01 12:00:00', 'CL', 5, null, 'Vive la mariée',2),
('19 Rue de la Mairie,Marseille', 'Bonne fête maman', '2023-05-10 14:00:00', '2023-04-24 14:00:00', 'CL', 4, null, 'Maman',3),
('14 Rue du Palais,Lille', 'Pour toi mon amour', '2022-06-15 16:00:00', '2022-05-15 16:00:00', 'CL', 10, null, 'L’Exotique',4),
('11 Rue de la Gare,Bordeaux', 'Bonne fête papa', '2022-06-22 10:00:00', '2022-05-22 10:00:00', 'CL', 1, null, 'Gros Merci',5),
('21 Rue des Lilas,Toulouse', 'Bon rétablissement', '2022-07-03 10:00:00', '2022-06-03 10:00:00', 'CL', 8, null, 'Le Jardin Anglais',6),
('28 Rue des Champs, Lyon', 'Joyeux anniversaire', '2023-05-12 12:00:00', '2023-05-02 12:00:00', 'CL', 2, 1,null,2),
('14 Rue du Palais,Lille', 'Pour toi mon amour', '2022-06-15 16:00:00', '2022-05-15 16:00:00', 'CL', 10, 2, null,4),
('11 Rue de la Gare,Bordeaux', 'Bonne fête papa', '2022-06-22 10:00:00', '2022-05-22 10:00:00', 'CL', 1,3, null,5),
('21 Rue des Lilas,Toulouse', 'Bon rétablissement', '2022-07-03 10:00:00', '2022-06-03 10:00:00', 'CL', 8,4, null,6),
('28 Rue des Champs, Lyon', 'Joyeux anniversaire', '2023-05-12 12:00:00', '2023-05-02 12:00:00', 'CL', 2,5, null,2),
('28 Rue des Champs, Lyon', 'Joyeux anniversaire', '2022-12-05 12:00:00', '2022-11-05 12:00:00', 'CL', 2,6, null,2),
('17 Rue de la Paix, Marseille', 'Félicitations', '2022-12-09 12:00:00', '2022-11-09 12:00:00', 'CL', 3,7 ,null,3),
('11 Rue de la Gare, Lille', 'Joyeuses fêtes', '2022-12-13 12:00:00', '2022-11-13 12:00:00', 'CL', 4,8, null,4),
('9 Rue de la Liberté, Bordeaux', 'Bon rétablissement', '2022-12-17 12:00:00', '2022-11-17 12:00:00', 'CL', 5,9, null,5),
('13 Rue des Fleurs, Toulouse', 'Félicitations pour le bébé', '2022-12-21 12:00:00', '2022-11-21 12:00:00', 'CL', 6,10, null,6),
('21 Rue des Lilas, Nice', 'Je t’aime', '2022-12-25 12:00:00', '2022-11-25 12:00:00', 'CL', 7,11, null, 7),
('6 Rue du Marché, Nantes', 'Bon courage', '2022-12-29 12:00:00', '2022-11-29 12:00:00', 'CL', 8,12, null,8);



INSERT INTO arrangement_contains (arrangement_id, item_name, quantity) VALUES
(1, 'Pot en terre cuite', 2),
(1, 'Glaïeul', 1),
(1, 'Rose rouge', 3),
(2, 'Ginger', 2),
(2, 'Pot en terre cuite', 1),
(2, 'Tulipe', 3),
(3, 'Gerbera', 1),
(3, 'Glaïeul', 2),
(3, 'Pot en terre cuite', 3),
(4, 'Ginger', 1),
(4, 'Marguerite', 3),
(4, 'Tulipe', 2),
(5, 'Gerbera', 3),
(5, 'Ginger', 2),
(5, 'Glaïeul', 1),
(6, 'Tulipe', 1),
(6, 'Rose rouge', 3),
(6, 'Pot en terre cuite', 2),
(7, 'Gerbera', 2),
(7, 'Ginger', 1),
(7, 'Glaïeul', 3),
(8, 'Tulipe', 3),
(8, 'Rose rouge', 2),
(8, 'Pot en terre cuite', 1),
(9, 'Gerbera', 1),
(9, 'Ginger', 3),
(9, 'Glaïeul', 2),
(10, 'Tulipe', 2),
(10, 'Rose rouge', 1),
(10, 'Pot en terre cuite', 3),
(11, 'Orchidée', 9),
(11, 'Bouquet de pivoines', 5),
(12, 'Chrysanthème', 3),
(12, 'Oeillet', 7),
(13, 'Lavande', 1),
(13, 'Anthurium', 4),
(14, 'Dahlia', 8),
(14, 'Edelweiss', 2),
(15, 'Freesia', 6),
(15, 'Iris', 9),
(16, 'Lilas', 5),
(16, 'Pensée', 7),
(17, 'Gerbera', 2),
(17, 'Lys', 1),
(18, 'Chrysanthème', 6),
(18, 'Gerbera', 8),
(19, 'Bouquet de pivoines', 4),
(19, 'Chrysanthème', 7);

INSERT INTO is_stored (item_name, shop_id, quantity) VALUES
('Pot en terre cuite', 01, 267),
('Pot en terre cuite', 02, 193),
('Vase en verre', 04, 118),
('Vase en verre', 05, 220),
('Cache-pot en osier', 07, 223),
('Cache-pot en osier', 08, 52),
('Panier en rotin', 10, 275),
('Panier en rotin', 11, 154),
('Tuteur pour plante', 3, 127),
('Tuteur pour plante', 4, 99),
('Arrosoir en métal', 6, 243),
('Arrosoir en métal', 7, 179),
('Pince à élaguer', 9, 54),
('Gerbera', 01, 320),
('Gerbera', 02, 280),
('Glaïeul', 04, 420),
('Glaïeul', 05, 380),
('Rose rouge', 07, 560),
('Rose rouge', 08, 510),
('Ginger', 10, 440),
('Ginger', 11, 390),
('Marguerite', 3, 660),
('Marguerite', 4, 600),
('Tulipe', 6, 580),
('Tulipe', 7, 530),
('Orchidée', 9, 430),
('Bouquet de pivoines', 01, 460),
('Bouquet de pivoines', 02, 410),
('Chrysanthème', 04, 710),
('Chrysanthème', 05, 660),
('Oeillet', 07, 890),
('Oeillet', 08, 840),
('Lavande', 10, 760),
('Lavande', 11, 710),
('Anthurium', 3, 990),
('Anthurium', 4, 940),
('Dahlia', 6, 860),
('Dahlia', 7, 810),
('Edelweiss', 9, 1080),
('Freesia', 01, 1010),
('Freesia', 02, 960),
('Iris', 04, 1220),
('Iris', 05, 1170),
('Lilas', 07, 1340),
('Lilas', 08, 1290),
('Pensée', 10, 1470),
('Pensée', 11, 1420),
('Lys', 3, 1590),
('Lys', 4, 1540);


USE florist;
SELECT * FROM mysql.user;
SELECT * FROM mysql.role_edges;

-- Procedure to check if an email is valid and if it exists in the database
DROP PROCEDURE IF EXISTS check_email;
DELIMITER //
CREATE PROCEDURE check_email(IN email_param VARCHAR(255), OUT is_valid BOOLEAN, OUT email_exists BOOLEAN)
BEGIN
    DECLARE count INT;
    DECLARE pattern VARCHAR(255);
    SET is_valid = 0;
    SET email_exists = 0;
    SET pattern = '^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$';
    
    IF email_param REGEXP pattern THEN
        SET is_valid = 1;
        SELECT COUNT(*) INTO count FROM florist.client WHERE email = email_param;
        
        IF count > 0 THEN
            SET email_exists = 1;
        END IF;
    END IF;
END //
DELIMITER ;
GRANT EXECUTE ON PROCEDURE florist.check_email TO 'florist_user'@'localhost';



-- Procedure to create a new user
DROP PROCEDURE IF EXISTS create_client;
DELIMITER //
CREATE PROCEDURE IF NOT EXISTS create_client(
    IN email_param VARCHAR(320),
    IN password_param VARCHAR(255),
    IN first_name_param VARCHAR(50),
    IN name_param VARCHAR(50),
    IN phone_number_param VARCHAR(20),
    IN address_param VARCHAR(100),
    IN credit_card_param VARCHAR(19),
    OUT success BOOLEAN
)
BEGIN
    DECLARE is_valid BOOLEAN;
    DECLARE email_exists BOOLEAN;

    SET success = 0;
    
    -- Check if the email is valid and if it exists
    CALL check_email(email_param, is_valid, email_exists);

    -- Check if length of password is at least 8 characters
    IF CHAR_LENGTH(password_param) < 8 THEN
        SET is_valid = 0;
    END IF;
    
    
    IF is_valid = 1 THEN
        IF email_exists = 0 THEN
            -- Proceed with creating the user

            INSERT INTO Client (first_name, name, email, phone_number, address, credit_card, inscription_date)
            VALUES (first_name_param, name_param, email_param, phone_number_param, address_param, credit_card_param, NOW());
            
            SET @create_user_sql = CONCAT('CREATE USER \'', email_param, '\'@\'localhost\' IDENTIFIED BY \'', password_param, '\'');
            PREPARE create_user_stmt FROM @create_user_sql;
            EXECUTE create_user_stmt;
            DEALLOCATE PREPARE create_user_stmt;

            SET @grant_sql = CONCAT('GRANT florist_client TO \'', email_param, '\'@\'localhost\'');
            PREPARE stmt FROM @grant_sql;
            EXECUTE stmt;
            DEALLOCATE PREPARE stmt;

            SET @set_default_sql = CONCAT('SET DEFAULT ROLE florist_client TO \'', email_param, '\'@\'localhost\'');
            PREPARE stmt FROM @set_default_sql;
            EXECUTE stmt;
            DEALLOCATE PREPARE stmt;

            SET @grant_all_procedures_sql = CONCAT('GRANT EXECUTE ON florist.* TO \'', email_param, '\'@\'localhost\'');
            PREPARE stmt FROM @grant_all_procedures_sql;
            EXECUTE stmt;
            DEALLOCATE PREPARE stmt;

            SET success = 1;
            SELECT 'User created' AS result;
        ELSE
            SELECT 'User with this email already exists' AS result;
        END IF;
    ELSE
        SELECT 'Invalid informations' AS result;
    END IF;
END //
DELIMITER ;
GRANT EXECUTE ON PROCEDURE florist.create_client TO 'florist_user'@'localhost';



-- Procedure to create a new purchase order for a standard bouquet
DROP PROCEDURE IF EXISTS order_standard_bouquet;
DELIMITER //
CREATE PROCEDURE IF NOT EXISTS order_standard_bouquet(
    IN delivery_address_param VARCHAR(50),
    IN message_param VARCHAR(50),
    IN delivery_date_param DATETIME,
    IN bouquet_name_param VARCHAR(50),
    IN shop_id_param INT,
    OUT success BOOLEAN
)
BEGIN
    DECLARE bouquet_exists BOOLEAN;
    DECLARE client_exists BOOLEAN;

    DECLARE client_id_param INT;
    DECLARE threeDaysLater DATETIME;
    DECLARE orderState VARCHAR(4);

    SET success = 0;

    -- Check if the bouquet name exists in the standard_bouquet table
    SELECT COUNT(*) INTO bouquet_exists FROM standard_bouquet WHERE name = bouquet_name_param;

    -- Find the client ID based on the username
    SELECT id INTO client_id_param FROM client WHERE CONCAT(email, "@localhost") = USER();

    
    SET threeDaysLater = NOW() + INTERVAL 3 DAY;

    IF delivery_date_param <= threeDaysLater THEN
        SET orderState = 'VINV';
    ELSE
        SET orderState = 'CC';
    END IF;

    IF bouquet_exists = 1 AND client_id_param IS NOT NULL THEN
        -- Insert the new purchase order
        INSERT INTO purchase_order (delivery_address, message, delivery_date, order_date, order_state, client_id, bouquet_name, shop_id)
        VALUES (delivery_address_param, message_param, delivery_date_param, NOW(), orderState, client_id_param, bouquet_name_param, shop_id_param);

        SET success = 1;
        SELECT 'Purchase order created' AS result;
    ELSE
        IF bouquet_exists = 0 THEN
            SELECT 'Invalid bouquet name' AS result;
        ELSE
            SELECT 'Invalid client username' AS result;
        END IF;
    END IF;
END //
DELIMITER ;
GRANT EXECUTE ON PROCEDURE florist.order_standard_bouquet TO florist_client;

SELECT * FROM florist.purchase_order;


-- Procedure to check if an email is valid and if it exists in the database
DROP PROCEDURE IF EXISTS get_client_id;
DELIMITER //
CREATE PROCEDURE IF NOT EXISTS get_client_id(OUT client_id INT)
BEGIN

    SELECT id INTO client_id
    FROM client
    WHERE CONCAT(email, "@localhost") = USER();

END //
DELIMITER ;
GRANT EXECUTE ON PROCEDURE florist.get_client_id TO florist_client;

DROP PROCEDURE IF EXISTS order_flower_arrangement;
CREATE PROCEDURE order_flower_arrangement
(
    IN item_dict_param TEXT,
    IN delivery_address_param VARCHAR(100),
    IN message_param VARCHAR(200),
    IN delivery_date_param DATETIME,
    IN shop_id_param INT,
    IN price_param INT,
    OUT success BOOLEAN
)
BEGIN
    DECLARE i INT DEFAULT 0;
    DECLARE item_id VARCHAR(50);
    DECLARE quantity INT;
    DECLARE client_id_param INT;
    DECLARE client_exists BOOLEAN;
    DECLARE new_arrangement_id INT;
    SET success = 0;

    -- Find the client ID based on the username
    SELECT id INTO client_id_param FROM client WHERE CONCAT(email, "@localhost") = USER();

    
    -- Create the arrangement
    INSERT INTO flower_arrangement (price) VALUES (price_param);
  
    SET new_arrangement_id = LAST_INSERT_ID();


    IF client_id_param IS NOT NULL THEN
        -- Insert the new purchase order
        INSERT INTO purchase_order (delivery_address, message, delivery_date, order_date, order_state, client_id, arrangement_id, shop_id)
        VALUES (delivery_address_param, message_param, delivery_date_param, NOW(), "CPAV", client_id_param, new_arrangement_id, shop_id_param);

        SET success = 1;
        SELECT 'Purchase order created' AS result;
    ELSE
        IF client_id_param IS NULL THEN
            SELECT 'Invalid client username' AS result;
        END IF;
    END IF;

    SET i = 0;
    WHILE i < JSON_LENGTH(item_dict_param) DO
        SET item_id = JSON_EXTRACT(item_dict_param, CONCAT('$[', i, '].name'));
        SET item_id = SUBSTRING(item_id, 2, CHAR_LENGTH(item_id) - 2);
        SET quantity = JSON_EXTRACT(item_dict_param, CONCAT('$[', i, '].value'));


        INSERT INTO arrangement_contains (arrangement_id, item_name, quantity)
        VALUES (new_arrangement_id, item_id, quantity);
        SET i = i + 1;
    END WHILE;
END