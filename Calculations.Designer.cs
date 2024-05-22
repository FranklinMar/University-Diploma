
namespace University_Diploma
{
    partial class Calculations
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
            this.TextPaths = new System.Windows.Forms.TextBox();
            this.PathCaption = new System.Windows.Forms.Label();
            this.CutsCaption = new System.Windows.Forms.Label();
            this.TextCuts = new System.Windows.Forms.TextBox();
            this.LowerLabel = new System.Windows.Forms.Label();
            this.TextLower = new System.Windows.Forms.TextBox();
            this.UpperLabel = new System.Windows.Forms.Label();
            this.TextUpper = new System.Windows.Forms.TextBox();
            this.CalcLabel = new System.Windows.Forms.Label();
            this.LowerResult = new System.Windows.Forms.Label();
            this.CalcLabel2 = new System.Windows.Forms.Label();
            this.UpperResult = new System.Windows.Forms.Label();
            this.TargetBox = new System.Windows.Forms.ComboBox();
            this.TargetLabel = new System.Windows.Forms.Label();
            this.SourceBox = new System.Windows.Forms.ComboBox();
            this.SourceLabel = new System.Windows.Forms.Label();
            this.CalcButton = new System.Windows.Forms.Button();
            this.PlotPanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // TextPaths
            // 
            this.TextPaths.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.TextPaths.ForeColor = System.Drawing.Color.White;
            this.TextPaths.Location = new System.Drawing.Point(13, 60);
            this.TextPaths.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.TextPaths.Multiline = true;
            this.TextPaths.Name = "TextPaths";
            this.TextPaths.ReadOnly = true;
            this.TextPaths.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TextPaths.Size = new System.Drawing.Size(470, 111);
            this.TextPaths.TabIndex = 0;
            // 
            // PathCaption
            // 
            this.PathCaption.AutoSize = true;
            this.PathCaption.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.PathCaption.Location = new System.Drawing.Point(15, 24);
            this.PathCaption.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.PathCaption.Name = "PathCaption";
            this.PathCaption.Size = new System.Drawing.Size(204, 23);
            this.PathCaption.TabIndex = 1;
            this.PathCaption.Text = "Minimal Paths | α(X)";
            // 
            // CutsCaption
            // 
            this.CutsCaption.AutoSize = true;
            this.CutsCaption.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.CutsCaption.Location = new System.Drawing.Point(501, 24);
            this.CutsCaption.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.CutsCaption.Name = "CutsCaption";
            this.CutsCaption.Size = new System.Drawing.Size(193, 23);
            this.CutsCaption.TabIndex = 3;
            this.CutsCaption.Text = "Minimal Cuts | β(X)";
            // 
            // TextCuts
            // 
            this.TextCuts.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.TextCuts.ForeColor = System.Drawing.Color.White;
            this.TextCuts.Location = new System.Drawing.Point(501, 60);
            this.TextCuts.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.TextCuts.Multiline = true;
            this.TextCuts.Name = "TextCuts";
            this.TextCuts.ReadOnly = true;
            this.TextCuts.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TextCuts.Size = new System.Drawing.Size(470, 111);
            this.TextCuts.TabIndex = 0;
            // 
            // LowerLabel
            // 
            this.LowerLabel.AutoSize = true;
            this.LowerLabel.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.LowerLabel.Location = new System.Drawing.Point(501, 188);
            this.LowerLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LowerLabel.Name = "LowerLabel";
            this.LowerLabel.Size = new System.Drawing.Size(344, 23);
            this.LowerLabel.TabIndex = 5;
            this.LowerLabel.Text = "Lower Limit | h̲ₑₚ = ∏₁ ≤ ₖ ≤ ₙ (1 - ∏qᵢ)";
            // 
            // TextLower
            // 
            this.TextLower.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.TextLower.ForeColor = System.Drawing.Color.White;
            this.TextLower.Location = new System.Drawing.Point(501, 224);
            this.TextLower.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.TextLower.Multiline = true;
            this.TextLower.Name = "TextLower";
            this.TextLower.ReadOnly = true;
            this.TextLower.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TextLower.Size = new System.Drawing.Size(470, 111);
            this.TextLower.TabIndex = 0;
            // 
            // UpperLabel
            // 
            this.UpperLabel.AutoSize = true;
            this.UpperLabel.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.UpperLabel.Location = new System.Drawing.Point(15, 188);
            this.UpperLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.UpperLabel.Name = "UpperLabel";
            this.UpperLabel.Size = new System.Drawing.Size(377, 23);
            this.UpperLabel.TabIndex = 7;
            this.UpperLabel.Text = "Upper Limit | h̅ₑₚ = 1 - ∏₁ ≤ ⱼ ≤ ₘ (1 - ∏pₒ)";
            // 
            // TextUpper
            // 
            this.TextUpper.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.TextUpper.ForeColor = System.Drawing.Color.White;
            this.TextUpper.Location = new System.Drawing.Point(13, 224);
            this.TextUpper.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.TextUpper.Multiline = true;
            this.TextUpper.Name = "TextUpper";
            this.TextUpper.ReadOnly = true;
            this.TextUpper.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TextUpper.Size = new System.Drawing.Size(470, 111);
            this.TextUpper.TabIndex = 0;
            // 
            // CalcLabel
            // 
            this.CalcLabel.AutoSize = true;
            this.CalcLabel.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.CalcLabel.Location = new System.Drawing.Point(13, 726);
            this.CalcLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.CalcLabel.Name = "CalcLabel";
            this.CalcLabel.Size = new System.Drawing.Size(174, 19);
            this.CalcLabel.TabIndex = 8;
            this.CalcLabel.Text = "Calculations Result: ";
            // 
            // LowerResult
            // 
            this.LowerResult.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.LowerResult.Location = new System.Drawing.Point(175, 726);
            this.LowerResult.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LowerResult.Name = "LowerResult";
            this.LowerResult.Size = new System.Drawing.Size(45, 21);
            this.LowerResult.TabIndex = 9;
            this.LowerResult.Text = "0,00";
            this.LowerResult.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // CalcLabel2
            // 
            this.CalcLabel2.AutoSize = true;
            this.CalcLabel2.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.CalcLabel2.Location = new System.Drawing.Point(219, 726);
            this.CalcLabel2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.CalcLabel2.Name = "CalcLabel2";
            this.CalcLabel2.Size = new System.Drawing.Size(88, 19);
            this.CalcLabel2.TabIndex = 10;
            this.CalcLabel2.Text = "≤ M Ф (X) ≤";
            // 
            // UpperResult
            // 
            this.UpperResult.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.UpperResult.Location = new System.Drawing.Point(305, 726);
            this.UpperResult.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.UpperResult.Name = "UpperResult";
            this.UpperResult.Size = new System.Drawing.Size(46, 21);
            this.UpperResult.TabIndex = 11;
            this.UpperResult.Text = "1,00";
            // 
            // TargetBox
            // 
            this.TargetBox.Enabled = false;
            this.TargetBox.FormattingEnabled = true;
            this.TargetBox.Location = new System.Drawing.Point(346, 341);
            this.TargetBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.TargetBox.Name = "TargetBox";
            this.TargetBox.Size = new System.Drawing.Size(120, 23);
            this.TargetBox.TabIndex = 15;
            this.TargetBox.SelectedValueChanged += new System.EventHandler(this.SelectionChanged);
            // 
            // TargetLabel
            // 
            this.TargetLabel.AutoSize = true;
            this.TargetLabel.Font = new System.Drawing.Font("Roboto", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.TargetLabel.Location = new System.Drawing.Point(251, 343);
            this.TargetLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.TargetLabel.Name = "TargetLabel";
            this.TargetLabel.Size = new System.Drawing.Size(87, 17);
            this.TargetLabel.TabIndex = 14;
            this.TargetLabel.Text = "Target Node:";
            // 
            // SourceBox
            // 
            this.SourceBox.Enabled = false;
            this.SourceBox.FormattingEnabled = true;
            this.SourceBox.Location = new System.Drawing.Point(116, 341);
            this.SourceBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.SourceBox.Name = "SourceBox";
            this.SourceBox.Size = new System.Drawing.Size(120, 23);
            this.SourceBox.TabIndex = 13;
            this.SourceBox.SelectedValueChanged += new System.EventHandler(this.SelectionChanged);
            // 
            // SourceLabel
            // 
            this.SourceLabel.AutoSize = true;
            this.SourceLabel.Font = new System.Drawing.Font("Roboto", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.SourceLabel.Location = new System.Drawing.Point(18, 343);
            this.SourceLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.SourceLabel.Name = "SourceLabel";
            this.SourceLabel.Size = new System.Drawing.Size(90, 17);
            this.SourceLabel.TabIndex = 12;
            this.SourceLabel.Text = "Source Node:";
            // 
            // CalcButton
            // 
            this.CalcButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.CalcButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.CalcButton.Enabled = false;
            this.CalcButton.Font = new System.Drawing.Font("Roboto", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.CalcButton.Location = new System.Drawing.Point(583, 341);
            this.CalcButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.CalcButton.Name = "CalcButton";
            this.CalcButton.Size = new System.Drawing.Size(303, 30);
            this.CalcButton.TabIndex = 16;
            this.CalcButton.Text = "Calculate";
            this.CalcButton.UseVisualStyleBackColor = false;
            this.CalcButton.Click += new System.EventHandler(this.CalcClick);
            // 
            // PlotPanel
            // 
            this.PlotPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.PlotPanel.Location = new System.Drawing.Point(474, 377);
            this.PlotPanel.Name = "PlotPanel";
            this.PlotPanel.Size = new System.Drawing.Size(497, 370);
            this.PlotPanel.TabIndex = 17;
            // 
            // Calculations
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(35)))));
            this.ClientSize = new System.Drawing.Size(984, 761);
            this.Controls.Add(this.PlotPanel);
            this.Controls.Add(this.CalcButton);
            this.Controls.Add(this.TargetBox);
            this.Controls.Add(this.TargetLabel);
            this.Controls.Add(this.SourceBox);
            this.Controls.Add(this.SourceLabel);
            this.Controls.Add(this.UpperResult);
            this.Controls.Add(this.CalcLabel2);
            this.Controls.Add(this.LowerResult);
            this.Controls.Add(this.CalcLabel);
            this.Controls.Add(this.UpperLabel);
            this.Controls.Add(this.TextUpper);
            this.Controls.Add(this.LowerLabel);
            this.Controls.Add(this.TextLower);
            this.Controls.Add(this.CutsCaption);
            this.Controls.Add(this.TextCuts);
            this.Controls.Add(this.PathCaption);
            this.Controls.Add(this.TextPaths);
            this.Font = new System.Drawing.Font("Roboto", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.ForeColor = System.Drawing.Color.White;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "Calculations";
            this.Text = "Calculations Tab";
            this.Load += new System.EventHandler(this.Loaded);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TextPaths;
        private System.Windows.Forms.Label PathCaption;
        private System.Windows.Forms.Label CutsCaption;
        private System.Windows.Forms.TextBox TextCuts;
        private System.Windows.Forms.Label LowerLabel;
        private System.Windows.Forms.TextBox TextLower;
        private System.Windows.Forms.Label UpperLabel;
        private System.Windows.Forms.TextBox TextUpper;
        private System.Windows.Forms.Label CalcLabel;
        private System.Windows.Forms.Label LowerResult;
        private System.Windows.Forms.Label CalcLabel2;
        private System.Windows.Forms.Label UpperResult;
        private System.Windows.Forms.ComboBox TargetBox;
        private System.Windows.Forms.Label TargetLabel;
        private System.Windows.Forms.ComboBox SourceBox;
        private System.Windows.Forms.Label SourceLabel;
        private System.Windows.Forms.Button CalcButton;
        private System.Windows.Forms.Panel PlotPanel;
    }
}