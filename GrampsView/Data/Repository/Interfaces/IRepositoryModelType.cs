namespace GrampsView.Data.Repositories
{
    /// <summary>
    /// Interfaces for Base Repository.
    /// </summary>
    /// <typeparam name="T">
    /// Data ViewModel.
    /// </typeparam>
    /// <typeparam name="U">
    /// $$(HLink)$$ ViewModel.
    /// </typeparam>
    public interface IRepositoryModelDictionary<out T, TU>
    {
        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <value>
        /// The count.
        /// </value>
        int Count { get; }

        /// <summary>
        /// Gets the <see cref="T"/> with the specified key.
        /// </summary>
        /// <value>
        /// The <see cref="T"/>.
        /// </value>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <returns>
        /// </returns>
        T this[string key] { get; }

        /// <summary>
        /// Gets the <see cref="T"/> with the specified key.
        /// </summary>
        /// <value>
        /// The <see cref="T"/>.
        /// </value>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <returns>
        /// </returns>
        T this[TU hLink] { get; }

        /// <summary>
        /// Clear the Repository Data.
        /// </summary>
        void Clear();

        /// <summary>
        /// Gets or sets the <see cref="T"/> with the specified key.
        /// </summary>
        /// <value>
        /// The <see cref="T"/>.
        /// </value>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <returns>
        /// </returns>
        T Find(string key);

        /// <summary>
        /// Randoms the item.
        /// </summary>
        /// <returns>
        /// </returns>
        T GetRandomItem();
    }
}