CREATE PROCEDURE [dbo].[SP_User_Update]
	@Id UNIQUEIDENTIFIER,
	@Username NVARCHAR(50),
	@Email NVARCHAR(100),
	@Lastname NVARCHAR(50),
	@Firstname NVARCHAR(50),
	@Address NVARCHAR(50)
AS
	
BEGIN TRY
	BEGIN TRANSACTION

		UPDATE [dbo].[Users]
		SET 			
		[Username] = @Username,
		[Email] = @Email,
		[Lastname] = @Lastname,
		[Firstname] = @Firstname,
		[Address] = @Address
		WHERE 
		[Id] = @Id

		RETURN 1

	COMMIT TRANSACTION
END TRY
BEGIN CATCH

	ROLLBACK TRANSACTION
	RETURN -1

END CATCH
