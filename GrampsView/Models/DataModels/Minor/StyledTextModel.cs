// Copyright (c) phandcock.  All rights reserved.


using GrampsView.Common;

using SharedSharp.Common.Interfaces;

using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace GrampsView.Data.Model
{
    /// <summary>
    /// Styled Text model collection.
    /// </summary>

    [KnownType(typeof(StyledTextModel))]
    public class StyledTextModel : ObservableObject, IStyledTextModel
    {
        private string _GText = string.Empty;

        private FormattedString _TextFormatted = new();

        public StyledTextModel()
        {
        }

        public string GText
        {
            get => _GText;

            set => SetProperty(ref _GText, value);
        }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        public ObservableCollection<GrampsStyle> Styles { get; } = new ObservableCollection<GrampsStyle>();

        public FormattedString TextFormatted
        {
            get
            {
                if (_TextFormatted.Spans.Count == 0)
                {
                    _TextFormatted = GrampsTextToXamarinText.GetFormattedString(this, Ioc.Default.GetRequiredService<ISharedSharpFontSizes>().FontMedium);
                }

                return _TextFormatted;
            }
        }

        /// <summary>
        /// Gets the shortened form of the text. Maximum length is 100.
        /// </summary>
        /// <value>
        /// The text short.
        /// </value>
        public string TextShort => GText[..Math.Min(GText.Length, 100)];
    }
}