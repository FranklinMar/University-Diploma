﻿
using System;

namespace University_Diploma
{
    partial class ModelForm
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
            this.ImportButton = new System.Windows.Forms.Button();
            this.ExportButton = new System.Windows.Forms.Button();
            this.CalcButton = new System.Windows.Forms.Button();
            this.TargetBox = new System.Windows.Forms.ComboBox();
            this.TargetLabel = new System.Windows.Forms.Label();
            this.SourceBox = new System.Windows.Forms.ComboBox();
            this.SourceLabel = new System.Windows.Forms.Label();
            this.SidePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // Browser
            // 
            this.Browser.ActivateBrowserOnCreation = false;
            this.Browser.Location = new System.Drawing.Point(0, 0);
            this.Browser.Name = "Browser";
            this.Browser.Size = new System.Drawing.Size(782, 653);
            this.Browser.TabIndex = 0;
            this.Browser.LoadingStateChanged += new System.EventHandler<CefSharp.LoadingStateChangedEventArgs>(this.PageLoaded);
            // 
            // SidePanel
            // 
            this.SidePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SidePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(35)))));
            this.SidePanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.SidePanel.Controls.Add(this.ImportButton);
            this.SidePanel.Controls.Add(this.ExportButton);
            this.SidePanel.Controls.Add(this.CalcButton);
            this.SidePanel.Controls.Add(this.TargetBox);
            this.SidePanel.Controls.Add(this.TargetLabel);
            this.SidePanel.Controls.Add(this.SourceBox);
            this.SidePanel.Controls.Add(this.SourceLabel);
            this.SidePanel.Location = new System.Drawing.Point(783, 0);
            this.SidePanel.Name = "SidePanel";
            this.SidePanel.Size = new System.Drawing.Size(198, 653);
            this.SidePanel.TabIndex = 2;
            // 
            // ImportButton
            // 
            this.ImportButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.ImportButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ImportButton.Location = new System.Drawing.Point(20, 575);
            this.ImportButton.Name = "ImportButton";
            this.ImportButton.Size = new System.Drawing.Size(151, 29);
            this.ImportButton.TabIndex = 6;
            this.ImportButton.Text = "Import Button";
            this.ImportButton.UseVisualStyleBackColor = false;
            this.ImportButton.Click += new System.EventHandler(this.ImportClick);
            // 
            // ExportButton
            // 
            this.ExportButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.ExportButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ExportButton.Location = new System.Drawing.Point(20, 610);
            this.ExportButton.Name = "ExportButton";
            this.ExportButton.Size = new System.Drawing.Size(151, 29);
            this.ExportButton.TabIndex = 5;
            this.ExportButton.Text = "Export Button";
            this.ExportButton.UseVisualStyleBackColor = false;
            this.ExportButton.Click += new System.EventHandler(this.ExportClick);
            // 
            // CalcButton
            // 
            this.CalcButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.CalcButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.CalcButton.Enabled = false;
            this.CalcButton.Location = new System.Drawing.Point(20, 159);
            this.CalcButton.Name = "CalcButton";
            this.CalcButton.Size = new System.Drawing.Size(151, 29);
            this.CalcButton.TabIndex = 4;
            this.CalcButton.Text = "Calculate";
            this.CalcButton.UseVisualStyleBackColor = false;
            this.CalcButton.Click += new System.EventHandler(this.CalcClick);
            // 
            // TargetBox
            // 
            this.TargetBox.Enabled = false;
            this.TargetBox.FormattingEnabled = true;
            this.TargetBox.Location = new System.Drawing.Point(20, 126);
            this.TargetBox.Name = "TargetBox";
            this.TargetBox.Size = new System.Drawing.Size(151, 23);
            this.TargetBox.TabIndex = 3;
            this.TargetBox.SelectedValueChanged += new System.EventHandler(this.SelectionChanged);
            // 
            // TargetLabel
            // 
            this.TargetLabel.AutoSize = true;
            this.TargetLabel.Location = new System.Drawing.Point(20, 103);
            this.TargetLabel.Name = "TargetLabel";
            this.TargetLabel.Size = new System.Drawing.Size(79, 15);
            this.TargetLabel.TabIndex = 2;
            this.TargetLabel.Text = "Target Node:";
            // 
            // SourceBox
            // 
            this.SourceBox.Enabled = false;
            this.SourceBox.FormattingEnabled = true;
            this.SourceBox.Location = new System.Drawing.Point(20, 73);
            this.SourceBox.Name = "SourceBox";
            this.SourceBox.Size = new System.Drawing.Size(151, 23);
            this.SourceBox.TabIndex = 1;
            this.SourceBox.SelectedValueChanged += new System.EventHandler(this.SelectionChanged);
            // 
            // SourceLabel
            // 
            this.SourceLabel.AutoSize = true;
            this.SourceLabel.Location = new System.Drawing.Point(20, 50);
            this.SourceLabel.Name = "SourceLabel";
            this.SourceLabel.Size = new System.Drawing.Size(83, 15);
            this.SourceLabel.TabIndex = 0;
            this.SourceLabel.Text = "Source Node:";
            // 
            // ModelForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(25)))));
            this.ClientSize = new System.Drawing.Size(982, 653);
            this.Controls.Add(this.SidePanel);
            this.Controls.Add(this.Browser);
            this.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.Name = "ModelForm";
            this.Text = "NetworkForm";
            this.SidePanel.ResumeLayout(false);
            this.SidePanel.PerformLayout();
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
    }
}