﻿using System.Windows;
using System.Windows.Controls;
using System.Security;
using System;
using System.Windows.Data;
using System.Reflection;

namespace Booth.WpfControls
{
    public class PasswordBoxBehavior
    {
        public static bool GetSelectAllTextOnFocus(PasswordBox passwordBox)
        {
            return (bool)passwordBox.GetValue(SelectAllTextOnFocusProperty);
        }

        public static void SetSelectAllTextOnFocus(PasswordBox passwordBox, bool value)
        {
            passwordBox.SetValue(SelectAllTextOnFocusProperty, value);
        }

        public static readonly DependencyProperty SelectAllTextOnFocusProperty =
            DependencyProperty.RegisterAttached("SelectAllTextOnFocus", typeof(bool), typeof(PasswordBox), new UIPropertyMetadata(false, OnSelectAllTextOnFocusChanged));

        private static void OnSelectAllTextOnFocusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var passwordBox = d as PasswordBox;
            if (passwordBox == null)
                return;

            if (e.NewValue is bool == false)
                return;

            if ((bool)e.NewValue)
                passwordBox.GotKeyboardFocus += SelectAll;
            else
                passwordBox.GotKeyboardFocus += SelectAll;
        }

        private static void SelectAll(object sender, RoutedEventArgs e)
        {
            var passwordBox = e.OriginalSource as PasswordBox;
            if (passwordBox == null)
                return;

            passwordBox.SelectAll();
        }

        

        public static string GetPassword(DependencyObject passwordBox)
        {
            return (string)passwordBox.GetValue(PasswordProperty);
        }

        public static void SetPassword(DependencyObject passwordBox, string value)
        {
            passwordBox.SetValue(PasswordProperty, value);
        }
                
        public static readonly DependencyProperty PasswordProperty =
                    DependencyProperty.RegisterAttached("Password", typeof(string), typeof(PasswordBoxBehavior), new UIPropertyMetadata("", OnSourcePropertyChanged));

        private static void OnSourcePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var passwordBox = d as PasswordBox;
            if (passwordBox == null)
                return;

            if (e.NewValue == null)
            {
                passwordBox.PasswordChanged -= OnPasswordBoxValueChanged;
                passwordBox.Password = string.Empty;
                passwordBox.PasswordChanged += OnPasswordBoxValueChanged;
            }
        }

        private static void OnPasswordBoxValueChanged(object sender, RoutedEventArgs e)
        {
            var passwordBox = sender as PasswordBox;
            if (passwordBox == null)
                return;

            SetPassword(passwordBox, passwordBox.Password);
        }
    }
}
