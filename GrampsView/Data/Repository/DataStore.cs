using CommunityToolkit.Mvvm.ComponentModel;

using GrampsView.Common;

using System;

namespace GrampsView.Data.Repository
{
    /// <summary>
    /// Static Data Store.
    /// </summary>

    public sealed class DataStore : ObservableObject

    {
        private static readonly Lazy<DataStore> lazy = new(() => new DataStore());

        private DataStore()
        {
        }

        public static DataStore Instance => lazy.Value;

        /// <summary>
        /// Gets or sets the Application Wide Data Store.
        /// </summary>
        /// <value>
        /// The ad.
        /// </value>
        public ApplicationWideData AD { get; set; } = new ApplicationWideData();

        /// <summary>
        /// Gets the Data Store.
        /// </summary>
        /// <value>
        /// The DataStore.Instance.
        /// </value>
        public DataInstance DS { get; } = new DataInstance();

        //public IXamarinEssentials ES
        //{
        //    get; set;
        //} = new XamarinEssentials();

        //public IFFImageLoading FFIL
        //{
        //    get; set;
        //} = new XamarinFFImageLoading();
    }
}