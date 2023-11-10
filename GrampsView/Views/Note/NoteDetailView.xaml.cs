// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Data.Model;
using GrampsView.ViewModels.Note;

namespace GrampsView.Views
{
    public partial class NoteDetailPage : ViewBasePage
    {
        public NoteDetailPage()
        {
            InitializeComponent();
        }

        public NoteDetailPage(IHLinkDBBase argHLinkKey)
        {
            InitializeComponent();

            _viewModel = Ioc.Default.GetRequiredService<NoteDetailViewModel>();

            _viewModel.HandleParameter(argHLinkKey);

            BindingContext = _viewModel;
        }

        private NoteDetailViewModel _viewModel { get; set; }
    }
}