namespace GrampsView.Data.Model
{
    /// <summary>
    /// GRAMPS Alt element class.
    /// </summary>
    /// TODO Update fields as per Schema
    public class AltModel : ModelBase, IAltModel
    {
        public AltModel()
        {
        }

        /// <summary>
        /// turn the string 0 or 1 into true or false.
        /// </summary>
        /// <param name="altString">
        /// returns the string version of the Alt value.
        /// </param>
        public AltModel(string altString)
        {
            switch (altString)
            {
                case "0":
                    {
                        GAlt = false;
                        break;
                    }

                case "1":
                    {
                        GAlt = true;
                        break;
                    }

                default:
                    {
                        GAlt = false;
                        break;
                    }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether Alt is set.
        /// </summary>
        public bool GAlt
        {
            get;

            set;
        }
    }
}