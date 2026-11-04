using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Runtime.InteropServices;
using System.Text;

namespace TestWinAPI
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class MainForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button btnConnect;
		private const int WM_CLOSE = 16;
		private const int BN_CLICKED = 245;
        private int m_hwndBeas = 0;
        private GroupBox groupBoxConnection;
        private GroupBox groupBoxSend;

        private String is_receipt;
        private Button btnDisconnect;
        private GroupBox groupBoxGet;
        private TextBox txtgetvalue;
        private Button btnGet;
        private TextBox txtsetscript;
        private Button btnSend;
        private CheckBox chkreturnresult;
        private CheckBox chkcurrentwindow;
        private GroupBox groupBoxResult;
        private Button btnclear;
        private ListBox listBoxResult;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;
		/// <summary>
		/// The FindWindow API
		/// </summary>
		/// <param name="lpClassName">the class name for the window to search for</param>
		/// <param name="lpWindowName">the name of the window to search for</param>
		/// <returns></returns>
		[DllImport("User32.dll")]
		public static extern Int32 FindWindow(String lpClassName,String lpWindowName);

		/// <summary>
		/// The SendMessage API
		/// </summary>
		/// <param name="hWnd">handle to the required window</param>
		/// <param name="msg">the system/Custom message to send</param>
		/// <param name="wParam">first message parameter</param>
		/// <param name="lParam">second message parameter</param>
		/// <returns></returns>
		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		public static extern int SendMessage(int hWnd, int msg, int wParam, IntPtr lParam);
		
		/// <summary>
		/// The FindWindowEx API
		/// </summary>
		/// <param name="parentHandle">a handle to the parent window </param>
		/// <param name="childAfter">a handle to the child window to start search after</param>
		/// <param name="className">the class name for the window to search for</param>
		/// <param name="windowTitle">the name of the window to search for</param>
		/// <returns></returns>
		[DllImport("user32.dll", SetLastError = true)]
		public static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string className,  string  windowTitle);

		public MainForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.btnConnect = new System.Windows.Forms.Button();
            this.groupBoxConnection = new System.Windows.Forms.GroupBox();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.groupBoxSend = new System.Windows.Forms.GroupBox();
            this.chkcurrentwindow = new System.Windows.Forms.CheckBox();
            this.chkreturnresult = new System.Windows.Forms.CheckBox();
            this.txtsetscript = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.groupBoxGet = new System.Windows.Forms.GroupBox();
            this.txtgetvalue = new System.Windows.Forms.TextBox();
            this.btnGet = new System.Windows.Forms.Button();
            this.groupBoxResult = new System.Windows.Forms.GroupBox();
            this.btnclear = new System.Windows.Forms.Button();
            this.listBoxResult = new System.Windows.Forms.ListBox();
            this.groupBoxConnection.SuspendLayout();
            this.groupBoxSend.SuspendLayout();
            this.groupBoxGet.SuspendLayout();
            this.groupBoxResult.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(6, 19);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(99, 24);
            this.btnConnect.TabIndex = 0;
            this.btnConnect.Text = "Connect to Beas";
            this.btnConnect.Click += new System.EventHandler(this.btnGetWindows_Click);
            // 
            // groupBoxConnection
            // 
            this.groupBoxConnection.Controls.Add(this.btnDisconnect);
            this.groupBoxConnection.Controls.Add(this.btnConnect);
            this.groupBoxConnection.Location = new System.Drawing.Point(12, 12);
            this.groupBoxConnection.Name = "groupBoxConnection";
            this.groupBoxConnection.Size = new System.Drawing.Size(259, 51);
            this.groupBoxConnection.TabIndex = 7;
            this.groupBoxConnection.TabStop = false;
            this.groupBoxConnection.Text = "Connection";
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Enabled = false;
            this.btnDisconnect.Location = new System.Drawing.Point(111, 19);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(134, 24);
            this.btnDisconnect.TabIndex = 1;
            this.btnDisconnect.Text = "Disconnect From Beas";
            this.btnDisconnect.Click += new System.EventHandler(this.buttonDisconnect_Click);
            // 
            // groupBoxSend
            // 
            this.groupBoxSend.Controls.Add(this.chkcurrentwindow);
            this.groupBoxSend.Controls.Add(this.chkreturnresult);
            this.groupBoxSend.Controls.Add(this.txtsetscript);
            this.groupBoxSend.Controls.Add(this.btnSend);
            this.groupBoxSend.Enabled = false;
            this.groupBoxSend.Location = new System.Drawing.Point(12, 69);
            this.groupBoxSend.Name = "groupBoxSend";
            this.groupBoxSend.Size = new System.Drawing.Size(404, 113);
            this.groupBoxSend.TabIndex = 8;
            this.groupBoxSend.TabStop = false;
            this.groupBoxSend.Text = "Message System - Send Commands";
            // 
            // chkcurrentwindow
            // 
            this.chkcurrentwindow.AutoSize = true;
            this.chkcurrentwindow.Checked = true;
            this.chkcurrentwindow.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkcurrentwindow.Location = new System.Drawing.Point(265, 75);
            this.chkcurrentwindow.Name = "chkcurrentwindow";
            this.chkcurrentwindow.Size = new System.Drawing.Size(136, 17);
            this.chkcurrentwindow.TabIndex = 15;
            this.chkcurrentwindow.Text = "Exec. in ActiveWindow";
            this.chkcurrentwindow.UseVisualStyleBackColor = true;
            // 
            // chkreturnresult
            // 
            this.chkreturnresult.AutoSize = true;
            this.chkreturnresult.Checked = true;
            this.chkreturnresult.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkreturnresult.Location = new System.Drawing.Point(265, 52);
            this.chkreturnresult.Name = "chkreturnresult";
            this.chkreturnresult.Size = new System.Drawing.Size(91, 17);
            this.chkreturnresult.TabIndex = 14;
            this.chkreturnresult.Text = "Return Result";
            this.chkreturnresult.UseVisualStyleBackColor = true;
            // 
            // txtsetscript
            // 
            this.txtsetscript.Location = new System.Drawing.Point(6, 21);
            this.txtsetscript.Multiline = true;
            this.txtsetscript.Name = "txtsetscript";
            this.txtsetscript.Size = new System.Drawing.Size(253, 81);
            this.txtsetscript.TabIndex = 13;
            this.txtsetscript.Text = "messagebox=hola";
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(265, 21);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(133, 25);
            this.btnSend.TabIndex = 9;
            this.btnSend.Text = "Execute Script.";
            this.btnSend.Click += new System.EventHandler(this.buttonSend_Click);
            // 
            // groupBoxGet
            // 
            this.groupBoxGet.Controls.Add(this.txtgetvalue);
            this.groupBoxGet.Controls.Add(this.btnGet);
            this.groupBoxGet.Enabled = false;
            this.groupBoxGet.Location = new System.Drawing.Point(422, 69);
            this.groupBoxGet.Name = "groupBoxGet";
            this.groupBoxGet.Size = new System.Drawing.Size(264, 113);
            this.groupBoxGet.TabIndex = 9;
            this.groupBoxGet.TabStop = false;
            this.groupBoxGet.Text = "Message System - Get Values";
            // 
            // txtgetvalue
            // 
            this.txtgetvalue.Location = new System.Drawing.Point(6, 19);
            this.txtgetvalue.Multiline = true;
            this.txtgetvalue.Name = "txtgetvalue";
            this.txtgetvalue.Size = new System.Drawing.Size(167, 83);
            this.txtgetvalue.TabIndex = 12;
            this.txtgetvalue.Text = "<setup:dbversion>";
            // 
            // btnGet
            // 
            this.btnGet.Location = new System.Drawing.Point(179, 19);
            this.btnGet.Name = "btnGet";
            this.btnGet.Size = new System.Drawing.Size(79, 25);
            this.btnGet.TabIndex = 11;
            this.btnGet.Text = "GetValue";
            this.btnGet.Click += new System.EventHandler(this.buttonGet_Click);
            // 
            // groupBoxResult
            // 
            this.groupBoxResult.Controls.Add(this.btnclear);
            this.groupBoxResult.Controls.Add(this.listBoxResult);
            this.groupBoxResult.Enabled = false;
            this.groupBoxResult.Location = new System.Drawing.Point(11, 188);
            this.groupBoxResult.Name = "groupBoxResult";
            this.groupBoxResult.Size = new System.Drawing.Size(675, 90);
            this.groupBoxResult.TabIndex = 10;
            this.groupBoxResult.TabStop = false;
            this.groupBoxResult.Text = "Result";
            // 
            // btnclear
            // 
            this.btnclear.Location = new System.Drawing.Point(590, 19);
            this.btnclear.Name = "btnclear";
            this.btnclear.Size = new System.Drawing.Size(79, 24);
            this.btnclear.TabIndex = 1;
            this.btnclear.Text = "Clear List";
            this.btnclear.UseVisualStyleBackColor = true;
            this.btnclear.Click += new System.EventHandler(this.buttonclear_Click);
            // 
            // listBoxResult
            // 
            this.listBoxResult.FormattingEnabled = true;
            this.listBoxResult.Location = new System.Drawing.Point(7, 20);
            this.listBoxResult.Name = "listBoxResult";
            this.listBoxResult.Size = new System.Drawing.Size(577, 56);
            this.listBoxResult.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(698, 298);
            this.Controls.Add(this.groupBoxResult);
            this.Controls.Add(this.groupBoxGet);
            this.Controls.Add(this.groupBoxSend);
            this.Controls.Add(this.groupBoxConnection);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Test Beas Messages";
            this.groupBoxConnection.ResumeLayout(false);
            this.groupBoxSend.ResumeLayout(false);
            this.groupBoxSend.PerformLayout();
            this.groupBoxGet.ResumeLayout(false);
            this.groupBoxGet.PerformLayout();
            this.groupBoxResult.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new MainForm());
		}

		private void btnGetWindows_Click(object sender, System.EventArgs e)
		{
            //Connect to beas is easy, we simple have to connect to "beaswindow:appid" appid--> SAP identifier
            m_hwndBeas = FindWindow(null, "beaswindow:0");

            if (m_hwndBeas == 0)
			{
                //Maybe beas was started as Standalone app, we try to find this window
                m_hwndBeas = FindWindow(null, "beaswindow:single");
                if (m_hwndBeas == 0)
                {
                    MessageBox.Show(this, "Couldn't find Beas application.", "Test Beas Messages");
                }
			}

            SendMessageToBeas(m_hwndBeas, false, "app=external=register=handle=" + this.Handle.ToString() + "<tab>name=B1UP<tab>subscribe=Y", false);
        }
        private void SendMessageToBeas(int hwndBeas, Boolean bActivewindow, string strmessage, Boolean bReturnResult)
        {
            string strMessageToSend = "";
            if (bReturnResult == true)
            {
                strMessageToSend += "[handle=" + this.Handle.ToString() + "]";
            }

            strMessageToSend += strmessage;

            if (bActivewindow == true)
                strMessageToSend += "[executew]";
            else
                strMessageToSend += "[execute]";
            

            //We have to send word by word
            for (int i=0; i< strMessageToSend.Length;i++)
            {
                SendMessage(hwndBeas, 1025, strMessageToSend[i], IntPtr.Zero);
            }
            
        }

        private void GetValueFromBeas(int hwndBeas, string strmessage)
        {
            string strMessageToSend = "[handle=" + this.Handle.ToString() + "]";
            strMessageToSend += strmessage;
            strMessageToSend += "[execvalue]";
            
            //We have to send word by word
            for (int i = 0; i < strMessageToSend.Length; i++)
            {
                SendMessage(hwndBeas, 1025, strMessageToSend[i], IntPtr.Zero);
            }

        }

        
                protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == 1025)
            {
                is_receipt += char.ConvertFromUtf32(m.WParam.ToInt32());
                is_receipt = is_receipt.ToLower();
                if (is_receipt.Length >= 9)
                {
                    string strcheck = is_receipt.Substring(is_receipt.Length - 9);
                    if (strcheck == "[execute]")
                    {                        
                        string strcommand = is_receipt.Substring(0, is_receipt.Length - 9);
                        if (strcommand.StartsWith("welcome"))
                        {
                            groupBoxSend.Enabled = true;
                            groupBoxGet.Enabled = true;
                            groupBoxResult.Enabled = true;
                            btnDisconnect.Enabled = true;
                            btnConnect.Enabled = false;
                        }
                        else if (strcommand.StartsWith("app"))
                        {
                            groupBoxSend.Enabled = false;
                            groupBoxGet.Enabled = false;
                            groupBoxResult.Enabled = false;
                            btnDisconnect.Enabled = false;
                            btnConnect.Enabled = true;                            
                        }
                        else if (strcommand == "1")
                        {
                            strcommand = "ok";
                        }
                        else if (strcommand == "1")
                        {
                            strcommand = "False";
                        }
                        listBoxResult.Items.Add(strcommand);
                        is_receipt = "";

                    }
                }              
            }
                    
        }

               
        private void buttonDisconnect_Click(object sender, EventArgs e)
        {
            SendMessageToBeas(m_hwndBeas, false, "app=external=unregister=handle=" + this.Handle.ToString(), true);
        }
       
        private void buttonGet_Click(object sender, EventArgs e)
        {
            GetValueFromBeas(m_hwndBeas, txtgetvalue.Text);
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {            
            SendMessageToBeas(m_hwndBeas, chkcurrentwindow.Checked, txtsetscript.Text, chkreturnresult.Checked);
            
        }

        private void buttonclear_Click(object sender, EventArgs e)
        {
            listBoxResult.Items.Clear();
        }
    }
}
