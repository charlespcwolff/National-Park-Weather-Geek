
DELETE FROM survey_result;
DELETE FROM weather;
DELETE FROM park;

INSERT INTO park VALUES
('AAAA','AAAA National Park','Zaire',123,567,999,12,'Climate',9999,123456789,'Lorem ipsum dolor sit amet','John Doe','Pellentesque vulputate erat vulputate tellus consectetur, sodales dapibus nisi aliquam',99,555),
('BBBB','BBBB National Park','Congo',4321,8765,111,77,'etamilC',1111,987654321,'Cras efficitur, eros vel placerat','Jane Doe','Maecenas finibus mattis orci, at condimentum ligula iaculis non. Etiam',1,321);

INSERT INTO weather VALUES
('AAAA',1,50,70,'meatballs'),
('BBBB',4,20,30,'sllabtaem');

INSERT INTO survey_result VALUES
('AAAA','aaaa@aaaa.com','Liberia','sloth'),
('BBBB','bbbb@bbbb.com','Madagascar','cheetah'),
('AAAA','cccc@cccc.com','Rwanda','elephant');


