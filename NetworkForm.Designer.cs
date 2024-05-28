
using System;

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
            this.Browser = new CefSharp.WinForms.ChromiumWebBrowser();
            this.SidePanel = new System.Windows.Forms.Panel();
            this.AddPageButton = new System.Windows.Forms.Button();
            this.LabelPanel = new System.Windows.Forms.Panel();
            this.ProbLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ProbPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.ImportButton = new System.Windows.Forms.Button();
            this.ExportButton = new System.Windows.Forms.Button();
            this.CalcButton = new System.Windows.Forms.Button();
            this.TabControl = new System.Windows.Forms.TabControl();
            this.Tab = new System.Windows.Forms.TabPage();
            this.SidePanel.SuspendLayout();
            this.LabelPanel.SuspendLayout();
            this.TabControl.SuspendLayout();
            this.Tab.SuspendLayout();
            this.SuspendLayout();
            // 
            // Browser
            // 
            this.Browser.ActivateBrowserOnCreation = false;
            this.Browser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Browser.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(14)))));
            this.Browser.Location = new System.Drawing.Point(0, 0);
            this.Browser.Name = "Browser";
            this.Browser.Size = new System.Drawing.Size(749, 625);
            this.Browser.TabIndex = 0;
            this.Browser.LoadingStateChanged += new System.EventHandler<CefSharp.LoadingStateChangedEventArgs>(this.PageLoaded);
            // 
            // SidePanel
            // 
            this.SidePanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SidePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(35)))));
            this.SidePanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.SidePanel.Controls.Add(this.AddPageButton);
            this.SidePanel.Controls.Add(this.LabelPanel);
            this.SidePanel.Controls.Add(this.ProbPanel);
            this.SidePanel.Controls.Add(this.ImportButton);
            this.SidePanel.Controls.Add(this.ExportButton);
            this.SidePanel.Controls.Add(this.CalcButton);
            this.SidePanel.Location = new System.Drawing.Point(759, 0);
            this.SidePanel.Name = "SidePanel";
            this.SidePanel.Size = new System.Drawing.Size(222, 653);
            this.SidePanel.TabIndex = 2;
            // 
            // AddPageButton
            // 
            this.AddPageButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AddPageButton.BackColor = System.Drawing.Color.Green;
            this.AddPageButton.Location = new System.Drawing.Point(-4, -2);
            this.AddPageButton.Name = "AddPageButton";
            this.AddPageButton.Size = new System.Drawing.Size(25, 25);
            this.AddPageButton.TabIndex = 1;
            this.AddPageButton.Text = "+";
            this.AddPageButton.UseVisualStyleBackColor = false;
            this.AddPageButton.Click += new System.EventHandler(this.AddPageClick);
            // 
            // LabelPanel
            // 
            this.LabelPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LabelPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(35)))));
            this.LabelPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.LabelPanel.Controls.Add(this.ProbLabel);
            this.LabelPanel.Controls.Add(this.label1);
            this.LabelPanel.Location = new System.Drawing.Point(-1, 83);
            this.LabelPanel.Name = "LabelPanel";
            this.LabelPanel.Size = new System.Drawing.Size(223, 33);
            this.LabelPanel.TabIndex = 9;
            // 
            // ProbLabel
            // 
            this.ProbLabel.Font = new System.Drawing.Font("Roboto", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point);
            this.ProbLabel.Location = new System.Drawing.Point(-1, 3);
            this.ProbLabel.Name = "ProbLabel";
            this.ProbLabel.Size = new System.Drawing.Size(215, 24);
            this.ProbLabel.TabIndex = 7;
            this.ProbLabel.Text = "Edge Success Probabilities";
            this.ProbLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 15);
            this.label1.TabIndex = 2;
            this.label1.Visible = false;
            // 
            // ProbPanel
            // 
            this.ProbPanel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.ProbPanel.AutoScroll = true;
            this.ProbPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(35)))));
            this.ProbPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ProbPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.ProbPanel.Location = new System.Drawing.Point(-1, 116);
            this.ProbPanel.Name = "ProbPanel";
            this.ProbPanel.Padding = new System.Windows.Forms.Padding(0, 15, 0, 15);
            this.ProbPanel.Size = new System.Drawing.Size(223, 449);
            this.ProbPanel.TabIndex = 8;
            this.ProbPanel.WrapContents = false;
            // 
            // ImportButton
            // 
            this.ImportButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ImportButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.ImportButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ImportButton.Location = new System.Drawing.Point(19, 576);
            this.ImportButton.Name = "ImportButton";
            this.ImportButton.Size = new System.Drawing.Size(172, 29);
            this.ImportButton.TabIndex = 6;
            this.ImportButton.Text = "Import Graph";
            this.ImportButton.UseVisualStyleBackColor = false;
            this.ImportButton.Click += new System.EventHandler(this.ImportClick);
            // 
            // ExportButton
            // 
            this.ExportButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ExportButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.ExportButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ExportButton.Location = new System.Drawing.Point(19, 610);
            this.ExportButton.Name = "ExportButton";
            this.ExportButton.Size = new System.Drawing.Size(172, 29);
            this.ExportButton.TabIndex = 5;
            this.ExportButton.Text = "Export Graph";
            this.ExportButton.UseVisualStyleBackColor = false;
            this.ExportButton.Click += new System.EventHandler(this.ExportClick);
            // 
            // CalcButton
            // 
            this.CalcButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CalcButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.CalcButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.CalcButton.Enabled = false;
            this.CalcButton.Location = new System.Drawing.Point(19, 29);
            this.CalcButton.Name = "CalcButton";
            this.CalcButton.Size = new System.Drawing.Size(172, 29);
            this.CalcButton.TabIndex = 4;
            this.CalcButton.Text = "Calculations";
            this.CalcButton.UseVisualStyleBackColor = false;
            this.CalcButton.Click += new System.EventHandler(this.CalcClick);
            // 
            // TabControl
            // 
            this.TabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TabControl.Controls.Add(this.Tab);
            this.TabControl.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.TabControl.Location = new System.Drawing.Point(0, 0);
            this.TabControl.Name = "TabControl";
            this.TabControl.SelectedIndex = 0;
            this.TabControl.Size = new System.Drawing.Size(757, 653);
            this.TabControl.TabIndex = 0;
            this.TabControl.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.TabDraw);
            this.TabControl.SelectedIndexChanged += new System.EventHandler(this.SelectedChanged);
            this.TabControl.MouseClick += new System.Windows.Forms.MouseEventHandler(this.TabClose);
            // 
            // Tab
            // 
            this.Tab.Controls.Add(this.Browser);
            this.Tab.Location = new System.Drawing.Point(4, 24);
            this.Tab.Name = "Tab";
            this.Tab.Padding = new System.Windows.Forms.Padding(3);
            this.Tab.Size = new System.Drawing.Size(749, 625);
            this.Tab.TabIndex = 0;
            this.Tab.Text = "Graph 1   ";
            this.Tab.UseVisualStyleBackColor = true;
            // 
            // NetworkForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(25)))));
            this.ClientSize = new System.Drawing.Size(982, 653);
            this.Controls.Add(this.TabControl);
            this.Controls.Add(this.SidePanel);
            this.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.Name = "NetworkForm";
            this.Text = "Network Modeller";
            this.Load += new System.EventHandler(this.Loaded);
            this.SidePanel.ResumeLayout(false);
            this.LabelPanel.ResumeLayout(false);
            this.LabelPanel.PerformLayout();
            this.TabControl.ResumeLayout(false);
            this.Tab.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private CefSharp.WinForms.ChromiumWebBrowser Browser;
        private System.Windows.Forms.Panel SidePanel;
        private System.Windows.Forms.Button CalcButton;
        private System.Windows.Forms.Button ImportButton;
        private System.Windows.Forms.TabControl TabControl;
        private System.Windows.Forms.TabPage Tab;
        private System.Windows.Forms.FlowLayoutPanel ProbPanel;
        private System.Windows.Forms.Panel LabelPanel;
        private System.Windows.Forms.Label ProbLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button AddPageButton;
        private System.Windows.Forms.Button ExportButton;
    }
}