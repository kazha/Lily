SELECT
	*
FROM
	[Users] [User]
WHERE
	[User].[Age] > 36
	AND [Gender] = 'M'
ORDER BY
	[User].[FirstName],
	[User].[LastName]