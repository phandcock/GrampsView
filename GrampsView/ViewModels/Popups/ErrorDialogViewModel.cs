namespace GrampsView.ViewModels
{
    using GrampsView.Data.Repository;

    using Microsoft.Extensions.DependencyInjection;

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
            App.Current.Services.GetService<IErrorNotifications>().DialogShown = true;

            ErrorInfo t = new ErrorInfo();

            if (DataStore.Instance.CN.PopupQueue.Count > 0)
            {
                t = App.Current.Services.GetService<IErrorNotifications>().PopupQueue.Dequeue();
            }

            Title = t.DialogBoxTitle;

            AdaArgs = t;
        }

        //public override void BaseHandleDisAppearingEvent()
        //{
        //}
    }
}