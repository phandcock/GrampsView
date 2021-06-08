namespace GrampsView.Common
{
    using FFImageLoading;
    using FFImageLoading.Cache;

    using System.Threading.Tasks;

    public class XamarinIFFImageLoading : IFFImageLoading
    {
        public async Task InvalidateCacheAsync(CacheType argCacheType)
        {
            await ImageService.Instance.InvalidateCacheAsync(CacheType.All).ConfigureAwait(false);
        }
    }
}