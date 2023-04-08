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
        public Canvas CanvasHandle { get; set; }

        public VertexUserControl(Vertex vertex, Canvas canvasHandle)
        {
            InitializeComponent();
            CanvasHandle = canvasHandle;

            ellipse.Width = 50;
            ellipse.Height = 50;
            ellipse.Fill = Brushes.Red;
            ellipse.Stroke = Brushes.Yellow;
            ellipse.StrokeThickness = 2;

            textBlock.Text = vertex.Label;
            textBlock.TextAlignment = TextAlignment.Center;
            textBlock.VerticalAlignment = VerticalAlignment.Center;
            textBlock.FontSize = 14;
            textBlock.FontWeight = FontWeights.Bold;            
        }

        private void OnPreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (sender is VertexUserControl shape)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    Point p = e.GetPosition(CanvasHandle);
                    Canvas.SetLeft(shape, p.X - shape.ActualWidth / 2);
                    Canvas.SetTop(shape, p.Y - shape.ActualHeight / 2);
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
