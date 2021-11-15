namespace GrampsView.ViewModels
{
    using GrampsView.Data.Repository;

    using SharedSharp.Errors;

    public class ErrorDialogViewModel : ViewModelBase
    {
        private ErrorInfo _ADArgs = new ErrorInfo();

        private string _Title;

        public ErrorInfo AdaArgs
        {
            get => _ADArgs;
            set => SetProperty(ref _ADArgs, value);
        }

        public string Title
        {
            get => _Title;

            set => SetProperty(ref _Title, value);
        }

        public ErrorDialogViewModel()
        {
            DataStore.Instance.CN.DialogShown = true;

            ErrorInfo t = new ErrorInfo();

            if (DataStore.Instance.CN.PopupQueue.Count > 0)
            {
                t = DataStore.Instance.CN.PopupQueue.Dequeue();
            }

            Title = t.DialogBoxTitle;

            AdaArgs = t;
        }

        //public override void BaseHandleDisAppearingEvent()
        //{
        //}
    }
}