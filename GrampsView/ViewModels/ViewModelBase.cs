// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Common;
using GrampsView.Data.Model;
using GrampsView.DBModels;
using GrampsView.Models.DataModels;
using GrampsView.Models.DBModels.Interfaces;
using GrampsView.Models.HLinks;

using PropertyChanged;

using SharedSharp.Common.CustomClasses;
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

        /// <summary>Gets or sets the base detail.</summary>
        /// <value>The base detail.</value>
        public SharedSharpObservableRangeCollection<object> BaseDetail
        {
            get; set;
        }

        = new SharedSharpObservableRangeCollection<object>();

        public IModelBase BaseModelBase { get; set; } = new ModelBase();

        public IDBModelBase BaseDBModelBase { get; set; } = new DBModelBase();

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

        public IHLinkDBBase DBNavigationParameter { get; set; } = new HLinkDBBase();
        public IHLinkBase NavigationParameter { get; set; } = new HLinkBase();
        public CardListLineCollection StandardDetails { get; set; } = new CardListLineCollection();

        public void HandleParameter(IHLinkBase argHLinkBase)
        {
            NavigationParameter = argHLinkBase;

            HandleViewModelParameters();
        }

        public void HandleParameter(IHLinkDBBase argHLinkBase)
        {
            DBNavigationParameter = argHLinkBase;

            HandleViewModelParameters();
        }

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
                BaseTitle = CommonRoutines.ReplaceLineSeparators(BaseTitle);

                BaseTitle = BaseTitle[..(BaseTitle.Length > 50 ? 50 : BaseTitle.Length)];
            }
        }
    }
}