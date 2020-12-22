DECLARE 
    @i int = 0,
    @gender varchar(1),
    @countryId int,
    @randomizer int,
    @age int;
DECLARE @Codes AS TABLE ([Code] nvarchar(8) unique);
    
    
WHILE @i < 50
BEGIN

  SET @randomizer = FLOOR(RAND()*(100-1+1))+1;
  SET @age = FLOOR(RAND()*(100-1+1))+1;

  SET @countryId = (SELECT TOP 1 [Id] FROM [Countries] with(nolock) ORDER BY newid());
  
  SET @gender = 
      case when  @randomizer > 50 
        then 'M'
        else 'F'
      end
      
  INSERT INTO
    [Users]
      (
        [FirstName],
        [LastName],
        [Gender],
        [CountryId],
        [Age]
      )
	VALUES(
      convert(nvarchar(50), newid()),
      convert(nvarchar(50), newid()),
      @gender,
      @countryId,
      @age
    )

	SET @i = @i + 1;

END