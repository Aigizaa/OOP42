using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOP42
{
    public partial class Form1 : Form
    {
        Number number;
        public class Number 
        {
            private int value1; //переменная для хранения значения С
            private int value2; //переменная для хранения значения В
            private int value3; //переменная для хранения значения А
            public System.EventHandler observers;

            public void SetValue(int value, int count) //метод для изменения значений
            {
                switch (count) //выбор, какое число будет меняться
                {
                    case 1:
                        { //условия для числа С
                            if (value < 0 && value >= this.value2 && value >= this.value3) this.value1 = 0;
                            else if (value > 100) this.value1 = 100;
                            else if (value <= this.value2)
                            {
                                if (value >= this.value3)
                                {
                                    this.value1 = value;
                                    this.value2 = value;
                                }
                            }
                            else if (value >= this.value2)
                            {
                                this.value1 = value;
                            }
                            observers.Invoke(this, null);
                            break;
                        }
                    case 2:
                        { //Условия для числа В
                            if (value < 0 && value >= this.value3 && value <= value1) this.value2 = 0;
                            else if (value > 100 && value >= this.value3 && value <= value1) this.value2 = 100;
                            else if (value >= this.value3 && value <= value1) this.value2 = value;
                            observers.Invoke(this, null);
                            break;
                        }
                    case 3:
                        { //Условия для числа А
                            if (value < 0) this.value3 = 0;
                            else if (value > 100 && value <= value1) this.value3 = 100;
                            else if (value < this.value2 && value < 100)
                            {
                                this.value3 = value;
                            }
                            else if (value < 100 && (value == this.value2 || value > this.value2) && this.value2 < this.value1)
                            {
                                if (this.value2 <= 100 && this.value2 <= this.value1)
                                {
                                    this.value2 = value;
                                    this.value3 = value;
                                }
                            }
                            observers.Invoke(this, null);
                            break;
                        }
                }
            }
            public int GetValue(int count) //метод для возврата значений чисел
            {
                switch (count)
                {
                    case 1:
                        {
                            return this.value1;
                        }
                    case 2:
                        {
                            return this.value2;
                        }
                    case 3:
                        {
                            return this.value3;
                        }
                }
                return 0;
            }
        }
        public Form1()
        {
            InitializeComponent();
            number = new Number();//инициализация
            number.observers += new System.EventHandler(this.UpdateFromClass);//обновления чисел
        }
        private void UpdateFromClass(object sender, EventArgs e) //метод обновления чисел
        {
            textBox1.Text = number.GetValue(1).ToString();
            textBox2.Text = number.GetValue(2).ToString();
            textBox3.Text = number.GetValue(3).ToString();
            numericUpDown1.Value = number.GetValue(1);
            numericUpDown2.Value = number.GetValue(2);
            numericUpDown3.Value = number.GetValue(3);
            trackBar1.Value = number.GetValue(1);
            trackBar2.Value = number.GetValue(2);
            trackBar3.Value = number.GetValue(3);
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e) //метод сохранения значений A и C при закрытии формы
        {
            Properties.Settings.Default.number1 = number.GetValue(1);
            Properties.Settings.Default.number3 = number.GetValue(3);
            Properties.Settings.Default.Save();
        }
        private void Form1_Load(object sender, EventArgs e) //устанавливаем значения чисел в те, которые были сохранены перед закрытием формы
        {
            number.SetValue(Properties.Settings.Default.number1, 1);
            number.SetValue(Properties.Settings.Default.number3, 3);
        }
        private void textBox1_KeyDown(object sender, KeyEventArgs e) 
        {
            int count;
            if (e.KeyCode == Keys.Enter)
                if (int.TryParse(textBox1.Text, out count))
                    number.SetValue(Int32.Parse(textBox1.Text), 1);
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            int count;
            if (e.KeyCode == Keys.Enter)
                if (int.TryParse(textBox1.Text, out count))
                    number.SetValue(Int32.Parse(textBox2.Text), 2);
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            int count;
            if (e.KeyCode == Keys.Enter)
                if (int.TryParse(textBox1.Text, out count))
                    number.SetValue(Int32.Parse(textBox3.Text), 3);
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            number.SetValue(Decimal.ToInt32(numericUpDown1.Value), 1);
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            number.SetValue(Decimal.ToInt32(numericUpDown2.Value), 2);
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            number.SetValue(Decimal.ToInt32(numericUpDown3.Value), 3);
        }
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            number.SetValue(trackBar1.Value, 1);
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            number.SetValue(trackBar2.Value, 2);
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            number.SetValue(trackBar3.Value, 3);
        }
    }
}
