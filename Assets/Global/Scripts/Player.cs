using UnityEngine;

public class Player : MonoBehaviour
{
    public BaseMovement baseMovement;
    
    public Player()
    {
        baseMovement = new Idle(this);
    }
    public void SetState(BaseMovement newState)
    {
        baseMovement = newState;
    }

    
}
