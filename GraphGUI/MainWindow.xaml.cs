using GraphGUI.UserControls;
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
        public Graph CurrentGraph { get; set; }
        public List<VertexUserControl> VertexUserControls { get; set; } = new List<VertexUserControl>();
        public List<Line> EdgesLines { get; set; } = new List<Line>();
        public List<TextBlock> WeightsTextBlocks { get; set; } = new List<TextBlock>();
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
            GenerateLines();
        }

        private void GenerateShapes()
        {
            int xOffset = 5;
            int yOffset = 5;

            foreach (Vertex vertex in CurrentGraph.Vertices)
            {
                VertexUserControls.Add(CreateVertexUserControl(vertex, xOffset, yOffset));
                xOffset += 100;
                if (xOffset >= 400)
                {
                    yOffset += 100;
                    xOffset = 5;
                }
            }
        }

        public void GenerateLines()
        {
            foreach (Line line in EdgesLines)
                GraphCanvas.Children.Remove(line);
            EdgesLines.Clear();

            foreach (TextBlock textBlock in WeightsTextBlocks)
                GraphCanvas.Children.Remove(textBlock);
            WeightsTextBlocks.Clear();

            foreach (Edge edge in CurrentGraph.Edges)
                DrawLineBetweenVertexUserControls(edge);
        }

        private VertexUserControl CreateVertexUserControl(Vertex vertex, int xOffset, int yOffset)
        {
            VertexUserControl vertexUserControl = new VertexUserControl(vertex, this);

            Canvas.SetLeft(vertexUserControl, xOffset);
            Canvas.SetTop(vertexUserControl, yOffset);

            GraphCanvas.Children.Add(vertexUserControl);

            vertexUserControl.Loaded += (sender, e) =>
            {
                GenerateLines();
            };

            return vertexUserControl;
        }

        private VertexUserControl FindVertexUserControlByLabel(string label)
        {
            return VertexUserControls.Find(vuc => vuc.Vertex.Label == label);
        }

        private void DrawLineBetweenVertexUserControls(Edge edge)
        {
            VertexUserControl vertexControl1 = FindVertexUserControlByLabel(edge.FromVertex.Label);
            VertexUserControl vertexControl2 = FindVertexUserControlByLabel(edge.ToVertex.Label);

            // Create a new line
            Line line = new Line();

            // Set the start and end points of the line
            line.X1 = Canvas.GetLeft(vertexControl1) + vertexControl1.ActualWidth / 2;
            line.Y1 = Canvas.GetTop(vertexControl1) + vertexControl1.ActualHeight / 2;

            line.X2 = Canvas.GetLeft(vertexControl2) + vertexControl2.ActualWidth / 2;
            line.Y2 = Canvas.GetTop(vertexControl2) + vertexControl2.ActualHeight / 2;

            // Set the color and thickness of the line
            line.Stroke = Brushes.Wheat;
            line.StrokeThickness = 2;

            Canvas.SetZIndex(line, -1);

            // Add the line to the canvas
            GraphCanvas.Children.Add(line);
            EdgesLines.Add(line);

            PutWeightOnLine(line, edge.Weight);
        }

        private void PutWeightOnLine(Line line, int weight)
        {
            TextBlock textBlock = new TextBlock();
            textBlock.Text = weight.ToString();
            textBlock.TextAlignment = TextAlignment.Center;
            textBlock.VerticalAlignment = VerticalAlignment.Center;
            textBlock.FontSize = 22;
            textBlock.FontWeight = FontWeights.Normal;
            textBlock.Foreground = Brushes.Yellow;

            textBlock.Loaded += (sender, e) =>
            {
                double textBlockLeft = (line.X1 + line.X2 - textBlock.ActualWidth) / 2;
                double textBlockTop = (line.Y1 + line.Y2 - textBlock.ActualHeight) / 2 - 15;

                Canvas.SetLeft(textBlock, textBlockLeft);
                Canvas.SetTop(textBlock, textBlockTop);
                Canvas.SetZIndex(textBlock, 1);
            };

            GraphCanvas.Children.Add(textBlock);
            WeightsTextBlocks.Add(textBlock);
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
