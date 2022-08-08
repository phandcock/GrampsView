// TODO Needs XML 1.71 check

namespace GrampsView.Data.Model
{
    /// <summary>
    /// GRAMPS Surname element class.
    ///
    /// XML 1.71 all done
    /// </summary>
    public class SurnameModel : ModelBase, IURLModel
    {
        public SurnameModel()
        {
            ModelItemGlyph.Symbol = Common.Constants.IconSurname;
        }

        public string GConnector
        {
            get;
            set;
        }

        public string GDerivation
        {
            get;
            set;
        }

        public string GPrefix
        {
            get;
            set;
        }

        public bool GPrim
        {
            get;
            set;
        }

        public string GText
        {
            get;
            set;
        }

        public override bool Valid
        {
            get
            {
                return !string.IsNullOrEmpty(ToString());
            }
        }

        public override string ToString()
        {
            return GText;
        }
    }
}