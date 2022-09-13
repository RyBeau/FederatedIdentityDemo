CREATE DATABASE IF NOT EXISTS AUTH_DEMO;

CREATE TABLE roles (
    id int AUTO_INCREMENT,
    name VARCHAR(50) NOT NULL,
    PRIMARY KEY (id)
);

CREATE TABLE users (
    id int AUTO_INCREMENT,
    username VARCHAR(50) NOT NULL,
    password VARCHAR(100) NOT NULL,
    role_id INT NOT NULL,
    PRIMARY KEY (id),
    FOREIGN KEY (role_id) REFERENCES roles(id)
);

INSERT INTO roles (name)
VALUES ('Admin');
INSERT INTO roles (name)
VALUES ('Developer');
INSERT INTO roles (name)
VALUES ('Basic');

INSERT INTO users (username, password, role_id)
VALUES ("ryan.beaumont", SHA2('ryanspassword', 256), 1);

INSERT INTO users (username, password, role_id)
VALUES ("mr.dev", SHA2('devspassword', 256), 2);

INSERT INTO users (username, password, role_id)
VALUES ("mr.basic", SHA2('basicspassword', 256), 3);