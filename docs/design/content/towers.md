# Towers
[↑Index](../../index.md)

Towers are the main way to defend yourself from the onslaught of enemies. You can create a tower with three "Tower Parts": 
Tower Base: As the name implies this is the foundation of our tower. This is the part that determines a tower's health amount, attack range, and field of vision. It also can affect Turn speed multiplicatively.
Tower Head: This is how the tower damages the enemies. This part as a multitude of functions and data, including:
----------- The type of projectile the tower fires, the number of projectiles fired per shot, and the number of shots per burst.
----------- The base attack stats for damage amount, fire interval (time between shots), and area of effect.
----------- How quickly a tower can turn to target an enemy.
----------- And it has some special properties for random projectile count and spread angle when the number of projectiles is more than 1.
Tower Core: This acts as the modifier of the tower. In essence, it takes the base stats from the head and multiplicatively increases/decreases them based on the core's modifier stats.
----------- In some special case, the core will add a status effect to the tower's projectile. For example, our ice core will add the ice property to the projectile, which will freeze enemies on contact.

The projectiles our towers could receive also can be divided into four categories:
Single Shot: This is the basic projectile, it simply flies towards an enemy, deals damage to it, and it gets destroyed. Very simple projectile, makes 0 use of the area of effect stat.
Piercing Shot: Just like the single shot, it'll mainly fly towards an enemy, but where it differs is that it can go through enemies and attack others in it's path.
-------------- The max amount of enemies it can pierce is determined by the tower's area of effect stat.
Contact AOE: This projectile's damage comes in two stages: Contact damage similar to a single shot, then an "explosion" of sorts damaging surrounding enemies.
------------ The radius of the explosion is determined by the tower's area of effect stat, and it's damage is determined by the tower's damage stat
Immediate AOE: This is the most unique of our projectile types. It essentially sends out a shockwave stemming from the center of the tower, out to the max radius where it will start to dissipate.
-------------- The max radius of the shockwave is determined by the area of effect stat. Towers with this projectile type is exempt from having FOV, as it's attack range is its vision.
