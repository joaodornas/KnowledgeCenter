USE PUBMED
GO

/*INSERT INTO PUBMEDXML(XMLDATA, LOADEDDATETIME)
	SELECT CONVERT(XML, BulkColumn,2) AS BulkColumn, GETDATE() 
		FROM OPENROWSET(BULK 'E:\PUBMED_tmp\pubmed18n0001.xml', SINGLE_BLOB) as x;*/

SELECT * FROM PUBMEDXML