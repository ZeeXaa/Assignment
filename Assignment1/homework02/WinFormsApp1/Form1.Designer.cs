namespace WinFormsApp1
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            button1 = new Button();
            contextMenuStrip1 = new ContextMenuStrip(components);
            toolStripMenuItem2 = new ToolStripMenuItem();
            减ToolStripMenuItem = new ToolStripMenuItem();
            乘ToolStripMenuItem = new ToolStripMenuItem();
            除ToolStripMenuItem = new ToolStripMenuItem();
            button2 = new Button();
            contextMenuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.Location = new Point(182, 103);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(107, 30);
            textBox1.TabIndex = 0;
            textBox1.Text = "请输入第一个数字";
            textBox1.TextAlign = HorizontalAlignment.Center;
            textBox1.Click += textBox1_Click;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(320, 103);
            textBox2.Multiline = true;
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(107, 30);
            textBox2.TabIndex = 1;
            textBox2.Text = "请输入第二个数字";
            textBox2.TextAlign = HorizontalAlignment.Center;
            textBox2.Click += textBox2_Click;
            textBox2.TextChanged += textBox2_TextChanged;
            // 
            // button1
            // 
            button1.ContextMenuStrip = contextMenuStrip1;
            button1.Location = new Point(451, 103);
            button1.Name = "button1";
            button1.Size = new Size(112, 30);
            button1.TabIndex = 2;
            button1.Text = "请选择运算符";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { toolStripMenuItem2, 减ToolStripMenuItem, 乘ToolStripMenuItem, 除ToolStripMenuItem });
            contextMenuStrip1.LayoutStyle = ToolStripLayoutStyle.Table;
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(89, 92);
            contextMenuStrip1.Click += contextMenuStrip1_Click;
            // 
            // toolStripMenuItem2
            // 
            toolStripMenuItem2.Name = "toolStripMenuItem2";
            toolStripMenuItem2.Size = new Size(88, 22);
            toolStripMenuItem2.Text = "加";
            toolStripMenuItem2.Click += 加ToolStripMenuItem_Click;
            // 
            // 减ToolStripMenuItem
            // 
            减ToolStripMenuItem.Name = "减ToolStripMenuItem";
            减ToolStripMenuItem.Size = new Size(88, 22);
            减ToolStripMenuItem.Text = "减";
            减ToolStripMenuItem.Click += 减ToolStripMenuItem_Click;
            // 
            // 乘ToolStripMenuItem
            // 
            乘ToolStripMenuItem.Name = "乘ToolStripMenuItem";
            乘ToolStripMenuItem.Size = new Size(88, 22);
            乘ToolStripMenuItem.Text = "乘";
            乘ToolStripMenuItem.Click += 乘ToolStripMenuItem_Click;
            // 
            // 除ToolStripMenuItem
            // 
            除ToolStripMenuItem.Name = "除ToolStripMenuItem";
            除ToolStripMenuItem.Size = new Size(88, 22);
            除ToolStripMenuItem.Text = "除";
            除ToolStripMenuItem.Click += 除ToolStripMenuItem_Click;
            // 
            // button2
            // 
            button2.Location = new Point(339, 203);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 3;
            button2.Text = "计算";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Azure;
            ClientSize = new Size(790, 460);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            Click += Form1_Click;
            DoubleClick += Form1_DoubleClick;
            contextMenuStrip1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBox1;
        private TextBox textBox2;
        private Button button1;
        private Button button2;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem toolStripMenuItem2;
        private ToolStripMenuItem 减ToolStripMenuItem;
        private ToolStripMenuItem 乘ToolStripMenuItem;
        private ToolStripMenuItem 除ToolStripMenuItem;
    }
}
