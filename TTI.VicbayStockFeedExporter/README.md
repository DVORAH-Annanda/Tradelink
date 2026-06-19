# TTI.VicbayStockFeedExporter — corrected project file

This version uses an SDK-style .NET Framework project and targets .NET Framework 4.7.2 (`net472`).

## Best way to add it to the TTI solution

1. In Visual Studio, right-click the solution > **Add** > **Existing Project**.
2. Select `TTI.VicbayStockFeedExporter.csproj`.
3. Right-click the solution > **Restore NuGet Packages**.
4. Build the project.

If Visual Studio says the .NET Framework 4.7.2 targeting pack is missing, install it through Visual Studio Installer (Individual components: `.NET Framework 4.7.2 targeting pack`) and re-open the solution.

## Important

Set the `TTISqlConnection` value and `CentralWarehouseId` in `App.config` before running it.
