create table dbo.KvStore
(
    k nvarchar(32)  not null,
    v nvarchar(max) not null
)
go

create index KvStore_k_index
    on dbo.KvStore (k desc)
go

alter table dbo.KvStore
    add constraint KvStore_pk
        primary key nonclustered (k desc)
go

alter table dbo.KvStore
    add createdAt datetime2 not null default getdate()
go