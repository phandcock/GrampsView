namespace GrampsView.Services
{
    using GrampsView.Common;
    using GrampsView.Views;

    public class InputFileDisplayService : IInputFileDisplayService
    {
        private static bool shown;

        public InputFileDisplayService()
        {
        }

        public void ShowIfAppropriate()
        {
            if (!CommonLocalSettings.DataSerialised && !shown)
            {
                shown = true;

                CommonRoutines.Navigate(nameof(FileInputHandlerPage));
            }
        }
    }
}