SELECT
	[AgeGroup].[Value] [AgeGroup],
	count(1) [Count]
FROM
	[Users] [User]
CROSS APPLY
	(SELECT 
		case
			when [User].[Age] <= 12 then 'Child'
			when [User].[Age] > 12 AND [User].[Age] <= 18 then 'Adolescence'
			when [User].[Age] > 18 AND [User].[Age] <= 59 then 'Adult'
			when [User].[Age] > 59 then 'Senior Adult'
			else 'Unknown'
		end as [Value]
	) as [AgeGroup]
GROUP BY
	[AgeGroup].[Value]
ORDER BY
	count(1)
DESC