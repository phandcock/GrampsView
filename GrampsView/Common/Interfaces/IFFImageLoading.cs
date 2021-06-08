namespace GrampsView.Common
{
    using FFImageLoading.Cache;

    using System.Threading.Tasks;

    /// <summary>
    /// Implement interface to allow Unit Tetsing and Mocking
    /// </summary>
    public interface IFFImageLoading
    {
        Task InvalidateCacheAsync(CacheType argCacheType);
    }
}