# Transaction_Tracker
Programming  Class C# Project
# Create Table Queries

mysql>create table account (id int  auto_increment,
    -> username varchar(20) collate utf8mb4_0900_as_cs not null,
    -> name varchar(20) not null,
    -> opening_balance int default 0,
    -> closing_balance int,
    -> total_credit int default 0,
    -> total_debit int default 0,
    -> status int default 1,
    -> total_transaction int default 0,
    -> primary key(id),
    -> foreign key (username) references profile(username)
    -> );

mysql> create table transaction(
    -> id int auto_increment, 
    -> username varchar(20) not null collate utf8mb4_0900_as_cs, 
    -> account int not null, 
    -> transaction_desc varchar(20) not null, 
    -> transaction_type tinyint not null, 
    -> amount int not null,
    -> status int default  1,
    -> created_at timestamp default current_timestamp not null,
    -> updated_at timestamp default current_timestamp on update current_timestamp not null, 
    -> primary key(id), 
    -> foreign key (username) references profile (username), 
    -> foreign key (account) references account (id));

mysql> Create table profile(
    -> username varchar(20) collate utf8mb4_0900_as_cs,
    -> password  varchar(20),
    -> primary key(username)
    -> );
