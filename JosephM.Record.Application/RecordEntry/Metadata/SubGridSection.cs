﻿#region

using System.Collections.Generic;

#endregion

namespace JosephM.Application.ViewModel.RecordEntry.Metadata
{
    public class SubGridSection : FormSection
    {
        /// <summary>
        /// !! DEPRECIATED !! Use EnumerableField type instead
        /// </summary>
        /// <param name="sectionName"></param>
        /// <param name="linkedRecordType"></param>
        /// <param name="linkedRecordLookup"></param>
        /// <param name="fields"></param>
        public SubGridSection(string sectionName, string linkedRecordType, string linkedRecordLookup,
            IEnumerable<GridFieldMetadata> fields)
            : base(sectionName, 10000)
        {
            Fields = fields;
            LinkedRecordLookup = linkedRecordLookup;
            LinkedRecordType = linkedRecordType;
        }

        public string LinkedRecordType { get; private set; }
        public string LinkedRecordLookup { get; private set; }
        public IEnumerable<GridFieldMetadata> Fields { get; private set; }
    }
}