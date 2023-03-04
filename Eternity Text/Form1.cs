using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using WeAreDevs_API;

namespace Eternity_Text
{
	public partial class Form1 : Form
	{
		ExploitAPI Eternity = new ExploitAPI();
		Point lastPoint;
		public Form1()
		{
			InitializeComponent();

			TextEditor.Navigate(new Uri(string.Format("file:///{0}/ace/index.html", Directory.GetCurrentDirectory())));
		}

		private void AddTab(object sender, EventArgs e)
		{
			TabPage tabPage = new TabPage();
			{
				tabPage.Name = "Script";
				tabPage.Text = "Script " + visualStudioTabControl1.TabCount;
				tabPage.Parent = visualStudioTabControl1;
				TextEditor.Dock = DockStyle.Fill;
				TextEditor.Navigate(new Uri(string.Format("file:///{0}/ace/index.html", Directory.GetCurrentDirectory())));
				TextEditor.Parent = tabPage;
				TextEditor.Name = "TextEditor";
				TextEditor = new WebBrowser();
				visualStudioTabControl1.SelectTab(tabPage);
			}
		}

		private void Close(object sender, EventArgs e)
		{
			this.Close();
		}

		private void Minimize(object sender, EventArgs e)
		{
			WindowState = FormWindowState.Minimized;
		}

		private void Attach(object sender, EventArgs e)
		{
			Eternity.LaunchExploit();
		}

		private void Execute(object sender, EventArgs e)
		{
			try
			{
				if (visualStudioTabControl1.TabPages.Count > 0)
				{
					TabControl.TabPageCollection pages = visualStudioTabControl1.TabPages;

					for (int i = 0; i < pages.Count; i++)
					{
						WebBrowser TextEditor = pages[i].Controls.Find("TextEditor", true).FirstOrDefault<Control>() as WebBrowser;
						HtmlDocument document = TextEditor.Document;
						string scriptName = "GetText";
						object[] args = new string[0];
						object obj = document.InvokeScript(scriptName, args);
						string script = obj.ToString();

						Eternity.SendLuaScript(TextEditor.DocumentText);
					}
				}
			}
			catch { }
		}

		private void Form1_MouseDown(object sender, MouseEventArgs e)
		{
			lastPoint = new Point(e.X, e.Y);
		}

		private void Form1_MouseMove(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				this.Left += e.X - lastPoint.X;
				this.Top += e.Y - lastPoint.Y;
			}
		}
	}
}
