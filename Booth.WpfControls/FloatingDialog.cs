using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Collections.ObjectModel;

namespace Booth.WpfControls
{
    public class FloatingDialog : ContentControl
    {
        private DialogAdorner? _AdornerDialog;
        private AdornerLayer? _MainWindowAdornerLayer;

        private Grid? _DialogWindowPart;

        static FloatingDialog()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FloatingDialog), new FrameworkPropertyMetadata(typeof(FloatingDialog)));
        }

        public FloatingDialog()
            : base()
        {
            Focusable = false;
        }

        public override void OnApplyTemplate()
        {
            if (_AdornerDialog == null)
            {
                _DialogWindowPart = GetTemplateChild("PART_DialogWindow") as Grid;
                if (_DialogWindowPart != null)
                {
                    _DialogWindowPart.Visibility = Visibility.Hidden;

                    _AdornerDialog = new DialogAdorner(this, _DialogWindowPart);
                    _MainWindowAdornerLayer = AdornerLayer.GetAdornerLayer(this);
                    _MainWindowAdornerLayer.Add(_AdornerDialog);
                }
            }

            base.OnApplyTemplate();
        }

        public static DependencyProperty CommandsProperty = DependencyProperty.Register(
                      "Commands", typeof(ObservableCollection<DialogCommand>), typeof(FloatingDialog),
                      new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));
        public ObservableCollection<DialogCommand> Commands
        {
            get
            {
                return (ObservableCollection<DialogCommand>)GetValue(CommandsProperty);
            }
            set
            {
                SetValue(CommandsProperty, value);
            }
        }

        public static DependencyProperty IsOpenProperty = DependencyProperty.Register(
                      "IsOpen", typeof(bool), typeof(FloatingDialog),
                      new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(OnOpenChanged)));
        public bool IsOpen
        {
            get
            {
                return (bool)GetValue(IsOpenProperty);
            }
            set
            {
                SetValue(IsOpenProperty, value);
            }
        }

        private static void OnOpenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (FloatingDialog)d;

            if (control._DialogWindowPart != null)
            {
                if ((bool)e.NewValue)
                {
                    control._DialogWindowPart.Visibility = Visibility.Visible;
                    control.Focusable = true;
                    control.Focus();
                }
                else
                {
                    control._DialogWindowPart.Visibility = Visibility.Hidden;
                    control.Focusable = false;
                }
            }
        }

        protected override void OnGotFocus(RoutedEventArgs e)
        {
            base.OnGotFocus(e);

            if (e.OriginalSource == this)
            {
                TraversalRequest tRequest = new TraversalRequest(FocusNavigationDirection.Next);
                this.MoveFocus(tRequest);
            }

        }

    }
}
