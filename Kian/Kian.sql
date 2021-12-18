;WITH lasts AS
(
   SELECT *,
		ROW_NUMBER() OVER (PARTITION BY InstrumentID ORDER BY DateTimeEn DESC) AS rn
   FROM Trade
)
SELECT 
	lasts.ID, 
	ins.ShortName, 
	InstrumentID, 
	DateTimeEn, 
	OpenPrice, 
	HighPrice, 
	LowPrice, 
	ClosePrice, 
	RealClosePrice
INTO LastTrades
FROM lasts
JOIN Instrument ins
ON lasts.InstrumentID = ins.ID
WHERE rn = 1