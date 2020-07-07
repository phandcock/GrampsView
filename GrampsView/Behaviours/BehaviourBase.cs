namespace GrampsView.Behaviours
{
    using System;

    using Xamarin.Forms;

    public class BehaviorBase<T> : Behavior<T> where T : BindableObject
    {
        public T AssociatedObject { get; private set; }

        protected override void OnAttachedTo(T argBindable)
        {
            if (argBindable == null)
            {
                throw new ArgumentNullException(nameof(argBindable));
            }

            base.OnAttachedTo(argBindable);
            AssociatedObject = argBindable;

            if (argBindable.BindingContext != null)
            {
                BindingContext = argBindable.BindingContext;
            }

            argBindable.BindingContextChanged += OnBindingContextChanged;
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            BindingContext = AssociatedObject.BindingContext;
        }

        protected override void OnDetachingFrom(T argBindable)
        {
            if (argBindable == null)
            {
                throw new ArgumentNullException(nameof(argBindable));
            }

            base.OnDetachingFrom(argBindable);
            argBindable.BindingContextChanged -= OnBindingContextChanged;
            AssociatedObject = null;
        }

        private void OnBindingContextChanged(object sender, EventArgs e)
        {
            OnBindingContextChanged();
        }
    }
}