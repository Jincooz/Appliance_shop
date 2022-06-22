DROP SCHEMA IF EXISTS Online_shop;
CREATE SCHEMA IF NOT EXISTS Online_shop 
	DEFAULT CHARACTER SET utf8 
	COLLATE utf8_unicode_ci;
USE Online_shop;

DROP TABLE IF EXISTS ROLE;
CREATE TABLE IF NOT EXISTS ROLE(
  id BIGINT UNSIGNED NOT NULL AUTO_INCREMENT,
  name VARCHAR(255) NOT NULL,
  CONSTRAINT pk_role PRIMARY KEY (id)
) ENGINE = InnoDB;

ALTER TABLE ROLE ADD CONSTRAINT
	uc_name UNIQUE(name);

DROP TABLE IF EXISTS ROLE_RIGHTS;
CREATE TABLE IF NOT EXISTS ROLE_RIGHTS(
	role_id BIGINT UNSIGNED NOT NULL,
    right_id BIGINT UNSIGNED NOT NULL,    
  CONSTRAINT pk_role_rights PRIMARY KEY (role_id, right_id)
) ENGINE = InnoDB;

DROP TABLE IF EXISTS RIGHTS;
CREATE TABLE IF NOT  EXISTS RIGHTS(
	id BIGINT UNSIGNED NOT NULL AUTO_INCREMENT,
    title VARCHAR(255) NOT NULL,
    CONSTRAINT pk_rights PRIMARY KEY (id)
) ENGINE = InnoDB;

ALTER TABLE RIGHTS ADD CONSTRAINT
	uc_title UNIQUE(title);

DROP TABLE IF EXISTS USER;
CREATE TABLE IF NOT EXISTS USER(
  id BIGINT UNSIGNED NOT NULL AUTO_INCREMENT,
  login VARCHAR(255) NOT NULL,
  email VARCHAR(255) NOT NULL,
  hashed_password CHAR(48) NOT NULL,
  Enabled TINYINT(1) NOT NULL DEFAULT 1,
  phone_number CHAR(10) NULL,
  last_active_time TIMESTAMP NOT NULL DEFAULT NOW(),
  creation_time TIMESTAMP NOT NULL DEFAULT NOW(),
  role_id BIGINT UNSIGNED NOT NULL,
  CONSTRAINT pk_user PRIMARY KEY (id)
);

ALTER TABLE USER ADD CONSTRAINT
	uc_login UNIQUE(login);

ALTER TABLE USER ADD CONSTRAINT
	uc_email UNIQUE(email);
    
ALTER TABLE USER ADD CONSTRAINT
	uc_phone_number UNIQUE(phone_number);
    
DROP TABLE IF EXISTS TRADEMARK;
CREATE TABLE IF NOT EXISTS TRADEMARK(
  id BIGINT UNSIGNED NOT NULL AUTO_INCREMENT,
  name VARCHAR(255) NOT NULL,
  CONSTRAINT pk_trademark PRIMARY KEY (id)
) ENGINE = InnoDB;

ALTER TABLE TRADEMARK ADD CONSTRAINT
	uc_name UNIQUE(name);

DROP TABLE IF EXISTS APPLIANCES_CATEGORY;
CREATE TABLE IF NOT EXISTS APPLIANCES_CATEGORY(
  id BIGINT UNSIGNED NOT NULL AUTO_INCREMENT,
  name VARCHAR(255) NOT NULL,
  CONSTRAINT pk_appliances_category PRIMARY KEY (id)
) ENGINE = InnoDB;

ALTER TABLE APPLIANCES_CATEGORY ADD CONSTRAINT
	uc_name UNIQUE(name);

DROP TABLE IF EXISTS APPLIANCE;
CREATE TABLE IF NOT EXISTS APPLIANCE(
  EAN CHAR(13) NOT NULL,
  name VARCHAR(255) NOT NULL,
  price DECIMAL NOT NULL,
  category_id BIGINT UNSIGNED NOT NULL,
  trademark_id BIGINT UNSIGNED NOT NULL,
  guaranty_time BIGINT NULL,
  CONSTRAINT pk_appliance PRIMARY KEY (EAN)
) ENGINE = InnoDB;

ALTER TABLE APPLIANCE ADD CONSTRAINT
	uc_name UNIQUE(name);

DROP TABLE IF EXISTS ORDERS;
CREATE TABLE IF NOT EXISTS ORDERS(
  id BIGINT UNSIGNED NOT NULL AUTO_INCREMENT,
  active TINYINT(1) NOT NULL DEFAULT 0,
  creating_moment TIMESTAMP NULL,
  last_update_time TIMESTAMP NULL,
  supply TINYINT(1) NOT NULL DEFAULT 1,
  user_id BIGINT UNSIGNED NOT NULL,
  CONSTRAINT pk_orders PRIMARY KEY (id)
) ENGINE = InnoDB;

DROP TABLE IF EXISTS TRANSACTIONS;
CREATE TABLE IF NOT EXISTS TRANSACTIONS(
  id BIGINT UNSIGNED NOT NULL AUTO_INCREMENT,
  debit SMALLINT(4) UNSIGNED NOT NULL,
  credit SMALLINT(4) UNSIGNED NOT NULL,
  description VARCHAR(255) NULL,
  sum DECIMAL(11,2) UNSIGNED NOT NULL,
  order_id BIGINT UNSIGNED NULL,
  time TIMESTAMP NULL,
  CONSTRAINT pk_transactions PRIMARY KEY (id)
) ENGINE = InnoDB;

DROP TABLE IF EXISTS CHART_OF_ACCOUNTS;
CREATE TABLE IF NOT EXISTS CHART_OF_ACCOUNTS(
	id SMALLINT(4) UNSIGNED NOT NULL,
    description VARCHAR(255) NULL,
    CONSTRAINT pk_chart_of_accounts PRIMARY KEY(id)
) ENGINE = InnoDB;

DROP TABLE IF EXISTS ORDER_ITEMS;
CREATE TABLE IF NOT EXISTS ORDER_ITEMS(
  order_id BIGINT UNSIGNED NOT NULL,
  appliance_EAN CHAR(13) NOT NULL,
  amount BIGINT UNSIGNED NOT NULL,
  CONSTRAINT pk_order_items PRIMARY KEY (order_id, appliance_EAN)
) ENGINE = InnoDB;

ALTER TABLE USER ADD CONSTRAINT
	fk_user_role FOREIGN KEY (role_id)
    REFERENCES ROLE(id);
    
ALTER TABLE ROLE_RIGHTS ADD CONSTRAINT
	fk_role_rights_role FOREIGN KEY (role_id)
    REFERENCES ROLE(id);
    
ALTER TABLE ROLE_RIGHTS ADD CONSTRAINT
	fk_role_rights_right FOREIGN KEY (right_id)
    REFERENCES RIGHTS(id);

ALTER TABLE APPLIANCE ADD CONSTRAINT
	fk_appliance_trademark FOREIGN KEY (trademark_id)
    REFERENCES TRADEMARK(id);

ALTER TABLE APPLIANCE ADD CONSTRAINT
	fk_appliance_category FOREIGN KEY (category_id)
    REFERENCES APPLIANCES_CATEGORY(id);

ALTER TABLE ORDER_ITEMS ADD CONSTRAINT 
	fk_order_item_orders FOREIGN KEY (order_id)
    REFERENCES ORDERS(id);
    
ALTER TABLE ORDER_ITEMS ADD CONSTRAINT 
	fk_order_item_appliance FOREIGN KEY (appliance_EAN)
    REFERENCES APPLIANCE(EAN);

ALTER TABLE ORDERS ADD CONSTRAINT
	fk_order_user FOREIGN KEY (user_id)
    REFERENCES USER(id);

ALTER TABLE TRANSACTIONS ADD CONSTRAINT
	fk_debit FOREIGN KEY(debit)
    REFERENCES CHART_OF_ACCOUNTS(id);

ALTER TABLE TRANSACTIONS ADD CONSTRAINT
	fk_credit FOREIGN KEY(credit)
    REFERENCES CHART_OF_ACCOUNTS(id);