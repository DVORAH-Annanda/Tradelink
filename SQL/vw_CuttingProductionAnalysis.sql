USE [TTI2]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('dbo.vw_CuttingProductionAnalysis', 'V') IS NOT NULL
    DROP VIEW dbo.vw_CuttingProductionAnalysis
GO

CREATE VIEW dbo.vw_CuttingProductionAnalysis
AS
WITH ActualAgg AS
(
    SELECT
        RD.TLCUTSHRD_CutSheet_FK AS ReceiptPk,
        SUM(ISNULL(RD.TLCUTSHRD_BoxUnits, 0)) AS ActualQty
    FROM dbo.TLCUT_CutSheetReceiptDetail RD
    GROUP BY RD.TLCUTSHRD_CutSheet_FK
),
ExpectedAgg AS
(
    SELECT
        E.TLCUTE_CutSheet_FK AS CutSheetPk,
        SUM(ISNULL(E.TLCUTE_NoofGarments, 0)) AS ExpectedQty,
        COUNT(*) AS ExpectedLineCount,
        MIN(E.TLCUTE_Size_FK) AS SingleSizeFk
    FROM dbo.TLCUT_ExpectedUnits E
    GROUP BY E.TLCUTE_CutSheet_FK
),
FabWeightAgg AS
(
    SELECT
        D.TLCutSHD_CutSheet_FK AS CutSheetPk,
        SUM(
            CASE
                WHEN D.TLCUTSHD_Body = 1
                    THEN ISNULL(D.TLCUTSHD_NettWeight, 0)
                ELSE 0
            END
        ) AS FabNetWeight
    FROM dbo.TLCUT_CutSheetDetail D
    GROUP BY D.TLCutSHD_CutSheet_FK
)
SELECT
    R.TLCUTSHR_Pk AS ReceiptPk,
    CS.TLCutSH_Pk AS CutSheetPk,
    CS.TLCutSH_No AS CutSheetNo,
    R.TLCUTSHR_DateIntoPanelStore AS ProductionDate,
    DB.DYEB_BatchNo AS DyeBatchNo,
    M.MD_Description AS Machine,
    G.TLGreige_Description AS Quality,
    ST.Sty_Description AS Style,
    C.Col_Display AS Colour,
    CASE
        WHEN ISNULL(EA.ExpectedLineCount, 0) = 0 THEN ''
        WHEN EA.ExpectedLineCount = 1 THEN ISNULL(SZ.SI_Description, '')
        ELSE 'MM'
    END AS Size,
    ISNULL(FW.FabNetWeight, 0) AS FabNetWeight,
    ISNULL(EA.ExpectedQty, 0) AS ExpectedQty,
    ISNULL(AA.ActualQty, 0) AS ActualQty,
    CS.TLCutSH_Closed AS IsClosed,
    R.TLCUTSHR_Machine_FK AS MachinePk,
    CS.TLCutSH_Styles_FK AS StylePk,
    CS.TLCutSH_Colour_FK AS ColourPk
FROM dbo.TLCUT_CutSheetReceipt R
INNER JOIN dbo.TLCUT_CutSheet CS
    ON R.TLCUTSHR_CutSheet_FK = CS.TLCutSH_Pk
LEFT JOIN ActualAgg AA
    ON R.TLCUTSHR_Pk = AA.ReceiptPk
LEFT JOIN ExpectedAgg EA
    ON CS.TLCutSH_Pk = EA.CutSheetPk
LEFT JOIN FabWeightAgg FW
    ON CS.TLCutSH_Pk = FW.CutSheetPk
LEFT JOIN dbo.TLDYE_DyeBatch DB
    ON CS.TLCutSH_DyeBatch_FK = DB.DYEB_Pk
LEFT JOIN dbo.TLADM_MachineDefinitions M
    ON R.TLCUTSHR_Machine_FK = M.MD_Pk
LEFT JOIN dbo.TLADM_Griege G
    ON DB.DYEB_Greige_FK = G.TLGreige_Id
LEFT JOIN dbo.TLADM_Styles ST
    ON CS.TLCutSH_Styles_FK = ST.Sty_Id
LEFT JOIN dbo.TLADM_Colours C
    ON CS.TLCutSH_Colour_FK = C.Col_Id
LEFT JOIN dbo.TLADM_Sizes SZ
    ON EA.SingleSizeFk = SZ.SI_Id
WHERE R.TLCUTSHR_DateIntoPanelStore >=
      DATEADD(MONTH, -12, CAST(GETDATE() AS DATE));
GO
