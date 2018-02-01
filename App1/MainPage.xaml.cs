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

        Ellipse moveMe;
        Border legal, legal1;

        private void MyEl_Tapped(object sender, TappedRoutedEventArgs e)
        {
           Ellipse curr = (Ellipse)sender;
            moveMe = curr;
            //curr.Fill = new SolidColorBrush(Colors.White);
            curr.Tapped -= MyEl_Tapped;
            legal = new Border();
            legal.Background = new SolidColorBrush(Colors.Green);
            legal1 = new Border();
            legal1.Background = new SolidColorBrush(Colors.Green);
            var column = Grid.GetColumn(curr);
            var row = Grid.GetRow(curr);
            Debug.WriteLine("Row " + row + " col " + column);
            Boolean move = false;
            if (curr.Tag.Equals("mouse"))
            {

                legalMove(row, column, 1);
                move = true;

            }
            if (curr.Tag.Equals("cat"))
            {


                legalMove(row, column, -1);
                move = true;


            }

            if (move == true && curr.Tag.Equals("mouse"))
            {
                Debug.WriteLine("The mouse has been selected...Setup move function with tapped event for it!!");
            }
            else
            {
                Debug.WriteLine("The cat has been selected...Setup move function with tapped event for it!!");
            }


        }

        private void legalMove(int rowPos, int colPos, int xy)
        {
            //put if statement to check if the value of y is < 0 or > 7.
            //For edge of board.
            Border bdr = new Border();
            bdr.Background = new SolidColorBrush(Colors.Green);
            
            
            int highlightY = colPos + xy;
            int highlightY1 = colPos - xy;
            int highlightX = rowPos - xy;
            
            //to deal with edge cells of board reset position of legal move to one square.
            if (highlightY < 0)
            {
                highlightY = 1;
            }
            else if (highlightY1 > 7)
            {
                highlightY1 = 7;
            }
           
            bdr.Tag = "valid";
            bdr.Tapped += moveToken;
            legal = bdr;
            legal.SetValue(Grid.ColumnProperty, highlightY);
            legal.SetValue(Grid.RowProperty, highlightX);
            grdBoard.Children.Add(legal);
            bdr.Tag = "valid";
            bdr.Tapped += moveToken;
            legal = bdr;
            legal1.SetValue(Grid.ColumnProperty, highlightY1);
            legal1.SetValue(Grid.RowProperty, highlightX);
            grdBoard.Children.Add(legal1);
        }
        private void moveToken(object sender, TappedRoutedEventArgs e)
        {
            Border current = (Border)sender;

            moveMe.SetValue(Grid.RowProperty, current.GetValue(Grid.RowProperty));
            moveMe.SetValue(Grid.ColumnProperty, current.GetValue(Grid.ColumnProperty));

            legal.Tapped -= moveToken;
            legal.Background = new SolidColorBrush(Colors.Red);
            legal1.Tapped -= moveToken;
            legal1.Background = new SolidColorBrush(Colors.Red);
        }
    }
}
