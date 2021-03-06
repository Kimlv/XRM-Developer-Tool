﻿using JosephM.Application.ViewModel.Dialog;
using JosephM.Record.Xrm.XrmRecord;

namespace JosephM.Xrm.RecordExtract.RecordExtract
{
    public class XrmRecordExtractDialog : RecordExtractDialogBase<XrmRecordExtractService>
    {
        public XrmRecordExtractDialog(XrmRecordExtractService service, IDialogController dialogController,
            XrmRecordService xrmRecordService)
            : base(service, dialogController, xrmRecordService)
        {
        }
    }
}