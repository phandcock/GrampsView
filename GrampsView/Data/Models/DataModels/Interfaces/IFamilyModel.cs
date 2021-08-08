namespace GrampsView.Data.Model
{
    /// <summary>
    /// interface definitions.
    /// </summary>
    public interface IFamilyModel : IModelBase
    {
        /// <summary>
        /// Gets the get hlink Family Model that points to this ViewModel.
        /// </summary>
        /// <value>
        /// The get h link.
        /// </value>
        HLinkFamilyModel HLink
        {
            get;
        }
    }
}