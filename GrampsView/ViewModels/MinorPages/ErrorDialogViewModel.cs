//-----------------------------------------------------------------------
//
// View model for the fly-out page view
//
// <copyright file="AboutViewModel.cs" company="MeMyselfAndI">
// Copyright (c) MeMyselfAndI. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.ViewModels
{
    using System;

    using GrampsView.Common;

    using Prism.AppModel;
    using Prism.Commands;
    using Prism.Mvvm;
    using Prism.Services.Dialogs;

    public class ErrorDialogViewModel : BindableBase, IDialogAware, IAutoInitialize
    {
        private ActionDialogArgs _ADArgs;

        public ErrorDialogViewModel()
        {
            CloseCommand = new DelegateCommand(() => RequestClose(null));
        }

        public event Action<IDialogParameters> RequestClose;

        [AutoInitialize(true)] // Makes adaArgs parameter required
        public ActionDialogArgs AdaArgs
        {
            get => _ADArgs;
            set => SetProperty(ref _ADArgs, value);
        }

        public DelegateCommand CloseCommand { get; }

        public bool CanCloseDialog() => true;

        public void OnDialogClosed()
        {
            Console.WriteLine("The Error Dialog has been closed...");
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            // No need to do anything as IAutoInitialize will take care of what we need here...
        }
    }
}