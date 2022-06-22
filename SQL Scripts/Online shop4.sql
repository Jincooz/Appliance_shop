INSERT INTO `online_shop`.`role` (`name`) VALUES ('guest');
INSERT INTO `online_shop`.`role` (`name`) VALUES ('user');
INSERT INTO `online_shop`.`role` (`name`) VALUES ('admin');

INSERT INTO `online_shop`.`chart_of_accounts` (`id`) VALUES ('46');
INSERT INTO `online_shop`.`chart_of_accounts` (`id`) VALUES ('281');
INSERT INTO `online_shop`.`chart_of_accounts` (`id`) VALUES ('311');
INSERT INTO `online_shop`.`chart_of_accounts` (`id`) VALUES ('361');
INSERT INTO `online_shop`.`chart_of_accounts` (`id`) VALUES ('401');
INSERT INTO `online_shop`.`chart_of_accounts` (`id`) VALUES ('631');
INSERT INTO `online_shop`.`chart_of_accounts` (`id`) VALUES ('702');
INSERT INTO `online_shop`.`chart_of_accounts` (`id`) VALUES ('791');
INSERT INTO `online_shop`.`chart_of_accounts` (`id`) VALUES ('902');

INSERT INTO `online_shop`.`transactions` (`debit`, `credit`, `description`, `sum`, `order_id`, `time`) VALUES ('46', '401', 'Create shop', '1000000', NULL, NOW());
INSERT INTO `online_shop`.`transactions` (`debit`, `credit`, `description`, `sum`, `order_id`, `time`) VALUES ('311', '46', 'Contributed capital to the bank', '1000000', NULL, NOW());

INSERT INTO `online_shop`.`rights` (`title`) VALUES ('viewappliance');
INSERT INTO `online_shop`.`rights` (`title`) VALUES ('login');
INSERT INTO `online_shop`.`rights` (`title`) VALUES ('register');
INSERT INTO `online_shop`.`rights` (`title`) VALUES ('haveshopinglist');
INSERT INTO `online_shop`.`rights` (`title`) VALUES ('changeprifile');
INSERT INTO `online_shop`.`rights` (`title`) VALUES ('logout');
INSERT INTO `online_shop`.`rights` (`title`) VALUES ('viewfinancialinfo');
INSERT INTO `online_shop`.`rights` (`title`) VALUES ('viewusers');
INSERT INTO `online_shop`.`rights` (`title`) VALUES ('addsuply');
INSERT INTO `online_shop`.`rights` (`title`) VALUES ('clickrowheader');


INSERT INTO `online_shop`.`role_rights` (`role_id`, `right_id`) VALUES ('1', '1');
INSERT INTO `online_shop`.`role_rights` (`role_id`, `right_id`) VALUES ('2', '1');
INSERT INTO `online_shop`.`role_rights` (`role_id`, `right_id`) VALUES ('3', '1');
INSERT INTO `online_shop`.`role_rights` (`role_id`, `right_id`) VALUES ('1', '2');
INSERT INTO `online_shop`.`role_rights` (`role_id`, `right_id`) VALUES ('1', '3');
INSERT INTO `online_shop`.`role_rights` (`role_id`, `right_id`) VALUES ('2', '4');
INSERT INTO `online_shop`.`role_rights` (`role_id`, `right_id`) VALUES ('3', '4');
INSERT INTO `online_shop`.`role_rights` (`role_id`, `right_id`) VALUES ('2', '5');
INSERT INTO `online_shop`.`role_rights` (`role_id`, `right_id`) VALUES ('3', '5');
INSERT INTO `online_shop`.`role_rights` (`role_id`, `right_id`) VALUES ('2', '6');
INSERT INTO `online_shop`.`role_rights` (`role_id`, `right_id`) VALUES ('3', '6');
INSERT INTO `online_shop`.`role_rights` (`role_id`, `right_id`) VALUES ('3', '7');
INSERT INTO `online_shop`.`role_rights` (`role_id`, `right_id`) VALUES ('3', '8');
INSERT INTO `online_shop`.`role_rights` (`role_id`, `right_id`) VALUES ('3', '9');
INSERT INTO `online_shop`.`role_rights` (`role_id`, `right_id`) VALUES ('2', '10');
INSERT INTO `online_shop`.`role_rights` (`role_id`, `right_id`) VALUES ('3', '10');
