﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JosephM.Record.Metadata;

namespace JosephM.Record.IService
{
    public interface IFieldMetadata
    {
        RecordFieldType FieldType { get; }
        bool IsMandatory { get; }
        string DisplayName { get; }
        int MaxLength { get; }
        decimal MinValue { get; }
        decimal MaxValue { get; }
        string SchemaName { get; }
        bool IsPrimaryKey { get; }
        string Description { get; }
        bool Audit { get; }
        bool Searchable { get; }
        TextFormat TextFormat { get; }
        bool IncludeTime { get; }
        int DecimalPrecision { get; }
        IntegerType IntegerFormat { get; }
        bool IsNonNullable { get; }
        bool IsSharedPicklist { get; }
        string PicklistName { get; }
        bool IsCustomField { get; }
        bool Readable { get; }
        bool Writeable { get; }
        bool Createable { get; }
        bool IsDisplayRelated { get; }

        string MetadataId { get; }
    }
}
