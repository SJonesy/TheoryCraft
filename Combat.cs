/*
 * Created by SharpDevelop.
 * User: Stephen
 * Date: 5/8/2008
 * Time: 10:44 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.Threading;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;

namespace Theory_Craft
{
	public class Combat
	{
		private static ArrayList characterList = new ArrayList();
		private static RichTextBox mainCombatWindow;
		private static Button fight = new Button();
		private static Random rand = new Random();
		private static Party party1;
		private static Party party2;
		private static bool manaRegen = false;
		
		public Combat()
		{
		}
		
		public void Round(Party party1in, Party party2in, System.Windows.Forms.RichTextBox incomingMainCombatWindow, Button incomingFightButton)
		{	
			//Copy incoming data globally
			party1 = party1in;
			party2 = party2in;
			fight = incomingFightButton;
			mainCombatWindow = incomingMainCombatWindow;

			//Set up combat display
			mainCombatWindow.Clear();
			mainCombatWindow.SelectionAlignment = HorizontalAlignment.Center;

			//Create global character list
			foreach (Character character in party1.character) 
			{
				characterList.Add(character);
			}
			foreach (Character character in party2.character) 
			{
				characterList.Add(character);
			}
			
			Character currentActor = new Character(); //Lets us know whose turn it is
			int highInitiative = 0; //Used for finding the highest intiative
			
			//Roll initiatives
			RollInitiatives(characterList);
			
			//Main Round Loop
			foreach (Character next in characterList)
			{
				if (Victory()) 
				{ 
					characterList.Clear();
					return; 
				}
				
				// Added mana regen (every other round) 16 Sep 2012
				if (manaRegen && next.mana < next.maxMana && !next.isDead)
				{
					next.mana++;
					manaRegen = false; 
				}
				else
				{
					manaRegen = true;
				}
					
				//Find highest initiative
				foreach (Character character in characterList)
				{
					if (character.initiativeRoll >= highInitiative && !character.alreadyActed && !character.isDead) 
					{
						currentActor = character;
						highInitiative = character.initiativeRoll;
					}
				}
				
				//If the current character hasn't gone yet, make sure
				//they aren't paralyzed, asleep, or feared, then let them
				//go.  Also do OnActivate effects.
				if (!currentActor.alreadyActed)
				{
					OnActivate(currentActor);
					
					if (currentActor.paralyzedTurnsRemaining > 0)
					{
						Say(Color.SteelBlue, FontStyle.Italic, currentActor.name + " can not move!");
						currentActor.paralyzedTurnsRemaining--;
						
						if (currentActor.paralyzedTurnsRemaining == 0) 
						{
							Say(Color.SteelBlue, FontStyle.Italic, currentActor.name + " can move again!");
						}
					}
					else if (currentActor.sleepTurnsRemaining > 0)
					{
						Say(Color.SteelBlue, FontStyle.Italic, currentActor.name + " is sleeping!");
						currentActor.sleepTurnsRemaining--;
						
						if (currentActor.sleepTurnsRemaining == 0) {
							Say(Color.SteelBlue, FontStyle.Italic, currentActor.name + " wakes up!");
						}
					}
					else if (currentActor.isFeared)
					{
						Say(Color.DarkCyan, FontStyle.Italic, currentActor.name + " is stopped by fear.");
						currentActor.isFeared = false;
					}
					else
					{
						DecideAction(currentActor);
					}
				}
				
				//Reset
				highInitiative = 0;
				currentActor.alreadyActed = true;
				
				//Check for a winner
				if (Victory()) 
				{ 
					characterList.Clear();
					return; 
				}
			}
			
			//Reset each alreadyActed at end of round
			foreach (Character character in characterList)
			{
				character.alreadyActed = false;
			}
			
			characterList.Clear();
		}
		
		private static bool Victory()
		{
			//Check to see if someone has won
			bool someoneAlive = false;
			
			foreach (Character character in party1.character)
			{
				if (!character.isDead)
				{
					someoneAlive = true;
				}
			}
			if (!someoneAlive) {
				Say(Color.Blue, FontStyle.Bold, "*** " + party2.partyName + " WINS! ***");
				fight.Enabled = false;
				return true;
			}
			someoneAlive = false;
			foreach (Character character in party2.character)
			{
				if (!character.isDead) { someoneAlive = true; }
			}
			if (!someoneAlive) {
				Say(Color.Red, FontStyle.Bold, "*** " + party1.partyName + " WINS! ***");
				fight.Enabled = false;
				return true;
			}
			
			return false;
		}
		
		private void DecideAction(Character currentActor)
		{
			//Find how many enemies there are
			int enemyCount = 0;
			
			foreach (Character character in characterList)
			{
				if (character.party != currentActor.party && !character.isDead)
					enemyCount++;
			}
			
			//"Free" abilities such as DoTs
			foreach (Ability abil in currentActor.abilities)
			{
				if (abil == Ability.Burn && currentActor.mana >= 3) { DoAbility(Ability.Burn, currentActor); }
				if (abil == Ability.Corruption && currentActor.mana >= 3) { DoAbility(Ability.Corruption, currentActor); }
				if (abil == Ability.Poison && currentActor.mana >= 3) { DoAbility(Ability.Poison, currentActor); }
				if (abil == Ability.MagicMissile && currentActor.mana >= 1) { DoAbility(Ability.MagicMissile, currentActor); }
			}
			
			int totalHealthDisparity = 0;
						
			//See if the group can use a group heal and if so do it.
			foreach(Character character in characterList) 
			{
				if (!character.isDead && character.party == currentActor.party) 
				{
					totalHealthDisparity += character.maxHitPoints - character.hitPoints;
				}
			}
				
			if (totalHealthDisparity > 100) 
			{
				foreach (Ability abil in currentActor.abilities) 
				{
					switch (abil)
					{
							case Ability.PrayerOfMending:
								if (currentActor.mana >= 6) 
								{
									DoAbility(Ability.PrayerOfMending, currentActor);
									return;
								}
								break;
							case Ability.GreaterPrayerOfMending:
								if (currentActor.mana >= 8) 
								{
									DoAbility(Ability.GreaterPrayerOfMending, currentActor);
									return;
								}
								break;
					}
				}
			}
				
			//Single-target heals
			foreach(Character character in characterList) 
			{
				if (!character.isDead && character.maxHitPoints - character.hitPoints >= 20 && character.party == currentActor.party) 
				{
					foreach (Ability abil in currentActor.abilities) 
					{
						if (abil == Ability.GreaterHealing && currentActor.mana >= 5) { DoAbility(Ability.GreaterHealing, currentActor, character); return; }
						if (abil == Ability.Healing && currentActor.mana >= 4) { DoAbility(Ability.Healing, currentActor, character); return; }
						if (abil == Ability.LesserHealing && currentActor.mana >= 2) { DoAbility(Ability.LesserHealing, currentActor, character); return; }
					}
				}
			}
				
			foreach (Ability abil in currentActor.abilities)
			{
			//AoEs
				if (abil == Ability.Blizzard && currentActor.mana >= 10) { DoAbility(Ability.Blizzard, currentActor); return; }
				if (abil == Ability.Inferno && currentActor.mana >= 10) { DoAbility(Ability.Inferno, currentActor); return; }
				if (abil == Ability.NightOfTheDamned && currentActor.mana >= 10) { DoAbility(Ability.NightOfTheDamned, currentActor); return; }
				if (abil == Ability.WaveOfDecay && currentActor.mana >= 11) { DoAbility(Ability.WaveOfDecay, currentActor); return; }
				if (abil == Ability.WaveOfFear && currentActor.mana >= 12) { DoAbility(Ability.WaveOfFear, currentActor); return; }

			//CC
				//Sleep
				if (abil == Ability.Sleep && currentActor.mana >= 4) 
				{ 
					foreach(Character character in characterList) 
					{
						if (character.party != currentActor.party && character.sleepTurnsRemaining == 0 && !(character.paralyzedTurnsRemaining > 0) && !character.isDead && !character.immuneToSleep && !character.isFeared) 
						{
							DoAbility(Ability.Sleep, currentActor); 
							return;
						}
					}
				}
				
				//Paralyze
				if (abil == Ability.Paralyze && currentActor.mana >= 13) {
					foreach(Character character in characterList) 
					{
						if (character.party != currentActor.party && character.paralyzedTurnsRemaining == 0 && !(character.sleepTurnsRemaining > 0) && !character.isDead && !character.immuneToParalyze && !character.isFeared) 
						{
							DoAbility(Ability.Paralyze, currentActor); 
							return;
						}
					}
				}
			}
			
			//Do Direct Damage if nothing else is available
			Character target = new Character();
			target.hitPoints = 99999;
			
			//Find Weakest Enemy - Characters no longer attack sleeping characters
			foreach(Character character in characterList) 
			{
				if (character.hitPoints < target.hitPoints && !character.isDead && character.party != currentActor.party && (character.sleepTurnsRemaining == 0 || enemyCount == 1))
				{
					target = character;
				}
			}

			//Do DD - Spells, then weapons
			foreach (Ability abil in currentActor.abilities)
			{
				//DD Spells
				if (abil == Ability.GreaterFireBolt && currentActor.mana >= 4) { DoAbility(Ability.GreaterFireBolt, currentActor, target); return; }
				if (abil == Ability.GreaterIceBolt && currentActor.mana >= 4) { DoAbility(Ability.GreaterIceBolt, currentActor, target); return;}
				if (abil == Ability.GreaterHolyBolt && currentActor.mana >= 6) { DoAbility(Ability.GreaterHolyBolt, currentActor, target); return;}
				if (abil == Ability.FireBolt && currentActor.mana >= 2) { DoAbility(Ability.FireBolt, currentActor, target); return;}
				if (abil == Ability.IceBolt && currentActor.mana >= 2) { DoAbility(Ability.IceBolt, currentActor, target); return;}
				if (abil == Ability.UnholyBolt && currentActor.mana >= 3) { DoAbility(Ability.UnholyBolt, currentActor, target); return;}
				if (abil == Ability.HolyBolt && currentActor.mana >= 3) { DoAbility(Ability.HolyBolt, currentActor, target); return;}
				if (abil == Ability.LesserFireBolt && currentActor.mana >= 1) { DoAbility(Ability.LesserFireBolt, currentActor, target); return;}
				if (abil == Ability.LesserIceBolt && currentActor.mana >= 1) { DoAbility(Ability.LesserIceBolt, currentActor, target); return;}
				if (abil == Ability.LesserHolyBolt && currentActor.mana >= 1) { DoAbility(Ability.LesserHolyBolt, currentActor, target); return;}
				if (abil == Ability.Bow) { DoAbility(Ability.Bow, currentActor, target); return; } //Bow is treated more like a spell since it hits the weakest target
			}

			//Melee abilities now hit the FRONT character instead of the weakest - 16 Sep 2013
			foreach(Character character in characterList) 
			{
				if (!character.isDead && character.party != currentActor.party && (character.sleepTurnsRemaining == 0 || enemyCount == 1)) 
				{
					target = character;
					break;
				}
			}
			
            foreach (Ability abil in currentActor.abilities)
            {
                //Weapons
                if (abil == Ability.Kryss) { DoAbility(Ability.Kryss, currentActor, target); return; }
                if (abil == Ability.Spear) { DoAbility(Ability.Spear, currentActor, target); return; }
                if (abil == Ability.Hammer) { DoAbility(Ability.Hammer, currentActor, target); return; }
                if (abil == Ability.Sword) { DoAbility(Ability.Sword, currentActor, target); return; }
            }
		
			//If nothing's left,
			DoAbility(Ability.Punch, currentActor, target);
		}
		
		private void DoAbility(Ability ability, Character currentActor)
		{
			switch(ability)
			{
				//Prayer of Mending
				case Ability.PrayerOfMending:
					currentActor.mana -= 6;
					Say(Color.White, FontStyle.Regular, currentActor.name + " gestures towards his whole party.");
					foreach(Character character in characterList) 
					{
						if(character.party == currentActor.party && !character.isDead)
						{
							Say(Color.Blue, FontStyle.Italic, character.name + " looks slightly more healthy.");
							Heal(currentActor, character, Ability.Healing);
						}
					}
				break;
					
				//Greater Prayer of Mending
				case Ability.GreaterPrayerOfMending:
					currentActor.mana -= 8;
					Say(Color.White, FontStyle.Regular, currentActor.name + " gestures towards his whole party.");
					foreach(Character character in characterList) 
					{
						if (character.party == currentActor.party && !character.isDead)
						{
							Say(Color.Blue, FontStyle.Italic, character.name + " looks much more healthy.");
							Heal(currentActor, character, Ability.GreaterHealing);
						}
					}
				break;
				
				//Wave of Fear
				case Ability.WaveOfFear:
					currentActor.mana -= 12;
					Say(Color.White, FontStyle.Regular, currentActor.name + " rears back his head and screams like a banshee.");
					foreach(Character character in characterList) 
					{
						if (!character.immuneToFear && character.party != currentActor.party && !character.isDead) 
						{
							character.isFeared = true;
							Say(Color.DarkCyan, FontStyle.Italic, character.name + " freezes in horror.");
						}
					}
				break;
					
				//Sleep
				case Ability.Sleep:
					foreach(Character character in characterList) 
					{
						if (character.party != currentActor.party && character.sleepTurnsRemaining == 0 && !(character.paralyzedTurnsRemaining > 0) && !character.isDead && !character.immuneToSleep && !character.isFeared) 
						{
							currentActor.mana -= 6;
							character.sleepTurnsRemaining = 2;
							Say(Color.White, FontStyle.Regular, currentActor.name + " gestures wildly.");
							Say(Color.SteelBlue, FontStyle.Italic, character.name + " falls asleep!");
							break;
						}
					}
				break;
				
				//Paralyze
				case Ability.Paralyze:
					foreach(Character character in characterList) 
					{
						if (character.party != currentActor.party && character.paralyzedTurnsRemaining == 0 && !(character.sleepTurnsRemaining > 0) && !character.isDead && !character.immuneToParalyze && !character.isFeared) 
						{
							currentActor.mana -= 13;
							character.paralyzedTurnsRemaining = 4;
							Say(Color.White, FontStyle.Regular, currentActor.name + " gestures wildly.");
							Say(Color.SteelBlue, FontStyle.Italic, character.name + " stops moving!");
							break;
						}
					}
				break;
				
				//Night of the Damned
				case Ability.NightOfTheDamned:
					currentActor.mana -= 10;
					Say(Color.White, FontStyle.Regular, currentActor.name + " tilts his head back and laughs as spirits materialize everywhere!");
					foreach (Character character in characterList)
					{
						if (!character.isDead)
						{
							Say(Color.DarkCyan, FontStyle.Italic, character.name + " is struck by a spirit.");
							Damage(currentActor, character, 4, 11, DamageType.Unholy);
						}
					}
				break;
				
				//Made AoEs 1-sided - 3 Nov 2012
				
				//Inferno
				case Ability.Inferno:
					currentActor.mana -= 10;
					Say(Color.White, FontStyle.Regular, currentActor.name + " points at the ground and calls forth a flaming inferno!");
					foreach (Character character in characterList)
					{
						if ( !character.isDead && (character.party != currentActor.party) )
						{
							Say(Color.Red, FontStyle.Italic, character.name + " is struck by the inferno.");
							Damage(currentActor, character, 1, 10, DamageType.Fire);
						}
					}
				break;
				
				//Blizzard
				case Ability.Blizzard:
					currentActor.mana -= 10;
					Say(Color.White, FontStyle.Regular, currentActor.name + " looks towards the sky and calls forth a mighty blizzard!");
					foreach (Character character in characterList)
					{
						if ( !character.isDead && (character.party != currentActor.party) )
						{
							Say(Color.Aqua, FontStyle.Italic, character.name + " is struck by the blizzard.");
							Damage(currentActor, character, 3, 7, DamageType.Cold);
						}
					}
				break;
				
				//Wave of Decay
				case Ability.WaveOfDecay:
					Say(Color.White, FontStyle.Regular, currentActor.name + " stabs himself in the heart.");
						
					foreach(Character character in characterList) 
					{
						if ( !character.isDead && (character.party != currentActor.party) )
						{
							character.unholyResist -= 4;
							Say(Color.DarkCyan, FontStyle.Italic, character.name + " fears the night.");
							Damage(currentActor, character, 1, 2, DamageType.Neutral);
						}
					}
						
					Damage(currentActor, currentActor, 120, 120, DamageType.Unholy);
				break;	
				
				//Poison	
				case Ability.Poison:
					foreach(Character character in characterList) 
					{
						if (character.party != currentActor.party && character.poisonedTurnsRemaining == 0 && !character.isDead && !character.immuneToPoison) 
						{
							currentActor.mana -= 3;
							character.poisonedTurnsRemaining = 10;
							Say(Color.White, FontStyle.Regular, currentActor.name + " gestures wildly.");
							Say(Color.Green, FontStyle.Italic, character.name + " looks sick.");
							break;
						}
					}
				break;
				
				//Burn
				case Ability.Burn:
					foreach(Character character in characterList) 
					{
						if (character.party != currentActor.party && character.burningTurnsRemaining == 0 && !character.isDead  && !character.immuneToBurn) 
						{
							currentActor.mana -= 3;
							character.burningTurnsRemaining = 5;
							Say(Color.White, FontStyle.Regular, currentActor.name + " gestures wildly.");
							Say(Color.Red, FontStyle.Italic, character.name + " bursts into flames!");
							break;
						}
					}
				break;
				
				//Corruption
				case Ability.Corruption:
					foreach(Character character in characterList) 
					{
						if (character.party != currentActor.party && character.corruptedTurnsRemaining == 0 && !character.isDead) 
						{
							currentActor.mana -= 3;
							character.corruptedTurnsRemaining = 4;
							Say(Color.White, FontStyle.Regular, currentActor.name + " gestures wildly.");
							Say(Color.DarkCyan, FontStyle.Italic, character.name + "'s skin is covered in boils!");
							break;
						}
					}
				break;
				
				//Magic Missile - 18 Sep 2013
				case Ability.MagicMissile:
					currentActor.mana -= 1;
					ArrayList targets = new ArrayList();
					Say(Color.White, FontStyle.Regular, currentActor.name + " gestures wildly.");
					foreach(Character character in characterList) 
					{
						if (character.party != currentActor.party && !character.isDead) 
						{
							targets.Add(character);
						}
					}
					for (int i = 0; i < 3; i++)
					{
						Character target = targets[Rand(0, targets.Count)] as Character;
						Say(Color.DarkMagenta, FontStyle.Italic, target.name + " is struck by a magic missile!");
						Damage(currentActor, target, 2, 4, DamageType.Neutral);
					}
				break;
			}
		}
		
		private void DoAbility(Ability ability, Character currentActor, Character target)
		{
			switch(ability)
			{
				//Weapons
				//=======
				
				//Kryss
				case Ability.Kryss:
					if (target.hasDodge && CoinFlip())
					{
						Say(Color.Silver, FontStyle.Italic, currentActor.name + " swings his kryss at " + target.name + " but they dodge. ");
						break;
					}
					if (CoinFlip()) 
					{
						Say(Color.White, FontStyle.Italic, currentActor.name + " swings his kryss at " + target.name + " and hits! ");
					    Damage(currentActor, target, 8, 12, DamageType.Kryss);
					}
					else
					{
						Say(Color.Silver, FontStyle.Italic, currentActor.name + " swings his kryss at " + target.name + " but misses. ");
					}
					
					if (CoinFlip()) 
					{
						Say(Color.White, FontStyle.Italic, currentActor.name + " swings his kryss at " + target.name + " and hits! ");
					    Damage(currentActor, target, 8, 12, DamageType.Kryss);
					}
					else
					{
						Say(Color.Silver, FontStyle.Italic, currentActor.name + " swings his kryss at " + target.name + " but misses. ");
					}
				break;
				
				//Sword - Can't be dodged
				case Ability.Sword:
					if (CoinFlip() || CoinFlip()) 
					{
						Say(Color.White, FontStyle.Italic, currentActor.name + " swings his sword at " + target.name + " and hits! ");
					    Damage(currentActor, target, 12, 20, DamageType.Sword);
					}
					else
					{
						Say(Color.Silver, FontStyle.Italic, currentActor.name + " swings his sword at " + target.name + " but misses. ");
					}
				break;
					
				//Hammer
				case Ability.Hammer:
					if (target.hasDodge && CoinFlip())
					{
						Say(Color.Silver, FontStyle.Italic, currentActor.name + " swings his hammer at " + target.name + " but they dodge. ");
						break;
					}
					if (CoinFlip()) 
					{
						Say(Color.White, FontStyle.Italic, currentActor.name + " swings his hammer at " + target.name + " and hits! ");
					    Damage(currentActor, target, 25, 30, DamageType.Hammer);
					}
					else
					{
						Say(Color.Silver, FontStyle.Italic, currentActor.name + " swings his hammer at " + target.name + " but misses. ");
					}
				break;
				
				// Bow - Shoots the back line
				case Ability.Bow:
					if (target.hasDodge && CoinFlip())
					{
						Say(Color.Silver, FontStyle.Italic, currentActor.name + " shoots an arrow at " + target.name + " but they dodge. ");
						break;
					}
					if (CoinFlip()) 
					{
						Say(Color.White, FontStyle.Italic, currentActor.name + " shoots an arrow at " + target.name + " and hits! ");
					    Damage(currentActor, target, 17, 20, DamageType.Bow);
					}
					else
					{
						Say(Color.Silver, FontStyle.Italic, currentActor.name + " shoots an arrow at " + target.name + " but misses. ");
					}
				break;
				
				//Spear
				case Ability.Spear:
					if (target.hasDodge && CoinFlip())
					{
						Say(Color.Silver, FontStyle.Italic, currentActor.name + " swings his spear at " + target.name + " but they dodge. ");
						break;
					}
					if (CoinFlip()) 
					{
						Say(Color.White, FontStyle.Italic, currentActor.name + " swings his spear at " + target.name + " and hits! ");
					    Damage(currentActor, target, 17, 20, DamageType.Spear);
					}
					else
					{
						Say(Color.Silver, FontStyle.Italic, currentActor.name + " swings his spear at " + target.name + " but misses. ");
					}
				break;
					
				//Single-target Heals
				//===================
				
				//Lesser Healing
				case Ability.LesserHealing:
					Say(Color.White, FontStyle.Regular, currentActor.name + " gestures towards " + target.name);
					Say(Color.Blue, FontStyle.Italic, target.name + " looks a little healthier.");
					Heal(currentActor, target, Ability.LesserHealing);
					currentActor.mana -= 2;
				break;
				
				//Healing
				case Ability.Healing:
					Say(Color.White, FontStyle.Regular, currentActor.name + " gestures towards " + target.name);
					Say(Color.Blue, FontStyle.Italic, target.name + " looks healthier.");
					Heal(currentActor, target, Ability.Healing);
					currentActor.mana -= 4;
				break;
				
				//Greater Healing
				case Ability.GreaterHealing:
					Say(Color.White, FontStyle.Regular, currentActor.name + " gestures towards " + target.name);
					Say(Color.Blue, FontStyle.Italic, target.name + " looks much healthier!");
					Heal(currentActor, target, Ability.GreaterHealing);
					currentActor.mana -= 5;
				break;
				
				//Direct Damage Spells
				//====================
				
				//Greater Fire Bolt
				case Ability.GreaterFireBolt:
					Say(Color.White, FontStyle.Regular, currentActor.name + " points at " + target.name + ".");
					Say(Color.Red, FontStyle.Italic, "A large bolt of fire shoots from " + currentActor.name + "'s finger and strikes " + target.name + " directly in the chest.");
					Damage(currentActor, target, 18, 21, DamageType.Fire);
					currentActor.mana -= 4;
				break;
				
				//Fire Bolt
				case Ability.FireBolt:
					Say(Color.White, FontStyle.Regular, currentActor.name + " points at " + target.name + ".");
					Say(Color.Red, FontStyle.Italic, "A small bolt of fire shoots from " + currentActor.name + "'s finger and strikes " + target.name + " directly in the chest.");
					Damage(currentActor, target, 12, 15, DamageType.Fire);
					currentActor.mana -= 2;
				break;
				
				//Lesser Fire Bolt
				case Ability.LesserFireBolt:
					Say(Color.White, FontStyle.Regular, currentActor.name + " points at " + target.name + ".");
					Say(Color.Red, FontStyle.Italic, "A tiny bolt of fire shoots from " + currentActor.name + "'s finger and strikes " + target.name + " directly in the chest.");
					Damage(currentActor, target, 8, 11, DamageType.Fire);
					currentActor.mana -= 1;
				break;
				
				//Greater Ice Bolt
				case Ability.GreaterIceBolt:
					Say(Color.White, FontStyle.Regular, currentActor.name + " points at " + target.name + ".");
					Say(Color.Aqua, FontStyle.Italic, "A large bolt of ice shoots from " + currentActor.name + "'s finger and strikes " + target.name + " directly in the chest.");
					Damage(currentActor, target, 18, 21, DamageType.Cold);
					currentActor.mana -= 4;
				break;
				
				//Ice Bolt
				case Ability.IceBolt:
					Say(Color.White, FontStyle.Regular, currentActor.name + " points at " + target.name + ".");
					Say(Color.Aqua, FontStyle.Italic, "A small bolt of ice shoots from " + currentActor.name + "'s finger and strikes " + target.name + " directly in the chest.");
					Damage(currentActor, target, 12, 15, DamageType.Cold);
					currentActor.mana -= 2;
				break;
				
				//Lesser Ice Bolt
				case Ability.LesserIceBolt:
					Say(Color.White, FontStyle.Regular, currentActor.name + " points at " + target.name + ".");
					Say(Color.Aqua, FontStyle.Italic, "A tiny bolt of ice shoots from " + currentActor.name + "'s finger and strikes " + target.name + " directly in the chest.");
					Damage(currentActor, target, 8, 11, DamageType.Cold);
					currentActor.mana -= 1;
				break;
				
				//Greater Holy Bolt
				case Ability.GreaterHolyBolt:
					Say(Color.White, FontStyle.Regular, currentActor.name + " points at " + target.name + ".");
					Say(Color.Gold, FontStyle.Italic, "A huge blast of heavenly energy engulfs " + target.name + ".");
					Damage(currentActor, target, 18, 22, DamageType.Holy);
					currentActor.mana -= 6;
				break;
					
				//Holy Bolt
				case Ability.HolyBolt:
					Say(Color.White, FontStyle.Regular, currentActor.name + " points at " + target.name + ".");
					Say(Color.Gold, FontStyle.Italic, target.name + " is struck by divine light!");
					Damage(currentActor, target, 13, 17, DamageType.Holy);
					currentActor.mana -= 3;
				break;
				
				//Lesser Holy Bolt
				case Ability.LesserHolyBolt:
					Say(Color.White, FontStyle.Regular, currentActor.name + " points at " + target.name + ".");
					Say(Color.Gold, FontStyle.Italic, target.name + " looks humbled for a moment.");
					Damage(currentActor, target, 9, 13, DamageType.Holy);
					currentActor.mana -= 2;
				break;
				
				//Unholy Bolt
				case Ability.UnholyBolt:
					Say(Color.White, FontStyle.Regular, currentActor.name + " points at " + target.name + ".");
					Say(Color.DarkCyan, FontStyle.Italic, "Black ribbons lash out from " + currentActor.name + "'s finger, striking " + target.name + ".");
					Damage(currentActor, target, 13, 17, DamageType.Unholy);
					currentActor.mana -= 3;
				break;
				
				//Punches
				//=======
				
				//Punch
				case Ability.Punch:
					if (target.hasDodge && CoinFlip())
					{
						Say(Color.Silver, FontStyle.Italic, currentActor.name + " swings his fist at " + target.name + " but he dodges. ");
						break;
					}
					Say(Color.White, FontStyle.Italic, currentActor.name + " punches " + target.name + " right in the face.");
					Damage(currentActor, target, 1, 3, DamageType.Punch);
				break;
			}
		}
		
		private static bool CoinFlip()
		{					
			if (Rand(100) <= 49) 
			{
				return false;
			}
			else 
			{
				return true;
			}
		}
		
		public static void Heal (Character healer, Character target, Ability abil)
		{
			Random random = new Random();
			int healing = 0;
			
			switch (abil)
			{
				case Ability.LesserHealing:
					healing = random.Next(8, 14);
				break;
				case Ability.Healing:
					healing = random.Next(14, 20);
				break;
				case Ability.GreaterHealing:
					healing = random.Next(20, 28);
				break;
			}
			
			healing += healer.holyBonus;
            healing -= target.healingResist;

			target.hitPoints += healing;
			if (target.hitPoints > target.maxHitPoints) { target.hitPoints = target.maxHitPoints; }
		}
		
		public static void Damage (Character currentActor, Character target, int min, int max, DamageType type)
		{
			Random random = new Random();
			int damage = random.Next(min, max);
			
			switch(type)
			{
				case DamageType.Punch:
                    damage -= target.punchResist;
                    damage += currentActor.punchBonus;
					break;
				case DamageType.Poison:
					damage -= target.poisonResist;
					damage += currentActor.poisonBonus;
					break;
				case DamageType.Hammer:
					damage -= target.hammerResist;
					damage += currentActor.hammerBonus;
					damage += currentActor.weaponBonus;
					break;
				case DamageType.Sword:
					damage -= target.swordResist;
					damage += currentActor.swordBonus;
					damage += currentActor.weaponBonus;
					break;
				case DamageType.Spear:
					damage -= target.spearResist;
					damage += currentActor.spearBonus;
					damage += currentActor.weaponBonus;
					break;
				case DamageType.Kryss:
					damage -= target.kryssResist;
					damage += currentActor.kryssBonus;
					damage += currentActor.weaponBonus;
					break;
				case DamageType.Fire:
					damage -= target.fireResist;
					damage += currentActor.fireBonus;
					break;
				case DamageType.Unholy:
					damage -= target.unholyResist;
					damage += currentActor.unholyBonus;
					break;
				case DamageType.Holy:
					damage -= target.holyResist;
					damage += currentActor.holyBonus;
					break;
				case DamageType.Cold:
					damage -= target.coldResist;
					damage += currentActor.coldBonus;
					break;
				case DamageType.Bow:
					damage -= target.bowResist;
					damage += currentActor.weaponBonus;
					damage += currentActor.bowBonus;
					break;
				case DamageType.Neutral:
					break;
			}
			
			if (currentActor.race == Race.Vampire && (type == DamageType.Hammer || type == DamageType.Kryss || type == DamageType.Punch || type == DamageType.Spear || type == DamageType.Sword))
			{
				int healthDrain;
				healthDrain = Rand(1, 4);
				
				damage += healthDrain;
				currentActor.hitPoints += healthDrain;
			}
			
			if (damage > 0) 
			{ 
				target.hitPoints -= damage; 
				target.sleepTurnsRemaining = 0;
			}
			
			if (target.hitPoints <= 0 && target.isDead == false)
			{
				Say(Color.Brown, FontStyle.Regular, target.name + " has died.");
				target.isDead = true;
			}
		}
		
		public static void Damage (Character target, int min, int max, DamageType type)
		{
			int damage = Rand(min, max);
			
			switch(type)
			{
				case DamageType.Punch:
                    damage -= target.punchResist;
					break;
				case DamageType.Fire:
					damage -= target.fireResist;
					break;
				case DamageType.Unholy:
					damage -= target.unholyResist;
					break;
				case DamageType.Holy:
					damage -= target.holyResist;
					break;
				case DamageType.Cold:
					damage -= target.coldResist;
					break;
			}
			
			if (damage > 0) 
			{
				target.hitPoints -= damage;
				target.sleepTurnsRemaining = 0;
			}
			
			if (target.hitPoints <= 0 && target.isDead == false)
			{
				Say(Color.Brown, FontStyle.Regular, target.name + " has died.");
				target.isDead = true;
			}
		}
		
		private static void RollInitiatives(ArrayList characterList)
		{
			foreach (Character character in characterList) 
			{
				character.initiativeRoll = Rand(character.initiativeMax);
			}
		}
		
		private static void Say(Color color, FontStyle style, string words)
		{
			mainCombatWindow.SelectionFont = new Font("Tahoma", 8, style);
			mainCombatWindow.SelectionColor = color;
			mainCombatWindow.SelectedText = words + '\n';
		}
		
		private static void OnActivate(Character currentActor)
		{
			if (currentActor.race == Race.Troll) 
			{
				if (currentActor.maxHitPoints - currentActor.hitPoints > 5) 
				{
					currentActor.hitPoints += 5;
				}
			}

			/* Took out auto-regen 3 Nov 2012
			if (currentActor.hitPoints < currentActor.maxHitPoints) 
			{
				currentActor.hitPoints++;
			}
			*/
			
			if (currentActor.burningTurnsRemaining > 0) 
			{
				Say(Color.Red, FontStyle.Italic, currentActor.name + " burns.");
				Damage(currentActor, 1, 10, DamageType.Fire);
				currentActor.burningTurnsRemaining--;
			}
			
			if (currentActor.corruptedTurnsRemaining > 0) 
			{
				Say(Color.DarkCyan, FontStyle.Italic, currentActor.name + "'s boils fester, oozing puss.");
				Damage(currentActor, 4, 7, DamageType.Unholy);
				currentActor.corruptedTurnsRemaining--;
			}
			
			if (currentActor.poisonedTurnsRemaining > 0) 
			{
				Say(Color.Green, FontStyle.Italic, currentActor.name + " vomits.");
				Damage(currentActor, 3, 5, DamageType.Poison);
				currentActor.poisonedTurnsRemaining--;
			}

		}
		
		private static int Rand(int min, int max)
		{
			return rand.Next(min, max);
		}

		private static int Rand(int max)
		{
			return rand.Next(max);
		}
		
		private static void Debug(string words)
		{
			mainCombatWindow.SelectionFont = new Font("Tahoma", 8, FontStyle.Regular);
			mainCombatWindow.SelectionColor = Color.Gray;
			mainCombatWindow.SelectedText = words + '\n';
		}
		
		private static void Debug(int number)
		{
			mainCombatWindow.SelectionFont = new Font("Tahoma", 8, FontStyle.Regular);
			mainCombatWindow.SelectionColor = Color.Gray;
			mainCombatWindow.SelectedText = number.ToString() + '\n';
		}
	}
}
