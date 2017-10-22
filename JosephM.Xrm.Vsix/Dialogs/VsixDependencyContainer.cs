﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JosephM.Core.AppConfig;
using Microsoft.Practices.Unity;
using JosephM.Application.Application;
using JosephM.Prism.XrmModule.SavedXrmConnections;
using JosephM.XRM.VSIX.Utilities;
using JosephM.Application.ViewModel.Grid;
using JosephM.Record.Xrm.XrmRecord;
using JosephM.Prism.Infrastructure.Module.Crud;
using JosephM.Application.ViewModel.Dialog;
using JosephM.Record.Service;

namespace JosephM.XRM.VSIX.Dialogs
{
    public class VsixDependencyContainer : IDependencyResolver
    {
        private IUnityContainer UnityContainer { get; set; }

        public VsixDependencyContainer(IUnityContainer unityContainer)
        {
            UnityContainer = unityContainer;
        }

        public object ResolveType(Type type)
        {
            return UnityContainer.Resolve(type);
        }

        public void RegisterInstance(Type type, object instance)
        {
            UnityContainer.RegisterInstance(type, instance);
        }

        public object ResolveType(string name)
        {
            return UnityContainer.Resolve(typeof(object), name);
        }

        public void RegisterType<I, T>()
        {
            UnityContainer.RegisterType(typeof (I), typeof (T));
        }

        public void RegisterTypeForNavigation<T>()
        {
            UnityContainer.RegisterType(typeof(object), typeof(T), typeof(T).FullName);
        }

        public static VsixDependencyContainer Create(XrmPackageSettings settings, IVisualStudioService visualStudioService)
        {
            var container = new VsixDependencyContainer(new UnityContainer());
            container.RegisterInstance(typeof(ISettingsManager), new VsixSettingsManager(visualStudioService));
            container.RegisterInstance(typeof(ISavedXrmConnections), settings);
            return container;
        }

        public void RegisterInstance(Type type, string key, object instance)
        {
            UnityContainer.RegisterInstance(type, key, instance);
        }

        public object ResolveInstance(Type type, string key)
        {
            return UnityContainer.Resolve(type, key);
        }
    }
}
