namespace GrampsView.Data.Model
{
    using GrampsView.Common;

    /// <summary>
    /// Date Object Val type Interface
    /// </summary>
    public interface IDateObjectModelVal : IDateObjectModel
    {
        string GCformat { get; }

        bool GDualdated { get; }

        string GNewYear { get; }

        CommonEnums.DateQuality GQuality { get; }

        string GVal { get; }
    }
}