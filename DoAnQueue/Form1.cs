using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DoAnQueue.Form1;
using static System.Net.Mime.MediaTypeNames;

namespace DoAnQueue
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        MyQueue myQueue = new MyQueue();
        private void button1_Click(object sender, EventArgs e)
        {
            
            AddCardtoQueue();
            CheckFormat();
            

        }

        int countQueue = 0;
        public void AddCardtoQueue()
        {
            string text = textBox1.Text.ToString();
            string contentCard = null;


            for (int i = 0; i < text.Length; i++)
            {
                if(text[i] == '<' )
                {
                    if(contentCard == null)
                    {
                        contentCard += text[i];
                    }else
                    {
                       
                        myQueue.Enqueue(contentCard);
                        contentCard = null;
                        contentCard += text[i];
                        countQueue++;
                    }
                    
                }
                else
                {
                    if (text[i] != '>')
                    {
                        contentCard +=text[i];
                    }
                    else
                    {
                        contentCard += text[i];
                        myQueue.Enqueue(contentCard);
                        countQueue++;
                        contentCard = null;
                    }
                    

                }

            }


        }






        public void CheckFormat()
        {
            for (int i = 0; i < countQueue; i++)
            {
                string ktra = myQueue.Dequeue().data.ToString();
                if(CheckCard(ktra))
                {
                    textBox2.Text += ktra + " \n";

                }else
                if (ktra.Contains("<") & !ktra.Contains(">") | ktra.Contains(">") & !ktra.Contains("<"))
                {
                    MessageBox.Show("Lỗi không theo đúng định dạng html < >" + "\n"+ktra.ToString());
                    
                }
                
            }
           
        }

        public bool CheckCard(string s)
        {
            
            if (!s.Contains("<") && !s.Contains(">"))
            {
                return true;
            }else
            {
                return false;
            }

        }



        public class Card
        {
            public Card prev, next;
            public string  data;

        }
        
        public class MyQueue
        {
            Card rear, front; public bool IsEmpty()
            {
                return rear == null || front == null;
            }
            int count = 0;
            public void Enqueue(string ele)
            {
                Card n = new Card();
                n.data = ele;
                if (rear == null)
                {
                    rear = n; front = n;
                    count++;
                }
                else
                {
                    rear.prev = n;
                    count++;
                    n.next = rear; rear = n;
                }
            }
            public Card Dequeue()
            {
                if (front == null) return null;
                Card d = front;
                front = front.prev;
                if (front == null)
                {
                    rear = null;
                    count--;
                }
                else
                    front.next = null;
                return d;
            }

            public int Count()
            {
                return count;
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}






