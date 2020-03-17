CREATE PROCEDURE [dbo].[procedure]
    @employee nvarchar(50),
    @department nvarchar(50),
    @Id int out
AS
    INSERT INTO datadataGrid(Employee, Department)
    VALUES (@employee, @department)
   
    SET @Id=SCOPE_IDENTITY()
GO