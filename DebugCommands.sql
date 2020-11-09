select * from Translations 
join Phrases as A on Translations.AId = A.Id 
join Phrases as B on Translations.BId = B.Id 


go
select * from Phrases
go
drop table Translations
drop table Phrases
ALTER DATABASE LearnLang SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
DROP DATABASE LearnLang ;
go

