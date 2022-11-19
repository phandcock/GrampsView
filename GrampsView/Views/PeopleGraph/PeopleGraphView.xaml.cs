namespace GrampsView.Views
{
    using GrampsView.ViewModels.PeopleGraph;

    using Microsoft.Extensions.DependencyInjection;

    using SkiaSharp.Views.Forms;

    public sealed partial class PeopleGraphPage : ViewBasePage
    {
        public PeopleGraphPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = Ioc.Default.GetService<PeopleGraphViewModel>();
        }

        private PeopleGraphViewModel _viewModel { get; set; }

        private void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            _viewModel.OnCanvasViewPaintSurface(sender, args);
        }

        /// <summary>
        /// Handles the Loaded event of the GraphViewerPage control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="RoutedEventArgs"/> instance containing the event data.
        /// </param>
        //private void GraphViewerPage_Loaded(object sender, RoutedEventArgs e)
        //{
        //    // Draw the nodes
        //    for (int i = 0; i < ViewModel.TreeGraph.Count; i++)
        //    {
        //        PeopleGraphNode item = ViewModel.TreeGraph[i];

        // // Assume person PersonModel t = DV.PersonDV.GetModel(item.nodeHLink.HLinkKey);

        // if (t.HLink.Valid == true) { PersonCardSmall tt = new PersonCardSmall { DataContext = t,
        // }; theGraph.Children.Add(tt);

        // tt.SetValue(Canvas.LeftProperty, item.xStart); tt.SetValue(Canvas.TopProperty,
        // item.yStart); } else { // Assume Family FamilyModel tf =
        // DV.FamilyDV.GetModel(item.nodeHLink.HLinkKey); FamilyCardSmall tt = new FamilyCardSmall {
        // DataContext = tf, };

        // theGraph.Children.Add(tt);

        // tt.SetValue(Canvas.LeftProperty, item.xStart); tt.SetValue(Canvas.TopProperty,
        // item.yStart); } }

        //    // Draw the edges
        //    for (int i = 0; i < ViewModel.Edges.Count; i++)
        //    {
        //        theGraph.Children.Add(ViewModel.Edges[i].TheLine);
        //    }
        //}
    }
}