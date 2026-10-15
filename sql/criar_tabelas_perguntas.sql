CREATE TABLE perguntas (
	id INT PRIMARY KEY,
	pergunta text NOT NULL,
	resposta VARCHAR (20) NOT NULL,
	video date NOT NULL,
	tema VARCHAR(100),
	origem VARCHAR(100) NOT NULL,
	relacionadaId INT,
	
	CONSTRAINT fk_perguntas_relacionadaId
	FOREIGN KEY (relacionadaId)
	REFERENCES perguntas(id)
    ON DELETE RESTRICT
) ENGINE=InnoDB 
DEFAULT CHARSET=utf8mb4 
COLLATE=utf8mb4_unicode_ci;

/*relacionadaId é a pergunta mãe, quando existir*/