create table Cidades
(
    Id       uniqueidentifier not null
        constraint PK_Cidades
            primary key,
    Nome     varchar(100)     not null,
    RegiaoId uniqueidentifier
        constraint FK_Cidades_Regioes_RegiaoId
            references Regioes
);


create index IX_Cidades_RegiaoId
    on Cidades (RegiaoId);

