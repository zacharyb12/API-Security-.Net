CREATE PROCEDURE [dbo].[SP_User_Update_Password]
	@Id UNIQUEIDENTIFIER,
	@Email NVARCHAR(100),
	@Password NVARCHAR(50),
	@NewPassword NVARCHAR(50)
AS

BEGIN TRY
	BEGIN TRANSACTION;

	DECLARE @StoredPassword  VARBINARY(64)
	DECLARE @StoredSalt UNIQUEIDENTIFIER

	SELECT 
	@StoredPassword = Password,
	@StoredSalt = Salt
	FROM [dbo].[Users]
	WHERE Id = @Id AND Email = @Email;


	IF @StoredPassword IS NULL OR @StoredSalt IS NULL
	BEGIN
		ROLLBACK TRANSACTION
		RETURN -1;
	END

	DECLARE @Pepper UNIQUEIDENTIFIER = dbo.SP_GetPepper();
	DECLARE @OldPasswordCombined NVARCHAR(255) = @Password + CONVERT(NVARCHAR(50), @StoredSalt) + CONVERT(NVARCHAR(50), @Pepper);
	DECLARE @OldPasswordHash VARBINARY(64) = HASHBYTES('SHA2_256', CONVERT(VARBINARY(255), @OldPasswordCombined));


	IF @OldPasswordHash <> @StoredPassword
	BEGIN
	ROLLBACK TRANSACTION
		RETURN -1;
	END

	DECLARE @NewSalt UNIQUEIDENTIFIER = NEWID();
	DECLARE @NewPasswordCombined NVARCHAR(255) = @NewPassword + CONVERT(NVARCHAR(50), @NewSalt) + CONVERT(NVARCHAR(50), @Pepper);
	DECLARE @NewPasswordHash VARBINARY(64) = HASHBYTES('SHA2_256', CONVERT(VARBINARY(255), @NewPasswordCombined));

	UPDATE [dbo].[Users]
	SET 
		[Password] = @NewPasswordHash,
		[Salt] = @NewSalt
	WHERE Id = @Id;

	COMMIT TRANSACTION;

	RETURN 1
END TRY
BEGIN CATCH

	ROLLBACK TRANSACTION

	RETURN 0

END CATCH
