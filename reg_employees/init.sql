USE cad_funcionarios;

CREATE TABLE IF NOT EXISTS login 
(
    idLogin INT PRIMARY KEY AUTO_INCREMENT,
    login VARCHAR(50) NOT NULL UNIQUE,
    senha VARCHAR(255) NOT NULL -- armazena o hash da senha
);

CREATE TABLE IF NOT EXISTS funcionarios 
(
    idFunc INT PRIMARY KEY AUTO_INCREMENT,
    nomeFunc VARCHAR(100) NOT NULL,
    cpf VARCHAR(11) NOT NULL UNIQUE, -- Limite de caracteres para CPF
    cargo VARCHAR(50) NOT NULL,
    salario FLOAT NOT NULL,
    email VARCHAR(255) UNIQUE,
    idlogin INT,
    FOREIGN KEY (idlogin) REFERENCES login (idLogin)
);