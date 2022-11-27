// TODO Needs XML 1.71 check

using GrampsView.Models.DataModels;

namespace GrampsView.Data.Model
{
    /// <summary>
    /// GRAMPS Alt element class. TODO Update fields as per Schema
    /// </summary>
    /// TODO Update fields as per Schema
    public class SrcAttributeModel : ModelBase, ISourceAttributeModel
    {
        public SrcAttributeModel()
        {
        }

        /// <summary>
        /// Gets or sets the g text.
        /// </summary>
        /// <value>
        /// The g text.
        /// </value>
        public string GType
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the g value.
        /// </summary>
        /// <value>
        /// The g value.
        /// </value>
        public string GValue
        {
            get;
            set;
        }
    }
}