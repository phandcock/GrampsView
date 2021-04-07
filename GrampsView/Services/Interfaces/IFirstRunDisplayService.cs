namespace GrampsView.Services
{
    using System.Threading.Tasks;

    public interface IFirstRunDisplayService
    {
        Task<bool> ShowIfAppropriate();
    }
}