-- Spinning IPI: database read-only cross-check (SQL Server 2008 R2 compatible)
-- Use this alongside Spinning > Quality Results by Spinning Machine,
-- measurement option 2, for the SAME date, machine and yarn order.
-- No view or table changes are required.

DECLARE @FromDate datetime;
DECLARE @ToDateExclusive datetime;
SET @FromDate = '20260101';
SET @ToDateExclusive = '20261101';

SELECT TOP (100)
       q.YarnQA_Pk AS MeasurementId,
       q.YarnQA_Date AS TestDate,
       md.MD_MachineCode AS Machine,
       cm.TLCTM_Description AS MergeName,
       yo.YarnO_OrderNumber AS YarnOrder,
       yt.YA_TexCount AS NominalTex,
       q.YarnQA_08H00 AS MeasuredTex,
       q.YarnQA_10H00 AS CvPercentage,
       q.YarnQA_12H00 AS Thin,
       q.YarnQA_14H00 AS Thick,
       q.YarnQA_16H00 AS Nep,
       q.YarnQA_18H00 AS StoredIpi,
       (q.YarnQA_12H00 + q.YarnQA_14H00 + q.YarnQA_16H00) AS ComponentsTotal
FROM dbo.TLSPN_QAMeasurements q
LEFT JOIN dbo.TLSPN_YarnOrder yo
    ON yo.YarnO_Pk = q.YarnQA_YarnOrder_FK
LEFT JOIN dbo.TLSPN_CottonMerge cm
    ON cm.TLCTM_Pk = yo.YarnO_MergeContract_FK
LEFT JOIN dbo.TLADM_Yarn yt
    ON yt.YA_Id = yo.Yarno_YarnType_FK
LEFT JOIN dbo.TLADM_MachineDefinitions md
    ON md.MD_Pk = q.YarnQA_MachineNo_FK
WHERE q.YarnQA_MeasureNo = 2
  AND q.YarnQA_Date >= @FromDate
  AND q.YarnQA_Date < @ToDateExclusive
ORDER BY q.YarnQA_Date DESC, q.YarnQA_Pk DESC;
