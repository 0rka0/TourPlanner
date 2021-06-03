drop table tours;

create table tours (
id integer primary key,
tourname varchar(60) not null,
description varchar(1000),
information varchar(1000),
distance varchar(10) not null, 
image varchar(70)
);

drop table tourlogs;

create table tourlogs (
id integer primary key,
date Timestamp,
distance real,
report varchar(1000), 
totaltime real,
rating integer,
avgspeed int,
weather int,
traffic int,
breaks int,
groupsize int,
tid integer,
CONSTRAINT fk_tour
  FOREIGN KEY(tid) 
  REFERENCES tours(id)
  ON DELETE CASCADE
);

drop table attractions;

create table attractions (
id int primary key,
pid varchar(50),
name varchar(100),
rating real,
total_ratings int,
address varchar(100),
tid integer,
CONSTRAINT fk_tour
	FOREIGN KEY(tid)
	REFERENCES tours(id)
	ON DELETE CASCADE
);