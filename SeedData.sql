insert into Phrases ([Text],Culture) values
(N'Now or never!','en-EN'),			--1
(N'Сейчас или никогда!','ru-RU'),	--2
(N'Everyone has one&#146;s own path.','en-EN'),			--3
(N'У каждого своя дорога','ru-RU'),	--4
(N'Life is beautiful.','en-EN'),			--5
(N'Жизнь прекрасна','ru-RU'),	--6
(N'Life is a journey','en-EN'),			--7
(N'Жизнь это приключение','ru-RU')	--8
go

insert into Translations(AId,BId) values
(1,2),
(3,4),
(5,6),
(7,8)