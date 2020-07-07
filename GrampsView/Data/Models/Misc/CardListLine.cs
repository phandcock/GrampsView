// <copyright file="CardListLine.cs" company="PlaceholderCompany">
//     Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

/// <summary>
/// Common routines
/// </summary>
namespace GrampsView.Data.Model
{
    using System.ComponentModel;
    using System.Runtime.Serialization;

    /// <summary>
    /// </summary>
    [DataContract]
    public class CardListLine : INotifyPropertyChanged
    {
        private string localLabel = string.Empty;

        private string localValue = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="CardListLine"/> class.
        /// </summary>
        /// <param name="LabelArg">
        /// The label argument.
        /// </param>
        /// <param name="ValueArg">
        /// The value argument.
        /// </param>
        public CardListLine(string LabelArg, string ValueArg)
        {
            Label = LabelArg;
            Value = ValueArg;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CardListLine"/> class.
        /// </summary>
        /// <param name="LabelArg">
        /// The label argument.
        /// </param>
        /// <param name="ValueArg">
        /// if set to <c>true</c> [value argument].
        /// </param>
        public CardListLine(string LabelArg, bool ValueArg)
        {
            Label = LabelArg;
            Value = ValueArg.ToString(System.Globalization.CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CardListLine"/> class.
        /// </summary>
        /// <param name="LabelArg">
        /// The label argument.
        /// </param>
        /// <param name="ValueArg">
        /// if set to <c>true</c> [value argument].
        /// </param>
        public CardListLine(string LabelArg, int ValueArg)
        {
            Label = LabelArg;
            Value = ValueArg.ToString(System.Globalization.CultureInfo.CurrentCulture);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        /// <value>
        /// The label.
        /// </value>
        [DataMember]
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
        [DataMember]
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