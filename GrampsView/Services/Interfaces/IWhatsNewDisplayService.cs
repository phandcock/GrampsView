namespace GrampsView.Services
{
    using System.Threading.Tasks;

    public interface IWhatsNewDisplayService
    {
        Task<bool> ShowIfAppropriate();
    }
}