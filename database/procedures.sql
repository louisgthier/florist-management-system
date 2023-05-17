
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