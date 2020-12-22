UPDATE
	[Users]
SET
	[Age] = [Age] + 10
WHERE
	[Id] IN (SELECT TOP 10 [_user].[Id] FROM [Users] as [_user] ORDER BY [_user].[Age])
