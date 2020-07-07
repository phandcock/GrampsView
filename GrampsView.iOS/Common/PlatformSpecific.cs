namespace GrampsView.iOS.Common
{
    using GrampsView.Common.CustomClasses;
    using GrampsView.Data.Model;

    using Prism.Events;

    internal class PlatformSpecific : IPlatformSpecific
    {
        public PlatformSpecific(IEventAggregator iocEventAggregator)
        {
        }

        public void ActivityTimeLineAdd(PersonModel argPersonModel)
        {
        }
    }
}