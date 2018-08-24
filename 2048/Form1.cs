using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace _2048
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int Zaderjka = 500, Q = 1;
        Random rnd = new Random();
        

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.Rows.Add(4);

            for (int i = 0; i < 4; i++)
            {
                dataGridView1.Columns[i].Width = 100;
                dataGridView1.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1.Rows[i].Height = 100;
            }

            dataGridView1.ClearSelection();
            this.Focus();

            New4islo();
        }

        public void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            this.Enabled = false;

            if (e.KeyCode == Keys.Up)
            {
                SdvigPoVertikali(0);
            }

            if (e.KeyCode == Keys.Down)
            {
                SdvigPoVertikali(-3);
            }

            if (e.KeyCode == Keys.Left)
            {
                SdvigPoGorizontali(0);
            }

            if (e.KeyCode == Keys.Right)
            {
                SdvigPoGorizontali(-3);
            }

            if (e.KeyCode == Keys.F5)
            {
                SaveRecord();
            }

            if (e.KeyCode == Keys.F6)
            {
                if (Q == 1)
                {
                    Zaderjka = 0;
                    this.Text = "2048 (Zaderjka = 0)";
                    Q = 0;
                }
                else
                {
                    Zaderjka = 500;
                    this.Text = "2048 (Zaderjka = 500)";

                    Q = 1;
                }
            }

            this.Enabled = true;
        }

        private void SaveRecord()
        {
            int Summa = 0, Naib = 2;

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Summa += Convert.ToInt32(dataGridView1.Rows[i].Cells[j].Value);

                    if (Convert.ToInt32(dataGridView1.Rows[i].Cells[j].Value) > Naib)
                    {
                        Naib = Convert.ToInt32(dataGridView1.Rows[i].Cells[j].Value);
                    }
                }
            }

            System.IO.File.AppendAllText(System.IO.Path.GetDirectoryName(Application.ExecutablePath.ToString()) + "\\Таблица рекордов.txt", DateTime.Now.ToString() + " " + Convert.ToString(Naib) + " " + Convert.ToString(Summa) + "\r\n");
            System.Diagnostics.Process.Start(System.IO.Path.GetDirectoryName(Application.ExecutablePath.ToString()) + "\\Таблица рекордов.txt");
        }

        private void New4islo()
        {
            int x = rnd.Next(4), y = rnd.Next(4), Pystoe = 0;
            if (dataGridView1.Rows[x].Cells[y].Value == null)
            {
                dataGridView1.Rows[x].Cells[y].Value = Math.Pow(2, rnd.Next(2) + 1);
            }
            else
            {
                Pystoe = 0;

                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (dataGridView1.Rows[i].Cells[j].Value == null)
                        {
                            Pystoe++;
                        }
                    }
                }

                if (Pystoe == 0)
                {
                    SaveRecord();
                    this.Close();
                }

                New4islo();
            }
        }

        async void SdvigPoVertikali(int I)
        {
            for (int ii = 0; ii < 4; ii++)
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (dataGridView1.Rows[Math.Abs(I + i)].Cells[j].Value == null)
                        {
                            dataGridView1.Rows[Math.Abs(I + i)].Cells[j].Value = dataGridView1.Rows[Math.Abs(I + i + 1)].Cells[j].Value;
                            dataGridView1.Rows[Math.Abs(I + i + 1)].Cells[j].Value = null;
                            
                        }
                        else
                        {
                            if (Convert.ToString(dataGridView1.Rows[Math.Abs(I + i)].Cells[j].Value) == Convert.ToString(dataGridView1.Rows[Math.Abs(I + i + 1)].Cells[j].Value))
                            {
                                dataGridView1.Rows[Math.Abs(I + i)].Cells[j].Style.BackColor = Color.Lime;
                                dataGridView1.Rows[Math.Abs(I + i + 1)].Cells[j].Style.BackColor = Color.Lime;
                                await Task.Delay(Zaderjka);
                                dataGridView1.Rows[Math.Abs(I + i)].Cells[j].Value = Convert.ToInt32(dataGridView1.Rows[Math.Abs(I + i)].Cells[j].Value) * 2;
                                dataGridView1.Rows[Math.Abs(I + i + 1)].Cells[j].Value = null;
                                dataGridView1.Rows[Math.Abs(I + i + 1)].Cells[j].Style.BackColor = Color.White;
                                await Task.Delay(Zaderjka);
                                dataGridView1.Rows[Math.Abs(I + i)].Cells[j].Style.BackColor = Color.White;
                            }
                        }
                    }
                }
            }
            New4islo();
        }

        async void SdvigPoGorizontali(int I)
        {
            for (int ii = 0; ii < 4; ii++)
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (dataGridView1.Rows[j].Cells[Math.Abs(I + i)].Value == null)
                        {
                            dataGridView1.Rows[j].Cells[Math.Abs(I + i)].Value = dataGridView1.Rows[j].Cells[Math.Abs(I + i + 1)].Value;
                            dataGridView1.Rows[j].Cells[Math.Abs(I + i + 1)].Value = null;
                        }
                        else
                        {
                            if (Convert.ToString(dataGridView1.Rows[j].Cells[Math.Abs(I + i)].Value) == Convert.ToString(dataGridView1.Rows[j].Cells[Math.Abs(I + i + 1)].Value))
                            {
                                dataGridView1.Rows[j].Cells[Math.Abs(I + i)].Style.BackColor = Color.Lime;
                                dataGridView1.Rows[j].Cells[Math.Abs(I + i + 1)].Style.BackColor = Color.Lime;
                                await Task.Delay(Zaderjka);
                                dataGridView1.Rows[j].Cells[Math.Abs(I + i)].Value = Convert.ToInt32(dataGridView1.Rows[j].Cells[Math.Abs(I + i)].Value) * 2;
                                dataGridView1.Rows[j].Cells[Math.Abs(I + i + 1)].Value = null;
                                dataGridView1.Rows[j].Cells[Math.Abs(I + i + 1)].Style.BackColor = Color.White;
                                await Task.Delay(Zaderjka);
                                dataGridView1.Rows[j].Cells[Math.Abs(I + i)].Style.BackColor = Color.White;
                            }
                        }
                    }
                }
            }
            New4islo();
        }
    }
}
