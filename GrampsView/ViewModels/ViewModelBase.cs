// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Common;
using GrampsView.Data.Model;
using GrampsView.Models.DataModels;

using PropertyChanged;

using SharedSharp.ViewModels;

using System.ComponentModel;
using System.Diagnostics;

namespace GrampsView.ViewModels
{
    public class ViewModelBase : SharedSharpViewModelBase, INotifyPropertyChanged
    {
        private string _BaseTitle = string.Empty;

        public ViewModelBase(ILog iocCommonLogging)
        {
            BaseCL = iocCommonLogging;
        }

        /// <summary>Initializes a new instance of the <see cref="ViewModelBase" /> class.</summary>
        /// <param name="iocCommonLogging">The ioc common logging.</param>
        /// <summary>
        /// Gets the base detail.
        /// </summary>
        /// <value>
        /// The base detail.
        /// </value>
        public SharedSharpObservableRangeCollection<object> BaseDetail
        {
            get; set;
        }

        = new SharedSharpObservableRangeCollection<object>();

        public IModelBase BaseModelBase { get; set; } = new ModelBase();

        /// <summary>
        /// Gets or sets the base title.
        /// </summary>
        /// <value>
        /// The base title.
        /// </value>
        public override string BaseTitle
        {
            get => !string.IsNullOrEmpty(_BaseTitle) ? _BaseTitle : BaseModelBase.Valid ? BaseModelBase.DefaultTextShort : string.Empty;
            set => SetProperty(ref _BaseTitle, value);
        }

        public CardListLineCollection StandardDetails { get; set; } = new CardListLineCollection();

        /// <summary>
        /// Called when [basecl changed]. Frody automatically wires this up.
        /// </summary>
        [SuppressPropertyChangedWarnings]
        private void OnBaseCLChanged()
        {
            Debug.Assert(BaseCL != null, "BaseCL is null.  Was this set in the constructor for the derived class?");
        }

        /// <summary>
        /// Called when [basetitlechanged]. Frody automatically wires this up.
        /// </summary>
        private void OnBaseTitleChanged()
        {
            if (!(BaseTitle == null))
            {
                BaseTitle = CommonRoutines.ReplaceLineSeperators(BaseTitle);

                BaseTitle = BaseTitle[..(BaseTitle.Length > 50 ? 50 : BaseTitle.Length)];
            }
        }
    }
}