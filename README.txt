#SOAP KNIGHTS

Author: Lucas Fraulin, Jiyong Song, Andrew Domfe
Student number: 250963527, 251112894, 251104337
Email: lfraulin@uwo.ca, jsong386@uwo.ca, adomfe@uwo.ca

Group Members: 

## FEATURES

- Four Levels
	- Level 1
		- 2 Dirt piles to clean up
		- 1 Basic Slime enemy to kill
		- 1 Entrance and 1 Exit door
		- When all dirt piles are cleaned and the slime enemy is defeated then you go to the exit door
		  and interact with it to complete the level
	- Level 2
		- 3 Dirt piles to clean up
		- 2 Basic Slime enemies, 2 Patrolling Slime enemies to kill
		- 1 Entrance and 1 Exit door
		- When all dirt piles are cleaned and the slime enemy is defeated then you go to the exit door
		  and interact with it to complete the level
	- Level 3
		- 6 Dirt piles to clean up
		- 4 Basicc Slime enemies, 2 Ranged Slime enemies to kill
		- 1 Entrance and 1 Exit door
		- When all dirt piles are cleaned and the slime enemy is defeated then you go to the door
		  and interact with it to complete the level
	- Boss Level
		- 1 Boss enemy with two different phases to kill
		- 1 Water fall (showing dirty water)
		- 1 Lever to purify the water (changes to clean water)
		- In first phase, enemy throws mud balls to player
		- In second phase, enemy runs on the ground and does melee attack
		- When boss is dead then you go to the lever and pull it to purify the water supply 
		  and cleanse the kingdom to beat the game

- Playable character (Soap Knight)
	- Movement
		- Walk
		- Jump
		- Attack
	- Animations (with sound effects)
		- Jump
		- Attack
		- Take Damage
		- Walk
		- Idle
	- Weapon
		- Water Gun
			- shoots water particle to damage Enemies from a range and also cleans dirt
			- Attack - shooting animation 
		- Sword
			- slashes in front of player to damage Enemies and also cleans dirt
			- Attack - slashing animation
	
	- Clean dirt by attacking it with water gun or sword
	- Kill enemies by attacking them with water gun or sword
	- Health bar
		- If all health is lost then Game Over

- Enemies and clearable obstacles
	- Basic Slime
		- Health, damage stats
		- Animations
			- Take damage
			- Idle player not near
			- Player near
			- Death
		- If player comes into contact with slime, player takes damage and gets knocked back
	- Patrolling Slime
		- Health, damage stats
		- Animations
			- Take damage
			- Idle player not near
			- Player near
			- Death
		- If player comes into contact with slime, player takes damage and gets knocked back
		- Patrols from wall to wall
	- Ranged Slime
		- Health, damage stats
		- Animations
			- Take damage
			- Idle
			- Death
		- If player comes into contact with slime, player takes damage and gets knocked back
		- Shoots bullets to player location that does damage
	- Boss (Queen of Mud)
		- Transitions 
			- Entrance, transform Stage 1 -> stage 2, death
				- animations
				- sound effects
				- Health bar filled on enemy start and start of stage 2
				- gameobject destroyed after death animation
		- Stage 1: 
			- ranged attack: shoots mud balls 
		- Stage 2:
			- melee attack, runs to where player is
		- animations and sound effects 
		- movement towards player within range
	- Dirt
		- 3 stage animation
		- health
		- gets smaller as more water particles hit it
		- All dirt must be clear and enemies dead before player can pull the lever to purify water supply and win game

- Interactables
	- Waterfall + lever
		- once dirt and enemies removed can interact with "E" to pull lever and clean water supply
		- on lever pull waterfall turns from brown to blue representing the purified water
		- when near lever and dirt and enemies still present - message appears saying to clean and kill all enemies before
		  pulling lever
		- when near lever and dirt and enemies removed message appear saying to press "E" to pull the lever and clean
		  the water supply
	- Exit door
		- once dirt and enemies removed can interact with "E" to open the door and progress to next level
		- when near door and dirt and enemies still present - message appears saying to clean and kill all enemies before
		  opening door
		- when near door and dirt and enemies removed message appear saying to press "E" to open the door
- Water particles
	- disappear on contact with ground
	- does damage to enemies and dirt	
- Enemy bullets
	- disappear on contact with ground
	- does damage to and knockback player
- Menus
	- Main Menu
	- Pause Menu - press 'ESC' to open and close pause menu during gameplay
	- Game Over Menu when you die
	- Level Complete menu when you win the game
		- all menus give you the option to exit the game or restart it, and the game over and level complete menu
		  give an option to go back to main menu

## CONTROLS

- 'A' and 'D' for horizontal movement
- 'spacebar' for jump
- 'Left Click' for ranged attack
- 'Right Click' or 'X' for melee attack
- 'E' to interact
- 'ESC' for pause menu (open/close)
- 'Left Click' while in menus for selections

## GAMEPLAY

The main goal is to purify the water supply to cleanse the kingdom.

Initially you will start in the main menu. Click play game. 
Then you will load into level 1 that explains how to complete the level. 
The goal is then to kill the basic slime and clear the dirt by using your water gun that shoots water particle or your sword.
You must do this without touching the slime that can damage you and eventually kill you. 

You can move on to the next levels after this.
Level 2 features basic slimes, and a new enemy, patrolling slimes that you must defeat. You must also clear multiple dirt piles.
Once you have cleared everything, you can find the door and open it.
Level 3 features basic slimes and a new enemy, ranged slime, as well as more dirt piles. 
Once you clear them all, open the door to go to boss level.
Boss level features the boss with two phases. Defeat the boss both times.
Once the Queen Oof  then you can find the lever and pull it to purify the water supply and you will have cleaned the kingdom.
Then you will have completed the game.

An option to choose the levels has been added.

## Minor Bugs
When entering pause menu for the first time, 'ESC' button might need to be pressed multiple times.
