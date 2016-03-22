CREATE TABLE Users
(
	[UserID] int not null primary key identity,
	[UserName] nvarchar(100) not null,
	[UserEmail] nvarchar(200) not null,
	[UserPassword] nvarchar(500) not null,
	[UserPrivilege] int not null primary key 
)
