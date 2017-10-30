﻿using System;
using System.Collections.Generic;
using System.Linq;
using JosephM.Application.ViewModel.Grid;
using JosephM.Application.ViewModel.RecordEntry.Metadata;
using JosephM.Core.Extentions;
using JosephM.Record.Extentions;
using JosephM.Record.IService;
using JosephM.Record.Metadata;
using JosephM.Record.Query;
using JosephM.Core.Service;
using JosephM.Application.ViewModel.TabArea;
using JosephM.Application.Application;
using JosephM.ObjectMapping;
using JosephM.Core.AppConfig;
using JosephM.Record.Service;
using JosephM.Application.ViewModel.RecordEntry;
using JosephM.Application.ViewModel.RecordEntry.Form;
using JosephM.Application.Modules;

namespace JosephM.Application.ViewModel.Extentions
{
    public static class Extentions
    {
        public static void AddCustomGridFunction(this ModuleBase module, CustomGridFunction customGridFunction, Type type)
        {
            //okay this one is autmatically created by the unity container 
            //but iteratively add and resolve 2 items and verify they are retained in the resolved list
            var customGridFunctions = (CustomGridFunctions)module.ApplicationController.ResolveInstance(typeof(CustomGridFunctions), type.AssemblyQualifiedName);
            customGridFunctions.AddFunction(customGridFunction);
            module.ApplicationController.RegisterInstance(typeof(CustomGridFunctions), type.AssemblyQualifiedName, customGridFunctions);
        }

        public static IEnumerable<GridFieldMetadata> GetGridFields(this IRecordService recordService, string recordType,
            ViewType preferredViewType)
        {
            var view = GetView(recordService, recordType, preferredViewType);
            return view
                .Fields
                .Select(f => new GridFieldMetadata(f))
                .ToArray();
        }

        public static ViewMetadata GetView(this IRecordService recordService, string recordType,  ViewType preferredViewType)
        {
            var savedViews = recordService.GetViews(recordType);
            if (savedViews != null)
            {
                var matchingViews = savedViews.Where(v => v.ViewType == preferredViewType);
                if (matchingViews.Any())
                    return matchingViews.First();
                if (savedViews.Any())
                    return savedViews.First();
            }
            if (preferredViewType == ViewType.LookupView)
            {
                var primaryField = recordService.GetPrimaryField(recordType);
                if (primaryField.IsNullOrWhiteSpace())
                    throw new NullReferenceException(string.Format("No primary field defined for type {0}", recordType));
                return new ViewMetadata(new[] { new ViewField(recordService.GetPrimaryField(recordType), 10, 200) });
            }
            return new ViewMetadata(recordService.GetFields(recordType).Select(f => new ViewField(f, 1, 200)));
        }

        public static GetGridRecordsResponse GetGridRecordPage(this DynamicGridViewModel gridViewModel, IEnumerable<Condition> conditions, IEnumerable<SortExpression> sorts)
        {
            var sortList = sorts == null ? new List<SortExpression>() : sorts.ToList();
            if (gridViewModel.GetLastSortExpression() != null)
                sortList.Insert(0, gridViewModel.GetLastSortExpression());
            var records = gridViewModel.RecordService.GetFirstX(gridViewModel.RecordType,
                gridViewModel.CurrentPageCeiling + 1, null, conditions, sortList);
            var hasMoreRows = records.Count() > gridViewModel.CurrentPageCeiling;
            records = records.Skip(gridViewModel.CurrentPageFloor).Take(gridViewModel.PageSize).ToArray();
            return new GetGridRecordsResponse(records, hasMoreRows);
        }

        public static GetGridRecordsResponse GetGridRecordPage(this DynamicGridViewModel gridViewModel, QueryDefinition query)
        {
            query.Top = gridViewModel.CurrentPageCeiling + 1;
            if (gridViewModel.GetLastSortExpression() != null)
                query.Sorts.Insert(0, gridViewModel.GetLastSortExpression());
            var records = gridViewModel.RecordService.RetreiveAll(query);
            var hasMoreRows = records.Count() > gridViewModel.CurrentPageCeiling;
            records = records.Skip(gridViewModel.CurrentPageFloor).Take(gridViewModel.PageSize).ToArray();
            return new GetGridRecordsResponse(records, hasMoreRows);
        }

        public static Uri ToPrismNavigationUriType(Type type, UriQuery uriQuery)
        {
            var prismQuery = new Microsoft.Practices.Prism.UriQuery();
            if (uriQuery != null)
            {
                foreach (var arg in uriQuery.Arguments)
                    prismQuery.Add(arg.Key, arg.Value);
            }
            var uri = new Uri(type.FullName + prismQuery, UriKind.Relative);
            return uri;
        }

        public static void SaveSettingObject(this TabAreaViewModelBase viewModel, object theObject)
        {
            var theObjectType = theObject.GetType();
            if (!theObjectType.IsTypeOf(typeof(IAllowSaveAndLoad)))
                throw new Exception(string.Format("type {0} is not of type {1}", theObjectType.Name, typeof(IAllowSaveAndLoad).Name));

            //this is an object specifically for entering the name and autoload properties
            //they are mapped into the IAllowSaveAndLoad object after entry then it is saved
            var saveObject = new SaveAndLoadFields();

            Action saveSettings = () =>
            {
                var mapper = new ClassSelfMapper();
                mapper.Map(saveObject, theObject);

                var settingsManager = viewModel.ApplicationController.ResolveType<ISettingsManager>();
                var settings = settingsManager.Resolve<SavedSettings>(theObjectType);

                if (saveObject.Autoload)
                {
                    foreach (var item in settings.SavedRequests.Cast<IAllowSaveAndLoad>())
                        item.Autoload = false;
                }

                settings.SavedRequests = settings.SavedRequests.Union(new[] { theObject }).ToArray();
                settingsManager.SaveSettingsObject(settings, theObjectType);

                viewModel.ClearChildForm();

                viewModel.ApplicationController.UserMessage("Saved!");
            };

            var os = new ObjectRecordService(saveObject, viewModel.ApplicationController, null);
            var ofs = new ObjectFormService(saveObject, os, null);
            var fc = new FormController(os, ofs, viewModel.ApplicationController);
            var vm = new ObjectEntryViewModel(saveSettings, () => viewModel.ClearChildForm(), saveObject, fc);
            viewModel.LoadChildForm(vm);
        }
    }
}
