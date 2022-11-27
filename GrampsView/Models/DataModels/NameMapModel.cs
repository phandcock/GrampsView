// TODO Needs XML 1.71 check

namespace GrampsView.Data.Model
{
    using GrampsView.Common;
    using GrampsView.Models.DataModels;

    using System;
    using System.Collections;

    /// TODO Update fields as per Schema
    /// <summary>
    /// </summary>
    /// <seealso cref="Models.DataModels.ModelBase"/>
    /// <seealso cref="GrampsView.Data.Model.INameMapModel"/>
    /// <seealso cref="System.IComparable"/>
    /// <seealso cref="System.Collections.IComparer"/>
    // </code> </summary>

    public sealed class NameMapModel : ModelBase, INameMapModel, IComparable, IComparer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NameMapModel"/> class.
        /// </summary>
        public NameMapModel()
        {
            ModelItemGlyph.Symbol = Constants.IconNameMaps;
            ModelItemGlyph.SymbolColour = CommonRoutines.ResourceColourGet("CardBackGroundNameMap");
        }

        /// <summary>
        /// Gets the get h link.
        /// </summary>
        /// <value>
        /// The get h link.
        /// </value>
        public HLinkNameMapModel HLink
        {
            get
            {
                HLinkNameMapModel t = new HLinkNameMapModel
                {
                    HLinkKey = HLinkKey,
                    HLinkGlyphItem = ModelItemGlyph,
                };
                return t;
            }
        }

        /// <summary>
        /// Compares two objects.
        /// </summary>
        /// <param name="a">
        /// object A.
        /// </param>
        /// <param name="b">
        /// object B.
        /// </param>
        /// <returns>
        /// One, two or three.
        /// </returns>
        int IComparer.Compare(object a, object b)
        {
            // TagModel firstEvent = (TagModel)a; TagModel secondEvent = (TagModel)b;

            //// compare on Priority first
            // int testFlag = string.Compare(firstEvent.Name, secondEvent.Name, StringComparison.CurrentCulture);

            // return testFlag;
            return 0;
        }

        /// <summary>
        /// Implement IComparable CompareTo method.
        /// </summary>
        /// <param name="obj">
        /// The object to compare.
        /// </param>
        /// <returns>
        /// One, two or three.
        /// </returns>
        int IComparable.CompareTo(object obj)
        {
            // TagModel secondEvent = (TagModel)obj;

            //// compare on Name first
            // int testFlag = string.Compare(Name, secondEvent.Name, StringComparison.CurrentCulture);

            // return testFlag;
            return 0;
        }
    }
}