// <copyright file="PeopleGraphViewModel.cs" company="MeMyselfAndI">
//     Copyright (c) MeMyselfAndI. All rights reserved.
// </copyright>

namespace GrampsView.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using GrampsView.Common;
    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;
    using GrampsView.Data.Repository;

    using Prism.Events;
    using Prism.Navigation;

    /// <summary>
    /// View Model for People Graph.
    /// </summary>
    /// <seealso cref="Prism.Mvvm.ViewModelBase"/>
    public class PeopleGraphViewModel : ViewModelBase
    {
        /// <summary>
        /// The node horizontal seperation.
        /// </summary>
        public const int MinNodeXSeperation = 50;

        /// <summary>
        /// The node vertical seperation.
        /// </summary>
        public const int MinNodeYSeperation = 100;

        /// <summary>
        /// The node wisth.
        /// </summary>
        public const int NodeX = 210;

        /// <summary>
        /// The node height.
        /// </summary>
        public const int NodeY = 70;

        /// <summary>
        /// The visited.
        /// </summary>
        private readonly Dictionary<string, bool> nodeVisited = new Dictionary<string, bool>();

        /// <summary>
        /// The graph collection.
        /// </summary>
        private readonly Queue<NextModel> nodeVisitQueue = new Queue<NextModel>();

        /// <summary>
        /// The local canvas height.
        /// </summary>
        private int localCanvasHeight = 0;

        /// <summary>
        /// The local canvas width.
        /// </summary>
        private int localCanvasWidth = 0;

        /// <summary>
        /// The current family or person.
        /// </summary>
        private HLinkBase localStartHLink = null;

        /// <summary>
        /// Gets or sets the maximum level.
        /// </summary>
        /// <value>
        /// The maximum level.
        /// </value>
        private int maxLevel = 0;

        /// <summary>
        /// The maximum level nodes.
        /// </summary>
        private int maxLevelNodes = 0;

        /// <summary>
        /// The maximum horizontal.
        /// </summary>
        private SortedDictionary<int, int> maxXNodes = new SortedDictionary<int, int>()
        {
        };

        /// <summary>
        /// The minimum level.
        /// </summary>
        private int minLevel = 0;

        /// <summary>
        /// The number levels.
        /// </summary>
        private int numLevels;

        /// <summary>
        /// Initializes a new instance of the <see cref="PeopleGraphViewModel"/> class.
        /// </summary>
        /// <param name="iocCommonLogging">
        /// The ioc common logging.
        /// </param>
        /// <param name="iocEventAggregator">
        /// The ioc event aggregator.
        /// </param>
        public PeopleGraphViewModel(ICommonLogging iocCommonLogging, IEventAggregator iocEventAggregator, INavigationService iocNavigationService)
            : base(iocCommonLogging, iocEventAggregator, iocNavigationService)
        {
            BaseTitle = "People Graph";
            BaseTitleIcon = CommonConstants.IconPeopleGraph;
        }

        /// <summary>
        /// Gets or sets the height of the canvas.
        /// </summary>
        /// <value>
        /// The height of the canvas.
        /// </value>
        public int CanvasHeight
        {
            get
            {
                return localCanvasHeight;
            }

            set
            {
                SetProperty(ref localCanvasHeight, value);
            }
        }

        /// <summary>
        /// Gets or sets the width of the canvas.
        /// </summary>
        /// <value>
        /// The width of the canvas.
        /// </value>
        public int CanvasWidth
        {
            get
            {
                return localCanvasWidth;
            }

            set
            {
                SetProperty(ref localCanvasWidth, value);
            }
        }

        /// <summary>
        /// Gets or sets the edges.
        /// </summary>
        /// <value>
        /// The edges.
        /// </value>
        public List<EdgeItem> Edges { get; set; }

            = new List<EdgeItem>();

        /// <summary>
        /// Gets or sets the graph canvas.
        /// </summary>
        /// <value>
        /// The graph canvas.
        /// </value>
        public SkiaSharp.SKCanvas GraphCanvas { get; set; }

        /// <summary>
        /// Gets or sets the current person ViewModel.
        /// </summary>
        /// <value>
        /// The current person ViewModel.
        /// </value>
        // [RestorableState]
        public HLinkBase StartHLink
        {
            get
            {
                return localStartHLink;
            }

            set
            {
                SetProperty(ref localStartHLink, value);
            }
        }

        /// <summary>
        /// Gets or sets the tree graph.
        /// </summary>
        /// <value>
        /// The tree graph.
        /// </value>
        public List<PeopleGraphNode> TreeGraph { get; set; } = new List<PeopleGraphNode>();

        /// <summary>
        /// Adds the h link.
        /// </summary>
        /// <param name="argFrom">
        /// The argument from.
        /// </param>
        /// <param name="argTo">
        /// The argument to.
        /// </param>
        /// <param name="argJumpsFromStart">
        /// The argument distance.
        /// </param>
        /// <param name="argLevel">
        /// The argument level.
        /// </param>
        public void AddHLink(HLinkBase argFrom, HLinkBase argTo, int argJumpsFromStart, int argLevel)
        {
            if (argFrom is null)
            {
                throw new ArgumentNullException(nameof(argFrom));
            }

            if (argTo is null)
            {
                throw new ArgumentNullException(nameof(argTo));
            }

            if (argJumpsFromStart > 0)
            {
                if (!nodeVisited.ContainsKey(argFrom.HLinkKey))
                {
                    if (argFrom.Valid)
                    {
                        nodeVisited.Add(argFrom.HLinkKey, false);

                        nodeVisitQueue.Enqueue(new NextModel()
                        {
                            HLink = argFrom,
                            JumpsFromStartNode = argJumpsFromStart - 1,
                            Level = argLevel,
                        });

                        AddEdge(new EdgeItem() { From = argFrom.HLinkKey, To = argTo.HLinkKey });
                    }
                }

                if (!nodeVisited.ContainsKey(argTo.HLinkKey))
                {
                    if (argTo.Valid)
                    {
                        nodeVisited.Add(argTo.HLinkKey, false);

                        nodeVisitQueue.Enqueue(new NextModel()
                        {
                            HLink = argTo,
                            JumpsFromStartNode = argJumpsFromStart - 1,
                            Level = argLevel,
                        });

                        AddEdge(new EdgeItem() { From = argFrom.HLinkKey, To = argTo.HLinkKey });
                    }
                }
            }
        }

        ///// <summary>
        ///// Draws the graph.
        ///// </summary>
        //public void DrawTheGraph()
        //{
        //    Canvas theGraph = new Canvas();

        // // Draw the edges for (int i = 0; i < Edges.Count; i++) {
        // theGraph.Children.Add(Edges[i].TheLine); }

        // // Draw the nodes for (int i = 0; i < TreeGraph.Count; i++) { PeopleGraphNode item = TreeGraph[i];

        // // Assume person PersonModel t = DV.PersonDV.GetModel(item.nodeHLink.HLinkKey);

        // if (t.HLink.Valid == true) { PersonCardSmall tt = new PersonCardSmall { DataContext = t,
        // Background = new SolidColorBrush(Colors.AliceBlue), }; theGraph.Children.Add(tt);

        // tt.SetValue(Canvas.LeftProperty, item.xStart); tt.SetValue(Canvas.TopProperty,
        // item.yStart); } else { // Assume Family FamilyModel tf =
        // DV.FamilyDV.GetModel(item.nodeHLink.HLinkKey); FamilyCardSmall tt = new FamilyCardSmall {
        // DataContext = tf, Background = new SolidColorBrush(Colors.AliceBlue), };

        // theGraph.Children.Add(tt);

        // tt.SetValue(Canvas.LeftProperty, item.xStart); tt.SetValue(Canvas.TopProperty,
        // item.yStart); } }

        //    // Display it
        //    GraphCanvas = theGraph;
        //}

        /// <summary>
        /// Gets the graph.
        /// </summary>
        /// <param name="argHLink">
        /// The argument h link.
        /// </param>
        public void GetGraph()
        {
            // Get people related to personObject person (within 3 jumps and starting at level zero
            // (the middlish point))
            nodeVisitQueue.Enqueue(new NextModel() { HLink = StartHLink, JumpsFromStartNode = 3, Level = 0 });

            PeopleGraphNode theNode;

            do
            {
                NextModel t = nodeVisitQueue.Dequeue();

                // Handle people
                if (t.HLink.GetType() == typeof(HLinkPersonModel))
                {
                    // Add person to graph
                    theNode = new PeopleGraphNode
                    {
                        NodeHLink = (t.HLink as HLinkPersonModel).DeRef.HLink,
                        YStart = t.Level,
                    };
                    TreeGraph.Add(theNode);

                    // Add one to the level
                    if (!maxXNodes.ContainsKey(t.Level))
                    {
                        maxXNodes.Add(t.Level, 0);
                    }

                    // Add one to the level
                    maxXNodes[t.Level] = maxXNodes[t.Level] + 1;

                    AddVisited((t.HLink as HLinkPersonModel).DeRef.HLinkKey, false);

                    if (t.JumpsFromStartNode > 0)
                    {
                        // get parents
                        AddHLink((t.HLink as HLinkPersonModel).DeRef.GChildOf, t.HLink, t.JumpsFromStartNode, t.Level - 1);

                        // get families
                        foreach (HLinkFamilyModel item in (t.HLink as HLinkPersonModel).DeRef.GParentInRefCollection)
                        {
                            AddHLink(t.HLink, item, t.JumpsFromStartNode, t.Level + 1);
                        }
                    }
                }

                // Handle families
                if (t.HLink.GetType() == typeof(HLinkFamilyModel))
                {
                    // Add Family
                    theNode = new PeopleGraphNode
                    {
                        NodeHLink = (t.HLink as HLinkFamilyModel).DeRef.HLink,
                        YStart = t.Level,
                    };
                    TreeGraph.Add(theNode);

                    // Add one to the level
                    if (!maxXNodes.ContainsKey(t.Level))
                    {
                        maxXNodes.Add(t.Level, 0);
                    }

                    // Add one to the level
                    maxXNodes[t.Level] = maxXNodes[t.Level] + 1;

                    AddVisited((t.HLink as HLinkFamilyModel).DeRef.HLinkKey, false);

                    if (t.JumpsFromStartNode > 0)
                    {
                        // Add Mother and Father at previous level
                        AddHLink((t.HLink as HLinkFamilyModel).DeRef.GMother, t.HLink, t.JumpsFromStartNode, t.Level - 1);

                        AddHLink((t.HLink as HLinkFamilyModel).DeRef.GFather, t.HLink, t.JumpsFromStartNode, t.Level - 1);

                        // Add children at next level
                        foreach (HLinkPersonModel item in (t.HLink as HLinkFamilyModel).DeRef.GChildRefCollection)
                        {
                            AddHLink(t.HLink, item, t.JumpsFromStartNode, t.Level + 1);
                        }
                    }
                }
            }
            while (nodeVisitQueue.Count > 0);
        }

        /// <summary>
        /// Reset data structures in case we have to restart the graph after the page is loaded.
        /// </summary>
        public void GraphReset()
        {
            TreeGraph = new List<PeopleGraphNode>();
        }

        /// <summary>
        /// Layouts the edge.
        /// </summary>
        public void LayoutEdges()
        {
            //for (int i = 0; i < Edges.Count; i++)
            //{
            //    EdgeItem tt = Edges[i];

            // tt.TheLine = new Line { Stroke = new SolidColorBrush(Colors.AliceBlue),
            // StrokeThickness = 2, }; tt.TheLine.SetValue(Canvas.ZIndexProperty, -10); // Default
            // for control nodes is -1

            // // Fnd the end node centres PeopleGraphNode startNode = TreeGraph.FirstOrDefault(iq
            // => iq.nodeHLink.HLinkKey == tt.From); PeopleGraphNode endNode =
            // TreeGraph.FirstOrDefault(iq => iq.nodeHLink.HLinkKey == tt.To);

            // // Draw the nodes from bottom of control to the top tt.TheLine.X1 = startNode.xStart
            // + (NodeX / 2); tt.TheLine.Y1 = startNode.yStart + NodeY;

            // tt.TheLine.X2 = endNode.xStart + (NodeX / 2); tt.TheLine.Y2 = endNode.yStart;

            // //tt.TheLine.Stroke = Microsoft.Toolkit.Uwp.Helpers.ColorHelper.ToColor("Red");
            // tt.TheLine.StrokeEndLineCap = PenLineCap.Triangle;

            //    Edges[i] = tt;
            //}
        }

        /// <summary>
        /// Layouts the nodes.
        /// </summary>
        public void LayoutNodes()
        {
            int nodeYSeperation = NodeY + MinNodeYSeperation;
            int nodeXSeperation = NodeX + MinNodeXSeperation;
            int levelXMax = nodeXSeperation * maxLevelNodes;

            // Sort nodes for layout by level
            TreeGraph.Sort(PeopleGraphNode.SortByYStart);

            // Layout the nodes
            int currentY = -1;
            int currentX = -01;
            int levelNodeOffset = 0;

            for (int i = 0; i < TreeGraph.Count; i++)
            {
                PeopleGraphNode item = TreeGraph[i];

                // check for chnaged level
                if (item.YStart > currentY)
                {
                    currentY = item.YStart;
                    currentX = -1;

                    levelNodeOffset = ((maxLevelNodes * nodeXSeperation) - (maxXNodes[currentY] * nodeXSeperation)) / maxXNodes[currentY];
                }

                // Get vertical start location
                int offsetY = item.YStart * nodeYSeperation;

                // Get the next horizontal offset and save
                currentX += 1;

                // Do the actual X layout
                int offsetX = 0;

                if (maxXNodes[currentY] == 1)
                {
                    offsetX = (levelXMax - nodeXSeperation) / 2;
                }
                else
                {
                    offsetX = (nodeXSeperation * currentX) + (levelNodeOffset * currentX);
                }

                // Save the location for use when laying out the edges
                PeopleGraphNode itemTemp = TreeGraph.FirstOrDefault(ie => ie.NodeHLink == item.NodeHLink);
                itemTemp.XStart = offsetX;
                itemTemp.YStart = offsetY;
                TreeGraph[i] = itemTemp;
            }

            // Allow for spacing TODO make more flexiable
            CanvasHeight = nodeYSeperation * numLevels;
            CanvasWidth = nodeXSeperation * maxLevelNodes;
        }

        /// <summary>
        /// Pres the layout.
        /// </summary>
        public void LayoutPreStart()
        {
            // Get max horizontal and max/min level
            minLevel = 0;
            maxLevel = 0;
            maxLevelNodes = 0;

            foreach (var item in maxXNodes)
            {
                if (item.Key < minLevel)
                {
                    minLevel = item.Key;
                }

                if (item.Key > maxLevel)
                {
                    maxLevel = item.Key;
                }

                if (item.Value > maxLevelNodes)
                {
                    maxLevelNodes = item.Value;
                }
            }

            // Convert relative levels to absolute starting at 0 Allows for easy layout on canvas
            int numNegativeLevels = Math.Abs(minLevel);
            numLevels = (Math.Abs(minLevel) + maxLevel) + 1;        // Plus 1 for the starting zero level

            for (int i = 0; i < TreeGraph.Count; i++)
            {
                PeopleGraphNode t = TreeGraph[i];

                t.YStart = TreeGraph[i].YStart + numNegativeLevels;

                TreeGraph[i] = t;
            }

            // offset the maxnodes list to match the TreeGraph absolute levels
            SortedDictionary<int, int> tempMaxX = new SortedDictionary<int, int>();

            foreach (var item in maxXNodes)
            {
                tempMaxX.Add(item.Key + numNegativeLevels, maxXNodes[item.Key]);
            }

            maxXNodes = tempMaxX;
        }

        /// <summary>
        /// Override for the OnNavigatedTo Prism method.
        /// </summary>
        public override void PopulateViewModel()
        {
            BaseCL.LogRoutineEntry("PeopleGraphViewModel");

            string startPoint = BaseNavParamsHLinkDefault(new HLinkBase { HLinkKey = "_c47a6bd11500b4b0cc8" }).HLinkKey;

            // Assume person
            PersonModel t = DV.PersonDV.GetModelFromHLinkString(startPoint);

            if (t.HLink.Valid == true)
            {
                StartHLink = t.HLink;
            }
            else
            {
                FamilyModel tf = DV.FamilyDV.GetModelFromHLinkString(startPoint);

                StartHLink = tf.HLink;
            }

            if (!StartHLink.Valid)
            {
                DataStore.CN.NotifyError("HLink passed to PersonGraph not found");
                return;
            }

            GraphReset();

            GetGraph();

            LayoutPreStart();

            LayoutNodes();

            LayoutEdges();
        }

        /// <summary>
        /// Adds the edge.
        /// </summary>
        /// <param name="argEdge">
        /// The argument edge.
        /// </param>
        private void AddEdge(EdgeItem argEdge)
        {
            if (!Edges.Contains(argEdge))
            {
                Edges.Add(argEdge);
            }
        }

        /// <summary>
        /// Adds the visited.
        /// </summary>
        /// <param name="argHLink">
        /// The argument h link.
        /// </param>
        /// <param name="argBool">
        /// if set to <c>true</c> [argument bool].
        /// </param>
        private void AddVisited(string argHLink, bool argBool)
        {
            if (!nodeVisited.ContainsKey(argHLink))
            {
                nodeVisited.Add(argHLink, argBool);
            }
        }

        /// <summary>
        /// Details on an edge between nodes.
        /// </summary>
        public struct EdgeItem
        {
            /// <summary>
            /// Gets or sets from.
            /// </summary>
            public string From { get; set; }

            /// <summary>
            /// Gets or sets the line.
            /// </summary>
            public object TheLine { get; set; }

            /// <summary>
            /// Gets or sets to.
            /// </summary>
            public string To { get; set; }

            public static bool operator !=(EdgeItem left, EdgeItem right)
            {
                return !(left == right);
            }

            public static bool operator ==(EdgeItem left, EdgeItem right)
            {
                return left.Equals(right);
            }

            public override bool Equals(object obj)
            {
                return GetHashCode() == obj.GetHashCode();
            }

            public override int GetHashCode()
            {
                return (From + To).GetHashCode();
            }
        }

        /// <summary>
        /// The next model to be looked at as we traverse the treee.
        /// </summary>
        private struct NextModel
        {
            /// <summary>
            /// The hlink.
            /// </summary>
            public HLinkBase HLink;

            /// <summary>
            /// The jumps from start node.
            /// </summary>
            public int JumpsFromStartNode;

            /// <summary>
            /// The level.
            /// </summary>
            public int Level;
        }
    }
}