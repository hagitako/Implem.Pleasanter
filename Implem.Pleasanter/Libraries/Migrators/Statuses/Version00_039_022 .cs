﻿using Implem.Libraries.Utilities;
using Implem.Pleasanter.Libraries.DataSources;
using Implem.Pleasanter.Libraries.Settings;
using Implem.Pleasanter.Models;
using System.Data;
using System.Linq;
namespace Implem.Pleasanter.Libraries.Migrators.Statuses
{
    public static class Version00_039_022
    {
        public static void Migrate()
        {
            new ExportSettingCollection()
                .ForEach(exportSettingModel =>
                    Migrate(exportSettingModel));
        }

        private static void Migrate(ExportSettingModel exportSettingModel)
        {
            var ss = Rds.ExecuteScalar_string(statements:
                Rds.SelectSites(
                    column: Rds.SitesColumn().SiteSettings(),
                    where: Rds.SitesWhere().SiteId(exportSettingModel.ReferenceId)))
                        .Deserialize<SiteSettings>();
            if (ss != null)
            {
                ss.Exports = ss.Exports ?? new SettingList<Export>();
                ss.Exports.Add(new Export(
                    ss: ss,
                    id: ss.Exports.Any()
                        ? ss.Exports.Max(o => o.Id) + 1
                        : 1,
                    name: exportSettingModel.Title.Value,
                    header: exportSettingModel.AddHeader,
                    columns: exportSettingModel.ExportColumns.Columns
                        .Where(o => o.Value)
                        .Select(o => o.Key)));
                Rds.ExecuteNonQuery(statements:
                    Rds.UpdateSites(
                        param: Rds.SitesParam().SiteSettings(ss.RecordingJson()),
                        where: Rds.SitesWhere().SiteId(exportSettingModel.ReferenceId)));
            }
        }
    }
}