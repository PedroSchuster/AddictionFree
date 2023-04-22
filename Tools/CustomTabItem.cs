using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Syncfusion.Maui.TabView;

namespace AddictionApp.Tools
{
    public class CustomTabItem : SfTabItem
    {
      
        public static readonly BindableProperty PageProperty =
     BindableProperty.Create(nameof(Page), typeof(ContentPage), typeof(CustomTabItem), null, propertyChanged: OnPagePropertyChanged);

       
        public CustomTabItem()
        {
        }

        public ContentPage Page
        {
            get => (ContentPage)GetValue(PageProperty);
            set => SetValue(PageProperty, value);
        }

        private static void OnPagePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var page = bindable as CustomTabItem;
            if (page != null)
            {
                page.Content = (newValue as ContentPage).Content;
            }
        }
    }

}
