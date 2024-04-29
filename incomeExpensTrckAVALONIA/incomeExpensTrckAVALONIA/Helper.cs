//using Avalonia.Controls;
//using Avalonia.VisualTree;

//namespace incomeExpensTrckAVALONIA
//{
//    public static class Helper
//    {
//        public static void SetCarouselToTabControl(Carousel carousel, bool v)
//        {
//            var tabControl = carousel.FindAncestorOfType<TabControl>();
//            if (tabControl == null)
//            {
//                carousel.AttachedToVisualTree += delegate { SetCarouselToTabControl(carousel, true); };
//                return;
//            }

//            carousel.ItemsSource = tabControl.Items;
//        }
//    }
//}
