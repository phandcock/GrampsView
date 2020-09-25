namespace GrampsView.Data.Model
{
    /// <summary>
    /// Public interfaces for the Event elements.
    /// </summary>
    public interface IHLinkMediaModel : IHLinkBase
    {
        IMediaModel DeRef { get; }

    
    }
}