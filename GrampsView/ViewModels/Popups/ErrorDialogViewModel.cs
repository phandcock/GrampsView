﻿namespace GrampsView.ViewModels
{
    using GrampsView.Common;
    using GrampsView.Common.CustomClasses;
    using GrampsView.Data.Repository;

    public class ErrorDialogViewModel : CommonBindableBase
    {
        private ErrorInfo _ADArgs = new ErrorInfo();

        private string _Title;

        public ErrorDialogViewModel()
        {
            DataStore.Instance.CN.DialogShown = true;

            ErrorInfo t = DataStore.Instance.CN.PopupQueue.Dequeue();

            Title = t.DialogBoxTitle;

            AdaArgs = t;
        }

        public ErrorInfo AdaArgs
        {
            get => _ADArgs;
            set => SetProperty(ref _ADArgs, value);
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
    }
}