namespace GrampsView.Services
{
    using Prism.Events;

    public interface IWhatsNewDisplayService
    {
        bool ShowIfAppropriate(IEventAggregator iocEventAggregator);
    }
}