# barn-owl-extravaganza
Action-RPG/Platformer game en route

I don't have a name for the game yet, so random repository name it is!

Note: This game has naught to do with barn owls, as much as it should.

This game will be an Action-RPG/Platformer, inspired by games like Castlevania: Aria of Sorrow for the GBA, where the player will traverse a variety of dungeons, fight bosses, get loot, and obtain upgrades allowing them to progress further. I *may* implement multiplayer co-op in the future, but I would like to focus on a solid singleplayer experience first.

Development is temporarily paused while I make some design choices. Though some basic mechanics have been implemented, I am taking some time to decide on different dungeons, enemies, spells, and even where the story will go, so I will have an idea of where to go next in terms of programming.
# What's Done So Far
* Player Controls: Movement, jumping, attacking in different directions, different aerial attacks, and so on.
* Major Progression Upgrades: Double jumping, wall jumping, sliding, swimming, and breaking specific blocks are now implemented. This includes most of the progression abilities I have planned so far.
* Basic Stat Calculations: Damage dealt and received is determined by the attacker's offensive stats and the defender's defensive stats. Accuracy is also calculated, allowing attacks to miss against particularly agile enemies.
* Dialogue: I can now type stuff to the screen! Not that there's much dialogue, or any NPCs to speak it, but it's functional nonetheless.

* Basic Enemy Behaviors: I have separated movement and attack behaviors, allowing me to mix and match behaviors to easily create new enemy types. Of course, some enemies, like bosses, will have to be more complex and will require custom behavior scripts. More behaviors will be implemented, but the current behaviors are plenty for me to test some different enemies.

  * Movement Behaviors include running towards the player on the ground, wandering left and right on the ground, moving by jumping into the air, chasing the player in the air, following the player in the air while keeping a short distance, and following a predetermined path in the air.
  * Attack Behaviors include slashing at the player when nearby, and periodically shooting single or multiple projectiles at the player.

* Overworld: Moving from dungeon to dungeon on a world map, allowing the player to select which dungeon to enter.


# To Be Done
* More enemy behaviors: No doubt the game will need more than the simple building blocks I have implemented so far.
* Bosses: Every dungeon will have at least one boss, which will drop powerful equipment or some major upgrade allowing the player to further progress. Of course, a majority of these bosses will need unique custom behaviors.
* Items, Equipment, Classes, and Skills: Though I have implemented a few weapon types and classes for testing purposes, I expect to add many more throughout the game.
* Skill Trees: Every class will have three skill trees, each focusing on some different aspect of the class, allowing the player to experiment with different playstyles.
* Design Multi-path Dungeons: This will be a significant portion of the game. Dungeons will have one or more paths, with each completed path unlocking a new path on the overworld. This will allow players to complete some dungeons in different orders and in some cases tackle harder dungeons early on for better loot.
* Character Customization: Along with choosing a class, players will be able to choose a species to play, likely including popular fantasy choices such as elves and dwarves, as well as some color options for hair, skin, clothing, etc. I love a game with customization, however small, because I feel it makes it easier to grow attached to your character and care more about the world as opposed to playing some predetermined character.
* Towns: At least one town is planned, containing important story NPCs, shops, crafting, and other NPC adventurers that give you hints throughout the game. Some NPCs will simply chat while others may give you tips about your class, an upcoming dungeon, or perhaps even how to make a fancy new weapon!
* Add Graphics and Music: Right now it's a game about squares, which was fun about 40 years ago.
