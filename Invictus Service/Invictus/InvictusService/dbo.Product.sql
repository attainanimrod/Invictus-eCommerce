CREATE TABLE [dbo].[Product] (
    [Prod_ID]          INT  IDENTITY (1, 1)             NOT NULL,
    [Prod_Name]        VARCHAR(MAX) NOT NULL,
    [Prod_Image]       VARCHAR (MAX)   NOT NULL,
    [Prod_Desciption]  VARCHAR (MAX)   NOT NULL,
    [Prod_Price]       MONEY           NOT NULL,
    [Prod_Quantity]    INT             NOT NULL,
    Category Varchar(MAX) Not Null,
    [DISC_DiscPercent] DECIMAL (3, 2)  NULL,
	Extra_Image1 varchar(MAX),
	Extra_Image2 varchar(MAX),
	Extra_Image3 varchar(MAX),
    [DISC_Active]      INT             NOT NULL,
    [Status]           INT             NOT NULL,
    PRIMARY KEY CLUSTERED ([Prod_ID] ASC),
   
);

