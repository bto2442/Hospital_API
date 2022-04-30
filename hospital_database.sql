CREATE DATABASE hospital;

USE hospital;

CREATE TABLE Staffs(
  id integer PRIMARY KEY,
  first_name varchar(255) NOT NULL,
  last_name varchar(255) NOT NULL,
  phone_num varchar(255) NOT NULL,
  position varchar(255) NOT NULL
);

CREATE TABLE Patients(
  id integer PRIMARY KEY,
  first_name varchar(255) NOT NULL,
  last_name varchar(255) NOT NULL,
  phone_num varchar(255),
  address varchar(255),
  admitted datetime,
  discharged boolean,
  staffid integer,
  FOREIGN KEY (staffid) REFERENCES Staffs(id)
);

CREATE TABLE Prescriptions(
  id integer PRIMARY KEY,
  patientid integer NOT NULL,
  presc_name varchar(255),
  quantity integer,
  FOREIGN KEY (patientid) REFERENCES Patients(id)
);

CREATE TABLE Diagnosis(
  id integer PRIMARY KEY,
  patientid integer NOT NULL,
  cause varchar(255),
  symptoms varchar(255),
  FOREIGN KEY (patientid) REFERENCES Patients(id)
);

INSERT INTO Staffs VALUES(1, "Kimberly", "Khan", "212-200-4322", "Doctor");
INSERT INTO Staffs VALUES(2, "Paul", "Phil", "912-291-1758", "Head Doctor");
INSERT INTO Staffs VALUES(3, "Arianna", "Santiago", "212-202-3885", "Surgeon");
INSERT INTO Staffs VALUES(4, "Abraham", "Abdu", "212-204-1763", "Nurse");
INSERT INTO Staffs VALUES(5, "Joe", "Murberry", "626-231-6602", "Pediatrician");
INSERT INTO Staffs VALUES(6, "Amie", "Massery", "212-258-3092", "Surgeon");
INSERT INTO Staffs VALUES(7, "Abigail", "Larsen", "929-216-1110", "Nurse");
INSERT INTO Staffs VALUES(8, "Matteo", "Gibbs", "626-293-7274", "Surgeon");
INSERT INTO Staffs VALUES(9, "Keaton", "Chavez", "917-328-0731", "Janitor");
INSERT INTO Staffs VALUES(10, "Cora", "Rowland", "212-259-8104", "Doctor");
INSERT INTO Staffs VALUES(11, "Arjun", "Boyle", "212-381-8160", "Nurse");
INSERT INTO Staffs VALUES(12, "Cara", "Adams", "929-206-0213", "Surgeon");
INSERT INTO Staffs VALUES(13, "Travis", "Blackburn", "212-345-4438", "Nurse");
INSERT INTO Staffs VALUES(14, "Victor", "Glenn", "212-298-0527", "Nurse");
INSERT INTO Staffs VALUES(15, "Alesha", "Ferell", "917-355-2120", "Surgeon");
INSERT INTO Staffs VALUES(16, "Muhammad", "Saunders", "212-400-9308", "Janitor");
INSERT INTO Staffs VALUES(17, "Aoife", "Matthams", "917-446-7745", "Pediatrician");
INSERT INTO Staffs VALUES(18, "Kyla", "Chan", "212-482-2353", "Surgeon");
INSERT INTO Staffs VALUES(19, "Nina", "Lowery", "626-416-6881", "Nurse");
INSERT INTO Staffs VALUES(20, "Harry", "Nunez", "212-424-4178", "Doctor");
INSERT INTO Staffs VALUES(21, "Scarlet", "Lane", "929-470-2047", "Doctor");

INSERT INTO Patients VALUES (1,"Milosz","Decker","718-531-1234","632 W. Cottage Avenue Brooklyn, NY 11214","2016-04-01",TRUE,1);
INSERT INTO Patients VALUES (2,"Shivam","Talbot","347-420-3882","9373 Delaware Ave. Brooklyn, NY 11215","2016-05-24",FALSE,3);
INSERT INTO Patients VALUES (3,"Steffan","Allen","917-081-0923","562 1st Lane Freeport Freeport, NY 11520","2016-07-29",FALSE,5);
INSERT INTO Patients VALUES (4,"Hywel","Bradley","646-177-9438","217 River Court Patchogue, NY 11772","2016-08-23",TRUE,7);
INSERT INTO Patients VALUES (5,"Mert","Mcgill","718-027-8364","230 Griffin Drive Brooklyn, NY 11234","2016-10-03",TRUE,9);
INSERT INTO Patients VALUES (6,"Dillon","Meadows","347-658-7823","8300 South Henry Smith Dr. Jackson Heights, NY 11372","2016-11-12",TRUE,11);
INSERT INTO Patients VALUES (7,"Aysha","Fischer","917-576-6572","7325 N. Williams Lane Rome, NY 13440","2017-06-12",FALSE,13);
INSERT INTO Patients VALUES (8,"Sonny","Pittman","646-908-9382","526 Spring Rd. Bronx, NY 10462","2017-12-05",TRUE,15);
INSERT INTO Patients VALUES (9,"Kaeden","Dawe","718-827-2312","722 South Ivy Street Bronx, NY 10467","2018-01-19",FALSE,17);
INSERT INTO Patients VALUES (10,"Nancie","Patterson","347-402-8987","53 Bishop Lane Brooklyn, NY 11224","2018-03-27",FALSE,19);
INSERT INTO Patients VALUES (11,"Tayler","Aguilar","917-447-4104","1 E. Cherry Hill Lane South Richmond Hill, NY 11419","2019-02-11",FALSE,21);
INSERT INTO Patients VALUES (12,"Syed","Gould","646-475-8703","88 High Point Drive Brooklyn, NY 11216","2019-05-10",TRUE,2);
INSERT INTO Patients VALUES (13,"Fredrick","Coles","718-829-9545","213 Sage Road Levittown, NY 11756","2019-05-15",TRUE,4);
INSERT INTO Patients VALUES (14,"Owen","Fitzgerald","347-348-4743","2 Blue Spring St. Staten Island, NY 10314","2019-10-26",TRUE,6);
INSERT INTO Patients VALUES (15,"Yousif","Lucero","917-951-3566","9353 Brown St. New York, NY 10027","2020-04-11",TRUE,8);
INSERT INTO Patients VALUES (16,"Mildred","Norman","646-447-9120","564 Bedford Dr. Brooklyn, NY 11203","2020-04-17",FALSE,10);
INSERT INTO Patients VALUES (17,"Noah","Knights","718-682-7589","325 Winding Way Circle Brentwood, NY 11717","2020-05-10",TRUE,12);
INSERT INTO Patients VALUES (18,"Meg","Coffey","347-612-5919","50 Hickory St. Brooklyn, NY 11230","2020-05-22",TRUE,14);
INSERT INTO Patients VALUES (19,"Latoya","Burns","917-997-6617","30 Eagle St. Ridgewood, NY 11385","2020-05-24",FALSE,16);
INSERT INTO Patients VALUES (20,"Norma","Sosa","646-620-3080","25 Johnson Court Brooklyn, NY 11212","2020-07-23",TRUE,18);
INSERT INTO Patients VALUES (21,"Olivia","Brady","718-100-3005","8278 Squaw Creek Ave. Buffalo, NY 14215","2020-09-10",TRUE,20);
INSERT INTO Patients VALUES (22,"Jackary","Smith","929-229-4158","390 Ocean Parkway Brooklyn, NY 11218","2020-09-15",TRUE,2);

INSERT INTO Diagnosis VALUES (1, 1, "breast cancer", "swelling");
INSERT INTO Diagnosis VALUES (2, 2, "anemia", "low blood count");
INSERT INTO Diagnosis VALUES (3, 3, "kidney stones", "dehydration");
INSERT INTO Diagnosis VALUES (4, 4, "internal bleeding", "low blood count");
INSERT INTO Diagnosis VALUES (5, 5, "rabies", "hydrophobia");
INSERT INTO Diagnosis VALUES (6, 6, "heart attack", "lack of oxygen");
INSERT INTO Diagnosis VALUES (7, 7, "stroke", "exhaustion");
INSERT INTO Diagnosis VALUES (8, 8, "tumor", "brain damage");
INSERT INTO Diagnosis VALUES (9, 9, "concussion", "blunt force");
INSERT INTO Diagnosis VALUES (10, 10, "laceration", "severe bleeding");
INSERT INTO Diagnosis VALUES (11, 11, "glaucoma", "vision failure");
INSERT INTO Diagnosis VALUES (12, 12, "alzheimers", "memory deficiency");
INSERT INTO Diagnosis VALUES (13, 13, "diabetes", "irregular glucose"); 
INSERT INTO Diagnosis VALUES (14, 14, "asthma", "lung weakness");
INSERT INTO Diagnosis VALUES (15, 15, "pneumonia", "lung weakness");
INSERT INTO Diagnosis VALUES (16, 16, "nerve damage", "extreme pain");
INSERT INTO Diagnosis VALUES (17, 17, "paralysis", "heart attack");
INSERT INTO Diagnosis VALUES (18, 18, "heart attack", "clogged artery");
INSERT INTO Diagnosis VALUES (19, 19, "heart attack", "clogged artery");
INSERT INTO Diagnosis VALUES (20, 20, "hemorrhage", "severe trauma");
INSERT INTO Diagnosis VALUES (21, 21, "brain cancer", "genetic mutation");

INSERT INTO Prescriptions VALUES (1,1,'Vicodin',20);
INSERT INTO Prescriptions VALUES (2,2,'Synthroid',20);
INSERT INTO Prescriptions VALUES (3,3,'Delasone',15);
INSERT INTO Prescriptions VALUES (4,4,'Norco',50);
INSERT INTO Prescriptions VALUES (5,5,'Metformin',35);
INSERT INTO Prescriptions VALUES (6,6,'Rosuvastutiin',100);
INSERT INTO Prescriptions VALUES (7,7,'Amoxil',50);
INSERT INTO Prescriptions VALUES (8,8,'Lexapro',75);
INSERT INTO Prescriptions VALUES (9,9,'Risperdal',35);
INSERT INTO Prescriptions VALUES (10,10,'Amlodipine',45);
INSERT INTO Prescriptions VALUES (11,11,'Albuterol',60);
INSERT INTO Prescriptions VALUES (12,12,'Losartan',15);
INSERT INTO Prescriptions VALUES (13,13,'Metoprolol',10);
INSERT INTO Prescriptions VALUES (14,14,'Simvastatin',45);
INSERT INTO Prescriptions VALUES (15,15,'Omeprazole',25);
INSERT INTO Prescriptions VALUES (16,16,'Elavil',120);
INSERT INTO Prescriptions VALUES (17,17,'Xanax',15);
INSERT INTO Prescriptions VALUES (18,18,'Cordarone',45);
INSERT INTO Prescriptions VALUES (19,19,'Methadone',10);
INSERT INTO Prescriptions VALUES (20,20,'Hexalen',10);
INSERT INTO Prescriptions VALUES (21,21,'Escitalopram',20);
