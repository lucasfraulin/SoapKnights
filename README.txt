#SOAP KNIGHTS PROTOTYPE

Author: Lucas Fraulin
Student number: 250963527
Email: lfraulin@uwo.ca

## FEATURES

- Single Level
	- 2 Dirt piles to clean up
	- 1 Slime enemy to kill
	- 1 Water fall (showing dirty water)
	- 1 Lever to purify the water (changes to clean water)
	- When all dirt piles are cleaned and the slime enemy is dead then you go to the lever
	  and pull it to purify the water supply and cleanse the kingdom completing the level

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
		- Water Sword
			- slash projects 3 waterParticles to damage Enemies and also cleans dirt
			- Attack animation 
	
	- Clean dirt by attacking it with water particles from water sword
	- Kill enemies by attacking them with water sword
	- Health bar
		- If all health is lost then Game Over

- Enemies and clearable obstacles
	- Slime
		- Health, damage stats
		- Animations
			- Take damage
			- Idle player not near
			- Player near
			- Death
		- If player comes into contact with slime, player takes damage and gets knocked back

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
- Water particles
	- disappear on contact with ground
	- does damage to enemies and dirt

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
- 'Left Click' for attack
- 'E' to interact
- 'ESC' for pause menu (open/close)
- 'Left Click' while in menus for selections

## GAMEPLAY

The main goal is to purify the water supply to cleanse the kingdom.

Initially you will start in the main menu. Click play game. 
Then you will load into the level that starts immediately. 
The goal is then to kill the slime and clear the dirt by using your water sword that shoots particles.
You must do this without touching the slime that can damage you and eventually kill you. 
Once the slime is dead and the dirt is clear then you can find the lever and pull it to purify the water supply.
If you do that you win the game.



## IMPORTANT 

Currently there is a bug (that will be fixed in future iterations) where if you spam attack while moving into your enemy (taking damage)
and the enemy dies the character is frozen in place with the walking animation going. This was a recent bug that will be fixed moving forward.
If you encounter the bug you will have to alt-f4 to or close the game manually because the pause menu will not work and you cannot exit the game.