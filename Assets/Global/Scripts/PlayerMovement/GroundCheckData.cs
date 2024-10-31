using UnityEngine;

public struct GroundCheckData
{
    public bool IsGrounded;
    public float SlopeAngle;
    public Vector3 GravityDirection; // functional direction (used for the math and stuff)
    public Vector3 ModelDownDirection; // visual direction (used for the visual rotation of the player)
    // They will probably be aligned with each other most of the time. And the moments that they arent, it will not be that big of a difference
}

// Why this class (also explaining why gravity):
// - There should be different states in the movement class since we are going to have all kinds of different behaviors: walking/running , jumping, falling, climbing, gliding.
// - Just like any other decent movement script, you need some way of checking if you are on the ground.
// - We also have to work with our own custom gravity, reason for that alone: 
//      1. Last time i checked looking at a 4 legged mammal, their legs are always aligned with the floor underneath. (there are a lot more ways to do this, but this is one of them)
//      2. We are working with a squirrel, that also has to be able to climb pretty much any surface if we so chose. It should also work with any kind of slope like trees, rocks, and hanging branches. And it should also actually look like you are climbing
//      3. We dont want to change the gravity of the whole scene. So changing gravity in project settings are out of the question.
// - Some states, like climbing, might have different requirements for this gravity. and so we want to be able to override the method that calculates this gravity.
// - All these 3 attributes require you to do a raycast to the ground. In fact, the exact same raycast. They are also always calculated each FixedUpdate in sequence.
//    So we can either:
//      1. redo the same raycast 3 times in sequence.
//      2. rely on the order of the 3 methods, and use the saved date from one in the other (This is just asking for bugs, since someone might change the order without knowing,
//          and its also not really noticeable if you re-ordered them. Then a method will be using the wrong data from the previous fixedUpdate)
//      3. (this) make a class that contains all the data, and just combine the 3 methods in to 1 that returns this class.
