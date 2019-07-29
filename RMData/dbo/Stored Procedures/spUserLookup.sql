CREATE PROCEDURE [dbo].[spUserLookup]
	@Id nvarchar(128)
AS
begin
	set nocount on;

	SELECT Id,FirstName,LastName,EmailAdress,CreatedDate
	FROM [dbo].[User]
	WHERE Id = @Id
end
