using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace App1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        // int _iCount;
        public MainPage()
        {
            this.InitializeComponent();


            Board();
            Peices();
        }

        int _Rows = 8;
        int _Cols = 8;
        private void Board()
        {
            for (int i = 0; i < _Rows; i++)
            {
                grdBoard.ColumnDefinitions.Add(new ColumnDefinition());
                grdBoard.RowDefinitions.Add(new RowDefinition());

            }

            //
            Border borderBG;
            int iRow, iCol;

            // use R&C to name the objects
            for (iRow = 0; iRow < _Rows; iRow++)
            {

                for (iCol = 0; iCol < _Cols; iCol++)
                {
                    borderBG = new Border();
                    borderBG.Name = "square_" + iRow.ToString() + "_" + iCol.ToString();
                    borderBG.Background = new SolidColorBrush(Colors.Black);


                    if ((iRow + iCol) % 2 == 0)
                    {
                        borderBG.Background = new SolidColorBrush(Colors.Red);

                    }

                    borderBG.SetValue(Grid.ColumnProperty, iCol);
                    borderBG.SetValue(Grid.RowProperty, iRow);
                    borderBG.HorizontalAlignment = HorizontalAlignment.Center;
                    borderBG.VerticalAlignment = VerticalAlignment.Center;

                    borderBG.Height = 61;
                    borderBG.Width = 61;
                    grdBoard.Children.Add(borderBG);

                }

            }
        }

        private void Peices()
        {
            Ellipse ellipse;

            for (int i = 0; i < _Rows; i++)
            {
                if (i % 2 == 0)
                {
                    ellipse = new Ellipse();
                    // add event handler
                    ellipse.Tapped += MyEl_Tapped;
                    ellipse.Tag = "cat";
                    ellipse.Name = "cat_" + _Rows + "_" + _Cols;
                    //use this for testing output
                    // System.Diagnostics.Debug.WriteLine(cat.Name);
                    ellipse.Fill = new SolidColorBrush(Colors.RosyBrown);
                    ellipse.Height = 45;
                    ellipse.Width = 45;
                    ellipse.SetValue(Grid.RowProperty, 0);
                    ellipse.SetValue(Grid.ColumnProperty, i);
                    grdBoard.Children.Add(ellipse);

                }
            }
            ellipse = new Ellipse();
            // add event handler
            ellipse.Tapped += MyEl_Tapped;
            ellipse.Name = "mouse_" + _Rows + "_" + _Cols;
            ellipse.Tag = "mouse";
            //use this for testing output
            // Debug.WriteLine(mouse.Name);
            ellipse.Fill = new SolidColorBrush(Colors.DarkOrange);
            ellipse.Height = 45;
            ellipse.Width = 45;
            ellipse.SetValue(Grid.RowProperty, 7);
            ellipse.SetValue(Grid.ColumnProperty, 3);
            grdBoard.Children.Add(ellipse);
        }
        Ellipse curr;
        Border legal, legal1;
        private void MyEl_Tapped(object sender, TappedRoutedEventArgs e)
        {
            curr = (Ellipse)sender;
            //curr.Fill = new SolidColorBrush(Colors.White);
            curr.Tapped -= MyEl_Tapped;
            legal = new Border();
            legal.Background = new SolidColorBrush(Colors.Green);
            legal1 = new Border();
            legal1.Background = new SolidColorBrush(Colors.Green);
            var column = Grid.GetColumn(curr);
            var row = Grid.GetRow(curr);
            Debug.WriteLine("Row " + row + " col " + column);
            if (curr.Tag.Equals("mouse"))
            {
               
                legal.SetValue(Grid.ColumnProperty, column + 1);
                legal.SetValue(Grid.RowProperty, row - 1);
                grdBoard.Children.Add(legal);
                
                legal1.SetValue(Grid.ColumnProperty, column - 1);
                legal1.SetValue(Grid.RowProperty, row - 1);
                grdBoard.Children.Add(legal1);
            }
            if (curr.Tag.Equals("cat"))
            {
                
                legal.SetValue(Grid.ColumnProperty, column + 1);
                legal.SetValue(Grid.RowProperty, row + 1);
                grdBoard.Children.Add(legal);
                
                legal1.SetValue(Grid.ColumnProperty, column - 1);
                legal1.SetValue(Grid.RowProperty, row + 1);
                grdBoard.Children.Add(legal1);
            }
        }
    }
}
