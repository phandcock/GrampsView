namespace GrampsView.Droid.Common
{
    using GrampsView.Common.CustomClasses;
    using GrampsView.Data.Model;

    using System.Threading.Tasks;

    internal class PlatformSpecific : IPlatformSpecific
    {
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task ActivityTimeLineAdd(PersonModel argPersonModel)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            // No Timeline functionality so ignore this
        }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task ActivityTimeLineAdd(FamilyModel argFamilyModel)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            // No Timeline functionality so ignore this
        }
    }
}