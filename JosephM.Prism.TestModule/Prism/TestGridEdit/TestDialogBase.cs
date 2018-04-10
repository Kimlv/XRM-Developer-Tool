﻿using JosephM.Application.Prism.Module.ServiceRequest;
using JosephM.Application.ViewModel.Dialog;
using JosephM.Application.ViewModel.Fakes;

namespace JosephM.Prism.TestModule.Prism.TestGridEdit
{
    public class TestGridEdit :
        ServiceRequestDialog<TestGridEditService, TestGridEditRequest, TestGridEditResponse, TestGridEditResponseItem>
    {
        public TestGridEdit(IDialogController dialogController)
            : base(new TestGridEditService(), dialogController, FakeRecordService.Get())
        {
        }
    }
}