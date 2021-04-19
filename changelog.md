# Changelog

## April 16, 2021
### Changes
* Enemies using the GroundChaseMovement behavior can now jump if the target player is above them or walk off of a platform if the player is below them. These behaviors are both optional.
* Tweaked player hitboxes and movement behavior to fix the issue of getting caught on corners. If this solution continues to work, enemy hitboxes will be updated soon.

### Known Issues
* ~~Player and enemy hitboxes occasionally get caught on the corners of terrain due to the shape of the hitboxes.~~
* Player and enemy sometimes jitter around when falling while pressing against a wall. Cause unknown.
