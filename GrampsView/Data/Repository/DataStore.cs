using GrampsView.Common;

using System;

namespace GrampsView.Data.Repository
{
    /// <summary>
    /// Static Data Store.
    /// </summary>

    public sealed class DataStore : CommonBindableBase

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
        public ICommonNotifications CN { get; set; }

        /// <summary>
        /// Gets the Data Store.
        /// </summary>
        /// <value>
        /// The DataStore.Instance.
        /// </value>
        public DataInstance DS { get; } = new DataInstance();

        public NavCmd NV { get; set; } = new NavCmd();
    }
}