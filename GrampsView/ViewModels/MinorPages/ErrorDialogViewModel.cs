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
    using GrampsView.Common;

    using Prism.Commands;
    using Prism.Mvvm;
    using Prism.Services.Dialogs;

    using System;
    using System.Diagnostics.Contracts;

    public class ErrorDialogViewModel : BindableBase, IDialogAware
    {
        private ActionDialogArgs _ADArgs;

        public ErrorDialogViewModel()
        {
            CloseCommand = new DelegateCommand(() => RequestClose(null));
        }

        public event Action<IDialogParameters> RequestClose;

        public ActionDialogArgs AdaArgs
        {
            get => _ADArgs;
            set => SetProperty(ref _ADArgs, value);
        }

        public DelegateCommand CloseCommand { get; }

        public bool CanCloseDialog() => true;

        public void OnDialogClosed()
        {
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            Contract.Requires(parameters != null);
            Contract.Requires(parameters.Count == 1);

            AdaArgs = parameters["adaArgs"] as ActionDialogArgs;
        }

        protected virtual void CloseDialog(string parameter)
        {
        }
    }
}