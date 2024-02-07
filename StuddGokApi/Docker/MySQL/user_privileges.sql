# create user
CREATE USER IF NOT EXISTS 'ga-app'@'localhost' IDENTIFIED BY 'ga-5ecret-%';
CREATE USER IF NOT EXISTS 'ga-app'@'%' IDENTIFIED BY 'ga-5ecret-%';
 
GRANT ALL privileges ON studd_gok_api.* TO 'ga-app'@'%';
GRANT ALL privileges ON studd_gok_api.* TO 'ga-app'@'localhost';

FLUSH PRIVILEGES