// TODO Needs XML 1.71 check

using GrampsView.Models.DataModels;
using GrampsView.Models.DataModels.Interfaces;
using GrampsView.Views;

using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace GrampsView.Data.Model
{
    /// TODO Update fields as per Schema XML 1.71
    [DataContract]
    public class ParentLinkModel : ModelBase, IParentLinkModel
    {
        private FamilyModel _Parents = new();

        public ParentLinkModel()
        {
        }

        public FamilyModel Parents
        {
            get => _Parents;

            set
            {
                _ = SetProperty(ref _Parents, value);

                // Set HLinkKey to the family model so Valid is true. TODO Why?
                HLinkKey = Parents.HLinkKey;
                ModelItemGlyph = Parents.ModelItemGlyph;
            }
        }

        public override async Task UCNavigate()
        {
            await UCNavigateBase(this, nameof(FamilyDetailPage));
            return;
        }
    }
}