DROP VIEW IF EXISTS shoping_list;
CREATE VIEW shoping_list AS
	SELECT u.id AS user_id, oi.appliance_EAN, oi.amount 
					FROM ((SELECT id FROM USER WHERE Enabled = 1) u 
					INNER JOIN (SELECT id, user_id FROM ORDERS WHERE active = 1) o 
						ON u.id = o.user_id) 
                    INNER JOIN ORDER_ITEMS oi ON oi.order_id = o.id;	
                    
DROP VIEW IF EXISTS avaliable_devices;
CREATE VIEW avaliable_devices AS
SELECT EAN, sum(x.amount) AS amount
	FROM	
    (SELECT oi.appliance_EAN AS EAN, 
		CASE WHEN o.supply = 1 THEN oi.amount
        else -oi.amount
        end AS amount
		FROM ((SELECT id, supply FROM ORDERS) o 
        INNER JOIN ORDER_ITEMS oi ON oi.order_id = o.id)) x
        GROUP BY EAN;
        
DROP VIEW IF EXISTS avaliable_devices_full;
CREATE VIEW avaliable_devices_full AS
SELECT ad.EAN, ad.amount, a.name AS title, a.price, t.name AS trademark, ac.name AS category, a.guaranty_time 
	FROM avaliable_devices ad	INNER JOIN APPLIANCE a ON ad.EAN = a.EAN
								INNER JOIN TRADEMARK t ON t.id = a.trademark_id
								INNER JOIN APPLIANCES_CATEGORY ac ON ac.id = a.category_id;
    
DROP VIEW IF EXISTS users;
CREATE VIEW users AS
	SELECT u.id, u.login, u.email, u.hashed_password, u.Enabled, u.phone_number, u.creation_time, u.last_active_time, r.name AS Role FROM
		USER u INNER JOIN ROLE r ON u.role_id = r.id;
        
DROP VIEW IF EXISTS appliances_list;
CREATE VIEW appliances_list AS
	SELECT a.EAN, a.name AS title, a.price, t.name AS trademark, ac.name AS category, a.guaranty_time 
		FROM APPLIANCE a INNER JOIN TRADEMARK t ON a.trademark_id = t.id
        INNER JOIN APPLIANCES_CATEGORY ac ON a.category_id = ac.id;

DROP VIEW IF EXISTS role_rights_list;
CREATE VIEW role_rights_list AS
	SELECT r.name AS role_name, ri.title AS right_name
		FROM ROLE r JOIN ROLE_RIGHTS rri ON r.id = rri.role_id
			JOIN RIGHTS ri on rri.right_id = ri.id;