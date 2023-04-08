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
        public Graph CurrentGraph { get; set; } = null;
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
        }

        private void GenerateShapes()
        {
            int offset = 0;

            foreach (Vertex vertex in CurrentGraph.Vertices)
            {
                CreateVertexUserControl(vertex, offset, 0);
                offset += 100;
            }
        }

        private VertexUserControl CreateVertexUserControl(Vertex vertex, int xOffset, int yOffset)
        {
            VertexUserControl vertexUserControl = new VertexUserControl(vertex, GraphCanvas);

            Canvas.SetLeft(vertexUserControl, xOffset);
            Canvas.SetTop(vertexUserControl, yOffset);

            GraphCanvas.Children.Add(vertexUserControl);

            return vertexUserControl;
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
