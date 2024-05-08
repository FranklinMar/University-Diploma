
namespace University_Diploma
{
    partial class NetworkForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NetworkForm));
            this.GraphViewer = new Microsoft.Msagl.GraphViewerGdi.GViewer();
            this.MenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addNode = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteNode = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteEdge = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // GraphViewer
            // 
            this.GraphViewer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GraphViewer.ArrowheadLength = 10D;
            this.GraphViewer.AsyncLayout = true;
            this.GraphViewer.AutoScroll = true;
            this.GraphViewer.BackColor = System.Drawing.Color.Transparent;
            this.GraphViewer.BackwardEnabled = false;
            this.GraphViewer.BuildHitTree = true;
            this.GraphViewer.CurrentLayoutMethod = Microsoft.Msagl.GraphViewerGdi.LayoutMethod.UseSettingsOfTheGraph;
            this.GraphViewer.EdgeInsertButtonVisible = true;
            this.GraphViewer.FileName = "";
            this.GraphViewer.ForwardEnabled = false;
            this.GraphViewer.Graph = null;
            this.GraphViewer.IncrementalDraggingModeAlways = false;
            this.GraphViewer.InsertingEdge = false;
            this.GraphViewer.LayoutAlgorithmSettingsButtonVisible = true;
            this.GraphViewer.LayoutEditingEnabled = true;
            this.GraphViewer.Location = new System.Drawing.Point(0, 0);
            this.GraphViewer.LooseOffsetForRouting = 0.25D;
            this.GraphViewer.MouseHitDistance = 0.05D;
            this.GraphViewer.Name = "GraphViewer";
            this.GraphViewer.NavigationVisible = true;
            this.GraphViewer.NeedToCalculateLayout = true;
            this.GraphViewer.OffsetForRelaxingInRouting = 0.6D;
            this.GraphViewer.PaddingForEdgeRouting = 8D;
            this.GraphViewer.PanButtonPressed = false;
            this.GraphViewer.SaveAsImageEnabled = false;
            this.GraphViewer.SaveAsMsaglEnabled = true;
            this.GraphViewer.SaveButtonVisible = true;
            this.GraphViewer.SaveGraphButtonVisible = true;
            this.GraphViewer.SaveInVectorFormatEnabled = false;
            this.GraphViewer.Size = new System.Drawing.Size(800, 561);
            this.GraphViewer.TabIndex = 0;
            this.GraphViewer.TightOffsetForRouting = 0.125D;
            this.GraphViewer.ToolBarIsVisible = true;
            this.GraphViewer.Transform = ((Microsoft.Msagl.Core.Geometry.Curves.PlaneTransformation)(resources.GetObject("GraphViewer.Transform")));
            this.GraphViewer.UndoRedoButtonsVisible = true;
            this.GraphViewer.WindowZoomButtonPressed = false;
            this.GraphViewer.ZoomF = 1D;
            this.GraphViewer.ZoomWindowThreshold = 1D;
            this.GraphViewer.Load += new System.EventHandler(this.ViewerLoaded);
            // 
            // MenuStrip
            // 
            this.MenuStrip.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addNode,
            this.deleteNode,
            this.deleteEdge});
            this.MenuStrip.Name = "MenuStrip";
            this.MenuStrip.Size = new System.Drawing.Size(181, 92);
            // 
            // addNode
            // 
            this.addNode.Name = "addNode";
            this.addNode.Size = new System.Drawing.Size(180, 22);
            this.addNode.Text = "Add Node";
            this.addNode.Click += new System.EventHandler(this.AddNodeClick);
            // 
            // deleteNode
            // 
            this.deleteNode.Name = "deleteNode";
            this.deleteNode.Size = new System.Drawing.Size(180, 22);
            this.deleteNode.Text = "Delete Node";
            this.deleteNode.Click += new System.EventHandler(this.DeleteNodeClick);
            // 
            // deleteEdge
            // 
            this.deleteEdge.Name = "deleteEdge";
            this.deleteEdge.Size = new System.Drawing.Size(180, 22);
            this.deleteEdge.Text = "Delete Edge";
            this.deleteEdge.Visible = false;
            this.deleteEdge.Click += new System.EventHandler(this.DeleteEdgeClick);
            // 
            // NetworkForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(25)))));
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Controls.Add(this.GraphViewer);
            this.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.Name = "NetworkForm";
            this.Text = "NetworkForm";
            this.MenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Msagl.GraphViewerGdi.GViewer GraphViewer;
        private System.Windows.Forms.ContextMenuStrip MenuStrip;
        private System.Windows.Forms.ToolStripMenuItem addNodeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteNodeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addNode;
        private System.Windows.Forms.ToolStripMenuItem deleteNode;
        private System.Windows.Forms.ToolStripMenuItem deleteEdgeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteEdge;
    }
}