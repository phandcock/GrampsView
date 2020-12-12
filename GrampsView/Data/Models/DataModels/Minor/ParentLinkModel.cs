// TODO Needs XML 1.71 check

namespace GrampsView.Data.Model
{
    using System.Runtime.Serialization;

    /// <summary>
    /// GRAMPS Alt element class.
    /// </summary>
    /// TODO Update fields as per Schema
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

                // Set HlinkKey to the family model so Valid is true;
                this.HLinkKey = Parents.HLinkKey;
            }
        }
    }
}