# Documentation for issue [#7](https://github.com/orgs/SillyBusinessInc/projects/1/views/1?pane=issue&itemId=85873169&issue=SillyBusinessInc%7CSillyBusinessGame%7C7)

## class PickupBase
This abstract class handles the basic logic for the pickup system.

### functionality of PickupBase
On Start() I import the necessary components: Rigidbody, ParticleSystem, MeshRenderer and Collider. And set the target to the Player.

The Rigidbody can be absent in which case the script will automatically make the pickup static (meaning it does not move and therefore doesn't need a rigidbody). The Rigidbody if present must be on the base object that this script is attached to due to the functionality of the Rigidbody component.

The ParticleSystem can also be absent in which case the Pickup will destroy itself instantly upon collecting instead of playing a particle effect. If present the ParticleSystem must be located in one of the children of the Pickup object.

The MeshRenderer and Collider can of course not be absent and must be located in one of the Children of the Pickup object.

Collect() is called when the Pickup is near the target. It calculates the path the pickup will take to the target in a small animation and then calls a Coroutine called CollectAnimation() to execute the animation. After this the OnCollectCompleted() is called which calles the OnTrigger() method for any effect of the pickup and then destroys the Pickup appropriately.


### implementation of PickupBase
pickupRange -> Defines the range at which the pickup can be collected.
pickupSpeed -> Defines the speed with which the pickup will move to the player.
isLocked -> Locks the Pickup so it cannot be collected.
isstatic -> Prevents the pickup from moving or being affected by physics.

## class PickupHealth
This class implements the PickupBase class as a useable pickup object.

It does this by overriding the OnTrigger() method to add functionality when the pickup is triggered. In this case by adding health.