using GrampsView.Data.Model;

using System.Threading.Tasks;

namespace GrampsView.Common.CustomClasses
{
    public interface IPlatformSpecific
    {
        Task ActivityTimeLineAdd(PersonModel argPersonModel);

        Task ActivityTimeLineAdd(FamilyModel argFamilyModel);
    }
}