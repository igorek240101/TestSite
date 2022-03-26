USE [TestSite]
GO

SELECT [Id]
      ,[Name]
      ,[DepartamentId]
      ,[BirthDate]
      ,[StartWorkDate]
      ,[Wage]
  FROM [dbo].[Worker]

  WHERE [Wage] > 10000
GO


