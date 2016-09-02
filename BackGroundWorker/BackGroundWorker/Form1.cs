/*Created by:Rong Fan
 *email:rong.fan1031@gmail.com
 *Desc: using BackGroundWorker to implement multiple threads
 *Dt: 2016-9-1
 * Version:.NET 4.0
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace BackGroundWorker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        BackgroundWorker bgWorker = null;
        private ManualResetEvent manualReset = new ManualResetEvent(true);

        private void InitiateBgWorker()
        {
            bgWorker = new BackgroundWorker();
            bgWorker.WorkerReportsProgress = true;
            bgWorker.WorkerSupportsCancellation = true;
            bgWorker.DoWork+=new DoWorkEventHandler(bgWorker_DoWork);
            bgWorker.RunWorkerCompleted+=new RunWorkerCompletedEventHandler(bgWorker_RunWorkerCompleted);
            bgWorker.ProgressChanged+=new ProgressChangedEventHandler(bgWorker_ProgressChanged);
        }


        private void bgWorker_DoWork(object send,DoWorkEventArgs e)
        {
            for (int i = 0; i < 100; i++)
            {
                if(bgWorker.CancellationPending)//if user cancel operation,如果用户申请了取消曹组
                {
                    for (int k = i; k >= 0; k--)
                    {
                        Thread.Sleep(10);
                        bgWorker .ReportProgress(k);//simulate a roll back,模拟一个回滚的效果
                    }
                    e.Cancel = true;
                    return;// return 跳出操作123
                }
                //operation1 操作1 
                //operation2 操作2
                //operation3 操作3
                manualReset.WaitOne();//if  ManualResetEvent's initial state is stop; this method is still working, until you receive reset; then, continue to work
                //如果ManualResetEvent的初始化为终止状态（true），那么该方法将一直工作，直到收到Reset信号。然后，直到收到Set信号，就继续工作。
                //反之亦然
                Thread.Sleep(500);
                bgWorker.ReportProgress(i + 1);
            }
        }

        private void bgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void bgWorker_RunWorkerCompleted(object sender,RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
                MessageBox.Show("user canceled ");
            else
                MessageBox.Show("operation finished");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            InitiateBgWorker();
            bgWorker.RunWorkerAsync();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.Text == "pause")
            {
                manualReset.Reset();//暂停当前线程的工作，发信号给waitOne方法，阻塞
                button2.Text = "continue";
            }
            else
            {
                manualReset.Set();//继续某个线程的工作
                button2.Text = "pause";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            bgWorker.CancelAsync();
        }
    }
}
