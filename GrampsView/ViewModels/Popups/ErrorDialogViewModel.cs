namespace GrampsView.ViewModels
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
            Title = DataStore.Instance.CN.DialogArgs.DialogBoxTitle;

            AdaArgs = DataStore.Instance.CN.DialogArgs;
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