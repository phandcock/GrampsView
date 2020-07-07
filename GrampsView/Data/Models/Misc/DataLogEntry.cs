namespace GrampsView.Common
{
    /// <summary>
    /// Defines format of Data Log Entries.
    /// </summary>
    public struct DataLogEntry : System.IEquatable<DataLogEntry>
    {
        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        /// <value>
        /// The label.
        /// </value>
        public string Label
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        public string Text
        {
            get; set;
        }

        public static bool operator !=(DataLogEntry left, DataLogEntry right)
        {
            return !(left == right);
        }

        public static bool operator ==(DataLogEntry left, DataLogEntry right)
        {
            return left.Equals(right);
        }

        public override bool Equals(object obj)
        {
            if (obj is null)
            {
                return false;
            }

            if (obj.GetType() != this.GetType())
            {
                return false;
            }

            if (this.Label == ((DataLogEntry)obj).Label)
            {
                return true;
            }

            return false;
        }

        public bool Equals(DataLogEntry other)
        {
            if ((this.Label + this.Text) == (other.Label + other.Text))
            {
                return true;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return this.Label.GetHashCode() + this.Text.GetHashCode();
        }
    }
}