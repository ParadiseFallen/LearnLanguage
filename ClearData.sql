delete from Translations where Translations.Id<>0
delete from Phrases where Phrases.Id<>0
DBCC CHECKIDENT ('[Translations]', RESEED, 0);
DBCC CHECKIDENT ('[Phrases]', RESEED, 0);
drop database [C:\USERS\PARADISE\DOCUMENTS\LEARNLANG.MDF]