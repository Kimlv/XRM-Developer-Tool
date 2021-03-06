﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JosephM.Core.FieldType;

namespace JosephM.Record.Metadata
{
    public interface IPicklistSet : IMetadata
    {
        IEnumerable<PicklistOption> PicklistOptions { get; }
        string DisplayName { get; }
    }
}
