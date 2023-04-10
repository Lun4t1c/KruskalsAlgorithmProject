using GraphLib;
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

namespace GraphGUI.UserControls
{
    /// <summary>
    /// Interaction logic for VertexUserControl.xaml
    /// </summary>
    public partial class VertexUserControl : UserControl
    {
        public MainWindow MainWindowHandle { get; set; }
        public Vertex Vertex { get; set; }
        public double LocationX { get; set; } = 0;
        public double LocationY { get; set; } = 0;

        public VertexUserControl(Vertex vertex, MainWindow mainWindowHandle)
        {
            InitializeComponent();
            Vertex = vertex;
            MainWindowHandle = mainWindowHandle;

            ellipse.Width = 50;
            ellipse.Height = 50;
            ellipse.Fill = Brushes.Red;
            ellipse.Stroke = Brushes.Yellow;
            ellipse.StrokeThickness = 2;

            textBlock.Text = vertex.Label;
            textBlock.TextAlignment = TextAlignment.Center;
            textBlock.VerticalAlignment = VerticalAlignment.Center;
            textBlock.FontSize = 20;
            textBlock.FontWeight = FontWeights.Bold;            
        }

        private void OnPreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (sender is VertexUserControl shape)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    Point p = e.GetPosition(MainWindowHandle.GraphCanvas);

                    LocationX = p.X - shape.ActualWidth / 2;
                    LocationY = p.Y - shape.ActualHeight / 2;

                    Canvas.SetLeft(shape, LocationX);
                    Canvas.SetTop(shape, LocationY);
                    MainWindowHandle.GenerateLines();
                    shape.CaptureMouse();
                }
                else
                {
                    shape.ReleaseMouseCapture();
                }
            }
        }
    }
}
