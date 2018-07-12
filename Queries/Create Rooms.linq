<Query Kind="Statements">
  <Connection>
    <ID>76e8a54c-d3ed-416f-bfe9-6df181e63e5f</ID>
    <Persist>true</Persist>
    <Server>tcp:pltnm-dev-testing.database.windows.net,1433</Server>
    <SqlSecurity>true</SqlSecurity>
    <Database>DCC_Kelly</Database>
    <UserName>kstrandholm</UserName>
    <Password>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAA3OCcWARJzkS50yxLSIqc4wAAAAACAAAAAAADZgAAwAAAABAAAAALOYKQcsBuXwegTbHCzZymAAAAAASAAACgAAAAEAAAAF7ByM+uNJsferum3TUGYe8YAAAA1XVlJaeslvOOFC5so7OcA4c9IxO+PVhLFAAAAMylMsgdxIxK6UhK6Wv/40oTy2fL</Password>
    <DbVersion>Azure</DbVersion>
  </Connection>
  <NuGetReference>com.pltnm.package.linqpadlibrary</NuGetReference>
  <NuGetReference>com.pltnm.windows.commonlibraries</NuGetReference>
  <NuGetReference>com.pltnm.windows.mothermodel</NuGetReference>
  <Namespace>CommonLibs.Extensions</Namespace>
  <Namespace>CommonLibs.Misc</Namespace>
  <Namespace>LinqPadLibrary</Namespace>
  <Namespace>LinqPadLibrary.dba_Utilities</Namespace>
  <Namespace>LinqPadLibrary.dbe_Utilities</Namespace>
  <Namespace>LinqPadLibrary.Helper_Classes</Namespace>
  <Namespace>LinqPadLibrary.Statements</Namespace>
  <Namespace>LinqPadLibrary.Variable_Utilities</Namespace>
  <Namespace>MotherModel</Namespace>
  <Namespace>MotherModel.EntityFramework</Namespace>
  <Namespace>MotherModel.Models</Namespace>
  <Namespace>MotherModel.Models.Custom</Namespace>
  <Namespace>MotherModel.Models.Custom.Interfaces</Namespace>
  <Namespace>MotherModel.Models.Enums</Namespace>
  <Namespace>MotherModel.Models.Interfaces</Namespace>
  <Namespace>MotherModel.Models.Mappings</Namespace>
  <Namespace>MotherModel.Utilities</Namespace>
  <Namespace>MotherModel.Utilities.Enums</Namespace>
  <Namespace>PremiumCompute</Namespace>
  <Namespace>System.ComponentModel.DataAnnotations</Namespace>
  <Namespace>System.ComponentModel.DataAnnotations.Schema</Namespace>
  <Namespace>System.Data.Entity</Namespace>
  <Namespace>System.Data.Entity.Core</Namespace>
  <Namespace>System.Data.Entity.Core.Common</Namespace>
  <Namespace>System.Data.Entity.Core.Common.CommandTrees</Namespace>
  <Namespace>System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder</Namespace>
  <Namespace>System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder.Spatial</Namespace>
  <Namespace>System.Data.Entity.Core.Common.EntitySql</Namespace>
  <Namespace>System.Data.Entity.Core.EntityClient</Namespace>
  <Namespace>System.Data.Entity.Core.Mapping</Namespace>
  <Namespace>System.Data.Entity.Core.Metadata.Edm</Namespace>
  <Namespace>System.Data.Entity.Core.Objects</Namespace>
  <Namespace>System.Data.Entity.Core.Objects.DataClasses</Namespace>
  <Namespace>System.Data.Entity.Infrastructure</Namespace>
  <Namespace>System.Data.Entity.Infrastructure.Annotations</Namespace>
  <Namespace>System.Data.Entity.Infrastructure.DependencyResolution</Namespace>
  <Namespace>System.Data.Entity.Infrastructure.Design</Namespace>
  <Namespace>System.Data.Entity.Infrastructure.Interception</Namespace>
  <Namespace>System.Data.Entity.Infrastructure.MappingViews</Namespace>
  <Namespace>System.Data.Entity.Infrastructure.Pluralization</Namespace>
  <Namespace>System.Data.Entity.Migrations</Namespace>
  <Namespace>System.Data.Entity.Migrations.Builders</Namespace>
  <Namespace>System.Data.Entity.Migrations.Design</Namespace>
  <Namespace>System.Data.Entity.Migrations.History</Namespace>
  <Namespace>System.Data.Entity.Migrations.Infrastructure</Namespace>
  <Namespace>System.Data.Entity.Migrations.Model</Namespace>
  <Namespace>System.Data.Entity.Migrations.Sql</Namespace>
  <Namespace>System.Data.Entity.Migrations.Utilities</Namespace>
  <Namespace>System.Data.Entity.ModelConfiguration</Namespace>
  <Namespace>System.Data.Entity.ModelConfiguration.Configuration</Namespace>
  <Namespace>System.Data.Entity.ModelConfiguration.Conventions</Namespace>
  <Namespace>System.Data.Entity.Spatial</Namespace>
  <Namespace>System.Data.Entity.SqlServer</Namespace>
  <Namespace>System.Data.Entity.SqlServer.Utilities</Namespace>
  <Namespace>System.Data.Entity.Utilities</Namespace>
  <Namespace>System.Data.Entity.Validation</Namespace>
</Query>

var diagnosticInfo = "Creating Rooms";

var rooms = new List<Rooms> {
    new Rooms
    {
        RoomName = "Royale I",
        Capacity = 20,
        Venue = "Skyward Hotel and Convention Center",
        UpdateTime = DateTime.Now,
        DiagnosticInformation = diagnosticInfo
    },
    new Rooms
    {
        RoomName = "Royale II",
        Capacity = 30,
        Venue = "Skyward Hotel and Convention Center",
        UpdateTime = DateTime.Now,
        DiagnosticInformation = diagnosticInfo
    },
    new Rooms
    {
        RoomName = "Grande I",
        Capacity = 40,
        Venue = "Skyward Hotel and Convention Center",
        UpdateTime = DateTime.Now,
        DiagnosticInformation = diagnosticInfo
    },
    new Rooms
    {
        RoomName = "Grande II",
        Capacity = 40,
        Venue = "Skyward Hotel and Convention Center",
        UpdateTime = DateTime.Now,
        DiagnosticInformation = diagnosticInfo
    }
};

Rooms.InsertAllOnSubmit(rooms);

if (false)
{
    SubmitChanges();
}

Rooms.Dump();