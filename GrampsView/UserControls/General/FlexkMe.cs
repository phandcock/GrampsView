using Microsoft.Maui.Controls.Shapes;

namespace GrampsView.UserControls
{
    public class FlexMe : FlexLayout
    {
        public FlexMe()
        {
            Add(new Border()
            {
                Stroke = Color.FromArgb("#C49B33"),
                Background = Color.FromArgb("#2B0B98"),
                StrokeThickness = 4,
                Padding = new Thickness(16, 8),
                HorizontalOptions = LayoutOptions.Center,
                StrokeShape = new RoundRectangle
                {
                    CornerRadius = new CornerRadius(40, 0, 0, 40)
                },
                Content = new Label
                {
                    Text = ".NET MAUI",
                    TextColor = Colors.White,
                    FontSize = 18,
                    FontAttributes = FontAttributes.Bold
                }
            });
            ;
            ;
        }
    }
}