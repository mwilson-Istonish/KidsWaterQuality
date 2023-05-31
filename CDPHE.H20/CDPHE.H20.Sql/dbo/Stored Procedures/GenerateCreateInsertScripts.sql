CREATE PROCEDURE GenerateCreateInsertScripts
    @TableName NVARCHAR(128)
AS
BEGIN
    --Generate the CREATE TABLE script
    DECLARE @SQL NVARCHAR(MAX) = '';
    DECLARE @ColumnList NVARCHAR(MAX) = '';
    SELECT @SQL = @SQL + ', ' + QUOTENAME(c.COLUMN_NAME) + ' ' +
                  c.DATA_TYPE + 
                  CASE WHEN c.CHARACTER_MAXIMUM_LENGTH IS NOT NULL 
                       THEN '(' + CAST(c.CHARACTER_MAXIMUM_LENGTH AS NVARCHAR) + ')' 
                       ELSE '' END +
                  CASE WHEN c.IS_NULLABLE = 'NO' THEN ' NOT NULL' ELSE ' NULL' END
    FROM INFORMATION_SCHEMA.COLUMNS AS c
    WHERE c.TABLE_NAME = @TableName;

    SET @SQL = 'CREATE TABLE ' + QUOTENAME(@TableName) + ' (' + STUFF(@SQL, 1, 1, '') + ');';

    --Generate the INSERT scripts
    SELECT @ColumnList = @ColumnList + QUOTENAME(c.COLUMN_NAME) + ','
    FROM INFORMATION_SCHEMA.COLUMNS AS c
    WHERE c.TABLE_NAME = @TableName;
    
    SET @ColumnList = LEFT(@ColumnList, LEN(@ColumnList) - 1);
    SET @SQL = @SQL + ' INSERT INTO ' + QUOTENAME(@TableName) + '(' + @ColumnList + ') VALUES (' +
               STUFF((SELECT ', ' + QUOTENAME(c.COLUMN_NAME) 
                      FROM INFORMATION_SCHEMA.COLUMNS AS c
                      WHERE c.TABLE_NAME = @TableName
                      FOR XML PATH('')), 1, 1, '') + ');';

    EXEC sp_executesql @SQL;
END;