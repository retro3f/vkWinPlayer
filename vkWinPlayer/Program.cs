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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Windows.Forms;


namespace vkWinPlayer
{
    static class Program
    {


        public static bool CheckForInternetConnection()
        {
            IPStatus status = IPStatus.TimedOut;
            try
            {
                Ping ping = new Ping();
                PingReply reply = ping.Send(@"google.com");
                status = reply.Status;
            }
            catch { }
            if (status != IPStatus.Success)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                try
                {
                    if (CheckForInternetConnection() == true)
                    {

                        Application.EnableVisualStyles();
                        Application.SetCompatibleTextRenderingDefault(false);
                        IniLib ini = new IniLib(Application.StartupPath + "\\vkWinPlayerSettings.ini");
                        string statusAuth = ini.IniRead("UserInfo", "auth");

                        // Если ююзер авторизован. А что то лучше я не придумал...:)
                        if (statusAuth == "ok")
                        {
                            Application.Run(new Form1());
                        }
                        else
                        {
                            Application.Run(new authVk());
                        }
                    }
                    else
                    {
                        MessageBox.Show("Пожалуйста проверьте подключение к интернету!", "vkWinPlayer::ERROR_CONNECTION", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Environment.Exit(0);
                    }
                }
                catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
                {
                    //System.NullReferenceException
                }
            }
            catch (System.NullReferenceException)
            {

                //MetroMessageBox.Show("System.NullReferenceException", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
} 