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
    partial class authVk
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(authVk));
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.metroButton1 = new MetroFramework.Controls.MetroButton();
            this.Author = new MetroFramework.Controls.MetroLink();
            this.SuspendLayout();
            // 
            // webBrowser1
            // 
            this.webBrowser1.Location = new System.Drawing.Point(4, 27);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(676, 361);
            this.webBrowser1.TabIndex = 0;
            this.webBrowser1.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser1_DocumentCompleted);
            // 
            // metroButton1
            // 
            this.metroButton1.Location = new System.Drawing.Point(226, 401);
            this.metroButton1.Name = "metroButton1";
            this.metroButton1.Size = new System.Drawing.Size(212, 39);
            this.metroButton1.TabIndex = 1;
            this.metroButton1.Text = "Войти через VK";
            this.metroButton1.UseSelectable = true;
            this.metroButton1.Click += new System.EventHandler(this.vkLogin_Click);
            // 
            // Author
            // 
            this.Author.Location = new System.Drawing.Point(556, 431);
            this.Author.Name = "Author";
            this.Author.Size = new System.Drawing.Size(124, 23);
            this.Author.TabIndex = 2;
            this.Author.Text = "by qNULL © 2016";
            this.Author.UseSelectable = true;
            this.Author.Click += new System.EventHandler(this.Author_Click);
            // 
            // authVk
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(683, 452);
            this.Controls.Add(this.Author);
            this.Controls.Add(this.metroButton1);
            this.Controls.Add(this.webBrowser1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "authVk";
            this.Resizable = false;
            this.Style = MetroFramework.MetroColorStyle.Green;
            this.Text = "authVk";
            this.Theme = MetroFramework.MetroThemeStyle.Default;
            this.Load += new System.EventHandler(this.authVk_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser webBrowser1;
        private MetroFramework.Controls.MetroButton metroButton1;
        private MetroFramework.Controls.MetroLink Author;
    }
}