﻿#region

using System;
using System.IO;
using JosephM.Core.Extentions;
using JosephM.Core.Service;
using JosephM.Record.IService;

#endregion

namespace JosephM.Deployment.ImportCsvs
{
    public class ImportCsvsResponseItem : DataImportResponseItem
    {
        public string FileName 
        {
            get { return FileNameQualified.IsNullOrWhiteSpace() ? null : Path.GetFileName(FileNameQualified); }
        }

        private string FileNameQualified { get; set; }

        public ImportCsvsResponseItem(string message, string fileNameQualified, Exception ex)
            : base(message, ex)
        {
            FileNameQualified = fileNameQualified;
        }

        public ImportCsvsResponseItem(DataImportResponseItem item)
            : base(item.Entity, item.Field, item.Name, item.Message, item.Exception)
        {
        }
    }
}