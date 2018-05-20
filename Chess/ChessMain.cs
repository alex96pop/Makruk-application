using System;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using ChessLibrary;

namespace Chess
{
	/// <summary>
	/// Summary description for ChessMain.
	/// </summary>
	public class ChessMain : System.Windows.Forms.Form
	{
		private GameUI GameObj;		// The Game UI object
		public System.Windows.Forms.PictureBox WhitePlayerImage;
		public System.Windows.Forms.PictureBox BlackPlayerImage;
		public System.Windows.Forms.Label BlackPlayerName;
		public System.Windows.Forms.Label WhitePlayerName;
		public System.Windows.Forms.Label WhitePlayerTime;
		public System.Windows.Forms.Label BlackPlayerTime;
		public System.Windows.Forms.ListView lstHistory;
		public System.Windows.Forms.Panel PnlComputerThinkStatus;
		public System.Windows.Forms.ProgressBar PrgComputerThinkDepth;
		public System.Windows.Forms.Label LblComuterThinkLabel;
		public Chess.CaptureBar ChessCaptureBar;

		private System.Windows.Forms.MainMenu MainMenu;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem5;
		private System.Windows.Forms.MenuItem menuItem6;
		private System.Windows.Forms.MenuItem menuItem7;
		private System.Windows.Forms.MenuItem menuItem8;
		private System.Windows.Forms.MenuItem menuItem9;
		private System.Windows.Forms.MenuItem mnuNewGame;
		private System.Windows.Forms.MenuItem mnuFileExit;
		private System.Windows.Forms.MenuItem menuItem11;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.MenuItem menuItem14;
		private System.Windows.Forms.Timer TurnTicker;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.MenuItem mnuEditUndoMove;
		private System.Windows.Forms.MenuItem mnuEditShowLog;
		private System.Windows.Forms.MenuItem mnuShowLog;
		private System.Windows.Forms.ColumnHeader LstIndex;
		private System.Windows.Forms.ColumnHeader lstMove;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.MenuItem mnuComputerPlayer;
        private MenuItem mnuShowComputerThinkDepth;
        private MenuItem menuItem2;
        private MenuItem menuItem3;
        private MenuItem menuItem4;
        public Label whiteCountLabel;
        public Label blackCountLabel;
        private System.Windows.Forms.MenuItem mnuShowMoveHelp;
		
		public ChessMain()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChessMain));
            this.MainMenu = new System.Windows.Forms.MainMenu(this.components);
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.mnuNewGame = new System.Windows.Forms.MenuItem();
            this.menuItem9 = new System.Windows.Forms.MenuItem();
            this.mnuFileExit = new System.Windows.Forms.MenuItem();
            this.mnuEditShowLog = new System.Windows.Forms.MenuItem();
            this.mnuEditUndoMove = new System.Windows.Forms.MenuItem();
            this.menuItem11 = new System.Windows.Forms.MenuItem();
            this.mnuShowLog = new System.Windows.Forms.MenuItem();
            this.menuItem14 = new System.Windows.Forms.MenuItem();
            this.mnuShowMoveHelp = new System.Windows.Forms.MenuItem();
            this.mnuComputerPlayer = new System.Windows.Forms.MenuItem();
            this.mnuShowComputerThinkDepth = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.menuItem6 = new System.Windows.Forms.MenuItem();
            this.menuItem7 = new System.Windows.Forms.MenuItem();
            this.menuItem8 = new System.Windows.Forms.MenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.blackCountLabel = new System.Windows.Forms.Label();
            this.whiteCountLabel = new System.Windows.Forms.Label();
            this.lstHistory = new System.Windows.Forms.ListView();
            this.LstIndex = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lstMove = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.WhitePlayerTime = new System.Windows.Forms.Label();
            this.BlackPlayerTime = new System.Windows.Forms.Label();
            this.WhitePlayerName = new System.Windows.Forms.Label();
            this.BlackPlayerName = new System.Windows.Forms.Label();
            this.WhitePlayerImage = new System.Windows.Forms.PictureBox();
            this.BlackPlayerImage = new System.Windows.Forms.PictureBox();
            this.TurnTicker = new System.Windows.Forms.Timer(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.ChessCaptureBar = new Chess.CaptureBar();
            this.PnlComputerThinkStatus = new System.Windows.Forms.Panel();
            this.LblComuterThinkLabel = new System.Windows.Forms.Label();
            this.PrgComputerThinkDepth = new System.Windows.Forms.ProgressBar();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.WhitePlayerImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BlackPlayerImage)).BeginInit();
            this.panel2.SuspendLayout();
            this.PnlComputerThinkStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainMenu
            // 
            this.MainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem1,
            this.mnuEditShowLog,
            this.mnuComputerPlayer,
            this.menuItem2});
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 0;
            this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuNewGame,
            this.menuItem9,
            this.mnuFileExit});
            this.menuItem1.Text = "&File";
            // 
            // mnuNewGame
            // 
            this.mnuNewGame.Index = 0;
            this.mnuNewGame.Shortcut = System.Windows.Forms.Shortcut.CtrlN;
            this.mnuNewGame.Text = "&New";
            this.mnuNewGame.Click += new System.EventHandler(this.mnuNewGame_Click);
            // 
            // menuItem9
            // 
            this.menuItem9.Index = 1;
            this.menuItem9.Text = "-";
            // 
            // mnuFileExit
            // 
            this.mnuFileExit.Index = 2;
            this.mnuFileExit.Text = "&Exit";
            this.mnuFileExit.Click += new System.EventHandler(this.mnuFileExit_Click);
            // 
            // mnuEditShowLog
            // 
            this.mnuEditShowLog.Index = 1;
            this.mnuEditShowLog.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuEditUndoMove,
            this.menuItem11,
            this.mnuShowLog,
            this.menuItem14,
            this.mnuShowMoveHelp});
            this.mnuEditShowLog.Text = "&Edit";
            // 
            // mnuEditUndoMove
            // 
            this.mnuEditUndoMove.Index = 0;
            this.mnuEditUndoMove.Shortcut = System.Windows.Forms.Shortcut.CtrlZ;
            this.mnuEditUndoMove.Text = "&Undo";
            this.mnuEditUndoMove.Click += new System.EventHandler(this.mnuEditUndoMove_Click);
            // 
            // menuItem11
            // 
            this.menuItem11.Index = 1;
            this.menuItem11.Text = "-";
            // 
            // mnuShowLog
            // 
            this.mnuShowLog.Checked = true;
            this.mnuShowLog.Index = 2;
            this.mnuShowLog.Text = "Show Log";
            this.mnuShowLog.Click += new System.EventHandler(this.mnuShowLog_Click);
            // 
            // menuItem14
            // 
            this.menuItem14.Index = 3;
            this.menuItem14.Text = "-";
            // 
            // mnuShowMoveHelp
            // 
            this.mnuShowMoveHelp.Index = 4;
            this.mnuShowMoveHelp.Text = "Show Move Help";
            this.mnuShowMoveHelp.Click += new System.EventHandler(this.mnuShowMoveHelp_Click);
            // 
            // mnuComputerPlayer
            // 
            this.mnuComputerPlayer.Index = 2;
            this.mnuComputerPlayer.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuShowComputerThinkDepth});
            this.mnuComputerPlayer.Text = "Computer";
            // 
            // mnuShowComputerThinkDepth
            // 
            this.mnuShowComputerThinkDepth.Checked = true;
            this.mnuShowComputerThinkDepth.Index = 0;
            this.mnuShowComputerThinkDepth.Text = "Show Computer Think Depth";
            this.mnuShowComputerThinkDepth.Click += new System.EventHandler(this.mnuShowComputerThinkDepth_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 3;
            this.menuItem2.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem3,
            this.menuItem4});
            this.menuItem2.Text = "Player";
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 0;
            this.menuItem3.Text = "White Start Counting";
            this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
            // 
            // menuItem4
            // 
            this.menuItem4.Index = 1;
            this.menuItem4.Text = "Black Start Counting";
            this.menuItem4.Click += new System.EventHandler(this.menuItem4_Click);
            // 
            // menuItem5
            // 
            this.menuItem5.Index = -1;
            this.menuItem5.Text = "-";
            // 
            // menuItem6
            // 
            this.menuItem6.Index = -1;
            this.menuItem6.Text = "Beginner";
            // 
            // menuItem7
            // 
            this.menuItem7.Index = -1;
            this.menuItem7.Text = "Intermediate";
            // 
            // menuItem8
            // 
            this.menuItem8.Index = -1;
            this.menuItem8.Text = "Expert";
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.Controls.Add(this.blackCountLabel);
            this.panel1.Controls.Add(this.whiteCountLabel);
            this.panel1.Controls.Add(this.lstHistory);
            this.panel1.Controls.Add(this.WhitePlayerTime);
            this.panel1.Controls.Add(this.BlackPlayerTime);
            this.panel1.Controls.Add(this.WhitePlayerName);
            this.panel1.Controls.Add(this.BlackPlayerName);
            this.panel1.Controls.Add(this.WhitePlayerImage);
            this.panel1.Controls.Add(this.BlackPlayerImage);
            this.panel1.ForeColor = System.Drawing.Color.Red;
            this.panel1.Location = new System.Drawing.Point(504, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(208, 515);
            this.panel1.TabIndex = 0;
            // 
            // blackCountLabel
            // 
            this.blackCountLabel.AutoSize = true;
            this.blackCountLabel.Location = new System.Drawing.Point(107, 240);
            this.blackCountLabel.MaximumSize = new System.Drawing.Size(40, 20);
            this.blackCountLabel.MinimumSize = new System.Drawing.Size(40, 20);
            this.blackCountLabel.Name = "blackCountLabel";
            this.blackCountLabel.Size = new System.Drawing.Size(40, 20);
            this.blackCountLabel.TabIndex = 8;
            // 
            // whiteCountLabel
            // 
            this.whiteCountLabel.AutoSize = true;
            this.whiteCountLabel.Location = new System.Drawing.Point(29, 240);
            this.whiteCountLabel.MaximumSize = new System.Drawing.Size(40, 20);
            this.whiteCountLabel.MinimumSize = new System.Drawing.Size(40, 20);
            this.whiteCountLabel.Name = "whiteCountLabel";
            this.whiteCountLabel.Size = new System.Drawing.Size(40, 20);
            this.whiteCountLabel.TabIndex = 7;
            // 
            // lstHistory
            // 
            this.lstHistory.AllowColumnReorder = true;
            this.lstHistory.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.LstIndex,
            this.lstMove});
            this.lstHistory.GridLines = true;
            this.lstHistory.HideSelection = false;
            this.lstHistory.LabelEdit = true;
            this.lstHistory.Location = new System.Drawing.Point(18, 277);
            this.lstHistory.MultiSelect = false;
            this.lstHistory.Name = "lstHistory";
            this.lstHistory.Size = new System.Drawing.Size(154, 203);
            this.lstHistory.TabIndex = 6;
            this.lstHistory.UseCompatibleStateImageBehavior = false;
            this.lstHistory.View = System.Windows.Forms.View.Details;
            // 
            // LstIndex
            // 
            this.LstIndex.Text = "No.";
            this.LstIndex.Width = 41;
            // 
            // lstMove
            // 
            this.lstMove.Text = "Move";
            this.lstMove.Width = 107;
            // 
            // WhitePlayerTime
            // 
            this.WhitePlayerTime.BackColor = System.Drawing.Color.White;
            this.WhitePlayerTime.Font = new System.Drawing.Font("Verdana", 12.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.WhitePlayerTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.WhitePlayerTime.Location = new System.Drawing.Point(70, 138);
            this.WhitePlayerTime.Name = "WhitePlayerTime";
            this.WhitePlayerTime.Size = new System.Drawing.Size(96, 24);
            this.WhitePlayerTime.TabIndex = 5;
            // 
            // BlackPlayerTime
            // 
            this.BlackPlayerTime.BackColor = System.Drawing.Color.White;
            this.BlackPlayerTime.Font = new System.Drawing.Font("Verdana", 12.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BlackPlayerTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.BlackPlayerTime.Location = new System.Drawing.Point(70, 32);
            this.BlackPlayerTime.Name = "BlackPlayerTime";
            this.BlackPlayerTime.Size = new System.Drawing.Size(96, 24);
            this.BlackPlayerTime.TabIndex = 4;
            // 
            // WhitePlayerName
            // 
            this.WhitePlayerName.BackColor = System.Drawing.Color.Transparent;
            this.WhitePlayerName.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.WhitePlayerName.ForeColor = System.Drawing.SystemColors.ControlText;
            this.WhitePlayerName.Location = new System.Drawing.Point(24, 179);
            this.WhitePlayerName.Name = "WhitePlayerName";
            this.WhitePlayerName.Size = new System.Drawing.Size(136, 24);
            this.WhitePlayerName.TabIndex = 3;
            // 
            // BlackPlayerName
            // 
            this.BlackPlayerName.BackColor = System.Drawing.Color.Transparent;
            this.BlackPlayerName.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BlackPlayerName.ForeColor = System.Drawing.SystemColors.ControlText;
            this.BlackPlayerName.Location = new System.Drawing.Point(24, 72);
            this.BlackPlayerName.Name = "BlackPlayerName";
            this.BlackPlayerName.Size = new System.Drawing.Size(136, 24);
            this.BlackPlayerName.TabIndex = 2;
            // 
            // WhitePlayerImage
            // 
            this.WhitePlayerImage.BackColor = System.Drawing.Color.Transparent;
            this.WhitePlayerImage.Location = new System.Drawing.Point(24, 126);
            this.WhitePlayerImage.Name = "WhitePlayerImage";
            this.WhitePlayerImage.Size = new System.Drawing.Size(45, 50);
            this.WhitePlayerImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.WhitePlayerImage.TabIndex = 1;
            this.WhitePlayerImage.TabStop = false;
            // 
            // BlackPlayerImage
            // 
            this.BlackPlayerImage.BackColor = System.Drawing.Color.Transparent;
            this.BlackPlayerImage.Location = new System.Drawing.Point(24, 20);
            this.BlackPlayerImage.Name = "BlackPlayerImage";
            this.BlackPlayerImage.Size = new System.Drawing.Size(45, 50);
            this.BlackPlayerImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.BlackPlayerImage.TabIndex = 0;
            this.BlackPlayerImage.TabStop = false;
            // 
            // TurnTicker
            // 
            this.TurnTicker.Enabled = true;
            this.TurnTicker.Interval = 500;
            this.TurnTicker.Tick += new System.EventHandler(this.TurnTicker_Tick);
            // 
            // panel2
            // 
            this.panel2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel2.BackgroundImage")));
            this.panel2.Controls.Add(this.ChessCaptureBar);
            this.panel2.Controls.Add(this.PnlComputerThinkStatus);
            this.panel2.Location = new System.Drawing.Point(0, 504);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(695, 86);
            this.panel2.TabIndex = 1;
            // 
            // ChessCaptureBar
            // 
            this.ChessCaptureBar.Location = new System.Drawing.Point(18, 16);
            this.ChessCaptureBar.Name = "ChessCaptureBar";
            this.ChessCaptureBar.Size = new System.Drawing.Size(660, 55);
            this.ChessCaptureBar.TabIndex = 1;
            // 
            // PnlComputerThinkStatus
            // 
            this.PnlComputerThinkStatus.BackColor = System.Drawing.Color.White;
            this.PnlComputerThinkStatus.Controls.Add(this.LblComuterThinkLabel);
            this.PnlComputerThinkStatus.Controls.Add(this.PrgComputerThinkDepth);
            this.PnlComputerThinkStatus.Location = new System.Drawing.Point(20, 16);
            this.PnlComputerThinkStatus.Name = "PnlComputerThinkStatus";
            this.PnlComputerThinkStatus.Size = new System.Drawing.Size(655, 55);
            this.PnlComputerThinkStatus.TabIndex = 0;
            // 
            // LblComuterThinkLabel
            // 
            this.LblComuterThinkLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblComuterThinkLabel.ForeColor = System.Drawing.Color.Blue;
            this.LblComuterThinkLabel.Location = new System.Drawing.Point(5, 32);
            this.LblComuterThinkLabel.Name = "LblComuterThinkLabel";
            this.LblComuterThinkLabel.Size = new System.Drawing.Size(640, 16);
            this.LblComuterThinkLabel.TabIndex = 1;
            // 
            // PrgComputerThinkDepth
            // 
            this.PrgComputerThinkDepth.Location = new System.Drawing.Point(3, 3);
            this.PrgComputerThinkDepth.Name = "PrgComputerThinkDepth";
            this.PrgComputerThinkDepth.Size = new System.Drawing.Size(659, 24);
            this.PrgComputerThinkDepth.TabIndex = 0;
            // 
            // ChessMain
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(694, 589);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Menu = this.MainMenu;
            this.Name = "ChessMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quick Chess";
            this.Load += new System.EventHandler(this.ChessMain_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.WhitePlayerImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BlackPlayerImage)).EndInit();
            this.panel2.ResumeLayout(false);
            this.PnlComputerThinkStatus.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
            Application.EnableVisualStyles();
			Application.Run(new ChessMain());
		}

        /// <summary>
        /// Enable the Save Menu
        /// </summary>
        public void EnableSaveMenu()
        {
            //mnuSaveGame.Enabled = true;
        }

        /// <summary>
        /// Set/Re-set the Game Preferences menu
        /// </summary>

        // Chess board load event. Initialize all the chess positions
        private void ChessMain_Load(object sender, System.EventArgs e)
        {
            GameObj = new GameUI(this);
            this.mnuShowMoveHelp.Checked = GameObj.ShowMoveHelp;    // Show the check box
        }

        // Menu Handler
        private void mnuNewGame_Click(object sender, System.EventArgs e)
		{
			GameObj.NewGame();	// Initialize the new game
            Rules.whiteStartCounting = false;
            Rules.blackStartCounting = false;
            this.menuItem3.Checked = false;
            this.menuItem3.Text = "White Start Counting";
            this.menuItem4.Checked = false;
            this.menuItem4.Text = "Black Start Counting";

        }

		private void mnuFileExit_Click(object sender, System.EventArgs e)
		{
			Application.Exit();		// Send the terminate signal to all running threads
		}

		private void mnuShowMoveHelp_Click(object sender, System.EventArgs e)
		{
			GameObj.ShowMoveHelp = !GameObj.ShowMoveHelp;	// Reverse the show help check box state
			this.mnuShowMoveHelp.Checked = GameObj.ShowMoveHelp;	// Show the check box
		}

		private void TurnTicker_Tick(object sender, System.EventArgs e)
		{
            if (Rules.whiteStartCounting)
                whiteCountLabel.Text = Board.maxMoves.ToString();
            else
                whiteCountLabel.Text = "";
            if (Rules.blackStartCounting)
                blackCountLabel.Text = Board.maxMoves.ToString();
            else
                blackCountLabel.Text = "";


			if (GameObj.IsRunning && !GameObj.IsOver )	// game is in active state
			{
				GameObj.ShowPlayerTurn();
			}
		}

		private void mnuEditUndoMove_Click(object sender, System.EventArgs e)
		{
			if (GameObj.IsRunning)	// game is in active state
			{
				GameObj.UndoMove();
			}		
		}


		private void mnuShowLog_Click(object sender, System.EventArgs e)
		{
			mnuShowLog.Checked = !mnuShowLog.Checked;

			if (mnuShowLog.Checked)	// Need to show the log
			{
				this.Width +=190;
				this.Height +=96;
			}
			else
			{
				this.Width -=190;
				this.Height -=96;
			}
		}


		private void hScrollBar1_Scroll(object sender, System.Windows.Forms.ScrollEventArgs e)
		{
		}

        private void mnuShowComputerThinkDepth_Click(object sender, EventArgs e)
        {
            // Toggle the status
            this.mnuShowComputerThinkDepth.Checked = !this.mnuShowComputerThinkDepth.Checked;

            // Update the game status
            GameObj.ShowComputerThinkingProgres = this.mnuShowComputerThinkDepth.Checked;
        }

        private void menuItem3_Click(object sender, EventArgs e)
        {
            // If white player's turn, he can start counting until stalemale or stop counting
            if (this.menuItem4.Checked == false && Game.GameTurn == Side.SideType.White)
            {
                if (this.menuItem3.Checked == false )
                {
                    int x = GameObj.ChessGame.Board.VerifyCountforWhite();
                    if (x > 0)
                    {
                        this.menuItem3.Checked = true;
                        this.menuItem3.Text = "White Stop Counting";
                        Board.maxMoves = x;
                        Rules.whiteStartCounting = true;
                    }

                }
                else
                {
                    this.menuItem3.Checked = false;
                    this.menuItem3.Text = "White Start Counting";
                    Rules.whiteStartCounting = false;
                }
            }
        }

        private void menuItem4_Click(object sender, EventArgs e)
        {
            // If white player's turn, he can start counting until stalemale or stop counting
            if (this.menuItem3.Checked == false && Game.GameTurn == Side.SideType.Black)
            {
                if (this.menuItem4.Checked == false)
                {
                    int x = GameObj.ChessGame.Board.VerifyCountforBlack();
                    if (x > 0)
                    {
                        this.menuItem4.Checked = true;
                        this.menuItem4.Text = "Black Stop Counting";
                        Rules.blackStartCounting = true;
                        Board.maxMoves = x;
                    }
                }
                else
                {
                    this.menuItem4.Checked = false;
                    this.menuItem4.Text = "Black Start Counting";
                    Rules.blackStartCounting = false;
                }
            }
        }
    }
}
