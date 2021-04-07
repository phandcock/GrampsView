namespace GrampsView.Services
{
    using System.Threading.Tasks;

    /// <summary>
    /// Display the database reload view if required.
    /// </summary>
    public interface IDatabaseReloadDisplayService
    {
        Task<bool> ShowIfAppropriate();
    }
}