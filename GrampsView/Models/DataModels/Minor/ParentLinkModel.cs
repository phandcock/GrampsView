// TODO Needs XML 1.71 check

namespace GrampsView.Data.Model
{
    using GrampsView.Views;

    using System.Runtime.Serialization;
    using System.Threading.Tasks;

    /// TODO Update fields as per Schema XML 1.71
    [DataContract]
    public class ParentLinkModel : ModelBase, IParentLinkModel
    {
        private FamilyModel _Parents = new FamilyModel();

        public ParentLinkModel()
        {
        }

        public FamilyModel Parents
        {
            get
            {
                return _Parents;
            }

            set
            {
                SetProperty(ref _Parents, value);

                // Set HLinkKey to the family model so Valid is true. TODO Why?
                this.HLinkKey = Parents.HLinkKey;
                this.ModelItemGlyph = Parents.ModelItemGlyph;
            }
        }

        public override async Task UCNavigate()
        {
            await UCNavigateBase(this, nameof(FamilyDetailPage));
            return;
        }
    }
}