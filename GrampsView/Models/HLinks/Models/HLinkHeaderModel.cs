namespace GrampsView.Data.Model
{
    using GrampsView.Data.DataView;
    using GrampsView.Models.HLinks;

    /// <summary>
    /// HLink to the Header model.
    ///
    /// XML 1.71 check done
    /// </summary>

    public class HLinkHeaderModel : HLinkBase, IHLinkHeaderModel
    {
        private HeaderModel _Deref = new HeaderModel();

        private bool DeRefCached = false;

        public HLinkHeaderModel()
        {
            HLinkGlyphItem.Symbol = Common.Constants.IconHeader;
        }

        /// <summary>
        /// Gets the dereference.
        /// </summary>
        /// <value>
        /// The de reference.
        /// </value>

        public HeaderModel DeRef
        {
            get
            {
                if (Valid && (!DeRefCached))
                {
                    _Deref = DV.HeaderDV.GetModelFromHLinkKey(HLinkKey);

                    if (_Deref.Valid)
                    {
                        DeRefCached = true;
                    }
                }

                return _Deref;
            }
        }
    }
}