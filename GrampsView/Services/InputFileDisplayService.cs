namespace GrampsView.Services
{
    using GrampsView.Common;
    using GrampsView.Views;

    using System.Threading.Tasks;

    /// <summary>
    /// Shows the New File Handler page of appropriate.
    /// </summary>
    public class InputFileDisplayService : IInputFileDisplayService
    {
        private static bool shown;

        public InputFileDisplayService()
        {
        }

        public async Task ShowIfAppropriate()
        {
            if (!CommonLocalSettings.DataSerialised && !shown)
            {
                shown = true;

                await CommonRoutines.NavigateAsync(nameof(FileInputHandlerPage));
            }
        }
    }
}