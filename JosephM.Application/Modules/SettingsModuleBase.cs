﻿#region

using JosephM.Application.Application;
using JosephM.Application.Options;
using JosephM.Core.AppConfig;
using System;
using System.IO;

#endregion

namespace JosephM.Application.Modules
{
    /// <summary>
    ///     Base Class For Implementing Modules To Plug Into The Application Framework
    /// </summary>
    public abstract class SettingsModuleBase : ActionModuleBase
    {
        public override void InitialiseModule()
        {
            AddSetting(MainOperationName, DialogCommand);
        }

        public override void RegisterTypes()
        {
        }
    }
}