namespace GrampsView.Data.Repository
{
    /// <summary>Interfaces for Base Repository.</summary>
    /// <typeparam name="T">Data ViewModel.</typeparam>
    /// <typeparam name="TU"></typeparam>
    public interface IRepositoryModelDictionary<out T, TU>
    {
        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <value>
        /// The count.
        /// </value>
        int Count { get; }


        T this[string key] { get; }


        T this[TU hLink] { get; }

        /// <summary>
        /// Clear the Repository Data.
        /// </summary>
        void Clear();


        T Find(string key);

        /// <summary>
        /// Randoms the item.
        /// </summary>
        /// <returns>
        /// </returns>
        T GetRandomItem();
    }
}