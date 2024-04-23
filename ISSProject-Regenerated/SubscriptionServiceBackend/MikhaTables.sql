CREATE TABLE MockUsers(
	mockuser_id INT PRIMARY KEY IDENTITY(1, 1),
	mockuser_password VARCHAR(MAX) NOT NULL,
	email VARCHAR(200) NOT NULL UNIQUE,
	first_name VARCHAR(MAX) NOT NULL,
	last_name VARCHAR(MAX) NOT NULL,
	birth_date DATETIME NOT NULL,
	phone_number VARCHAR(20),
);


CREATE TABLE MockMessages(
	message_id INT PRIMARY KEY IDENTITY(1, 1),
	sender_id INT FOREIGN KEY REFERENCES MockUsers(mockuser_id),
    receiver_id INT FOREIGN KEY REFERENCES MockUsers(mockuser_id),
	communication_date DATETIME NOT NULL,
	message_content VARCHAR(MAX)
);


create table MockPosts(
	mockpost_id int primary key identity(1,1),
	mockpost_title varchar(100) not null,
	mockpost_content text,
	mockpost_creator_id int foreign key
	references MockUsers(mockuser_id) not null,
	mockpost_date datetime not null
);


create table PremiumUsers(
	id int primary key identity(1,1),
	mockuser_id int foreign key
	references MockUsers(mockuser_id) not null

);

create table PremiumPosts(
	id int primary key identity(1,1),
	mockpost_id int foreign key
	references MockPosts(mockpost_id) not null

);

create table PremiumMessages(
	id int primary key identity(1,1),
	message_id int foreign key
	references MockMessages(message_id) not null
);

create table MockGroups(
	mockgroup_id int primary key identity(1,1),
	mockgroup_name varchar(100) not null
);


create table GroupUserRelationship(
	mockgroup_id int foreign key
	references MockGroups(mockgroup_id),
	mockuser_id int foreign key
	references MockUsers(mockuser_id),
	primary key(mockgroup_id, mockuser_id)
)

go
create or alter view PremiumUsersView
as
select MU.*
from PremiumUsers PU join
MockUsers MU on PU.mockuser_id = MU.mockuser_id
go
create or alter view PremiumPostsView
as
select MP.*
from PremiumPosts PP join
MockPosts MP on PP.mockpost_id = MP.mockpost_id
go
create or alter view PremiumMessagesView
as
select MM.*
from PremiumMessages PM join
MockMessages MM on PM.message_id = MM.message_id
go
create or alter function GetUsersFromGroup(@group_id int)
returns table
as
	return(
		select MU.mockuser_id
		from MockGroups MG 
		join GroupUserRelationship GUR on MG.mockgroup_id = GUR.mockgroup_id
		join MockUsers MU on GUR.mockuser_id = MU.mockuser_id
		where MG.mockgroup_id = @group_id
	)
go

/*
drop function GetUsersFromGroup
drop view PremiumUsersView
drop view PremiumPostsView
drop view PremiumMessagesView
drop table PremiumUsers
drop table PremiumPosts
drop table PremiumMessages
drop table GroupUserRelationship
drop table MockGroups
drop table MockPosts
drop table MockMessages
drop table MockUsers
*/