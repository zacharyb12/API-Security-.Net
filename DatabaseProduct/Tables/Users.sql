CREATE TABLE [dbo].[Users]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(), 
    [Username] NVARCHAR(50) NOT NULL, 
    [Email] NVARCHAR(100) NOT NULL, 
    [Password] VARBINARY(64) NOT NULL, 
    [Role] NVARCHAR(5) NOT NULL DEFAULT 'user', 
    [Lastname] NVARCHAR(50) NOT NULL, 
    [Firstname] NVARCHAR(50) NOT NULL, 
    [Address] NVARCHAR(150) NULL, 
    [Salt] UNIQUEIDENTIFIER NOT NULL

    CONSTRAINT [UQ_Users_Username] UNIQUE ([Username]),
    CONSTRAINT [UQ_Users_Email] UNIQUE ([Email]),
    CONSTRAINT [CK_Users_Role] CHECK ([Role] IN ('user', 'admin'))
)
