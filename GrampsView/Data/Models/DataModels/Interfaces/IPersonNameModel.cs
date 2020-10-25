namespace GrampsView.Data.Model
{
    /// <summary>
    /// </summary>
    public interface IPersonNameModel : IModelBase
    {
        string FirstFirstName { get; }

        DateObjectModel GDate { get; set; }

        string GDisplay { get; set; }

        new string GetDefaultText { get; }

        string GTitle { get; }

        string GType { get; }

        HLinkPersonNameModel HLink { get; }

        string SortName { get; }

        /// <summary>
        /// Compares the specified a.
        /// </summary>
        /// <param name="a">
        /// a.
        /// </param>
        /// <param name="b">
        /// The b.
        /// </param>
        /// <returns>
        /// </returns>
        int Compare(object a, object b);

        /// <summary>
        /// Compares to.
        /// </summary>
        /// <param name="argObj">
        /// The object.
        /// </param>
        /// <returns>
        /// </returns>
        int CompareTo(object argObj);
    }
}