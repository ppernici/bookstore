﻿create table Products
(
	[ProductID] int not null primary key identity,
	[Name] nvarchar(100) not null,
	[Author] nvarchar(100) not null,
	[Description] nvarchar(500) not null,
	[Category] nvarchar(50) not null,
	[Price] decimal(16,2) not null
)

insert into Products values ('The Republic', 'Plato', 'In which Socrates describes the ideal society.', 'Philosophy', 12.99)
insert into Products values ('The Laws', 'Plato', 'Plato writes about laws.', 'Philosophy', 12.98)
insert into Products values ('Euthyphro', 'Plato', 'Socrates explores the true nature of morality.', 'Philosophy', 4.99)
insert into Products values ('The Apology', 'Plato', 'In which Socrates insults a jury of his peers holding his life in their hands.', 'Philosophy', 5.99)
insert into Products values ('Timaeus and Critias', 'Plato', 'Two dialogues of Plato (one unfinished) most famous for starting the Atlantis myth.', 'Philosophy', 3.69)
insert into Products values ('The Politics', 'Aristotle', 'A treatise on the nature of human society', 'Political Science', 15.99)
insert into Products values ('The Bible', 'God', 'Man has ignored its prescriptions for millennia, so why stop now?', 'Religion', 0.00)
insert into Products values ('The Almagest', 'Claudius Ptolemy', 'After some greeks realized that the Earth may indeed revolve around the Sun, Ptolemy wrote this fantastic work of mathematics, ensuring their discovery would lay dormant for millennia.', 'Mathematics', 105000.00)
insert into Products values ('De Revolutionibus Orbium Coelestium', 'Nicolas Copernicus', 'Copernicus wrote this groundbreaking work 1500 years after the Greeks had already beaten him to the punch because the calendars of Ptolemy had begun to fail.', 'Science', 1200.00)
insert into Products values ('Principia Mathematica', 'Isaac Newton', 'This seminal work revolutionized science, and also doomed thousands of college students to interminable calculus homework.', 'Science', 150.00)
insert into Products values ('Essay Concerning Human Understanding', 'John Locke', 'A verbose, repetitive, but intriguing work that helped kick off empricisim.', 'Philosophy', 25.99)
insert into Products values ('Two treatises on Government', 'John Locke', 'A work of politics that lay the foundations of the American Revolution.', 'Political Science', 15.99)
insert into Products values ('The Social Contract', 'Jean Jacques Rousseau', 'As John Locke is to the American Revolution, so Roussea is to the French.', 'Political Science', 12.00)
insert into Products values ('The Federalist', 'Hamilton, Madison, and Jay', 'Long, boring, but necessary reading for every American citizen.', 'Political Science', 17.99)
insert into Products values ('Capital', 'Karl Marx', 'Few read it, but many have heard of it.', 'Political Science', 13.99)
insert into Products values ('The Communist Manifesto', 'Karl Marx', 'If you are not a socialist when you are younger than 30, you have no heart; if you are still a socialist after you are 30, you have no head.', 'Political Science', 7.29)
insert into Products values ('On the Origin of Species', 'Charles Darwin', 'Published only so that Darwin would not be beaten out by a colleague, this book is to biology what the Principia is to physics.', 'Science', 500.00)
insert into Products values ('The Descent of Man', 'Charles Darwin', 'A book about sexual selection that could have been so much more.', 'Science', 72.00)