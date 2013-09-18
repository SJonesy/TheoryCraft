/*
 * Created by SharpDevelop.
 * User: Stephen
 * Date: 5/8/2008
 * Time: 5:28 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;

namespace Theory_Craft
{
	public partial class MainForm : Form
	{
		public Party party1;
		public Party party2;
		
		public MainForm()
		{
			party1 = new Party();
			party2 = new Party();

			InitializeComponent();
		}
		
		void ResetToolStripMenuItemClick(object sender, EventArgs e)
		{
			party1.Reset(1);
			party2.Reset(2);
			button1.Enabled = true;
			Display();
			mainCombatWindow.Clear();
		}

		void LoadParty1ToolStripMenuItemClick(object sender, EventArgs e)
		{
			// create an open file dialog
		    OpenFileDialog dlgOpen = new OpenFileDialog();
		    
		    // set properties for the dialog
		    dlgOpen.Title = "Select Party 1";
		    dlgOpen.ShowReadOnly = true;
		    dlgOpen.Multiselect = false;
		
		    // display the dialog and return results
		    if (dlgOpen.ShowDialog() == DialogResult.OK)
		    {
		    	party1.Load(dlgOpen.FileName, 1);
		    }
		    
		    button1.Enabled = true;
		    Display();
		}
		
		void LoadParty2ToolStripMenuItemClick(object sender, EventArgs e)
		{
			// create an open file dialog
		    OpenFileDialog dlgOpen = new OpenFileDialog();
		    
		    // set properties for the dialog
		    dlgOpen.Title = "Select Party 2";
		    dlgOpen.ShowReadOnly = true;
		    dlgOpen.Multiselect = false;
		
		    // display the dialog and return results
		    if (dlgOpen.ShowDialog() == DialogResult.OK)
		    {
		    	party2.Load(dlgOpen.FileName, 2);
		    }
		    
		    button1.Enabled = true;
		    Display();
		}
		
		void ExitToolStripMenuItemClick(object sender, EventArgs e)
		{
			Application.Exit();
		}
		
		void Display()
		{
			if (!String.IsNullOrEmpty(party1.partyName))
			{
				party1Name.Text = party1.partyName;
				
				party1char1name.Text = party1.character[0].name;
				party1char2name.Text = party1.character[1].name;
				party1char3name.Text = party1.character[2].name;
				party1char4name.Text = party1.character[3].name;
				party1char5name.Text = party1.character[4].name;
				party1char6name.Text = party1.character[5].name;
				
				party1char1hits.Text = "H:" + party1.character[0].hitPoints.ToString() + " M:" + party1.character[0].mana.ToString();
				party1char2hits.Text = "H:" + party1.character[1].hitPoints.ToString() + " M:" + party1.character[1].mana.ToString();
				party1char3hits.Text = "H:" + party1.character[2].hitPoints.ToString() + " M:" + party1.character[2].mana.ToString();
				party1char4hits.Text = "H:" + party1.character[3].hitPoints.ToString() + " M:" + party1.character[3].mana.ToString();
				party1char5hits.Text = "H:" + party1.character[4].hitPoints.ToString() + " M:" + party1.character[4].mana.ToString();
				party1char6hits.Text = "H:" + party1.character[5].hitPoints.ToString() + " M:" + party1.character[5].mana.ToString();
			}
			
			if (!String.IsNullOrEmpty(party2.partyName))
			{
				party2Name.Text = party2.partyName;
				
				party2char1name.Text = party2.character[0].name;
				party2char2name.Text = party2.character[1].name;
				party2char3name.Text = party2.character[2].name;
				party2char4name.Text = party2.character[3].name;
				party2char5name.Text = party2.character[4].name;
				party2char6name.Text = party2.character[5].name;
				
				party2char1hits.Text = "H:" + party2.character[0].hitPoints.ToString() + " M:" + party2.character[0].mana.ToString();
				party2char2hits.Text = "H:" + party2.character[1].hitPoints.ToString() + " M:" + party2.character[1].mana.ToString();
				party2char3hits.Text = "H:" + party2.character[2].hitPoints.ToString() + " M:" + party2.character[2].mana.ToString();
				party2char4hits.Text = "H:" + party2.character[3].hitPoints.ToString() + " M:" + party2.character[3].mana.ToString();
				party2char5hits.Text = "H:" + party2.character[4].hitPoints.ToString() + " M:" + party2.character[4].mana.ToString();
				party2char6hits.Text = "H:" + party2.character[5].hitPoints.ToString() + " M:" + party2.character[5].mana.ToString();
			}
		}
		
		
		void Button1Click(object sender, EventArgs e)
		{
			Combat combat = new Combat();
			combat.Round(party1, party2, mainCombatWindow, button1);
			Display();
		}
	}
}
