

namespace GrampsView.Data.Model
{

    using GrampsView.Common;


    /// <summary>
    /// Public interfaces for the DateObject elements.
    /// </summary>
    public interface IDateObjectModelSpan : IDateObjectModel
    {
        string GCformat { get; }

        bool GDualdated { get; }

        string GNewYear { get; }

        CommonEnums.DateQuality GQuality { get; }

        string GStart { get; }

        string GStop { get; }
    }
}