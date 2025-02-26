
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 02/13/2025 14:48:04
-- Generated from EDMX file: C:\Users\Hilda\Documents\Visual Studio 2022\Projects\TradeLink_Source\Utilities\TTI2Entities.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [TTI2];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_TLADM_ConsumablesOther_TLADM_ConsumableGroups]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TLADM_ConsumablesOther] DROP CONSTRAINT [FK_TLADM_ConsumablesOther_TLADM_ConsumableGroups];
GO
IF OBJECT_ID(N'[dbo].[FK_TLADM_ConsumablesOther_TLADM_Sizes]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TLADM_ConsumablesOther] DROP CONSTRAINT [FK_TLADM_ConsumablesOther_TLADM_Sizes];
GO
IF OBJECT_ID(N'[dbo].[FK_TLADM_ConsumablesOther_TLADM_StoreTypes]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TLADM_ConsumablesOther] DROP CONSTRAINT [FK_TLADM_ConsumablesOther_TLADM_StoreTypes];
GO
IF OBJECT_ID(N'[dbo].[FK_TLADM_ConsumablesOther_TLADM_UOM]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TLADM_ConsumablesOther] DROP CONSTRAINT [FK_TLADM_ConsumablesOther_TLADM_UOM];
GO
IF OBJECT_ID(N'[dbo].[FK_TLADM_ConsumablesOther_TLADM_UOM1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TLADM_ConsumablesOther] DROP CONSTRAINT [FK_TLADM_ConsumablesOther_TLADM_UOM1];
GO
IF OBJECT_ID(N'[dbo].[FK_TLADM_CustomerFile_TLADM_CustomerTypes]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TLADM_CustomerFile] DROP CONSTRAINT [FK_TLADM_CustomerFile_TLADM_CustomerTypes];
GO
IF OBJECT_ID(N'[dbo].[FK_TLADM_FabricType_TLADM_FabricProduct]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TLADM_FabricType] DROP CONSTRAINT [FK_TLADM_FabricType_TLADM_FabricProduct];
GO
IF OBJECT_ID(N'[dbo].[FK_TLADM_NonStockItems_TLADM_NonStockCat]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TLADM_NonStockItems] DROP CONSTRAINT [FK_TLADM_NonStockItems_TLADM_NonStockCat];
GO
IF OBJECT_ID(N'[dbo].[FK_TLADM_NonStockItems_TLADM_StockTypes]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TLADM_NonStockItems] DROP CONSTRAINT [FK_TLADM_NonStockItems_TLADM_StockTypes];
GO
IF OBJECT_ID(N'[dbo].[FK_TLADM_NonStockItems_TLADM_UOM]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TLADM_NonStockItems] DROP CONSTRAINT [FK_TLADM_NonStockItems_TLADM_UOM];
GO
IF OBJECT_ID(N'[dbo].[FK_TLADM_QualityDefinition_TLADM_RejectReasons1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TLADM_QualityDefinition] DROP CONSTRAINT [FK_TLADM_QualityDefinition_TLADM_RejectReasons1];
GO
IF OBJECT_ID(N'[dbo].[FK_TLADM_Suppliers_TLADM_ProductTypes]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TLADM_Suppliers] DROP CONSTRAINT [FK_TLADM_Suppliers_TLADM_ProductTypes];
GO
IF OBJECT_ID(N'[dbo].[FK_TLSPN_YarnProductionPerMachine_TLADM_MachineDefinitions]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TLSPN_YarnProductionPerMachine] DROP CONSTRAINT [FK_TLSPN_YarnProductionPerMachine_TLADM_MachineDefinitions];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[sysdiagrams]', 'U') IS NOT NULL
    DROP TABLE [dbo].[sysdiagrams];
GO
IF OBJECT_ID(N'[dbo].[TLADM_AdditionalAddress]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLADM_AdditionalAddress];
GO
IF OBJECT_ID(N'[dbo].[TLADM_AuxColours]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLADM_AuxColours];
GO
IF OBJECT_ID(N'[dbo].[TLADM_BoxType_Packing_Specifications]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLADM_BoxType_Packing_Specifications];
GO
IF OBJECT_ID(N'[dbo].[TLADM_BoxTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLADM_BoxTypes];
GO
IF OBJECT_ID(N'[dbo].[TLADM_CMTMeasurementPoints]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLADM_CMTMeasurementPoints];
GO
IF OBJECT_ID(N'[dbo].[TLADM_CMTNonCompliance]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLADM_CMTNonCompliance];
GO
IF OBJECT_ID(N'[dbo].[TLADM_Colours]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLADM_Colours];
GO
IF OBJECT_ID(N'[dbo].[TLADM_CompanyDetails]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLADM_CompanyDetails];
GO
IF OBJECT_ID(N'[dbo].[TLADM_ConsumableGroups]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLADM_ConsumableGroups];
GO
IF OBJECT_ID(N'[dbo].[TLADM_ConsumablesDC]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLADM_ConsumablesDC];
GO
IF OBJECT_ID(N'[dbo].[TLADM_ConsumablesOther]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLADM_ConsumablesOther];
GO
IF OBJECT_ID(N'[dbo].[TLADM_Cotton]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLADM_Cotton];
GO
IF OBJECT_ID(N'[dbo].[TLADM_CottonAgent]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLADM_CottonAgent];
GO
IF OBJECT_ID(N'[dbo].[TLADM_CottonContracts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLADM_CottonContracts];
GO
IF OBJECT_ID(N'[dbo].[TLADM_CottonHauliers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLADM_CottonHauliers];
GO
IF OBJECT_ID(N'[dbo].[TLADM_CottonHauliersVehicles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLADM_CottonHauliersVehicles];
GO
IF OBJECT_ID(N'[dbo].[TLADM_CottonOrigin]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLADM_CottonOrigin];
GO
IF OBJECT_ID(N'[dbo].[TLADM_CustomerAccess]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLADM_CustomerAccess];
GO
IF OBJECT_ID(N'[dbo].[TLADM_CustomerFile]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLADM_CustomerFile];
GO
IF OBJECT_ID(N'[dbo].[TLADM_CustomerTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLADM_CustomerTypes];
GO
IF OBJECT_ID(N'[dbo].[TLADM_CutAreaLocations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLADM_CutAreaLocations];
GO
IF OBJECT_ID(N'[dbo].[TLADM_CutFleeceCuffs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLADM_CutFleeceCuffs];
GO
IF OBJECT_ID(N'[dbo].[TLADM_CutFleeceWaist]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLADM_CutFleeceWaist];
GO
IF OBJECT_ID(N'[dbo].[TLADM_CutMeasureArea]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLADM_CutMeasureArea];
GO
IF OBJECT_ID(N'[dbo].[TLADM_CutMeasureStandards]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLADM_CutMeasureStandards];
GO
IF OBJECT_ID(N'[dbo].[TLADM_CutTrims]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLADM_CutTrims];
GO
IF OBJECT_ID(N'[dbo].[TLADM_DailyLog]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLADM_DailyLog];
GO
IF OBJECT_ID(N'[dbo].[TLADM_Departments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLADM_Departments];
GO
IF OBJECT_ID(N'[dbo].[TLADM_DepartmentsArea]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLADM_DepartmentsArea];
GO
IF OBJECT_ID(N'[dbo].[TLADM_DepartmentsAreaTransaction]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLADM_DepartmentsAreaTransaction];
GO
IF OBJECT_ID(N'[dbo].[TLADM_DyeQDCodes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLADM_DyeQDCodes];
GO
IF OBJECT_ID(N'[dbo].[TLADM_DyeRemendyCodes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLADM_DyeRemendyCodes];
GO
IF OBJECT_ID(N'[dbo].[TLADM_FabricAttributes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLADM_FabricAttributes];
GO
IF OBJECT_ID(N'[dbo].[TLADM_FabricProduct]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLADM_FabricProduct];
GO
IF OBJECT_ID(N'[dbo].[TLADM_FabricType]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLADM_FabricType];
GO
IF OBJECT_ID(N'[dbo].[TLADM_FabricWeight]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLADM_FabricWeight];
GO
IF OBJECT_ID(N'[dbo].[TLADM_FabWidth]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLADM_FabWidth];
GO
IF OBJECT_ID(N'[dbo].[TLADM_FinishedGoods]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLADM_FinishedGoods];
GO
IF OBJECT_ID(N'[dbo].[TLADM_GarmentDef]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLADM_GarmentDef];
GO
IF OBJECT_ID(N'[dbo].[TLADM_GarmentDefectCodes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLADM_GarmentDefectCodes];
GO
IF OBJECT_ID(N'[dbo].[TLADM_Greige_Yarn]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLADM_Greige_Yarn];
GO
IF OBJECT_ID(N'[dbo].[TLADM_GreigeColour]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLADM_GreigeColour];
GO
IF OBJECT_ID(N'[dbo].[TLADM_GreigeQuality]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLADM_GreigeQuality];
GO
IF OBJECT_ID(N'[dbo].[TLADM_Griege]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLADM_Griege];
GO
IF OBJECT_ID(N'[dbo].[TLADM_Labels]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLADM_Labels];
GO
IF OBJECT_ID(N'[dbo].[TLADM_LastNumberUsed]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLADM_LastNumberUsed];
GO
IF OBJECT_ID(N'[dbo].[TLADM_MachineDefinitions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLADM_MachineDefinitions];
GO
IF OBJECT_ID(N'[dbo].[TLADM_MachineMaintenance]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLADM_MachineMaintenance];
GO
IF OBJECT_ID(N'[dbo].[TLADM_MachineMaintenanceTasks]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLADM_MachineMaintenanceTasks];
GO
IF OBJECT_ID(N'[dbo].[TLADM_MachineOperators]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLADM_MachineOperators];
GO
IF OBJECT_ID(N'[dbo].[TLADM_Months]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLADM_Months];
GO
IF OBJECT_ID(N'[dbo].[TLADM_NonStockCat]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLADM_NonStockCat];
GO
IF OBJECT_ID(N'[dbo].[TLADM_NonStockItems]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLADM_NonStockItems];
GO
IF OBJECT_ID(N'[dbo].[TLADM_PanelAttributes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLADM_PanelAttributes];
GO
IF OBJECT_ID(N'[dbo].[TLADM_ProductionLoss]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLADM_ProductionLoss];
GO
IF OBJECT_ID(N'[dbo].[TLADM_ProductRating]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLADM_ProductRating];
GO
IF OBJECT_ID(N'[dbo].[TLADM_ProductRating_Detail]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLADM_ProductRating_Detail];
GO
IF OBJECT_ID(N'[dbo].[TLADM_ProductTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLADM_ProductTypes];
GO
IF OBJECT_ID(N'[dbo].[TLADM_QADyeProcess]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLADM_QADyeProcess];
GO
IF OBJECT_ID(N'[dbo].[TLADM_QADyeProcessFields]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLADM_QADyeProcessFields];
GO
IF OBJECT_ID(N'[dbo].[TLADM_QualityDefinition]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLADM_QualityDefinition];
GO
IF OBJECT_ID(N'[dbo].[TLADM_RejectReasons]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLADM_RejectReasons];
GO
IF OBJECT_ID(N'[dbo].[TLADM_Ribbing]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLADM_Ribbing];
GO
IF OBJECT_ID(N'[dbo].[TLADM_Shifts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLADM_Shifts];
GO
IF OBJECT_ID(N'[dbo].[TLADM_Sizes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLADM_Sizes];
GO
IF OBJECT_ID(N'[dbo].[TLADM_StandardProduct]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLADM_StandardProduct];
GO
IF OBJECT_ID(N'[dbo].[TLADM_StockTakeFreq]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLADM_StockTakeFreq];
GO
IF OBJECT_ID(N'[dbo].[TLADM_StockTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLADM_StockTypes];
GO
IF OBJECT_ID(N'[dbo].[TLADM_StoreBal]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLADM_StoreBal];
GO
IF OBJECT_ID(N'[dbo].[TLADM_StoreTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLADM_StoreTypes];
GO
IF OBJECT_ID(N'[dbo].[TLADM_StyAssoc]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLADM_StyAssoc];
GO
IF OBJECT_ID(N'[dbo].[TLADM_Style_Quality]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLADM_Style_Quality];
GO
IF OBJECT_ID(N'[dbo].[TLADM_StyleColour]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLADM_StyleColour];
GO
IF OBJECT_ID(N'[dbo].[TLADM_Styles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLADM_Styles];
GO
IF OBJECT_ID(N'[dbo].[TLADM_StylesAdditional]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLADM_StylesAdditional];
GO
IF OBJECT_ID(N'[dbo].[TLADM_StylesGrades]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLADM_StylesGrades];
GO
IF OBJECT_ID(N'[dbo].[TLADM_StyleTrim]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLADM_StyleTrim];
GO
IF OBJECT_ID(N'[dbo].[TLADM_Suppliers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLADM_Suppliers];
GO
IF OBJECT_ID(N'[dbo].[TLADM_TranactionType]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLADM_TranactionType];
GO
IF OBJECT_ID(N'[dbo].[TLADM_Transporters]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLADM_Transporters];
GO
IF OBJECT_ID(N'[dbo].[TLADM_Trims]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLADM_Trims];
GO
IF OBJECT_ID(N'[dbo].[TLADM_UOM]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLADM_UOM];
GO
IF OBJECT_ID(N'[dbo].[TLADM_WareHouseAssociation]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLADM_WareHouseAssociation];
GO
IF OBJECT_ID(N'[dbo].[TLADM_WhseStore]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLADM_WhseStore];
GO
IF OBJECT_ID(N'[dbo].[TLADM_Yarn]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLADM_Yarn];
GO
IF OBJECT_ID(N'[dbo].[TLCMT_AuditMeasurements]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLCMT_AuditMeasurements];
GO
IF OBJECT_ID(N'[dbo].[TLCMT_AuditMeasureRecorded]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLCMT_AuditMeasureRecorded];
GO
IF OBJECT_ID(N'[dbo].[TLCMT_BodyMeasureRP]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLCMT_BodyMeasureRP];
GO
IF OBJECT_ID(N'[dbo].[TLCMT_CompletedWork]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLCMT_CompletedWork];
GO
IF OBJECT_ID(N'[dbo].[TLCMT_DeflectFlaw]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLCMT_DeflectFlaw];
GO
IF OBJECT_ID(N'[dbo].[TLCMT_FactConfig]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLCMT_FactConfig];
GO
IF OBJECT_ID(N'[dbo].[TLCMT_HistoryBoxedQty]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLCMT_HistoryBoxedQty];
GO
IF OBJECT_ID(N'[dbo].[TLCMT_LineFeederBundleCheck]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLCMT_LineFeederBundleCheck];
GO
IF OBJECT_ID(N'[dbo].[TLCMT_LineIssue]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLCMT_LineIssue];
GO
IF OBJECT_ID(N'[dbo].[TLCMT_NonCompliance]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLCMT_NonCompliance];
GO
IF OBJECT_ID(N'[dbo].[TLCMT_PanelIssue]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLCMT_PanelIssue];
GO
IF OBJECT_ID(N'[dbo].[TLCMT_PanelIssueDetail]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLCMT_PanelIssueDetail];
GO
IF OBJECT_ID(N'[dbo].[TLCMT_ProductDefects]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLCMT_ProductDefects];
GO
IF OBJECT_ID(N'[dbo].[TLCMT_ProductionCosts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLCMT_ProductionCosts];
GO
IF OBJECT_ID(N'[dbo].[TLCMT_ProductionFaults]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLCMT_ProductionFaults];
GO
IF OBJECT_ID(N'[dbo].[TLCMT_Statistics]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLCMT_Statistics];
GO
IF OBJECT_ID(N'[dbo].[TLCMT_UnitProductionTargets]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLCMT_UnitProductionTargets];
GO
IF OBJECT_ID(N'[dbo].[TLCSV_BoughtInGoods]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLCSV_BoughtInGoods];
GO
IF OBJECT_ID(N'[dbo].[TLCSV_BoxConfiguration]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLCSV_BoxConfiguration];
GO
IF OBJECT_ID(N'[dbo].[TLCSV_BoxSelected]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLCSV_BoxSelected];
GO
IF OBJECT_ID(N'[dbo].[TLCSV_BoxSplit]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLCSV_BoxSplit];
GO
IF OBJECT_ID(N'[dbo].[TLCSV_MergePODetail]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLCSV_MergePODetail];
GO
IF OBJECT_ID(N'[dbo].[TLCSV_Movement]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLCSV_Movement];
GO
IF OBJECT_ID(N'[dbo].[TLCSV_OrderAllocated]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLCSV_OrderAllocated];
GO
IF OBJECT_ID(N'[dbo].[TLCSV_PickingListMaster]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLCSV_PickingListMaster];
GO
IF OBJECT_ID(N'[dbo].[TLCSV_PuchaseOrderDetail]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLCSV_PuchaseOrderDetail];
GO
IF OBJECT_ID(N'[dbo].[TLCSV_PurchaseOrder]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLCSV_PurchaseOrder];
GO
IF OBJECT_ID(N'[dbo].[TLCSV_RePackConfig]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLCSV_RePackConfig];
GO
IF OBJECT_ID(N'[dbo].[TLCSV_RePackTransactions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLCSV_RePackTransactions];
GO
IF OBJECT_ID(N'[dbo].[TLCSV_StockOnHand]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLCSV_StockOnHand];
GO
IF OBJECT_ID(N'[dbo].[TLCSV_WhseTransfer]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLCSV_WhseTransfer];
GO
IF OBJECT_ID(N'[dbo].[TLCSV_WhseTransferDetail]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLCSV_WhseTransferDetail];
GO
IF OBJECT_ID(N'[dbo].[TLCUT_CutFleeceStats]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLCUT_CutFleeceStats];
GO
IF OBJECT_ID(N'[dbo].[TLCUT_CutMeasureActuals]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLCUT_CutMeasureActuals];
GO
IF OBJECT_ID(N'[dbo].[TLCUT_CutSheet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLCUT_CutSheet];
GO
IF OBJECT_ID(N'[dbo].[TLCUT_CutSheetDetail]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLCUT_CutSheetDetail];
GO
IF OBJECT_ID(N'[dbo].[TLCUT_CutSheetReceipt]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLCUT_CutSheetReceipt];
GO
IF OBJECT_ID(N'[dbo].[TLCUT_CutSheetReceiptBoxes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLCUT_CutSheetReceiptBoxes];
GO
IF OBJECT_ID(N'[dbo].[TLCUT_CutSheetReceiptDetail]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLCUT_CutSheetReceiptDetail];
GO
IF OBJECT_ID(N'[dbo].[TLCUT_ExpectedUnits]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLCUT_ExpectedUnits];
GO
IF OBJECT_ID(N'[dbo].[TLCUT_FabricReturns]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLCUT_FabricReturns];
GO
IF OBJECT_ID(N'[dbo].[TLCUT_QAResults]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLCUT_QAResults];
GO
IF OBJECT_ID(N'[dbo].[TLCUT_QCBerrie]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLCUT_QCBerrie];
GO
IF OBJECT_ID(N'[dbo].[TLCUT_RejectReasons]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLCUT_RejectReasons];
GO
IF OBJECT_ID(N'[dbo].[TLCUT_TrimsOnCut]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLCUT_TrimsOnCut];
GO
IF OBJECT_ID(N'[dbo].[TLDYE_AllocatedOperator]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLDYE_AllocatedOperator];
GO
IF OBJECT_ID(N'[dbo].[TLDYE_BIFInTransit]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLDYE_BIFInTransit];
GO
IF OBJECT_ID(N'[dbo].[TLDYE_BIFInTransitDetails]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLDYE_BIFInTransitDetails];
GO
IF OBJECT_ID(N'[dbo].[TLDYE_ComDyeBatch]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLDYE_ComDyeBatch];
GO
IF OBJECT_ID(N'[dbo].[TLDYE_ComDyeBatchDetails]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLDYE_ComDyeBatchDetails];
GO
IF OBJECT_ID(N'[dbo].[TLDYE_ConsumableSOH]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLDYE_ConsumableSOH];
GO
IF OBJECT_ID(N'[dbo].[TLDYE_ConSummableReceived]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLDYE_ConSummableReceived];
GO
IF OBJECT_ID(N'[dbo].[TLDYE_DefinitionDetails]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLDYE_DefinitionDetails];
GO
IF OBJECT_ID(N'[dbo].[TLDYE_DyeBatch]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLDYE_DyeBatch];
GO
IF OBJECT_ID(N'[dbo].[TLDYE_DyeBatchAllocated]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLDYE_DyeBatchAllocated];
GO
IF OBJECT_ID(N'[dbo].[TLDYE_DyeBatchDetails]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLDYE_DyeBatchDetails];
GO
IF OBJECT_ID(N'[dbo].[TLDYE_DyeingStandards]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLDYE_DyeingStandards];
GO
IF OBJECT_ID(N'[dbo].[TLDYE_DyeOrder]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLDYE_DyeOrder];
GO
IF OBJECT_ID(N'[dbo].[TLDYE_DyeOrderDetails]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLDYE_DyeOrderDetails];
GO
IF OBJECT_ID(N'[dbo].[TLDYE_DyeOrderFabric]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLDYE_DyeOrderFabric];
GO
IF OBJECT_ID(N'[dbo].[TLDYE_DyeTransactions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLDYE_DyeTransactions];
GO
IF OBJECT_ID(N'[dbo].[TLDYE_GarmentDyeingProduction]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLDYE_GarmentDyeingProduction];
GO
IF OBJECT_ID(N'[dbo].[TLDYE_NonCompliance]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLDYE_NonCompliance];
GO
IF OBJECT_ID(N'[dbo].[TLDYE_NonComplianceAnalysis]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLDYE_NonComplianceAnalysis];
GO
IF OBJECT_ID(N'[dbo].[TLDYE_NonComplianceConsDetail]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLDYE_NonComplianceConsDetail];
GO
IF OBJECT_ID(N'[dbo].[TLDYE_NonComplianceDetail]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLDYE_NonComplianceDetail];
GO
IF OBJECT_ID(N'[dbo].[TLDye_QualityException]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLDye_QualityException];
GO
IF OBJECT_ID(N'[dbo].[TLDYE_ReceipeGreigeQual]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLDYE_ReceipeGreigeQual];
GO
IF OBJECT_ID(N'[dbo].[TLDYE_RecipeColourDefinition]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLDYE_RecipeColourDefinition];
GO
IF OBJECT_ID(N'[dbo].[TLDYE_RecipeDefinition]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLDYE_RecipeDefinition];
GO
IF OBJECT_ID(N'[dbo].[TLDYE_RFDHistory]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLDYE_RFDHistory];
GO
IF OBJECT_ID(N'[dbo].[TLFIN_Calendar]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLFIN_Calendar];
GO
IF OBJECT_ID(N'[dbo].[TLFIN_FinancialYear]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLFIN_FinancialYear];
GO
IF OBJECT_ID(N'[dbo].[TLKNI_BoughtInFabric]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLKNI_BoughtInFabric];
GO
IF OBJECT_ID(N'[dbo].[TLKNI_GreigeCommisionAdjustment]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLKNI_GreigeCommisionAdjustment];
GO
IF OBJECT_ID(N'[dbo].[TLKNI_GreigeCommissionTransctions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLKNI_GreigeCommissionTransctions];
GO
IF OBJECT_ID(N'[dbo].[TLKNI_GreigeProduction]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLKNI_GreigeProduction];
GO
IF OBJECT_ID(N'[dbo].[TLKNI_GreigeTransactions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLKNI_GreigeTransactions];
GO
IF OBJECT_ID(N'[dbo].[TLKNI_MachineLastNumber]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLKNI_MachineLastNumber];
GO
IF OBJECT_ID(N'[dbo].[TLKNI_Order]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLKNI_Order];
GO
IF OBJECT_ID(N'[dbo].[TLKNI_ProductionSplit]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLKNI_ProductionSplit];
GO
IF OBJECT_ID(N'[dbo].[TLKNI_YarnAllocTransctions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLKNI_YarnAllocTransctions];
GO
IF OBJECT_ID(N'[dbo].[TLKNI_YarnOrderPallets]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLKNI_YarnOrderPallets];
GO
IF OBJECT_ID(N'[dbo].[TLKNI_YarnTransaction]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLKNI_YarnTransaction];
GO
IF OBJECT_ID(N'[dbo].[TLKNI_YarnTransactionDetails]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLKNI_YarnTransactionDetails];
GO
IF OBJECT_ID(N'[dbo].[TLPPS_InterDept]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLPPS_InterDept];
GO
IF OBJECT_ID(N'[dbo].[TLPPS_ProductionLeadTime]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLPPS_ProductionLeadTime];
GO
IF OBJECT_ID(N'[dbo].[TLPPS_Replenishment]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLPPS_Replenishment];
GO
IF OBJECT_ID(N'[dbo].[TLSEC_Departments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLSEC_Departments];
GO
IF OBJECT_ID(N'[dbo].[TLSEC_Sections]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLSEC_Sections];
GO
IF OBJECT_ID(N'[dbo].[TLSEC_UserAccess]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLSEC_UserAccess];
GO
IF OBJECT_ID(N'[dbo].[TLSEC_UserSections]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLSEC_UserSections];
GO
IF OBJECT_ID(N'[dbo].[TLSPN_CottonMerge]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLSPN_CottonMerge];
GO
IF OBJECT_ID(N'[dbo].[TLSPN_CottonMergeDetails]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLSPN_CottonMergeDetails];
GO
IF OBJECT_ID(N'[dbo].[TLSPN_CottonReceived]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLSPN_CottonReceived];
GO
IF OBJECT_ID(N'[dbo].[TLSPN_CottonReceivedBales]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLSPN_CottonReceivedBales];
GO
IF OBJECT_ID(N'[dbo].[TLSPN_CottonTransactions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLSPN_CottonTransactions];
GO
IF OBJECT_ID(N'[dbo].[TLSPN_OpenBalance]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLSPN_OpenBalance];
GO
IF OBJECT_ID(N'[dbo].[TLSPN_QAMeasurements]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLSPN_QAMeasurements];
GO
IF OBJECT_ID(N'[dbo].[TLSPN_YarnOrder]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLSPN_YarnOrder];
GO
IF OBJECT_ID(N'[dbo].[TLSPN_YarnOrderLayDown]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLSPN_YarnOrderLayDown];
GO
IF OBJECT_ID(N'[dbo].[TLSPN_YarnOrderPallets]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLSPN_YarnOrderPallets];
GO
IF OBJECT_ID(N'[dbo].[TLSPN_YarnProductionPerMachine]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLSPN_YarnProductionPerMachine];
GO
IF OBJECT_ID(N'[dbo].[TLSPN_YarnTransactions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLSPN_YarnTransactions];
GO
IF OBJECT_ID(N'[dbo].[TLSPN_YarnWaste]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TLSPN_YarnWaste];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'TLADM_ConsumableGroups'
CREATE TABLE [dbo].[TLADM_ConsumableGroups] (
    [ConG_Pk] int IDENTITY(1,1) NOT NULL,
    [ConG_ShortCode] varchar(5)  NOT NULL,
    [ConG_Description] varchar(50)  NOT NULL
);
GO

-- Creating table 'TLADM_ConsumablesOther'
CREATE TABLE [dbo].[TLADM_ConsumablesOther] (
    [ConsOther_Pk] int IDENTITY(1,1) NOT NULL,
    [ConsOther_Code] varchar(10)  NOT NULL,
    [ConsOther_Description] varchar(50)  NOT NULL,
    [ConsOther_UOM_FK] int  NOT NULL,
    [ConsOther_AUOM_FK] int  NOT NULL,
    [ConsOther_ConvFactor] decimal(18,6)  NOT NULL,
    [ConsOther_StockType_Fk] int  NOT NULL,
    [ConsOther_Store_FK] int  NOT NULL,
    [ConsOther_ShowQty] bit  NOT NULL,
    [ConsOther_Unit] int  NOT NULL,
    [ConsOther_ShowStdCost] bit  NOT NULL,
    [ConsOther_StdCost] decimal(18,6)  NOT NULL,
    [ConsOther_ReOrderLevel] int  NOT NULL,
    [ConsOther_EconomicReOrderQty] int  NOT NULL,
    [ConsOther_MinReOrderQty] int  NOT NULL,
    [ConsOther_DeliveryLeadTime] int  NOT NULL,
    [ConsOther_StockTakeFrequency_Fk] int  NOT NULL,
    [ConsOther_ConsGroup_FK] int  NOT NULL,
    [ConsOther_PreferredSupplier_FK] int  NOT NULL,
    [ConsOther_SizeCode_FK] int  NOT NULL,
    [ConsOther_Notes] varchar(max)  NULL,
    [ConsOther_Consignment] bit  NOT NULL
);
GO

-- Creating table 'TLADM_CustomerTypes'
CREATE TABLE [dbo].[TLADM_CustomerTypes] (
    [CT_Id] int IDENTITY(1,1) NOT NULL,
    [CT_ShortCode] varchar(5)  NOT NULL,
    [CT_Description] varchar(50)  NOT NULL
);
GO

-- Creating table 'TLADM_FabricType'
CREATE TABLE [dbo].[TLADM_FabricType] (
    [FT_Id] int IDENTITY(1,1) NOT NULL,
    [FT_Description] varchar(50)  NOT NULL,
    [FT_Product_FK] int  NOT NULL,
    [FT_PowerN] int  NOT NULL,
    [FT_TotalPN_Weight] int  NOT NULL,
    [FT_TotalPN_Width] int  NOT NULL
);
GO

-- Creating table 'TLADM_FabricWeight'
CREATE TABLE [dbo].[TLADM_FabricWeight] (
    [FWW_Id] int IDENTITY(1,1) NOT NULL,
    [FWW_Description] varchar(50)  NOT NULL,
    [FWW_PowerN] int  NOT NULL,
    [FWW_Discontinued] bit  NULL,
    [FWW_Discontinued_Date] datetime  NULL,
    [FWW_Calculation_Value] int  NOT NULL
);
GO

-- Creating table 'TLADM_FinishedGoods'
CREATE TABLE [dbo].[TLADM_FinishedGoods] (
    [Fin_Pk] int IDENTITY(1,1) NOT NULL,
    [Fin_Description] varchar(50)  NOT NULL
);
GO

-- Creating table 'TLADM_GarmentDef'
CREATE TABLE [dbo].[TLADM_GarmentDef] (
    [GarDef_Id] int IDENTITY(1,1) NOT NULL,
    [GarDef_Description] varchar(50)  NOT NULL,
    [GarDef_Alt_Description] varchar(50)  NULL,
    [GarDef_Label_FK] int  NOT NULL
);
GO

-- Creating table 'TLADM_GarmentDefectCodes'
CREATE TABLE [dbo].[TLADM_GarmentDefectCodes] (
    [Defect_Id] int IDENTITY(1,1) NOT NULL,
    [Defect_ShortCode] varchar(10)  NOT NULL,
    [Defect_Dept_Fk] int  NOT NULL,
    [Defect_Desacription] varchar(50)  NOT NULL
);
GO

-- Creating table 'TLADM_NonStockCat'
CREATE TABLE [dbo].[TLADM_NonStockCat] (
    [NSC_Pk] int IDENTITY(1,1) NOT NULL,
    [NSC_ShortCode] varchar(5)  NOT NULL,
    [NSC_Description] varchar(50)  NOT NULL
);
GO

-- Creating table 'TLADM_NonStockItems'
CREATE TABLE [dbo].[TLADM_NonStockItems] (
    [NSI_Pk] int IDENTITY(1,1) NOT NULL,
    [NSI_Code] varchar(10)  NOT NULL,
    [NSI_Description] varchar(50)  NOT NULL,
    [NSI_UOM_FK] int  NOT NULL,
    [NSI_StockType_FK] int  NOT NULL,
    [NSI_Department_PWN] int  NOT NULL,
    [NSI_Category_FK] int  NOT NULL,
    [NSI_ShowUnitCost] bit  NOT NULL,
    [NSI_UnitCost] decimal(19,4)  NOT NULL
);
GO

-- Creating table 'TLADM_ProductTypes'
CREATE TABLE [dbo].[TLADM_ProductTypes] (
    [PT_pk] int IDENTITY(1,1) NOT NULL,
    [PT_ShortCode] varchar(5)  NOT NULL,
    [PT_Description] varchar(50)  NOT NULL,
    [PT_UOMFk] int  NOT NULL,
    [PT_StdCost] bit  NOT NULL,
    [PT_Hazardous] bit  NOT NULL,
    [PT_StdCostValue] decimal(19,4)  NOT NULL
);
GO

-- Creating table 'TLADM_RejectReasons'
CREATE TABLE [dbo].[TLADM_RejectReasons] (
    [RJR_Pk] int IDENTITY(1,1) NOT NULL,
    [RJR_Description] varchar(50)  NOT NULL
);
GO

-- Creating table 'TLADM_Ribbing'
CREATE TABLE [dbo].[TLADM_Ribbing] (
    [RI_Id] int IDENTITY(1,1) NOT NULL,
    [RI_Description] varchar(50)  NOT NULL,
    [RI_PowerN] int  NOT NULL,
    [RI_Discontinued] bit  NULL,
    [RI_Discontinued_Date] datetime  NULL
);
GO

-- Creating table 'TLADM_StockTakeFreq'
CREATE TABLE [dbo].[TLADM_StockTakeFreq] (
    [STF_Pk] int IDENTITY(1,1) NOT NULL,
    [STF_ShortCode] varchar(5)  NOT NULL,
    [STF_Description] varchar(50)  NOT NULL,
    [STF_Period_Weeks] int  NOT NULL
);
GO

-- Creating table 'TLADM_StockTypes'
CREATE TABLE [dbo].[TLADM_StockTypes] (
    [ST_Id] int IDENTITY(1,1) NOT NULL,
    [ST_ShortCode] varchar(5)  NOT NULL,
    [ST_Description] varchar(50)  NOT NULL
);
GO

-- Creating table 'TLADM_StoreTypes'
CREATE TABLE [dbo].[TLADM_StoreTypes] (
    [StoreT_Id] int IDENTITY(1,1) NOT NULL,
    [StoreT_ShortCode] varchar(5)  NOT NULL,
    [StoreT_Description] varchar(50)  NOT NULL,
    [StoreT_UOMFK] int  NOT NULL,
    [StoreT_ProdTypeFK] int  NOT NULL,
    [StoreT_Hazardous] bit  NOT NULL
);
GO

-- Creating table 'TLADM_Suppliers'
CREATE TABLE [dbo].[TLADM_Suppliers] (
    [Sup_Pk] int IDENTITY(1,1) NOT NULL,
    [Sup_Code] varchar(10)  NOT NULL,
    [Sup_Description] varchar(50)  NOT NULL,
    [Sup_StdPayMentTerms] varchar(50)  NOT NULL,
    [Sup_DiscountStructure] varchar(5)  NOT NULL,
    [Sup_PostalAddress] varchar(max)  NOT NULL,
    [Sup_Telephone] varchar(50)  NOT NULL,
    [Sup_ContactPerson] varchar(50)  NOT NULL,
    [Suip_ShippingAddress1] varchar(max)  NOT NULL,
    [Sup_ShippingAddress2] varchar(max)  NULL,
    [Sup_ShippingAddress3] varchar(max)  NULL,
    [Sup_ShippingAddress4] varchar(max)  NULL,
    [Sup_Blocked] bit  NOT NULL,
    [Sup_ProductTypes_FK] int  NOT NULL,
    [Sup_AllowsConsignment] bit  NOT NULL,
    [Sup_VatReference] varchar(50)  NOT NULL,
    [Sup_EMailContact] varchar(50)  NOT NULL,
    [Sup_AllowsEMail] bit  NULL,
    [Sup_Notes] varchar(max)  NULL,
    [Sup_Fax] varchar(20)  NULL,
    [Sup_ProductGroups_FK] int  NOT NULL
);
GO

-- Creating table 'TLADM_UOM'
CREATE TABLE [dbo].[TLADM_UOM] (
    [UOM_Pk] int IDENTITY(1,1) NOT NULL,
    [UOM_ShortCode] varchar(5)  NOT NULL,
    [UOM_Description] varchar(50)  NOT NULL,
    [UOM_Discontinued] bit  NOT NULL,
    [UOM_DiscontinuedDate] datetime  NULL
);
GO

-- Creating table 'TLFIN_Calendar'
CREATE TABLE [dbo].[TLFIN_Calendar] (
    [Cal_Id] int IDENTITY(1,1) NOT NULL,
    [Cal_DateFrom] datetime  NOT NULL,
    [Cal_DateTo] datetime  NOT NULL,
    [Cal_FinPeriod] int  NOT NULL,
    [Cal_FinPeriod_Description] varchar(50)  NOT NULL,
    [Cal_Qtr] int  NOT NULL,
    [Cal_Financial_Year_FK] int  NOT NULL
);
GO

-- Creating table 'TLFIN_FinancialYear'
CREATE TABLE [dbo].[TLFIN_FinancialYear] (
    [Finy_Id] int IDENTITY(1,1) NOT NULL,
    [Fin_Year_Description] varchar(50)  NOT NULL,
    [Fin_Year] int  NOT NULL
);
GO

-- Creating table 'TLADM_FabricProduct'
CREATE TABLE [dbo].[TLADM_FabricProduct] (
    [FP_Id] int IDENTITY(1,1) NOT NULL,
    [FP_Description] varchar(50)  NOT NULL,
    [FP_PowerN] int  NOT NULL,
    [FP_Discontinued] bit  NULL,
    [FP_Discontinued_Date] datetime  NULL
);
GO

-- Creating table 'TLADM_AdditionalAddress'
CREATE TABLE [dbo].[TLADM_AdditionalAddress] (
    [Addit_Pk] int IDENTITY(1,1) NOT NULL,
    [Addit_Description] varchar(50)  NOT NULL,
    [Addit_Address] varchar(max)  NOT NULL,
    [Addit_Notes] varchar(max)  NULL,
    [Addit_IsCustomer] bit  NOT NULL,
    [Addit_FK] int  NOT NULL
);
GO

-- Creating table 'TLADM_StylesAdditional'
CREATE TABLE [dbo].[TLADM_StylesAdditional] (
    [AddSty_Pk] int IDENTITY(1,1) NOT NULL,
    [AddSty_Style_FK] int  NOT NULL,
    [AddSty_Size_FK] int  NOT NULL,
    [AddSty_Colour_FK] int  NOT NULL,
    [AddSty_StandardCost] decimal(19,4)  NOT NULL,
    [AddSty_RefundCost] decimal(19,4)  NOT NULL
);
GO

-- Creating table 'TLADM_CompanyDetails'
CREATE TABLE [dbo].[TLADM_CompanyDetails] (
    [Comp_Pk] int IDENTITY(1,1) NOT NULL,
    [Comp_Name] varchar(50)  NOT NULL,
    [Comp_Address] varchar(max)  NULL,
    [Comp_TelephoneNo] varchar(50)  NULL,
    [Comp_FaxNo] varchar(50)  NULL,
    [Comp_ContactPerson] varchar(50)  NULL,
    [Comp_ContactPersonEmail] varchar(50)  NULL,
    [Comp_TotalPowerN] int  NOT NULL
);
GO

-- Creating table 'TLADM_CottonAgent'
CREATE TABLE [dbo].[TLADM_CottonAgent] (
    [CottonAgent_Pk] int IDENTITY(1,1) NOT NULL,
    [CottonAgent_Description] varchar(50)  NOT NULL,
    [CottonAgent_Address] varchar(max)  NOT NULL,
    [CottonAgent_Contact] varchar(50)  NULL,
    [CottonAgent_Email] varchar(50)  NULL,
    [CottonAgent_PhoneNo] int  NULL
);
GO

-- Creating table 'TLADM_CottonHauliers'
CREATE TABLE [dbo].[TLADM_CottonHauliers] (
    [Haul_Pk] int IDENTITY(1,1) NOT NULL,
    [Haul_No] varchar(10)  NOT NULL,
    [Haul_Description] varchar(50)  NOT NULL,
    [Haul_Address] varchar(max)  NULL,
    [Haul_ContactPerson] varchar(50)  NULL,
    [Haul_TelephoneNo] int  NULL,
    [Haul_EMailAddress] varchar(50)  NULL
);
GO

-- Creating table 'TLADM_CottonHauliersVehicles'
CREATE TABLE [dbo].[TLADM_CottonHauliersVehicles] (
    [HaulVeh_Pk] int IDENTITY(1,1) NOT NULL,
    [HaulVeh_Haulier_FK] int  NOT NULL,
    [HaulVeh_RegNo] varchar(20)  NOT NULL,
    [HaulVeh_Description] varchar(50)  NULL
);
GO

-- Creating table 'TLADM_TranactionType'
CREATE TABLE [dbo].[TLADM_TranactionType] (
    [TrxT_Pk] int IDENTITY(1,1) NOT NULL,
    [TrxT_Department_FK] int  NOT NULL,
    [TrxT_Number] int  NOT NULL,
    [TrxT_Description] varchar(50)  NOT NULL,
    [TrxT_FromWhse_FK] int  NULL,
    [TrxT_ToWhse_FK] int  NULL,
    [TrxT_FinishedGoods_FK] int  NOT NULL,
    [TrxT_Discontinued] bit  NOT NULL,
    [TrxT_DiscontinuedDate] datetime  NULL
);
GO

-- Creating table 'TLADM_StoreBal'
CREATE TABLE [dbo].[TLADM_StoreBal] (
    [StoreBal_Pk] int IDENTITY(1,1) NOT NULL,
    [StoreBal_StoreId_FK] int  NOT NULL,
    [StoreBal_Month] int  NOT NULL,
    [StoreBal_Year] int  NOT NULL,
    [StoreBal_FinishedGoods_FK] int  NOT NULL,
    [StoreBal_NumberOf] int  NOT NULL,
    [StoreBal_Weight] decimal(18,4)  NULL
);
GO

-- Creating table 'TLSPN_CottonReceived'
CREATE TABLE [dbo].[TLSPN_CottonReceived] (
    [CotRec_Pk] int IDENTITY(1,1) NOT NULL,
    [CotRec_Supplier_FK] int  NOT NULL,
    [CotRec_Contract_FK] int  NOT NULL,
    [CotRec_LotNo] int  NOT NULL,
    [CotRec_VehReg_FK] int  NOT NULL,
    [CotRec_DateReceived] datetime  NOT NULL,
    [CotRec_NoOfBales] int  NOT NULL,
    [CotRec_Supplier_GrossWeight] decimal(18,4)  NOT NULL,
    [CotRec_Supplier_NetWeight] decimal(18,4)  NOT NULL,
    [CotRec_WeighBridge_Gross] decimal(18,4)  NOT NULL,
    [CotRec_WeighBridge_Nett] decimal(18,4)  NOT NULL,
    [CotReC_TranType_FK] int  NOT NULL,
    [CotRec_NettCottonWeight] decimal(18,4)  NOT NULL,
    [CotRec_GrossAvgBaleWeight] decimal(18,4)  NOT NULL,
    [CotRec_NettAvgBaleWeight] decimal(18,4)  NOT NULL,
    [CoRec_GRNNumber] int  NOT NULL
);
GO

-- Creating table 'TLSPN_CottonReceivedBales'
CREATE TABLE [dbo].[TLSPN_CottonReceivedBales] (
    [CotBales_Pk] int IDENTITY(1,1) NOT NULL,
    [CotBales_CotReceived_FK] int  NOT NULL,
    [CotBales_LotNo] int  NOT NULL,
    [CotBales_BaleNo] int  NOT NULL,
    [CotBales_Mic] decimal(18,4)  NOT NULL,
    [CotBales_Weight_Nett] decimal(18,4)  NOT NULL,
    [CotBales_Weight_Gross] decimal(18,4)  NOT NULL,
    [CotBales_Staple] decimal(18,4)  NOT NULL,
    [CoBales_IssuedToProd] bit  NOT NULL,
    [CoBales_CottonReturned] bit  NOT NULL,
    [CoBales_CottonSold] bit  NOT NULL,
    [CoBales_CottonSequence] int  NOT NULL,
    [CoBales_BlowRoomPosition] int  NOT NULL,
    [CotBales_ConfirmedByQA] bit  NOT NULL
);
GO

-- Creating table 'TLSPN_OpenBalance'
CREATE TABLE [dbo].[TLSPN_OpenBalance] (
    [OpenBal_Pk] int IDENTITY(1,1) NOT NULL,
    [OpenBal_Store_FK] int  NOT NULL,
    [OpenBal_NoOfBales] int  NOT NULL,
    [OpenBal_GrossBaleWeight] decimal(18,4)  NOT NULL,
    [OpenBal_NettBaleWeight] decimal(18,4)  NOT NULL
);
GO

-- Creating table 'TLSPN_QAMeasurements'
CREATE TABLE [dbo].[TLSPN_QAMeasurements] (
    [YarnQA_Pk] int IDENTITY(1,1) NOT NULL,
    [YarnQA_YarnOrder_FK] int  NULL,
    [YarnQA_Date] datetime  NOT NULL,
    [YarnQA_MeasureNo] int  NOT NULL,
    [YarnQA_MachineNo_FK] int  NOT NULL,
    [YarnQA_TestNo] int  NOT NULL,
    [YarnQA_08H00] decimal(18,4)  NOT NULL,
    [YarnQA_10H00] decimal(18,4)  NOT NULL,
    [YarnQA_12H00] decimal(18,4)  NOT NULL,
    [YarnQA_14H00] int  NOT NULL,
    [YarnQA_16H00] int  NOT NULL,
    [YarnQA_18H00] int  NOT NULL
);
GO

-- Creating table 'TLKNI_YarnTransaction'
CREATE TABLE [dbo].[TLKNI_YarnTransaction] (
    [KnitY_Pk] int IDENTITY(1,1) NOT NULL,
    [KnitY_Customer_FK] int  NOT NULL,
    [KnitY_TransactionDate] datetime  NOT NULL,
    [KnitY_TransactionDoc] varchar(50)  NOT NULL,
    [KnitY_GRNNumber] int  NOT NULL,
    [KnitY_Notes] varchar(max)  NULL,
    [KnitY_ThirdParty] bit  NOT NULL,
    [KnitY_OrigInvNo] int  NULL,
    [KnitY_ApprovedBy] varchar(50)  NULL,
    [KnitY_RTS] bit  NOT NULL,
    [KnitY_Cones] int  NOT NULL,
    [KnitY_NettWeight] decimal(18,4)  NULL,
    [KnitY_TranType_FK] int  NULL
);
GO

-- Creating table 'TLKNI_GreigeTransactions'
CREATE TABLE [dbo].[TLKNI_GreigeTransactions] (
    [GreigeT_Pk] int IDENTITY(1,1) NOT NULL,
    [GreigeT_Piece_FK] int  NOT NULL,
    [GreigeT_KOrder_FK] int  NOT NULL,
    [GreigeT_TransactionDate] datetime  NOT NULL,
    [GreigeT_Grade] varchar(10)  NOT NULL,
    [GreigeT_TransactionType_FK] int  NOT NULL,
    [GreigeT_AdjustedWeight] decimal(18,4)  NOT NULL,
    [GreigeT_TransactionNumber] int  NOT NULL,
    [GreigeT_ApprovedBy] varchar(50)  NULL
);
GO

-- Creating table 'TLADM_Labels'
CREATE TABLE [dbo].[TLADM_Labels] (
    [Lbl_Id] int IDENTITY(1,1) NOT NULL,
    [Lbl_Description] varchar(50)  NOT NULL,
    [Lbl_Discontinued] bit  NULL,
    [Lbl_Discontinued_Date] datetime  NULL,
    [Lbl_PowerN] int  NOT NULL,
    [Lbl_Customer_FK] int  NOT NULL
);
GO

-- Creating table 'TLADM_Yarn'
CREATE TABLE [dbo].[TLADM_Yarn] (
    [YA_Id] int IDENTITY(1,1) NOT NULL,
    [YA_Description] varchar(50)  NOT NULL,
    [YA_PowerN] int  NOT NULL,
    [YA_Discontinued] bit  NULL,
    [YA_Discontnued_Date] datetime  NULL,
    [YA_YarnType] varchar(10)  NOT NULL,
    [YA_Blocked] bit  NOT NULL,
    [YA_CottonOrigin_FK] int  NOT NULL,
    [YA_ProductGroup_FK] int  NOT NULL,
    [YA_ProductType_FK] int  NOT NULL,
    [YA_Qty_Show] bit  NOT NULL,
    [YA_ROL] int  NOT NULL,
    [YA_ROQ] int  NOT NULL,
    [YA_StdCost_Actual] decimal(18,2)  NOT NULL,
    [YA_StdCost_Show] bit  NOT NULL,
    [YA_Supplier_FK] int  NOT NULL,
    [YA_TexCount] decimal(18,2)  NOT NULL,
    [YA_Twist] decimal(18,2)  NOT NULL,
    [YA_UOM_Fk] int  NOT NULL,
    [YA_ConeColour] varchar(50)  NULL
);
GO

-- Creating table 'TLADM_ProductionLoss'
CREATE TABLE [dbo].[TLADM_ProductionLoss] (
    [TLProdLoss_Pk] int IDENTITY(1,1) NOT NULL,
    [TLProdLoss_Dept_Fk] int  NOT NULL,
    [TLProdLoss_Percent] decimal(18,4)  NOT NULL,
    [TLProdLoss_Kg] int  NOT NULL
);
GO

-- Creating table 'TLADM_FabricAttributes'
CREATE TABLE [dbo].[TLADM_FabricAttributes] (
    [FbAtrib_Pk] int IDENTITY(1,1) NOT NULL,
    [FbAtrib_Description] varchar(50)  NOT NULL,
    [FbAtrib_Brand_FK] int  NOT NULL,
    [FbAtrib_Greige_FK] int  NOT NULL,
    [FbAtrib_Colour_FK] int  NOT NULL,
    [FbAtrib_FabProduct_FK] int  NOT NULL,
    [FbAtrib_FabProductTypes_FK] int  NOT NULL,
    [FbAtrib_Blocked] bit  NOT NULL,
    [FbAtrib_UOM_Fk] int  NOT NULL,
    [FbAtrib_PreferedSupplier_FK] int  NOT NULL,
    [FbAtrib_BarCode] bit  NOT NULL,
    [FbAtrib_ShowQty] bit  NOT NULL,
    [FbAtrib_Discontinued] bit  NULL,
    [FbAtrib_Discontinued_Date] datetime  NULL,
    [FbAtrib_PowerN] int  NOT NULL
);
GO

-- Creating table 'TLADM_MachineOperators'
CREATE TABLE [dbo].[TLADM_MachineOperators] (
    [MachOp_Pk] int IDENTITY(1,1) NOT NULL,
    [MachOp_Code] varchar(5)  NOT NULL,
    [MachOp_Description] varchar(50)  NOT NULL,
    [MachOp_Department_FK] int  NOT NULL,
    [MachOp_Payroll_Code] varchar(15)  NOT NULL,
    [MachOp_Inspector] bit  NOT NULL,
    [MachOp_Discontinued] bit  NOT NULL,
    [MachOp_Discontinued_Date] datetime  NULL
);
GO

-- Creating table 'TLADM_ProductRating_Detail'
CREATE TABLE [dbo].[TLADM_ProductRating_Detail] (
    [prd_Id] int IDENTITY(1,1) NOT NULL,
    [prd_Parent_FK] int  NOT NULL,
    [Prd_SizePN] int  NOT NULL,
    [Prd_MarkerRatio] decimal(18,4)  NOT NULL
);
GO

-- Creating table 'TLDYE_DefinitionDetails'
CREATE TABLE [dbo].[TLDYE_DefinitionDetails] (
    [TLDYED_PK] int IDENTITY(1,1) NOT NULL,
    [TLDYED_Receipe_FK] int  NOT NULL,
    [TLDYED_Cosumables_FK] int  NOT NULL,
    [TLDYED_MELFC] decimal(18,4)  NOT NULL,
    [TLDYED_LiqRatio] int  NOT NULL,
    [TLDYED_LiqCalc] bit  NOT NULL
);
GO

-- Creating table 'TLSPN_CottonTransactions'
CREATE TABLE [dbo].[TLSPN_CottonTransactions] (
    [cotrx_pk] int IDENTITY(1,1) NOT NULL,
    [cotrx_TransDate] datetime  NOT NULL,
    [cotrx_VehReg] varchar(20)  NULL,
    [cotrx_Supplier_FK] int  NOT NULL,
    [cotrx_Customer_FK] int  NULL,
    [cotrx_Return_No] int  NOT NULL,
    [cotrx_Haulier_FK] int  NULL,
    [cotrx_ContractNo_Fk] int  NOT NULL,
    [cotrx_LotNo] int  NOT NULL,
    [cotrx_NoBales] int  NULL,
    [cotrx_GrossWeight] decimal(18,4)  NOT NULL,
    [cotrx_NetWeight] decimal(18,4)  NOT NULL,
    [cotrx_WeighBridgeFull] decimal(18,4)  NOT NULL,
    [cotrx_WeighBridgeEmpty] decimal(18,4)  NOT NULL,
    [cotrx_NettPerWB] decimal(18,4)  NOT NULL,
    [cotrx_GrossAveBaleWeight] decimal(18,4)  NOT NULL,
    [cottrx_NettAveBaleWeight] decimal(18,4)  NOT NULL,
    [cotrx_TranType] int  NOT NULL,
    [cotrx_WriteOff] bit  NOT NULL,
    [cotrx_Notes] varchar(max)  NULL,
    [cotrx_Selected4Yarn] bit  NOT NULL,
    [cotrx_YarnOrder_FK] int  NULL
);
GO

-- Creating table 'TLSPN_CottonMerge'
CREATE TABLE [dbo].[TLSPN_CottonMerge] (
    [TLCTM_Pk] int IDENTITY(1,1) NOT NULL,
    [TLCTM_Description] varchar(50)  NOT NULL,
    [TLCTM_Date] datetime  NOT NULL
);
GO

-- Creating table 'TLSPN_CottonMergeDetails'
CREATE TABLE [dbo].[TLSPN_CottonMergeDetails] (
    [TLCTMD_Pk] int IDENTITY(1,1) NOT NULL,
    [TLCTMD_CTM_FK] int  NOT NULL,
    [TLCTMD_Supplier_FK] int  NOT NULL,
    [TLCTMD_Contract_FK] int  NOT NULL,
    [TLCTMD_Split] decimal(18,4)  NOT NULL
);
GO

-- Creating table 'TLSPN_YarnOrder'
CREATE TABLE [dbo].[TLSPN_YarnOrder] (
    [YarnO_Pk] int IDENTITY(1,1) NOT NULL,
    [YarnO_Date] datetime  NOT NULL,
    [YarnO_DelDate] datetime  NOT NULL,
    [YarnO_OrderNumber] int  NOT NULL,
    [Yarno_LayDown_FK] int  NOT NULL,
    [Yarno_CottonOrigin_FK] int  NOT NULL,
    [YarnO_Supplier_FK] int  NOT NULL,
    [Yarno_YarnType_FK] int  NOT NULL,
    [Yarno_MachNo_FK] int  NOT NULL,
    [Yarno_OrderWeight] int  NOT NULL,
    [Yarno_Packing] varchar(max)  NULL,
    [Yarno_Closed] bit  NOT NULL,
    [Yarno_ClosedDate] datetime  NULL,
    [YarnO_TransactionType_FK] int  NOT NULL,
    [YarnO_Reinstate] bit  NOT NULL,
    [YarnO_MergeContract_FK] int  NULL
);
GO

-- Creating table 'TLSPN_YarnWaste'
CREATE TABLE [dbo].[TLSPN_YarnWaste] (
    [TLYW_Pk] int IDENTITY(1,1) NOT NULL,
    [TLYW_Date] datetime  NOT NULL,
    [TLYW_BaleNo] varchar(50)  NOT NULL,
    [TLYW_BaleGrossWeight] decimal(18,4)  NOT NULL,
    [TLYW_BaleNettWeight] decimal(18,4)  NOT NULL,
    [TLYW_Disposed] bit  NOT NULL,
    [TLYW_DateDisposed] datetime  NULL,
    [TLYW_Customer_FK] int  NULL,
    [TLYW_SalesTransactionNO] int  NOT NULL,
    [TLYW_TransactionType_In] int  NOT NULL,
    [TLYW_TransactionType_Out] int  NOT NULL
);
GO

-- Creating table 'TLDYE_DyeBatchAllocated'
CREATE TABLE [dbo].[TLDYE_DyeBatchAllocated] (
    [TLDYEA_Pk] int IDENTITY(1,1) NOT NULL,
    [TLDYEA_AllocateDate] datetime  NOT NULL,
    [TLDYEA_MachCode_FK] int  NOT NULL,
    [TLDYEA_DyeBatch_FK] int  NOT NULL,
    [TLDYEA_FabricType_FK] int  NOT NULL
);
GO

-- Creating table 'TLADM_DyeQDCodes'
CREATE TABLE [dbo].[TLADM_DyeQDCodes] (
    [QDF_Pk] int IDENTITY(1,1) NOT NULL,
    [QDF_Code] varchar(20)  NOT NULL,
    [QDF_Description] varchar(50)  NOT NULL,
    [QDF_NCRRequired] bit  NOT NULL
);
GO

-- Creating table 'TLADM_DyeRemendyCodes'
CREATE TABLE [dbo].[TLADM_DyeRemendyCodes] (
    [QRC_Pk] int IDENTITY(1,1) NOT NULL,
    [QRC_Code] varchar(20)  NOT NULL,
    [QRC_Description] varchar(50)  NOT NULL,
    [QRC_AdditionalResources] bit  NOT NULL
);
GO

-- Creating table 'TLADM_QADyeProcess'
CREATE TABLE [dbo].[TLADM_QADyeProcess] (
    [QADYEP_Pk] int IDENTITY(1,1) NOT NULL,
    [QADYEP_Description] varchar(50)  NOT NULL
);
GO

-- Creating table 'TLDYE_NonComplianceDetail'
CREATE TABLE [dbo].[TLDYE_NonComplianceDetail] (
    [DYENCRD_PK] int IDENTITY(1,1) NOT NULL,
    [DYENCRD_ComNumber] int  NOT NULL,
    [DYENCRD_BatchNo_Fk] int  NOT NULL,
    [DYENCRD_Code_FK] int  NOT NULL,
    [DYENCRD_FR] bit  NOT NULL
);
GO

-- Creating table 'TLSPN_YarnOrderLayDown'
CREATE TABLE [dbo].[TLSPN_YarnOrderLayDown] (
    [YarnLD_Pk] int IDENTITY(1,1) NOT NULL,
    [YarnLD_Date] datetime  NOT NULL,
    [YarnLD_LayDownNo] int  NOT NULL,
    [YarnLD_LotNo] int  NOT NULL,
    [YarnLD_NoOfBales] int  NOT NULL,
    [YarnLD_WeightKg] decimal(18,4)  NOT NULL,
    [YarnLD_BaleAvgWeight] decimal(18,4)  NOT NULL
);
GO

-- Creating table 'TLDYE_ComDyeBatch'
CREATE TABLE [dbo].[TLDYE_ComDyeBatch] (
    [DYEBC_Pk] int IDENTITY(1,1) NOT NULL,
    [DYEBC_BatchNo] varchar(50)  NOT NULL,
    [DYEBC_Customer_FK] int  NOT NULL,
    [DYEBC_Colour_FK] int  NOT NULL,
    [DYEBC_BatchDate] datetime  NOT NULL,
    [DYEBC_DateOrder] datetime  NOT NULL,
    [DYEBC_DateRequired] datetime  NOT NULL,
    [DYEBC_Greige_FK] int  NOT NULL,
    [DYEBC_Trim_Fk] int  NOT NULL,
    [DYEBC_TransactionType] int  NOT NULL
);
GO

-- Creating table 'TLDYE_ComDyeBatchDetails'
CREATE TABLE [dbo].[TLDYE_ComDyeBatchDetails] (
    [TLCDD_Pk] int IDENTITY(1,1) NOT NULL,
    [TLCDD_PieceNo_FK] int  NOT NULL,
    [TLCDD_ComDyeBatch_FK] int  NOT NULL
);
GO

-- Creating table 'TLDYE_AllocatedOperator'
CREATE TABLE [dbo].[TLDYE_AllocatedOperator] (
    [DYEOP_Pk] int IDENTITY(1,1) NOT NULL,
    [DYEOP_BatchNo_FK] int  NOT NULL,
    [DYEOP_BatchDate] datetime  NOT NULL,
    [DYEOP_Operator_FK] int  NOT NULL
);
GO

-- Creating table 'TLSPN_YarnTransactions'
CREATE TABLE [dbo].[TLSPN_YarnTransactions] (
    [YarnTrx_Pk] int IDENTITY(1,1) NOT NULL,
    [YarnTrx_YarnOrder_FK] int  NOT NULL,
    [YarnTrx_PalletNo_Fk] int  NOT NULL,
    [YarnTrx_TranType_FK] int  NOT NULL,
    [YarnTrx_Date] datetime  NOT NULL,
    [YarnTrx_SequenceNo] int  NOT NULL,
    [YarnTrx_Customer_FK] int  NULL,
    [YarnTrx_OrderNo] varchar(50)  NULL,
    [YarnTrx_Cones] int  NOT NULL,
    [YarnTrx_ApprovedBy] varchar(50)  NULL,
    [YarnTrx_Reasons] varchar(max)  NULL,
    [YarnTrx_NettWeight] decimal(18,4)  NULL,
    [YarnTrx_WriteOff] bit  NOT NULL,
    [YarnTrx_FromDep_FK] int  NULL,
    [YarnTrx_ToDep_FK] int  NULL
);
GO

-- Creating table 'TLADM_DepartmentsAreaTransaction'
CREATE TABLE [dbo].[TLADM_DepartmentsAreaTransaction] (
    [TLDEPA_Pk] int IDENTITY(1,1) NOT NULL,
    [TLDEPA_Date] datetime  NOT NULL,
    [TRDEPA_Department_FK] int  NOT NULL,
    [TRDEPA_Value] decimal(18,4)  NOT NULL,
    [TRDEPA_DeptA_FK] int  NOT NULL
);
GO

-- Creating table 'TLKNI_GreigeCommisionAdjustment'
CREATE TABLE [dbo].[TLKNI_GreigeCommisionAdjustment] (
    [GreigeComAJ_Pk] int IDENTITY(1,1) NOT NULL,
    [GreigeComAJ_PieceNo_FK] int  NOT NULL,
    [GreigeComAJ_TransDate] datetime  NOT NULL,
    [GreigeComAJ_AjustmentNo] int  NOT NULL,
    [GreigeComAJ_AprovedBy] varchar(50)  NOT NULL,
    [GreigeComAJ_Reasons] varchar(50)  NOT NULL,
    [GreigeComAJ_AmtAdjusted] decimal(18,4)  NOT NULL,
    [GreigeComAJ_Strore_FK] int  NOT NULL,
    [GreigeComAJ_GrnNumber] int  NOT NULL,
    [GreigeComAJ_GreigeProduction_FK] int  NOT NULL
);
GO

-- Creating table 'TLCUT_CutSheetReceiptBoxes'
CREATE TABLE [dbo].[TLCUT_CutSheetReceiptBoxes] (
    [TLCUTSHB_Pk] int IDENTITY(1,1) NOT NULL,
    [TLCUTSHB_CutSheet_FK] int  NOT NULL,
    [TLCUTSHB_AdultBoxes] int  NOT NULL,
    [TLCUTSHB_KidBoxes] int  NOT NULL,
    [TLCUTSHB_Binding] int  NOT NULL,
    [TLCUTSHB_Ribbing] int  NOT NULL
);
GO

-- Creating table 'TLCUT_QCBerrie'
CREATE TABLE [dbo].[TLCUT_QCBerrie] (
    [TLQCFB_Pk] int IDENTITY(1,1) NOT NULL,
    [TLQCFB_Operator_FK] int  NOT NULL,
    [TLQCFB_CutSheetReceipt_FK] int  NOT NULL,
    [TLQCFB_Measure1] int  NOT NULL,
    [TLQCFB_Measure2] int  NOT NULL,
    [TLQCFB_Measure3] int  NOT NULL,
    [TLQCFB_Measure4] int  NOT NULL,
    [TLQCFB_Measure5] int  NOT NULL,
    [TLQCFB_Measure6] int  NOT NULL,
    [TLQCFB_Measure7] int  NOT NULL,
    [TLQCFB_Measure8] int  NOT NULL,
    [TLQCFB_Measure9] int  NOT NULL,
    [TLQCFB_Measure10] int  NOT NULL,
    [TLQCFB_Measure11] int  NOT NULL
);
GO

-- Creating table 'TLADM_CutMeasureArea'
CREATE TABLE [dbo].[TLADM_CutMeasureArea] (
    [TLCUTA_Pk] int IDENTITY(1,1) NOT NULL,
    [TLCUTA_ShortCode] varchar(10)  NOT NULL,
    [TLCUTA_Description] varchar(50)  NOT NULL,
    [TLCUTA_Style_FK] int  NULL,
    [TLCUTA_Size_FK] int  NULL
);
GO

-- Creating table 'TLCUT_FabricReturns'
CREATE TABLE [dbo].[TLCUT_FabricReturns] (
    [TLCUTFR_Pk] int IDENTITY(1,1) NOT NULL,
    [TLCUTFR_Number] varchar(20)  NOT NULL,
    [TLCUTFR_Date] datetime  NOT NULL,
    [TLCUTFR_KgReturned] decimal(18,4)  NOT NULL,
    [TLCUTFR_TransacftionType] int  NOT NULL,
    [TLCUTFR_ReasonCode] varchar(10)  NULL,
    [TLCUTFR_CutSheetD_FK] int  NOT NULL
);
GO

-- Creating table 'TLADM_CutTrims'
CREATE TABLE [dbo].[TLADM_CutTrims] (
    [TLCUTTOC_Pk] int IDENTITY(1,1) NOT NULL,
    [TLCUTTOC_ShortCode] varchar(10)  NOT NULL,
    [TLCUTTOC_Description] varchar(50)  NOT NULL
);
GO

-- Creating table 'TLADM_CutFleeceCuffs'
CREATE TABLE [dbo].[TLADM_CutFleeceCuffs] (
    [TLADMFC_Pk] int IDENTITY(1,1) NOT NULL,
    [TLADMFC_shortCode] varchar(10)  NULL,
    [TLADMFC_Size_FK] int  NOT NULL
);
GO

-- Creating table 'TLADM_CutFleeceWaist'
CREATE TABLE [dbo].[TLADM_CutFleeceWaist] (
    [TLADMFW_Pk] int IDENTITY(1,1) NOT NULL,
    [TLADMFW_ShortCode] varchar(10)  NULL,
    [TLADMFW_Size_FK] int  NOT NULL
);
GO

-- Creating table 'TLCUT_TrimsOnCut'
CREATE TABLE [dbo].[TLCUT_TrimsOnCut] (
    [TLCUTTOC_Pk] int IDENTITY(1,1) NOT NULL,
    [TLCUTTOC_Date] datetime  NOT NULL,
    [TLCUTTOC_CutSheet_FK] int  NOT NULL,
    [TLCUTTOC_Description_FK] int  NOT NULL,
    [TLCUTTOC_Size_FK] int  NOT NULL,
    [TLCUTTOC_Qty] int  NOT NULL,
    [TLCUTTOC_Kgs] decimal(18,4)  NOT NULL
);
GO

-- Creating table 'TLCUT_CutFleeceStats'
CREATE TABLE [dbo].[TLCUT_CutFleeceStats] (
    [TLFCFW_Pk] int IDENTITY(1,1) NOT NULL,
    [TLFCFW_FleeceType] int  NOT NULL,
    [TLFCFW_CutSheet_FK] int  NOT NULL,
    [TLFCFW_Size_FK] int  NOT NULL,
    [TLFCFW_Kgs] decimal(18,4)  NOT NULL
);
GO

-- Creating table 'TLCMT_ProductDefects'
CREATE TABLE [dbo].[TLCMT_ProductDefects] (
    [PD_Id] int IDENTITY(1,1) NOT NULL,
    [PD_Description] varchar(50)  NOT NULL
);
GO

-- Creating table 'TLCUT_RejectReasons'
CREATE TABLE [dbo].[TLCUT_RejectReasons] (
    [TLCUTRJR_Pk] int IDENTITY(1,1) NOT NULL,
    [TLCUTRJR_ShortCode] varchar(10)  NOT NULL,
    [TLCUTRJR_Description] varchar(50)  NOT NULL
);
GO

-- Creating table 'TLCUT_CutSheetDetail'
CREATE TABLE [dbo].[TLCUT_CutSheetDetail] (
    [TLCutSHD_Pk] int IDENTITY(1,1) NOT NULL,
    [TLCutSHD_CutSheet_FK] int  NOT NULL,
    [TLCutSHD_DyeBatchDet_FK] int  NOT NULL,
    [TLCutSHD_CurrentStore_FK] int  NOT NULL,
    [TLCutSHD_Transaction_Type] int  NULL,
    [TLCUTSHD_NettWeight] decimal(18,4)  NOT NULL,
    [TLCUTSHD_Body] bit  NOT NULL
);
GO

-- Creating table 'TLADM_StandardProduct'
CREATE TABLE [dbo].[TLADM_StandardProduct] (
    [TLADMSP_Pk] int IDENTITY(1,1) NOT NULL,
    [TLADMSP_ShortCode] varchar(10)  NOT NULL,
    [TLADMSP_Description] varchar(50)  NOT NULL
);
GO

-- Creating table 'TLCMT_ProductionFaults'
CREATE TABLE [dbo].[TLCMT_ProductionFaults] (
    [TLCMTPF_Pk] int IDENTITY(1,1) NOT NULL,
    [TLCMTPF_PanelIssue_FK] int  NOT NULL,
    [TLCMTPF_LineIssue_FK] int  NOT NULL,
    [TLCMTPF_Fault_FK] int  NOT NULL,
    [TLCMTPF_Qty] int  NOT NULL
);
GO

-- Creating table 'TLKNI_ProductionSplit'
CREATE TABLE [dbo].[TLKNI_ProductionSplit] (
    [TLKNISP_Pk] int IDENTITY(1,1) NOT NULL,
    [TLKNISP_FromStore_FK] int  NOT NULL,
    [TLKNISP_ToStore_FK] int  NOT NULL,
    [TLKNISP_OrigPieceNo] varchar(10)  NOT NULL,
    [TLKNISP_KnitOrder_FK] int  NOT NULL,
    [TLKNISP_NewPieceNo] varchar(20)  NOT NULL,
    [TLKNISP_Mass] decimal(18,4)  NOT NULL,
    [TLKNISP_Date] datetime  NOT NULL,
    [TLKNISP_Notes] varchar(50)  NULL
);
GO

-- Creating table 'TLCMT_BodyMeasureRP'
CREATE TABLE [dbo].[TLCMT_BodyMeasureRP] (
    [TLBTMRP_Pk] int IDENTITY(1,1) NOT NULL,
    [TLBTMRP_CutSheet_PK] int  NOT NULL,
    [TLBMPRP_BundleNo] varchar(10)  NOT NULL,
    [TLBMPRP_Measurement_FK] int  NOT NULL,
    [TLBMPRP_RequiredMeasure] decimal(18,4)  NOT NULL,
    [TLBMPRP_Top] decimal(18,4)  NOT NULL,
    [TLBMPRP_Middle] decimal(18,4)  NOT NULL,
    [TLBMPRP_Bottom] decimal(18,4)  NOT NULL,
    [TLBMPRP_TransDate] datetime  NOT NULL,
    [TLBMPRP_Comments] varchar(50)  NULL
);
GO

-- Creating table 'TLCMT_LineFeederBundleCheck'
CREATE TABLE [dbo].[TLCMT_LineFeederBundleCheck] (
    [TLCMTLF_Pk] int IDENTITY(1,1) NOT NULL,
    [TLCMTLF_Operator_FK] int  NOT NULL,
    [TLCMTLF_LineNo_FK] int  NOT NULL,
    [TLCMTLF_CutSheet_FK] int  NOT NULL,
    [TLCMTLF_Facility_FK] int  NOT NULL,
    [TLCMTLF_Colour_FK] int  NOT NULL,
    [TLCMTLF_Bundle_No] varchar(20)  NOT NULL,
    [TLCMTLF_Body_Qty] int  NOT NULL,
    [TLCMTLF_Sleeve_Qty] int  NOT NULL,
    [TLCMTLF_Labels_Qty] int  NOT NULL,
    [TLCMTLF_Size_FK] int  NOT NULL,
    [TLCMTLF_Difference] int  NOT NULL,
    [TLCMTFL_TransDate] datetime  NOT NULL,
    [TLCMTFL_Comments] varchar(50)  NULL
);
GO

-- Creating table 'TLCSV_BoxSelected'
CREATE TABLE [dbo].[TLCSV_BoxSelected] (
    [TLCSV_Pk] int IDENTITY(1,1) NOT NULL,
    [TLCSV_TransDate] datetime  NOT NULL,
    [TLCSV_From_FK] int  NOT NULL,
    [TLCSV_To_FK] int  NOT NULL,
    [TLCSV_TransNumber] int  NOT NULL,
    [TLCSV_PickingList] bit  NOT NULL,
    [TLCSV_PLTransDate] datetime  NULL,
    [TLCSV_Despatched] bit  NOT NULL,
    [TLCSV_DespatchedDate] datetime  NULL,
    [TLCSV_PLDetails] varchar(50)  NOT NULL,
    [TLCSV_DNDeails] varchar(50)  NULL,
    [TLCSV_DNTransNumber] int  NULL,
    [TLCSV_Receipted] bit  NOT NULL,
    [TLCSV_DateReceipted] datetime  NULL
);
GO

-- Creating table 'TLCSV_BoxSplit'
CREATE TABLE [dbo].[TLCSV_BoxSplit] (
    [TLCMTBS_Pk] int IDENTITY(1,1) NOT NULL,
    [TLCMTBS_Completed_FK] int  NOT NULL,
    [TLCMTBS_BoxNo] varchar(20)  NOT NULL,
    [TLCMTBS_Qty] int  NOT NULL,
    [TLCMTBS_Weight] decimal(18,4)  NOT NULL,
    [TLCMTBS_Grade] varchar(5)  NOT NULL,
    [TLCMTBS_AuthorisedBy] varchar(50)  NOT NULL,
    [TLCMTBS_AdjustmentNo] int  NOT NULL
);
GO

-- Creating table 'TLDYE_DyeOrderDetails'
CREATE TABLE [dbo].[TLDYE_DyeOrderDetails] (
    [TLDYOD_Pk] int IDENTITY(1,1) NOT NULL,
    [TLDYOD_DyeOrder_Fk] int  NOT NULL,
    [TLDYOD_Labels_FK] int  NULL,
    [TLDYOD_Rating] decimal(18,4)  NOT NULL,
    [TLDYOD_Yield] decimal(18,4)  NOT NULL,
    [TLDYOD_Kgs] decimal(18,4)  NULL,
    [TLDYOD_BodyOrTrim] bit  NOT NULL,
    [TLDYOD_Units] int  NOT NULL,
    [TLDYOD_Greige_FK] int  NOT NULL,
    [TLDYOD_MarkerRating_FK] int  NOT NULL,
    [TLDYOD_Trims_FK] int  NULL,
    [TLDYOD_OriginalUnit] int  NOT NULL,
    [TLDYOD_Styles_FK] int  NOT NULL,
    [TLDYOD_Sizes_FK] int  NOT NULL
);
GO

-- Creating table 'TLSEC_UserSections'
CREATE TABLE [dbo].[TLSEC_UserSections] (
    [TLSECDEP_Pk] int IDENTITY(1,1) NOT NULL,
    [TLSECDEP_User_FK] int  NOT NULL,
    [TLSECDEP_Section_FK] int  NOT NULL,
    [TLSECDEP_Department_FK] int  NOT NULL,
    [TLSECDEP_AccessGranted] bit  NOT NULL
);
GO

-- Creating table 'TLADM_DailyLog'
CREATE TABLE [dbo].[TLADM_DailyLog] (
    [TLDL_Pk] int IDENTITY(1,1) NOT NULL,
    [TLDL_Dept_Fk] int  NOT NULL,
    [TLDL_Date] datetime  NOT NULL,
    [TLDL_IPAddress] varchar(50)  NOT NULL,
    [TLDL_AuthorisedBy] varchar(50)  NULL,
    [TLDL_Comments] varchar(100)  NULL,
    [TLDL_TransDetail] varchar(50)  NOT NULL
);
GO

-- Creating table 'TLADM_Cotton'
CREATE TABLE [dbo].[TLADM_Cotton] (
    [Cotton_Pk] int IDENTITY(1,1) NOT NULL,
    [Cotton_Code] varchar(10)  NOT NULL,
    [Cotton_Description] varchar(50)  NOT NULL,
    [Cotton_UOM_Fk] int  NOT NULL,
    [Cotton_StockType_FK] int  NOT NULL,
    [Cotton_Store_FK] int  NOT NULL,
    [Cotton_Origin_FK] int  NOT NULL,
    [Cotton_ShowQty] bit  NOT NULL,
    [Cotton_ShowStdCost] bit  NOT NULL,
    [Cotton_ROL] decimal(18,6)  NOT NULL,
    [Cotton_ROQ] decimal(18,6)  NOT NULL,
    [Cotton_Contact] varchar(50)  NULL,
    [Cotton_Grade] varchar(12)  NOT NULL,
    [Cotton_Notes] varchar(max)  NULL,
    [Cotton_StdCost] decimal(19,4)  NOT NULL,
    [Cotton_Units] int  NOT NULL,
    [Cotton_Agent_FK] int  NOT NULL
);
GO

-- Creating table 'TLADM_StyleColour'
CREATE TABLE [dbo].[TLADM_StyleColour] (
    [STYCOL_Pk] int IDENTITY(1,1) NOT NULL,
    [STYCOL_Style_FK] int  NOT NULL,
    [STYCOL_Colour_FK] int  NULL
);
GO

-- Creating table 'TLKNI_YarnTransactionDetails'
CREATE TABLE [dbo].[TLKNI_YarnTransactionDetails] (
    [KnitYD_Pk] int IDENTITY(1,1) NOT NULL,
    [KnitYD_KnitY_FK] int  NOT NULL,
    [KnitYD_PalletNo_FK] int  NOT NULL,
    [KnitYD_YarnType_FK] int  NOT NULL,
    [KnitYD_GrossWeight] decimal(18,4)  NOT NULL,
    [KnitYD_NettWeight] decimal(18,4)  NOT NULL,
    [KnitYD_NoOfCones] int  NOT NULL,
    [KnitYD_WriteOff] bit  NOT NULL,
    [KnitYD_RTS] bit  NULL,
    [KnitYD_TransactionDate] datetime  NULL,
    [KnitYD_TransactionNumber] int  NULL,
    [KnitYD_Notes] varchar(max)  NULL,
    [KnitYD_TransactionType] int  NOT NULL,
    [KnitYD_ApprovedBy] varchar(50)  NULL,
    [KnitYD_OriginalOrderNo] int  NULL,
    [KnitYD_YarnReturned] bit  NOT NULL,
    [KnitYD_YarnWithDrew] bit  NOT NULL
);
GO

-- Creating table 'TLSPN_YarnOrderPallets'
CREATE TABLE [dbo].[TLSPN_YarnOrderPallets] (
    [YarnOP_Pk] int IDENTITY(1,1) NOT NULL,
    [YarnOP_YarnOrder_FK] int  NULL,
    [YaqrnOP_YarnReceived_FK] int  NULL,
    [YarnOP_PalletNo] int  NOT NULL,
    [YarnOP_CDN] int  NOT NULL,
    [YarnOP_Grade] varchar(10)  NULL,
    [YarnOP_PackBy] varchar(50)  NULL,
    [YarnOP_DispatchedBy] varchar(50)  NULL,
    [YarnOP_ReceivedBy] varchar(50)  NULL,
    [YarnOP_GrossWeight] decimal(18,2)  NOT NULL,
    [YarnOP_TareWeight] decimal(18,2)  NOT NULL,
    [YarnOP_NettWeight] decimal(18,2)  NOT NULL,
    [YarnOP_DatePacked] datetime  NULL,
    [YarnOP_DateDispatched] datetime  NULL,
    [YarnOP_DateRecieved] datetime  NULL,
    [YarnOP_NoOfCones] int  NOT NULL,
    [YarnOP_Complete] bit  NOT NULL,
    [YarnOP_NoOfConesSpun] int  NOT NULL,
    [YarnOP_Issued] bit  NOT NULL,
    [YarnOP_Sold] bit  NOT NULL,
    [YarnOP_Scrapped] bit  NOT NULL,
    [YarnOP_Store_FK] int  NOT NULL,
    [YarnOP_CommisionCust] bit  NOT NULL,
    [YarnOP_RTS] bit  NOT NULL,
    [YarnOP_PalletReserved] bit  NOT NULL,
    [YarnOP_PalletReserved_FK] int  NULL,
    [YarnOP_YarnType_FK] int  NOT NULL,
    [YarnOP_PalletReservedWeight] decimal(18,2)  NOT NULL,
    [YarnOP_YarnReceived_FK] int  NULL,
    [YarnOP_CommissionCust] bit  NOT NULL,
    [YarnOP_ReservedBy] int  NULL,
    [YarnOP_PalletsReservedCones] int  NOT NULL,
    [YarnOP_Operator_FK] int  NOT NULL,
    [YarnOP_YarnAvailable] bit  NOT NULL
);
GO

-- Creating table 'TLKNI_Order'
CREATE TABLE [dbo].[TLKNI_Order] (
    [KnitO_Pk] int IDENTITY(1,1) NOT NULL,
    [KnitO_Product_FK] int  NOT NULL,
    [KnitO_Weight] decimal(18,4)  NOT NULL,
    [KnitO_NoOfPieces] int  NOT NULL,
    [KnitO_Machine_FK] int  NOT NULL,
    [KnitO_YLTSetting] decimal(18,4)  NOT NULL,
    [KnitO_OrderDate] datetime  NOT NULL,
    [KnitO_DeliveryDate] datetime  NOT NULL,
    [KnitO_Customer_FK] int  NOT NULL,
    [KnitO_OrderNumber] int  NOT NULL,
    [KnitO_YarnO_FK] int  NULL,
    [KnitO_Notes] varchar(max)  NULL,
    [KnitO_KnitO_FK] int  NULL,
    [KnitO_OrderConfirmed] bit  NOT NULL,
    [KnitO_OrderConfirmedDate] datetime  NULL,
    [KnitO_Closed] bit  NOT NULL,
    [KnitO_ClosedDate] datetime  NULL,
    [KnitO_Confirmed] bit  NOT NULL,
    [KnitO_ProductionCaptured] bit  NOT NULL,
    [KnitO_YarnReturned] bit  NOT NULL,
    [KnitO_ReOpen] bit  NOT NULL,
    [KnitO_CommisionCust] bit  NOT NULL,
    [KnitO_ProductionStartDate] datetime  NULL,
    [KnitO_ProductionEndDate] datetime  NULL,
    [KnitO_YarnAssigned] bit  NOT NULL,
    [KnitO_Colour_Fk] int  NULL,
    [KnitO_Size_Fk] int  NULL
);
GO

-- Creating table 'TLADM_StylesGrades'
CREATE TABLE [dbo].[TLADM_StylesGrades] (
    [TLSG_Pk] int IDENTITY(1,1) NOT NULL,
    [TLSG_Style_Fk] int  NOT NULL,
    [TLSG_Grade_A] varchar(10)  NOT NULL,
    [TLSG_Grade_B] varchar(10)  NOT NULL
);
GO

-- Creating table 'TLSEC_Departments'
CREATE TABLE [dbo].[TLSEC_Departments] (
    [TLSECDT_Pk] int IDENTITY(1,1) NOT NULL,
    [TLSECDT_Description] varchar(50)  NOT NULL
);
GO

-- Creating table 'TLADM_ConsumablesDC'
CREATE TABLE [dbo].[TLADM_ConsumablesDC] (
    [ConsDC_Pk] int IDENTITY(1,1) NOT NULL,
    [ConsDC_Code] varchar(10)  NOT NULL,
    [ConsDC_Description] varchar(50)  NOT NULL,
    [ConsDC_UOM_Fk] int  NOT NULL,
    [ConsDC_AUOM_FK] int  NOT NULL,
    [ConsDC_ConsFactors] decimal(18,6)  NOT NULL,
    [ConsDC_StockType_FK] int  NOT NULL,
    [ConsDC_Hazardous] bit  NOT NULL,
    [ConsDC_ShowQty] bit  NOT NULL,
    [ConsDC_ShowStdCost] bit  NULL,
    [ConsDC_ReOrderLevel] int  NOT NULL,
    [ConsDC_Economic_ReOrderQty] int  NOT NULL,
    [ConsDC_MinReorderQty] int  NOT NULL,
    [ConsDC_DelLeadTime] int  NOT NULL,
    [ConsDC_StockTake_FK] int  NOT NULL,
    [ConsDC_StandardCost] decimal(19,4)  NOT NULL,
    [ConsDC_Units] int  NOT NULL,
    [ConsDC_PreferedSupplier_FK] int  NOT NULL,
    [ConsDC_Consignment] bit  NOT NULL,
    [ConsDC_Notes] varchar(max)  NULL,
    [ConsDC_HazChem] varchar(max)  NULL,
    [ConsDC_StoreCode_FK] int  NOT NULL,
    [ConsDC_ConsGroup_FK] int  NOT NULL,
    [ConsDC_Discontinued] bit  NOT NULL,
    [ConsDC_DiscontinuedDate] datetime  NULL
);
GO

-- Creating table 'TLDYE_RecipeColourDefinition'
CREATE TABLE [dbo].[TLDYE_RecipeColourDefinition] (
    [TLDYECD_Pk] int IDENTITY(1,1) NOT NULL,
    [TLDYECD_Receipe_FK] int  NOT NULL,
    [TLDYECD_Consumable_FK] int  NOT NULL,
    [TLDYECD_MELFC] decimal(18,4)  NOT NULL,
    [TLDYECD_Colour_FK] int  NOT NULL
);
GO

-- Creating table 'TLADM_CottonContracts'
CREATE TABLE [dbo].[TLADM_CottonContracts] (
    [CottonCon_Pk] int IDENTITY(1,1) NOT NULL,
    [CottonCon_ConSupplier_FK] int  NOT NULL,
    [CottonCon_No] varchar(10)  NOT NULL,
    [CottonCon_Reference] varchar(10)  NOT NULL,
    [CottonCon_ContractDate] datetime  NOT NULL,
    [CottonCon_Mass] int  NOT NULL,
    [CottonCon_NoOfBales] int  NOT NULL,
    [CottonCon_UOM_Fk] int  NOT NULL,
    [CottonCon_StartDate] datetime  NOT NULL,
    [CottonCon_PerMonth] decimal(18,2)  NOT NULL,
    [CottonCon_EndDate] datetime  NOT NULL,
    [CottonCon_USPriceperLb] decimal(19,4)  NOT NULL,
    [CottonCon_USPricePerKg] decimal(19,4)  NOT NULL,
    [CottonCon_ZAPricePerKg] decimal(19,4)  NOT NULL,
    [CottonCon_Description] varchar(50)  NULL,
    [CottonCon_Remarks] varchar(max)  NULL,
    [CottonCon_ShowOutStandingKg] bit  NOT NULL,
    [CottonCon_ShowTotalKgReceived] bit  NOT NULL,
    [CottonCon_MicraFrom] decimal(18,4)  NOT NULL,
    [CottonCon_MicraTo] decimal(18,4)  NOT NULL,
    [CottonCon_StapleFrom] decimal(18,4)  NOT NULL,
    [CottonCon_StapleTo] decimal(18,4)  NOT NULL,
    [CottonCon_Closed] bit  NOT NULL,
    [CottonCon_DateClosed] datetime  NULL
);
GO

-- Creating table 'TLCUT_CutMeasureActuals'
CREATE TABLE [dbo].[TLCUT_CutMeasureActuals] (
    [TLCUTM_Pk] int IDENTITY(1,1) NOT NULL,
    [TLCUTM_Bundle_FK] int  NOT NULL,
    [TLCUTM_TransDate] datetime  NOT NULL,
    [TLCUTM_Inspector_FK] int  NOT NULL,
    [TLCUTM_Col1] decimal(18,4)  NOT NULL,
    [TLCUTM_Col2] decimal(18,4)  NOT NULL,
    [TLCUTM_Col3] decimal(18,4)  NOT NULL,
    [TLCUTM_Col4] decimal(18,4)  NOT NULL
);
GO

-- Creating table 'TLADM_CutAreaLocations'
CREATE TABLE [dbo].[TLADM_CutAreaLocations] (
    [TLCUTAL_Pk] int IDENTITY(1,1) NOT NULL,
    [TLCUTAL_ShortCode] varchar(10)  NOT NULL,
    [TLCUTAL_Description] varchar(50)  NOT NULL,
    [TLCUTAL_PPS] bit  NOT NULL
);
GO

-- Creating table 'TLADM_CutMeasureStandards'
CREATE TABLE [dbo].[TLADM_CutMeasureStandards] (
    [TLCUTAS_Pk] int IDENTITY(1,1) NOT NULL,
    [TLCUTAS_Standard] decimal(18,4)  NOT NULL,
    [TLCUTAS_Style_FK] int  NULL,
    [TLCUTAS_Size_FK] int  NULL,
    [TLCUTAS_Col1] decimal(18,4)  NOT NULL,
    [TLCUTAS_Col2] decimal(18,4)  NOT NULL,
    [TLCUTAS_Col3] decimal(18,4)  NOT NULL,
    [TLCUTAS_Col4] decimal(18,4)  NOT NULL,
    [TLCUTAS_PPS] bit  NOT NULL
);
GO

-- Creating table 'TLCUT_QAResults'
CREATE TABLE [dbo].[TLCUT_QAResults] (
    [TLCUTQA_Pk] int IDENTITY(1,1) NOT NULL,
    [TLCUTQA_Bundle_FK] int  NOT NULL,
    [TLCUTQA_Inspectore_FK] int  NOT NULL,
    [TLCUTQA_MeasureArea_FK] int  NOT NULL,
    [TLCUTQA_PPS] bit  NOT NULL,
    [TLCUTQA_Date] datetime  NOT NULL,
    [TLCUTQA_Col1] decimal(18,4)  NOT NULL,
    [TLCUTQA_Col2] decimal(18,4)  NOT NULL,
    [TLCUTQA_Col3] decimal(18,4)  NOT NULL,
    [TLCUTQA_Col4] decimal(18,4)  NOT NULL
);
GO

-- Creating table 'TLCSV_MergePODetail'
CREATE TABLE [dbo].[TLCSV_MergePODetail] (
    [TLMerge_PK] int IDENTITY(1,1) NOT NULL,
    [TLMerge_StockOnHand_Fk] int  NOT NULL,
    [TLMerge_PoDetail_FK] int  NOT NULL
);
GO

-- Creating table 'TLADM_WareHouseAssociation'
CREATE TABLE [dbo].[TLADM_WareHouseAssociation] (
    [TLWA_Pk] int IDENTITY(1,1) NOT NULL,
    [TLWA_PrimaryWareHouse] int  NOT NULL,
    [TLWA_SecondaryWareHouse] int  NULL
);
GO

-- Creating table 'TLDYE_ReceipeGreigeQual'
CREATE TABLE [dbo].[TLDYE_ReceipeGreigeQual] (
    [TLGQ_Pk] int IDENTITY(1,1) NOT NULL,
    [TLGQ_ReceipeDef_FK] int  NOT NULL,
    [TLGQ_GreigeQuality_FK] int  NOT NULL,
    [TLGQ_ColourChart_FK] int  NULL
);
GO

-- Creating table 'TLDYE_RecipeDefinition'
CREATE TABLE [dbo].[TLDYE_RecipeDefinition] (
    [TLDYE_DefinePk] int IDENTITY(1,1) NOT NULL,
    [TLDYE_DefineCode] varchar(50)  NOT NULL,
    [TLDYE_DefineDescription] varchar(50)  NOT NULL,
    [TLDYE_ColorChart_FK] int  NULL,
    [TLDYE_ProgramLoad] int  NOT NULL,
    [TLDYE_LiquidLoad] int  NOT NULL,
    [TLDYE_StandardReceipe] bit  NOT NULL
);
GO

-- Creating table 'TLCUT_CutSheetReceiptDetail'
CREATE TABLE [dbo].[TLCUT_CutSheetReceiptDetail] (
    [TLCUTSHRD_Pk] int IDENTITY(1,1) NOT NULL,
    [TLCUTSHRD_CutSheet_FK] int  NOT NULL,
    [TLCUTSHRD_Description] varchar(20)  NOT NULL,
    [TLCUTSHRD_Size_FK] int  NOT NULL,
    [TLCUTSHRD_BundleQty] int  NOT NULL,
    [TLCUTSHRD_BoxNumber] varchar(50)  NULL,
    [TLCUTSHRD_BoxUnits] int  NOT NULL,
    [TLCUTSHRD_BoxType_FK] int  NULL,
    [TLCUTSHRD_CurrentStore_FK] int  NOT NULL,
    [TLCUTSHRD_TransactionType] int  NOT NULL,
    [TLCUTSHRD_PanelRejected] bit  NOT NULL,
    [TLCUTSHRD_RejectDate] datetime  NULL,
    [TLCUTSHRD_RejectQty] int  NOT NULL,
    [TLCUTSHRD_RejectReason] int  NOT NULL,
    [TLCUTSHRD_ToCMT] bit  NOT NULL,
    [TLCUTSHRD_InBundleStore] bit  NOT NULL,
    [TLCUTSHRD_PanelDate] datetime  NULL,
    [TLCUTSHRD_ToCMTDate] datetime  NULL,
    [TLCUTSHRD_OnHold] bit  NOT NULL,
    [TLCUTSHRD_OnHold_FK] int  NOT NULL,
    [TLCUTSHRD_WasteMeasurement] decimal(18,4)  NOT NULL
);
GO

-- Creating table 'TLADM_StyleTrim'
CREATE TABLE [dbo].[TLADM_StyleTrim] (
    [StyTrim_Pk] int IDENTITY(1,1) NOT NULL,
    [StyTrim_Styles_Fk] int  NOT NULL,
    [StyTrim_Trim_Fk] int  NOT NULL,
    [StyTrim_ProdRating_FK] int  NOT NULL,
    [StyTrim_Discontinued] bit  NOT NULL
);
GO

-- Creating table 'TLKNI_GreigeCommissionTransctions'
CREATE TABLE [dbo].[TLKNI_GreigeCommissionTransctions] (
    [GreigeCom_PK] int IDENTITY(1,1) NOT NULL,
    [GreigeCom_PieceNo] varchar(20)  NOT NULL,
    [GreigeCom_GrnNo] int  NOT NULL,
    [GreigeCom_Transdate] datetime  NOT NULL,
    [GreigeCom_Custdoc] varchar(20)  NOT NULL,
    [GreigeCom_TTSNo] varchar(20)  NOT NULL,
    [GreigeCom_ProductType_FK] int  NOT NULL,
    [GreigeCom_Grade] varchar(5)  NOT NULL,
    [GreigeCom_CustOrderNo] varchar(25)  NOT NULL,
    [GreigeCom_Customer_FK] int  NOT NULL,
    [GreigeCom_NettWeight] decimal(18,4)  NOT NULL,
    [GreigeCom_Comments] varchar(50)  NULL,
    [GreigeCom_Store_FK] int  NOT NULL,
    [GreigeCom_ApprovedBy] varchar(50)  NULL,
    [GreigeCom_Reason] varchar(50)  NULL,
    [GreigeCom_SentDye] bit  NOT NULL,
    [GreigeCom_Batched] bit  NOT NULL,
    [GreigeCom_DyeBatch_FK] int  NULL,
    [GreigeCom_Display] varchar(20)  NULL,
    [GreigeCom_DeliveryNo] int  NOT NULL,
    [GreigeCom_Sold] bit  NOT NULL,
    [GreigeCom_Delivered] bit  NOT NULL,
    [GreigeCom_DeliveryDate] datetime  NULL,
    [GreigeCom_PreparationDate] datetime  NULL
);
GO

-- Creating table 'TLKNI_MachineLastNumber'
CREATE TABLE [dbo].[TLKNI_MachineLastNumber] (
    [TLMDD_Pk] int IDENTITY(1,1) NOT NULL,
    [TLMDD_Machine_FK] int  NOT NULL,
    [TLMDD_LastNumber] int  NOT NULL,
    [TLMDD_MachineCode] varchar(10)  NOT NULL
);
GO

-- Creating table 'TLADM_LastNumberUsed'
CREATE TABLE [dbo].[TLADM_LastNumberUsed] (
    [LUN_Pk] int IDENTITY(1,1) NOT NULL,
    [LUN_Department_FK] int  NOT NULL,
    [col1] int  NOT NULL,
    [col2] int  NOT NULL,
    [col3] int  NOT NULL,
    [col4] int  NOT NULL,
    [col5] int  NOT NULL,
    [col6] int  NOT NULL,
    [col7] int  NOT NULL,
    [col8] int  NOT NULL,
    [col9] int  NOT NULL,
    [col10] int  NOT NULL,
    [col11] int  NOT NULL,
    [col12] int  NOT NULL,
    [col13] int  NOT NULL,
    [col14] int  NOT NULL
);
GO

-- Creating table 'TLKNI_YarnAllocTransctions'
CREATE TABLE [dbo].[TLKNI_YarnAllocTransctions] (
    [TLKYT_Pk] int IDENTITY(1,1) NOT NULL,
    [TLKYT_TranType] int  NOT NULL,
    [TLKYT_YOP_FK] int  NOT NULL,
    [TLKYT_TransDate] datetime  NOT NULL,
    [TLKYT_NoOfCones] int  NOT NULL,
    [TLKYT_NettWeight] decimal(18,4)  NOT NULL,
    [TLKYT_KnitOrder_FK] int  NOT NULL
);
GO

-- Creating table 'TLCMT_Statistics'
CREATE TABLE [dbo].[TLCMT_Statistics] (
    [CMTS_Pk] int IDENTITY(1,1) NOT NULL,
    [CMTS_PanelIssue_FK] int  NOT NULL,
    [CMTS_Transdate] datetime  NOT NULL,
    [CMTS_TotalPanelIssued] int  NOT NULL,
    [CMTS_Total_A_Grades] int  NOT NULL,
    [CMTS_Total_B_Grades] int  NOT NULL,
    [CMTS_Total_Difference] int  NOT NULL,
    [CMTS_Panels] int  NOT NULL
);
GO

-- Creating table 'TLCMT_PanelIssue'
CREATE TABLE [dbo].[TLCMT_PanelIssue] (
    [CMTPI_Pk] int IDENTITY(1,1) NOT NULL,
    [CMTPI_Number] int  NOT NULL,
    [CMTPI_Date] datetime  NOT NULL,
    [CMTPI_TranType_FK] int  NOT NULL,
    [CMTPI_Department_FK] int  NOT NULL,
    [CMTPI_Closed] bit  NOT NULL,
    [CMTPI_DeliveryNumber] int  NOT NULL,
    [CMTPI_Receipted] bit  NOT NULL,
    [CMTPI_CutSheetSummary] bit  NOT NULL,
    [CMTPI_FromWhse_FK] int  NOT NULL,
    [CMTPI_Display] varchar(20)  NULL,
    [CMTPI_Cancelled] bit  NOT NULL
);
GO

-- Creating table 'TLCMT_DeflectFlaw'
CREATE TABLE [dbo].[TLCMT_DeflectFlaw] (
    [TLCMTDF_Pk] int IDENTITY(1,1) NOT NULL,
    [TLCMTDF_ShortCode] varchar(5)  NOT NULL,
    [TLCMTDF_Description] varchar(50)  NOT NULL,
    [TLCMTDF_Manufacturing] bit  NOT NULL
);
GO

-- Creating table 'TLADM_AuxColours'
CREATE TABLE [dbo].[TLADM_AuxColours] (
    [AuxCol_Id] int IDENTITY(1,1) NOT NULL,
    [AuxCol_Description] varchar(50)  NOT NULL,
    [AuxCol_FinishedCode] varchar(10)  NOT NULL,
    [AuxCol_Colour_Fk] int  NOT NULL
);
GO

-- Creating table 'TLADM_CMTNonCompliance'
CREATE TABLE [dbo].[TLADM_CMTNonCompliance] (
    [CMTNC_Pk] int IDENTITY(1,1) NOT NULL,
    [CMTNC_ShortCode] varchar(10)  NULL,
    [CMTNC_Description] varchar(50)  NOT NULL
);
GO

-- Creating table 'TLADM_BoxTypes'
CREATE TABLE [dbo].[TLADM_BoxTypes] (
    [TLADMBT_Pk] int IDENTITY(1,1) NOT NULL,
    [TLADMBT_ShortCode] nchar(10)  NOT NULL,
    [TLADMBT_Description] varchar(50)  NOT NULL,
    [TLADMBT_DryWeight] decimal(18,1)  NOT NULL
);
GO

-- Creating table 'TLCSV_WhseTransferDetail'
CREATE TABLE [dbo].[TLCSV_WhseTransferDetail] (
    [TLCSVWHTD_Pk] int IDENTITY(1,1) NOT NULL,
    [TLCSVWHTD_WhseTranfer_FK] int  NOT NULL,
    [TLCSVWHTD_TLSOH_Fk] int  NOT NULL,
    [TLCSVWHTD_PickList] bit  NOT NULL,
    [TLCSVWHTD_DeliveryNote] bit  NOT NULL,
    [TLCSVWHTD_Receipted] bit  NOT NULL
);
GO

-- Creating table 'TLCSV_WhseTransfer'
CREATE TABLE [dbo].[TLCSV_WhseTransfer] (
    [TLCSVWHT_Pk] int IDENTITY(1,1) NOT NULL,
    [TLCSVWHT_Date] datetime  NOT NULL,
    [TLCSVWHT_FromWhse_Fk] int  NOT NULL,
    [TLCSVWHT_ToWhse_Fk] int  NOT NULL,
    [TLCSVWHT_PickList] bit  NOT NULL,
    [TLCSVWHT_PickListDate] datetime  NULL,
    [TLCSVWHT_PickListNo] int  NOT NULL,
    [TLCSVWHT_DeliveryNote] bit  NOT NULL,
    [TLCSVWHT_DeliveryNo] int  NOT NULL,
    [TLCSVWHT_Receipted] bit  NOT NULL,
    [TLCSVWHT_Receipt_Date] datetime  NULL,
    [TLCSVWHT_ReceiptNo] int  NOT NULL
);
GO

-- Creating table 'TLADM_CustomerAccess'
CREATE TABLE [dbo].[TLADM_CustomerAccess] (
    [CustAcc_Pk] int IDENTITY(1,1) NOT NULL,
    [CustAcc_Customer_Fk] int  NOT NULL,
    [CustAcc_User_Fk] int  NOT NULL
);
GO

-- Creating table 'TLCMT_AuditMeasureRecorded'
CREATE TABLE [dbo].[TLCMT_AuditMeasureRecorded] (
    [TLBFAR_Pk] int IDENTITY(1,1) NOT NULL,
    [TLBFAR_CutSheet_FK] int  NOT NULL,
    [TLBFAR_AuditMeasure_FK] int  NOT NULL,
    [TLBFAR_Department_FK] int  NOT NULL,
    [TLDFAR_Date] datetime  NOT NULL,
    [TLDFAR_Prod1] decimal(18,4)  NOT NULL,
    [TLDFAR_Prod2] decimal(18,4)  NOT NULL,
    [TLDFAR_Prod3] decimal(18,4)  NOT NULL,
    [TLDFAR_Prod4] decimal(18,4)  NOT NULL,
    [TLDFAR_Prod5] decimal(18,4)  NOT NULL,
    [TLDFAR_Prod6] decimal(18,4)  NOT NULL,
    [TLDFAR_Prod7] decimal(18,4)  NOT NULL,
    [TLDFAR_Prod8] decimal(18,4)  NOT NULL,
    [TLDFAR_Prod9] decimal(18,4)  NOT NULL,
    [TLDFAR_Prod10] decimal(18,4)  NOT NULL,
    [TLDFAR_Inspector_FK] int  NOT NULL,
    [TLDFAR_Size_FK] int  NOT NULL
);
GO

-- Creating table 'TLCMT_PanelIssueDetail'
CREATE TABLE [dbo].[TLCMT_PanelIssueDetail] (
    [CMTPID_Pk] int IDENTITY(1,1) NOT NULL,
    [CMTPID_PI_FK] int  NOT NULL,
    [CMTPID_CutSheet_FK] int  NOT NULL,
    [CMTPID_Receipted] bit  NOT NULL,
    [CMTPID_BIFabric] bit  NOT NULL,
    [CMTPID_BIFabricDetail_FK] int  NOT NULL,
    [CMTPID_CutSheetSummary] bit  NOT NULL
);
GO

-- Creating table 'TLKNI_BoughtInFabric'
CREATE TABLE [dbo].[TLKNI_BoughtInFabric] (
    [TLBIN_Pk] int IDENTITY(1,1) NOT NULL,
    [TLBIN_TransDate] datetime  NOT NULL,
    [TLBIN_COfOrigin_FK] int  NOT NULL,
    [TLBIN_Greige_FK] int  NOT NULL,
    [TLBIN_Their_PN] varchar(20)  NOT NULL,
    [TLBIN_TTS_PN] varchar(20)  NOT NULL,
    [TLBIN_Machine_FK] int  NOT NULL,
    [TLBIN_Dsk_Weight] decimal(18,4)  NOT NULL,
    [TLBIN_Dsk_Width] decimal(18,4)  NOT NULL,
    [TLBIN_Nett_Weight] decimal(18,4)  NOT NULL,
    [TLBIN_CurrentStore_FK] int  NOT NULL,
    [TLBIN_TransNumber] int  NOT NULL,
    [TLBIN_Colour_FK] int  NOT NULL,
    [TLBIN_Meters_Roll] decimal(18,4)  NOT NULL
);
GO

-- Creating table 'TLDYE_BIFInTransitDetails'
CREATE TABLE [dbo].[TLDYE_BIFInTransitDetails] (
    [BIFD_Pk] int IDENTITY(1,1) NOT NULL,
    [BIFD_Intransit_FK] int  NOT NULL,
    [BIFD_Greige_FK] int  NOT NULL,
    [BIFD_DyeBatchDetail_FK] int  NOT NULL,
    [BIFD_Colour_FK] int  NOT NULL
);
GO

-- Creating table 'TLDYE_BIFInTransit'
CREATE TABLE [dbo].[TLDYE_BIFInTransit] (
    [BIFT_Pk] int IDENTITY(1,1) NOT NULL,
    [BIFT_PickingList] bit  NOT NULL,
    [BIFT_PickingList_Date] datetime  NOT NULL,
    [BIFT_PickingList_Number] int  NOT NULL,
    [BIFT_Despatched] bit  NOT NULL,
    [BIFT_Despatched_Date] datetime  NULL,
    [BIFT_Despatch_FK] int  NOT NULL,
    [BIFT_Receipted] bit  NOT NULL,
    [BIFT_Receipt_Date] datetime  NULL,
    [BIFT_FromFabric_FK] int  NOT NULL,
    [BIFT_ToFabric_FK] int  NOT NULL,
    [BIFT_Delivery_Number] int  NOT NULL
);
GO

-- Creating table 'TLCMT_NonCompliance'
CREATE TABLE [dbo].[TLCMT_NonCompliance] (
    [CMTNCD_Pk] int IDENTITY(1,1) NOT NULL,
    [CMTNCD_Applicable] bit  NOT NULL,
    [CMTNCD_NonCompliance_Fk] int  NOT NULL,
    [CMTNCD_CutSheet_Fk] int  NOT NULL,
    [CMTNCD_TransDate] datetime  NOT NULL,
    [CMTNCD_WeekNumber] int  NOT NULL,
    [CMTNCD_Year] int  NOT NULL,
    [CMTNCD_Style_FK] int  NOT NULL,
    [CMTNCD_Line_FK] int  NOT NULL
);
GO

-- Creating table 'TLCSV_Movement'
CREATE TABLE [dbo].[TLCSV_Movement] (
    [TLMV_Pk] int IDENTITY(1,1) NOT NULL,
    [TLMV_FromCMT_Fk] int  NULL,
    [TLMV_ToWhse_FK] int  NULL,
    [TLMV_Customer_FK] int  NULL,
    [TLMV_TransactionNumber] int  NOT NULL,
    [TLMV_TransDate] datetime  NOT NULL,
    [TLMV_BoxSelected_FK] int  NULL,
    [TLMV_NoOfBoxes] int  NOT NULL,
    [TLMV_BoxedQty] int  NULL,
    [TLMV_OriginalNumber] varchar(50)  NULL,
    [TLMV_AuthorisedBy] varchar(50)  NULL,
    [TLMV_Reasons] varchar(50)  NULL,
    [TLMV_BoxNumber] varchar(50)  NULL,
    [TLMV_AjustedBoxQty] int  NOT NULL
);
GO

-- Creating table 'TLADM_BoxType_Packing_Specifications'
CREATE TABLE [dbo].[TLADM_BoxType_Packing_Specifications] (
    [TLBPS_Pk] int IDENTITY(1,1) NOT NULL,
    [TLBPS_BoxType_Fk] int  NOT NULL,
    [TLBPS_Style_Fk] int  NOT NULL,
    [TLBPS_Size_Fk] int  NOT NULL,
    [TLBPS_Quantity] int  NOT NULL,
    [TLBPS_Volume_CM] decimal(18,4)  NOT NULL
);
GO

-- Creating table 'TLCSV_BoxConfiguration'
CREATE TABLE [dbo].[TLCSV_BoxConfiguration] (
    [BxCon_Pk] int IDENTITY(1,1) NOT NULL,
    [BxCon_OrderNo_FK] int  NOT NULL,
    [BxCon_Instance_No] int  NOT NULL,
    [BxCon_Style_FK] int  NOT NULL,
    [BxCon_Size_FK] int  NOT NULL,
    [BxCon_Quantity_Per_Box] int  NOT NULL,
    [BxCon_Total_Boxes] int  NOT NULL
);
GO

-- Creating table 'TLCSV_RePackConfig'
CREATE TABLE [dbo].[TLCSV_RePackConfig] (
    [PORConfig_Pk] int IDENTITY(1,1) NOT NULL,
    [PORConfig_PONumber_Fk] int  NOT NULL,
    [PORConfig_Style_FK] int  NOT NULL,
    [PORConfig_Colour_FK] int  NOT NULL,
    [PORConfig_Size_FK] int  NOT NULL,
    [PORConfig_SizeBoxQty] int  NOT NULL,
    [PORConfig_SizeBoxQty_Picked] int  NOT NULL,
    [PORConfig_StockOnHand_FK] int  NOT NULL,
    [PORConfig_SizeBoxQty_Delivered] int  NOT NULL,
    [PORConfig_TotalBoxes] int  NOT NULL,
    [PORConfig_BoxNumber] varchar(20)  NOT NULL,
    [PORConfig_Display] varchar(20)  NULL,
    [PORConfig_BoxNumber_Key] int  NOT NULL
);
GO

-- Creating table 'TLCSV_RePackTransactions'
CREATE TABLE [dbo].[TLCSV_RePackTransactions] (
    [REPACT_Pk] int IDENTITY(1,1) NOT NULL,
    [REPACT_BoxedQty] int  NOT NULL,
    [REPACT_RePackConfig_FK] int  NOT NULL,
    [REPACT_StockOnHand_FK] int  NOT NULL,
    [REPACT_PurchaseOrderDetail_FK] int  NOT NULL,
    [REPACT_PurchaseOrder_FK] int  NOT NULL
);
GO

-- Creating table 'TLPPS_ProductionLeadTime'
CREATE TABLE [dbo].[TLPPS_ProductionLeadTime] (
    [TLPDL_Pk] int IDENTITY(1,1) NOT NULL,
    [TLPDL_Description] varchar(50)  NOT NULL,
    [TLPDL_LineNo] int  NOT NULL,
    [TLPDL_LeadTimeDays] int  NOT NULL
);
GO

-- Creating table 'TLCMT_UnitProductionTargets'
CREATE TABLE [dbo].[TLCMT_UnitProductionTargets] (
    [TLUPT_Pk] int IDENTITY(1,1) NOT NULL,
    [TLUPT_CMT_Pk] int  NOT NULL,
    [TLUPT_TransDate] datetime  NULL,
    [TLUPT_LineNo_Fk] int  NOT NULL,
    [TLUPT_Style_Fk] int  NOT NULL,
    [TLUPT_Unit_Target] int  NOT NULL
);
GO

-- Creating table 'TLSEC_Sections'
CREATE TABLE [dbo].[TLSEC_Sections] (
    [TLSECSect_Pk] int IDENTITY(1,1) NOT NULL,
    [TLSECSect_Department_FK] int  NOT NULL,
    [TLSECSect_Name] varchar(150)  NOT NULL,
    [TLSECSect_Description] varchar(150)  NOT NULL,
    [TLSECSect_InUse] bit  NOT NULL
);
GO

-- Creating table 'TLKNI_YarnOrderPallets'
CREATE TABLE [dbo].[TLKNI_YarnOrderPallets] (
    [TLKNIOP_Pk] int IDENTITY(1,1) NOT NULL,
    [TLKNIOP_PalletNo] int  NOT NULL,
    [TLKNIOP_TLPalletNo] varchar(50)  NULL,
    [TLKNIOP_Grade] varchar(10)  NULL,
    [TLKNIOP_YarnType_FK] int  NOT NULL,
    [TLKNIOP_Cones] int  NOT NULL,
    [TLKNIOP_GrossWeight] decimal(18,4)  NOT NULL,
    [TLKNIOP_TareWeight] decimal(18,4)  NOT NULL,
    [TLKNIOP_NettWeight] decimal(18,4)  NOT NULL,
    [TLKNIOP_ConesReserved] int  NOT NULL,
    [TLKNIOP_NettWeightReserved] decimal(18,4)  NOT NULL,
    [TLKNIOP_YarnOrder_FK] int  NULL,
    [TLKNIOP_Store_FK] int  NOT NULL,
    [TLKNIOP_HeaderRecord_FK] int  NULL,
    [TLKNIOP_DatePacked] datetime  NULL,
    [TLKNIOP_CommisionCust] bit  NOT NULL,
    [TLKNIOP_OwnYarn] bit  NOT NULL,
    [TLKNIOP_ReservedBy] int  NOT NULL,
    [TLKNIOP_OrderConfirmed] bit  NOT NULL,
    [TLKNIOP_ReservedDate] datetime  NULL,
    [TLKNIOP_SplitPallet] bit  NOT NULL,
    [TLKNIOP_ConesReturned] int  NOT NULL,
    [TLKNIOP_NettWeightReturned] decimal(18,4)  NOT NULL,
    [TLKNIOP_PalletAllocated] bit  NOT NULL,
    [TLKNIOP_CommissionCustomer_FK] int  NOT NULL,
    [TLKNIOP_NettWeightConsummed] decimal(18,4)  NOT NULL,
    [TLKNIOP_AdditionalYarn] decimal(18,4)  NOT NULL
);
GO

-- Creating table 'TLCMT_FactConfig'
CREATE TABLE [dbo].[TLCMT_FactConfig] (
    [TLCMTCFG_Pk] int IDENTITY(1,1) NOT NULL,
    [TLCMTCFG_Department_FK] int  NOT NULL,
    [TLCMTCFG_LineNo] int  NOT NULL,
    [TLCMTCFG_Description] varchar(50)  NOT NULL,
    [TLCMTCFG_Quality_FK] int  NOT NULL,
    [TLCMTCFG_NoOfOperators] int  NOT NULL,
    [TLCMTCFG_StdOutput] decimal(18,4)  NOT NULL,
    [TLCMTCFG_UOM_FK] int  NOT NULL,
    [TLCMTCFG_Operator] varchar(50)  NOT NULL,
    [TLCMTCFG_DisplayOrder] int  NOT NULL,
    [TLCMTCFG_CurrentlyOperational] bit  NOT NULL
);
GO

-- Creating table 'TLPPS_Replenishment'
CREATE TABLE [dbo].[TLPPS_Replenishment] (
    [TLREP_Pk] int IDENTITY(1,1) NOT NULL,
    [TLREP_Style_FK] int  NOT NULL,
    [TLREP_Colour_FK] int  NOT NULL,
    [TLREP_Size_FK] int  NOT NULL,
    [TLREP_ExpectedSales] int  NOT NULL,
    [TLREP_ReOrderLevel] int  NOT NULL,
    [TLREP_ReOrderQty] int  NOT NULL,
    [TLREP_ReOrderLevelWeeks] int  NOT NULL,
    [TLREP_ReorderQtyWeeks] int  NOT NULL,
    [TLREP_Discontinued] bit  NOT NULL
);
GO

-- Creating table 'TLCSV_PickingListMaster'
CREATE TABLE [dbo].[TLCSV_PickingListMaster] (
    [TLPL_Pk] int IDENTITY(1,1) NOT NULL,
    [TLPL_IPAddress] varchar(20)  NOT NULL,
    [TLPL_PODetail_FK] int  NOT NULL,
    [TLPL_StockOnHand_FK] int  NOT NULL,
    [TLPL_BoxRePack_FK] int  NOT NULL,
    [TLPL_StandardTrans] bit  NOT NULL
);
GO

-- Creating table 'TLDye_QualityException'
CREATE TABLE [dbo].[TLDye_QualityException] (
    [TLDyeIns_Pk] int IDENTITY(1,1) NOT NULL,
    [TLDyeIns_DyeBatch_Fk] int  NOT NULL,
    [TLDyeIns_Quantity] int  NOT NULL,
    [TLDyeIns_QADyeProcessField_Fk] int  NOT NULL,
    [TLDyeIns_TransactionDate] datetime  NOT NULL,
    [TLDyeIns_GriegeProduct_Fk] int  NOT NULL
);
GO

-- Creating table 'TLSEC_UserAccess'
CREATE TABLE [dbo].[TLSEC_UserAccess] (
    [TLSECUA_Pk] int IDENTITY(1,1) NOT NULL,
    [TLSECUA_UserName] varchar(50)  NOT NULL,
    [TLSECUA_UserPassword] varchar(50)  NOT NULL,
    [TLSECUA_ConfirmedPassword] bit  NOT NULL,
    [TLSECUA_SuperUser] bit  NOT NULL,
    [TLSECUA_Discontinued] bit  NOT NULL,
    [TLSECUA_DisDate] datetime  NULL,
    [TLSECUA_Reason] varchar(50)  NULL,
    [TLSECUA_External] bit  NOT NULL,
    [TLSUCUA_EmailAddress] varchar(50)  NULL,
    [TLSECUA_QAFunction] bit  NOT NULL,
    [TLSECUA_DownSizeAuthority] bit  NOT NULL,
    [TLSECUA_IgnoreFivePercent] bit  NOT NULL
);
GO

-- Creating table 'TLCSV_OrderAllocated'
CREATE TABLE [dbo].[TLCSV_OrderAllocated] (
    [TLORDA_Pk] int IDENTITY(1,1) NOT NULL,
    [TLORDA_POOrder_FK] int  NOT NULL,
    [TLORDA_TransNumber] int  NOT NULL,
    [TLORDA_TransDate] datetime  NOT NULL,
    [TLORDA_PickListPrint] bit  NOT NULL,
    [TLORDA_PLPrintDate] datetime  NULL,
    [TLORDA_Delivered] bit  NOT NULL,
    [TLORDA_DelTransNumber] int  NOT NULL,
    [TLORDA_DeliveredDate] datetime  NULL,
    [TLORDA_Invoiced] bit  NOT NULL,
    [TLORDA_DateInvoiced] datetime  NULL,
    [TLORDA_WareHouse_FK] int  NOT NULL,
    [TLORDA_Customer_FK] int  NOT NULL,
    [TLORDA_Transporter] varchar(50)  NULL,
    [TLORDA_ReturnReason] varchar(50)  NULL,
    [TLORDA_ReturnCustRef] varchar(50)  NULL,
    [TLORDA_ReturnNumber] int  NOT NULL,
    [TLORDA_ApprovedBy] varchar(50)  NULL,
    [TLORDA_Returned] bit  NOT NULL,
    [TLORDA_ReturnedDate] datetime  NULL,
    [TLORDA_BoxSelected] bit  NOT NULL,
    [TLORDA_PLStockOrder] bit  NOT NULL,
    [TLORDA_Transporter_FK] int  NULL,
    [TLORDA_PLConfirmed] bit  NOT NULL
);
GO

-- Creating table 'TLADM_Transporters'
CREATE TABLE [dbo].[TLADM_Transporters] (
    [TLTRNS_Pk] int IDENTITY(1,1) NOT NULL,
    [TLTRNS_Description] varchar(50)  NOT NULL
);
GO

-- Creating table 'TLCMT_ProductionCosts'
CREATE TABLE [dbo].[TLCMT_ProductionCosts] (
    [CMTP_Pk] int IDENTITY(1,1) NOT NULL,
    [CMTP_Style_FK] int  NOT NULL,
    [CMTP_Colour_FK] int  NULL,
    [CMTP_Size_FK] int  NULL,
    [CMTP_CMTFacility_FK] int  NOT NULL,
    [CMTP_Production_Cost] decimal(18,4)  NOT NULL,
    [CMTP_Production_Damage] decimal(18,4)  NOT NULL,
    [CMTP_Production_Loss] decimal(18,4)  NOT NULL,
    [CMTP_CMTLineNo_FK] int  NOT NULL
);
GO

-- Creating table 'TLADM_CMTMeasurementPoints'
CREATE TABLE [dbo].[TLADM_CMTMeasurementPoints] (
    [CMTMP_Pk] int IDENTITY(1,1) NOT NULL,
    [CMTMP_ShortCode] varchar(10)  NOT NULL,
    [CMTMP_Description] varchar(50)  NOT NULL,
    [CMTMP_B2MRawPanels] bit  NOT NULL,
    [CMTMP_DisplayOrder] int  NOT NULL,
    [CMTMP_Active] bit  NOT NULL
);
GO

-- Creating table 'TLADM_FabWidth'
CREATE TABLE [dbo].[TLADM_FabWidth] (
    [FW_Id] int IDENTITY(1,1) NOT NULL,
    [FW_Description] varchar(50)  NOT NULL,
    [FW_PowerN] int  NOT NULL,
    [FW_Discontinued] bit  NULL,
    [FW_Discontinued_Date] datetime  NULL,
    [FW_Calculation_Value] int  NOT NULL
);
GO

-- Creating table 'TLADM_QualityDefinition'
CREATE TABLE [dbo].[TLADM_QualityDefinition] (
    [QD_Pk] int IDENTITY(1,1) NOT NULL,
    [QD_ShortCode] varchar(5)  NOT NULL,
    [QD_Description] varchar(50)  NOT NULL,
    [QD_RejectReasonFK] int  NOT NULL,
    [QD_ReportingDept_FK] int  NOT NULL,
    [QD_OriginatingDept_FK] int  NOT NULL,
    [QD_GradeCol] bit  NOT NULL,
    [QD_SplitCol] bit  NOT NULL,
    [QD_Measurable] bit  NOT NULL,
    [QD_ColumnIndex] int  NOT NULL
);
GO

-- Creating table 'TLCMT_LineIssue'
CREATE TABLE [dbo].[TLCMT_LineIssue] (
    [TLCMTLI_Pk] int IDENTITY(1,1) NOT NULL,
    [TLCMTLI_Date] datetime  NOT NULL,
    [TLCMTLI_CutSheet_FK] int  NOT NULL,
    [TLCMTLI_CmtFacility_FK] int  NOT NULL,
    [TLCMTLI_LineNo_FK] int  NOT NULL,
    [TLCMTLI_PanelIssue_FK] int  NOT NULL,
    [TLCMTLI_WorkCompleted] bit  NOT NULL,
    [TLCMTLI_WorkCompletedDate] datetime  NULL,
    [TLCMTLI_TransferApproved] varchar(50)  NULL,
    [TLCMTLI_TransferDate] datetime  NULL,
    [TLCMTLI_Reason] varchar(50)  NULL,
    [TLCMTLI_ToLineNo_FK] int  NULL,
    [TLCMTLI_TransactionNo] int  NULL,
    [TLCMTLI_IssueQty] int  NOT NULL,
    [TLCMTLI_WhStore_FK] int  NOT NULL,
    [TLCMTLI_IssuedToLine] bit  NOT NULL,
    [TLCMTLI_CutSheetDetails] varchar(15)  NULL,
    [TLCMTLI_OnHold] bit  NOT NULL,
    [TLCMTLI_BoxType_FK] int  NOT NULL,
    [TLCMTLI_Priority] bit  NOT NULL,
    [TLCMTLI_Priority_Date] datetime  NULL,
    [TLCMTLI_Required_Date] datetime  NULL,
    [TLCMTLI_MarkedForDeltion] bit  NOT NULL
);
GO

-- Creating table 'TLADM_CottonOrigin'
CREATE TABLE [dbo].[TLADM_CottonOrigin] (
    [CottonOrigin_Pk] int IDENTITY(1,1) NOT NULL,
    [CottonOrigin_ShortCode] varchar(10)  NOT NULL,
    [CottonOrigin_Description] varchar(50)  NOT NULL,
    [CottonOrigin_LastNumber] int  NOT NULL
);
GO

-- Creating table 'TLDYE_DyeBatch'
CREATE TABLE [dbo].[TLDYE_DyeBatch] (
    [DYEB_Pk] int IDENTITY(1,1) NOT NULL,
    [DYEB_BatchNo] varchar(50)  NULL,
    [DYEB_SequenceNo] int  NOT NULL,
    [DYEB_DyeOrder_FK] int  NOT NULL,
    [DYEB_Colour_FK] int  NOT NULL,
    [DYEB_BatchDate] datetime  NULL,
    [DYEB_RequiredDate] datetime  NULL,
    [DYEB_BatchKG] decimal(18,4)  NOT NULL,
    [DYEB_Lab] bit  NOT NULL,
    [DYEB_Wrap] bit  NOT NULL,
    [DYEB_Notes] varchar(max)  NULL,
    [DYEB_Closed] bit  NOT NULL,
    [DYEB_Transfered] bit  NOT NULL,
    [DYEB_TransferDate] datetime  NULL,
    [DYEB_Allocated] bit  NOT NULL,
    [DYEB_Stage1] bit  NOT NULL,
    [DYEB_Stage2] bit  NOT NULL,
    [DYEB_Stage3] bit  NOT NULL,
    [DYEB_TransactionType_FK] int  NOT NULL,
    [DYEB_Reprocess] bit  NOT NULL,
    [DYEB_OutProcess] bit  NOT NULL,
    [DYEB_OutProcessDate] datetime  NULL,
    [DYEB_FabricMode] bit  NOT NULL,
    [DYEB_CommissinCust] bit  NOT NULL,
    [DYEB_Customer_FK] int  NOT NULL,
    [DYEB_OriginalBatch_FK] int  NOT NULL,
    [DYEB_Greige_FK] int  NOT NULL,
    [DYEB_QExceptionCause] varchar(50)  NULL,
    [DYEB_OnHold] bit  NOT NULL,
    [DYEB_OnHold_Date] datetime  NULL,
    [DYEB_OnHold_Reason] varchar(50)  NULL,
    [DYEB_QAInspected] bit  NOT NULL,
    [DYEB_Stage4] bit  NOT NULL,
    [DYEB_FabicSales] bit  NOT NULL,
    [DYEB_Stage5] bit  NOT NULL,
    [DYEB_Stage6] bit  NOT NULL,
    [DYEB_DateStage1] datetime  NULL,
    [DYEB_DateStage2] datetime  NULL,
    [DYEB_DateStage3] datetime  NULL,
    [DYEB_DateStage4] datetime  NULL,
    [DYEB_DateStage5] datetime  NULL,
    [DYEB_DateStage6] datetime  NULL
);
GO

-- Creating table 'TLCMT_CompletedWork'
CREATE TABLE [dbo].[TLCMT_CompletedWork] (
    [TLCMTWC_Pk] int IDENTITY(1,1) NOT NULL,
    [TLCMTWC_PanelIssue_FK] int  NOT NULL,
    [TLCMTWC_TransactionDate] datetime  NOT NULL,
    [TLCMTWC_LineIssue_FK] int  NOT NULL,
    [TLCMTWC_ToWhse_FK] int  NOT NULL,
    [TLCMTWC_BoxNumber] varchar(20)  NOT NULL,
    [TLCMTWC_PastelNumber] varchar(20)  NOT NULL,
    [TLCMTWC_Size_FK] int  NOT NULL,
    [TLCMTWC_Grade] varchar(5)  NOT NULL,
    [TLCMTWC_Qty] int  NOT NULL,
    [TLCMTWC_Weight] decimal(18,4)  NOT NULL,
    [TLCMTWC_CutSheet_FK] int  NOT NULL,
    [TLCMTWC_Despatched] bit  NOT NULL,
    [TLCMTWC_DepatchedList_FK] int  NULL,
    [TLCMTWC_Style_FK] int  NOT NULL,
    [TLCMTWC_Colour_FK] int  NOT NULL,
    [TLCMTWC_CMTFacility_FK] int  NOT NULL,
    [TLCMTWC_Picked] bit  NOT NULL,
    [TLCMTWC_PickList_FK] int  NULL,
    [TLCMTWC_BoxType_FK] int  NOT NULL,
    [TLCMTWC_BoxReceiptedWhse] bit  NOT NULL,
    [TLCMTWC_CMTBilled] bit  NOT NULL,
    [TLCMTWC_Is_A] bit  NOT NULL,
    [TLCMTWC_MarkedForDeletion] bit  NOT NULL
);
GO

-- Creating table 'TLADM_GreigeColour'
CREATE TABLE [dbo].[TLADM_GreigeColour] (
    [Grcl_PK] int IDENTITY(1,1) NOT NULL,
    [Grcl_Greige_FK] int  NOT NULL,
    [Grlc_Colour_FK] int  NOT NULL
);
GO

-- Creating table 'TLCUT_CutSheetReceipt'
CREATE TABLE [dbo].[TLCUT_CutSheetReceipt] (
    [TLCUTSHR_Pk] int IDENTITY(1,1) NOT NULL,
    [TLCUTSHR_CutSheet_FK] int  NOT NULL,
    [TLCUTSHR_Date] datetime  NOT NULL,
    [TLCUTSHR_NoOfBundles] int  NOT NULL,
    [TLCUTSHR_Machine_FK] int  NOT NULL,
    [TLCUTSHR_Style_FK] int  NOT NULL,
    [TLCUTSHR_Colour_FK] int  NOT NULL,
    [TLCUTSHR_Issued] bit  NOT NULL,
    [TLCUTSHR_InBundleStore] bit  NOT NULL,
    [TLCUTSHR_InPanelStore] bit  NOT NULL,
    [TLCUTSHR_DateIntoPanelStore] datetime  NULL,
    [TLCUTSHR_DateIntoBunStore] datetime  NULL,
    [TLCUTSHR_InReceiptCage] bit  NOT NULL,
    [TLCUTSHR_WhsePanStore_FK] int  NOT NULL,
    [TLCUTSHR_WhseBunStore_FK] int  NOT NULL,
    [TLCUTSHR_WastePanels] decimal(18,4)  NOT NULL,
    [TLCUTSHR_WasteCutSheet] decimal(18,4)  NOT NULL
);
GO

-- Creating table 'TLCUT_ExpectedUnits'
CREATE TABLE [dbo].[TLCUT_ExpectedUnits] (
    [TLCUTE_Pk] int IDENTITY(1,1) NOT NULL,
    [TLCUTE_CutSheet_FK] int  NOT NULL,
    [TLCUTE_Size_FK] int  NOT NULL,
    [TLCUTE_NoofGarments] int  NOT NULL,
    [TLCUTE_NoOfTrims] decimal(18,4)  NOT NULL,
    [TLCUTE_NoOfBinding] decimal(18,4)  NOT NULL,
    [TLCUTE_MarkerRatio] decimal(18,1)  NOT NULL,
    [TLCUTE_EstNettWeight] decimal(18,4)  NOT NULL
);
GO

-- Creating table 'TLSPN_YarnProductionPerMachine'
CREATE TABLE [dbo].[TLSPN_YarnProductionPerMachine] (
    [YarnProduction_PK] int IDENTITY(1,1) NOT NULL,
    [MachineNo_FK] int  NOT NULL,
    [YarnProduction] decimal(18,0)  NULL,
    [YarnProductionDate] datetime  NULL
);
GO

-- Creating table 'TLADM_ProductRating'
CREATE TABLE [dbo].[TLADM_ProductRating] (
    [Pr_Id] int IDENTITY(1,1) NOT NULL,
    [Pr_Customer_FK] int  NOT NULL,
    [Pr_Style_FK] int  NOT NULL,
    [Pr_Size_Power] int  NOT NULL,
    [Pr_BodyorRibbing] int  NOT NULL,
    [Pr_PowerN] int  NOT NULL,
    [Pr_Ratio] decimal(18,4)  NOT NULL,
    [Pr_Marker_Length] decimal(18,6)  NOT NULL,
    [Pr_numeric_Rating] decimal(18,6)  NOT NULL,
    [Pr_Display] varchar(max)  NULL,
    [Pr_Size_FK] int  NOT NULL,
    [Pr_Trim_FK] int  NOT NULL,
    [Pr_TrimKeyString] varchar(250)  NULL,
    [Pr_MultiMarker] bit  NOT NULL,
    [Pr_Discontinued] bit  NOT NULL,
    [PR_Discontinued_Date] datetime  NULL,
    [PR_FabricWidth] varchar(50)  NULL
);
GO

-- Creating table 'TLCMT_AuditMeasurements'
CREATE TABLE [dbo].[TLCMT_AuditMeasurements] (
    [CMTBFA_Pk] int IDENTITY(1,1) NOT NULL,
    [CMTBFA_Customer_FK] int  NOT NULL,
    [CMTBFA_Style_FK] int  NOT NULL,
    [CMTBFA_Size_FK] int  NOT NULL,
    [CMTBFA_MeasureP_FK] int  NOT NULL,
    [CMTBFA_Measurement] decimal(18,4)  NOT NULL
);
GO

-- Creating table 'TLCMT_HistoryBoxedQty'
CREATE TABLE [dbo].[TLCMT_HistoryBoxedQty] (
    [BoxHist_Pk] int IDENTITY(1,1) NOT NULL,
    [BoxHist_CutSheet_FK] int  NOT NULL,
    [BoxHist_CompletedWork_FK] int  NOT NULL,
    [BoxHist_DateTime] datetime  NOT NULL,
    [BoxHist_Orignal_Qty] int  NOT NULL,
    [BoxHist_New_Qty] int  NOT NULL,
    [BoxHist_Size_FK] int  NOT NULL,
    [BoxHist_No] varchar(50)  NOT NULL
);
GO

-- Creating table 'TLADM_Trims'
CREATE TABLE [dbo].[TLADM_Trims] (
    [TR_Id] int IDENTITY(1,1) NOT NULL,
    [TR_Description] varchar(50)  NOT NULL,
    [TR_powerN] int  NOT NULL,
    [TR_Discontinued] bit  NULL,
    [TR_Discontinued_Date] datetime  NULL,
    [Tr_Body] bit  NOT NULL,
    [TR_Weight] int  NOT NULL,
    [TR_Width] int  NOT NULL,
    [TR_Greige_FK] int  NULL,
    [TR_IsBinding] bit  NOT NULL,
    [TR_Size_FK] int  NULL,
    [TR_IsSizes] bit  NOT NULL,
    [TR_Added_ThisSession] bit  NOT NULL,
    [TR_Removed_ThisSession] bit  NOT NULL
);
GO

-- Creating table 'TLCUT_CutSheet'
CREATE TABLE [dbo].[TLCUT_CutSheet] (
    [TLCutSH_Pk] int IDENTITY(1,1) NOT NULL,
    [TLCutSH_No] varchar(20)  NOT NULL,
    [TLCutSH_Date] datetime  NOT NULL,
    [TLCutSH_DyeBatch_FK] int  NOT NULL,
    [TLCutSH_Size_FK] int  NOT NULL,
    [TLCutSH_Notes] varchar(max)  NULL,
    [TLCutSH_Size_PN] int  NOT NULL,
    [TLCutSH_Accepted] bit  NOT NULL,
    [TLCutSH_WIPComplete] bit  NOT NULL,
    [TLCutSH_Quality_FK] int  NOT NULL,
    [TLCutSH_Styles_FK] int  NOT NULL,
    [TLCutSH_Colour_FK] int  NOT NULL,
    [TLCutSH_Customer_FK] int  NOT NULL,
    [TLCutSH_Department_FK] int  NOT NULL,
    [TLCutSH_Closed] bit  NOT NULL,
    [TLCutSH_ClosedDate] datetime  NULL,
    [TLCUTSH_RequiredDate] datetime  NULL,
    [TLCUTSH_Completed_Date] datetime  NULL,
    [TLCUTSH_MarkedForDeletion] bit  NOT NULL,
    [TLCUTSH_AddNotes] varchar(max)  NULL,
    [TLCUTSH_OnHold] bit  NOT NULL,
    [TLCUTSH_OnHoldDate] datetime  NULL,
    [TLCUTSH_OnHold_Reasons] varchar(50)  NULL,
    [TLCUTSH_Priority] bit  NOT NULL
);
GO

-- Creating table 'TLPPS_InterDept'
CREATE TABLE [dbo].[TLPPS_InterDept] (
    [TLInter_Pk] int IDENTITY(1,1) NOT NULL,
    [TLInter_Description] varchar(50)  NOT NULL,
    [TLInter_Knitting_Fk] int  NOT NULL,
    [TLInter_Dying_Fk] int  NOT NULL,
    [TLInter_CMT_Fk] int  NOT NULL
);
GO

-- Creating table 'TLADM_MachineDefinitions'
CREATE TABLE [dbo].[TLADM_MachineDefinitions] (
    [MD_Pk] int IDENTITY(1,1) NOT NULL,
    [MD_MachineCode] varchar(10)  NOT NULL,
    [MD_Department_FK] int  NOT NULL,
    [MD_FabricType_FK] int  NOT NULL,
    [MD_MaxCapacity] decimal(18,2)  NOT NULL,
    [MD_Description] varchar(50)  NOT NULL,
    [MD_Realistic] decimal(18,2)  NOT NULL,
    [MD_GLCostCentre] varchar(10)  NULL,
    [MD_YarnProduction] bit  NOT NULL,
    [MD_AssetRegNo] varchar(10)  NULL,
    [MD_SerialNo] varchar(15)  NULL,
    [MD_FirstMeasure_FK] int  NULL,
    [MD_FirstMeasure_Qty] decimal(18,4)  NULL,
    [MD_SecMeasure_FK] int  NULL,
    [MD_SecMeasure_Qty] decimal(18,4)  NULL,
    [MD_ThirdMeasure_FK] int  NULL,
    [MD_ThirdMeasure_Qty] decimal(18,4)  NULL,
    [MD_FourthMeasure_FK] int  NULL,
    [MD_FourthMeasure_Qty] decimal(18,4)  NULL,
    [MD_FifthMeasure_FK] int  NULL,
    [MD_FifthMeasure_Qty] decimal(18,4)  NULL,
    [MD_SixMeasure_FK] int  NULL,
    [MD_SixMeasure_Qty] decimal(18,2)  NULL,
    [MD_SevenMeasure_FK] int  NULL,
    [MD_SevenMeasure_Qty] decimal(18,4)  NULL,
    [MD_EightMeasure_FK] int  NULL,
    [MD_EightMeasure_Qty] decimal(18,2)  NULL,
    [MD_FinishGoods_FK] int  NULL,
    [MD_Operators_TotalPN] int  NOT NULL,
    [MD_Maintenance_TotalPN] int  NULL,
    [MD_GreigeType_FK] int  NULL,
    [MD_AlternateDesc] varchar(50)  NULL,
    [MD_LastNumberUsed] int  NOT NULL,
    [MD_Hydro] bit  NOT NULL,
    [MD_Drier] bit  NOT NULL,
    [MD_Compactor] bit  NOT NULL
);
GO

-- Creating table 'TLDYE_NonCompliance'
CREATE TABLE [dbo].[TLDYE_NonCompliance] (
    [TLDYE_NcrPk] int IDENTITY(1,1) NOT NULL,
    [TLDYE_NcrBatchNo_FK] int  NOT NULL,
    [TLDYE_NcrNumber] int  NOT NULL,
    [TLDYE_NcrNotes] varchar(max)  NULL,
    [TLDYE_ComplainceDate] datetime  NOT NULL,
    [TLDYE_Department_FK] int  NOT NULL,
    [TLDYE_Machine_FK] int  NOT NULL,
    [TLDYE_Operator_FK] int  NOT NULL,
    [TLDYE_NCStage] int  NOT NULL,
    [TLDYE_RemedyApplied] bit  NOT NULL,
    [TLDYE_PortStatus] int  NULL
);
GO

-- Creating table 'TLDYE_DyeingStandards'
CREATE TABLE [dbo].[TLDYE_DyeingStandards] (
    [DyeStan_Pk] int IDENTITY(1,1) NOT NULL,
    [DyeStan_Style_FK] int  NOT NULL,
    [DyeStan_Quality_FK] int  NOT NULL,
    [DyeStan_QAProccessField_FK] int  NOT NULL,
    [DyeStan_Value] int  NOT NULL
);
GO

-- Creating table 'TLADM_QADyeProcessFields'
CREATE TABLE [dbo].[TLADM_QADyeProcessFields] (
    [TLQADPF_Pk] int IDENTITY(1,1) NOT NULL,
    [TLQADPF_Process_FK] int  NOT NULL,
    [TLQADPF_ShortCode] varchar(5)  NULL,
    [TLQADPF_Description] varchar(50)  NOT NULL,
    [TLQADPF_Hydro] bit  NOT NULL,
    [TLQADPF_Drier] bit  NOT NULL,
    [TLQAPF_Compactor] bit  NOT NULL,
    [TLQAPF_Decimal] bit  NOT NULL,
    [TLQAPF_ColumnNo] int  NOT NULL,
    [TLQAPF_Padding] bit  NOT NULL,
    [TLQAPF_Operator_Ins] bit  NOT NULL
);
GO

-- Creating table 'TLDYE_ConSummableReceived'
CREATE TABLE [dbo].[TLDYE_ConSummableReceived] (
    [DYECON_Pk] int IDENTITY(1,1) NOT NULL,
    [DYECON_Consumable_FK] int  NOT NULL,
    [DYECON_TransactionDate] datetime  NOT NULL,
    [DYECON_WhseStore_FK] int  NOT NULL,
    [DYECON_Amount] decimal(18,4)  NOT NULL,
    [DYECON_UOM_FK] int  NOT NULL,
    [DYECON_Supplier_FK] int  NOT NULL,
    [DYECON_OrderNo] varchar(50)  NULL,
    [DYECON_ContainerId] varchar(50)  NULL,
    [DYECON_Pass] bit  NOT NULL,
    [DYECON_TransNumber] int  NOT NULL
);
GO

-- Creating table 'TLADM_MachineMaintenanceTasks'
CREATE TABLE [dbo].[TLADM_MachineMaintenanceTasks] (
    [TLMtask_Pk] int IDENTITY(1,1) NOT NULL,
    [TLMtask_Dept_FK] int  NOT NULL,
    [TLMtask_Description] varchar(50)  NOT NULL
);
GO

-- Creating table 'TLADM_MachineMaintenance'
CREATE TABLE [dbo].[TLADM_MachineMaintenance] (
    [mman_Pk] int IDENTITY(1,1) NOT NULL,
    [mman_MaintenanceTask_FK] int  NOT NULL,
    [mman_Machine_Fk] int  NOT NULL,
    [mman_Interval_Between_Tasks] int  NOT NULL,
    [mman_Interval_UOM] int  NOT NULL,
    [mman_Interval_CurrentValue] int  NOT NULL,
    [mman_Reset] bit  NOT NULL,
    [mman_Date_Last_Reset] datetime  NULL,
    [mman_Volume_CurrentValue] int  NOT NULL,
    [mman_Volume_Between_Tasks] int  NOT NULL,
    [mman_Volumne_UOM] int  NOT NULL,
    [mman_Last_Identifier] varchar(50)  NULL
);
GO

-- Creating table 'TLADM_Departments'
CREATE TABLE [dbo].[TLADM_Departments] (
    [Dep_Id] int IDENTITY(1,1) NOT NULL,
    [Dep_ShortCode] varchar(5)  NOT NULL,
    [Dep_Description] varchar(50)  NOT NULL,
    [Dep_PowerN] int  NOT NULL,
    [Dep_UOM] int  NOT NULL,
    [Dep_ProductType_FK] int  NULL,
    [Dep_Location] varchar(50)  NULL,
    [Dep_IsCMT] bit  NOT NULL,
    [Dep_IsCut] bit  NOT NULL,
    [Dep_Hours_Per_Day] decimal(18,4)  NOT NULL
);
GO

-- Creating table 'TLADM_WhseStore'
CREATE TABLE [dbo].[TLADM_WhseStore] (
    [WhStore_Id] int IDENTITY(1,1) NOT NULL,
    [WhStore_Code] varchar(7)  NULL,
    [WhStore_Description] varchar(50)  NOT NULL,
    [WhStore_DepartmentFK] int  NOT NULL,
    [WhStore_TypeFK] int  NOT NULL,
    [WhStore_Address1] varchar(50)  NOT NULL,
    [WhStore_Address2] varchar(50)  NOT NULL,
    [WhStore_Address3] varchar(50)  NULL,
    [WhStore_Address4] varchar(50)  NULL,
    [WhStore_Address5] varchar(50)  NULL,
    [WhStore_Contact] varchar(50)  NULL,
    [WhStore_Telephone] varchar(50)  NULL,
    [WhStore_Email] varchar(50)  NULL,
    [WhStore_Notes] varchar(max)  NULL,
    [WhStore_WhseOrStore] bit  NOT NULL,
    [WhStore_CountryOfOrigin] varchar(50)  NULL,
    [WhStore_DeliveryNote] int  NOT NULL,
    [WhStore_PickingList] int  NOT NULL,
    [WhStore_GradeA] bit  NOT NULL,
    [WhStore_DyeKitchen] bit  NOT NULL,
    [WhStore_ChemicalStore] bit  NOT NULL,
    [WhStore_BundleStore] bit  NOT NULL,
    [WhStore_PanelStore] bit  NOT NULL,
    [WhStore_RePack] bit  NOT NULL,
    [WhStore_Default] bit  NOT NULL,
    [WhStore_Quarantine] bit  NOT NULL,
    [WhStore_RFD] bit  NOT NULL
);
GO

-- Creating table 'TLCSV_BoughtInGoods'
CREATE TABLE [dbo].[TLCSV_BoughtInGoods] (
    [TLBIG_Pk] int IDENTITY(1,1) NOT NULL,
    [TLBIG_TransDate] datetime  NOT NULL,
    [TLBIG_TransNumber] int  NOT NULL,
    [TLBIG_TTIOrderNo] varchar(50)  NOT NULL,
    [TLBIG_Supplier_FK] int  NOT NULL,
    [TLBIG_SupplierDelNo] varchar(50)  NOT NULL,
    [TLBIG_WareHouse_FK] int  NOT NULL
);
GO

-- Creating table 'TLCSV_StockOnHand'
CREATE TABLE [dbo].[TLCSV_StockOnHand] (
    [TLSOH_Pk] int IDENTITY(1,1) NOT NULL,
    [TLSOH_WareHouse_FK] int  NOT NULL,
    [TLSOH_CMT_FK] int  NULL,
    [TLSOH_DateIntoStock] datetime  NULL,
    [TLSOH_BoxSelected_FK] int  NOT NULL,
    [TLSOH_Sold] bit  NOT NULL,
    [TLSOH_SoldDate] datetime  NULL,
    [TLSOH_Notes] varchar(max)  NULL,
    [TLSOH_Transfered] bit  NOT NULL,
    [TLSOH_TransferNotes] varchar(max)  NULL,
    [TLSOH_Split] bit  NOT NULL,
    [TLSOH_BoxedQty] int  NOT NULL,
    [TLSOH_Picked] bit  NOT NULL,
    [TLSOH_PickListNo] int  NULL,
    [TLSOH_PickListDate] datetime  NULL,
    [TLSOH_DNListNo] int  NOT NULL,
    [TLSOH_DNListDate] datetime  NULL,
    [TLSOH_Movement_FK] int  NULL,
    [TLSOH_Style_FK] int  NOT NULL,
    [TLSOH_Colour_FK] int  NOT NULL,
    [TLSOH_Size_FK] int  NOT NULL,
    [TLSOH_BoxNumber] varchar(50)  NULL,
    [TLSOH_Weight] decimal(18,4)  NOT NULL,
    [TLSOH_PastelNumber] varchar(20)  NULL,
    [TLSOH_BoxType] int  NOT NULL,
    [TLSOH_POOrder_FK] int  NULL,
    [TLSOH_POOrderDetail_FK] int  NULL,
    [TLSOH_Returned] bit  NOT NULL,
    [TLSOH_ReturnedDate] datetime  NULL,
    [TLSOH_ReturnNumber] int  NOT NULL,
    [TLSOH_ReturnedBoxQty] int  NOT NULL,
    [TLSOH_ReturnedWeight] decimal(18,4)  NOT NULL,
    [TLSOH_QtyAdjusted] int  NOT NULL,
    [TLSOH_MoveAdjust_FK] int  NULL,
    [TLSOH_Customer_Fk] int  NULL,
    [TLSOH_WareHousePickList] int  NOT NULL,
    [TLSOH_WareHouseDeliveryNo] int  NOT NULL,
    [TLSOH_Grade] varchar(5)  NULL,
    [TLSOH_InTransit] bit  NOT NULL,
    [TLSOH_Session_Key] int  NOT NULL,
    [TLSOH_Write_Off] bit  NOT NULL,
    [TLSOH_Is_A] bit  NOT NULL,
    [TLSOH_RePackConfig_Fk] int  NULL,
    [TLSOH_CutSheet_FK] int  NOT NULL,
    [TLSOH_Invoiced] bit  NOT NULL,
    [TLSOH_InvDate] datetime  NULL,
    [TLSOH_Supplier_Fk] int  NULL,
    [TLSOH_SupplierTransNumber] int  NOT NULL,
    [TLSOH_BoughtInGoods] bit  NOT NULL,
    [TLSOH_BoughtInGoods_Fk] int  NULL,
    [TLSOH_RFD_NotYetDyed] bit  NOT NULL
);
GO

-- Creating table 'TLADM_Sizes'
CREATE TABLE [dbo].[TLADM_Sizes] (
    [SI_id] int IDENTITY(1,1) NOT NULL,
    [SI_Description] varchar(50)  NOT NULL,
    [SI_PowerN] int  NOT NULL,
    [SI_Discontinued] bit  NOT NULL,
    [SI_Discontinued_Date] datetime  NULL,
    [SI_FinishedCode] varchar(10)  NULL,
    [SI_ColNumber] int  NOT NULL,
    [SI_PastelNo] int  NOT NULL,
    [SI_DisplayOrder] int  NOT NULL,
    [SI_Adult] bit  NOT NULL,
    [SI_ContiSize] int  NOT NULL,
    [SI_Display] varchar(50)  NULL
);
GO

-- Creating table 'TLCSV_PuchaseOrderDetail'
CREATE TABLE [dbo].[TLCSV_PuchaseOrderDetail] (
    [TLCUSTO_Pk] int IDENTITY(1,1) NOT NULL,
    [TLCUSTO_PurchaseOrder_FK] int  NOT NULL,
    [TLCUSTO_Style_FK] int  NULL,
    [TLCUSTO_Size_FK] int  NULL,
    [TLCUSTO_Colour_FK] int  NOT NULL,
    [TLCUSTO_Qty] int  NOT NULL,
    [TLCUSTO_Picked] bit  NOT NULL,
    [TLCUSTO_PickedDate] datetime  NULL,
    [TLCUSTO_PickedNumber] int  NOT NULL,
    [TLCUSTO_Invoiced] bit  NOT NULL,
    [TLCUSTO_InvoicedDate] datetime  NULL,
    [TLCUSTO_Delivered] bit  NOT NULL,
    [TLCUSTO_DeliveredDate] datetime  NULL,
    [TLCUSTO_DeliveryNumber] int  NOT NULL,
    [TLCUSTO_LineNumber] int  NOT NULL,
    [TLCUSTO_Closed] bit  NOT NULL,
    [TLCUSTO_Grade] varchar(2)  NOT NULL,
    [TLCUSTO_Transporter] varchar(50)  NULL,
    [TLCUSTO_StockOnHand_FK] int  NULL,
    [TLCUSTO_Customer_FK] int  NOT NULL,
    [TLCUSTO_QtyDelivered_ToDate] int  NOT NULL,
    [TLCUSTO_QtyPicked_ToDate] int  NOT NULL,
    [TLCUSTO_DateRequired] datetime  NULL,
    [TLCUSTO_Quality_FK] int  NULL,
    [TLCUSTO_QtyMeters] decimal(18,4)  NOT NULL,
    [TLCUSTO_QtyMeters_Delivered] decimal(18,4)  NOT NULL
);
GO

-- Creating table 'TLADM_Months'
CREATE TABLE [dbo].[TLADM_Months] (
    [Mth_Pk] int IDENTITY(1,1) NOT NULL,
    [Mth_Description] varchar(5)  NOT NULL
);
GO

-- Creating table 'TLCSV_PurchaseOrder'
CREATE TABLE [dbo].[TLCSV_PurchaseOrder] (
    [TLCSVPO_Pk] int IDENTITY(1,1) NOT NULL,
    [TLCSVPO_Customer_FK] int  NOT NULL,
    [TLCSVPO_TransDate] datetime  NOT NULL,
    [TLCSVPO_RequiredDate] datetime  NOT NULL,
    [TLCSVPO_PurchaseOrder] varchar(50)  NOT NULL,
    [TLCSVPO_Closeed] bit  NOT NULL,
    [TLCSVPO_ClosedDate] datetime  NULL,
    [TLCVSPO_SequenceNo] int  NOT NULL,
    [TLCSVPO_SpecialOrder] bit  NOT NULL,
    [TLCSVPO_RepackTransaction] bit  NOT NULL,
    [TLCSVPO_Provisional] bit  NOT NULL,
    [TLCSVPO_FabricCustomer] bit  NOT NULL,
    [TLCSVPO_ClosedBy] varchar(50)  NULL
);
GO

-- Creating table 'TLADM_Griege'
CREATE TABLE [dbo].[TLADM_Griege] (
    [TLGreige_Id] int IDENTITY(1,1) NOT NULL,
    [TLGreige_Description] varchar(50)  NOT NULL,
    [TLGreige_OldCode] int  NULL,
    [TLGreige_PowerN] int  NOT NULL,
    [TLGriege_Discontinued] bit  NULL,
    [TLGreige_Discontinued_Date] datetime  NULL,
    [TLGreige_BarCode] bit  NOT NULL,
    [TLGreige_FabricWeight_FK] int  NOT NULL,
    [TLGreige_FabricWidth_FK] int  NOT NULL,
    [TLGreige_Machine_FK] int  NOT NULL,
    [TLGreige_ProductType_FK] int  NOT NULL,
    [TLGreige_Quality_FK] int  NOT NULL,
    [TLGreige_ROL] int  NOT NULL,
    [TLGreige_ROQ] int  NOT NULL,
    [TLGreige_ShowQty] bit  NOT NULL,
    [TLGreige_StockTakeFreq_FK] int  NOT NULL,
    [TLGreige_UOM_Fk] int  NOT NULL,
    [TLGreige_YarnPowerN] int  NOT NULL,
    [TLGreige_KgPerPiece] decimal(18,1)  NOT NULL,
    [TLGreige_LatestSuggest] decimal(18,4)  NOT NULL,
    [TLGreige_Meters] int  NOT NULL,
    [TLGreige_FaultsAllowed] int  NOT NULL,
    [TLGreige_IsBoughtIn] bit  NOT NULL,
    [TLGreige_IsLining] bit  NOT NULL,
    [TLGreige_CubicWeight] decimal(18,4)  NOT NULL,
    [TLGreige_Body] bit  NOT NULL
);
GO

-- Creating table 'TLDYE_DyeOrder'
CREATE TABLE [dbo].[TLDYE_DyeOrder] (
    [TLDYO_Pk] int IDENTITY(1,1) NOT NULL,
    [TLDYO_DyeOrderNum] varchar(50)  NOT NULL,
    [TLDYO_OrderNum] varchar(50)  NOT NULL,
    [TLDYO_Customer_FK] int  NOT NULL,
    [TLDYO_Style_FK] int  NOT NULL,
    [TLDYO_Greige_FK] int  NOT NULL,
    [TLDYO_Colour_FK] int  NOT NULL,
    [TLDYO_Label_FK] int  NOT NULL,
    [TLDYO_DyeReqWeek] int  NOT NULL,
    [TLDYO_CutReqWeek] int  NOT NULL,
    [TLDYO_CMTReqWeek] int  NOT NULL,
    [TLDYO_DyePLoss] decimal(18,4)  NOT NULL,
    [TLDYO_CutPLoss] decimal(18,4)  NOT NULL,
    [TLDYO_CMTPLoss] decimal(18,4)  NOT NULL,
    [TLDYO_OrderDate] datetime  NOT NULL,
    [TLDYO_GarmOrFab] bit  NOT NULL,
    [TLDYO_Notes] varchar(max)  NULL,
    [TLDYO_Closed] bit  NOT NULL,
    [TLDYO_CustomerOrder_Fk] int  NULL
);
GO

-- Creating table 'TLDYE_DyeOrderFabric'
CREATE TABLE [dbo].[TLDYE_DyeOrderFabric] (
    [TLDYEF_Pk] int IDENTITY(1,1) NOT NULL,
    [TLDYEF_OrderDate] datetime  NOT NULL,
    [TLDYEF_Customer_FK] int  NOT NULL,
    [TLDYEF_DyeWeek] int  NOT NULL,
    [TLDYEF_PLoss] decimal(18,4)  NOT NULL,
    [TLDYEF_CustomerOrder] varchar(50)  NOT NULL,
    [TLDYEF_DyeOrderNo] varchar(50)  NOT NULL,
    [TLDYEF_Greige_FK] int  NOT NULL,
    [TLDYEF_Yield] decimal(18,4)  NOT NULL,
    [TLDYEF_Quantity] decimal(18,4)  NOT NULL,
    [TLDYEF_ProjectedLoss] decimal(18,4)  NOT NULL,
    [TLDYEF_Demand] decimal(18,4)  NOT NULL,
    [TLDYEF_Colours_FK] int  NOT NULL,
    [TLDYEF_LineNo] int  NOT NULL,
    [TLDYEF_Closed] bit  NOT NULL,
    [TLDYEF_FabricOrder_FK] int  NOT NULL,
    [TLDYEF_DyeOrderNumeric] int  NOT NULL,
    [TLDYEF_Body] bit  NOT NULL,
    [TLDYEF_BatchedToDate] decimal(18,4)  NOT NULL
);
GO

-- Creating table 'TLDYE_DyeTransactions'
CREATE TABLE [dbo].[TLDYE_DyeTransactions] (
    [TLDYET_Pk] int IDENTITY(1,1) NOT NULL,
    [TLDYET_Date] datetime  NOT NULL,
    [TLDYET_RejectDate] datetime  NULL,
    [TLDYET_BatchNo] varchar(50)  NULL,
    [TLDYET_SequenceNo] int  NOT NULL,
    [TLDYET_TransactionNumber] varchar(50)  NULL,
    [TLDYET_TransactionType] int  NOT NULL,
    [TLDYET_BatchWeight] decimal(18,4)  NOT NULL,
    [TLDYET_TransactionWeight] decimal(18,4)  NOT NULL,
    [TLDYET_AuthorisedBy] varchar(50)  NULL,
    [TLDYET_Batch_FK] int  NOT NULL,
    [TLDYET_Customer_FK] int  NULL,
    [TLDYET_CurrentStore_FK] int  NOT NULL,
    [TLDYET_Adjustment_Reasons] varchar(max)  NULL,
    [TLDYET_Stage] int  NOT NULL,
    [TLDYET_Rejected] bit  NOT NULL,
    [TLDYET_WriteOff] bit  NULL,
    [TLDYET_FabricSales] bit  NOT NULL,
    [TLDYET_CustomerOrderNo] varchar(50)  NULL,
    [TLDYET_MeasurementField_FK] int  NOT NULL
);
GO

-- Creating table 'TLDYE_NonComplianceConsDetail'
CREATE TABLE [dbo].[TLDYE_NonComplianceConsDetail] (
    [DYENCCON_Pk] int IDENTITY(1,1) NOT NULL,
    [DYENCCON_ConNumber] int  NOT NULL,
    [DYENCCON_BatchNo_FK] int  NOT NULL,
    [DYENCCON_Code_FK] int  NOT NULL,
    [DYENCCON_ConOrNon] bit  NOT NULL,
    [DYENCCON_Qunatities] decimal(18,4)  NOT NULL,
    [DYENCCON_LiqCalc] bit  NOT NULL,
    [DYENCCON_LiqRatio] decimal(18,4)  NOT NULL
);
GO

-- Creating table 'TLDYE_NonComplianceAnalysis'
CREATE TABLE [dbo].[TLDYE_NonComplianceAnalysis] (
    [TLDYEDC_Pk] int IDENTITY(1,1) NOT NULL,
    [TLDYEDC_BatchNo] int  NOT NULL,
    [TLDYEDC_NCStage] int  NOT NULL,
    [TLDYEDC_PieceNo_FK] int  NOT NULL,
    [TLDYEDC_Code_FK] int  NOT NULL,
    [TLDYEDC_Value] decimal(18,4)  NOT NULL,
    [TLDYEDC_Operator_FK] int  NOT NULL,
    [TLDYEDC_Pass] bit  NOT NULL,
    [TLDYEDC_Date] datetime  NULL,
    [TLDYEDC_Quality_FK] int  NULL
);
GO

-- Creating table 'TLADM_PanelAttributes'
CREATE TABLE [dbo].[TLADM_PanelAttributes] (
    [Pan_PK] int IDENTITY(1,1) NOT NULL,
    [Pan_Description] varchar(50)  NOT NULL,
    [Pan_Discontinued] bit  NULL,
    [Pan_Discontinued_Date] datetime  NULL,
    [Pan_PowerN] int  NOT NULL,
    [Pan_ShowQty] bit  NOT NULL,
    [Pan_Single_Colour] bit  NOT NULL,
    [Pan_Single_Colour_FK] int  NULL,
    [Pan_Style_FK] int  NOT NULL
);
GO

-- Creating table 'TLDYE_ConsumableSOH'
CREATE TABLE [dbo].[TLDYE_ConsumableSOH] (
    [DYCSH_Pk] int IDENTITY(1,1) NOT NULL,
    [DYCSH_Consumable_FK] int  NOT NULL,
    [DYCSH_WhseStore_FK] int  NULL,
    [DYCSH_StockOnHand] decimal(18,4)  NOT NULL,
    [DYCSH_TransNumber] int  NOT NULL,
    [DYCSH_DyeKitchen] bit  NOT NULL,
    [DYCSH_Quarantine] bit  NOT NULL,
    [DYCSH_Pass] bit  NOT NULL,
    [DYCSH_QuarantineStore_FK] int  NOT NULL,
    [DYCSH_DyeKitchen_FK] int  NULL,
    [DYCSH_SOHQuar] decimal(18,4)  NOT NULL,
    [DYCSH_SOHKitchen] decimal(18,4)  NOT NULL,
    [DYCSH_K_Opening] decimal(18,4)  NOT NULL,
    [DYCSH_K_Closing] decimal(18,4)  NOT NULL,
    [DYCSH_K_Used] decimal(18,4)  NOT NULL,
    [DCSH_K_Adjusted] decimal(18,4)  NOT NULL
);
GO

-- Creating table 'TLADM_Colours'
CREATE TABLE [dbo].[TLADM_Colours] (
    [Col_Id] int IDENTITY(1,1) NOT NULL,
    [Col_Description] varchar(50)  NOT NULL,
    [Col_PowerN] int  NOT NULL,
    [Col_Discontinued] bit  NULL,
    [Col_Discontinued_Date] datetime  NULL,
    [Col_FinishedCode] varchar(5)  NULL,
    [Col_HasAuxColors] bit  NOT NULL,
    [Col_AuxPowerN] int  NOT NULL,
    [Col_Display] varchar(50)  NULL,
    [Col_StandardTime] decimal(18,4)  NOT NULL,
    [Col_Benchmark] bit  NOT NULL,
    [Col_Removed_InSession] bit  NOT NULL,
    [Col_Added_InSession] bit  NOT NULL,
    [Col_Padding] bit  NULL,
    [Col_Pastel] varchar(10)  NULL,
    [Col_Ratio] decimal(18,4)  NOT NULL,
    [Col_ColCosting] bit  NOT NULL
);
GO

-- Creating table 'TLKNI_GreigeProduction'
CREATE TABLE [dbo].[TLKNI_GreigeProduction] (
    [GreigeP_Pk] int IDENTITY(1,1) NOT NULL,
    [GreigeP_KnitO_Fk] int  NULL,
    [GreigeP_Greige_Fk] int  NULL,
    [GreigeP_PieceNo] varchar(50)  NULL,
    [GreigeP_weight] decimal(18,4)  NOT NULL,
    [GreigeP_weightAvail] decimal(18,4)  NOT NULL,
    [GreigeP_PDate] datetime  NULL,
    [GreigeP_Machine_FK] int  NULL,
    [GreigeP_Shift_FK] int  NULL,
    [GreigeP_Operator_FK] int  NULL,
    [GreigeP_Captured] bit  NOT NULL,
    [GreigeP_Grade] varchar(5)  NULL,
    [GreigeP_Remarks] varchar(50)  NULL,
    [GreigeP_Meas1] int  NOT NULL,
    [GreigeP_Meas2] int  NOT NULL,
    [GreigeP_Meas3] int  NOT NULL,
    [GreigeP_Meas4] int  NOT NULL,
    [GreigeP_Meas5] int  NOT NULL,
    [GreigeP_Meas6] int  NOT NULL,
    [GreigeP_Meas7] int  NOT NULL,
    [GreigeP_Meas8] int  NOT NULL,
    [GreigeP_Inspected] bit  NOT NULL,
    [GreigeP_InspDate] datetime  NULL,
    [GreigeP_Inspector_FK] int  NULL,
    [GreigeP_Store_FK] int  NULL,
    [GreigeP_Dye] bit  NOT NULL,
    [GreigeP_DyeBatch_FK] int  NULL,
    [GreigeP_CommisionCust] bit  NOT NULL,
    [GreigeP_CommissionCust_FK] int  NULL,
    [GreigeP_PalletNo] varchar(20)  NULL,
    [GreigeP_BoughtIn] bit  NOT NULL,
    [GreigeP_BoughtIn_FabWidth] decimal(18,4)  NOT NULL,
    [GreigeP_BoughtIn_FabWeight] decimal(18,4)  NOT NULL,
    [GreigeP_CommissionGrn] int  NOT NULL,
    [GreigeP_InTransit] bit  NOT NULL,
    [GreigeP_Receipted] bit  NOT NULL,
    [GreigeP_BIFColour_FK] int  NOT NULL,
    [GreigeP_YarnSupplier] varchar(50)  NULL,
    [GreigeP_YarnTex] decimal(18,4)  NOT NULL,
    [GreigeP_Meters] decimal(18,4)  NOT NULL,
    [GreigeP_BoughtIn_Fk] int  NULL,
    [GreigeP_MergeDetail] varchar(30)  NULL,
    [GreigeP_Size_Fk] int  NULL,
    [GreigeP_WarningMessage] bit  NOT NULL,
    [GreigeP_DskWeight] decimal(18,4)  NOT NULL,
    [GreigeP_VarianceDiskWeight] decimal(18,4)  NOT NULL,
    [GreigeP_DiskWidth] decimal(18,4)  NOT NULL,
    [GreigeP_MarkedForDeletion] bit  NOT NULL
);
GO

-- Creating table 'TLADM_GreigeQuality'
CREATE TABLE [dbo].[TLADM_GreigeQuality] (
    [GQ_Pk] int IDENTITY(1,1) NOT NULL,
    [GQ_Description] varchar(50)  NOT NULL,
    [GQ_Discontinued] bit  NULL,
    [GQ_Discontinued_Date] datetime  NULL,
    [GQ_PowerN] int  NOT NULL
);
GO

-- Creating table 'TLADM_StyAssoc'
CREATE TABLE [dbo].[TLADM_StyAssoc] (
    [StyAssoc_Pk] int IDENTITY(1,1) NOT NULL,
    [StyAssoc_StyPk] int  NOT NULL,
    [StyAssoc_StyOther] int  NOT NULL
);
GO

-- Creating table 'TLDYE_RFDHistory'
CREATE TABLE [dbo].[TLDYE_RFDHistory] (
    [DyeRFD_Pk] int IDENTITY(1,1) NOT NULL,
    [DyeRFD_StockOnHand_Fk] int  NOT NULL,
    [DyeRFD_Transaction_No] int  NOT NULL,
    [DyeRFD_BeginDyeDate] datetime  NULL,
    [DyeRFD_FinishDyeDate] datetime  NULL,
    [DyeRFD_CurrentStyle] int  NOT NULL,
    [DyeRFD_DyeToColour] int  NOT NULL,
    [DyeRFD_Completed] bit  NOT NULL,
    [DyeRFD_NoumberOfAGrades] int  NOT NULL,
    [DyeRFD_NumberOfBGrades] int  NOT NULL,
    [DyeRFD_NumberOfOtherGrades] int  NOT NULL
);
GO

-- Creating table 'TLADM_CustomerFile'
CREATE TABLE [dbo].[TLADM_CustomerFile] (
    [Cust_Pk] int IDENTITY(1,1) NOT NULL,
    [Cust_Code] varchar(10)  NOT NULL,
    [Cust_Description] varchar(50)  NOT NULL,
    [Cust_PostalAddress] varchar(max)  NOT NULL,
    [Cust_Telephone] varchar(30)  NULL,
    [Cust_Fax] varchar(30)  NULL,
    [Cust_ContactPerson] varchar(50)  NULL,
    [Cust_ContactPersonEmail] varchar(50)  NULL,
    [Cust_Blocked] bit  NOT NULL,
    [Cust_CustomerCat_FK] int  NOT NULL,
    [Cust_VatReferenced] varchar(50)  NOT NULL,
    [Cust_EmailContact] varchar(50)  NOT NULL,
    [Cust_SendEmail] bit  NOT NULL,
    [Cust_Notes] varchar(max)  NULL,
    [Cust_Address1] varchar(max)  NULL,
    [Cust_CommissionCust] bit  NOT NULL,
    [Cust_GreigePrefix] nchar(10)  NULL,
    [Cust_LastNumberUsed] int  NOT NULL,
    [Cust_OwnStock] bit  NOT NULL,
    [Cust_RePack] bit  NOT NULL,
    [Cust_WareHouse_FK] int  NULL,
    [Cust_FabricCustomer] bit  NOT NULL,
    [Cust_PostalCode] varchar(10)  NULL,
    [Cust_PFD] bit  NOT NULL,
    [Cust_AllowAll] bit  NOT NULL
);
GO

-- Creating table 'sysdiagrams'
CREATE TABLE [dbo].[sysdiagrams] (
    [name] nvarchar(128)  NOT NULL,
    [principal_id] int  NOT NULL,
    [diagram_id] int IDENTITY(1,1) NOT NULL,
    [version] int  NULL,
    [definition] varbinary(max)  NULL
);
GO

-- Creating table 'TLADM_Greige_Yarn'
CREATE TABLE [dbo].[TLADM_Greige_Yarn] (
    [TLQual_Yarn_Id] int IDENTITY(1,1) NOT NULL,
    [TLQual_Yarn_Fk] int  NOT NULL,
    [TLQual_Greige_Fk] int  NOT NULL
);
GO

-- Creating table 'TLADM_Style_Quality'
CREATE TABLE [dbo].[TLADM_Style_Quality] (
    [SQual_Pk] int IDENTITY(1,1) NOT NULL,
    [SQual_Griege_Fk] int  NOT NULL,
    [SQual_Style_Fk] int  NOT NULL
);
GO

-- Creating table 'TLADM_Styles'
CREATE TABLE [dbo].[TLADM_Styles] (
    [Sty_Id] int IDENTITY(1,1) NOT NULL,
    [Sty_Customer_Fk] int  NOT NULL,
    [Sty_Description] varchar(50)  NOT NULL,
    [Sty_Discontinued] bit  NULL,
    [Sty_Discontinued_Date] datetime  NULL,
    [Sty_Sizes_PN] int  NOT NULL,
    [Sty_Trims_PN] int  NOT NULL,
    [Sty_Labels_FK] int  NOT NULL,
    [Sty_ChkMandatory] bit  NOT NULL,
    [Sty_PastelNo] int  NOT NULL,
    [Sty_PastelCode] varchar(10)  NULL,
    [Sty_CottonFactor] int  NOT NULL,
    [Sty_Bags] int  NOT NULL,
    [Sty_Buttons] bit  NOT NULL,
    [Sty_BoughtIn] bit  NOT NULL,
    [Sty_GreigeQual_PN] int  NOT NULL,
    [Sty_Equiv] bit  NOT NULL,
    [Sty_DisplayOrder] int  NOT NULL,
    [Sty_Units_Per_Hour] int  NOT NULL,
    [Sty_WorkWear] bit  NOT NULL,
    [Sty_PFD] bit  NOT NULL
);
GO

-- Creating table 'TLADM_Shifts'
CREATE TABLE [dbo].[TLADM_Shifts] (
    [Shft_Pk] int IDENTITY(1,1) NOT NULL,
    [Shft_Description] varchar(50)  NOT NULL,
    [Shft_Discontinued] bit  NOT NULL,
    [Shft_DiscontinuedDate] datetime  NULL,
    [Shft_Dept_FK] int  NOT NULL,
    [Shft_Start] time  NULL,
    [Shft_End] time  NULL
);
GO

-- Creating table 'TLADM_DepartmentsArea'
CREATE TABLE [dbo].[TLADM_DepartmentsArea] (
    [DeptA_Pk] int IDENTITY(1,1) NOT NULL,
    [DeptA_Description] varchar(50)  NOT NULL,
    [DeptA_Discontinued] bit  NOT NULL,
    [DeptA_DiscontinuedDate] datetime  NULL,
    [DeptA_Dep_Fk] int  NOT NULL
);
GO

-- Creating table 'TLDYE_DyeBatchDetails'
CREATE TABLE [dbo].[TLDYE_DyeBatchDetails] (
    [DYEBD_Pk] int IDENTITY(1,1) NOT NULL,
    [DYEBD_DyeBatch_FK] int  NOT NULL,
    [DYEBD_DyeOrderDet_FK] int  NOT NULL,
    [DYEBD_GreigeProduction_FK] int  NOT NULL,
    [DYEBD_GreigeProduction_Weight] decimal(18,4)  NOT NULL,
    [DYEBD_BodyTrim] bit  NOT NULL,
    [DYEBD_QualityKey] int  NOT NULL,
    [DYEBO_TrimKey] int  NULL,
    [DYEBO_GVRowNumber] int  NOT NULL,
    [DYEBO_Nett] decimal(18,4)  NOT NULL,
    [DYEBO_DiskWeight] decimal(18,4)  NOT NULL,
    [DYEBO_Width] decimal(18,4)  NOT NULL,
    [DYEBO_Meters] decimal(18,4)  NOT NULL,
    [DYEBO_DyeDate] datetime  NULL,
    [DYEBO_TransDate] datetime  NULL,
    [DYEBO_QAApproved] bit  NOT NULL,
    [DYEBO_ApprovalDate] datetime  NULL,
    [DYEBO_Rejected] bit  NOT NULL,
    [DYEBO_RejectedDate] datetime  NULL,
    [DYEBO_TransferPrinted] bit  NULL,
    [DYEBO_WriteOff] bit  NOT NULL,
    [DYEBO_CurrentStore_FK] int  NOT NULL,
    [DYEBO_Sold] bit  NOT NULL,
    [DYEBO_DateSold] datetime  NULL,
    [DYEBO_TransactionNo] varchar(50)  NULL,
    [DYEBO_ProductRating_FK] int  NOT NULL,
    [DYEBO_CutSheet] bit  NOT NULL,
    [DYEBO_AdjustedWeight] decimal(18,4)  NOT NULL,
    [DYEBO_BIFInTransit] bit  NOT NULL,
    [DYEBO_WasRejected] bit  NOT NULL,
    [DYEBO_Notes] varchar(50)  NULL,
    [DYEBO_FWAtCutting] decimal(18,4)  NOT NULL,
    [DYEBO_PurchaseOrderDetail_FK] int  NOT NULL,
    [DYEBO_PendingDelivery] bit  NOT NULL,
    [DYEBO_SaleConfirmed] bit  NOT NULL,
    [DYEBO_FabricDespatched] bit  NOT NULL,
    [DYEBO_MarkedForDeletion] bit  NOT NULL
);
GO

-- Creating table 'TLDYE_GarmentDyeingProduction'
CREATE TABLE [dbo].[TLDYE_GarmentDyeingProduction] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [GarmentDyeingTransactionNo] int  NOT NULL,
    [Size] int  NOT NULL,
    [BoxNo] char(10)  NOT NULL,
    [BoxQuantity] int  NOT NULL,
    [Grade] char(1)  NOT NULL,
    [Closed] bit  NOT NULL
);
GO

-- Creating table 'TLADM_ProductCodes'
CREATE TABLE [dbo].[TLADM_ProductCodes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ProductCode] nvarchar(max)  NOT NULL,
    [StyleId] int  NOT NULL,
    [ColourId] int  NOT NULL,
    [SizeId] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ConG_Pk] in table 'TLADM_ConsumableGroups'
ALTER TABLE [dbo].[TLADM_ConsumableGroups]
ADD CONSTRAINT [PK_TLADM_ConsumableGroups]
    PRIMARY KEY CLUSTERED ([ConG_Pk] ASC);
GO

-- Creating primary key on [ConsOther_Pk] in table 'TLADM_ConsumablesOther'
ALTER TABLE [dbo].[TLADM_ConsumablesOther]
ADD CONSTRAINT [PK_TLADM_ConsumablesOther]
    PRIMARY KEY CLUSTERED ([ConsOther_Pk] ASC);
GO

-- Creating primary key on [CT_Id] in table 'TLADM_CustomerTypes'
ALTER TABLE [dbo].[TLADM_CustomerTypes]
ADD CONSTRAINT [PK_TLADM_CustomerTypes]
    PRIMARY KEY CLUSTERED ([CT_Id] ASC);
GO

-- Creating primary key on [FT_Id] in table 'TLADM_FabricType'
ALTER TABLE [dbo].[TLADM_FabricType]
ADD CONSTRAINT [PK_TLADM_FabricType]
    PRIMARY KEY CLUSTERED ([FT_Id] ASC);
GO

-- Creating primary key on [FWW_Id] in table 'TLADM_FabricWeight'
ALTER TABLE [dbo].[TLADM_FabricWeight]
ADD CONSTRAINT [PK_TLADM_FabricWeight]
    PRIMARY KEY CLUSTERED ([FWW_Id] ASC);
GO

-- Creating primary key on [Fin_Pk] in table 'TLADM_FinishedGoods'
ALTER TABLE [dbo].[TLADM_FinishedGoods]
ADD CONSTRAINT [PK_TLADM_FinishedGoods]
    PRIMARY KEY CLUSTERED ([Fin_Pk] ASC);
GO

-- Creating primary key on [GarDef_Id] in table 'TLADM_GarmentDef'
ALTER TABLE [dbo].[TLADM_GarmentDef]
ADD CONSTRAINT [PK_TLADM_GarmentDef]
    PRIMARY KEY CLUSTERED ([GarDef_Id] ASC);
GO

-- Creating primary key on [Defect_Id] in table 'TLADM_GarmentDefectCodes'
ALTER TABLE [dbo].[TLADM_GarmentDefectCodes]
ADD CONSTRAINT [PK_TLADM_GarmentDefectCodes]
    PRIMARY KEY CLUSTERED ([Defect_Id] ASC);
GO

-- Creating primary key on [NSC_Pk] in table 'TLADM_NonStockCat'
ALTER TABLE [dbo].[TLADM_NonStockCat]
ADD CONSTRAINT [PK_TLADM_NonStockCat]
    PRIMARY KEY CLUSTERED ([NSC_Pk] ASC);
GO

-- Creating primary key on [NSI_Pk] in table 'TLADM_NonStockItems'
ALTER TABLE [dbo].[TLADM_NonStockItems]
ADD CONSTRAINT [PK_TLADM_NonStockItems]
    PRIMARY KEY CLUSTERED ([NSI_Pk] ASC);
GO

-- Creating primary key on [PT_pk] in table 'TLADM_ProductTypes'
ALTER TABLE [dbo].[TLADM_ProductTypes]
ADD CONSTRAINT [PK_TLADM_ProductTypes]
    PRIMARY KEY CLUSTERED ([PT_pk] ASC);
GO

-- Creating primary key on [RJR_Pk] in table 'TLADM_RejectReasons'
ALTER TABLE [dbo].[TLADM_RejectReasons]
ADD CONSTRAINT [PK_TLADM_RejectReasons]
    PRIMARY KEY CLUSTERED ([RJR_Pk] ASC);
GO

-- Creating primary key on [RI_Id] in table 'TLADM_Ribbing'
ALTER TABLE [dbo].[TLADM_Ribbing]
ADD CONSTRAINT [PK_TLADM_Ribbing]
    PRIMARY KEY CLUSTERED ([RI_Id] ASC);
GO

-- Creating primary key on [STF_Pk] in table 'TLADM_StockTakeFreq'
ALTER TABLE [dbo].[TLADM_StockTakeFreq]
ADD CONSTRAINT [PK_TLADM_StockTakeFreq]
    PRIMARY KEY CLUSTERED ([STF_Pk] ASC);
GO

-- Creating primary key on [ST_Id] in table 'TLADM_StockTypes'
ALTER TABLE [dbo].[TLADM_StockTypes]
ADD CONSTRAINT [PK_TLADM_StockTypes]
    PRIMARY KEY CLUSTERED ([ST_Id] ASC);
GO

-- Creating primary key on [StoreT_Id] in table 'TLADM_StoreTypes'
ALTER TABLE [dbo].[TLADM_StoreTypes]
ADD CONSTRAINT [PK_TLADM_StoreTypes]
    PRIMARY KEY CLUSTERED ([StoreT_Id] ASC);
GO

-- Creating primary key on [Sup_Pk] in table 'TLADM_Suppliers'
ALTER TABLE [dbo].[TLADM_Suppliers]
ADD CONSTRAINT [PK_TLADM_Suppliers]
    PRIMARY KEY CLUSTERED ([Sup_Pk] ASC);
GO

-- Creating primary key on [UOM_Pk] in table 'TLADM_UOM'
ALTER TABLE [dbo].[TLADM_UOM]
ADD CONSTRAINT [PK_TLADM_UOM]
    PRIMARY KEY CLUSTERED ([UOM_Pk] ASC);
GO

-- Creating primary key on [Cal_Id] in table 'TLFIN_Calendar'
ALTER TABLE [dbo].[TLFIN_Calendar]
ADD CONSTRAINT [PK_TLFIN_Calendar]
    PRIMARY KEY CLUSTERED ([Cal_Id] ASC);
GO

-- Creating primary key on [Finy_Id] in table 'TLFIN_FinancialYear'
ALTER TABLE [dbo].[TLFIN_FinancialYear]
ADD CONSTRAINT [PK_TLFIN_FinancialYear]
    PRIMARY KEY CLUSTERED ([Finy_Id] ASC);
GO

-- Creating primary key on [FP_Id] in table 'TLADM_FabricProduct'
ALTER TABLE [dbo].[TLADM_FabricProduct]
ADD CONSTRAINT [PK_TLADM_FabricProduct]
    PRIMARY KEY CLUSTERED ([FP_Id] ASC);
GO

-- Creating primary key on [Addit_Pk] in table 'TLADM_AdditionalAddress'
ALTER TABLE [dbo].[TLADM_AdditionalAddress]
ADD CONSTRAINT [PK_TLADM_AdditionalAddress]
    PRIMARY KEY CLUSTERED ([Addit_Pk] ASC);
GO

-- Creating primary key on [AddSty_Pk] in table 'TLADM_StylesAdditional'
ALTER TABLE [dbo].[TLADM_StylesAdditional]
ADD CONSTRAINT [PK_TLADM_StylesAdditional]
    PRIMARY KEY CLUSTERED ([AddSty_Pk] ASC);
GO

-- Creating primary key on [Comp_Pk] in table 'TLADM_CompanyDetails'
ALTER TABLE [dbo].[TLADM_CompanyDetails]
ADD CONSTRAINT [PK_TLADM_CompanyDetails]
    PRIMARY KEY CLUSTERED ([Comp_Pk] ASC);
GO

-- Creating primary key on [CottonAgent_Pk] in table 'TLADM_CottonAgent'
ALTER TABLE [dbo].[TLADM_CottonAgent]
ADD CONSTRAINT [PK_TLADM_CottonAgent]
    PRIMARY KEY CLUSTERED ([CottonAgent_Pk] ASC);
GO

-- Creating primary key on [Haul_Pk] in table 'TLADM_CottonHauliers'
ALTER TABLE [dbo].[TLADM_CottonHauliers]
ADD CONSTRAINT [PK_TLADM_CottonHauliers]
    PRIMARY KEY CLUSTERED ([Haul_Pk] ASC);
GO

-- Creating primary key on [HaulVeh_Pk] in table 'TLADM_CottonHauliersVehicles'
ALTER TABLE [dbo].[TLADM_CottonHauliersVehicles]
ADD CONSTRAINT [PK_TLADM_CottonHauliersVehicles]
    PRIMARY KEY CLUSTERED ([HaulVeh_Pk] ASC);
GO

-- Creating primary key on [TrxT_Pk] in table 'TLADM_TranactionType'
ALTER TABLE [dbo].[TLADM_TranactionType]
ADD CONSTRAINT [PK_TLADM_TranactionType]
    PRIMARY KEY CLUSTERED ([TrxT_Pk] ASC);
GO

-- Creating primary key on [StoreBal_Pk] in table 'TLADM_StoreBal'
ALTER TABLE [dbo].[TLADM_StoreBal]
ADD CONSTRAINT [PK_TLADM_StoreBal]
    PRIMARY KEY CLUSTERED ([StoreBal_Pk] ASC);
GO

-- Creating primary key on [CotRec_Pk] in table 'TLSPN_CottonReceived'
ALTER TABLE [dbo].[TLSPN_CottonReceived]
ADD CONSTRAINT [PK_TLSPN_CottonReceived]
    PRIMARY KEY CLUSTERED ([CotRec_Pk] ASC);
GO

-- Creating primary key on [CotBales_Pk] in table 'TLSPN_CottonReceivedBales'
ALTER TABLE [dbo].[TLSPN_CottonReceivedBales]
ADD CONSTRAINT [PK_TLSPN_CottonReceivedBales]
    PRIMARY KEY CLUSTERED ([CotBales_Pk] ASC);
GO

-- Creating primary key on [OpenBal_Pk] in table 'TLSPN_OpenBalance'
ALTER TABLE [dbo].[TLSPN_OpenBalance]
ADD CONSTRAINT [PK_TLSPN_OpenBalance]
    PRIMARY KEY CLUSTERED ([OpenBal_Pk] ASC);
GO

-- Creating primary key on [YarnQA_Pk] in table 'TLSPN_QAMeasurements'
ALTER TABLE [dbo].[TLSPN_QAMeasurements]
ADD CONSTRAINT [PK_TLSPN_QAMeasurements]
    PRIMARY KEY CLUSTERED ([YarnQA_Pk] ASC);
GO

-- Creating primary key on [KnitY_Pk] in table 'TLKNI_YarnTransaction'
ALTER TABLE [dbo].[TLKNI_YarnTransaction]
ADD CONSTRAINT [PK_TLKNI_YarnTransaction]
    PRIMARY KEY CLUSTERED ([KnitY_Pk] ASC);
GO

-- Creating primary key on [GreigeT_Pk] in table 'TLKNI_GreigeTransactions'
ALTER TABLE [dbo].[TLKNI_GreigeTransactions]
ADD CONSTRAINT [PK_TLKNI_GreigeTransactions]
    PRIMARY KEY CLUSTERED ([GreigeT_Pk] ASC);
GO

-- Creating primary key on [Lbl_Id] in table 'TLADM_Labels'
ALTER TABLE [dbo].[TLADM_Labels]
ADD CONSTRAINT [PK_TLADM_Labels]
    PRIMARY KEY CLUSTERED ([Lbl_Id] ASC);
GO

-- Creating primary key on [YA_Id] in table 'TLADM_Yarn'
ALTER TABLE [dbo].[TLADM_Yarn]
ADD CONSTRAINT [PK_TLADM_Yarn]
    PRIMARY KEY CLUSTERED ([YA_Id] ASC);
GO

-- Creating primary key on [TLProdLoss_Pk] in table 'TLADM_ProductionLoss'
ALTER TABLE [dbo].[TLADM_ProductionLoss]
ADD CONSTRAINT [PK_TLADM_ProductionLoss]
    PRIMARY KEY CLUSTERED ([TLProdLoss_Pk] ASC);
GO

-- Creating primary key on [FbAtrib_Pk] in table 'TLADM_FabricAttributes'
ALTER TABLE [dbo].[TLADM_FabricAttributes]
ADD CONSTRAINT [PK_TLADM_FabricAttributes]
    PRIMARY KEY CLUSTERED ([FbAtrib_Pk] ASC);
GO

-- Creating primary key on [MachOp_Pk] in table 'TLADM_MachineOperators'
ALTER TABLE [dbo].[TLADM_MachineOperators]
ADD CONSTRAINT [PK_TLADM_MachineOperators]
    PRIMARY KEY CLUSTERED ([MachOp_Pk] ASC);
GO

-- Creating primary key on [prd_Id] in table 'TLADM_ProductRating_Detail'
ALTER TABLE [dbo].[TLADM_ProductRating_Detail]
ADD CONSTRAINT [PK_TLADM_ProductRating_Detail]
    PRIMARY KEY CLUSTERED ([prd_Id] ASC);
GO

-- Creating primary key on [TLDYED_PK] in table 'TLDYE_DefinitionDetails'
ALTER TABLE [dbo].[TLDYE_DefinitionDetails]
ADD CONSTRAINT [PK_TLDYE_DefinitionDetails]
    PRIMARY KEY CLUSTERED ([TLDYED_PK] ASC);
GO

-- Creating primary key on [cotrx_pk] in table 'TLSPN_CottonTransactions'
ALTER TABLE [dbo].[TLSPN_CottonTransactions]
ADD CONSTRAINT [PK_TLSPN_CottonTransactions]
    PRIMARY KEY CLUSTERED ([cotrx_pk] ASC);
GO

-- Creating primary key on [TLCTM_Pk] in table 'TLSPN_CottonMerge'
ALTER TABLE [dbo].[TLSPN_CottonMerge]
ADD CONSTRAINT [PK_TLSPN_CottonMerge]
    PRIMARY KEY CLUSTERED ([TLCTM_Pk] ASC);
GO

-- Creating primary key on [TLCTMD_Pk] in table 'TLSPN_CottonMergeDetails'
ALTER TABLE [dbo].[TLSPN_CottonMergeDetails]
ADD CONSTRAINT [PK_TLSPN_CottonMergeDetails]
    PRIMARY KEY CLUSTERED ([TLCTMD_Pk] ASC);
GO

-- Creating primary key on [YarnO_Pk] in table 'TLSPN_YarnOrder'
ALTER TABLE [dbo].[TLSPN_YarnOrder]
ADD CONSTRAINT [PK_TLSPN_YarnOrder]
    PRIMARY KEY CLUSTERED ([YarnO_Pk] ASC);
GO

-- Creating primary key on [TLYW_Pk] in table 'TLSPN_YarnWaste'
ALTER TABLE [dbo].[TLSPN_YarnWaste]
ADD CONSTRAINT [PK_TLSPN_YarnWaste]
    PRIMARY KEY CLUSTERED ([TLYW_Pk] ASC);
GO

-- Creating primary key on [TLDYEA_Pk] in table 'TLDYE_DyeBatchAllocated'
ALTER TABLE [dbo].[TLDYE_DyeBatchAllocated]
ADD CONSTRAINT [PK_TLDYE_DyeBatchAllocated]
    PRIMARY KEY CLUSTERED ([TLDYEA_Pk] ASC);
GO

-- Creating primary key on [QDF_Pk] in table 'TLADM_DyeQDCodes'
ALTER TABLE [dbo].[TLADM_DyeQDCodes]
ADD CONSTRAINT [PK_TLADM_DyeQDCodes]
    PRIMARY KEY CLUSTERED ([QDF_Pk] ASC);
GO

-- Creating primary key on [QRC_Pk] in table 'TLADM_DyeRemendyCodes'
ALTER TABLE [dbo].[TLADM_DyeRemendyCodes]
ADD CONSTRAINT [PK_TLADM_DyeRemendyCodes]
    PRIMARY KEY CLUSTERED ([QRC_Pk] ASC);
GO

-- Creating primary key on [QADYEP_Pk] in table 'TLADM_QADyeProcess'
ALTER TABLE [dbo].[TLADM_QADyeProcess]
ADD CONSTRAINT [PK_TLADM_QADyeProcess]
    PRIMARY KEY CLUSTERED ([QADYEP_Pk] ASC);
GO

-- Creating primary key on [DYENCRD_PK] in table 'TLDYE_NonComplianceDetail'
ALTER TABLE [dbo].[TLDYE_NonComplianceDetail]
ADD CONSTRAINT [PK_TLDYE_NonComplianceDetail]
    PRIMARY KEY CLUSTERED ([DYENCRD_PK] ASC);
GO

-- Creating primary key on [YarnLD_Pk] in table 'TLSPN_YarnOrderLayDown'
ALTER TABLE [dbo].[TLSPN_YarnOrderLayDown]
ADD CONSTRAINT [PK_TLSPN_YarnOrderLayDown]
    PRIMARY KEY CLUSTERED ([YarnLD_Pk] ASC);
GO

-- Creating primary key on [DYEBC_Pk] in table 'TLDYE_ComDyeBatch'
ALTER TABLE [dbo].[TLDYE_ComDyeBatch]
ADD CONSTRAINT [PK_TLDYE_ComDyeBatch]
    PRIMARY KEY CLUSTERED ([DYEBC_Pk] ASC);
GO

-- Creating primary key on [TLCDD_Pk] in table 'TLDYE_ComDyeBatchDetails'
ALTER TABLE [dbo].[TLDYE_ComDyeBatchDetails]
ADD CONSTRAINT [PK_TLDYE_ComDyeBatchDetails]
    PRIMARY KEY CLUSTERED ([TLCDD_Pk] ASC);
GO

-- Creating primary key on [DYEOP_Pk] in table 'TLDYE_AllocatedOperator'
ALTER TABLE [dbo].[TLDYE_AllocatedOperator]
ADD CONSTRAINT [PK_TLDYE_AllocatedOperator]
    PRIMARY KEY CLUSTERED ([DYEOP_Pk] ASC);
GO

-- Creating primary key on [YarnTrx_Pk] in table 'TLSPN_YarnTransactions'
ALTER TABLE [dbo].[TLSPN_YarnTransactions]
ADD CONSTRAINT [PK_TLSPN_YarnTransactions]
    PRIMARY KEY CLUSTERED ([YarnTrx_Pk] ASC);
GO

-- Creating primary key on [TLDEPA_Pk] in table 'TLADM_DepartmentsAreaTransaction'
ALTER TABLE [dbo].[TLADM_DepartmentsAreaTransaction]
ADD CONSTRAINT [PK_TLADM_DepartmentsAreaTransaction]
    PRIMARY KEY CLUSTERED ([TLDEPA_Pk] ASC);
GO

-- Creating primary key on [GreigeComAJ_Pk] in table 'TLKNI_GreigeCommisionAdjustment'
ALTER TABLE [dbo].[TLKNI_GreigeCommisionAdjustment]
ADD CONSTRAINT [PK_TLKNI_GreigeCommisionAdjustment]
    PRIMARY KEY CLUSTERED ([GreigeComAJ_Pk] ASC);
GO

-- Creating primary key on [TLCUTSHB_Pk] in table 'TLCUT_CutSheetReceiptBoxes'
ALTER TABLE [dbo].[TLCUT_CutSheetReceiptBoxes]
ADD CONSTRAINT [PK_TLCUT_CutSheetReceiptBoxes]
    PRIMARY KEY CLUSTERED ([TLCUTSHB_Pk] ASC);
GO

-- Creating primary key on [TLQCFB_Pk] in table 'TLCUT_QCBerrie'
ALTER TABLE [dbo].[TLCUT_QCBerrie]
ADD CONSTRAINT [PK_TLCUT_QCBerrie]
    PRIMARY KEY CLUSTERED ([TLQCFB_Pk] ASC);
GO

-- Creating primary key on [TLCUTA_Pk] in table 'TLADM_CutMeasureArea'
ALTER TABLE [dbo].[TLADM_CutMeasureArea]
ADD CONSTRAINT [PK_TLADM_CutMeasureArea]
    PRIMARY KEY CLUSTERED ([TLCUTA_Pk] ASC);
GO

-- Creating primary key on [TLCUTFR_Pk] in table 'TLCUT_FabricReturns'
ALTER TABLE [dbo].[TLCUT_FabricReturns]
ADD CONSTRAINT [PK_TLCUT_FabricReturns]
    PRIMARY KEY CLUSTERED ([TLCUTFR_Pk] ASC);
GO

-- Creating primary key on [TLCUTTOC_Pk] in table 'TLADM_CutTrims'
ALTER TABLE [dbo].[TLADM_CutTrims]
ADD CONSTRAINT [PK_TLADM_CutTrims]
    PRIMARY KEY CLUSTERED ([TLCUTTOC_Pk] ASC);
GO

-- Creating primary key on [TLADMFC_Pk] in table 'TLADM_CutFleeceCuffs'
ALTER TABLE [dbo].[TLADM_CutFleeceCuffs]
ADD CONSTRAINT [PK_TLADM_CutFleeceCuffs]
    PRIMARY KEY CLUSTERED ([TLADMFC_Pk] ASC);
GO

-- Creating primary key on [TLADMFW_Pk] in table 'TLADM_CutFleeceWaist'
ALTER TABLE [dbo].[TLADM_CutFleeceWaist]
ADD CONSTRAINT [PK_TLADM_CutFleeceWaist]
    PRIMARY KEY CLUSTERED ([TLADMFW_Pk] ASC);
GO

-- Creating primary key on [TLCUTTOC_Pk] in table 'TLCUT_TrimsOnCut'
ALTER TABLE [dbo].[TLCUT_TrimsOnCut]
ADD CONSTRAINT [PK_TLCUT_TrimsOnCut]
    PRIMARY KEY CLUSTERED ([TLCUTTOC_Pk] ASC);
GO

-- Creating primary key on [TLFCFW_Pk] in table 'TLCUT_CutFleeceStats'
ALTER TABLE [dbo].[TLCUT_CutFleeceStats]
ADD CONSTRAINT [PK_TLCUT_CutFleeceStats]
    PRIMARY KEY CLUSTERED ([TLFCFW_Pk] ASC);
GO

-- Creating primary key on [PD_Id] in table 'TLCMT_ProductDefects'
ALTER TABLE [dbo].[TLCMT_ProductDefects]
ADD CONSTRAINT [PK_TLCMT_ProductDefects]
    PRIMARY KEY CLUSTERED ([PD_Id] ASC);
GO

-- Creating primary key on [TLCUTRJR_Pk] in table 'TLCUT_RejectReasons'
ALTER TABLE [dbo].[TLCUT_RejectReasons]
ADD CONSTRAINT [PK_TLCUT_RejectReasons]
    PRIMARY KEY CLUSTERED ([TLCUTRJR_Pk] ASC);
GO

-- Creating primary key on [TLCutSHD_Pk] in table 'TLCUT_CutSheetDetail'
ALTER TABLE [dbo].[TLCUT_CutSheetDetail]
ADD CONSTRAINT [PK_TLCUT_CutSheetDetail]
    PRIMARY KEY CLUSTERED ([TLCutSHD_Pk] ASC);
GO

-- Creating primary key on [TLADMSP_Pk] in table 'TLADM_StandardProduct'
ALTER TABLE [dbo].[TLADM_StandardProduct]
ADD CONSTRAINT [PK_TLADM_StandardProduct]
    PRIMARY KEY CLUSTERED ([TLADMSP_Pk] ASC);
GO

-- Creating primary key on [TLCMTPF_Pk] in table 'TLCMT_ProductionFaults'
ALTER TABLE [dbo].[TLCMT_ProductionFaults]
ADD CONSTRAINT [PK_TLCMT_ProductionFaults]
    PRIMARY KEY CLUSTERED ([TLCMTPF_Pk] ASC);
GO

-- Creating primary key on [TLKNISP_Pk] in table 'TLKNI_ProductionSplit'
ALTER TABLE [dbo].[TLKNI_ProductionSplit]
ADD CONSTRAINT [PK_TLKNI_ProductionSplit]
    PRIMARY KEY CLUSTERED ([TLKNISP_Pk] ASC);
GO

-- Creating primary key on [TLBTMRP_Pk] in table 'TLCMT_BodyMeasureRP'
ALTER TABLE [dbo].[TLCMT_BodyMeasureRP]
ADD CONSTRAINT [PK_TLCMT_BodyMeasureRP]
    PRIMARY KEY CLUSTERED ([TLBTMRP_Pk] ASC);
GO

-- Creating primary key on [TLCMTLF_Pk] in table 'TLCMT_LineFeederBundleCheck'
ALTER TABLE [dbo].[TLCMT_LineFeederBundleCheck]
ADD CONSTRAINT [PK_TLCMT_LineFeederBundleCheck]
    PRIMARY KEY CLUSTERED ([TLCMTLF_Pk] ASC);
GO

-- Creating primary key on [TLCSV_Pk] in table 'TLCSV_BoxSelected'
ALTER TABLE [dbo].[TLCSV_BoxSelected]
ADD CONSTRAINT [PK_TLCSV_BoxSelected]
    PRIMARY KEY CLUSTERED ([TLCSV_Pk] ASC);
GO

-- Creating primary key on [TLCMTBS_Pk] in table 'TLCSV_BoxSplit'
ALTER TABLE [dbo].[TLCSV_BoxSplit]
ADD CONSTRAINT [PK_TLCSV_BoxSplit]
    PRIMARY KEY CLUSTERED ([TLCMTBS_Pk] ASC);
GO

-- Creating primary key on [TLDYOD_Pk] in table 'TLDYE_DyeOrderDetails'
ALTER TABLE [dbo].[TLDYE_DyeOrderDetails]
ADD CONSTRAINT [PK_TLDYE_DyeOrderDetails]
    PRIMARY KEY CLUSTERED ([TLDYOD_Pk] ASC);
GO

-- Creating primary key on [TLSECDEP_Pk] in table 'TLSEC_UserSections'
ALTER TABLE [dbo].[TLSEC_UserSections]
ADD CONSTRAINT [PK_TLSEC_UserSections]
    PRIMARY KEY CLUSTERED ([TLSECDEP_Pk] ASC);
GO

-- Creating primary key on [TLDL_Pk] in table 'TLADM_DailyLog'
ALTER TABLE [dbo].[TLADM_DailyLog]
ADD CONSTRAINT [PK_TLADM_DailyLog]
    PRIMARY KEY CLUSTERED ([TLDL_Pk] ASC);
GO

-- Creating primary key on [Cotton_Pk] in table 'TLADM_Cotton'
ALTER TABLE [dbo].[TLADM_Cotton]
ADD CONSTRAINT [PK_TLADM_Cotton]
    PRIMARY KEY CLUSTERED ([Cotton_Pk] ASC);
GO

-- Creating primary key on [STYCOL_Pk] in table 'TLADM_StyleColour'
ALTER TABLE [dbo].[TLADM_StyleColour]
ADD CONSTRAINT [PK_TLADM_StyleColour]
    PRIMARY KEY CLUSTERED ([STYCOL_Pk] ASC);
GO

-- Creating primary key on [KnitYD_Pk] in table 'TLKNI_YarnTransactionDetails'
ALTER TABLE [dbo].[TLKNI_YarnTransactionDetails]
ADD CONSTRAINT [PK_TLKNI_YarnTransactionDetails]
    PRIMARY KEY CLUSTERED ([KnitYD_Pk] ASC);
GO

-- Creating primary key on [YarnOP_Pk] in table 'TLSPN_YarnOrderPallets'
ALTER TABLE [dbo].[TLSPN_YarnOrderPallets]
ADD CONSTRAINT [PK_TLSPN_YarnOrderPallets]
    PRIMARY KEY CLUSTERED ([YarnOP_Pk] ASC);
GO

-- Creating primary key on [KnitO_Pk] in table 'TLKNI_Order'
ALTER TABLE [dbo].[TLKNI_Order]
ADD CONSTRAINT [PK_TLKNI_Order]
    PRIMARY KEY CLUSTERED ([KnitO_Pk] ASC);
GO

-- Creating primary key on [TLSG_Pk] in table 'TLADM_StylesGrades'
ALTER TABLE [dbo].[TLADM_StylesGrades]
ADD CONSTRAINT [PK_TLADM_StylesGrades]
    PRIMARY KEY CLUSTERED ([TLSG_Pk] ASC);
GO

-- Creating primary key on [TLSECDT_Pk] in table 'TLSEC_Departments'
ALTER TABLE [dbo].[TLSEC_Departments]
ADD CONSTRAINT [PK_TLSEC_Departments]
    PRIMARY KEY CLUSTERED ([TLSECDT_Pk] ASC);
GO

-- Creating primary key on [ConsDC_Pk] in table 'TLADM_ConsumablesDC'
ALTER TABLE [dbo].[TLADM_ConsumablesDC]
ADD CONSTRAINT [PK_TLADM_ConsumablesDC]
    PRIMARY KEY CLUSTERED ([ConsDC_Pk] ASC);
GO

-- Creating primary key on [TLDYECD_Pk] in table 'TLDYE_RecipeColourDefinition'
ALTER TABLE [dbo].[TLDYE_RecipeColourDefinition]
ADD CONSTRAINT [PK_TLDYE_RecipeColourDefinition]
    PRIMARY KEY CLUSTERED ([TLDYECD_Pk] ASC);
GO

-- Creating primary key on [CottonCon_Pk] in table 'TLADM_CottonContracts'
ALTER TABLE [dbo].[TLADM_CottonContracts]
ADD CONSTRAINT [PK_TLADM_CottonContracts]
    PRIMARY KEY CLUSTERED ([CottonCon_Pk] ASC);
GO

-- Creating primary key on [TLCUTM_Pk] in table 'TLCUT_CutMeasureActuals'
ALTER TABLE [dbo].[TLCUT_CutMeasureActuals]
ADD CONSTRAINT [PK_TLCUT_CutMeasureActuals]
    PRIMARY KEY CLUSTERED ([TLCUTM_Pk] ASC);
GO

-- Creating primary key on [TLCUTAL_Pk] in table 'TLADM_CutAreaLocations'
ALTER TABLE [dbo].[TLADM_CutAreaLocations]
ADD CONSTRAINT [PK_TLADM_CutAreaLocations]
    PRIMARY KEY CLUSTERED ([TLCUTAL_Pk] ASC);
GO

-- Creating primary key on [TLCUTAS_Pk] in table 'TLADM_CutMeasureStandards'
ALTER TABLE [dbo].[TLADM_CutMeasureStandards]
ADD CONSTRAINT [PK_TLADM_CutMeasureStandards]
    PRIMARY KEY CLUSTERED ([TLCUTAS_Pk] ASC);
GO

-- Creating primary key on [TLCUTQA_Pk] in table 'TLCUT_QAResults'
ALTER TABLE [dbo].[TLCUT_QAResults]
ADD CONSTRAINT [PK_TLCUT_QAResults]
    PRIMARY KEY CLUSTERED ([TLCUTQA_Pk] ASC);
GO

-- Creating primary key on [TLMerge_PK] in table 'TLCSV_MergePODetail'
ALTER TABLE [dbo].[TLCSV_MergePODetail]
ADD CONSTRAINT [PK_TLCSV_MergePODetail]
    PRIMARY KEY CLUSTERED ([TLMerge_PK] ASC);
GO

-- Creating primary key on [TLWA_Pk] in table 'TLADM_WareHouseAssociation'
ALTER TABLE [dbo].[TLADM_WareHouseAssociation]
ADD CONSTRAINT [PK_TLADM_WareHouseAssociation]
    PRIMARY KEY CLUSTERED ([TLWA_Pk] ASC);
GO

-- Creating primary key on [TLGQ_Pk] in table 'TLDYE_ReceipeGreigeQual'
ALTER TABLE [dbo].[TLDYE_ReceipeGreigeQual]
ADD CONSTRAINT [PK_TLDYE_ReceipeGreigeQual]
    PRIMARY KEY CLUSTERED ([TLGQ_Pk] ASC);
GO

-- Creating primary key on [TLDYE_DefinePk] in table 'TLDYE_RecipeDefinition'
ALTER TABLE [dbo].[TLDYE_RecipeDefinition]
ADD CONSTRAINT [PK_TLDYE_RecipeDefinition]
    PRIMARY KEY CLUSTERED ([TLDYE_DefinePk] ASC);
GO

-- Creating primary key on [TLCUTSHRD_Pk] in table 'TLCUT_CutSheetReceiptDetail'
ALTER TABLE [dbo].[TLCUT_CutSheetReceiptDetail]
ADD CONSTRAINT [PK_TLCUT_CutSheetReceiptDetail]
    PRIMARY KEY CLUSTERED ([TLCUTSHRD_Pk] ASC);
GO

-- Creating primary key on [StyTrim_Pk] in table 'TLADM_StyleTrim'
ALTER TABLE [dbo].[TLADM_StyleTrim]
ADD CONSTRAINT [PK_TLADM_StyleTrim]
    PRIMARY KEY CLUSTERED ([StyTrim_Pk] ASC);
GO

-- Creating primary key on [GreigeCom_PK] in table 'TLKNI_GreigeCommissionTransctions'
ALTER TABLE [dbo].[TLKNI_GreigeCommissionTransctions]
ADD CONSTRAINT [PK_TLKNI_GreigeCommissionTransctions]
    PRIMARY KEY CLUSTERED ([GreigeCom_PK] ASC);
GO

-- Creating primary key on [TLMDD_Pk] in table 'TLKNI_MachineLastNumber'
ALTER TABLE [dbo].[TLKNI_MachineLastNumber]
ADD CONSTRAINT [PK_TLKNI_MachineLastNumber]
    PRIMARY KEY CLUSTERED ([TLMDD_Pk] ASC);
GO

-- Creating primary key on [LUN_Pk] in table 'TLADM_LastNumberUsed'
ALTER TABLE [dbo].[TLADM_LastNumberUsed]
ADD CONSTRAINT [PK_TLADM_LastNumberUsed]
    PRIMARY KEY CLUSTERED ([LUN_Pk] ASC);
GO

-- Creating primary key on [TLKYT_Pk] in table 'TLKNI_YarnAllocTransctions'
ALTER TABLE [dbo].[TLKNI_YarnAllocTransctions]
ADD CONSTRAINT [PK_TLKNI_YarnAllocTransctions]
    PRIMARY KEY CLUSTERED ([TLKYT_Pk] ASC);
GO

-- Creating primary key on [CMTS_Pk] in table 'TLCMT_Statistics'
ALTER TABLE [dbo].[TLCMT_Statistics]
ADD CONSTRAINT [PK_TLCMT_Statistics]
    PRIMARY KEY CLUSTERED ([CMTS_Pk] ASC);
GO

-- Creating primary key on [CMTPI_Pk] in table 'TLCMT_PanelIssue'
ALTER TABLE [dbo].[TLCMT_PanelIssue]
ADD CONSTRAINT [PK_TLCMT_PanelIssue]
    PRIMARY KEY CLUSTERED ([CMTPI_Pk] ASC);
GO

-- Creating primary key on [TLCMTDF_Pk] in table 'TLCMT_DeflectFlaw'
ALTER TABLE [dbo].[TLCMT_DeflectFlaw]
ADD CONSTRAINT [PK_TLCMT_DeflectFlaw]
    PRIMARY KEY CLUSTERED ([TLCMTDF_Pk] ASC);
GO

-- Creating primary key on [AuxCol_Id] in table 'TLADM_AuxColours'
ALTER TABLE [dbo].[TLADM_AuxColours]
ADD CONSTRAINT [PK_TLADM_AuxColours]
    PRIMARY KEY CLUSTERED ([AuxCol_Id] ASC);
GO

-- Creating primary key on [CMTNC_Pk] in table 'TLADM_CMTNonCompliance'
ALTER TABLE [dbo].[TLADM_CMTNonCompliance]
ADD CONSTRAINT [PK_TLADM_CMTNonCompliance]
    PRIMARY KEY CLUSTERED ([CMTNC_Pk] ASC);
GO

-- Creating primary key on [TLADMBT_Pk] in table 'TLADM_BoxTypes'
ALTER TABLE [dbo].[TLADM_BoxTypes]
ADD CONSTRAINT [PK_TLADM_BoxTypes]
    PRIMARY KEY CLUSTERED ([TLADMBT_Pk] ASC);
GO

-- Creating primary key on [TLCSVWHTD_Pk] in table 'TLCSV_WhseTransferDetail'
ALTER TABLE [dbo].[TLCSV_WhseTransferDetail]
ADD CONSTRAINT [PK_TLCSV_WhseTransferDetail]
    PRIMARY KEY CLUSTERED ([TLCSVWHTD_Pk] ASC);
GO

-- Creating primary key on [TLCSVWHT_Pk] in table 'TLCSV_WhseTransfer'
ALTER TABLE [dbo].[TLCSV_WhseTransfer]
ADD CONSTRAINT [PK_TLCSV_WhseTransfer]
    PRIMARY KEY CLUSTERED ([TLCSVWHT_Pk] ASC);
GO

-- Creating primary key on [CustAcc_Pk] in table 'TLADM_CustomerAccess'
ALTER TABLE [dbo].[TLADM_CustomerAccess]
ADD CONSTRAINT [PK_TLADM_CustomerAccess]
    PRIMARY KEY CLUSTERED ([CustAcc_Pk] ASC);
GO

-- Creating primary key on [TLBFAR_Pk] in table 'TLCMT_AuditMeasureRecorded'
ALTER TABLE [dbo].[TLCMT_AuditMeasureRecorded]
ADD CONSTRAINT [PK_TLCMT_AuditMeasureRecorded]
    PRIMARY KEY CLUSTERED ([TLBFAR_Pk] ASC);
GO

-- Creating primary key on [CMTPID_Pk] in table 'TLCMT_PanelIssueDetail'
ALTER TABLE [dbo].[TLCMT_PanelIssueDetail]
ADD CONSTRAINT [PK_TLCMT_PanelIssueDetail]
    PRIMARY KEY CLUSTERED ([CMTPID_Pk] ASC);
GO

-- Creating primary key on [TLBIN_Pk] in table 'TLKNI_BoughtInFabric'
ALTER TABLE [dbo].[TLKNI_BoughtInFabric]
ADD CONSTRAINT [PK_TLKNI_BoughtInFabric]
    PRIMARY KEY CLUSTERED ([TLBIN_Pk] ASC);
GO

-- Creating primary key on [BIFD_Pk] in table 'TLDYE_BIFInTransitDetails'
ALTER TABLE [dbo].[TLDYE_BIFInTransitDetails]
ADD CONSTRAINT [PK_TLDYE_BIFInTransitDetails]
    PRIMARY KEY CLUSTERED ([BIFD_Pk] ASC);
GO

-- Creating primary key on [BIFT_Pk] in table 'TLDYE_BIFInTransit'
ALTER TABLE [dbo].[TLDYE_BIFInTransit]
ADD CONSTRAINT [PK_TLDYE_BIFInTransit]
    PRIMARY KEY CLUSTERED ([BIFT_Pk] ASC);
GO

-- Creating primary key on [CMTNCD_Pk] in table 'TLCMT_NonCompliance'
ALTER TABLE [dbo].[TLCMT_NonCompliance]
ADD CONSTRAINT [PK_TLCMT_NonCompliance]
    PRIMARY KEY CLUSTERED ([CMTNCD_Pk] ASC);
GO

-- Creating primary key on [TLMV_Pk] in table 'TLCSV_Movement'
ALTER TABLE [dbo].[TLCSV_Movement]
ADD CONSTRAINT [PK_TLCSV_Movement]
    PRIMARY KEY CLUSTERED ([TLMV_Pk] ASC);
GO

-- Creating primary key on [TLBPS_Pk] in table 'TLADM_BoxType_Packing_Specifications'
ALTER TABLE [dbo].[TLADM_BoxType_Packing_Specifications]
ADD CONSTRAINT [PK_TLADM_BoxType_Packing_Specifications]
    PRIMARY KEY CLUSTERED ([TLBPS_Pk] ASC);
GO

-- Creating primary key on [BxCon_Pk] in table 'TLCSV_BoxConfiguration'
ALTER TABLE [dbo].[TLCSV_BoxConfiguration]
ADD CONSTRAINT [PK_TLCSV_BoxConfiguration]
    PRIMARY KEY CLUSTERED ([BxCon_Pk] ASC);
GO

-- Creating primary key on [PORConfig_Pk] in table 'TLCSV_RePackConfig'
ALTER TABLE [dbo].[TLCSV_RePackConfig]
ADD CONSTRAINT [PK_TLCSV_RePackConfig]
    PRIMARY KEY CLUSTERED ([PORConfig_Pk] ASC);
GO

-- Creating primary key on [REPACT_Pk] in table 'TLCSV_RePackTransactions'
ALTER TABLE [dbo].[TLCSV_RePackTransactions]
ADD CONSTRAINT [PK_TLCSV_RePackTransactions]
    PRIMARY KEY CLUSTERED ([REPACT_Pk] ASC);
GO

-- Creating primary key on [TLPDL_Pk] in table 'TLPPS_ProductionLeadTime'
ALTER TABLE [dbo].[TLPPS_ProductionLeadTime]
ADD CONSTRAINT [PK_TLPPS_ProductionLeadTime]
    PRIMARY KEY CLUSTERED ([TLPDL_Pk] ASC);
GO

-- Creating primary key on [TLUPT_Pk] in table 'TLCMT_UnitProductionTargets'
ALTER TABLE [dbo].[TLCMT_UnitProductionTargets]
ADD CONSTRAINT [PK_TLCMT_UnitProductionTargets]
    PRIMARY KEY CLUSTERED ([TLUPT_Pk] ASC);
GO

-- Creating primary key on [TLSECSect_Pk] in table 'TLSEC_Sections'
ALTER TABLE [dbo].[TLSEC_Sections]
ADD CONSTRAINT [PK_TLSEC_Sections]
    PRIMARY KEY CLUSTERED ([TLSECSect_Pk] ASC);
GO

-- Creating primary key on [TLKNIOP_Pk] in table 'TLKNI_YarnOrderPallets'
ALTER TABLE [dbo].[TLKNI_YarnOrderPallets]
ADD CONSTRAINT [PK_TLKNI_YarnOrderPallets]
    PRIMARY KEY CLUSTERED ([TLKNIOP_Pk] ASC);
GO

-- Creating primary key on [TLCMTCFG_Pk] in table 'TLCMT_FactConfig'
ALTER TABLE [dbo].[TLCMT_FactConfig]
ADD CONSTRAINT [PK_TLCMT_FactConfig]
    PRIMARY KEY CLUSTERED ([TLCMTCFG_Pk] ASC);
GO

-- Creating primary key on [TLREP_Pk] in table 'TLPPS_Replenishment'
ALTER TABLE [dbo].[TLPPS_Replenishment]
ADD CONSTRAINT [PK_TLPPS_Replenishment]
    PRIMARY KEY CLUSTERED ([TLREP_Pk] ASC);
GO

-- Creating primary key on [TLPL_Pk] in table 'TLCSV_PickingListMaster'
ALTER TABLE [dbo].[TLCSV_PickingListMaster]
ADD CONSTRAINT [PK_TLCSV_PickingListMaster]
    PRIMARY KEY CLUSTERED ([TLPL_Pk] ASC);
GO

-- Creating primary key on [TLDyeIns_Pk] in table 'TLDye_QualityException'
ALTER TABLE [dbo].[TLDye_QualityException]
ADD CONSTRAINT [PK_TLDye_QualityException]
    PRIMARY KEY CLUSTERED ([TLDyeIns_Pk] ASC);
GO

-- Creating primary key on [TLSECUA_Pk] in table 'TLSEC_UserAccess'
ALTER TABLE [dbo].[TLSEC_UserAccess]
ADD CONSTRAINT [PK_TLSEC_UserAccess]
    PRIMARY KEY CLUSTERED ([TLSECUA_Pk] ASC);
GO

-- Creating primary key on [TLORDA_Pk] in table 'TLCSV_OrderAllocated'
ALTER TABLE [dbo].[TLCSV_OrderAllocated]
ADD CONSTRAINT [PK_TLCSV_OrderAllocated]
    PRIMARY KEY CLUSTERED ([TLORDA_Pk] ASC);
GO

-- Creating primary key on [TLTRNS_Pk] in table 'TLADM_Transporters'
ALTER TABLE [dbo].[TLADM_Transporters]
ADD CONSTRAINT [PK_TLADM_Transporters]
    PRIMARY KEY CLUSTERED ([TLTRNS_Pk] ASC);
GO

-- Creating primary key on [CMTP_Pk] in table 'TLCMT_ProductionCosts'
ALTER TABLE [dbo].[TLCMT_ProductionCosts]
ADD CONSTRAINT [PK_TLCMT_ProductionCosts]
    PRIMARY KEY CLUSTERED ([CMTP_Pk] ASC);
GO

-- Creating primary key on [CMTMP_Pk] in table 'TLADM_CMTMeasurementPoints'
ALTER TABLE [dbo].[TLADM_CMTMeasurementPoints]
ADD CONSTRAINT [PK_TLADM_CMTMeasurementPoints]
    PRIMARY KEY CLUSTERED ([CMTMP_Pk] ASC);
GO

-- Creating primary key on [FW_Id] in table 'TLADM_FabWidth'
ALTER TABLE [dbo].[TLADM_FabWidth]
ADD CONSTRAINT [PK_TLADM_FabWidth]
    PRIMARY KEY CLUSTERED ([FW_Id] ASC);
GO

-- Creating primary key on [QD_Pk] in table 'TLADM_QualityDefinition'
ALTER TABLE [dbo].[TLADM_QualityDefinition]
ADD CONSTRAINT [PK_TLADM_QualityDefinition]
    PRIMARY KEY CLUSTERED ([QD_Pk] ASC);
GO

-- Creating primary key on [TLCMTLI_Pk] in table 'TLCMT_LineIssue'
ALTER TABLE [dbo].[TLCMT_LineIssue]
ADD CONSTRAINT [PK_TLCMT_LineIssue]
    PRIMARY KEY CLUSTERED ([TLCMTLI_Pk] ASC);
GO

-- Creating primary key on [CottonOrigin_Pk] in table 'TLADM_CottonOrigin'
ALTER TABLE [dbo].[TLADM_CottonOrigin]
ADD CONSTRAINT [PK_TLADM_CottonOrigin]
    PRIMARY KEY CLUSTERED ([CottonOrigin_Pk] ASC);
GO

-- Creating primary key on [DYEB_Pk] in table 'TLDYE_DyeBatch'
ALTER TABLE [dbo].[TLDYE_DyeBatch]
ADD CONSTRAINT [PK_TLDYE_DyeBatch]
    PRIMARY KEY CLUSTERED ([DYEB_Pk] ASC);
GO

-- Creating primary key on [TLCMTWC_Pk] in table 'TLCMT_CompletedWork'
ALTER TABLE [dbo].[TLCMT_CompletedWork]
ADD CONSTRAINT [PK_TLCMT_CompletedWork]
    PRIMARY KEY CLUSTERED ([TLCMTWC_Pk] ASC);
GO

-- Creating primary key on [Grcl_PK] in table 'TLADM_GreigeColour'
ALTER TABLE [dbo].[TLADM_GreigeColour]
ADD CONSTRAINT [PK_TLADM_GreigeColour]
    PRIMARY KEY CLUSTERED ([Grcl_PK] ASC);
GO

-- Creating primary key on [TLCUTSHR_Pk] in table 'TLCUT_CutSheetReceipt'
ALTER TABLE [dbo].[TLCUT_CutSheetReceipt]
ADD CONSTRAINT [PK_TLCUT_CutSheetReceipt]
    PRIMARY KEY CLUSTERED ([TLCUTSHR_Pk] ASC);
GO

-- Creating primary key on [TLCUTE_Pk] in table 'TLCUT_ExpectedUnits'
ALTER TABLE [dbo].[TLCUT_ExpectedUnits]
ADD CONSTRAINT [PK_TLCUT_ExpectedUnits]
    PRIMARY KEY CLUSTERED ([TLCUTE_Pk] ASC);
GO

-- Creating primary key on [YarnProduction_PK] in table 'TLSPN_YarnProductionPerMachine'
ALTER TABLE [dbo].[TLSPN_YarnProductionPerMachine]
ADD CONSTRAINT [PK_TLSPN_YarnProductionPerMachine]
    PRIMARY KEY CLUSTERED ([YarnProduction_PK] ASC);
GO

-- Creating primary key on [Pr_Id] in table 'TLADM_ProductRating'
ALTER TABLE [dbo].[TLADM_ProductRating]
ADD CONSTRAINT [PK_TLADM_ProductRating]
    PRIMARY KEY CLUSTERED ([Pr_Id] ASC);
GO

-- Creating primary key on [CMTBFA_Pk] in table 'TLCMT_AuditMeasurements'
ALTER TABLE [dbo].[TLCMT_AuditMeasurements]
ADD CONSTRAINT [PK_TLCMT_AuditMeasurements]
    PRIMARY KEY CLUSTERED ([CMTBFA_Pk] ASC);
GO

-- Creating primary key on [BoxHist_Pk] in table 'TLCMT_HistoryBoxedQty'
ALTER TABLE [dbo].[TLCMT_HistoryBoxedQty]
ADD CONSTRAINT [PK_TLCMT_HistoryBoxedQty]
    PRIMARY KEY CLUSTERED ([BoxHist_Pk] ASC);
GO

-- Creating primary key on [TR_Id] in table 'TLADM_Trims'
ALTER TABLE [dbo].[TLADM_Trims]
ADD CONSTRAINT [PK_TLADM_Trims]
    PRIMARY KEY CLUSTERED ([TR_Id] ASC);
GO

-- Creating primary key on [TLCutSH_Pk] in table 'TLCUT_CutSheet'
ALTER TABLE [dbo].[TLCUT_CutSheet]
ADD CONSTRAINT [PK_TLCUT_CutSheet]
    PRIMARY KEY CLUSTERED ([TLCutSH_Pk] ASC);
GO

-- Creating primary key on [TLInter_Pk] in table 'TLPPS_InterDept'
ALTER TABLE [dbo].[TLPPS_InterDept]
ADD CONSTRAINT [PK_TLPPS_InterDept]
    PRIMARY KEY CLUSTERED ([TLInter_Pk] ASC);
GO

-- Creating primary key on [MD_Pk] in table 'TLADM_MachineDefinitions'
ALTER TABLE [dbo].[TLADM_MachineDefinitions]
ADD CONSTRAINT [PK_TLADM_MachineDefinitions]
    PRIMARY KEY CLUSTERED ([MD_Pk] ASC);
GO

-- Creating primary key on [TLDYE_NcrPk] in table 'TLDYE_NonCompliance'
ALTER TABLE [dbo].[TLDYE_NonCompliance]
ADD CONSTRAINT [PK_TLDYE_NonCompliance]
    PRIMARY KEY CLUSTERED ([TLDYE_NcrPk] ASC);
GO

-- Creating primary key on [DyeStan_Pk] in table 'TLDYE_DyeingStandards'
ALTER TABLE [dbo].[TLDYE_DyeingStandards]
ADD CONSTRAINT [PK_TLDYE_DyeingStandards]
    PRIMARY KEY CLUSTERED ([DyeStan_Pk] ASC);
GO

-- Creating primary key on [TLQADPF_Pk] in table 'TLADM_QADyeProcessFields'
ALTER TABLE [dbo].[TLADM_QADyeProcessFields]
ADD CONSTRAINT [PK_TLADM_QADyeProcessFields]
    PRIMARY KEY CLUSTERED ([TLQADPF_Pk] ASC);
GO

-- Creating primary key on [DYECON_Pk] in table 'TLDYE_ConSummableReceived'
ALTER TABLE [dbo].[TLDYE_ConSummableReceived]
ADD CONSTRAINT [PK_TLDYE_ConSummableReceived]
    PRIMARY KEY CLUSTERED ([DYECON_Pk] ASC);
GO

-- Creating primary key on [TLMtask_Pk] in table 'TLADM_MachineMaintenanceTasks'
ALTER TABLE [dbo].[TLADM_MachineMaintenanceTasks]
ADD CONSTRAINT [PK_TLADM_MachineMaintenanceTasks]
    PRIMARY KEY CLUSTERED ([TLMtask_Pk] ASC);
GO

-- Creating primary key on [mman_Pk] in table 'TLADM_MachineMaintenance'
ALTER TABLE [dbo].[TLADM_MachineMaintenance]
ADD CONSTRAINT [PK_TLADM_MachineMaintenance]
    PRIMARY KEY CLUSTERED ([mman_Pk] ASC);
GO

-- Creating primary key on [Dep_Id] in table 'TLADM_Departments'
ALTER TABLE [dbo].[TLADM_Departments]
ADD CONSTRAINT [PK_TLADM_Departments]
    PRIMARY KEY CLUSTERED ([Dep_Id] ASC);
GO

-- Creating primary key on [WhStore_Id] in table 'TLADM_WhseStore'
ALTER TABLE [dbo].[TLADM_WhseStore]
ADD CONSTRAINT [PK_TLADM_WhseStore]
    PRIMARY KEY CLUSTERED ([WhStore_Id] ASC);
GO

-- Creating primary key on [TLBIG_Pk] in table 'TLCSV_BoughtInGoods'
ALTER TABLE [dbo].[TLCSV_BoughtInGoods]
ADD CONSTRAINT [PK_TLCSV_BoughtInGoods]
    PRIMARY KEY CLUSTERED ([TLBIG_Pk] ASC);
GO

-- Creating primary key on [TLSOH_Pk] in table 'TLCSV_StockOnHand'
ALTER TABLE [dbo].[TLCSV_StockOnHand]
ADD CONSTRAINT [PK_TLCSV_StockOnHand]
    PRIMARY KEY CLUSTERED ([TLSOH_Pk] ASC);
GO

-- Creating primary key on [SI_id] in table 'TLADM_Sizes'
ALTER TABLE [dbo].[TLADM_Sizes]
ADD CONSTRAINT [PK_TLADM_Sizes]
    PRIMARY KEY CLUSTERED ([SI_id] ASC);
GO

-- Creating primary key on [TLCUSTO_Pk] in table 'TLCSV_PuchaseOrderDetail'
ALTER TABLE [dbo].[TLCSV_PuchaseOrderDetail]
ADD CONSTRAINT [PK_TLCSV_PuchaseOrderDetail]
    PRIMARY KEY CLUSTERED ([TLCUSTO_Pk] ASC);
GO

-- Creating primary key on [Mth_Pk] in table 'TLADM_Months'
ALTER TABLE [dbo].[TLADM_Months]
ADD CONSTRAINT [PK_TLADM_Months]
    PRIMARY KEY CLUSTERED ([Mth_Pk] ASC);
GO

-- Creating primary key on [TLCSVPO_Pk] in table 'TLCSV_PurchaseOrder'
ALTER TABLE [dbo].[TLCSV_PurchaseOrder]
ADD CONSTRAINT [PK_TLCSV_PurchaseOrder]
    PRIMARY KEY CLUSTERED ([TLCSVPO_Pk] ASC);
GO

-- Creating primary key on [TLGreige_Id] in table 'TLADM_Griege'
ALTER TABLE [dbo].[TLADM_Griege]
ADD CONSTRAINT [PK_TLADM_Griege]
    PRIMARY KEY CLUSTERED ([TLGreige_Id] ASC);
GO

-- Creating primary key on [TLDYO_Pk] in table 'TLDYE_DyeOrder'
ALTER TABLE [dbo].[TLDYE_DyeOrder]
ADD CONSTRAINT [PK_TLDYE_DyeOrder]
    PRIMARY KEY CLUSTERED ([TLDYO_Pk] ASC);
GO

-- Creating primary key on [TLDYEF_Pk] in table 'TLDYE_DyeOrderFabric'
ALTER TABLE [dbo].[TLDYE_DyeOrderFabric]
ADD CONSTRAINT [PK_TLDYE_DyeOrderFabric]
    PRIMARY KEY CLUSTERED ([TLDYEF_Pk] ASC);
GO

-- Creating primary key on [TLDYET_Pk] in table 'TLDYE_DyeTransactions'
ALTER TABLE [dbo].[TLDYE_DyeTransactions]
ADD CONSTRAINT [PK_TLDYE_DyeTransactions]
    PRIMARY KEY CLUSTERED ([TLDYET_Pk] ASC);
GO

-- Creating primary key on [DYENCCON_Pk] in table 'TLDYE_NonComplianceConsDetail'
ALTER TABLE [dbo].[TLDYE_NonComplianceConsDetail]
ADD CONSTRAINT [PK_TLDYE_NonComplianceConsDetail]
    PRIMARY KEY CLUSTERED ([DYENCCON_Pk] ASC);
GO

-- Creating primary key on [TLDYEDC_Pk] in table 'TLDYE_NonComplianceAnalysis'
ALTER TABLE [dbo].[TLDYE_NonComplianceAnalysis]
ADD CONSTRAINT [PK_TLDYE_NonComplianceAnalysis]
    PRIMARY KEY CLUSTERED ([TLDYEDC_Pk] ASC);
GO

-- Creating primary key on [Pan_PK] in table 'TLADM_PanelAttributes'
ALTER TABLE [dbo].[TLADM_PanelAttributes]
ADD CONSTRAINT [PK_TLADM_PanelAttributes]
    PRIMARY KEY CLUSTERED ([Pan_PK] ASC);
GO

-- Creating primary key on [DYCSH_Pk] in table 'TLDYE_ConsumableSOH'
ALTER TABLE [dbo].[TLDYE_ConsumableSOH]
ADD CONSTRAINT [PK_TLDYE_ConsumableSOH]
    PRIMARY KEY CLUSTERED ([DYCSH_Pk] ASC);
GO

-- Creating primary key on [Col_Id] in table 'TLADM_Colours'
ALTER TABLE [dbo].[TLADM_Colours]
ADD CONSTRAINT [PK_TLADM_Colours]
    PRIMARY KEY CLUSTERED ([Col_Id] ASC);
GO

-- Creating primary key on [GreigeP_Pk] in table 'TLKNI_GreigeProduction'
ALTER TABLE [dbo].[TLKNI_GreigeProduction]
ADD CONSTRAINT [PK_TLKNI_GreigeProduction]
    PRIMARY KEY CLUSTERED ([GreigeP_Pk] ASC);
GO

-- Creating primary key on [GQ_Pk] in table 'TLADM_GreigeQuality'
ALTER TABLE [dbo].[TLADM_GreigeQuality]
ADD CONSTRAINT [PK_TLADM_GreigeQuality]
    PRIMARY KEY CLUSTERED ([GQ_Pk] ASC);
GO

-- Creating primary key on [StyAssoc_Pk] in table 'TLADM_StyAssoc'
ALTER TABLE [dbo].[TLADM_StyAssoc]
ADD CONSTRAINT [PK_TLADM_StyAssoc]
    PRIMARY KEY CLUSTERED ([StyAssoc_Pk] ASC);
GO

-- Creating primary key on [DyeRFD_Pk] in table 'TLDYE_RFDHistory'
ALTER TABLE [dbo].[TLDYE_RFDHistory]
ADD CONSTRAINT [PK_TLDYE_RFDHistory]
    PRIMARY KEY CLUSTERED ([DyeRFD_Pk] ASC);
GO

-- Creating primary key on [Cust_Pk] in table 'TLADM_CustomerFile'
ALTER TABLE [dbo].[TLADM_CustomerFile]
ADD CONSTRAINT [PK_TLADM_CustomerFile]
    PRIMARY KEY CLUSTERED ([Cust_Pk] ASC);
GO

-- Creating primary key on [diagram_id] in table 'sysdiagrams'
ALTER TABLE [dbo].[sysdiagrams]
ADD CONSTRAINT [PK_sysdiagrams]
    PRIMARY KEY CLUSTERED ([diagram_id] ASC);
GO

-- Creating primary key on [TLQual_Yarn_Id] in table 'TLADM_Greige_Yarn'
ALTER TABLE [dbo].[TLADM_Greige_Yarn]
ADD CONSTRAINT [PK_TLADM_Greige_Yarn]
    PRIMARY KEY CLUSTERED ([TLQual_Yarn_Id] ASC);
GO

-- Creating primary key on [SQual_Pk] in table 'TLADM_Style_Quality'
ALTER TABLE [dbo].[TLADM_Style_Quality]
ADD CONSTRAINT [PK_TLADM_Style_Quality]
    PRIMARY KEY CLUSTERED ([SQual_Pk] ASC);
GO

-- Creating primary key on [Sty_Id] in table 'TLADM_Styles'
ALTER TABLE [dbo].[TLADM_Styles]
ADD CONSTRAINT [PK_TLADM_Styles]
    PRIMARY KEY CLUSTERED ([Sty_Id] ASC);
GO

-- Creating primary key on [Shft_Pk] in table 'TLADM_Shifts'
ALTER TABLE [dbo].[TLADM_Shifts]
ADD CONSTRAINT [PK_TLADM_Shifts]
    PRIMARY KEY CLUSTERED ([Shft_Pk] ASC);
GO

-- Creating primary key on [DeptA_Pk] in table 'TLADM_DepartmentsArea'
ALTER TABLE [dbo].[TLADM_DepartmentsArea]
ADD CONSTRAINT [PK_TLADM_DepartmentsArea]
    PRIMARY KEY CLUSTERED ([DeptA_Pk] ASC);
GO

-- Creating primary key on [DYEBD_Pk] in table 'TLDYE_DyeBatchDetails'
ALTER TABLE [dbo].[TLDYE_DyeBatchDetails]
ADD CONSTRAINT [PK_TLDYE_DyeBatchDetails]
    PRIMARY KEY CLUSTERED ([DYEBD_Pk] ASC);
GO

-- Creating primary key on [Id] in table 'TLDYE_GarmentDyeingProduction'
ALTER TABLE [dbo].[TLDYE_GarmentDyeingProduction]
ADD CONSTRAINT [PK_TLDYE_GarmentDyeingProduction]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'TLADM_ProductCodes'
ALTER TABLE [dbo].[TLADM_ProductCodes]
ADD CONSTRAINT [PK_TLADM_ProductCodes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [ConsOther_ConsGroup_FK] in table 'TLADM_ConsumablesOther'
ALTER TABLE [dbo].[TLADM_ConsumablesOther]
ADD CONSTRAINT [FK_TLADM_ConsumablesOther_TLADM_ConsumableGroups]
    FOREIGN KEY ([ConsOther_ConsGroup_FK])
    REFERENCES [dbo].[TLADM_ConsumableGroups]
        ([ConG_Pk])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TLADM_ConsumablesOther_TLADM_ConsumableGroups'
CREATE INDEX [IX_FK_TLADM_ConsumablesOther_TLADM_ConsumableGroups]
ON [dbo].[TLADM_ConsumablesOther]
    ([ConsOther_ConsGroup_FK]);
GO

-- Creating foreign key on [ConsOther_Store_FK] in table 'TLADM_ConsumablesOther'
ALTER TABLE [dbo].[TLADM_ConsumablesOther]
ADD CONSTRAINT [FK_TLADM_ConsumablesOther_TLADM_StoreTypes]
    FOREIGN KEY ([ConsOther_Store_FK])
    REFERENCES [dbo].[TLADM_StoreTypes]
        ([StoreT_Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TLADM_ConsumablesOther_TLADM_StoreTypes'
CREATE INDEX [IX_FK_TLADM_ConsumablesOther_TLADM_StoreTypes]
ON [dbo].[TLADM_ConsumablesOther]
    ([ConsOther_Store_FK]);
GO

-- Creating foreign key on [ConsOther_UOM_FK] in table 'TLADM_ConsumablesOther'
ALTER TABLE [dbo].[TLADM_ConsumablesOther]
ADD CONSTRAINT [FK_TLADM_ConsumablesOther_TLADM_UOM]
    FOREIGN KEY ([ConsOther_UOM_FK])
    REFERENCES [dbo].[TLADM_UOM]
        ([UOM_Pk])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TLADM_ConsumablesOther_TLADM_UOM'
CREATE INDEX [IX_FK_TLADM_ConsumablesOther_TLADM_UOM]
ON [dbo].[TLADM_ConsumablesOther]
    ([ConsOther_UOM_FK]);
GO

-- Creating foreign key on [ConsOther_AUOM_FK] in table 'TLADM_ConsumablesOther'
ALTER TABLE [dbo].[TLADM_ConsumablesOther]
ADD CONSTRAINT [FK_TLADM_ConsumablesOther_TLADM_UOM1]
    FOREIGN KEY ([ConsOther_AUOM_FK])
    REFERENCES [dbo].[TLADM_UOM]
        ([UOM_Pk])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TLADM_ConsumablesOther_TLADM_UOM1'
CREATE INDEX [IX_FK_TLADM_ConsumablesOther_TLADM_UOM1]
ON [dbo].[TLADM_ConsumablesOther]
    ([ConsOther_AUOM_FK]);
GO

-- Creating foreign key on [NSI_Category_FK] in table 'TLADM_NonStockItems'
ALTER TABLE [dbo].[TLADM_NonStockItems]
ADD CONSTRAINT [FK_TLADM_NonStockItems_TLADM_NonStockCat]
    FOREIGN KEY ([NSI_Category_FK])
    REFERENCES [dbo].[TLADM_NonStockCat]
        ([NSC_Pk])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TLADM_NonStockItems_TLADM_NonStockCat'
CREATE INDEX [IX_FK_TLADM_NonStockItems_TLADM_NonStockCat]
ON [dbo].[TLADM_NonStockItems]
    ([NSI_Category_FK]);
GO

-- Creating foreign key on [NSI_StockType_FK] in table 'TLADM_NonStockItems'
ALTER TABLE [dbo].[TLADM_NonStockItems]
ADD CONSTRAINT [FK_TLADM_NonStockItems_TLADM_StockTypes]
    FOREIGN KEY ([NSI_StockType_FK])
    REFERENCES [dbo].[TLADM_StockTypes]
        ([ST_Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TLADM_NonStockItems_TLADM_StockTypes'
CREATE INDEX [IX_FK_TLADM_NonStockItems_TLADM_StockTypes]
ON [dbo].[TLADM_NonStockItems]
    ([NSI_StockType_FK]);
GO

-- Creating foreign key on [NSI_UOM_FK] in table 'TLADM_NonStockItems'
ALTER TABLE [dbo].[TLADM_NonStockItems]
ADD CONSTRAINT [FK_TLADM_NonStockItems_TLADM_UOM]
    FOREIGN KEY ([NSI_UOM_FK])
    REFERENCES [dbo].[TLADM_UOM]
        ([UOM_Pk])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TLADM_NonStockItems_TLADM_UOM'
CREATE INDEX [IX_FK_TLADM_NonStockItems_TLADM_UOM]
ON [dbo].[TLADM_NonStockItems]
    ([NSI_UOM_FK]);
GO

-- Creating foreign key on [Sup_ProductTypes_FK] in table 'TLADM_Suppliers'
ALTER TABLE [dbo].[TLADM_Suppliers]
ADD CONSTRAINT [FK_TLADM_Suppliers_TLADM_ProductTypes]
    FOREIGN KEY ([Sup_ProductTypes_FK])
    REFERENCES [dbo].[TLADM_ProductTypes]
        ([PT_pk])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TLADM_Suppliers_TLADM_ProductTypes'
CREATE INDEX [IX_FK_TLADM_Suppliers_TLADM_ProductTypes]
ON [dbo].[TLADM_Suppliers]
    ([Sup_ProductTypes_FK]);
GO

-- Creating foreign key on [FT_Product_FK] in table 'TLADM_FabricType'
ALTER TABLE [dbo].[TLADM_FabricType]
ADD CONSTRAINT [FK_TLADM_FabricType_TLADM_FabricProduct]
    FOREIGN KEY ([FT_Product_FK])
    REFERENCES [dbo].[TLADM_FabricProduct]
        ([FP_Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TLADM_FabricType_TLADM_FabricProduct'
CREATE INDEX [IX_FK_TLADM_FabricType_TLADM_FabricProduct]
ON [dbo].[TLADM_FabricType]
    ([FT_Product_FK]);
GO

-- Creating foreign key on [QD_RejectReasonFK] in table 'TLADM_QualityDefinition'
ALTER TABLE [dbo].[TLADM_QualityDefinition]
ADD CONSTRAINT [FK_TLADM_QualityDefinition_TLADM_RejectReasons1]
    FOREIGN KEY ([QD_RejectReasonFK])
    REFERENCES [dbo].[TLADM_RejectReasons]
        ([RJR_Pk])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TLADM_QualityDefinition_TLADM_RejectReasons1'
CREATE INDEX [IX_FK_TLADM_QualityDefinition_TLADM_RejectReasons1]
ON [dbo].[TLADM_QualityDefinition]
    ([QD_RejectReasonFK]);
GO

-- Creating foreign key on [MachineNo_FK] in table 'TLSPN_YarnProductionPerMachine'
ALTER TABLE [dbo].[TLSPN_YarnProductionPerMachine]
ADD CONSTRAINT [FK_TLSPN_YarnProductionPerMachine_TLADM_MachineDefinitions]
    FOREIGN KEY ([MachineNo_FK])
    REFERENCES [dbo].[TLADM_MachineDefinitions]
        ([MD_Pk])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TLSPN_YarnProductionPerMachine_TLADM_MachineDefinitions'
CREATE INDEX [IX_FK_TLSPN_YarnProductionPerMachine_TLADM_MachineDefinitions]
ON [dbo].[TLSPN_YarnProductionPerMachine]
    ([MachineNo_FK]);
GO

-- Creating foreign key on [ConsOther_SizeCode_FK] in table 'TLADM_ConsumablesOther'
ALTER TABLE [dbo].[TLADM_ConsumablesOther]
ADD CONSTRAINT [FK_TLADM_ConsumablesOther_TLADM_Sizes]
    FOREIGN KEY ([ConsOther_SizeCode_FK])
    REFERENCES [dbo].[TLADM_Sizes]
        ([SI_id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TLADM_ConsumablesOther_TLADM_Sizes'
CREATE INDEX [IX_FK_TLADM_ConsumablesOther_TLADM_Sizes]
ON [dbo].[TLADM_ConsumablesOther]
    ([ConsOther_SizeCode_FK]);
GO

-- Creating foreign key on [Cust_CustomerCat_FK] in table 'TLADM_CustomerFile'
ALTER TABLE [dbo].[TLADM_CustomerFile]
ADD CONSTRAINT [FK_TLADM_CustomerFile_TLADM_CustomerTypes]
    FOREIGN KEY ([Cust_CustomerCat_FK])
    REFERENCES [dbo].[TLADM_CustomerTypes]
        ([CT_Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TLADM_CustomerFile_TLADM_CustomerTypes'
CREATE INDEX [IX_FK_TLADM_CustomerFile_TLADM_CustomerTypes]
ON [dbo].[TLADM_CustomerFile]
    ([Cust_CustomerCat_FK]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------