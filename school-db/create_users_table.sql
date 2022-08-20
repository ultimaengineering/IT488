use brickbooks
go

create table dbo.Users
(
    id         uniqueidentifier not null
        constraint id
            primary key,
    username   varchar(254),
    password   varchar(254),
    last_login datetime
)
go

create unique index username_idx
    on dbo.Users (username)
go