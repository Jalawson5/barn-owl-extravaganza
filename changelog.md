# Changelog

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
