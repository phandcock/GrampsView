namespace GrampsView.UserControls
{
    using Xamarin.Forms;

    internal struct LayoutData
    {
        public LayoutData(int visibleChildCount, Size cellSize, int rows, int columns)
        {
            VisibleChildCount = visibleChildCount;
            CellSize = cellSize;
            Rows = rows;
            Columns = columns;
        }

        public Size CellSize { get; private set; }
        public int Columns { get; private set; }
        public int Rows { get; private set; }
        public int VisibleChildCount { get; private set; }
    }
}