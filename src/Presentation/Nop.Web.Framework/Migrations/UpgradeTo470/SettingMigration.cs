﻿using FluentMigrator;
using Nop.Core.Domain.Customers;
using Nop.Core.Infrastructure;
using Nop.Data;
using Nop.Data.Migrations;
using Nop.Services.Configuration;

namespace Nop.Web.Framework.Migrations.UpgradeTo470
{
    [NopMigration("2023-02-01 14:00:03", "4.70.0", UpdateMigrationType.Settings, MigrationProcessType.Update)]
    public class SettingMigration : MigrationBase
    {
        /// <summary>Collect the UP migration expressions</summary>
        public override void Up()
        {
            if (!DataSettingsManager.IsDatabaseInstalled())
                return;

            //do not use DI, because it produces exception on the installation process
            var settingService = EngineContext.Current.Resolve<ISettingService>();

            var customerSettings = settingService.LoadSetting<CustomerSettings>();

            if (!settingService.SettingExists(customerSettings, settings => settings.PasswordMaxLength))
            {
                customerSettings.PasswordMaxLength = 64;
                settingService.SaveSetting(customerSettings, settings => settings.PasswordMaxLength);
            }
        }

        public override void Down()
        {
            //add the downgrade logic if necessary 
        }
    }
}