CREATE DATABASE kunskaps_prov;
GO

USE kunskaps_prov;
GO


CREATE TABLE users (
    id INT PRIMARY KEY IDENTITY,
    first_name VARCHAR(255),
    last_name VARCHAR(255),
    email VARCHAR(255) NOT NULL,
    address VARCHAR(255),
    user_registered_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP

);
GO

INSERT INTO users (first_name, last_name, email, address)
VALUES
    ('Bilbo', 'Baggins', 'email.address@home.dk', 'Snövägen 7'),
    ('Oskar', 'Älg', 'coool.sten@yahoo.com', 'Storgatan 45b'),
    ('Monica', 'Tomte', 'sorg.bengalkatt123@away.se', 'Skogsdalen -4');
GO
