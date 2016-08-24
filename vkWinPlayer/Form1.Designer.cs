/**
 * ---------------------------------------------------------
 * 
 * vkWinPlayer - минималистичный плеер для VK.COM
 * @Author GROM - <botx68@gmail.com>
 * @WebSite: https://retro3f.github.io/ | https://github.com/retro3f
 * @About: Программа для прослушивания музыки с сайта vk.com
 * @Date: 10.08.2016
 * @Programming language: C#
 * 
 *----------------------------------------------------------
 */

namespace vkWinPlayer
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;


        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.timerOneUpdateTrackBarAndPosition = new System.Windows.Forms.Timer(this.components);
            this.audioTrackBar = new MetroFramework.Controls.MetroTrackBar();
            this.ButtonPlayAudio = new MetroFramework.Controls.MetroButton();
            this.ButtonPauseAudio = new MetroFramework.Controls.MetroButton();
            this.ButtonStopAudio = new MetroFramework.Controls.MetroButton();
            this.VolumeText = new MetroFramework.Controls.MetroLabel();
            this.changeInVolume = new MetroFramework.Controls.MetroTrackBar();
            this.currentPositionAudio = new MetroFramework.Controls.MetroLabel();
            this.durationAudio = new MetroFramework.Controls.MetroLabel();
            this.audioNameRealTimes = new MetroFramework.Controls.MetroLabel();
            this.textCountAudio = new MetroFramework.Controls.MetroLabel();
            this.timerTwoMusicSwitch = new System.Windows.Forms.Timer(this.components);
            this.textProfileName = new MetroFramework.Controls.MetroLink();
            this.imgProfile = new System.Windows.Forms.PictureBox();
            this.autoPlayMusic = new MetroFramework.Controls.MetroCheckBox();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.checkMute = new MetroFramework.Controls.MetroCheckBox();
            this.pictureGetArtist = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.imgProfile)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureGetArtist)).BeginInit();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.BackColor = System.Drawing.SystemColors.Window;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(21, 26);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(358, 160);
            this.listBox1.TabIndex = 2;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // timerOneUpdateTrackBarAndPosition
            // 
            this.timerOneUpdateTrackBarAndPosition.Interval = 1000;
            this.timerOneUpdateTrackBarAndPosition.Tick += new System.EventHandler(this.timerOneUpdateTrackBarAndPosition_Tick);
            // 
            // audioTrackBar
            // 
            this.audioTrackBar.BackColor = System.Drawing.Color.Transparent;
            this.audioTrackBar.Location = new System.Drawing.Point(67, 295);
            this.audioTrackBar.Name = "audioTrackBar";
            this.audioTrackBar.Size = new System.Drawing.Size(616, 23);
            this.audioTrackBar.TabIndex = 13;
            this.audioTrackBar.Text = "metroTrackBar1";
            this.audioTrackBar.Value = 1;
            this.audioTrackBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.audioTrackBar_Scroll);
            // 
            // ButtonPlayAudio
            // 
            this.ButtonPlayAudio.Location = new System.Drawing.Point(49, 334);
            this.ButtonPlayAudio.Name = "ButtonPlayAudio";
            this.ButtonPlayAudio.Size = new System.Drawing.Size(130, 29);
            this.ButtonPlayAudio.TabIndex = 16;
            this.ButtonPlayAudio.Text = "Играть";
            this.ButtonPlayAudio.UseSelectable = true;
            this.ButtonPlayAudio.Click += new System.EventHandler(this.ButtonPlayAudio_Click);
            // 
            // ButtonPauseAudio
            // 
            this.ButtonPauseAudio.Location = new System.Drawing.Point(221, 334);
            this.ButtonPauseAudio.Name = "ButtonPauseAudio";
            this.ButtonPauseAudio.Size = new System.Drawing.Size(130, 29);
            this.ButtonPauseAudio.TabIndex = 17;
            this.ButtonPauseAudio.Text = "Пауза";
            this.ButtonPauseAudio.UseSelectable = true;
            this.ButtonPauseAudio.Click += new System.EventHandler(this.ButtonPauseAudio_Click);
            // 
            // ButtonStopAudio
            // 
            this.ButtonStopAudio.Location = new System.Drawing.Point(391, 334);
            this.ButtonStopAudio.Name = "ButtonStopAudio";
            this.ButtonStopAudio.Size = new System.Drawing.Size(130, 29);
            this.ButtonStopAudio.TabIndex = 18;
            this.ButtonStopAudio.Text = "Остановить";
            this.ButtonStopAudio.UseSelectable = true;
            this.ButtonStopAudio.Click += new System.EventHandler(this.ButtonStopAudio_Click);
            // 
            // VolumeText
            // 
            this.VolumeText.AutoSize = true;
            this.VolumeText.Location = new System.Drawing.Point(537, 334);
            this.VolumeText.Name = "VolumeText";
            this.VolumeText.Size = new System.Drawing.Size(78, 19);
            this.VolumeText.TabIndex = 19;
            this.VolumeText.Text = "Громкость: ";
            // 
            // changeInVolume
            // 
            this.changeInVolume.BackColor = System.Drawing.Color.Transparent;
            this.changeInVolume.Location = new System.Drawing.Point(632, 334);
            this.changeInVolume.Name = "changeInVolume";
            this.changeInVolume.Size = new System.Drawing.Size(94, 23);
            this.changeInVolume.TabIndex = 20;
            this.changeInVolume.Text = "volumeInChange";
            this.changeInVolume.Value = 30;
            this.changeInVolume.Scroll += new System.Windows.Forms.ScrollEventHandler(this.changeInVolume_Scroll);
            // 
            // currentPositionAudio
            // 
            this.currentPositionAudio.AutoSize = true;
            this.currentPositionAudio.Location = new System.Drawing.Point(21, 295);
            this.currentPositionAudio.Name = "currentPositionAudio";
            this.currentPositionAudio.Size = new System.Drawing.Size(40, 19);
            this.currentPositionAudio.TabIndex = 21;
            this.currentPositionAudio.Text = "00:00";
            // 
            // durationAudio
            // 
            this.durationAudio.AutoSize = true;
            this.durationAudio.Location = new System.Drawing.Point(689, 295);
            this.durationAudio.Name = "durationAudio";
            this.durationAudio.Size = new System.Drawing.Size(40, 19);
            this.durationAudio.TabIndex = 22;
            this.durationAudio.Text = "00:00";
            // 
            // audioNameRealTimes
            // 
            this.audioNameRealTimes.AutoSize = true;
            this.audioNameRealTimes.Location = new System.Drawing.Point(23, 263);
            this.audioNameRealTimes.Name = "audioNameRealTimes";
            this.audioNameRealTimes.Size = new System.Drawing.Size(0, 0);
            this.audioNameRealTimes.TabIndex = 27;
            // 
            // textCountAudio
            // 
            this.textCountAudio.AutoSize = true;
            this.textCountAudio.Location = new System.Drawing.Point(385, 108);
            this.textCountAudio.Name = "textCountAudio";
            this.textCountAudio.Size = new System.Drawing.Size(49, 19);
            this.textCountAudio.TabIndex = 28;
            this.textCountAudio.Text = "qqqqq";
            // 
            // timerTwoMusicSwitch
            // 
            this.timerTwoMusicSwitch.Enabled = true;
            this.timerTwoMusicSwitch.Interval = 1000;
            this.timerTwoMusicSwitch.Tick += new System.EventHandler(this.timerTwoMusicSwitch_Tick);
            // 
            // textProfileName
            // 
            this.textProfileName.Location = new System.Drawing.Point(385, 26);
            this.textProfileName.Name = "textProfileName";
            this.textProfileName.Size = new System.Drawing.Size(298, 23);
            this.textProfileName.TabIndex = 29;
            this.textProfileName.UseSelectable = true;
            this.textProfileName.Click += new System.EventHandler(this.textProfileName_Click);
            // 
            // imgProfile
            // 
            this.imgProfile.Location = new System.Drawing.Point(391, 55);
            this.imgProfile.Name = "imgProfile";
            this.imgProfile.Size = new System.Drawing.Size(50, 50);
            this.imgProfile.TabIndex = 30;
            this.imgProfile.TabStop = false;
            // 
            // autoPlayMusic
            // 
            this.autoPlayMusic.AutoSize = true;
            this.autoPlayMusic.Location = new System.Drawing.Point(102, 218);
            this.autoPlayMusic.Name = "autoPlayMusic";
            this.autoPlayMusic.Size = new System.Drawing.Size(145, 15);
            this.autoPlayMusic.TabIndex = 31;
            this.autoPlayMusic.Text = "Автовоспроизведение";
            this.autoPlayMusic.UseSelectable = true;
            this.autoPlayMusic.CheckedChanged += new System.EventHandler(this.autoPlayMusic_CheckedChanged);
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(102, 196);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(77, 19);
            this.metroLabel1.TabIndex = 32;
            this.metroLabel1.Text = "Настройки:";
            // 
            // checkMute
            // 
            this.checkMute.AutoSize = true;
            this.checkMute.Location = new System.Drawing.Point(102, 239);
            this.checkMute.Name = "checkMute";
            this.checkMute.Size = new System.Drawing.Size(111, 15);
            this.checkMute.TabIndex = 33;
            this.checkMute.Text = "Отключить звук";
            this.checkMute.UseSelectable = true;
            this.checkMute.CheckedChanged += new System.EventHandler(this.checkMute_CheckedChanged);
            // 
            // pictureGetArtist
            // 
            this.pictureGetArtist.ImageLocation = "";
            this.pictureGetArtist.Location = new System.Drawing.Point(21, 196);
            this.pictureGetArtist.Name = "pictureGetArtist";
            this.pictureGetArtist.Size = new System.Drawing.Size(68, 67);
            this.pictureGetArtist.TabIndex = 34;
            this.pictureGetArtist.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(752, 398);
            this.Controls.Add(this.pictureGetArtist);
            this.Controls.Add(this.checkMute);
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.autoPlayMusic);
            this.Controls.Add(this.imgProfile);
            this.Controls.Add(this.textProfileName);
            this.Controls.Add(this.textCountAudio);
            this.Controls.Add(this.audioNameRealTimes);
            this.Controls.Add(this.durationAudio);
            this.Controls.Add(this.currentPositionAudio);
            this.Controls.Add(this.changeInVolume);
            this.Controls.Add(this.VolumeText);
            this.Controls.Add(this.ButtonStopAudio);
            this.Controls.Add(this.ButtonPauseAudio);
            this.Controls.Add(this.ButtonPlayAudio);
            this.Controls.Add(this.audioTrackBar);
            this.Controls.Add(this.listBox1);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Resizable = false;
            this.RightToLeftLayout = true;
            this.Style = MetroFramework.MetroColorStyle.Green;
            this.Text = "vkWinPlayer";
            this.Theme = MetroFramework.MetroThemeStyle.Default;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.imgProfile)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureGetArtist)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Timer timerOneUpdateTrackBarAndPosition;
        private MetroFramework.Controls.MetroTrackBar audioTrackBar;
        private MetroFramework.Controls.MetroButton ButtonPlayAudio;
        private MetroFramework.Controls.MetroButton ButtonPauseAudio;
        private MetroFramework.Controls.MetroButton ButtonStopAudio;
        private MetroFramework.Controls.MetroLabel VolumeText;
        private MetroFramework.Controls.MetroTrackBar changeInVolume;
        private MetroFramework.Controls.MetroLabel currentPositionAudio;
        private MetroFramework.Controls.MetroLabel durationAudio;
        private MetroFramework.Controls.MetroLabel audioNameRealTimes;
        private MetroFramework.Controls.MetroLabel textCountAudio;
        private System.Windows.Forms.Timer timerTwoMusicSwitch;
        private MetroFramework.Controls.MetroLink textProfileName;
        private System.Windows.Forms.PictureBox imgProfile;
        private MetroFramework.Controls.MetroCheckBox autoPlayMusic;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroCheckBox checkMute;
        private System.Windows.Forms.PictureBox pictureGetArtist;

    }
}

