namespace CalcForms
{
    public partial class Form1 : Form
    {
        const string MathOps = "+-/*=";

        double? first = null;
        char? operation = null;

        bool symClicked = false;

        public Form1()
        {
            InitializeComponent();

            RegisterButtonEvents();
        }

        private void RegisterButtonEvents()
        {
            foreach (Control item in this.panelNumpad.Controls)
            {
                Button btn = item as Button;

                if (btn == null)
                    continue;

                btn.Click += btnNum_Click;
            }
        }

        private void btnNum_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            char sym = btn.Text.FirstOrDefault();

            if (Char.IsDigit(sym))
            {
                if (symClicked)
                {
                    this.txtDisplay.Text = "";
                    symClicked = false;
                }

                if (this.txtDisplay.Text.StartsWith('0'))
                {
                    this.txtDisplay.Text = this.txtDisplay.Text.Remove(0, 1);
                }

                this.txtDisplay.Text += btn.Text;
            }

            if (MathOps.Contains(sym))
            {
                symClicked = true;
                if (first == null)
                {
                    first = double.Parse(this.txtDisplay.Text);
                    operation = sym;
                }
                else
                {
                    double second = double.Parse(this.txtDisplay.Text);

                    double result = Calc(first.Value, second, operation.Value);

                    this.txtDisplay.Text = result.ToString();

                    first = result;
                    operation = sym;

                    if (sym == '=')
                    {
                        first = null;
                    }
                }
            }
        }

        private double Calc(double first, double second, char sym)
        {
            switch (sym)
            {
                case '+':
                    return first + second;
                case '-':
                    return first - second;
                case '*':
                    return first * second;
                case '/':
                    return first / second;
                default:
                    return first;
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
          
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char key = e.KeyChar;


            foreach (Control button in this.panelNumpad.Controls)
            {
                Button btn = button as Button;
                if (btn == null)
                    continue;

                if (btn.Text == key.ToString())
                {
                    btn.PerformClick();
                    break;
                }
            }
        }
    }
}