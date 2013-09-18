/*
 * Created by SharpDevelop.
 * User: Stephen
 * Date: 5/8/2008
 * Time: 5:28 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace Theory_Craft
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.gameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.loadParty1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.loadParty2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.resetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.party1Name = new System.Windows.Forms.TextBox();
			this.party2Name = new System.Windows.Forms.TextBox();
			this.party1char1name = new System.Windows.Forms.TextBox();
			this.party1char1hits = new System.Windows.Forms.TextBox();
			this.party1char2hits = new System.Windows.Forms.TextBox();
			this.party1char2name = new System.Windows.Forms.TextBox();
			this.party1char3hits = new System.Windows.Forms.TextBox();
			this.party1char3name = new System.Windows.Forms.TextBox();
			this.party1char4hits = new System.Windows.Forms.TextBox();
			this.party1char4name = new System.Windows.Forms.TextBox();
			this.party1char5hits = new System.Windows.Forms.TextBox();
			this.party1char5name = new System.Windows.Forms.TextBox();
			this.party1char6hits = new System.Windows.Forms.TextBox();
			this.party1char6name = new System.Windows.Forms.TextBox();
			this.party2char1hits = new System.Windows.Forms.TextBox();
			this.party2char1name = new System.Windows.Forms.TextBox();
			this.party2char2hits = new System.Windows.Forms.TextBox();
			this.party2char2name = new System.Windows.Forms.TextBox();
			this.party2char3hits = new System.Windows.Forms.TextBox();
			this.party2char3name = new System.Windows.Forms.TextBox();
			this.party2char4hits = new System.Windows.Forms.TextBox();
			this.party2char4name = new System.Windows.Forms.TextBox();
			this.party2char5hits = new System.Windows.Forms.TextBox();
			this.party2char5name = new System.Windows.Forms.TextBox();
			this.party2char6hits = new System.Windows.Forms.TextBox();
			this.party2char6name = new System.Windows.Forms.TextBox();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.button1 = new System.Windows.Forms.Button();
			this.mainCombatWindow = new System.Windows.Forms.RichTextBox();
			this.menuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.gameToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(820, 24);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// gameToolStripMenuItem
			// 
			this.gameToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.loadParty1ToolStripMenuItem,
									this.loadParty2ToolStripMenuItem,
									this.resetToolStripMenuItem,
									this.exitToolStripMenuItem});
			this.gameToolStripMenuItem.Name = "gameToolStripMenuItem";
			this.gameToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
			this.gameToolStripMenuItem.Text = "Game";
			// 
			// loadParty1ToolStripMenuItem
			// 
			this.loadParty1ToolStripMenuItem.Name = "loadParty1ToolStripMenuItem";
			this.loadParty1ToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1;
			this.loadParty1ToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
			this.loadParty1ToolStripMenuItem.Text = "Load Party 1";
			this.loadParty1ToolStripMenuItem.Click += new System.EventHandler(this.LoadParty1ToolStripMenuItemClick);
			// 
			// loadParty2ToolStripMenuItem
			// 
			this.loadParty2ToolStripMenuItem.Name = "loadParty2ToolStripMenuItem";
			this.loadParty2ToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F2;
			this.loadParty2ToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
			this.loadParty2ToolStripMenuItem.Text = "Load Party 2";
			this.loadParty2ToolStripMenuItem.Click += new System.EventHandler(this.LoadParty2ToolStripMenuItemClick);
			// 
			// resetToolStripMenuItem
			// 
			this.resetToolStripMenuItem.Name = "resetToolStripMenuItem";
			this.resetToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
			this.resetToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
			this.resetToolStripMenuItem.Text = "Reset";
			this.resetToolStripMenuItem.Click += new System.EventHandler(this.ResetToolStripMenuItemClick);
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
			this.exitToolStripMenuItem.Text = "Exit";
			this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItemClick);
			// 
			// party1Name
			// 
			this.party1Name.BackColor = System.Drawing.Color.Red;
			this.party1Name.ForeColor = System.Drawing.Color.White;
			this.party1Name.Location = new System.Drawing.Point(12, 143);
			this.party1Name.Name = "party1Name";
			this.party1Name.ReadOnly = true;
			this.party1Name.Size = new System.Drawing.Size(184, 20);
			this.party1Name.TabIndex = 2;
			// 
			// party2Name
			// 
			this.party2Name.BackColor = System.Drawing.Color.Blue;
			this.party2Name.ForeColor = System.Drawing.Color.White;
			this.party2Name.Location = new System.Drawing.Point(622, 143);
			this.party2Name.Name = "party2Name";
			this.party2Name.ReadOnly = true;
			this.party2Name.Size = new System.Drawing.Size(186, 20);
			this.party2Name.TabIndex = 3;
			// 
			// party1char1name
			// 
			this.party1char1name.Location = new System.Drawing.Point(12, 191);
			this.party1char1name.Name = "party1char1name";
			this.party1char1name.ReadOnly = true;
			this.party1char1name.Size = new System.Drawing.Size(100, 20);
			this.party1char1name.TabIndex = 4;
			// 
			// party1char1hits
			// 
			this.party1char1hits.Location = new System.Drawing.Point(118, 191);
			this.party1char1hits.Name = "party1char1hits";
			this.party1char1hits.ReadOnly = true;
			this.party1char1hits.Size = new System.Drawing.Size(78, 20);
			this.party1char1hits.TabIndex = 5;
			// 
			// party1char2hits
			// 
			this.party1char2hits.Location = new System.Drawing.Point(118, 229);
			this.party1char2hits.Name = "party1char2hits";
			this.party1char2hits.ReadOnly = true;
			this.party1char2hits.Size = new System.Drawing.Size(78, 20);
			this.party1char2hits.TabIndex = 7;
			// 
			// party1char2name
			// 
			this.party1char2name.Location = new System.Drawing.Point(12, 229);
			this.party1char2name.Name = "party1char2name";
			this.party1char2name.ReadOnly = true;
			this.party1char2name.Size = new System.Drawing.Size(100, 20);
			this.party1char2name.TabIndex = 6;
			// 
			// party1char3hits
			// 
			this.party1char3hits.Location = new System.Drawing.Point(118, 270);
			this.party1char3hits.Name = "party1char3hits";
			this.party1char3hits.ReadOnly = true;
			this.party1char3hits.Size = new System.Drawing.Size(78, 20);
			this.party1char3hits.TabIndex = 9;
			// 
			// party1char3name
			// 
			this.party1char3name.Location = new System.Drawing.Point(12, 270);
			this.party1char3name.Name = "party1char3name";
			this.party1char3name.ReadOnly = true;
			this.party1char3name.Size = new System.Drawing.Size(100, 20);
			this.party1char3name.TabIndex = 8;
			// 
			// party1char4hits
			// 
			this.party1char4hits.Location = new System.Drawing.Point(118, 313);
			this.party1char4hits.Name = "party1char4hits";
			this.party1char4hits.ReadOnly = true;
			this.party1char4hits.Size = new System.Drawing.Size(78, 20);
			this.party1char4hits.TabIndex = 11;
			// 
			// party1char4name
			// 
			this.party1char4name.Location = new System.Drawing.Point(12, 313);
			this.party1char4name.Name = "party1char4name";
			this.party1char4name.ReadOnly = true;
			this.party1char4name.Size = new System.Drawing.Size(100, 20);
			this.party1char4name.TabIndex = 10;
			// 
			// party1char5hits
			// 
			this.party1char5hits.Location = new System.Drawing.Point(118, 362);
			this.party1char5hits.Name = "party1char5hits";
			this.party1char5hits.ReadOnly = true;
			this.party1char5hits.Size = new System.Drawing.Size(78, 20);
			this.party1char5hits.TabIndex = 13;
			// 
			// party1char5name
			// 
			this.party1char5name.Location = new System.Drawing.Point(12, 362);
			this.party1char5name.Name = "party1char5name";
			this.party1char5name.ReadOnly = true;
			this.party1char5name.Size = new System.Drawing.Size(100, 20);
			this.party1char5name.TabIndex = 12;
			// 
			// party1char6hits
			// 
			this.party1char6hits.Location = new System.Drawing.Point(118, 406);
			this.party1char6hits.Name = "party1char6hits";
			this.party1char6hits.ReadOnly = true;
			this.party1char6hits.Size = new System.Drawing.Size(78, 20);
			this.party1char6hits.TabIndex = 15;
			// 
			// party1char6name
			// 
			this.party1char6name.Location = new System.Drawing.Point(12, 406);
			this.party1char6name.Name = "party1char6name";
			this.party1char6name.ReadOnly = true;
			this.party1char6name.Size = new System.Drawing.Size(100, 20);
			this.party1char6name.TabIndex = 14;
			// 
			// party2char1hits
			// 
			this.party2char1hits.Location = new System.Drawing.Point(728, 191);
			this.party2char1hits.Name = "party2char1hits";
			this.party2char1hits.ReadOnly = true;
			this.party2char1hits.Size = new System.Drawing.Size(78, 20);
			this.party2char1hits.TabIndex = 17;
			// 
			// party2char1name
			// 
			this.party2char1name.Location = new System.Drawing.Point(622, 191);
			this.party2char1name.Name = "party2char1name";
			this.party2char1name.ReadOnly = true;
			this.party2char1name.Size = new System.Drawing.Size(100, 20);
			this.party2char1name.TabIndex = 16;
			// 
			// party2char2hits
			// 
			this.party2char2hits.Location = new System.Drawing.Point(728, 229);
			this.party2char2hits.Name = "party2char2hits";
			this.party2char2hits.ReadOnly = true;
			this.party2char2hits.Size = new System.Drawing.Size(78, 20);
			this.party2char2hits.TabIndex = 19;
			// 
			// party2char2name
			// 
			this.party2char2name.Location = new System.Drawing.Point(622, 229);
			this.party2char2name.Name = "party2char2name";
			this.party2char2name.ReadOnly = true;
			this.party2char2name.Size = new System.Drawing.Size(100, 20);
			this.party2char2name.TabIndex = 18;
			// 
			// party2char3hits
			// 
			this.party2char3hits.Location = new System.Drawing.Point(728, 270);
			this.party2char3hits.Name = "party2char3hits";
			this.party2char3hits.ReadOnly = true;
			this.party2char3hits.Size = new System.Drawing.Size(78, 20);
			this.party2char3hits.TabIndex = 21;
			// 
			// party2char3name
			// 
			this.party2char3name.Location = new System.Drawing.Point(622, 270);
			this.party2char3name.Name = "party2char3name";
			this.party2char3name.ReadOnly = true;
			this.party2char3name.Size = new System.Drawing.Size(100, 20);
			this.party2char3name.TabIndex = 20;
			// 
			// party2char4hits
			// 
			this.party2char4hits.Location = new System.Drawing.Point(728, 313);
			this.party2char4hits.Name = "party2char4hits";
			this.party2char4hits.ReadOnly = true;
			this.party2char4hits.Size = new System.Drawing.Size(78, 20);
			this.party2char4hits.TabIndex = 23;
			// 
			// party2char4name
			// 
			this.party2char4name.Location = new System.Drawing.Point(622, 313);
			this.party2char4name.Name = "party2char4name";
			this.party2char4name.ReadOnly = true;
			this.party2char4name.Size = new System.Drawing.Size(100, 20);
			this.party2char4name.TabIndex = 22;
			// 
			// party2char5hits
			// 
			this.party2char5hits.Location = new System.Drawing.Point(728, 362);
			this.party2char5hits.Name = "party2char5hits";
			this.party2char5hits.ReadOnly = true;
			this.party2char5hits.Size = new System.Drawing.Size(78, 20);
			this.party2char5hits.TabIndex = 25;
			// 
			// party2char5name
			// 
			this.party2char5name.Location = new System.Drawing.Point(622, 362);
			this.party2char5name.Name = "party2char5name";
			this.party2char5name.ReadOnly = true;
			this.party2char5name.Size = new System.Drawing.Size(100, 20);
			this.party2char5name.TabIndex = 24;
			// 
			// party2char6hits
			// 
			this.party2char6hits.Location = new System.Drawing.Point(728, 406);
			this.party2char6hits.Name = "party2char6hits";
			this.party2char6hits.ReadOnly = true;
			this.party2char6hits.Size = new System.Drawing.Size(78, 20);
			this.party2char6hits.TabIndex = 27;
			// 
			// party2char6name
			// 
			this.party2char6name.Location = new System.Drawing.Point(622, 406);
			this.party2char6name.Name = "party2char6name";
			this.party2char6name.ReadOnly = true;
			this.party2char6name.Size = new System.Drawing.Size(100, 20);
			this.party2char6name.TabIndex = 26;
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(317, 27);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(201, 100);
			this.pictureBox1.TabIndex = 28;
			this.pictureBox1.TabStop = false;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(382, 432);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 29;
			this.button1.Text = "FIGHT!";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.Button1Click);
			// 
			// mainCombatWindow
			// 
			this.mainCombatWindow.BackColor = System.Drawing.Color.Black;
			this.mainCombatWindow.Location = new System.Drawing.Point(202, 143);
			this.mainCombatWindow.Name = "mainCombatWindow";
			this.mainCombatWindow.ReadOnly = true;
			this.mainCombatWindow.Size = new System.Drawing.Size(414, 283);
			this.mainCombatWindow.TabIndex = 30;
			this.mainCombatWindow.Text = "";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.AutoSize = true;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.ClientSize = new System.Drawing.Size(820, 472);
			this.Controls.Add(this.mainCombatWindow);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.party2char6hits);
			this.Controls.Add(this.party2char6name);
			this.Controls.Add(this.party2char5hits);
			this.Controls.Add(this.party2char5name);
			this.Controls.Add(this.party2char4hits);
			this.Controls.Add(this.party2char4name);
			this.Controls.Add(this.party2char3hits);
			this.Controls.Add(this.party2char3name);
			this.Controls.Add(this.party2char2hits);
			this.Controls.Add(this.party2char2name);
			this.Controls.Add(this.party2char1hits);
			this.Controls.Add(this.party2char1name);
			this.Controls.Add(this.party1char6hits);
			this.Controls.Add(this.party1char6name);
			this.Controls.Add(this.party1char5hits);
			this.Controls.Add(this.party1char5name);
			this.Controls.Add(this.party1char4hits);
			this.Controls.Add(this.party1char4name);
			this.Controls.Add(this.party1char3hits);
			this.Controls.Add(this.party1char3name);
			this.Controls.Add(this.party1char2hits);
			this.Controls.Add(this.party1char2name);
			this.Controls.Add(this.party1char1hits);
			this.Controls.Add(this.party1char1name);
			this.Controls.Add(this.party2Name);
			this.Controls.Add(this.party1Name);
			this.Controls.Add(this.menuStrip1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "MainForm";
			this.Text = "Theory Craft";
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.ToolStripMenuItem resetToolStripMenuItem;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.TextBox party2char6name;
		private System.Windows.Forms.TextBox party2char6hits;
		private System.Windows.Forms.TextBox party2char5name;
		private System.Windows.Forms.TextBox party2char5hits;
		private System.Windows.Forms.TextBox party2char4name;
		private System.Windows.Forms.TextBox party2char4hits;
		private System.Windows.Forms.TextBox party2char3name;
		private System.Windows.Forms.TextBox party2char3hits;
		private System.Windows.Forms.TextBox party2char2name;
		private System.Windows.Forms.TextBox party2char2hits;
		private System.Windows.Forms.TextBox party2char1name;
		private System.Windows.Forms.TextBox party2char1hits;
		private System.Windows.Forms.TextBox party1char6name;
		private System.Windows.Forms.TextBox party1char6hits;
		private System.Windows.Forms.TextBox party1char5name;
		private System.Windows.Forms.TextBox party1char5hits;
		private System.Windows.Forms.TextBox party1char4name;
		private System.Windows.Forms.TextBox party1char4hits;
		private System.Windows.Forms.TextBox party1char3name;
		private System.Windows.Forms.TextBox party1char3hits;
		private System.Windows.Forms.TextBox party1char2name;
		private System.Windows.Forms.TextBox party1char2hits;
		private System.Windows.Forms.TextBox party1char1hits;
		private System.Windows.Forms.TextBox party1char1name;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.TextBox party2Name;
		private System.Windows.Forms.TextBox party1Name;
		private System.Windows.Forms.RichTextBox mainCombatWindow;
		private System.Windows.Forms.ToolStripMenuItem loadParty2ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem loadParty1ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem gameToolStripMenuItem;
		private System.Windows.Forms.MenuStrip menuStrip1;
	}
}
