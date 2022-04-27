namespace Orienty_MapManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.panelLeft = new System.Windows.Forms.Panel();
            this.B_DrawBeacons = new System.Windows.Forms.Button();
            this.draw_Pav = new System.Windows.Forms.Button();
            this.B_drawOuterWalls = new System.Windows.Forms.Button();
            this.drawEdgeButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.deleteALLButton = new System.Windows.Forms.Button();
            this.selectButton = new System.Windows.Forms.Button();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.panelContextBeacon = new System.Windows.Forms.Panel();
            this.NUD_txPower = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.TB_Mac = new System.Windows.Forms.TextBox();
            this.panelContextVertex = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.RB_Junktion = new System.Windows.Forms.RadioButton();
            this.RB_Pavilion = new System.Windows.Forms.RadioButton();
            this.RB_Exit = new System.Windows.Forms.RadioButton();
            this.TB_Name = new System.Windows.Forms.TextBox();
            this.sheet = new System.Windows.Forms.PictureBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.FileBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.SendSrv = new System.Windows.Forms.ToolStripMenuItem();
            this.SetBackgrBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.panelLeft.SuspendLayout();
            this.mainPanel.SuspendLayout();
            this.panelContextBeacon.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_txPower)).BeginInit();
            this.panelContextVertex.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sheet)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelLeft
            // 
            this.panelLeft.AutoSize = true;
            this.panelLeft.BackColor = System.Drawing.SystemColors.Control;
            this.panelLeft.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelLeft.Controls.Add(this.B_DrawBeacons);
            this.panelLeft.Controls.Add(this.draw_Pav);
            this.panelLeft.Controls.Add(this.B_drawOuterWalls);
            this.panelLeft.Controls.Add(this.drawEdgeButton);
            this.panelLeft.Controls.Add(this.deleteButton);
            this.panelLeft.Controls.Add(this.deleteALLButton);
            this.panelLeft.Controls.Add(this.selectButton);
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLeft.Location = new System.Drawing.Point(0, 42);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new System.Drawing.Size(117, 1105);
            this.panelLeft.TabIndex = 16;
            // 
            // B_DrawBeacons
            // 
            this.B_DrawBeacons.Image = ((System.Drawing.Image)(resources.GetObject("B_DrawBeacons.Image")));
            this.B_DrawBeacons.Location = new System.Drawing.Point(9, 356);
            this.B_DrawBeacons.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.B_DrawBeacons.Name = "B_DrawBeacons";
            this.B_DrawBeacons.Size = new System.Drawing.Size(100, 100);
            this.B_DrawBeacons.TabIndex = 18;
            this.B_DrawBeacons.UseVisualStyleBackColor = true;
            this.B_DrawBeacons.Click += new System.EventHandler(this.B_DrawBeacons_Click);
            // 
            // draw_Pav
            // 
            this.draw_Pav.Image = ((System.Drawing.Image)(resources.GetObject("draw_Pav.Image")));
            this.draw_Pav.Location = new System.Drawing.Point(9, 581);
            this.draw_Pav.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.draw_Pav.Name = "draw_Pav";
            this.draw_Pav.Size = new System.Drawing.Size(100, 100);
            this.draw_Pav.TabIndex = 15;
            this.draw_Pav.UseVisualStyleBackColor = true;
            this.draw_Pav.Click += new System.EventHandler(this.draw_Pav_Click);
            // 
            // B_drawOuterWalls
            // 
            this.B_drawOuterWalls.Image = ((System.Drawing.Image)(resources.GetObject("B_drawOuterWalls.Image")));
            this.B_drawOuterWalls.Location = new System.Drawing.Point(9, 469);
            this.B_drawOuterWalls.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.B_drawOuterWalls.Name = "B_drawOuterWalls";
            this.B_drawOuterWalls.Size = new System.Drawing.Size(100, 100);
            this.B_drawOuterWalls.TabIndex = 14;
            this.B_drawOuterWalls.UseVisualStyleBackColor = true;
            this.B_drawOuterWalls.Click += new System.EventHandler(this.B_drawOuterWalls_Click);
            // 
            // drawEdgeButton
            // 
            this.drawEdgeButton.Image = ((System.Drawing.Image)(resources.GetObject("drawEdgeButton.Image")));
            this.drawEdgeButton.Location = new System.Drawing.Point(9, 245);
            this.drawEdgeButton.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.drawEdgeButton.Name = "drawEdgeButton";
            this.drawEdgeButton.Size = new System.Drawing.Size(100, 100);
            this.drawEdgeButton.TabIndex = 2;
            this.drawEdgeButton.UseVisualStyleBackColor = true;
            this.drawEdgeButton.Click += new System.EventHandler(this.drawEdgeButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.Image = ((System.Drawing.Image)(resources.GetObject("deleteButton.Image")));
            this.deleteButton.Location = new System.Drawing.Point(9, 133);
            this.deleteButton.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(100, 100);
            this.deleteButton.TabIndex = 3;
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // deleteALLButton
            // 
            this.deleteALLButton.Image = ((System.Drawing.Image)(resources.GetObject("deleteALLButton.Image")));
            this.deleteALLButton.Location = new System.Drawing.Point(9, 694);
            this.deleteALLButton.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.deleteALLButton.Name = "deleteALLButton";
            this.deleteALLButton.Size = new System.Drawing.Size(100, 100);
            this.deleteALLButton.TabIndex = 5;
            this.deleteALLButton.UseVisualStyleBackColor = true;
            this.deleteALLButton.Click += new System.EventHandler(this.deleteALLButton_Click);
            // 
            // selectButton
            // 
            this.selectButton.Image = ((System.Drawing.Image)(resources.GetObject("selectButton.Image")));
            this.selectButton.Location = new System.Drawing.Point(9, 20);
            this.selectButton.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.selectButton.Name = "selectButton";
            this.selectButton.Size = new System.Drawing.Size(100, 100);
            this.selectButton.TabIndex = 9;
            this.selectButton.UseVisualStyleBackColor = true;
            this.selectButton.Click += new System.EventHandler(this.selectButton_Click);
            // 
            // mainPanel
            // 
            this.mainPanel.Controls.Add(this.panelContextBeacon);
            this.mainPanel.Controls.Add(this.panelContextVertex);
            this.mainPanel.Controls.Add(this.sheet);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(117, 42);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(1663, 1105);
            this.mainPanel.TabIndex = 17;
            // 
            // panelContextBeacon
            // 
            this.panelContextBeacon.Controls.Add(this.NUD_txPower);
            this.panelContextBeacon.Controls.Add(this.label2);
            this.panelContextBeacon.Controls.Add(this.label1);
            this.panelContextBeacon.Controls.Add(this.TB_Mac);
            this.panelContextBeacon.Location = new System.Drawing.Point(858, 358);
            this.panelContextBeacon.Name = "panelContextBeacon";
            this.panelContextBeacon.Size = new System.Drawing.Size(339, 106);
            this.panelContextBeacon.TabIndex = 3;
            this.panelContextBeacon.Visible = false;
            // 
            // NUD_txPower
            // 
            this.NUD_txPower.Location = new System.Drawing.Point(116, 66);
            this.NUD_txPower.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.NUD_txPower.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.NUD_txPower.Name = "NUD_txPower";
            this.NUD_txPower.Size = new System.Drawing.Size(87, 31);
            this.NUD_txPower.TabIndex = 5;
            this.NUD_txPower.Value = new decimal(new int[] {
            69,
            0,
            0,
            -2147483648});
            this.NUD_txPower.ValueChanged += new System.EventHandler(this.NUD_txPower_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 25);
            this.label2.TabIndex = 4;
            this.label2.Text = "tx power";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 25);
            this.label1.TabIndex = 3;
            this.label1.Text = "mac";
            // 
            // TB_Mac
            // 
            this.TB_Mac.Location = new System.Drawing.Point(74, 17);
            this.TB_Mac.MaxLength = 20;
            this.TB_Mac.Name = "TB_Mac";
            this.TB_Mac.Size = new System.Drawing.Size(248, 31);
            this.TB_Mac.TabIndex = 2;
            this.TB_Mac.TextChanged += new System.EventHandler(this.TB_Mac_TextChanged);
            this.TB_Mac.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TB_Mac_KeyDown);
            // 
            // panelContextVertex
            // 
            this.panelContextVertex.Controls.Add(this.groupBox1);
            this.panelContextVertex.Controls.Add(this.TB_Name);
            this.panelContextVertex.Location = new System.Drawing.Point(490, 366);
            this.panelContextVertex.Name = "panelContextVertex";
            this.panelContextVertex.Size = new System.Drawing.Size(288, 205);
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
            this.groupBox1.Size = new System.Drawing.Size(282, 131);
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
            this.RB_Pavilion.Location = new System.Drawing.Point(15, 66);
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
            // sheet
            // 
            this.sheet.BackColor = System.Drawing.SystemColors.Control;
            this.sheet.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.sheet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sheet.Location = new System.Drawing.Point(0, 0);
            this.sheet.Margin = new System.Windows.Forms.Padding(0);
            this.sheet.Name = "sheet";
            this.sheet.Size = new System.Drawing.Size(1663, 1105);
            this.sheet.TabIndex = 0;
            this.sheet.TabStop = false;
            this.sheet.MouseClick += new System.Windows.Forms.MouseEventHandler(this.sheet_MouseClick);
            this.sheet.MouseMove += new System.Windows.Forms.MouseEventHandler(this.sheet_MouseMove);
            this.sheet.Resize += new System.EventHandler(this.sheet_Resize);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileBtn});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.menuStrip1.Size = new System.Drawing.Size(1780, 42);
            this.menuStrip1.TabIndex = 18;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // FileBtn
            // 
            this.FileBtn.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenBtn,
            this.SaveBtn,
            this.SendSrv,
            this.SetBackgrBtn});
            this.FileBtn.Name = "FileBtn";
            this.FileBtn.Size = new System.Drawing.Size(90, 36);
            this.FileBtn.Text = "Файл";
            // 
            // OpenBtn
            // 
            this.OpenBtn.Name = "OpenBtn";
            this.OpenBtn.Size = new System.Drawing.Size(382, 44);
            this.OpenBtn.Text = "Открыть...";
            this.OpenBtn.Click += new System.EventHandler(this.OpenBtn_Click);
            // 
            // SaveBtn
            // 
            this.SaveBtn.Name = "SaveBtn";
            this.SaveBtn.Size = new System.Drawing.Size(382, 44);
            this.SaveBtn.Text = "Сохранить...";
            this.SaveBtn.Click += new System.EventHandler(this.SaveBtn_Click);
            // 
            // SendSrv
            // 
            this.SendSrv.Name = "SendSrv";
            this.SendSrv.Size = new System.Drawing.Size(382, 44);
            this.SendSrv.Text = "Отправить на сервер";
            this.SendSrv.Click += new System.EventHandler(this.SendSrv_Click);
            // 
            // SetBackgrBtn
            // 
            this.SetBackgrBtn.Name = "SetBackgrBtn";
            this.SetBackgrBtn.Size = new System.Drawing.Size(382, 44);
            this.SetBackgrBtn.Text = "Установить фон";
            this.SetBackgrBtn.Click += new System.EventHandler(this.SetBackgrBtn_Click);
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1780, 1147);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.panelLeft);
            this.Controls.Add(this.menuStrip1);
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.MinimumSize = new System.Drawing.Size(996, 1015);
            this.Name = "Form1";
            this.Text = "Orienty: Map Manager";
            this.panelLeft.ResumeLayout(false);
            this.mainPanel.ResumeLayout(false);
            this.panelContextBeacon.ResumeLayout(false);
            this.panelContextBeacon.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_txPower)).EndInit();
            this.panelContextVertex.ResumeLayout(false);
            this.panelContextVertex.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sheet)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox sheet;
        private System.Windows.Forms.Button drawEdgeButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Button deleteALLButton;
        private System.Windows.Forms.Button selectButton;
        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.Button B_drawOuterWalls;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.Panel panelContextVertex;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton RB_Junktion;
        private System.Windows.Forms.RadioButton RB_Exit;
        private System.Windows.Forms.RadioButton RB_Pavilion;
        private System.Windows.Forms.TextBox TB_Name;
        private System.Windows.Forms.Button draw_Pav;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button B_DrawBeacons;
        private System.Windows.Forms.Panel panelContextBeacon;
        private System.Windows.Forms.NumericUpDown NUD_txPower;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TB_Mac;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem FileBtn;
        private System.Windows.Forms.ToolStripMenuItem OpenBtn;
        private System.Windows.Forms.ToolStripMenuItem SaveBtn;
        private System.Windows.Forms.ToolStripMenuItem SendSrv;
        private System.Windows.Forms.ToolStripMenuItem SetBackgrBtn;
    }
}

