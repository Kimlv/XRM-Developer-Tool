﻿using System;
using System.IO;
using System.Linq;
using System.Reflection;
using JosephM.Application.ViewModel.Fakes;
using JosephM.Application.ViewModel.RecordEntry.Form;
using JosephM.ObjectMapping;
using JosephM.Record.Query;
using JosephM.Record.Xrm.Mappers;
using JosephM.XRM.VSIX.Commands.DeployAssembly;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JosephM.Record.Xrm.Test;
using JosephM.Xrm.Test;
using JosephM.XRM.VSIX.Commands.RefreshConnection;
using JosephM.XRM.VSIX.Commands.UpdateAssembly;
using Fields = JosephM.Xrm.Schema.Fields;
using Entities = JosephM.Xrm.Schema.Entities;

namespace JosephM.Xrm.Vsix.Test
{
    [TestClass]
    public class RefreshConnectionDialogTests : JosephMVsixTests
    {
        [TestMethod]
        public void RefreshConnectionDialogTest()
        {
            var fakeVisualStudioService = CreateVisualStudioService();
            var xrmConfiguration = new InterfaceMapperFor<IXrmConfiguration,XrmConfiguration>().Map(XrmConfiguration);
            var xrmRecordConfiguration = new XrmConfigurationMapper().Map(xrmConfiguration);
            var dialog = new ConnectionEntryDialog(CreateDialogController(), xrmRecordConfiguration, fakeVisualStudioService);
            dialog.Controller.BeginDialog();

            var entryViewModel = (ObjectEntryViewModel)dialog.Controller.UiItems.First();
            Assert.IsTrue(entryViewModel.Validate());
            entryViewModel.OnSave();
        }
    }
}
