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

-- SELECT * FROM CONTATOS