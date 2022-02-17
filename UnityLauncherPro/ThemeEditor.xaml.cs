﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace UnityLauncherPro
{
    public partial class ThemeEditor : Window
    {
        // TODO take from mainwindow?
        //Dictionary<string, SolidColorBrush> origResourceColors = new Dictionary<string, SolidColorBrush>();
        //public static List<ThemeColor> themeColors;
        public static ObservableCollection<ThemeColor> themeColors = new ObservableCollection<ThemeColor>();

        public ThemeEditor()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            themeColors.Clear();

            // get original colors
            foreach (DictionaryEntry item in Application.Current.Resources.MergedDictionaries[0])
            {
                // take original colors, so can reset them
                //origResourceColors[item.Key.ToString()] = (SolidColorBrush)item.Value;
                //var col = (SolidColorBrush)item.Value;
                //Console.WriteLine(item.Key.ToString() + "=" + col);
                var themeColorPair = new ThemeColor();
                themeColorPair.Key = item.Key.ToString();
                themeColorPair.Brush = (SolidColorBrush)item.Value;
                themeColors.Add(themeColorPair);
                //var col = new BrushConverter().ConvertFrom(row[1].Trim());
                // apply color
                //Application.Current.Resources[row[0]] = (SolidColorBrush)col;
            }

            // display current theme keys and values
            //gridThemeColors.ItemsSource = origResourceColors;
            gridThemeColors.ItemsSource = themeColors;
            //gridThemeColors.DataContext = themeColors;

        }

        int selectedRow = -1;
        bool forceValue = false;

        private void GridThemeColors_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (gridThemeColors.SelectedItem == null) return;
            //Console.WriteLine(gridThemeColors.SelectedItem);
            var item = gridThemeColors.SelectedItem as ThemeColor;

            //selectedRow = gridThemeColors.SelectedIndex;

            //selectedKey = k.Value.Key;
            //Console.WriteLine("Selected: " +selectedKey + "=" + origResourceColors[selectedKey].ToString());

            // show color
            // TODO show current color AND modified color next to each other
            //rectSelectedColor.Fill = origResourceColors[selectedKey];
            //var item = (string, SolidColorBrush)gridThemeColors.SelectedItem;
            rectSelectedColor.Fill = item.Brush;

            //txtSelectedColorHex.Text = origResourceColors[selectedKey].ToString();

            forceValue = true;
            sliderRed.Value = item.Brush.Color.R;
            forceValue = true;
            sliderGreen.Value = item.Brush.Color.G;
            forceValue = true;
            sliderBlue.Value = item.Brush.Color.B;
            forceValue = true;
            sliderAlpha.Value = item.Brush.Color.A;
            forceValue = false;
        }

        void UpdateColorPreview()
        {
            var newColor = new Color();
            newColor.R = byte.Parse(((int)sliderRed.Value).ToString());
            newColor.G = byte.Parse(((int)sliderGreen.Value).ToString());
            newColor.B = byte.Parse(((int)sliderBlue.Value).ToString());
            newColor.A = byte.Parse(((int)sliderAlpha.Value).ToString());
            var newColorBrush = new SolidColorBrush(newColor);
            rectSelectedColor.Fill = newColorBrush;

            // TODO apply color to datagrid or dictionary
            //if (selectedKey == null) return;
            //origResourceColors[selectedKey] = newColorBrush;
            //gridThemeColors.Items.Refresh();

            //DataRowView rowView = gridThemeColors.Items[ as DataRowView;
            //rowView.BeginEdit();
            //rowView[1] = "Change cell here";
            //rowView.EndEdit();
            //gridThemeColors.Items.Refresh();
            //Console.WriteLine(1234);

            //themeColors[gridThemeColors.SelectedIndex].Key = "asdf";
            themeColors[gridThemeColors.SelectedIndex].Brush = newColorBrush;

            // NOTE slow but works..
            gridThemeColors.Items.Refresh();

            // apply color changes to mainwindow
            var item = gridThemeColors.SelectedItem as ThemeColor;
            Application.Current.Resources[item.Key] = newColorBrush;
        }

        private void SliderRed_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (forceValue == true) return;
            // onchanged is called before other components are ready..wpf :D
            if (txtRed == null) return;
            txtRed.Text = ((int)((Slider)sender).Value).ToString();
            UpdateColorPreview();
            forceValue = false;
        }

        private void SliderGreen_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (forceValue == true) return;
            if (txtGreen == null) return;
            txtGreen.Text = ((int)((Slider)sender).Value).ToString();
            UpdateColorPreview();
            forceValue = false;
        }

        private void SliderBlue_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (forceValue == true) return;
            if (txtBlue == null) return;
            txtBlue.Text = ((int)((Slider)sender).Value).ToString();
            UpdateColorPreview();
            forceValue = false;
        }

        private void SliderAlpha_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (forceValue == true) return;
            if (txtAlpha == null) return;
            txtAlpha.Text = ((int)((Slider)sender).Value).ToString();
            UpdateColorPreview();
            forceValue = false;
        }

        private void BtnSaveTheme_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("TODO save theme to file..");
        }


    }
}