/*
 * Created by SharpDevelop.
 * User: Stephen
 * Date: 5/8/2008
 * Time: 5:37 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.IO;

namespace Theory_Craft
{
	public class Party
	{
		public string partyName;
		public Character [] character = new Character[6];
		public string partyFile;
		
		public Party()
		{
		}
		
		public void Load(string fileName, int partyNumber)
		{
			StreamReader file = new StreamReader(fileName);
			partyFile = fileName;
			
			partyName = file.ReadLine();
			
			for (int i = 0; i < 6; i++)
			{
				character[i] = new Character(file.ReadLine(), partyNumber);
			}

			file.Close();
		}
		
		public void Reset(int partyNumber)
		{
			Load(partyFile, partyNumber);
		}
	}
}
