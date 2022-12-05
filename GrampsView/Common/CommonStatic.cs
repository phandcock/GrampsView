using SharedSharp.Common.Interfaces;

namespace GrampsView.Common
{
    public static class CommonStatic
    {
        public static ISharedSharpCardSizes CardSizes { get; } = Ioc.Default.GetRequiredService<ISharedSharpCardSizes>();
    }
}