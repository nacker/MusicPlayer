using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MusicPlayer
{
    public partial class Form1 : Form
    {
        // 存放音乐地址数组
        List<string> musics = new List<string>();

        public Form1()
        {
            InitializeComponent();
        }

        private void addUrlBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                Player.URL = ofd.FileName;
                //playBtn.Enabled = true;
                //stopBtn.Enabled = true;
                //pauseBtn.Enabled = true;
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Player.settings.autoStart = false;
        }

        private void playBtn_Click(object sender, EventArgs e)
        {
            //Player.Ctlcontrols.play();
            if (playBtn.Text == "播放")
            {
                Player.Ctlcontrols.play();
                playBtn.Text = "暂停";
            }
            else
            {
                Player.Ctlcontrols.pause();
                playBtn.Text = "播放";
            }
        }

        private void pauseBtn_Click(object sender, EventArgs e)
        {
            if (pauseBtn.Text == "静音")
            {
                Player.settings.mute = true;
                pauseBtn.Text = "取消静音";
            }
            else
            {
                Player.settings.mute = false;
                pauseBtn.Text = "静音";
            }
        }

        private void stopBtn_Click(object sender, EventArgs e)
        {
            Player.Ctlcontrols.stop();
        }

        private void openBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = @"D:\";
            ofd.Filter = "音乐文件|*.mp3;*.wma";
            ofd.Title = "请选择文件";
            ofd.Multiselect = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                // listMusics.Items.AddRange(ofd.FileNames);
                // Player.URL = listMusics.Items[1].ToString();

                for (int i = 0; i < ofd.FileNames.Length; i++)
                {
                    musics.Add(ofd.FileNames[i]);
                    string[] temp = ofd.FileNames[i].Split(new char[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);

                    listMusics.Items.Add(temp[temp.Length - 1]);
                }

                myPlay();

            }
        }

        #region 播放
        private void myPlay(int i)
        {

            Player.URL = musics[i];
            listMusics.SelectedIndex = i;
            Player.Ctlcontrols.play();
            playBtn.Text = "暂停";
        }

        private void myPlay()
        {
            myPlay(0);
        }
        #endregion

        private void upBtn_Click(object sender, EventArgs e)
        {

            int index = listMusics.SelectedIndex;
            index--;
            if (index < 0)
            {
                myPlay(musics.Count - 1);
            }
            else
            {
                myPlay(index);
            }

           // myPlay(listMusics.SelectedIndex - 1 < 0 ? musics.Count - 1 : --listMusics.SelectedIndex);
        }

        private void nextBtn_Click(object sender, EventArgs e)
        {
            int index = listMusics.SelectedIndex;
            index++;
            if (index > musics.Count - 1)
            {
                myPlay(0);
            }
            else
            {
                myPlay(index);
            }


           // myPlay(listMusics.SelectedIndex + 1 > musics.Count - 1 ? 0 : ++listMusics.SelectedIndex);
        }

        /// <summary>
        /// 双击播放
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listMusics_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            myPlay(listMusics.SelectedIndex);
        }

        /// <summary>
        /// 右键删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listMusics.SelectedIndex != -1)
            {
                musics.RemoveAt(listMusics.SelectedIndex);
                listMusics.Items.RemoveAt(listMusics.SelectedIndex);
            }
        }
    }
}
