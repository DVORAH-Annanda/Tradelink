CREATE VIEW dbo.vw_SalesAnalysis
AS
SELECT
    SOH.TLSOH_Pk AS StockOnHandId,
    SOH.TLSOH_Style_FK AS StyleId,
    STY.Sty_Description AS Style,
    SOH.TLSOH_Colour_FK AS ColourId,
    COL.Col_Display AS Colour,
    SOH.TLSOH_Size_FK AS SizeId,
    SZ.SI_Description AS Size,
    SZ.SI_DisplayOrder AS SizeDisplayOrder,
    SOH.TLSOH_Customer_Fk AS CustomerId,
    CUST.Cust_Description AS Customer,
    SOH.TLSOH_WareHouse_FK AS WarehouseId,
    WH.WhStore_Description AS Warehouse,
    SOH.TLSOH_Sold AS Sold,
    SOH.TLSOH_SoldDate AS SoldDate,
    CASE WHEN SOH.TLSOH_Sold = 1 THEN ISNULL(SOH.TLSOH_BoxedQty, 0) ELSE 0 END AS GrossSalesQty,
    SOH.TLSOH_Returned AS Returned,
    SOH.TLSOH_ReturnedDate AS ReturnedDate,
    SOH.TLSOH_ReturnNumber AS ReturnNumber,
    CASE WHEN SOH.TLSOH_Returned = 1 THEN ISNULL(SOH.TLSOH_ReturnedBoxQty, 0) ELSE 0 END AS ReturnedQty
FROM dbo.TLCSV_StockOnHand SOH
LEFT JOIN dbo.TLADM_Styles STY ON SOH.TLSOH_Style_FK = STY.Sty_Id
LEFT JOIN dbo.TLADM_Colours COL ON SOH.TLSOH_Colour_FK = COL.Col_Id
LEFT JOIN dbo.TLADM_Sizes SZ ON SOH.TLSOH_Size_FK = SZ.SI_id
LEFT JOIN dbo.TLADM_CustomerFile CUST ON SOH.TLSOH_Customer_Fk = CUST.Cust_Pk
LEFT JOIN dbo.TLADM_WhseStore WH ON SOH.TLSOH_WareHouse_FK = WH.WhStore_Id
WHERE SOH.TLSOH_Sold = 1 OR SOH.TLSOH_Returned = 1;
