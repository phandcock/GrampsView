// XML 171 - All fields defined

using GrampsView.Data.Model;

namespace GrampsView.Models.HLinks.Models
{
    using GrampsView.Common;
    using GrampsView.Models.HLinks;
    using GrampsView.Views;

    using System.Threading.Tasks;

    /// <summary>
    /// HLink to a Date Model.
    /// </summary>

    public class HLinkDateModelStr : HLinkBase, IHLinkDateModel
    {
        public HLinkDateModelStr()
        {
            HLinkGlyphItem.Symbol = Constants.IconDate;
            HLinkGlyphItem.SymbolColour = CommonRoutines.ResourceColourGet("CardBackGroundUtility");

            HLinkKey = Common.CustomClasses.HLinkKey.NewAsGUID();
        }

        public DateObjectModelStr DeRef
        {
            get;
            set;
        } = new DateObjectModelStr();

        public string Title
        {
            get; set;
        } = string.Empty;

        public override bool Valid => DeRef.Valid;

        public override async Task UCNavigate()
        {
            await UCNavigateBase(this, nameof(DateStrDetailPage));
            return;
        }

        //protected override IModelBase GetDeRef()
        //{
        //    return this.DeRef;
        //}
    }
}