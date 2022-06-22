SET autocommit=0;
DROP PROCEDURE IF EXISTS ADD_ROLE;
DELIMITER |
	CREATE PROCEDURE ADD_ROLE(IN name VARCHAR(255))
	BEGIN
		INSERT INTO ROLE(name) VALUES (lower(name));
	END;|
DELIMITER ;
#USER
DROP PROCEDURE IF EXISTS ADD_USER;
DROP PROCEDURE IF EXISTS UPDATE_LAST_SEEN;
DROP PROCEDURE IF EXISTS UPDATE_USER_ROLE;
DROP PROCEDURE IF EXISTS UPDATE_USER_DATA;
DROP PROCEDURE IF EXISTS UPDATE_PASSWORD;
DROP PROCEDURE IF EXISTS DISSABLE_USER;
DROP PROCEDURE IF EXISTS ENABLE_USER;
DELIMITER |
    CREATE PROCEDURE ADD_USER(	IN login VARCHAR(255),
								IN email VARCHAR(255),
								IN hashed_password CHAR(48),
								IN phone_number CHAR(10),
                                IN role_name VARCHAR(255))
	BEGIN
		INSERT INTO USER (`login`, `email`, `hashed_password`, `role_id`, `phone_number`, `creation_time`) 
			SELECT login, email, hashed_password, ROLE.id, phone_number, NOW()
			FROM ROLE
			WHERE ROLE.name = lower(role_name);
	END;|
    CREATE PROCEDURE UPDATE_LAST_SEEN(IN id BIGINT UNSIGNED)
    BEGIN
		UPDATE USER SET 
			USER.last_active_time = NOW() 
            WHERE USER.id = id;
    END;|
    CREATE PROCEDURE UPDATE_USER_ROLE(IN id BIGINT UNSIGNED, IN role_name VARCHAR(255))
    BEGIN
		UPDATE USER, ROLE SET 
			USER.role_id = ROLE.id 
            WHERE USER.id = id AND ROLE.name = role_name;
    END;|
    CREATE PROCEDURE UPDATE_USER_DATA(	IN id BIGINT UNSIGNED,
										IN login VARCHAR(255),
										IN email VARCHAR(255),
										IN phone_number CHAR(10))
    BEGIN
		UPDATE USER SET 
			USER.login = login,
            USER.email = email,
            USER.hashed_password = hashed_password,
            USER.phone_number = phone_number
            WHERE USER.id = id;
    END;|
    CREATE PROCEDURE UPDATE_PASSWORD(IN new_hashed_password CHAR(48), IN id BIGINT UNSIGNED)
    BEGIN
		UPDATE USER SET
			USER.hashed_password = new_hashed_password
            WHERE USER.id = id;
    END;|
    CREATE PROCEDURE DISSABLE_USER (IN id BIGINT UNSIGNED)
    BEGIN
		UPDATE USER SET USER.Enabled = 0 WHERE USER.id = id;
    END;|
    CREATE PROCEDURE ENABLE_USER (IN id BIGINT UNSIGNED)
    BEGIN
		UPDATE USER SET USER.Enabled = 1 WHERE USER.id = id;
    END;|
DELIMITER ;
#TRADEMARK
DROP PROCEDURE IF EXISTS ADD_TRADEMARK;
DELIMITER |
	CREATE PROCEDURE ADD_TRADEMARK(IN name VARCHAR(255))
	BEGIN
		INSERT INTO TRADEMARK(name) VALUES (lower(name));
	END;|
DELIMITER ;
#APPLIANCES_CATEGORY
DROP PROCEDURE IF EXISTS ADD_APPLIANCE_CATEGORY;
DELIMITER |
	CREATE PROCEDURE ADD_APPLIANCE_CATEGORY(IN name VARCHAR(255))
	BEGIN
        INSERT INTO APPLIANCES_CATEGORY(name) VALUES (lower(name));
	END;|
DELIMITER ;
#APPLIANCE
DROP PROCEDURE IF EXISTS ADD_APPLIANCE;
DELIMITER |
	CREATE PROCEDURE ADD_APPLIANCE(EAN CHAR(13),
									name VARCHAR(255),
									price DECIMAL(11,2),
									category_name VARCHAR(255),
									trademark_name VARCHAR(255),
									guaranty_days BIGINT UNSIGNED)
	BEGIN
			INSERT INTO APPLIANCE(EAN, name, price, category_id,trademark_id,guaranty_time)
				SELECT EAN, name, price, ac.id,t.id,guaranty_days
                FROM APPLIANCES_CATEGORY ac JOIN TRADEMARK t
                WHERE category_name = ac.name AND t.name = trademark_name;
    END;|
DELIMITER ;
#ORDERS
DROP PROCEDURE IF EXISTS ADD_USER_ORDER;
DROP PROCEDURE IF EXISTS ADD_SUPPLY_ORDER;
DROP PROCEDURE IF EXISTS CLOSE_USER_ORDER;
DROP PROCEDURE IF EXISTS CLOSE_SUPPLY_ORDER;
DELIMITER |
	CREATE PROCEDURE ADD_USER_ORDER(user_id BIGINT UNSIGNED)
	BEGIN
			INSERT INTO ORDERS(active,creating_moment,last_update_time,supply,user_id)
				VALUES (1,NOW(),NOW(),0,user_id);
    END;|
	CREATE PROCEDURE ADD_SUPPLY_ORDER(user_id BIGINT UNSIGNED)
	BEGIN
			INSERT INTO ORDERS(active,creating_moment,last_update_time,supply,user_id)
				VALUES (1,NOW(),NOW(),1,user_id);
    END;|
    CREATE PROCEDURE CLOSE_USER_ORDER(user_id BIGINT UNSIGNED)
	BEGIN
		UPDATE ORDERS o
			SET 
			o.active = 0,
			o.last_update_time = NOW()
			WHERE o.user_id = user_id AND o.supply = 0;
    END;|
    CREATE PROCEDURE CLOSE_SUPPLY_ORDER(user_id BIGINT UNSIGNED)
	BEGIN
		UPDATE ORDERS o
			SET 
			o.active = 0,
			o.last_update_time = NOW()
			WHERE o.user_id = user_id  AND o.active = 1;
    END;|
DELIMITER ;
DROP PROCEDURE IF EXISTS ADD_ORDER_ITEM;
DROP PROCEDURE IF EXISTS UPDATE_ORDER_ITEM;
DROP PROCEDURE IF EXISTS DELETE_ORDER_ITEM;
DELIMITER |
	CREATE PROCEDURE ADD_ORDER_ITEM(EAN CHAR(13),
									user_id BIGINT UNSIGNED,
									amount BIGINT UNSIGNED)
	BEGIN
			INSERT INTO ORDER_ITEMS(order_id,appliance_EAN,amount)
				SELECT o.id,EAN,amount
                FROM ORDERS o
                WHERE o.user_id = user_id AND active = 1;
    END;|
	CREATE PROCEDURE UPDATE_ORDER_ITEM(EAN CHAR(13),
									user_id BIGINT UNSIGNED,
									new_amount BIGINT UNSIGNED)
	BEGIN
			UPDATE  ORDER_ITEMS oi
				SET oi.amount = new_amount
				WHERE (oi.appliance_EAN,oi.order_id) =(EAN,(SELECT id FROM ORDERS o WHERE o.user_id =user_id AND active = 1));
    END;|
	CREATE PROCEDURE DELETE_ORDER_ITEM(EAN CHAR(13),
									user_id BIGINT UNSIGNED)
	BEGIN
			DELETE FROM ORDER_ITEMS oi
				WHERE (oi.appliance_EAN,oi.order_id) = (EAN,(SELECT id FROM ORDERS o WHERE o.user_id =user_id AND active = 1));
    END;|
DELIMITER ;
DROP PROCEDURE IF EXISTS ADD_TRANSACTION;
DELIMITER |
	CREATE PROCEDURE ADD_TRANSACTION(	IN debit SMALLINT UNSIGNED,
							IN credit SMALLINT UNSIGNED,
                            IN description VARCHAR(255),
                            IN user_id SMALLINT UNSIGNED,
                            IN sum DECIMAL(11,2))
	BEGIN
        INSERT INTO TRANSACTIONS(debit, credit, description, order_id, time, sum)
			SELECT debit, credit, description, o.id, NOW(), sum
            FROM ORDERS o
            WHERE o.user_id = user_id AND o.active = 1;
	END;|
DELIMITER ;
DROP FUNCTION IF EXISTS CALC_CREDIT;
DROP FUNCTION IF EXISTS CALC_DEBIT;
DELIMITER |
	CREATE FUNCTION CALC_CREDIT(chart_id VARCHAR(255))
    RETURNS DECIMAL(11,2) READS SQL DATA
    BEGIN
        RETURN (SELECT sum(sum) FROM transactions WHERE credit = chart_id); 
    END;|
	CREATE FUNCTION CALC_DEBIT(chart_id VARCHAR(255))
    RETURNS DECIMAL(11,2) READS SQL DATA
    BEGIN
        RETURN (SELECT sum(sum) FROM transactions WHERE debit = chart_id); 
    END;|
DELIMITER ;













DROP PROCEDURE IF EXISTS buy;
DELIMITER |
CREATE PROCEDURE buy(IN user_id BIGINT UNSIGNED, IN sum DECIMAL(11, 2))
BEGIN
	START TRANSACTION;
		CALL ADD_TRANSACTION('361','702','User buy applianse',user_id,sum);
		CALL ADD_TRANSACTION('311','361','User pay out',user_id,sum);
		CALL ADD_TRANSACTION('902','281','Write off appliances from the warehouse',user_id,sum*0.8);
		CALL ADD_TRANSACTION('702','791','Write-off of income on the financial result',user_id,sum);
		CALL ADD_TRANSACTION('791','902','Write-off of value for financial result',user_id,sum*0.8);
		CALL CLOSE_USER_ORDER(user_id);
	IF (SELECT (SELECT COUNT(amount) FROM online_shop.avaliable_devices WHERE amount < 0) = 0)
		THEN COMMIT;
		ELSE ROLLBACK;
	END IF;
END|
DELIMITER ;