namespace GrampsView.iOS.Common
{
    using GrampsView.Common.CustomClasses;
    using GrampsView.Data.Model;

    using System.Threading.Tasks;

    internal partial class PlatformSpecific : IPlatformSpecific
    {
        public async Task ActivityTimeLineAdd(PersonModel argPersonModel)
        {
            // No Timeline functionality so ignore this

            return;
        }

        public async Task ActivityTimeLineAdd(FamilyModel argFamilyModel)
        {
            // No Timeline functionality so ignore this
        }
    }
}