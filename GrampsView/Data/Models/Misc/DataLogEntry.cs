namespace GrampsView.Common
{
    /// <summary>
    /// Defines format of Data Log Entries.
    /// </summary>
    public class DataLogEntry : CommonBindableBase, System.IEquatable<DataLogEntry>
    {
        private string _Label;

        private string _Text;

        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        /// <value>
        /// The label.
        /// </value>
        public string Label
        {
            get
            {
                return _Label;
            }

            set
            {
                SetProperty(ref _Label, value);
            }
        }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        public string Text
        {
            get
            {
                return _Text;
            }

            set
            {
                SetProperty(ref _Text, value);
            }
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