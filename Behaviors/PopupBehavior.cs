using System;
using XLabs.Forms.Controls
using Xamarin.Forms;

namespace Common.Behaviors
{
    public interface IPopupController
    {
        void ShowPopup();
        void DismissPopup();
    }

    public class PopupBehavior : Behavior<Page>, IPopupController
    {
        private static readonly BindablePropertyKey PopupControllerPropertyKey =
            BindableProperty.CreateReadOnly<PopupBehavior, IPopupController>(
                popupBehavior => popupBehavior.PopupController,
                null,
                BindingMode.OneWayToSource
                );

        public static readonly BindableProperty PopupControllerProperty =
            PopupControllerPropertyKey.BindableProperty;

        private readonly PopupLayout popupLayout;

        public PopupBehavior()
        {
            popupLayout = new PopupLayout();
            PopupController = this;
        }

        public IPopupController PopupController
        {
            get { return (IPopupController) GetValue(PopupControllerProperty); }
            private set { SetValue(PopupControllerPropertyKey, value); }
        }

        public ContentView ContentView { get; set; }

        public void ShowPopup()
        {          
            popupLayout.ShowPopup(ContentView);
        }

        public void DismissPopup()
        {
            popupLayout.DismissPopup();
        }

        protected override void OnAttachedTo(Page bindable)
        {
            base.OnAttachedTo(bindable);
            this.BindingContext = bindable.BindingContext;
            bindable.Appearing += OnPageAppearing;
        }

        protected override void OnDetachingFrom(Page bindable)
        {
            base.OnDetachingFrom(bindable);
            this.BindingContext = null;
            bindable.Appearing -= OnPageAppearing;
        }

        private void OnPageAppearing(object sender, EventArgs e)
        {
            var page = (ContentPage) sender;
            popupLayout.Content = page.Content;
            page.Content = popupLayout;
        }
    }
}