--insert into Languages([Name],CultureInfo,Flag) values
--(N'',)

insert into Phrases ([Text],CultureId) values
(N'Now or never!',3),			--1
(N'Сейчас или никогда!',4),	--2
(N'Everyone has one''s own path.',3),			--3
(N'У каждого своя дорога',4),	--4
(N'Life is beautiful.',3),			--5
(N'Жизнь прекрасна',4),	--6
(N'Life is a journey',3),			--7
(N'Жизнь это приключение',4)	--8
go

insert into Translations(AId,BId) values
(1,2),
(3,4),
(5,6),
(7,8)

--select * from Phrases
--update Phrases
--set [Text] = N'Everyone has one''s own path.'
--where Id = 3
/*

update Languages
set Flag = (select * from Openrowset( Bulk 'C:/Users/Paradise/Desktop/en.png', Single_Blob) as img)
where CultureInfo = N'en-EN'

*/