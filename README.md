# Food Fight - Game Project (C#/Unity 3D)
A top down 3D shooter made with C# and the Unity 3D games engine

//MainMenu SS

## Technologies
C#  

## Features
* Main menu and Win/Game Over screens
* Save highscores and statistics of all previously played games
* Credits
* Traverse through a linear 3D sculpted level
* Move the player and shoot using the keyboard and mouse
* Health pick ups which heals the damage a player has taken
* Pick up stacking power ups that alter the players abilities i.e. rapid fire, more damage
* Pick up keys to open doors
* Avoid obsticles and traps
* Destructable objects with dynamic physics
* 'Room Objectives' - Doors will only open once certain criteria have been met i.e. when all enemies in the room have been defeated
* Multiple enemy types, each with their own simple AI i.e. an enemy that charges at you, a ranged enemy which keeps at a certain distance from you and shoots at you
* Final boss with basic decision making AI
* Playable on a mobile phone, with an alternate control scheme  


## Code Highlights

#### Boss AI
The following file has the script where you can see how the simple descision making AI for the Boss works, it uses a timer for the time between decisions, and asynchronous routines to carry out an action once decided.

[Level Boss AI Example](https://github.com/aesmailsparta/FoodFight_C-Sharp_Game_Project/blob/master/README.md "Boss Artificial Inteligence")  

#### Destructible Objects
Here is a link to the script which contains all of the functionality for creating a destructible object, the *DestructibleObjectHealth* script attached some logic for destroying an object, and the *FractalDestroy* script allows an object to break up into small peices after an object has been destroyed.

[Destructible Object Example](https://github.com/aesmailsparta/FoodFight_C-Sharp_Game_Project/blob/master/README.md "Destructible Object Health Script") 

[Destructible Object - Fractal Destroy Example](https://github.com/aesmailsparta/FoodFight_C-Sharp_Game_Project/blob/master/README.md "Fractal Destroy Script") 

## ScreenShots

// Statistics  
// Game Start Screen
