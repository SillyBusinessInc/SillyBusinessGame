using UnityEngine;

public struct GroundCheckData
{
    public bool IsGrounded;
    public float SlopeAngle;
    public Vector3 GravityDirection; // functional direction (used for the math and stuff)
    public Vector3 ModelDownDirection; // visual direction (used for the visual rotation of the player)
    // They will probably be aligned with each other most of the time. And the moments that they arent, it will not be that big of a difference
}

