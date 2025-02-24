namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_DoubleClick(object sender, EventArgs e)
        {

        }

        private void Form1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }



        private void textBox1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "请输入第一个数字")
            {
                textBox1.Text = "";
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "请输入第二个数字")
            {
                textBox2.Text = "";
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            contextMenuStrip1.Show(Cursor.Position);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "请输入第一个数字" && textBox2.Text != "请输入第二个数字")
            {
                double a = Convert.ToDouble(textBox1.Text);
                double b = Convert.ToDouble(textBox2.Text);
                double c;
                string op = button1.Text;
                switch (op)
                {
                    case "加":
                        c = a + b;
                        MessageBox.Show("两数之和为：" + c);
                        break;
                    case "减":
                        c = a - b;
                        MessageBox.Show("两数之差为：" + c);
                        break;
                    case "乘":
                        c = a * b;
                        MessageBox.Show("两数之积为：" + c);
                        break;
                    case "除":
                        c = a / b;
                        MessageBox.Show("两数之商为：" + c);
                        break;
                    default:
                        MessageBox.Show("请选择运算符");
                        break;
                }
            }
        }
        

        private void contextMenuStrip1_Click(object sender, EventArgs e)
        {

        }

        private void 加ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button1.Text = "加";
        }
        private void 减ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button1.Text = "减";
        }
        private void 乘ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button1.Text = "乘";
        }
        private void 除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button1.Text = "除";
        }
    }
}
