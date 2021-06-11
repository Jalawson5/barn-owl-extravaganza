# Changelog

## June 11, 2021

### Changes
* Tweaked the position of the keyboard cursor to be a little more centered around each letter.
* Added a missing object to the Character Creator, which was causing some problems in the background.

### Known Issues
* While the name entry window is open, input is still controlling the cursor in the previous menu.~~, which causes some issues in the background.~~
* The character creator affects nothing cosmetically, due to the current lack of art.
* Wall jump speed needs some tweaking.
* The player can use their downward attack while sliding. This isn't intended, but may be reworked into a different kind of attack in the future.
* Still working on player movement.
* Enemies with the AirChaseMovement move type will not chase players if they are on the other side of a wall, rather than on top of the wall.
* The keyboard for naming your character does nothing at the moment. The cursor properly moves around the keyboard, however.

## June 8, 2021
Note: Been a while, huh? Progress has been a bit slow as of late, unfortunately.

### Changes
* Created the keyboard for inputting a name in the character creator. This was actually completed last week, but I can't remember if I pushed that code or not.
* Created a dictionary that holds any variables that need to be used in text. So far, this includes the player's name, race, class, and any appropriate pronouns. Later, this may include race or class specific dialogue for NPCs, but we'll see.

### Known Issues
* The character creator affects nothing cosmetically, due to the current lack of art.
* Wall jump speed needs some tweaking.
* The player can use their downward attack while sliding. This isn't intended, but may be reworked into a different kind of attack in the future.
* Still working on player movement.
* Enemies with the AirChaseMovement move type will not chase players if they are on the other side of a wall, rather than on top of the wall.
* The keyboard for naming your character does nothing at the moment. The cursor properly moves around the keyboard, however.

## June 2, 2021
Note: I'm back home, so code updates should come a little more often now.

### Changes
* Coded the cursor for the character creator's naming window.

### Known Issues
* The character creator affects nothing cosmetically, due to the current lack of art.
* Wall jump speed needs some tweaking.
* The player can use their downward attack while sliding. This isn't intended, but may be reworked into a different kind of attack in the future.
* Still working on player movement.
* Enemies with the AirChaseMovement move type will not chase players if they are on the other side of a wall, rather than on top of the wall.

## May 24, 2021

### Changes
* Implemented most of the character creator. The creator now calculates and displays the character's base stats based on the chosen race and class.

### Known Issues
* The character creator affects nothing cosmetically, due to the current lack of art.
* Wall jump speed needs some tweaking.
* The player can use their downward attack while sliding. This isn't intended, but may be reworked into a different kind of attack in the future.
* Still working on player movement.
* Enemies with the AirChaseMovement move type will not chase players if they are on the other side of a wall, rather than on top of the wall.

## May 21, 2021

### Changes
* Added some UI sprites.
* Created the UI for the character creator.

### Known Issues
* Wall jump speed needs some tweaking.
* The player can use their downward attack while sliding. This isn't intended, but may be reworked into a different kind of attack in the future.
* Still working on player movement.
* Enemies with the AirChaseMovement move type will not chase players if they are on the other side of a wall, rather than on top of the wall.

## May 20, 2021

### Changes
* Added character races (human, elf, dwarf, gnome, and three unnamed) to prepare for character creation. Races affect the player's base stats as well as what progression ability the character starts with.
* Tweaked classes to move some functionality (base stats) to character races instead.
* Added scenes to prepare for character creation.

### Known Issues
* Wall jump speed needs some tweaking.
* The player can use their downward attack while sliding. This isn't intended, but may be reworked into a different kind of attack in the future.
* Still working on player movement.
* Enemies with the AirChaseMovement move type will not chase players if they are on the other side of a wall, rather than on top of the wall.

## May 19, 2021

### Changes
* Changed the dialogue system to allow for different dialogue choices. Dialogue will change depending on the player's choice.
* Changed dialogue boxes to only appear during dialogue. The new dialogue box for different dialogue choices works the same way.

### Known Issues
* Wall jump speed needs some tweaking.
* The player can use their downward attack while sliding. This isn't intended, but may be reworked into a different kind of attack in the future.
* Still working on player movement.
* Enemies with the AirChaseMovement move type will not chase players if they are on the other side of a wall, rather than on top of the wall.

## May 18, 2021

### Changes
* Changed how dialogue works and how the Typewriter handles dialogue to prepare for decision making in the future.

### Known Issues
* Wall jump speed needs some tweaking.
* The player can use their downward attack while sliding. This isn't intended, but may be reworked into a different kind of attack in the future.
* Still working on player movement.
* Enemies with the AirChaseMovement move type will not chase players if they are on the other side of a wall, rather than on top of the wall.

## May 17, 2021
Note: I have been out of town for the past few days, and will be out of town for another couple of weeks or so, so updates may be a bit slower at this time.
### Changes
* Added fuctionality to the Typewriter. The Typewriter will write dialogue, wrapping to the next line if the dialogue is too long.
* Tweaked the Typewriter to fix the problem of printing incorrect characters when moving to a new dialogue.
* Added a basic pause function. The player cannot move while the game is paused. So far, no other objects are affected by pause. The Typewritter uses pausing to prevent the player from moving while dialogue is present.

### Known Issues
* ~~When writing to a new dialogue box (if the NPC's dialogue takes multiple windows, for example), the first letter of the dialogue will be incorrect. Cause is currently unknown.~~
* Wall jump speed needs some tweaking.
* The player can use their downward attack while sliding. This isn't intended, but may be reworked into a different kind of attack in the future.
* Still working on player movement.
* Enemies with the AirChaseMovement move type will not chase players if they are on the other side of a wall, rather than on top of the wall.

## May 12, 2021
### Changes
* Added 70 characters to the Typewriter.

## Known Issues
* Wall jump speed needs some tweaking.
* The player can use their downward attack while sliding. This isn't intended, but may be reworked into a different kind of attack in the future.
* Still working on player movement.
* Enemies with the AirChaseMovement move type will not chase players if they are on the other side of a wall, rather than on top of the wall.

## May 10, 2021
### Changes
* Tweaked the test font to (hopefully) make coding easier in the long-run.
* Added a "Typewriter" class that will eventually handle writing dialogue to the screen.

### Known Issues
* Wall jump speed needs some tweaking.
* The player can use their downward attack while sliding. This isn't intended, but may be reworked into a different kind of attack in the future.
* Still working on player movement.
* Enemies with the AirChaseMovement move type will not chase players if they are on the other side of a wall, rather than on top of the wall.

## May 7, 2021
### Changes
* Created and added a simple test font to experiment with. This font will not be in the final game.
* Created a container of sorts for dialogue, which will contain all of the information needed for a conversation.

### Known Issues
* Wall jump speed needs some tweaking.
* The player can use their downward attack while sliding. This isn't intended, but may be reworked into a different kind of attack in the future.
* Still working on player movement.
* Enemies with the AirChaseMovement move type will not chase players if they are on the other side of a wall, rather than on top of the wall.

## May 6, 2021
### Changes
* Added swimming mechanics. For now, swimming grants the player infinite double jumps while underwater if the player has unlocked the ability to swim.
* Not necessarily a change, but a discovery. With precise inputs, the player can exit a slide into a dash and gain an extended jump. It's currently unknown if a slide jump is longer than a normal dash jump, but I still think it's neat and I'm keeping it in.

### Known Issues
* Wall jump speed needs some tweaking.
* The player can use their downward attack while sliding. This isn't intended, but may be reworked into a different kind of attack in the future.
* Still working on player movement.
* Enemies with the AirChaseMovement move type will not chase players if they are on the other side of a wall, rather than on top of the wall.

## May 5, 2021
### Changes
* Added water to the game. While underwater, the player's movement is slowed.
* Tweaked wall jumping, fixing the problem causing it to not work without frame perfect inputs.

### Known Issues
* ~~When wall jumping (except frame perfect wall jumps), the player is pushed back to the wall. This is due to how the force for a wall jump is calculated. This occurs both underwater and above; however, it is far more noticable underwater due to the slower movement.~~
* Player *always* launches from the wall when wall jumping. This is a mere numbers problem that will be tweaked in the coming days.
* The player can use their downward attack while sliding. This isn't intended, but may be reworked into a different kind of attack in the future.
* Still working on player movement.
* Enemies with the AirChaseMovement move type will not chase players if they are on the other side of a wall, rather than on top of the wall.

## May 4, 2021
### Changes
* Added the ability to crouch by holding "down" while on the ground. This state shortens the player's hitbox. The player cannot move left and right while crouching, but can cancel the crouch by jumping.
* Added the ability to slide by pressing the jump button while crouching. This is only usable if the player has unlocked the ability to slide.
* Tweaked player movement to allow for changing direction while crouching. This also allows for the player to change direction while sliding; an unintended but expected side-effect I may consider leaving in.
* Tweaked crouching to prevent the player from standing where there is no room to stand.

### Known Issues
* Player can still launch from the wall when wall jumping. It seems this only occurs when pressing away from the wall and pressing the jump button on the same frame, but the exact cause is unknown.
* ~~If the player attempts to stand back up (simply let go of "down") when there is no room to stand up, the player will get stuck in the ceiling. This can be done by sliding into a narrow passage and standing up before reaching the other side.~~
* ~~The player cannot change direction while crouching.~~
* The player can use their downward attack while sliding. This isn't intended, but may be reworked into a different kind of attack in the future.
* Still working on player movement.
* Enemies with the AirChaseMovement move type will not chase players if they are on the other side of a wall, rather than on top of the wall.

## May 3, 2021
### Changes
* Gave the player character a taller sprite and hitbox to get a better feel for how movement feels with more realistic dimensions.
* Tweaked the player's collision detection to account for different hitbox dimensions. Something I should have fixed a long time ago.

### Known Issues
* Player can still launch from the wall when wall jumping. It seems this only occurs when pressing away from the wall and pressing the jump button on the same frame, but the exact cause is unknown.
* Still working on player movement.
* Enemies with the AirChaseMovement move type will not chase players if they are on the other side of a wall, rather than on top of the wall.

## May 1, 2021
### Changes
* The player can now only break blocks if they have the Rock Breaker ability. 

### Known Issues
* Still working on making player movement feel right.
* Enemies with the AirChaseMovement move type will not chase players if they are on the other side of a wall, rather than on top of the wall.

## April 30, 2021
### Changes
* Added breakable blocks. These blocks are treated as solid terrain, but can be broken with attacks, opening new paths.
* Wall-jumps now require timed inputs and prevent the player from infinitely jumping against the wall. Numbers still need tweaking.

### Known Issues
* ~~It turns out wall-jumping still doesn't work properly. You only jump off the wall when you press the direction away from the wall with good timing. Otherwise, you just float up the wall which isn't what I want.~~
* Player movement feels a bit sluggish with the new method of movement. Someday I'll be happy with the movement. Someday...
* Enemies with the AirChaseMovement move type will not chase players if they are on the other side of a wall, rather than on top of the wall.

## April 29, 2021
### Changes
* Tweaked player physics to feel a bit better when wall jumping.
* Further tweaked player physics to fix the issue of pushing away from the wall.
* Added a maximum horizontal velocity to the player to prevent inexplicable circumstances where the player suddenly moves incredibly fast.

### Known Issues
* ~~Player sometimes pushes away from the wall *ever so slightly* when falling from the edge of a cliff against the wall. Does not seem to have a major impact on gameplay. Cause unknown.~~
* ~~With precise inputs, the player can launch incredibly fast from a wall when wall jumping. I believe this has something to do with combining the player's inherent move speed with the force of the jump.~~ Resolved, kinda.
* Player movement feels a bit sluggish with the new method of movement. It's getting there.
* Enemies with the AirChaseMovement move type will not chase players if they are on the other side of a wall, rather than on top of the wall.

## April 28, 2021
### Changes
* Reworked player physics. The player character now calculates gravity separately from Unity's built-in physics to allow for...
* Added the ability to double jump. Pressing the jump button in mid-air will allow the player to jump again.
* Tweaked the new player physics, fixing the gravity problems near the edge of platforms. Hopefully without any weird side-effects.
* Tweaked the terrain's colliders, fixing the slowdown when jumping into a wall.
* Tweaked the player's wall and ground collision detection. The player now stops *much* closer to the floor and walls.
* Added the ability to wall jump and slide down walls.
* Changed how the player moves to allow for wall jumping.

### Known Issues
* ~~The player's gravity does not function properly when quickly moving left and right at the edge of a platform.~~
* ~~The player's new gravity causes the player to float slightly above the ground. Though it does not interfere with gameplay any, it's looks *really* bad.~~
* ~~Player's vertical velocity slows significantly when jumping into a wall.~~
* Player movement feels a bit sluggish with the new method of movement. Will be tweaked in the next few days.
* Enemies with the AirChaseMovement move type will not chase players if they are on the other side of a wall, rather than on top of the wall.

## April 27, 2021
### Changes
* Actually added the missing attack mentioned in April 26. Turns out the attack was implemented, but the change to the weapons themselves wasn't pushed to GitHub yet.
* Added progression abilities to character stats to prepare for the full implementation of progression abilities in the future.

### Known Issues
* Enemies with the AirChaseMovement move type will not chase players if they are on the other side of a wall, rather than on top of the wall. This is a result of how the movement type chooses a new target position. Enemies that can move through walls are unaffected.

## April 26, 2021
### Changes
* Added a previously missing attack. One-hand and two-hand melee weapons now have a downward-air attack.
* Added Controller Settings to clean up code and prepare for custom controls in the future.
* Fixed a devastating typo that was causing the MasterController script to not run properly. Oops!

### Known Issues
* Enemies with the AirChaseMovement move type will not chase players if they are on the other side of a wall, rather than on top of the wall. This is a result of how the movement type chooses a new target position. Enemies that can move through walls are unaffected.

## April 23, 2021
### Changes
* Tweaked the player's jumping physics because it was bugging me. The player now jumps for a longer time, but with a smaller force.
* Tweaked the organization of items to prepare for the full implementation of items in the future.
* Adjusted the player's Rigidbody component, fixing the issue of clipping into the ground after jumping.
* Added a UI element containing a Health bar and MP bar for the player. Each bar will reduce in size according to the player's current HP and MP values. Design not final.
* The camera now follows the player.
* Tweaked skills to fix a previously undiscovered type casting issue.
* Officially tested invincibility frames (they work!).

### Known Issues
* ~~Player and jumping enemies still clip into the ground after falling a long distance.~~
* Enemies with the AirChaseMovement move type will not chase players if they are on the other side of a wall, rather than on top of the wall. This is a result of how the movement type chooses a new target position. Enemies that can move through walls are unaffected.

## April 22, 2021
### Changes
* Tweaked the organization of skills to prepare for the full implementation of skills in the future.

### Known Issues
* Player and jumping enemies still clip into the ground after falling a long distance.
* Enemies with the AirChaseMovement move type will not chase players if they are on the other side of a wall, rather than on top of the wall. This is a result of how the movement type chooses a new target position. Enemies that can move through walls are unaffected.

## April 21, 2021
### Changes
* Enemies with the AirChaseMovement move type are now able to choose a new target position to dash towards if they would collide into a wall. This allows AirChaseMovement enemies to attempt to fly above walls to get a better angle to the player.

### Known Issues
* Player and jumping enemies still clip into the ground after falling a long distance.
* Enemies with the AirChaseMovement move type will not chase players if they are on the other side of a wall, rather than on top of the wall. This is a result of how the movement type chooses a new target position. Enemies that can move through walls are unaffected.

## April 19, 2021
### Changes
* Tweaked terrain colliders, mostly fixing jittering while falling against a wall, hopefully without a major hit to performance.
* Tweaked the player's jumping physics to allow the player to perform shorter jumps. Jumping physics are still likely to change.
* Updated the GroundChaseMovement behavior to better check for walls, fixing the issue of getting caught on corners.

### Known Issues
* Player (and likely jumping enemies) clip into the ground slightly after falling a long enough distance. This does not seem to have any impact on gameplay, but it definitely looks out of place.
* ~~Enemies do not use the player's updated movement behavior and can still get stuck on the corners of terrain.~~
* Enemies with the Air Chase behavior can still get stuck on the corners of terrain. Moving the player to an angle where the enemy moves away from the wall will free the enemy. Other movement behaviors do not seem to have this issue.

## April 16, 2021
### Changes
* Enemies using the GroundChaseMovement behavior can now jump if the target player is above them or walk off of a platform if the player is below them. These behaviors are both optional.
* Tweaked player hitboxes and movement behavior to fix the issue of getting caught on corners. If this solution continues to work, enemy hitboxes will be updated soon.

### Known Issues
* ~~Player and enemy hitboxes occasionally get caught on the corners of terrain due to the shape of the hitboxes.~~
* Player and enemy sometimes jitter around when falling while pressing against a wall. Cause unknown.
