create database trello_clone_DB;

use trello_clone_DB;

create table users ( 
	userId int primary key identity(1,1) not null,
	firstName varchar(255) not null,
	lastName varchar(255) not null,
	jobTitle varchar(255),
	userName varchar(255) not null,
	userPassword varchar(255) not null,
	email varchar(255) not null,
	teamId int,
);

create table teams (
	teamId int primary key identity(1,1) not null,
	teamName varchar(255),
	
);

create table boards (
	boardId uniqueidentifier primary key not null,
	boardName varchar(255) not null,
);

create table board_columns (
	columnId int primary key identity(1,1) not null,
	columnName varchar(255) not null,
	columnIndex int not null,
	boardId uniqueidentifier foreign key references boards(boardId) not null,
);

create table board_cards (
	cardId int primary key identity(1,1) not null,
	itemName varchar(255) not null,
	itemDescription varchar(255),
	jobType varchar(255),
	assignedUserId int foreign key references users(userId),
	dateCreated datetime not null,
	lastModifiedDate datetime not null,
	cardIndex int not null,
	columnId int foreign key references board_columns(columnId) not null,
	boardId uniqueidentifier foreign key references boards(boardId) not null,
);