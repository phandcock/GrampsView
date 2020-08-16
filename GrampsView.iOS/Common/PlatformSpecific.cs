namespace GrampsView.iOS.Common
{
    using GrampsView.Common.CustomClasses;
    using GrampsView.Data.Model;

    using Prism.Events;

    using System.Threading.Tasks;

    internal class PlatformSpecific : IPlatformSpecific
    {
        public PlatformSpecific(IEventAggregator iocEventAggregator)
        {
        }

        public async Task ActivityTimeLineAdd(PersonModel argPersonModel)
        {
            // No Timeline functionality so ignore this

            return ;
        }
    }
}