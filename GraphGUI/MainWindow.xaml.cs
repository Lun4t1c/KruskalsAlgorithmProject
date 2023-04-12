using GraphGUI.UserControls;
using GraphGUI.Utils;
using GraphLib;
using Newtonsoft.Json;
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
        public Graph OverlayGraph { get; set; } = null;
        public List<Graph> CurrentSteps { get; set; } = new List<Graph>();
        public int CurrentStepsIndex { get; set; } = 0;
        public List<VertexUserControl> VertexUserControls { get; set; } = new List<VertexUserControl>();
        public List<Line> EdgesLines { get; set; } = new List<Line>();
        public List<TextBlock> WeightsTextBlocks { get; set; } = new List<TextBlock>();
        private bool IsOverlayModeEnabled { get; set; } = false;
        public ToolsEnum SelectedTool { get; set; } = ToolsEnum.None;

        public Dictionary<string, Point> CustomVertexPoints { get; set; } = new Dictionary<string, Point>()
        {
            { "a", new Point() },
            { "b", new Point() },
            { "c", new Point() },
            { "d", new Point() },
            { "e", new Point() },
            { "f", new Point() },
            { "g", new Point() },
        };
        #endregion


        #region Constructor
        public MainWindow()
        {
            InitializeComponent();
            IsOverlayModeEnabled = true;
        }
        #endregion


        #region Methods
        private void GenerateGraph()
        {
            ClearCanvas();

            GenerateVertices();
            RedrawLines();
        }

        private void GenerateDefaultGraph()
        {
            CurrentGraph = Graph.GenerateSampleGraph2();
            GenerateGraph();
        }

        private void GenerateDefaultDisjointGraph()
        {
            CurrentGraph = Graph.GenerateSampleGraphDisjoint();
            GenerateGraph();
        }

        private void GenerateVertices()
        {
            ClearCanvas();

            int xOffset = 5;
            int yOffset = 5;

            foreach (Vertex vertex in CurrentGraph.Vertices)
            {
                Dictionary<string, Point> locationsDictionary = JsonConvert.DeserializeObject<Dictionary<string, Point>>(Properties.Settings.Default.VertexLocations);

                if (locationsDictionary != null && locationsDictionary.ContainsKey(vertex.Label))
                {
                    VertexUserControls.Add(CreateVertexUserControl(vertex, (int)locationsDictionary[vertex.Label].X, (int)locationsDictionary[vertex.Label].Y));
                }
                else
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
        }

        public void RedrawLines()
        {
            ClearLines();
            
            if (IsOverlayModeEnabled)
            {
                foreach (Edge edge in CurrentGraph.Edges)
                    DrawLineBetweenVertexUserControls(edge, Brushes.Gray, 3);

                if (OverlayGraph != null)
                    foreach (Edge edge in OverlayGraph.Edges)
                        DrawLineBetweenVertexUserControls(edge, Brushes.Green, 4);
            }
            else
            {
                if (OverlayGraph != null)
                    foreach (Edge edge in OverlayGraph.Edges)
                        DrawLineBetweenVertexUserControls(edge, Brushes.Gray, 3);
            }
        }

        private VertexUserControl CreateVertexUserControl(Vertex vertex, int xOffset, int yOffset)
        {
            VertexUserControl vertexUserControl = new VertexUserControl(vertex, this);
            vertexUserControl.LocationX = xOffset;
            vertexUserControl.LocationY = yOffset;

            Canvas.SetLeft(vertexUserControl, vertexUserControl.LocationX);
            Canvas.SetTop(vertexUserControl, vertexUserControl.LocationY);

            GraphCanvas.Children.Add(vertexUserControl);

            vertexUserControl.Loaded += (sender, e) =>
            {
                RedrawLines();
            };

            return vertexUserControl;
        }

        private VertexUserControl FindVertexUserControlByLabel(string label)
        {
            return VertexUserControls.Find(vuc => vuc.Vertex.Label == label);
        }

        private void DrawLineBetweenVertexUserControls(Edge edge, Brush brush, int thickness)
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
            line.Stroke = brush;
            line.StrokeThickness = thickness;

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
                double textBlockLeft = (line.X1 + line.X2 - textBlock.ActualWidth) / 2 + 20;
                double textBlockTop = (line.Y1 + line.Y2 - textBlock.ActualHeight) / 2 - 20;

                Canvas.SetLeft(textBlock, textBlockLeft);
                Canvas.SetTop(textBlock, textBlockTop);
                Canvas.SetZIndex(textBlock, 1);
            };

            GraphCanvas.Children.Add(textBlock);
            WeightsTextBlocks.Add(textBlock);
        }

        private void ClearCanvas()
        {
            foreach (VertexUserControl vertexUserControl in VertexUserControls)
                GraphCanvas.Children.Remove(vertexUserControl);
            VertexUserControls.Clear();

            ClearLines();
        }

        private void ClearLines()
        {
            foreach (Line line in EdgesLines)
                GraphCanvas.Children.Remove(line);
            EdgesLines.Clear();

            foreach (TextBlock textBlock in WeightsTextBlocks)
                GraphCanvas.Children.Remove(textBlock);
            WeightsTextBlocks.Clear();
        }

        private void NextStep()
        {
            if (CurrentStepsIndex >= CurrentSteps.Count - 1) return;
            else
            {
                CurrentStepsIndex++;
                OverlayGraph = CurrentSteps[CurrentStepsIndex];
                RedrawLines();
            }
        }

        private void PreviousStep()
        {
            if (CurrentStepsIndex <= 0) return;
            else
            {
                CurrentStepsIndex--;
                OverlayGraph = CurrentSteps[CurrentStepsIndex];
                RedrawLines();
            }
        }
        #endregion


        #region Button clicks
        private void GenerateDefaultGraphButton_Click(object sender, RoutedEventArgs e)
        {
            GenerateDefaultGraph();
        }

        private void GenerateDefaultDisjointGraphButton_Click(object sender, RoutedEventArgs e)
        {
            GenerateDefaultDisjointGraph();
        }

        private void PerformKruskalButton_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentGraph != null)
            {
                CurrentGraph.TransformIntoMSTKruskal();
                RedrawLines();
            }
        }

        private void PerformKruskalStepByStepButton_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentGraph != null)
            {
                CurrentSteps = CurrentGraph.GetMSTKruskalStepByStep();
                OverlayGraph = CurrentSteps[0];
                RedrawLines();
            }
        }

        private void NextStepButton_Click(object sender, RoutedEventArgs e)
        {
            NextStep();
        }

        private void PreviousStepButton_Click(object sender, RoutedEventArgs e)
        {
            PreviousStep();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            IsOverlayModeEnabled = true;
            if (CurrentGraph != null && CurrentGraph.Edges != null) 
                RedrawLines();
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            IsOverlayModeEnabled = false;
            if (CurrentGraph != null && CurrentGraph.Edges != null) 
                RedrawLines();
        }

        private void GraphCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (SelectedTool == ToolsEnum.AddVertex || Keyboard.Modifiers == ModifierKeys.Control)
            {
                CreateVertexUserControl(new Vertex(69, "benc"), (int)e.GetPosition(GraphCanvas).X, (int)e.GetPosition(GraphCanvas).Y);
            }
        }
        #endregion
    }
}
