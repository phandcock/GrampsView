namespace GrampsView.Behaviours
{
    using System;

    using Xamarin.Forms;

    public class TextChangedBehavior : Behavior<SearchBar>
    {
        protected override void OnAttachedTo(SearchBar bindable)
        {
            if (bindable is null)
            {
                throw new ArgumentNullException(nameof(bindable));
            }

            base.OnAttachedTo(bindable);
            bindable.TextChanged += Bindable_TextChanged;
        }

        protected override void OnDetachingFrom(SearchBar bindable)
        {
            if (bindable is null)
            {
                throw new ArgumentNullException(nameof(bindable));
            }

            base.OnDetachingFrom(bindable);
            bindable.TextChanged -= Bindable_TextChanged;
        }

        private void Bindable_TextChanged(object sender, TextChangedEventArgs e)
        {
            ((SearchBar)sender).SearchCommand?.Execute(e.NewTextValue);
        }
    }
}