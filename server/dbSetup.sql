CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name VARCHAR(255) COMMENT 'User Name',
  email VARCHAR(255) COMMENT 'User Email',
  picture VARCHAR(255) COMMENT 'User Picture'
) default charset utf8mb4 COMMENT '';


CREATE TABLE IF NOT EXISTS todos(
  id INT  NOT NULL AUTO_INCREMENT PRIMARY KEY COMMENT 'Primary Key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  title VARCHAR(255) NOT NULL COMMENT 'Title',
  description VARCHAR(500) COMMENT 'Task Description',
  completed BOOLEAN DEFAULT FALSE COMMENT 'Task Status',
  creatorId VARCHAR(255) NOT NULL COMMENT 'Creator Id',
  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8mb4 COMMENT '';

DROP TABLE IF EXISTS
todos,
accounts;

INSERT INTO accounts
  (id, name, email, picture)
VALUES
  ('1', 'AJ V', 'wanderwithme1899@gmail.com', 'https://media.licdn.com/media/AAYQAQSOAAgAAQAAAAAAAB-zrMZEDXI2T62PSuT6kpB6qg.png');

INSERT INTO todos
  (title, description, completed, creatorId)
VALUES
  ('Practice SQL Queries', 'Learn how to use SQL to interact with databases', false, '1'),
  ('Learn how to use JS w/ SQL', 'Learn how to use JS to interact with SQL databases', false, '1'),
  ('Learn how to use Retool', 'Learn how to use Retool\'s UI Dashboard using JS and SQL to create applications and internal tools within them.', false, '1'),
  ('Learn how to use Snowflake', 'Learn how to use Snowflake\'s cloud data platform to store and analyze data.', false, '1'),
  ('Learn how to utilize Snowflake, Retool, and JS', 'Learn how to use Snowflake and use SQL to query data tables within Retool, and JS to interact with the UI.', false, '1');