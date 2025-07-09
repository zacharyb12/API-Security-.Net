CREATE PROCEDURE [dbo].[SP_Product_Update]
	@Id UNIQUEIDENTIFIER,
	@Name NVARCHAR(100),
	@Stock INT,
	@Price DECIMAL(8,2),
	@Description NVARCHAR(500),
	@Category NVARCHAR(50)
AS
	BEGIN TRY 
		
		IF (SELECT COUNT(*) FROM [dbo].[Product] WHERE [Id] = @Id) = 0
		BEGIN 
			SELECT 0;
			RETURN;
		END

		UPDATE [dbo].[Product]
		SET 
			[Name] = @Name,
			[Stock] = @Stock,
			[Price] = @Price,
			[Description] = @Description,
			[Category] = @Category

			WHERE [Id] = @Id;

			SELECT 1;
	END TRY
	BEGIN CATCH
		SELECT 0;
	END CATCH
