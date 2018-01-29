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
        int _Rows, _Cols;
        // int _iCount;
        public MainPage()
        {
            this.InitializeComponent();
            // _iCount = 0;
            _Rows = 8;
            _Cols = 8;
            Board();
            Peices();
        }


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
                    borderBG.Name= "square_" + iRow.ToString() + "_" + iCol.ToString();
                  
                    
                        borderBG.Background = new SolidColorBrush(Colors.Black);

                    
                    if ((iRow+iCol)%2==0)
                    {
                        borderBG.Background = new SolidColorBrush(Colors.Red);
                        borderBG.SetValue(Grid.HeightProperty, 61);
                        borderBG.SetValue(Grid.WidthProperty, 61);
                    }
                   
                    borderBG.SetValue(Grid.ColumnProperty, iCol);
                    borderBG.SetValue(Grid.RowProperty, iRow);
                    
                    grdBoard.Children.Add(borderBG);













                }

            }
        }

        private void Peices()
        {
            Ellipse cat, mouse;

            for (int i = 0; i < _Rows; i+=2)
            {
                cat = new Ellipse();
                // add event handler
                cat.Tapped += MyEl_Tapped;
                cat.Name = "cat_" + _Rows + "_" + _Cols;
                //use this for testing output
               // System.Diagnostics.Debug.WriteLine(cat.Name);
                cat.Fill = new SolidColorBrush(Colors.RosyBrown);
                cat.Height = 45;
                cat.Width = 45;
                
                    cat.SetValue(Grid.RowProperty, 0);
                    cat.SetValue(Grid.ColumnProperty, i);
                    grdBoard.Children.Add(cat);
                
                
            }
            mouse = new Ellipse();
            // add event handler
            mouse.Tapped += MyEl_Tapped;
            mouse.Name = "mouse_" + _Rows + "_" + _Cols;
            //use this for testing output
            Debug.WriteLine(mouse.Name);
            mouse.Fill = new SolidColorBrush(Colors.DarkOrange);
            mouse.Height = 45;
            mouse.Width = 45;
            mouse.SetValue(Grid.RowProperty, 7);
            mouse.SetValue(Grid.ColumnProperty, 3);
            grdBoard.Children.Add(mouse);
        }
        private void MyEl_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Ellipse curr = (Ellipse)sender;
            curr.Fill = new SolidColorBrush(Colors.Red);
            curr.Opacity = 0.5;

            curr.Tapped -= MyEl_Tapped;
        }
    }
}
