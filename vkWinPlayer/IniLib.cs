/**
 * ---------------------------------------------------------
 * 
 * IniLib - Ini read, write
 * @Author GROM - <botx68@gmail.com>
 * @WebSite: https://retro3f.github.io/ | https://github.com/retro3f
 * @About: библиотека для записи INI файлов
 * @Date: 09.08.2016
 * @Programming language: C#
 * 
 *----------------------------------------------------------
 */
using System;
using System.Runtime.InteropServices;
using System.Text;

namespace vkWinPlayer
{
    /// <summary>
    /// Создание нового INI-файла для хранения данных
    /// </summary>
    public class IniLib
    {
        public string path;

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section,
            string key,string val,string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section,
            string key,string def, StringBuilder retVal,
            int size,string filePath);

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <PARAM name="INIPath">Путь к INI-файлу</PARAM>
        public IniLib(string INIPath)
        {
            path = INIPath;
        }
        /// <summary>
        /// Запись данных в INI-файл
        /// </summary>
        /// <PARAM name="Section"></PARAM>
        /// Название секции
        /// <PARAM name="Key"></PARAM>
        /// Имя ключа
        /// <PARAM name="Value"></PARAM>
        /// Значение
        public void IniWrite(string Section,string Key,string Value)
        {
            WritePrivateProfileString(Section,Key,Value,this.path);
        }
        
        /// <summary>
        /// Чтение данных из INI-файла
        /// </summary>
        /// <PARAM name="Section"></PARAM>
        /// <PARAM name="Key"></PARAM>
        /// <returns>Значение заданного ключа</returns>
        public string IniRead(string Section,string Key)
        {
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(Section,Key,"",temp, 255, this.path);
            return temp.ToString();
        }
    }
}
