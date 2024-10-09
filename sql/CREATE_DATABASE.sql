-- CREATE DATABASE PublicEnterpriseDb

USE PublicEnterpriseDb

CREATE TABLE [Ddds] (
    [Id] uniqueidentifier NOT NULL,
    [Codigo] nvarchar(450) NOT NULL,
    [DescricaoEstado] nvarchar(100) NOT NULL,
    [SiglaEstado] nvarchar(2) NOT NULL,
    CONSTRAINT [PK_Ddds] PRIMARY KEY ([Id])
);

CREATE TABLE [Contatos] (
    [Id] uniqueidentifier NOT NULL,
    [DddId] uniqueidentifier NOT NULL,
    [Email] nvarchar(100) NOT NULL,
    [Nome] nvarchar(200) NOT NULL,
    [Telefone] nvarchar(9) NOT NULL,
    CONSTRAINT [PK_Contatos] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Contatos_Ddds_DddId] FOREIGN KEY ([DddId]) REFERENCES [Ddds] ([Id]) ON DELETE CASCADE
);

CREATE INDEX [IX_Contatos_DddId] ON [Contatos] ([DddId]);

CREATE UNIQUE INDEX [IX_Ddds_Codigo] ON [Ddds] ([Codigo]);

-- Inserir os dados dos DDDs
INSERT INTO Ddds (Id, Codigo, DescricaoEstado, SiglaEstado) VALUES
(NEWID(), '11', 'São Paulo', 'SP'),
(NEWID(), '12', 'São Paulo', 'SP'),
(NEWID(), '13', 'São Paulo', 'SP'),
(NEWID(), '14', 'São Paulo', 'SP'),
(NEWID(), '15', 'São Paulo', 'SP'),
(NEWID(), '16', 'São Paulo', 'SP'),
(NEWID(), '17', 'São Paulo', 'SP'),
(NEWID(), '18', 'São Paulo', 'SP'),
(NEWID(), '19', 'São Paulo', 'SP'),
(NEWID(), '21', 'Rio de Janeiro', 'RJ'),
(NEWID(), '22', 'Rio de Janeiro', 'RJ'),
(NEWID(), '24', 'Rio de Janeiro', 'RJ'),
(NEWID(), '27', 'Alagoas', 'AL'),
(NEWID(), '28', 'Sergipe', 'SE'),
(NEWID(), '31', 'Minas Gerais', 'MG'),
(NEWID(), '32', 'Espírito Santo', 'ES'),
(NEWID(), '33', 'Rio de Janeiro', 'RJ'),
(NEWID(), '34', 'Minas Gerais', 'MG'),
(NEWID(), '35', 'São Paulo', 'SP'),
(NEWID(), '37', 'Minas Gerais', 'MG'),
(NEWID(), '38', 'Minas Gerais', 'MG'),
(NEWID(), '41', 'Paraná', 'PR'),
(NEWID(), '42', 'Santa Catarina', 'SC'),
(NEWID(), '43', 'Paraná', 'PR'),
(NEWID(), '44', 'Mato Grosso', 'MT'),
(NEWID(), '45', 'Paraná', 'PR'),
(NEWID(), '46', 'Paraná', 'PR'),
(NEWID(), '47', 'Santa Catarina', 'SC'),
(NEWID(), '48', 'Santa Catarina', 'SC'),
(NEWID(), '49', 'Santa Catarina', 'SC'),
(NEWID(), '51', 'Mato Grosso do Sul', 'MS'),
(NEWID(), '53', 'Maranhão', 'MA'),
(NEWID(), '55', 'Mato Grosso', 'MT'),
(NEWID(), '61', 'Distrito Federal', 'DF'),
(NEWID(), '62', 'Goiás', 'GO'),
(NEWID(), '63', 'Tocantins', 'TO'),
(NEWID(), '64', 'Goiás', 'GO'),
(NEWID(), '65', 'Mato Grosso', 'MT'),
(NEWID(), '66', 'Mato Grosso', 'MT'),
(NEWID(), '67', 'Mato Grosso do Sul', 'MS'),
(NEWID(), '68', 'Acre', 'AC'),
(NEWID(), '69', 'Rondônia', 'RO'),
(NEWID(), '71', 'Bahia', 'BA'),
(NEWID(), '73', 'Bahia', 'BA'),
(NEWID(), '74', 'Bahia', 'BA'),
(NEWID(), '75', 'Bahia', 'BA'),
(NEWID(), '77', 'Bahia', 'BA'),
(NEWID(), '79', 'Sergipe', 'SE'),
(NEWID(), '81', 'Pernambuco', 'PE'),
(NEWID(), '82', 'Alagoas', 'AL'),
(NEWID(), '83', 'Paraíba', 'PB'),
(NEWID(), '84', 'Rio Grande do Norte', 'RN'),
(NEWID(), '85', 'Ceará', 'CE'),
(NEWID(), '86', 'Piauí', 'PI'),
(NEWID(), '87', 'Pernambuco', 'PE'),
(NEWID(), '88', 'Ceará', 'CE'),
(NEWID(), '89', 'Piauí', 'PI'),
(NEWID(), '91', 'Pará', 'PA'),
(NEWID(), '92', 'Amazonas', 'AM'),
(NEWID(), '93', 'Pará', 'PA'),
(NEWID(), '94', 'Pará', 'PA'),
(NEWID(), '95', 'Roraima', 'RR'),
(NEWID(), '96', 'Amapá', 'AP'),
(NEWID(), '97', 'Amazonas', 'AM'),
(NEWID(), '98', 'Maranhão', 'MA'),
(NEWID(), '99', 'Maranhão', 'MA');