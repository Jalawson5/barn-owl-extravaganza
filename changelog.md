# Changelog

## April 19, 2021
### Changes
* Tweaked terrain colliders, mostly fixing jittering while falling against a wall, hopefully without a major hit to performance.
* Tweaked the player's jumping physics to allow the player to perform shorter jumps. Jumping physics are still likely to change.

### Known Issues
* Player (and likely jumping enemies) clip into the ground slightly after falling a long enough distance. This does not seem to have any impact on gameplay, but it definitely looks out of place.
* Enemies do not use the player's updated movement behavior and can still get stuck on the corners of terrain.

## April 16, 2021
### Changes
* Enemies using the GroundChaseMovement behavior can now jump if the target player is above them or walk off of a platform if the player is below them. These behaviors are both optional.
* Tweaked player hitboxes and movement behavior to fix the issue of getting caught on corners. If this solution continues to work, enemy hitboxes will be updated soon.

### Known Issues
* ~~Player and enemy hitboxes occasionally get caught on the corners of terrain due to the shape of the hitboxes.~~
* Player and enemy sometimes jitter around when falling while pressing against a wall. Cause unknown.
