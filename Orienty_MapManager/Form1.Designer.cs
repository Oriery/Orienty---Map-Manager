﻿namespace Orienty_MapManager
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.selectButton = new System.Windows.Forms.Button();
            this.deleteALLButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.drawEdgeButton = new System.Windows.Forms.Button();
            this.drawVertexButton = new System.Windows.Forms.Button();
            this.sheet = new System.Windows.Forms.PictureBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.TB_Debug = new System.Windows.Forms.TextBox();
            this.panelLeft = new System.Windows.Forms.Panel();
            this.B_drawOuterWalls = new System.Windows.Forms.Button();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.panelContextVertex = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.RB_Junktion = new System.Windows.Forms.RadioButton();
            this.RB_Pavilion = new System.Windows.Forms.RadioButton();
            this.RB_Exit = new System.Windows.Forms.RadioButton();
            this.TB_Name = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.sheet)).BeginInit();
            this.panelLeft.SuspendLayout();
            this.mainPanel.SuspendLayout();
            this.panelContextVertex.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // selectButton
            // 
            this.selectButton.Image = global::Orienty_MapManager.Properties.Resources.cursor;
            this.selectButton.Location = new System.Drawing.Point(15, 15);
            this.selectButton.Margin = new System.Windows.Forms.Padding(6);
            this.selectButton.Name = "selectButton";
            this.selectButton.Size = new System.Drawing.Size(100, 100);
            this.selectButton.TabIndex = 9;
            this.selectButton.UseVisualStyleBackColor = true;
            this.selectButton.Click += new System.EventHandler(this.selectButton_Click);
            // 
            // deleteALLButton
            // 
            this.deleteALLButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.deleteALLButton.Image = global::Orienty_MapManager.Properties.Resources.deleteAll;
            this.deleteALLButton.Location = new System.Drawing.Point(15, 1014);
            this.deleteALLButton.Margin = new System.Windows.Forms.Padding(6);
            this.deleteALLButton.Name = "deleteALLButton";
            this.deleteALLButton.Size = new System.Drawing.Size(100, 100);
            this.deleteALLButton.TabIndex = 5;
            this.deleteALLButton.UseVisualStyleBackColor = true;
            this.deleteALLButton.Click += new System.EventHandler(this.deleteALLButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.Image = global::Orienty_MapManager.Properties.Resources.delete;
            this.deleteButton.Location = new System.Drawing.Point(127, 15);
            this.deleteButton.Margin = new System.Windows.Forms.Padding(6);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(100, 100);
            this.deleteButton.TabIndex = 3;
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // drawEdgeButton
            // 
            this.drawEdgeButton.Image = global::Orienty_MapManager.Properties.Resources.edge;
            this.drawEdgeButton.Location = new System.Drawing.Point(127, 127);
            this.drawEdgeButton.Margin = new System.Windows.Forms.Padding(6);
            this.drawEdgeButton.Name = "drawEdgeButton";
            this.drawEdgeButton.Size = new System.Drawing.Size(100, 100);
            this.drawEdgeButton.TabIndex = 2;
            this.drawEdgeButton.UseVisualStyleBackColor = true;
            this.drawEdgeButton.Click += new System.EventHandler(this.drawEdgeButton_Click);
            // 
            // drawVertexButton
            // 
            this.drawVertexButton.Image = global::Orienty_MapManager.Properties.Resources.vertex;
            this.drawVertexButton.Location = new System.Drawing.Point(15, 127);
            this.drawVertexButton.Margin = new System.Windows.Forms.Padding(6);
            this.drawVertexButton.Name = "drawVertexButton";
            this.drawVertexButton.Size = new System.Drawing.Size(100, 100);
            this.drawVertexButton.TabIndex = 1;
            this.drawVertexButton.UseVisualStyleBackColor = true;
            this.drawVertexButton.Click += new System.EventHandler(this.drawVertexButton_Click);
            // 
            // sheet
            // 
            this.sheet.BackColor = System.Drawing.SystemColors.Control;
            this.sheet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sheet.Location = new System.Drawing.Point(0, 0);
            this.sheet.Margin = new System.Windows.Forms.Padding(0);
            this.sheet.Name = "sheet";
            this.sheet.Size = new System.Drawing.Size(1421, 1129);
            this.sheet.TabIndex = 0;
            this.sheet.TabStop = false;
            this.sheet.MouseClick += new System.Windows.Forms.MouseEventHandler(this.sheet_MouseClick);
            this.sheet.MouseMove += new System.Windows.Forms.MouseEventHandler(this.sheet_MouseMove);
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(24, 360);
            this.saveButton.Margin = new System.Windows.Forms.Padding(15);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(185, 44);
            this.saveButton.TabIndex = 13;
            this.saveButton.Text = "Сохранить";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // TB_Debug
            // 
            this.TB_Debug.Location = new System.Drawing.Point(239, 12);
            this.TB_Debug.Multiline = true;
            this.TB_Debug.Name = "TB_Debug";
            this.TB_Debug.Size = new System.Drawing.Size(368, 379);
            this.TB_Debug.TabIndex = 14;
            // 
            // panelLeft
            // 
            this.panelLeft.AutoSize = true;
            this.panelLeft.BackColor = System.Drawing.SystemColors.Control;
            this.panelLeft.Controls.Add(this.B_drawOuterWalls);
            this.panelLeft.Controls.Add(this.drawVertexButton);
            this.panelLeft.Controls.Add(this.drawEdgeButton);
            this.panelLeft.Controls.Add(this.deleteButton);
            this.panelLeft.Controls.Add(this.saveButton);
            this.panelLeft.Controls.Add(this.deleteALLButton);
            this.panelLeft.Controls.Add(this.selectButton);
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLeft.Location = new System.Drawing.Point(0, 0);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new System.Drawing.Size(233, 1129);
            this.panelLeft.TabIndex = 16;
            // 
            // B_drawOuterWalls
            // 
            this.B_drawOuterWalls.Location = new System.Drawing.Point(15, 239);
            this.B_drawOuterWalls.Margin = new System.Windows.Forms.Padding(6);
            this.B_drawOuterWalls.Name = "B_drawOuterWalls";
            this.B_drawOuterWalls.Size = new System.Drawing.Size(100, 100);
            this.B_drawOuterWalls.TabIndex = 14;
            this.B_drawOuterWalls.UseVisualStyleBackColor = true;
            this.B_drawOuterWalls.Click += new System.EventHandler(this.B_drawOuterWalls_Click);
            // 
            // mainPanel
            // 
            this.mainPanel.Controls.Add(this.panelContextVertex);
            this.mainPanel.Controls.Add(this.sheet);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(233, 0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(1421, 1129);
            this.mainPanel.TabIndex = 17;
            // 
            // panelContextVertex
            // 
            this.panelContextVertex.Controls.Add(this.groupBox1);
            this.panelContextVertex.Controls.Add(this.TB_Name);
            this.panelContextVertex.Location = new System.Drawing.Point(374, 517);
            this.panelContextVertex.Name = "panelContextVertex";
            this.panelContextVertex.Size = new System.Drawing.Size(288, 204);
            this.panelContextVertex.TabIndex = 1;
            this.panelContextVertex.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.RB_Junktion);
            this.groupBox1.Controls.Add(this.RB_Pavilion);
            this.groupBox1.Controls.Add(this.RB_Exit);
            this.groupBox1.Location = new System.Drawing.Point(3, 69);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(282, 132);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Тип";
            // 
            // RB_Junktion
            // 
            this.RB_Junktion.AutoSize = true;
            this.RB_Junktion.Checked = true;
            this.RB_Junktion.Location = new System.Drawing.Point(15, 30);
            this.RB_Junktion.Name = "RB_Junktion";
            this.RB_Junktion.Size = new System.Drawing.Size(173, 29);
            this.RB_Junktion.TabIndex = 0;
            this.RB_Junktion.TabStop = true;
            this.RB_Junktion.Tag = "Junktion";
            this.RB_Junktion.Text = "Перекрёсток";
            this.RB_Junktion.UseVisualStyleBackColor = true;
            this.RB_Junktion.CheckedChanged += new System.EventHandler(this.RB_Type_CheckedChanged);
            // 
            // RB_Pavilion
            // 
            this.RB_Pavilion.AutoSize = true;
            this.RB_Pavilion.Location = new System.Drawing.Point(15, 65);
            this.RB_Pavilion.Name = "RB_Pavilion";
            this.RB_Pavilion.Size = new System.Drawing.Size(140, 29);
            this.RB_Pavilion.TabIndex = 1;
            this.RB_Pavilion.Tag = "Pavilion";
            this.RB_Pavilion.Text = "Павильон";
            this.RB_Pavilion.UseVisualStyleBackColor = true;
            this.RB_Pavilion.CheckedChanged += new System.EventHandler(this.RB_Type_CheckedChanged);
            // 
            // RB_Exit
            // 
            this.RB_Exit.AutoSize = true;
            this.RB_Exit.Location = new System.Drawing.Point(15, 100);
            this.RB_Exit.Name = "RB_Exit";
            this.RB_Exit.Size = new System.Drawing.Size(107, 29);
            this.RB_Exit.TabIndex = 2;
            this.RB_Exit.Tag = "Exit";
            this.RB_Exit.Text = "Выход";
            this.RB_Exit.UseVisualStyleBackColor = true;
            this.RB_Exit.CheckedChanged += new System.EventHandler(this.RB_Type_CheckedChanged);
            // 
            // TB_Name
            // 
            this.TB_Name.Location = new System.Drawing.Point(18, 17);
            this.TB_Name.MaxLength = 20;
            this.TB_Name.Name = "TB_Name";
            this.TB_Name.Size = new System.Drawing.Size(252, 31);
            this.TB_Name.TabIndex = 2;
            this.TB_Name.TextChanged += new System.EventHandler(this.TB_Name_TextChanged);
            this.TB_Name.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.TB_Name_PreviewKeyDown);
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1654, 1129);
            this.Controls.Add(this.TB_Debug);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.panelLeft);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(6);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Orienty: Map Manager";
            ((System.ComponentModel.ISupportInitialize)(this.sheet)).EndInit();
            this.panelLeft.ResumeLayout(false);
            this.mainPanel.ResumeLayout(false);
            this.panelContextVertex.ResumeLayout(false);
            this.panelContextVertex.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox sheet;
        private System.Windows.Forms.Button drawVertexButton;
        private System.Windows.Forms.Button drawEdgeButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Button deleteALLButton;
        private System.Windows.Forms.Button selectButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.TextBox TB_Debug;
        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.Button B_drawOuterWalls;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.Panel panelContextVertex;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton RB_Junktion;
        private System.Windows.Forms.RadioButton RB_Exit;
        private System.Windows.Forms.RadioButton RB_Pavilion;
        private System.Windows.Forms.TextBox TB_Name;
    }
}

