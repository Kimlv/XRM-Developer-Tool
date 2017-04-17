﻿using System;
using System.IO;
using System.Linq;
using System.Reflection;
using JosephM.Application.ViewModel.Fakes;
using JosephM.Application.ViewModel.RecordEntry.Form;
using JosephM.Record.Query;
using JosephM.XRM.VSIX.Commands.DeployAssembly;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JosephM.Record.Xrm.Test;
using JosephM.Xrm.Test;
using JosephM.XRM.VSIX.Commands.UpdateAssembly;
using Fields = JosephM.Xrm.Schema.Fields;
using Entities = JosephM.Xrm.Schema.Entities;

namespace JosephM.Xrm.Vsix.Test
{
    [TestClass]
    public class VsixUpdateAssemblyDialogTests : JosephMVsixTests
    {
        [TestMethod]
        public void VsixUpdateAssemblyDialogTest()
        {
            var packageSettings = GetPackageSettingsAddToSolution();
            DeployAssembly(packageSettings);

            var dialog = new UpdateAssemblyDialog(new FakeDialogController(new FakeApplicationController()),
                GetTestPluginAssemblyFile(), XrmRecordService);
            dialog.Controller.BeginDialog();

        }
    }
}