IF NOT EXISTS (SELECT name FROM sys.server_principals WHERE name = 'IIS APPPOOL\SBAppPool')
BEGIN
    CREATE LOGIN [IIS APPPOOL\SBAppPool] 
      FROM WINDOWS WITH DEFAULT_DATABASE=[master], 
      DEFAULT_LANGUAGE=[us_english]
END
GO
DROP USER [SBInventoryUser]
CREATE USER [SBInventoryUser] 
  FOR LOGIN [IIS APPPOOL\SBAppPool]
GO
EXEC sp_addrolemember 'db_owner', 'SBInventoryUser'
GO