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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Threading;

//CustomLib
using WMPLib; // WMP
using Newtonsoft.Json;// JSON
using Newtonsoft.Json.Linq; // JSON
using MetroFramework.Forms;
using MetroFramework;
using System.Diagnostics;
using System.Text.RegularExpressions;



namespace vkWinPlayer
{
    public partial class Form1 : MetroForm
    {
        IniLib ini = new IniLib(Application.StartupPath + "\\vkWinPlayerSettings.ini");
        public Form1()
        {
            InitializeComponent();
            this.MouseWheel += new MouseEventHandler(changeInVolume_MouseWheel);
        }

        string apiServerVK = "https://api.vk.com/method/";
        string apiVersion = "5.56";
        public string accessToken;
        public string userUid;
        public string lastFmAccessToken;

        // Глобальные переменные функции - (getUserInfo)
        string getUserUid;
        string getFirstName;
        string getLastName;
        string getPhotoProfile;

        // Старт плеера.
        WindowsMediaPlayer WMPs = new WMPLib.WindowsMediaPlayer(); //создаётся плеер

        private void Form1_Load(object sender, EventArgs e)
        {
            getIniAccessTokenAndUid();

            if (accessToken == "")
            {
                MetroMessageBox.Show(this, "Ключ доступа(access_token) не найден!!!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
            }
            else if (userUid == "")
            {
                MetroMessageBox.Show(this, "Идентификатор(UID) пользователя не найден!!!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
            }
            else
            {
                if (lastFmAccessToken == "")
                {
                    MetroMessageBox.Show(this, "Ключ доступа LAST.FM(access_token) не найден!!!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                string allAudioFiles = Convert.ToString(getAudioCount(userUid));
                audioGet(userUid, allAudioFiles);
                getUserInfo(userUid, "photo_50");

                textCountAudio.Text = String.Format("У вас {0} аудиозаписей", allAudioFiles);
                textProfileName.Text = String.Format("Вы вошли как: {0} {1}", getFirstName, getLastName);

                imgProfile.ImageLocation = getPhotoProfile; // подгружаем аватарку профиля.
                pictureGetArtist.ImageLocation = getArtistPicture("");
                try
                {
                    // Задаем стандартную громкость(10%)
                    string getIniVolume = ini.IniRead("PlayerSettings", "Volume");
                    Regex Regular = new Regex(@"^[0-9 ]+$");

                    if (Regular.IsMatch(getIniVolume))
                    {
                        if (getIniVolume == "")
                        {
                            WMPs.settings.volume = changeInVolume.Value; // если не получили сохранненую громкость, то задаем дефолтную.
                            VolumeText.Text = string.Format("Громкость: {0}", changeInVolume.Value);
                        }
                        else
                        {
                            WMPs.settings.volume = changeInVolume.Value = Convert.ToInt32(getIniVolume); // если получили ранее сохраненную громкость, то ставим ее.
                            VolumeText.Text = string.Format("Громкость: {0}", changeInVolume.Value = Convert.ToInt32(getIniVolume));
                        }
                    }
                    else
                    {
                        WMPs.settings.volume = changeInVolume.Value; // если не получили сохранненую громкость или что то пошло не так, то задаем дефолтную.
                        VolumeText.Text = string.Format("Громкость: {0}", changeInVolume.Value);
                    }
                }
                catch (System.OverflowException)
                {
                    WMPs.settings.volume = changeInVolume.Value; // если не получили сохранненую громкость или что то пошло не так, то задаем дефолтную.
                    VolumeText.Text = string.Format("Громкость: {0}", changeInVolume.Value);
                }

                //чекбокс автовоспроизведения

                string getIniAutoPlayStatus = ini.IniRead("PlayerSettings", "AutoPlayStatus");

                if (getIniAutoPlayStatus == "True")
                {
                    autoPlayMusic.Checked = true;
                }
                else
                {
                    autoPlayMusic.Checked = false;
                }

                string getIniCheckMutes = ini.IniRead("PlayerSettings", "MuteStatus");

                if (getIniCheckMutes == "True")
                {
                    checkMute.Checked = true;
                }
                else
                {
                    checkMute.Checked = false;
                }

                string getIniAudioRepeat = ini.IniRead("PlayerSettings", "AudioRepeat");

                if (getIniAudioRepeat == "True")
                {
                    audioRepeat.Checked = true;
                }
                else
                {
                    audioRepeat.Checked = false;
                }

            }
        }

        public void getIniAccessTokenAndUid()
        {
            accessToken = ini.IniRead("UserInfo", "access_token");
            userUid = ini.IniRead("UserInfo", "uid");
            lastFmAccessToken = ini.IniRead("UserInfo", "lastfm_access_token");
        }


        // Функция получает более детальные данные о пользователе 
        public void getUserInfo(string uid, string fields)
        {

            string inq = apiServerVK + "users.get?user_ids=" + uid + "&access_token=" + accessToken + "&fields=" + fields + "&v=" + apiVersion;
            string inetGetCall = inetGet(inq);


            var json = JObject.Parse(inetGetCall);
            var reg_response = json["response"] as JContainer;
            var valid_json = reg_response[0] as JObject;

            dynamic GetUserInfo = JsonConvert.DeserializeObject<dynamic>(valid_json.ToString());


            getUserUid = GetUserInfo.id; // UID
            getFirstName = GetUserInfo.first_name; // first_name
            getLastName = GetUserInfo.last_name; // last_name
            getPhotoProfile = GetUserInfo.photo_50; // photo_50
        }


        // Функция для получения картинки артиста с Last.Fm
        public string getArtistPicture(string nameArtist)
        {
            string lastFmAccessToken = ini.IniRead("UserInfo", "lastfm_access_token");
            string error_img_link_no_album = "https://pp.vk.me/c630827/v630827017/43eff/Af5KwCw1bP4.jpg";

            if (lastFmAccessToken == "")
            {
                return error_img_link_no_album;
            }
            else
            {
                try
                {
                    string lastFmGetLink = "http://ws.audioscrobbler.com/2.0/?method=artist.getinfo&artist=" + nameArtist + "&api_key=" + lastFmAccessToken + "&format=json";
                    string lastFmResp = inetGet(lastFmGetLink);

                    if (lastFmResp == "")
                    {
                        return error_img_link_no_album;
                    }
                    else
                    {

                        var json = JObject.Parse(lastFmResp);
                        var reg_response = json["artist"]["image"] as JContainer;
                        var valid_json = reg_response[1] as JObject;
                        string reg_response2 = valid_json["#text"].ToString();

                        if (reg_response2 == "")
                        {
                            return error_img_link_no_album;
                        }
                        else
                        {
                            return reg_response2;
                        }
                    }
                }
                catch (System.NullReferenceException)
                {
                    return error_img_link_no_album;
                }
            }
        }


        // Функция получает количество аудизаписей у данного пользователя.
        public int getAudioCount(string uid)
        {
            try
            {
                if (uid == "")
                {
                    return 0; // если не был передан uid.
                }
                else
                {
                    string getAudioCounts = apiServerVK + "audio.getCount?owner_id=" + uid + "&access_token=" + accessToken + "&v=" + apiVersion;
                    string audioCountJson = inetGet(getAudioCounts);
                    dynamic ac = JsonConvert.DeserializeObject<dynamic>(audioCountJson);
                    return ac.response; // результат.
                }
            }
            catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
            {
                return 0;
            }
        }

        public List<Audio> audioList = null;

        public class Audio
        {
            public int id { get; set; }
            public int owner_id { get; set; }
            public string artist { get; set; }
            public string title { get; set; }
            public int duration { get; set; }
            public string url { get; set; }
            public string lyrics_id { get; set; }
            public int genre_id { get; set; }

        }

        public void audioGet(string owner_id, string count)
        {
            try
            {


                string q = apiServerVK + "audio.get?owner_id=" + owner_id + "&count=" + count + "&access_token=" + accessToken + "&v=" + apiVersion;
 
                string res = inetGet(q);

                if (res == "")
                {
                    MetroMessageBox.Show(this, "Неудалось синхронизироватся с API.VK.COM", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Environment.Exit(0);
                }
                else
                {
                    JToken token = JToken.Parse(res);
                    audioList = Enumerable.Skip(token["response"]["items"].Children(), 1).Select(c => c.ToObject<Audio>()).ToList();
                    //string[] arr1 = new string[] { };

                    this.Invoke((MethodInvoker)delegate
                    {
                        for (int i = 0; i < audioList.Count(); i++)
                        {
                            listBox1.Items.Add(audioList[i].artist + " - " + audioList[i].title);
                        }
                    });
                }
            }
            catch (System.NullReferenceException)
            {
                MetroMessageBox.Show(this, "Исключение: System.NullReferenceException| Функции(audioGet) не удалось получить список аудифайлов с сервера. ", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
            }
        }

        public string inetGet(string str)
        {
            try
            {
                var webRequest = WebRequest.Create(str);
                using (var response = webRequest.GetResponse())
                using (var content = response.GetResponseStream())
                using (var reader = new StreamReader(content))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (System.Net.WebException)
            {
                return "";
            }
        }

        public void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            WMPs.URL = audioList[listBox1.SelectedIndex].url;

            if (audioList[listBox1.SelectedIndex].url == "")
            {
                timerOneUpdateTrackBarAndPosition.Enabled = false;
            }
            else
            {
                WMPs.controls.play();
                ini.IniWrite("PlayerSettings", "AudioRepeatPos", Convert.ToString(listBox1.SelectedIndex));

                string sListBox = listBox1.SelectedItem.ToString();
                string[] splitslistBox = sListBox.Split('-');
                splitslistBox[0] = splitslistBox[0].Replace(@" ", @"");
                pictureGetArtist.ImageLocation = getArtistPicture(splitslistBox[0]);
                Thread.Sleep(1000);
                timerOneUpdateTrackBarAndPosition.Enabled = true;
                //timerTwoMusicSwitch.Enabled = true;
            }
        }

        public void timerOneUpdateTrackBarAndPosition_Tick(object sender, EventArgs e)
        {
            try
            {
                audioTrackBar.Maximum = Convert.ToInt32(WMPs.currentMedia.duration);
                audioTrackBar.Value = Convert.ToInt32(WMPs.controls.currentPosition);

                currentPositionAudio.Text = WMPs.controls.currentPositionString; // начало трека
                durationAudio.Text = WMPs.currentMedia.durationString; // Конец трека

                audioNameRealTimes.Text = WMPs.status.ToString();
            }
            catch (System.ArgumentOutOfRangeException)
            {

            }
        }

        private void audioTrackBar_Scroll(object sender, ScrollEventArgs e)
        {
            double time = WMPs.controls.currentPosition = audioTrackBar.Value;
        }

        public void changeInVolume_Scroll(object sender, ScrollEventArgs e)
        {
            ini.IniWrite("PlayerSettings", "Volume", Convert.ToString(changeInVolume.Value));
            VolumeText.Text = string.Format("Громкость: {0}", changeInVolume.Value);
            WMPs.settings.volume = changeInVolume.Value;
        }

        private void ButtonPlayAudio_Click(object sender, EventArgs e)
        {
            WMPs.controls.play();
            //timerTwoMusicSwitch.Enabled = true;
        }

        private void ButtonPauseAudio_Click(object sender, EventArgs e)
        {
            WMPs.controls.pause();
        }

        private void ButtonStopAudio_Click(object sender, EventArgs e)
        {
            WMPs.controls.stop();
            timerTwoMusicSwitch.Enabled = false;
        }

        public void timerTwoMusicSwitch_Tick(object sender, EventArgs e)
        {

            if (currentPositionAudio.Text == "00:00" && durationAudio.Text == "00:00")
            {
            }
            else
            {
                if (autoPlayMusic.Checked == true) // Если чекбокс активный, то делаем автовоспроизведение.
                {
                    if (audioNameRealTimes.Text == "Остановлено") // дикий костыль, а по другому я хз:)
                    {
                        try
                        {
                            listBox1.SelectedIndex += 1; // Если предыдущий трек завершился мы воспролизводим новый.
                        }
                        catch
                        {
                            listBox1.SelectedIndex = 0; // если плейлист закончился, то начинаем с нуля.
                        }
                    }
                }
            }
        }

        // тут и так все понятно. 
        private void textProfileName_Click(object sender, EventArgs e)
        {
            string profileLinks = "https://vk.com/id" + getUserUid;
            Process.Start(profileLinks);
        }

        private void autoPlayMusic_CheckedChanged(object sender, EventArgs e)
        {
            string checkAutoPlayStatus = ini.IniRead("PlayerSettings", "AutoPlayStatus");

            if (autoPlayMusic.Checked == true)
            {
                if (autoPlayMusic.Checked) audioRepeat.Checked = false;
                if (checkAutoPlayStatus == "True")
                {
                    timerTwoMusicSwitch.Enabled = true;
                    
                }
                else
                {
                    ini.IniWrite("PlayerSettings", "AutoPlayStatus", Convert.ToString(timerTwoMusicSwitch.Enabled = true));
                }
            }
            else
            {
                timerTwoMusicSwitch.Enabled = false;
                ini.IniWrite("PlayerSettings", "AutoPlayStatus", Convert.ToString(timerTwoMusicSwitch.Enabled = false));
            }
        }

        private void checkMute_CheckedChanged(object sender, EventArgs e)
        {
            string getIniVolumes = ini.IniRead("PlayerSettings", "Volume");
            string checkMuteStatus = ini.IniRead("PlayerSettings", "MuteStatus");

            if (checkMute.Checked == true)
            {
                if (checkMuteStatus == "True")
                {
                    VolumeText.Text = string.Format("Громкость: {0}", changeInVolume.Value = 0);
                    WMPs.settings.volume = changeInVolume.Value = 0;
                }
                else
                {
                    VolumeText.Text = string.Format("Громкость: {0}", changeInVolume.Value = 0);
                    WMPs.settings.volume = changeInVolume.Value = 0;
                    ini.IniWrite("PlayerSettings", "MuteStatus", Convert.ToString(checkMute.Checked));
                }
            }
            else
            {
                ini.IniWrite("PlayerSettings", "MuteStatus", Convert.ToString(checkMute.Checked));
                VolumeText.Text = string.Format("Громкость: {0}", changeInVolume.Value = Convert.ToInt32(getIniVolumes));
                WMPs.settings.volume = changeInVolume.Value = Convert.ToInt32(getIniVolumes);
            }
        }
        public void changeInVolume_MouseWheel(object sender, MouseEventArgs e)
        {
            ((HandledMouseEventArgs)e).Handled = true;//disable default mouse wheel
            if (e.Delta > 0)
            {
                if (changeInVolume.Value < changeInVolume.Maximum)
                {
                    VolumeText.Text = string.Format("Громкость: {0}", changeInVolume.Value = changeInVolume.Value++);
                    WMPs.settings.volume = changeInVolume.Value = changeInVolume.Value++;

                }
            }
            else
            {
                if (changeInVolume.Value > changeInVolume.Minimum)
                {
                    VolumeText.Text = string.Format("Громкость: {0}", changeInVolume.Value = changeInVolume.Value--);
                    WMPs.settings.volume = changeInVolume.Value = changeInVolume.Value--;
                }
            }
        }
        // очередной костыль :)
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void audioRepeat_CheckedChanged(object sender, EventArgs e)
        {
            string checkAudioRepeatStatus = ini.IniRead("PlayerSettings", "AudioRepeat");

            if (audioRepeat.Checked == true)
            {
                if (audioRepeat.Checked) autoPlayMusic.Checked = false;

                if (checkAudioRepeatStatus == "True")
                {
                    timerAudioRepeat.Enabled = true;

                }
                else
                {
                    ini.IniWrite("PlayerSettings", "AudioRepeat", Convert.ToString(timerAudioRepeat.Enabled = true));
                }
            }
            else
            {
                timerAudioRepeat.Enabled = false;
                ini.IniWrite("PlayerSettings", "AudioRepeat", Convert.ToString(timerAudioRepeat.Enabled = false));
            }
        }

        public void BtnDownloadAudioFile_Click(object sender, EventArgs e)
        {
            try
            {
                string getPathAudioFile = ini.IniRead("PlayerSettings", "AudioDownloadPath");

                if (getPathAudioFile == "")
                {
                    MetroMessageBox.Show(this, "В открывшемся окне выберите папку куда сохранить ваши аудиофайлы и нажмите кнопку: 'ок'.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    FolderBrowserDialog fbdPath = new FolderBrowserDialog();
                    if (fbdPath.ShowDialog() == DialogResult.OK)
                    {
                        ini.IniWrite("PlayerSettings", "AudioDownloadPath", fbdPath.SelectedPath);
                    }
                }
                else
                {
                    WebClient webClient = new WebClient();
                    string LinkAudioFile = audioList[listBox1.SelectedIndex].url;
                    string nameArtistOrTitle = audioList[listBox1.SelectedIndex].artist + " - " + audioList[listBox1.SelectedIndex].title;
                    LinkAudioFile = LinkAudioFile.Substring(0, LinkAudioFile.LastIndexOf('?'));

                    if (LinkAudioFile == "" && nameArtistOrTitle == "")
                    {
                        MetroMessageBox.Show(this, "Неудалось получить имя аудиофайла или ссылку с сервера...", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        webClient.DownloadFileAsync(new Uri(LinkAudioFile), getPathAudioFile + "/" + nameArtistOrTitle + ".mp3");

                        MetroMessageBox.Show(this, "Вы успешно скачали: " + nameArtistOrTitle, "", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    }
                }
            }
            catch (System.ArgumentOutOfRangeException)
            {
                // если item не выбран, а кнопка скачать была нажата.
                MetroMessageBox.Show(this, "Пожалуйста выберите аудиофайл для скачивания.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void timerAudioRepeat_Tick(object sender, EventArgs e)
        {
            //MessageBox.Show();
            if (currentPositionAudio.Text == "00:00" && durationAudio.Text == "00:00")
            {
            }
            else
            {
                if (audioRepeat.Checked == true) // Если чекбокс активный, то делаем автовоспроизведение.
                {
                    if (audioNameRealTimes.Text == "Остановлено")
                    {
                        try
                        {
                            string getIniPosAudioRepeat = ini.IniRead("PlayerSettings", "AudioRepeatPos");
                            listBox1.SelectedIndex = Convert.ToInt32(getIniPosAudioRepeat); // Если трек завершился мы воспроизводим его заново.
                            WMPs.controls.play();
                        }
                        catch
                        {
                            //!
                        }
                    }
                }
            }
        }
    }
}    