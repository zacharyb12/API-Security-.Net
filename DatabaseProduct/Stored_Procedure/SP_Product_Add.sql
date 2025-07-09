CREATE PROCEDURE [dbo].[SP_Product_Add]
	@Name NVARCHAR(100),
	@Stock INT,
	@Price DECIMAL(8,2),
	@Description NVARCHAR(500),
	@Category NVARCHAR(50)
AS
	
	BEGIN TRY
		INSERT INTO [dbo].[Product]
		(
			[Name],
			[Stock],
			[Price],
			[Description],
			[Category]
		)
		OUTPUT inserted.Id
		VALUES
		(
			@Name,
			@Stock,
			@Price,
			@Description,
			@Category
		)
	END TRY
	BEGIN CATCH

		SELECT CAST('00000000-0000-0000-0000-000000000000' AS UNIQUEIDENTIFIER) AS Id;

	END CATCH
