//-----------------------------------------------------------------------
//
namespace GrampsView.ViewModels
{
    using GrampsView.Common.CustomClasses;

    using Prism.Commands;
    using Prism.Mvvm;
    using Prism.Services.Dialogs;

    using System;
    using System.Diagnostics.Contracts;

    public class ErrorDialogViewModel : BindableBase, IDialogAware
    {
        private ErrorInfo _ADArgs = new ErrorInfo();

        private string _Title;

        public ErrorDialogViewModel()
        {
            CloseCommand = new DelegateCommand(() => RequestClose(null));
        }

        public event Action<IDialogParameters> RequestClose;

        public ErrorInfo AdaArgs
        {
            get => _ADArgs;
            set => SetProperty(ref _ADArgs, value);
        }

        public DelegateCommand CloseCommand
        {
            get;
        }

        public string Title
        {
            get
            {
                return _Title;
            }

            set
            {
                SetProperty(ref _Title, value);
            }
        }

        public bool CanCloseDialog() => true;

        public void OnDialogClosed()
        {
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            Contract.Requires(parameters != null);
            Contract.Requires(parameters.Count == 1);

            // AdaArgs.Name = parameters.GetValue<string>("Name"); AdaArgs.Text = parameters.GetValue<string>("Text");

            ErrorInfo tempArgs = parameters["argADA"] as ErrorInfo;

            Title = tempArgs.DialogBoxTitle;

            AdaArgs = tempArgs;
        }

        protected virtual void CloseDialog(string parameter)
        {
        }
    }
}