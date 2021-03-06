﻿using JosephM.Application.Desktop.Module.ServiceRequest;
using JosephM.Application.ViewModel.Dialog;
using JosephM.Record.Xrm.XrmRecord;

namespace JosephM.Deployment.ExportXml
{
    public class ExportXmlDialog :
        ServiceRequestDialog
            <ExportXmlService, ExportXmlRequest,
                ExportXmlResponse, ExportXmlResponseItem>
    {
        public ExportXmlDialog(ExportXmlService service,
            IDialogController dialogController, XrmRecordService lookupService)
            : base(service, dialogController, lookupService)
        {
        }
    }
}