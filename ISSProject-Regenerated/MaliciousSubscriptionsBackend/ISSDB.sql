CREATE DATABASE CelebrationOfCapitalism;

USE CelebrationOfCapitalism;

------			GENERAL			------
CREATE TABLE MockUsers(
	mockuser_id INT PRIMARY KEY IDENTITY(1, 1),
	mockuser_password VARCHAR(MAX) NOT NULL,
	email VARCHAR(200) NOT NULL UNIQUE,
	first_name VARCHAR(MAX) NOT NULL,
	last_name VARCHAR(MAX) NOT NULL,
	birth_date DATETIME NOT NULL,
	phone_number VARCHAR(20),
);

CREATE TABLE MockMessages(
	message_id INT PRIMARY KEY IDENTITY(1, 1),
	sender_id INT FOREIGN KEY REFERENCES MockUsers(mockuser_id),
    receiver_id INT FOREIGN KEY REFERENCES MockUsers(mockuser_id),
	communication_date DATETIME NOT NULL,
	message_content VARCHAR(MAX)
);

------			Scam bots			------
CREATE TABLE FakeUsers(
	fake_user_id INT FOREIGN KEY REFERENCES MockUsers(mockuser_id) UNIQUE,
);

-- this is definitely gonna be changed
CREATE TABLE BannedUsers(
	mockuser_id INT FOREIGN KEY REFERENCES MockUsers(mockuser_id) UNIQUE
);

CREATE TABLE ScamMessageTemplates(
	template_id INT PRIMARY KEY IDENTITY(1, 1),
	template_content VARCHAR(400) UNIQUE
);

CREATE TABLE ScamMessageLinks(
	link_id INT PRIMARY KEY IDENTITY(1, 1),
	link_url VARCHAR(70) UNIQUE
);

------			Social Engineering			------
CREATE TABLE GraphAnalyzerLogs(
    LogID INT PRIMARY KEY,
    LogTime DATETIME,
    SourceUserID INT FOREIGN KEY REFERENCES MockUsers(mockuser_id),
    TargetUserID INT FOREIGN KEY REFERENCES MockUsers(mockuser_id),
    Score INT NOT NULL,
    GeneratedMessage VARCHAR(1000)
);

------			Outside companies, cards			------ 
 CREATE TABLE CreditCards (
	creditcard_id INT PRIMARY KEY IDENTITY(1, 1),
	holder_user_id INT NOT NULL,
	creditcard_holder VARCHAR(MAX) NOT NULL,
	creditcard_number VARCHAR(16) NOT NULL,
	expiration_date VARCHAR(7) NOT NULL, -- should be MM/YY or MM/YYYY, seen both online, accounted for both
	cvv VARCHAR(3)

	CONSTRAINT holderFK FOREIGN KEY(holder_user_id) REFERENCES MockUsers(mockuser_id)
		ON DELETE CASCADE
		ON UPDATE CASCADE
);

CREATE TABLE CompanyTokens (
	company_id INT PRIMARY KEY IDENTITY(1, 1),
	company_name VARCHAR(MAX) NOT NULL,
	link_to_API VARCHAR(MAX),
	token VARCHAR(MAX),
	severity INT
);

--LOGIC BEHIND THIS: WHEN WE CHOOSE USERS TO SUBSCRIBE, WE'LL TAKE SOME USER IDS FROM BenignFlaggedUserIDs and SevereFlaggedUserIDs, AND WE'LL CHECK IF THEY ARE ALREADY SUBSCRIBED
--TO THE SERVICE VIA BenignFlaggedCrossedWithCompany AND SevereFlaggedCrossedWithCompany
CREATE TABLE BenignFlaggedUserIDs (
	mockuser_id INT PRIMARY KEY,
	CONSTRAINT benignFK FOREIGN KEY(mockuser_id) REFERENCES MockUsers(mockuser_id)
		ON DELETE CASCADE
		ON UPDATE CASCADE
);

CREATE TABLE SevereFlaggedUserIDs (
	mockuser_id INT PRIMARY KEY
	CONSTRAINT severeFK FOREIGN KEY(mockuser_id) REFERENCES MockUsers(mockuser_id)
		ON DELETE CASCADE
		ON UPDATE CASCADE
);

CREATE TABLE BenignFlaggedCrossedWithCompany (
	mockuser_id INT NOT NULL,
	company_id INT NOT NULL,
	PRIMARY KEY(mockuser_id, company_id),
	CONSTRAINT benigntousersFK FOREIGN KEY(mockuser_id) REFERENCES BenignFlaggedUserIDs(mockuser_id) 
		ON DELETE CASCADE 
		ON UPDATE CASCADE,
	CONSTRAINT benigntocompaniesFK FOREIGN KEY(company_id) REFERENCES CompanyTokens(company_id)
		ON DELETE CASCADE 
		ON UPDATE CASCADE
);

CREATE TABLE SevereFlaggedCrossedWithCompany (
	mockuser_id INT NOT NULL,
	company_id INT NOT NULL,
	PRIMARY KEY(mockuser_id, company_id),
	CONSTRAINT severetousersFK FOREIGN KEY(mockuser_id) REFERENCES SevereFlaggedUserIDs(mockuser_id) 
		ON DELETE CASCADE 
		ON UPDATE CASCADE,
	CONSTRAINT severetocompaniesFK FOREIGN KEY(company_id) REFERENCES CompanyTokens(company_id)
		ON DELETE CASCADE 
		ON UPDATE CASCADE
);