namespace GrampsView.Data.Model
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics.Contracts;

    /// <summary>
    /// </summary>

    public class CardListLine : INotifyPropertyChanged
    {
        private string localLabel = string.Empty;

        private string localValue = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="CardListLine"/> class.
        /// </summary>
        /// <param name="argLabel">
        /// The argument label.
        /// </param>
        /// <param name="argValue">
        /// The argument value.
        /// </param>
        public CardListLine(string argLabel, string argValue)
        {
            Contract.Assert(argLabel != null);

            if (argValue != null)
            {
                Label = argLabel.Trim();
                Value = argValue.Trim();
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CardListLine"/> class.
        /// </summary>
        /// <param name="LabelArg">
        /// The label argument.
        /// </param>
        /// <param name="ValueArg">
        /// if set to <c> true </c> [value argument].
        /// </param>
        public CardListLine(string argLabel, bool argValue)
        {
            Contract.Assert(argLabel != null);

            Label = argLabel.Trim();
            Value = argValue.ToString(System.Globalization.CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CardListLine"/> class.
        /// </summary>
        /// <param name="argLabel">
        /// The argument label.
        /// </param>
        /// <param name="argValue">
        /// if set to <c> true </c> [argument value].
        /// </param>
        /// <param name="argShowIf">
        /// Show if the value equals this vlaue.
        /// </param>
        public CardListLine(string argLabel, bool argValue, bool argShowIf)
        {
            Contract.Assert(argLabel != null);

            if (argValue == argShowIf)
            {
                Label = argLabel.Trim();
                Value = argValue.ToString(System.Globalization.CultureInfo.CurrentCulture);
            }
        }

        public CardListLine(string argLabel, string argValue, bool argShowIf)
        {
            Contract.Assert(argLabel != null);

            if (argShowIf)
            {
                Label = argLabel.Trim();
                Value = argValue.Trim();
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CardListLine"/> class.
        /// </summary>
        /// <param name="LabelArg">
        /// The label argument.
        /// </param>
        /// <param name="ValueArg">
        /// if set to <c> true </c> [value argument].
        /// </param>
        public CardListLine(string argLabel, int ValueArg)
        {
            Contract.Assert(argLabel != null);

            Label = argLabel.Trim();
            Value = $"{ValueArg:n0}";
        }

        public CardListLine(string argLabel, double ValueArg)
        {
            Contract.Assert(argLabel != null);

            Label = argLabel.Trim();
            Value = ValueArg.ToString(System.Globalization.CultureInfo.CurrentCulture);
        }

        public CardListLine(string argLabel, Nullable<int> ValueArg)
        {
            Contract.Assert(argLabel != null);

            if (ValueArg != null)
            {
                Label = argLabel.Trim();
                Value = ValueArg.HasValue ? ValueArg.Value.ToString(System.Globalization.CultureInfo.CurrentCulture) : string.Empty;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

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
                return localLabel;
            }

            set
            {
                localLabel = value;
                NotifyPropertyChanged(nameof(Label));
            }
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>

        public string Value
        {
            get
            {
                return localValue;
            }

            set
            {
                localValue = value;
                NotifyPropertyChanged(nameof(Value));
            }
        }

        /// <summary>
        /// Notify property changed event.
        /// </summary>
        /// <param name="p">
        /// The property name.
        /// </param>
        private void NotifyPropertyChanged(string p)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(p));
        }
    }
}