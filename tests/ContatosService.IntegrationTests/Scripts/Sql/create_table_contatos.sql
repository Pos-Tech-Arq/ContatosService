create table Contatos
(
    Id       uniqueidentifier        not null
        constraint PK_Contatos
            primary key,
    Nome     varchar(300)            not null,
    Ddd      nvarchar(max),
    Numero   nvarchar(max),
    RegiaoId uniqueidentifier
        constraint FK_Contatos_Regioes_RegiaoId
            references Regioes,
    Email    varchar(200) default '' not null
);

create index IX_Contatos_RegiaoId
    on Contatos (RegiaoId);

