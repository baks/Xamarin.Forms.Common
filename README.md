Various things, which I've used for Xamarin.Forms.

# Examples

1. Disable item selection in ListView

```xml
<ContentView xmlns="http://xamarin.com/schemas/2014/forms"
    xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
    xmlns:behaviors="clr-namespace:Common.Forms"
    x:Class="SomeContentView">

<ListView>
    <ListView.Behaviors>
        <behaviors:DisableSelectionInListViewBehavior />
    </ListView.Behaviors>
</ListView>

</ContentView>
```

2. Show popup with XLabs.Forms PopupLayout help

```xml
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
    xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
    xmlns:behaviors="clr-namespace:Common.Forms"
    x:Class="SomeContentPage">
    <ContentPage.Behaviors>
        <PopupBehavior PopupController="{Binding PopupController}">
            <PopupBehavior.ContentView>
                <StackLayout>
                    <Label Text="I am a popup" />
                    <Button Text="Close popup" Binding="{Binding ClosePopupCommand}" />
                </StackLayout>
            </PopupBehavior.ContentView>
        </PopupBehavior>
    </ContentPage.Behaviors>
    <ContentPage.Content>
        <StackLayout>
            <Label Text="I am a content page" />
            <Button Text="Show popup" Command="{Binding ShowPopupCommand}" />
        </StackLayout>
    <ContentPage.Content>
</ContentPage>
```

```csharp
public class SomeContentPageViewModel
{
    public SomeContentPageViewModel()
    {
        ShowPopupCommand = new Command(ShowPopup);
        ClosePopupCommand = new Command(ClosePopup);
    }

    public IPopupController PopupController { get; set; }
    
    public ICommand ShowPopupCommand { get; set; }
    public ICommand ClosePopupCommand { get; set; }
    
    private void ShowPopup()
    {
        PopupController.ShowPopup();
    }
    
    private void ClosePopup()
    {
        PopupController.ClosePopup();
    }
}
```