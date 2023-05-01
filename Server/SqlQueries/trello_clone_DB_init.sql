create database trello_clone_DB;

use trello_clone_DB;

create table users ( 
	UserId uniqueidentifier primary key not null,
	FirstName varchar(255) not null,
	LastName varchar(255) not null,
	JobTitle varchar(255),
	UserName varchar(255) unique not null,
	UserPassword varchar(255) not null,
	UserEmail varchar(255) unique not null,
	TeamId int not null,
);

create table teams (
	teamId uniqueidentifier primary key not null,
	teamName varchar(255),
	
);

create table boards (
	boardId uniqueidentifier primary key not null,
	boardName varchar(255) not null,
);

create table board_columns (
	columnId uniqueidentifier primary key not null,
	columnName varchar(255) not null,
	columnIndex int not null,
	boardId uniqueidentifier foreign key references boards(boardId) not null,
);

create table board_cards (
	cardId uniqueidentifier primary key not null,
	itemName varchar(255) not null,
	itemDescription varchar(255),
	jobType varchar(255),
	assignedUserId uniqueidentifier foreign key references users(userId),
	dateCreated datetime not null,
	lastModifiedDate datetime not null,
	cardIndex int not null,
	columnId uniqueidentifier foreign key references board_columns(columnId) not null,
	boardId uniqueidentifier foreign key references boards(boardId),
	dateEnd datetime
);