# Changelog

## May 4, 2021
### Changes
* Added the ability to crouch by holding "down" while on the ground. This state shortens the player's hitbox. The player cannot move left and right while crouching, but can cancel the crouch by jumping.
* Added the ability to slide by pressing the jump button while crouching. This is only usable if the player has unlocked the ability to slide.

### Known Issues
* Player can still launch from the wall when wall jumping. It seems this only occurs when pressing away from the wall and pressing the jump button on the same frame, but the exact cause is unknown.
* If the player attempts to stand back up (simply let go of "down") when there is no room to stand up, the player will get stuck in the ceiling. This can be done by sliding into a narrow passage and standing up before reaching the other side.
* The player cannot change direction while crouching.
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
