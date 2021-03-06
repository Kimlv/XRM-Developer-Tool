﻿using JosephM.TestModule.TestDialog;
using JosephM.XrmModule.Crud;
using JosephM.Xrm.Vsix.Application;
using JosephM.Xrm.Vsix.Module.DeployAssembly;
using JosephM.Xrm.Vsix.Module.PluginTriggers;
using JosephM.Xrm.Vsix.Module.Web;
using JosephM.Xrm.Vsix.Test;
using System;
using System.IO;
using System.Windows;

namespace JosephM.Xrm.Vsix.TestShell
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            //load an application with the module buttons and fake things to spawn the vsix dialogs

            var container = new VsixDependencyContainer();

            var applicationName = "Vsix Test Shell";

            var settingsFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                    "JosephM Xrm", applicationName);
            var visualStudioService = new FakeVisualStudioService(solutionDirectory: settingsFolder);
            container.RegisterInstance(typeof(IVisualStudioService), visualStudioService);

            var applicationController = new VsixApplicationController(container, applicationName: applicationName);
            VsixApplication = new VsixApplication(applicationController, new VsixSettingsManager(visualStudioService), new Guid("43816e6d-4db8-48d6-8bfa-75916cb080f0"));

            VsixApplication.AddModule<OpenWebModule>();
            VsixApplication.AddModule<DeployAssemblyModule>();
            VsixApplication.AddModule<ManagePluginTriggersModule>();
            VsixApplication.AddModule<XrmCrudModule>();
            VsixApplication.AddModule<TestDialogModule>();
        }

        public VsixApplication VsixApplication { get; set; }
    }
}
