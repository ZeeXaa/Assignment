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
            if (textBox1.Text == "�������һ������")
            {
                textBox1.Text = "";
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "������ڶ�������")
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
            if (textBox1.Text != "�������һ������" && textBox2.Text != "������ڶ�������")
            {
                double a = Convert.ToDouble(textBox1.Text);
                double b = Convert.ToDouble(textBox2.Text);
                double c;
                string op = button1.Text;
                switch (op)
                {
                    case "��":
                        c = a + b;
                        MessageBox.Show("����֮��Ϊ��" + c);
                        break;
                    case "��":
                        c = a - b;
                        MessageBox.Show("����֮��Ϊ��" + c);
                        break;
                    case "��":
                        c = a * b;
                        MessageBox.Show("����֮��Ϊ��" + c);
                        break;
                    case "��":
                        c = a / b;
                        MessageBox.Show("����֮��Ϊ��" + c);
                        break;
                    default:
                        MessageBox.Show("��ѡ�������");
                        break;
                }
            }
        }
        

        private void contextMenuStrip1_Click(object sender, EventArgs e)
        {

        }

        private void ��ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button1.Text = "��";
        }
        private void ��ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button1.Text = "��";
        }
        private void ��ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button1.Text = "��";
        }
        private void ��ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button1.Text = "��";
        }
    }
}
