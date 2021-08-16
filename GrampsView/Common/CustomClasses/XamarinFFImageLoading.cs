namespace GrampsView.Common
{
    using FFImageLoading;
    using FFImageLoading.Cache;

    using System.Threading.Tasks;

    public class XamarinFFImageLoading : IFFImageLoading
    {
        public async Task InvalidateCacheAsync(CacheType argCacheType)
        {
            await ImageService.Instance.InvalidateCacheAsync(CacheType.All).ConfigureAwait(false);
        }
    }
}