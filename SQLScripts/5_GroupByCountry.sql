SELECT
	[Country].[Name],
	count(1) as [Count]
FROM
	[Countries] [Country]
INNER JOIN
	[Users] [User]
		ON [Country].[Id] = [User].[CountryId]
GROUP BY
	[Country].[Name]
ORDER BY
	count(1)
DESC