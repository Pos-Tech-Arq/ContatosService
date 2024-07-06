create table Regioes
(
    Id     uniqueidentifier not null
        constraint PK_Regioes primary key,
    Ddd    varchar(3)       not null,
    Estado varchar(100)     not null
);