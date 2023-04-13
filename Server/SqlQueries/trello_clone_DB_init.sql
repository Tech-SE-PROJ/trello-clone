create database trello_clone_DB;

use trello_clone_DB;

create table users ( 
	userId uniqueidentifier primary key not null,
	firstName varchar(255) not null,
	lastName varchar(255) not null,
	jobTitle varchar(255),
	userName varchar(255) unique not null,
	userPassword varchar(255) not null,
	email varchar(255) unique not null,
	teamId int,
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
	boardId uniqueidentifier foreign key references boards(boardId) not null,
);