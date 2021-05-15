namespace GrampsView.ViewModels
{
    using GrampsView.Common.CustomClasses;
    using GrampsView.Data.Repository;

    public class ErrorDialogViewModel : ViewModelBase
    {
        private ErrorInfo _ADArgs = new ErrorInfo();

        private string _Title;

        public ErrorDialogViewModel()
        {
            DataStore.CN.DialogShown = true;

            ErrorInfo t = DataStore.CN.PopupQueue.Dequeue();

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

        public override void BaseHandleDisAppearingEvent()
        {
        }
    }
}