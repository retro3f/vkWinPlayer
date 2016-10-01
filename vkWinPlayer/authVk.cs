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

using MetroFramework;
using MetroFramework.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace vkWinPlayer
{
    public partial class authVk : MetroForm
    {
        // run ini lib
        IniLib ini = new IniLib(Application.StartupPath + "\\vkWinPlayerSettings.ini");

        public authVk()
        {
            InitializeComponent();
        }

        public string appIds;
        public string scope;
        public string appSecretKey;


        private void authVk_Load(object sender, EventArgs e)
        {
            if (!System.IO.File.Exists(Application.StartupPath + "\\vkWinPlayerSettings.ini")) // првоеряем конфиг
            {
                MetroMessageBox.Show(this, "Конфиг файл(vkWinPlayerSettings.ini) не найден и будет создан.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.IO.StreamWriter wIniConfigFile = new System.IO.StreamWriter(Application.StartupPath + "\\vkWinPlayerSettings.ini");
                wIniConfigFile.WriteLine("[Settings]");
                wIniConfigFile.WriteLine("appId=");
                wIniConfigFile.WriteLine("scope=offline,audio");
                wIniConfigFile.WriteLine("appSecretKey=");
                wIniConfigFile.WriteLine("[UserInfo]");
                wIniConfigFile.WriteLine("access_token=");
                wIniConfigFile.WriteLine("lastfm_access_token=");
                wIniConfigFile.Close();
            }
            else
            {
                getIniOptions();
                Process.Start("cmd.exe", "/C RunDll32.exe InetCpl.cpl,ClearMyTracksByProcess 255");
                // загружаем главную страницу
                webBrowser1.Refresh();
                string htmlMainPage = @"
            <html>
<head>
<title>vkWinPlayer | Auth</title>
</head>
<body>
<center>
 <p><font size='5'  face='Segoe UI Light'>Добро пожаловать в vkWinPlayer!</font></p>
</center>
<br>
<br>
<br>
<br>
<br>
<br>
<br>
<br>
<br>
<br>
<br>
<center>
<p><font size='5'  face='Segoe UI Light'>Давайте начнем</font></p>
&#8659 &#8659 &#8659
</center>
</center>

</body>
<html>
            ";
                webBrowser1.DocumentText = htmlMainPage;
            }
        }

        public void getIniOptions()
        {
            appIds = ini.IniRead("Settings", "appId");
            scope = ini.IniRead("Settings", "scope");
            appSecretKey = ini.IniRead("Settings", "appSecretKey");
        }
        private void vkLogin_Click(object sender, EventArgs e)
        {
            if (appIds == "") // проверяем указан ли appIds.
            {
                MetroMessageBox.Show(this, "Пожалуйста укажите appId в конфиге", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (scope == "")
            { // проверяем указанны ли права доступа.
                MetroMessageBox.Show(this, "Пожалуйста укажите права доступа приложения в конфиге", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (appSecretKey == "")
            { // проверяем указанн ли appSecretKey.
                MetroMessageBox.Show(this, "Пожалуйста укажите appSecretKey приложения в конфиге", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string tokenInqTemplate = "https://oauth.vk.com/authorize?client_id={0}&scope={1}&redirect_uri=http://vk.com/blank.html&response_type=code";
                string urlAuth = String.Format(tokenInqTemplate, appIds, scope);

                webBrowser1.ScriptErrorsSuppressed = true;
                webBrowser1.Navigate(urlAuth);
            }
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (webBrowser1.DocumentTitle == "OAuth Blank")
            {
                string urlOnes = webBrowser1.Url.ToString();
                string urlTwos = urlOnes.Split('=')[1];
                string secret_key = urlTwos.Split('&')[0];
                GetToken(secret_key);
            }
        }

        public void GetToken(string Code)
        {
            if (Code == "")
            {
                MetroMessageBox.Show(this, "Ошибка при передачи secretKey...", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string urlGetToken = "https://oauth.vk.com/access_token?client_id={0}&client_secret={1}&code={2}&redirect_uri=http://vk.com/blank.html";
                string tokenGo = String.Format(urlGetToken, appIds, appSecretKey, Code); // ид приложения, секретный ключ приложения, серверный секретный ключ.
                WebClient webClient = new WebClient();
                string responseToken = webClient.DownloadString(tokenGo);

                dynamic q = JsonConvert.DeserializeObject<dynamic>(responseToken);

                string AccessToken = q.access_token;
                string UserUID = q.user_id;

                if (AccessToken == "")
                {
                    MetroMessageBox.Show(this, "Не удалось получить access_token...", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Environment.Exit(0);
                }
                else if (UserUID == "")
                {
                    MetroMessageBox.Show(this, "Не удалось получить UID...", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Environment.Exit(0);
                }
                else
                {
                    {
                        ini.IniWrite("UserInfo", "access_token", AccessToken);
                        ini.IniWrite("UserInfo", "uid", UserUID);
                        ini.IniWrite("UserInfo", "auth", "ok");
                        ini.IniWrite("UserInfo", "lastfm_access_token", "");

                        Form1 ifrm = new Form1();
                        ifrm.Left = this.Left; 
                        ifrm.Top = this.Top;
                        ifrm.Show();
                        this.Hide();

                    }
                }
            }
        }

        private void Author_Click(object sender, EventArgs e)
        {
            Process.Start("http://github.com/retro3f");
        }


    }
}
