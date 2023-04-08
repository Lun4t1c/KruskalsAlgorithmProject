using GraphLib;
using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace GraphGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Properties
        public Graph CurrentGraph { get; set; } = null;
        public List<UIElement> shapes = new List<UIElement>();
        #endregion


        #region Constructor
        public MainWindow()
        {
            InitializeComponent();
        }
        #endregion


        #region Methods
        private void GenerateGraph()
        {
            CurrentGraph = Graph.GenerateSampleGraph();
            GenerateShapes();
            PaintCanvas();
        }

        private void GenerateShapes()
        {
            int offset = 0;

            foreach (Vertex vertex in CurrentGraph.Vertices)
            {
                Ellipse ellipse = CreateVertexEllipse(vertex, offset, 0);
                shapes.Add(ellipse);
                offset += 100;
            }
        }

        private void PaintCanvas()
        {
            GraphCanvas.Children.Clear();
            foreach (UIElement shape in shapes)
            {
                GraphCanvas.Children.Add(shape);
            }
        }

        private Ellipse CreateVertexEllipse(Vertex vertex, int xOffset, int yOffset)
        {
            Ellipse ellipse = new Ellipse();
            ellipse.Width = 50;
            ellipse.Height = 50;
            ellipse.Fill = Brushes.Red;
            ellipse.Stroke = Brushes.Yellow;
            ellipse.StrokeThickness = 2;

            Canvas.SetLeft(ellipse, xOffset);
            Canvas.SetTop(ellipse, yOffset);

            AddTextBlockToEllipse(ellipse, vertex.Label);

            GraphCanvas.Children.Add(ellipse);

            ellipse.MouseLeftButtonDown += (sender, e) =>
            {
                ellipse.CaptureMouse();
            };
            ellipse.MouseMove += (sender, e) =>
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    Point position = e.GetPosition(GraphCanvas);

                    Canvas.SetTop(ellipse, position.Y - (ellipse.Height / 2));
                    Canvas.SetLeft(ellipse, position.X - (ellipse.Width / 2));
                }
            };
            ellipse.MouseLeftButtonUp += (sender, e) =>
            {
                ellipse.ReleaseMouseCapture();
            };

            return ellipse;
        }

        private void AddTextBlockToEllipse(Ellipse ellipse, string text)
        {
            TextBlock textBlock = new TextBlock()
            {
                Text = text,
                TextAlignment = TextAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };

            GraphCanvas.Children.Add(textBlock);
            textBlock.Loaded += (sender, e) =>
            {
                Canvas.SetLeft(textBlock, Canvas.GetLeft(ellipse) + ellipse.Width / 2 - textBlock.ActualWidth / 2);
                Canvas.SetTop(textBlock, Canvas.GetTop(ellipse) + ellipse.Height / 2 - textBlock.ActualHeight / 2);
                Canvas.SetZIndex(textBlock, 1);
            };
            shapes.Add(textBlock);
        }
        #endregion


        #region Button clicks
        private void GenerateGraphButton_Click(object sender, RoutedEventArgs e)
        {
            GenerateGraph();
        }

        private void PerformKruskalButton_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
