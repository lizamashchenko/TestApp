using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EnglishTest_
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Vector relativeMousePos;
        FrameworkElement draggedObject;
        private int Score = 0, step = 0;
        const int num = 3;
        //ImageSource[,] words = new ImageSource[num, 3] { { "../car.jpg", "lorry.jpg", "plane.jpg" }, { "pear.jpg", "apple.jpg", "plum.jpg", }, { "house.jpg", "garden.jpg", "hell.jpg" } };
        //string[,] pics = new string[num, 3] { { "lorry", "car", "plane" }, { "pear", "plum", "apple" }, { "hell", "garden", "house" } };
        //int[,] answ = new int[,] { { 2, 1, 3 }, { 1, 3, 2 }, { 3, 2, 1 } };
        int[] answ = new int[3] { 3, 1, 2 };


        public MainWindow()
        {
            InitializeComponent();
            UpdateTest();
        }
        private void img_MouseDown(object sender, MouseButtonEventArgs e)
        {
            draggedObject = (FrameworkElement)sender;
            relativeMousePos = e.GetPosition(draggedObject) - new Point();
            draggedObject.MouseMove += OnDragMove;
            draggedObject.LostMouseCapture += OnLostCapture;
            draggedObject.MouseUp += OnMouseUp;
            Mouse.Capture(draggedObject);

        }
        private void OnDragMove(object sender, MouseEventArgs e)
        {
            UpdatePosition(e);
        }
        private void UpdatePosition(MouseEventArgs e)
        {
            var point = e.GetPosition(canvas);
            var newPos = point - relativeMousePos;
            Canvas.SetLeft(draggedObject, newPos.X);
            Canvas.SetTop(draggedObject, newPos.Y);
        }
        private void OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            FinishDrag(sender, e);
            Mouse.Capture(null);
        }
        private void OnLostCapture(object sender, MouseEventArgs e)
        {
            FinishDrag(sender, e);
        }
        private void FinishDrag(object sender, MouseEventArgs e)
        {
            draggedObject.MouseMove -= OnDragMove;
            draggedObject.LostMouseCapture -= OnLostCapture;
            draggedObject.MouseUp -= OnMouseUp;
            UpdatePosition(e);
        }

        private void CheckButton_Click(object sender, RoutedEventArgs e)
        {
            int check1 = 0, check2 = 0, check3 = 0;
            if (InShape(img1, rect1)) check1 = 1;
            else if (InShape(img1, rect2)) check1 = 2;
            else if (InShape(img1, rect3)) check1 = 3;
            if (InShape(img2, rect1)) check2 = 1;
            else if (InShape(img2, rect2)) check2 = 2;
            else if (InShape(img2, rect3)) check2 = 3;
            if (InShape(img3, rect1)) check3 = 1;
            else if (InShape(img3, rect2)) check3 = 2;
            else if (InShape(img3, rect3)) check3 = 3;

            if (answ[0] == check1) Score++;
            if (answ[1] == check2) Score++;
            if (answ[2] == check3) Score++;
            MessageBox.Show(Score.ToString());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (step < num)
                UpdateTest();
            else
                MessageBox.Show("No more tests left");
        }
        private void UpdateTest()
        {
            step++;
            UpdatePics();
            UpdateLabels();
        }
        private void UpdatePics()
        {
           // img1.Source = (ImageSource)
        }
        private void UpdateLabels()
        {

        }

        private bool InShape(Image img, Rectangle rect)
        {
            var imgLeft = Canvas.GetLeft(img);
            var rectLeft = Canvas.GetLeft(rect);
            var imgTop = Canvas.GetTop(img);
            var rectTop = Canvas.GetTop(rect);
            if (imgLeft >= rectLeft
                && imgLeft + img.Width + imgLeft <= rect.Width + rectLeft
                && imgTop >= rectTop
                && imgTop + img.Height <= rect.Height + rectTop)
                return true;
            return false;
        }
        
    }
    
}
