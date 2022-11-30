using SharedSharp.Common.Interfaces;

namespace GrampsView.UserControls
{


    public partial class ListLineCardSmall : Grid
    {
        public ListLineCardSmall()
        {
            InitializeComponent();

            CardSmallHeight = Ioc.Default.GetService<ISharedSharpCardSizes>().CardSmallHeight;
        }

        public double CardSmallHeight { get; set; } = 100;
    }
}