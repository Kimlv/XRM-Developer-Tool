﻿using JosephM.Application.Application;
using JosephM.Application.Prism.Application;
using JosephM.Prism.XrmModule.SavedXrmConnections;
using JosephM.Xrm.Vsix.Module.PackageSettings;

namespace JosephM.Xrm.Vsix.Application
{
    public class VsixDependencyContainer : PrismDependencyContainer
    {
        public VsixDependencyContainer()
            : base()
        {
        }

        public static VsixDependencyContainer Create(XrmPackageSettings settings, IVisualStudioService visualStudioService)
        {
            var container = new VsixDependencyContainer();
            container.RegisterInstance(typeof(ISettingsManager), new VsixSettingsManager(visualStudioService));
            container.RegisterInstance(typeof(ISavedXrmConnections), settings);
            return container;
        }
    }
}
