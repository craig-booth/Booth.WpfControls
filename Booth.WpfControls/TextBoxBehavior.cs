using System.Windows;
using System.Windows.Controls;

namespace Booth.WpfControls
{
    public class TextBoxBehavior
    {
        public static bool GetSelectAllTextOnFocus(TextBox textBox)
        {
            return (bool)textBox.GetValue(SelectAllTextOnFocusProperty);
        }

        public static void SetSelectAllTextOnFocus(TextBox textBox, bool value)
        {
            textBox.SetValue(SelectAllTextOnFocusProperty, value);
        }

        public static readonly DependencyProperty SelectAllTextOnFocusProperty =
            DependencyProperty.RegisterAttached("SelectAllTextOnFocus", typeof(bool), typeof(TextBoxBehavior), new UIPropertyMetadata(false, OnSelectAllTextOnFocusChanged));

        private static void OnSelectAllTextOnFocusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var textBox = d as TextBox;
            if (textBox == null)
                return;

            if (e.NewValue is bool == false)
                return;

            if ((bool)e.NewValue)
                textBox.GotKeyboardFocus += SelectAll;
            else
                textBox.GotKeyboardFocus += SelectAll;
        }

        private static void SelectAll(object sender, RoutedEventArgs e)
        {
            var textBox = e.OriginalSource as TextBox;
            if (textBox == null)
                return;

            textBox.SelectAll();
        }
    }
}
