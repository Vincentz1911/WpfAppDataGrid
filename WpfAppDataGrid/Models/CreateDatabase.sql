use master 
go

if not exists(select * from sys.databases where name = 'UserDatabase')
Create database UserDatabase
go

use UserDatabase
go

if not exists(select * from information_schema.tables where table_name = 'Users')
Create table Users
(
LoginName nvarchar(50),
[Password] nvarchar(50)
)

truncate table Users
insert into Users values ('Alba', 'Jessi'), ('JamesBond', '007'),  ('admin', 'admin')