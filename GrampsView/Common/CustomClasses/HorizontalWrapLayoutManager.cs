// Copyright (c) phandcock.  All rights reserved.

using Microsoft.Maui.Layouts;

using StackLayoutManager = Microsoft.Maui.Layouts.StackLayoutManager;

namespace GrampsView.Common.CustomClasses
{
    public class HorizontalWrapLayoutManager : StackLayoutManager
    {
        private HorizontalWrapLayout _layout;

        public HorizontalWrapLayoutManager(HorizontalWrapLayout horizontalWrapLayout) : base(horizontalWrapLayout)
        {
            _layout = horizontalWrapLayout;
        }

        public override Size ArrangeChildren(Rect bounds)
        {
            Thickness padding = Stack.Padding;
            double top = padding.Top + bounds.Top;
            double left = padding.Left + bounds.Left;

            double currentRowTop = top;
            double currentX = left;
            double currentRowHeight = 0;

            double maxStackWidth = currentX;

            for (int n = 0; n < _layout.Count; n++)
            {
                IView child = _layout[n];

                if (child.Visibility == Visibility.Collapsed)
                {
                    continue;
                }

                if (currentX + child.DesiredSize.Width > bounds.Right)
                {
                    // Keep track of our maximum width so far
                    maxStackWidth = Math.Max(maxStackWidth, currentX);

                    // Move down to the next row
                    currentX = left;
                    currentRowTop += currentRowHeight + _layout.Spacing;
                    currentRowHeight = 0;
                }

                Rect destination = new Rect(currentX, currentRowTop, child.DesiredSize.Width, child.DesiredSize.Height);
                child.Arrange(destination);

                currentX += destination.Width + _layout.Spacing;
                currentRowHeight = Math.Max(currentRowHeight, destination.Height);
            }

            Size actual = new Size(maxStackWidth, currentRowTop + currentRowHeight);

            return actual.AdjustForFill(bounds, Stack);
        }

        public override Size Measure(double widthConstraint, double heightConstraint)
        {
            Thickness padding = _layout.Padding;

            widthConstraint -= padding.HorizontalThickness;

            double currentRowWidth = 0;
            double currentRowHeight = 0;
            double totalWidth = 0;
            double totalHeight = 0;

            for (int n = 0; n < _layout.Count; n++)
            {
                IView child = _layout[n];

                if (child.Visibility == Visibility.Collapsed)
                {
                    continue;
                }

                Size measure = child.Measure(double.PositiveInfinity, heightConstraint);

                // Will adding this IView put us past the edge?
                if (currentRowWidth + measure.Width > widthConstraint)
                {
                    // Keep track of the width so far
                    totalWidth = Math.Max(totalWidth, currentRowWidth);
                    totalHeight += currentRowHeight;

                    // Account for spacing
                    totalHeight += _layout.Spacing;

                    // Start over at 0
                    currentRowWidth = 0;
                    currentRowHeight = measure.Height;
                }
                else
                {
                    currentRowWidth += measure.Width;
                    currentRowHeight = Math.Max(currentRowHeight, measure.Height);

                    if (n < _layout.Count - 1)
                    {
                        currentRowWidth += _layout.Spacing;
                    }
                }
            }

            // Account for the last row
            totalWidth = Math.Max(totalWidth, currentRowWidth);
            totalHeight += currentRowHeight;

            // Account for padding
            totalWidth += padding.HorizontalThickness;
            totalHeight += padding.VerticalThickness;

            double finalHeight = ResolveConstraints(heightConstraint, Stack.Height, totalHeight, Stack.MinimumHeight, Stack.MaximumHeight);
            double finalWidth = ResolveConstraints(widthConstraint, Stack.Width, totalWidth, Stack.MinimumWidth, Stack.MaximumWidth);

            return new Size(finalWidth, finalHeight);
        }
    }
}