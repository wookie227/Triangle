namespace Triangle
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private TextBox textBox1;
        private TextBox textBox2;
        private TextBox textBox3;
        private Button button1;
        private Label label1;
        private PictureBox pictureBox1;

        private void Button1_Click(object sender, EventArgs e)
        {
            LogAction("Button clicked");
            if (double.TryParse(textBox1.Text, out double a) &&
                double.TryParse(textBox2.Text, out double b) &&
                double.TryParse(textBox3.Text, out double c))
            {
                if (IsValidTriangle(a, b, c))
                {
                    string type = GetTriangleType(a, b, c);
                    label1.Text = type;
                    LogAction($"Triangle type: {type}", a, b, c);
                    DrawTriangle(a, b, c);
                }
                else
                {
                    MessageBox.Show("Введенные значения не могут образовать треугольник.");
                    LogAction("Invalid triangle sides", a, b, c);
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, введите числовые значения.");
                LogAction("Non-numeric input");
            }
        }


        private bool IsValidTriangle(double a, double b, double c)
        {
            return a + b > c && a + c > b && b + c > a;
        }

        private string GetTriangleType(double a, double b, double c)
        {
            if (a == b && b == c)
                return "Равносторонний";
            if (a == b || a == c || b == c)
                return "Равнобедренный";
            return "Разносторонний";
        }

        private void DrawTriangle(double a, double b, double c)
        {
            Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.White);

                // Первая вершина (0,0)
                PointF p1 = new PointF(0, 0);

                // Вторая вершина (c, 0)
                PointF p2 = new PointF((float)c, 0);

                // Вычисляем координаты третьей вершины
                float x = (float)((b * b - a * a + c * c) / (2 * c));
                float y = (float)Math.Sqrt(b * b - x * x);

                PointF p3 = new PointF(x, y);

                // Масштабирование и центрирование треугольника
                float scale = (float)Math.Min(pictureBox1.Width / (c + 20), pictureBox1.Height / (y + 20));
                p1 = new PointF(p1.X * scale + 10, pictureBox1.Height - (p1.Y * scale + 10));
                p2 = new PointF(p2.X * scale + 10, pictureBox1.Height - (p2.Y * scale + 10));
                p3 = new PointF(p3.X * scale + 10, pictureBox1.Height - (p3.Y * scale + 10));

                g.DrawPolygon(Pens.Black, new PointF[] { p1, p2, p3 });
            }
            pictureBox1.Image = bmp;
        }

        private void LogAction(string message, double? a = null, double? b = null, double? c = null)
        {
            using (StreamWriter writer = new StreamWriter("C:\\Users\\Andrey\\source\\repos\\Triangle\\log.txt", true))
            {
                string logMessage = $"{DateTime.Now}: {message}";
                if (a.HasValue && b.HasValue && c.HasValue)
                {
                    logMessage += $" (Sides: a={a.Value}, b={b.Value}, c={c.Value})";
                }
                writer.WriteLine(logMessage);
            }
        }

    }
}
