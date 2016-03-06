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

    public class PopupBehavior : Behavior<ContentPage>, IPopupController
    {
        private const string ContentPropertyName = "Content";

        private static readonly BindablePropertyKey PopupControllerPropertyKey =
            BindableProperty.CreateReadOnly<PopupBehavior, IPopupController>(
                popupBehavior => popupBehavior.PopupController,
                null,
                BindingMode.OneWayToSource
                );

        public static readonly BindableProperty PopupControllerProperty =
            PopupControllerPropertyKey.BindableProperty;

        private readonly PopupLayout popupLayout;

        private bool initialized;

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

        protected override void OnAttachedTo(ContentPage bindable)
        {
            base.OnAttachedTo(bindable);
            this.BindingContext = bindable.BindingContext;
            bindable.PropertyChanged += OnPropertyChanged;
        }

        protected override void OnDetachingFrom(ContentPage bindable)
        {
            base.OnDetachingFrom(bindable);
            this.BindingContext = null;
            bindable.PropertyChanged -= OnPropertyChanged;
        }
		
		private void OnPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (initialized == false && string.Equals(e.PropertyName, ContentPropertyName, StringComparison.OrdinalIgnoreCase))
            {
                initialized = true;
                var page = (ContentPage)sender;
                popupLayout.Content = page.Content;
                page.Content = popupLayout;
            }
        }
    }
}