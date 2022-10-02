CREATE VIEW [dbo].[sales_summary_view]
	AS SELECT  DATEPART(YEAR, TimeOfSale) as year,
        sum(SalesPrice) as sales,
        count(id) items_sold,
        Closing_month_name = DateName( month , DateAdd( month , DATEPART(Month , TimeOfSale) , -1 ))
FROM    sales
GROUP BY DATEPART(YEAR, TimeOfSale), DATEPART(MONTH, TimeOfSale);
