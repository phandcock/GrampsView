using GrampsView.Common;

using System;

using Xamarin.CommunityToolkit.ObjectModel;

namespace GrampsView.Data.Repository
{
    /// <summary>
    /// Static Data Store.
    /// </summary>

    public sealed class DataStore : ObservableObject

    {
        private static readonly Lazy<DataStore> lazy = new Lazy<DataStore>(() => new DataStore());

        private DataStore()
        {
        }

        public static DataStore Instance
        {
            get
            {
                return lazy.Value;
            }
        }

        /// <summary>
        /// Gets or sets the Application Wide Data Store.
        /// </summary>
        /// <value>
        /// The ad.
        /// </value>
        public ApplicationWideData AD { get; set; } = new ApplicationWideData();

        /// <summary>
        /// Gets or sets the cn.
        /// </summary>
        /// <value>
        /// The cn.
        /// </value>
        public ICommonNotifications CN
        {
            get; set;
        }

        /// <summary>
        /// Gets the Data Store.
        /// </summary>
        /// <value>
        /// The DataStore.Instance.
        /// </value>
        public DataInstance DS { get; } = new DataInstance();

        public IXamarinEssentials ES
        {
            get; set;
        } = new XamarinEssentials();

        public IFFImageLoading FFIL
        {
            get; set;
        } = new XamarinIFFImageLoading();
    }
}