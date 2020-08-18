namespace GrampsView.Droid.Common
{
    using GrampsView.Common.CustomClasses;
    using GrampsView.Data.Model;

    using System.Threading.Tasks;

    internal class PlatformSpecific : IPlatformSpecific
    {
        public async Task ActivityTimeLineAdd(PersonModel argPersonModel)
        {
            // No Timeline functionality so ignore this
        }

        public async Task ActivityTimeLineAdd(FamilyModel argFamilyModel)
        {
            // No Timeline functionality so ignore this
        }


        //public void Init(IEventAggregator iocEventAggregator)
        //{
        //}
    }
}