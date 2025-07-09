CREATE PROCEDURE [dbo].[SP_User_Add]
	@Username NVARCHAR(50),
	@Email NVARCHAR(100),
	@Password NVARCHAR(50),
	@Role NVARCHAR(5),
	@Lastname NVARCHAR(50),
	@Firstname NVARCHAR(50),
	@Address NVARCHAR(50)
AS
	BEGIN TRY
		-- verification si l'username existe déjà
		IF EXISTS (SELECT 1 FROM [dbo].[Users] WHERE [Username] = @Username)
		BEGIN
			RETURN -1
		END

		-- verification si l'email existe déjà
		IF EXISTS (SELECT 1 FROM [dbo].[Users] WHERE [Email] = @Email)
		BEGIN
			RETURN -1
		END

		BEGIN TRANSACTION

		DECLARE @SALT UNIQUEIDENTIFIER = NEWID()
		DECLARE @Pepper UNIQUEIDENTIFIER = dbo.SP_GetPepper()

		DECLARE @PasswordCombined NVARCHAR(255) = 
		@Password + CONVERT(NVARCHAR(50) , @Salt) + CONVERT(NVARCHAR(50) , @Pepper)

		DECLARE @PasswordHash VARBINARY(64) = HASHBYTES('SHA2_256', CONVERT(VARBINARY(255), @PasswordCombined))

		DECLARE @NewId TABLE (Id UNIQUEIDENTIFIER)

		INSERT INTO [dbo].[Users]
		(
			[Username],
			[Email],
			[Password],
			[Role],
			[Lastname],
			[Firstname],
			[Address],
			[Salt]
		)
		OUTPUT inserted.Id INTO @NewId
		VALUES
		(
		@Username,
		@Email,
		@PasswordHash,
		@Role,
		@Lastname,
		@Firstname,
		@Address,
		@Salt
		)
		COMMIT TRANSACTION

		SELECT TOP 1 Id AS NewUserId FROM @NewId

	END TRY
	BEGIN CATCH

	ROLLBACK TRANSACTION

	SELECT CAST('00000000-0000-0000-0000-000000000000' AS UNIQUEIDENTIFIER) AS NewUserId

	END CATCH