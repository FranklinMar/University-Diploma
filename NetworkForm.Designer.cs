
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
            this.LabelPanel = new System.Windows.Forms.Panel();
            this.ProbLabel = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.ProbPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.ImportButton = new System.Windows.Forms.Button();
            this.ExportButton = new System.Windows.Forms.Button();
            this.CalcButton = new System.Windows.Forms.Button();
            this.TargetBox = new System.Windows.Forms.ComboBox();
            this.TargetLabel = new System.Windows.Forms.Label();
            this.SourceBox = new System.Windows.Forms.ComboBox();
            this.SourceLabel = new System.Windows.Forms.Label();
            this.TabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.SidePanel.SuspendLayout();
            this.LabelPanel.SuspendLayout();
            this.TabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
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
            this.SidePanel.Controls.Add(this.LabelPanel);
            this.SidePanel.Controls.Add(this.ProbPanel);
            this.SidePanel.Controls.Add(this.ImportButton);
            this.SidePanel.Controls.Add(this.ExportButton);
            this.SidePanel.Controls.Add(this.CalcButton);
            this.SidePanel.Controls.Add(this.TargetBox);
            this.SidePanel.Controls.Add(this.TargetLabel);
            this.SidePanel.Controls.Add(this.SourceBox);
            this.SidePanel.Controls.Add(this.SourceLabel);
            this.SidePanel.Location = new System.Drawing.Point(758, 0);
            this.SidePanel.Name = "SidePanel";
            this.SidePanel.Size = new System.Drawing.Size(223, 653);
            this.SidePanel.TabIndex = 2;
            // 
            // LabelPanel
            // 
            this.LabelPanel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.LabelPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(35)))));
            this.LabelPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.LabelPanel.Controls.Add(this.ProbLabel);
            this.LabelPanel.Controls.Add(this.button1);
            this.LabelPanel.Controls.Add(this.button2);
            this.LabelPanel.Controls.Add(this.label1);
            this.LabelPanel.Location = new System.Drawing.Point(0, 83);
            this.LabelPanel.Name = "LabelPanel";
            this.LabelPanel.Size = new System.Drawing.Size(223, 33);
            this.LabelPanel.TabIndex = 9;
            // 
            // ProbLabel
            // 
            this.ProbLabel.Font = new System.Drawing.Font("Roboto", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point);
            this.ProbLabel.Location = new System.Drawing.Point(1, 2);
            this.ProbLabel.Name = "ProbLabel";
            this.ProbLabel.Size = new System.Drawing.Size(215, 24);
            this.ProbLabel.TabIndex = 7;
            this.ProbLabel.Text = "Edge Success Probabilities";
            this.ProbLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button1.Location = new System.Drawing.Point(39, 504);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(151, 29);
            this.button1.TabIndex = 6;
            this.button1.Text = "Import Graph";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button2.Location = new System.Drawing.Point(39, 539);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(151, 29);
            this.button2.TabIndex = 5;
            this.button2.Text = "Export Graph";
            this.button2.UseVisualStyleBackColor = false;
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
            this.ProbPanel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.ProbPanel.AutoScroll = true;
            this.ProbPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(35)))));
            this.ProbPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ProbPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.ProbPanel.Location = new System.Drawing.Point(0, 116);
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
            this.ImportButton.Location = new System.Drawing.Point(20, 575);
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
            this.ExportButton.Location = new System.Drawing.Point(20, 610);
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
            this.CalcButton.Location = new System.Drawing.Point(20, 25);
            this.CalcButton.Name = "CalcButton";
            this.CalcButton.Size = new System.Drawing.Size(172, 29);
            this.CalcButton.TabIndex = 4;
            this.CalcButton.Text = "Calculations";
            this.CalcButton.UseVisualStyleBackColor = false;
            this.CalcButton.Click += new System.EventHandler(this.CalcClick);
            // 
            // TargetBox
            // 
            this.TargetBox.Enabled = false;
            this.TargetBox.FormattingEnabled = true;
            this.TargetBox.Location = new System.Drawing.Point(20, 83);
            this.TargetBox.Name = "TargetBox";
            this.TargetBox.Size = new System.Drawing.Size(151, 23);
            this.TargetBox.TabIndex = 3;
            this.TargetBox.Visible = false;
            this.TargetBox.SelectedValueChanged += new System.EventHandler(this.SelectionChanged);
            // 
            // TargetLabel
            // 
            this.TargetLabel.AutoSize = true;
            this.TargetLabel.Location = new System.Drawing.Point(20, 60);
            this.TargetLabel.Name = "TargetLabel";
            this.TargetLabel.Size = new System.Drawing.Size(79, 15);
            this.TargetLabel.TabIndex = 2;
            this.TargetLabel.Text = "Target Node:";
            this.TargetLabel.Visible = false;
            // 
            // SourceBox
            // 
            this.SourceBox.Enabled = false;
            this.SourceBox.FormattingEnabled = true;
            this.SourceBox.Location = new System.Drawing.Point(20, 30);
            this.SourceBox.Name = "SourceBox";
            this.SourceBox.Size = new System.Drawing.Size(151, 23);
            this.SourceBox.TabIndex = 1;
            this.SourceBox.Visible = false;
            this.SourceBox.SelectedValueChanged += new System.EventHandler(this.SelectionChanged);
            // 
            // SourceLabel
            // 
            this.SourceLabel.AutoSize = true;
            this.SourceLabel.Location = new System.Drawing.Point(20, 7);
            this.SourceLabel.Name = "SourceLabel";
            this.SourceLabel.Size = new System.Drawing.Size(83, 15);
            this.SourceLabel.TabIndex = 0;
            this.SourceLabel.Text = "Source Node:";
            this.SourceLabel.Visible = false;
            // 
            // TabControl
            // 
            this.TabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TabControl.Controls.Add(this.tabPage1);
            this.TabControl.Controls.Add(this.tabPage2);
            this.TabControl.Location = new System.Drawing.Point(0, 0);
            this.TabControl.Name = "TabControl";
            this.TabControl.SelectedIndex = 0;
            this.TabControl.Size = new System.Drawing.Size(757, 653);
            this.TabControl.TabIndex = 7;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.Browser);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(749, 625);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(749, 625);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // ModelForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(25)))));
            this.ClientSize = new System.Drawing.Size(982, 653);
            this.Controls.Add(this.TabControl);
            this.Controls.Add(this.SidePanel);
            this.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.Name = "ModelForm";
            this.Text = "NetworkForm";
            this.Load += new System.EventHandler(this.ModelForm_Load);
            this.SidePanel.ResumeLayout(false);
            this.SidePanel.PerformLayout();
            this.LabelPanel.ResumeLayout(false);
            this.LabelPanel.PerformLayout();
            this.TabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private CefSharp.WinForms.ChromiumWebBrowser Browser;
        private System.Windows.Forms.Panel SidePanel;
        private System.Windows.Forms.ComboBox SourceBox;
        private System.Windows.Forms.Label SourceLabel;
        private System.Windows.Forms.ComboBox TargetBox;
        private System.Windows.Forms.Label TargetLabel;
        private System.Windows.Forms.Button CalcButton;
        private System.Windows.Forms.Button ImportButton;
        private System.Windows.Forms.Button ExportButton;
        private System.Windows.Forms.TabControl TabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.FlowLayoutPanel ProbPanel;
        private System.Windows.Forms.Panel LabelPanel;
        private System.Windows.Forms.Label ProbLabel;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
    }
}