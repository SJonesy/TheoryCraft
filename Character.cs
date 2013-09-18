/*
 * Created by SharpDevelop.
 * User: Stephen
 * Date: 5/8/2008
 * Time: 5:57 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;

namespace Theory_Craft
{
	public class Character
	{
		//Basic Attributes
		public string name;
		public int party;
		public Race race;
		public Ability [] abilities = new Ability[4];
		public int [] abilityUses = new int[3];
		
		public int maxHitPoints = 100;
		public int hitPoints = 0;
		public int maxMana = 20;
		public int mana = 0;
		
		//Resists
		public int coldResist = 0;
		public int fireResist = 0;
		public int holyResist = 0;
		public int unholyResist = 0;
		public int spearResist = 0; 
		public int swordResist = 0;
		public int kryssResist = 0;
		public int hammerResist = 0;
		public int poisonResist = 0;
        public int healingResist = 0;
        public int punchResist = 0;
        public int bowResist = 0;
		public bool immuneToSleep = false;
		public bool immuneToFear = false;
		public bool immuneToParalyze = false;
		public bool immuneToPoison = false;
		public bool immuneToBurn = false;
		
		//Initiative
		public int initiativeMax = 100;
		
		//Ability Bonuses
		public int coldBonus = 0;
		public int fireBonus = 0;
		public int holyBonus = 0;
		public int unholyBonus = 0;
		public int hammerBonus = 0;
		public int swordBonus = 0;
		public int spearBonus = 0;
		public int kryssBonus = 0;
		public int poisonBonus = 0;
		public int punchBonus = 0;
		public int weaponBonus = 0;
		public int bowBonus = 0;
		
		//Dodge
		public bool hasDodge = false;
		
		//Game-time variables
		public int sleepTurnsRemaining = 0;
		public int paralyzedTurnsRemaining = 0;
		public int burningTurnsRemaining = 0;
		public int corruptedTurnsRemaining = 0;
		public int poisonedTurnsRemaining = 0;
		public int initiativeRoll;
		public bool isDead = false;
		public bool isFeared = false;
		public bool alreadyActed = false;
		
		public Character()
		{
			
		}
		
		public Character(string charInfo, int partyNumber)
		{
			party = partyNumber;
			
			//Split by commas, Enum.Parse the strings into abilities
			string [] splitCharInfo = charInfo.Split(new Char [] {','});
			name = Convert.ToString(splitCharInfo[0]);
			race = (Race)Enum.Parse(typeof(Race), Convert.ToString(splitCharInfo[1]), true);
			abilities[0] = (Ability)Enum.Parse(typeof(Ability), Convert.ToString(splitCharInfo[2]), true);
			abilities[1] = (Ability)Enum.Parse(typeof(Ability), Convert.ToString(splitCharInfo[3]), true);
			abilities[2] = (Ability)Enum.Parse(typeof(Ability), Convert.ToString(splitCharInfo[4]), true);
			
			//Only humans get a fourth ability
			if (race == Race.Human)
			{
				abilities[3] = (Ability)Enum.Parse(typeof(Ability), Convert.ToString(splitCharInfo[5]), true);
			}
			
			SetRacialModifiers();
			SetAbilityModifiers();
			
			hitPoints = maxHitPoints;
			mana = maxMana;
		}
		
		private void SetRacialModifiers()
		{
			switch (race)
			{
				case Race.Human:
					break;
				case Race.Elf:
					immuneToSleep = true;
					immuneToParalyze = true;
					immuneToFear = true;
					bowBonus += 10;
					break;
				case Race.Dwarf:
					coldResist += 5;
					fireResist += 5;
					holyResist += 5;
					unholyResist += 5;
					maxMana -= 2;
					break;
				case Race.HalfGiant:
					maxHitPoints += 20;
					coldResist += 5;
					hammerResist += 5;
					swordResist += 5;
					spearResist += 5;
					kryssResist += 5;
					maxMana -= 5;
					break;
				case Race.Troll:
					fireResist -= 5;
					hammerBonus += 10;
					punchBonus += 5;
					maxHitPoints += 10;
					healingResist -= 5;
					break;
				case Race.Centaur:
					initiativeMax += 40;
					spearBonus += 5;
					break;
				case Race.Lizardman:
					immuneToPoison = true;
					spearBonus += 10;
					spearResist += 5;
					break;
				case Race.Gnome:
					coldBonus += 3;
					fireBonus += 3;
					holyBonus += 3;
					unholyBonus += 3;
					maxMana += 5;
					immuneToParalyze = true;
					maxHitPoints -= 10;
					break;
				case Race.Pixie:
					coldBonus += 5;
					fireBonus += 5;
					coldResist -= 5;
					fireResist -= 5;
					maxHitPoints -= 20;
					maxMana += 10;
					hasDodge = true;
					break;
				case Race.Demon:
					fireBonus += 10;
					coldResist -= 10;
					unholyResist += 3;
					fireResist += 10;
					immuneToBurn = true;
					break;
				case Race.Angel:
					unholyResist -= 10;
					holyBonus += 10;
					holyResist += 10;
					break;
				case Race.Vampire:
					holyResist -= 5;
					fireResist -= 5;
                    healingResist += 5;
					unholyResist += 10;
					unholyBonus += 5;
					holyResist -= 10;
					immuneToSleep = true;
					break;					
				case Race.Skeleton:
					fireResist -= 5;
					holyResist -= 10;
					unholyResist += 5;
					hammerResist -= 7;
					swordResist += 7;
					spearResist += 7;
					kryssResist += 7;
					coldResist += 7;
					bowResist += 7;
					immuneToSleep = true;
					immuneToParalyze = true;
					immuneToFear = true;
					immuneToPoison = true;
					break;
				case Race.Liche:
					fireResist -= 10;
					holyResist -= 10;
					unholyResist += 10;
					coldResist += 5;
					unholyBonus += 7;
					fireBonus += 5;
					coldBonus += 5;
					maxMana += 5;
					immuneToFear = true;
					break;
			}
		}
		
		private void SetAbilityModifiers()
		{
			foreach (Ability abil in abilities)
			{
				switch (abil)
				{
					case Ability.MagicResistance:
						fireResist += 10;
						coldResist += 10;
						break;
					case Ability.DivineResistance:
						holyResist += 10;
						unholyResist += 10;
						break;
					case Ability.Shield:
						spearResist += 5;
						swordResist += 5;
						kryssResist += 5;
						hammerResist += 5;
                        punchResist += 5;
                        bowResist += 7;
						break;
					case Ability.Spear:
						spearResist += 3;
						swordResist += 3;
						kryssResist += 3;
						hammerResist += 3;
						punchResist += 3;
						break;
					case Ability.MagicAffinity:
						fireBonus += 5;
						coldBonus += 5;
						break;
					case Ability.HolyAffinity:
						holyBonus += 10;
						break;
					case Ability.UnholyAffinity:
						unholyBonus += 10;
						break;
					case Ability.Toughness:
						maxHitPoints += 20;
						break;
					case Ability.FastFeet:
						initiativeMax += 20;
						break;
					case Ability.Dodge:
						hasDodge = true;
						break;
					case Ability.StreetFighting:
						punchBonus += 10;
						break;
					case Ability.WeaponAffinity:
						weaponBonus += 10;
						break;
				}
			}
		}
	}
}
