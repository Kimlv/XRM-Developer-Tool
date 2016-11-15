﻿using JosephM.Application.ViewModel.SettingTypes;
using JosephM.Core.Attributes;
using JosephM.Core.FieldType;
using JosephM.Core.Service;
using System.Collections.Generic;
using JosephM.Record.Attributes;
using JosephM.Record.IService;
using JosephM.Record.Metadata;
using JosephM.Record.Query;

namespace JosephM.CodeGenerator.Service
{
    [DisplayName("Code Generation")]
    public class CodeGeneratorRequest : ServiceRequestBase
    {
        public CodeGeneratorRequest()
        {
            IncludeEntities = true;
            IncludeFields = true;
            IncludeOptions = true;
            IncludeRelationships = true;
            AllRecordTypes = true;
            IncludeAllSharedOptions = true;
            IncludeActions = true;
        }

        [RequiredProperty]
        public CodeGeneratorType? Type { get; set; }

        [RequiredProperty]
        [PropertyInContextByPropertyValues(nameof(Type), new object[] { CodeGeneratorType.CSharpMetadata, CodeGeneratorType.JavaScriptOptionSets })]
        public Folder Folder { get; set; }

        [Multiline]
        [RequiredProperty]
        [PropertyInContextByPropertyValue(nameof(Type), CodeGeneratorType.FetchToJavascript)]
        public string Fetch { get; set; }

        [RequiredProperty]
        [InitialiseFor(nameof(Type), CodeGeneratorType.CSharpMetadata, "Schema")]
        [PropertyInContextByPropertyValues(nameof(Type), new object[] { CodeGeneratorType.CSharpMetadata, CodeGeneratorType.JavaScriptOptionSets })]
        public string FileName { get; set; }

        [RequiredProperty]
        [InitialiseFor(nameof(Type), CodeGeneratorType.CSharpMetadata, "Schema")]
        [PropertyInContextByPropertyValues(nameof(Type), new object[] { CodeGeneratorType.CSharpMetadata, CodeGeneratorType.JavaScriptOptionSets })]
        public string Namespace { get; set; }

        [RequiredProperty]
        [PropertyInContextByPropertyValues(nameof(Type), new object[] { CodeGeneratorType.JavaScriptOptionSets })]
        [RecordTypeFor(nameof(OptionField))]
        public RecordType RecordType { get; set; }

        [RequiredProperty]
        [PropertyInContextByPropertyValue(nameof(Type), CodeGeneratorType.CSharpMetadata)]
        public bool IncludeEntities { get; set; }

        [RequiredProperty]
        [PropertyInContextByPropertyValue(nameof(Type), CodeGeneratorType.CSharpMetadata)]
        public bool IncludeFields { get; set; }

        [RequiredProperty]
        [PropertyInContextByPropertyValue(nameof(Type), CodeGeneratorType.CSharpMetadata)]
        public bool IncludeRelationships { get; set; }

        [RequiredProperty]
        [PropertyInContextByPropertyValue(nameof(Type), CodeGeneratorType.CSharpMetadata)]
        public bool IncludeOptions { get; set; }

        [RequiredProperty]
        [PropertyInContextByPropertyValue(nameof(IncludeOptions), true)]
        [PropertyInContextByPropertyValue(nameof(Type), CodeGeneratorType.CSharpMetadata)]
        public bool IncludeAllSharedOptions { get; set; }

        [RequiredProperty]
        [PropertyInContextByPropertyValue(nameof(Type), CodeGeneratorType.CSharpMetadata)]
        public bool IncludeActions { get; set; }


        [RequiredProperty]
        [PropertyInContextByPropertyValues(nameof(Type), new object[] { CodeGeneratorType.CSharpMetadata })]
        public bool AllRecordTypes { get; set; }

        [RequiredProperty]
        [PropertyInContextByPropertyValue(nameof(AllRecordTypes), false)]
        [PropertyInContextByPropertyNotNull(nameof(Type))]
        public IEnumerable<RecordTypeSetting> RecordTypes { get; set; }

        [RequiredProperty]
        [PropertyInContextByPropertyValue(nameof(Type), CodeGeneratorType.JavaScriptOptionSets)]
        [PropertyInContextByPropertyNotNull(nameof(RecordType))]
        public bool AllFields { get; set; }

        [RequiredProperty]
        [PropertyInContextByPropertyValue(nameof(Type), CodeGeneratorType.JavaScriptOptionSets)]
        [PropertyInContextByPropertyValue(nameof(AllFields), false)]
        [PropertyInContextByPropertyNotNull(nameof(RecordType))]
        [LookupCondition(nameof(IFieldMetadata.FieldType), ConditionType.In, new [] { RecordFieldType.Picklist, RecordFieldType.State, RecordFieldType.Status })]
        public RecordField OptionField { get; set; }


    }
}