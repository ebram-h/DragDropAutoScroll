using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DragDropAutoScroll
{
    public class DragDropExtension : DependencyObject
    {
        public static readonly DependencyProperty AutoScrollWhileDraggingProperty =
            DependencyProperty.RegisterAttached("AutoScrollWhileDragging",
                typeof(bool), typeof(DragDropExtension),
                new PropertyMetadata(false, autoScrollWhileDraggingPropertyChanged));
        public static bool GetAutoScrollWhileDragging(DependencyObject obj) =>
            (bool)obj.GetValue(AutoScrollWhileDraggingProperty);
        public static void SetAutoScrollWhileDragging(DependencyObject obj, bool value)
        {
            obj.SetValue(AutoScrollWhileDraggingProperty, value);
        }

        private static void autoScrollWhileDraggingPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var container = d as FrameworkElement;

            container.PreviewDragOver -= container_PreviewDragOver;

            if ((bool)e.NewValue)
                container.PreviewDragOver += container_PreviewDragOver;
        }

        private static void container_PreviewDragOver(object sender, DragEventArgs e)
        {
            if (!(sender is FrameworkElement container))
                return;

            var scrollViewer = findChildOfType<ScrollViewer>(container);
            if (scrollViewer == null)
                return;

            const double heightOfAutoScrollZone = 25;
            double mouseYRelativeToContainer = e.GetPosition(container).Y;

            if (mouseYRelativeToContainer < heightOfAutoScrollZone)
            {
                double offsetChange = heightOfAutoScrollZone - mouseYRelativeToContainer;
                scrollViewer.ScrollToVerticalOffset(scrollViewer.VerticalOffset - offsetChange);
            }
            else if (mouseYRelativeToContainer > container.ActualHeight - heightOfAutoScrollZone)
            {
                double offsetChange = mouseYRelativeToContainer - (container.ActualHeight - heightOfAutoScrollZone);
                scrollViewer.ScrollToVerticalOffset(scrollViewer.VerticalOffset + offsetChange);
            }
        }

        private static TChild findChildOfType<TChild>(DependencyObject parent) where TChild : DependencyObject
        {
            if (parent == null) return null;

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);

                var result = (child as TChild) ?? findChildOfType<TChild>(child);
                if (result != null) return result;
            }
            return null;
        }
    }
}
