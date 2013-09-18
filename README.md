TheoryCraft
===========
A simple text based game where two player-created parties fight to the death.


Sample Team File
================
Each party must have exactly 6 characters with exactly 3 abilities, except humans get 4 abilities.  

Here's an example team:

D&D Party  
Fighter,Human,Sword,Shield,Toughness,Dodge  
Barbarian,HalfGiant,Hammer,Toughness,WaveOfFear  
Wizard,Pixie,IceBolt,MagicAffinity,MagicMissile  
Necromancer,Liche,UnholyAffinity,UnholyAffinity,UnholyBolt  
Rogue,Elf,Bow,Poison,Corruption  
Cleric,Dwarf,GreaterPrayerOfMending,GreaterHealing,Hammer


Races
=====
Human:  Four abilities instead of 3.  
Elf:  Immune to all crowd control effects (sleep, paralyze, fear) and they receive a bonus with bows.  
Dwarf:  Resistant to all damaging magic, slightly smaller mana pool.  
HalfGiant:  25% smaller mana pool, resistant to melee weapons and cold, and 20% more hit points.  
Troll:  Vulnerable to fire, bonus to hammers and punching, 10% more hit points, regenerates.  
Centaur:  High initiative bonus, and a bonus to spears.  
Lizardman:  Immune to poison, bonus to spears, and resistant to spears.  
Gnome:  10% less hit points, larger mana pool, small bonus to spells.  
Pixie:  Cold/fire spell bonus, vulnerable to cold/fire, 20% less hp, large increase to mana pool, Dodge ability for free.  
Demon:  Fire bonus, resists fire, vulnerable to cold.  
Angel:  Large holy bonus and resist, vulnerable to unholy.  
Vampire:  Vulnerable to holy damage, immune to sleep, resists unholy, drains health on melee hits.  
Skeleton:  Vulnerable to fire, holy, and hammer, resists weapons and cold, immune to crowd control and poison.  
Liche:  Vulnerable to fire and holy, resists unholy an dcold, unholy/fire/cold spell bonus, larger mana pool, immune to fear.  


Abilities:
==========

Weapons 
=======
Weapons deal damage to the front most enemy opponent.    

Sword:  Can't be dodged, highest weapon hit chance, medium damage  
Spear:  Low damage, increases weapon resistances.  
Hammer:  Highest damage melee weapon  
Kryss:  Swings twice  
Bow:  Low damage, targets lowest HP enemy.  

Direct Damage
=============
Direct damage spells target the enemy with the lowest HP.    

LesserIceBolt  
IceBolt  
GreaterIceBolt  
LesserFireBolt   
FireBolt  
GreaterFireBolt  
LesserHolyBolt  
HolyBolt  
GreaterHolyBolt  
UnholyBolt  

Crowd Control
=============
Sleep:  Targets can't take actions for 2 turns; wakes on taking damage.  
Paralyze:  Target can't take actions for 2 turns.  
WaveOfFear:  Entire enemy team loses 1 action.  

Damage over Time
================
The DoT spells are "free", meaning they don't use your action for the turn.    

Poison:  Poison DoT.  
Burn:  Fire DoT.  
Corruption:  Unholy DoT  

Area of Effect
==============
Blizzard:  Cold damage to entire enemy team.  
Inferno:  Fire damage to entire enemy team.  
WaveOfDecay:  Does 120 damage to the caster, lowers unholy resist of enemy team.  
NightOfTheDamned:  Unholy damage to all characters, including party members.  
MagicMissile:  Does a small amount of damage to 3 randomly selected enemies.  Doesn't use your action.  

Heals
=====
PrayerOfMending:  Group heal.  
GreaterPrayerOfMending:  Large group heal.  
LesserHealing:  Single target heal.  
Healing:  Single target heal.  
GreaterHealing:  Single target heal.

Passives
========
Dodge:  50% chance to dodge weapon attacks.  
MagicResistance:  Increased fire/cold resistance.  
DivineResistance:  Increased holy/unholy resistance.  
Shield:  Increase weapon resistance.    
MagicAffinity:  Increased fire/gold ability damage.  
HolyAffinity:  Increased holy ability damage.  
UnholyAffinity:  Increased unholy ability damage.  
StreetFighting:  Increased punching damage.  
Toughness:  20% more hitpoints.  
FastFeet:  Bonus initiative.    
WeaponAffinity:  Increased weapon damage.  




