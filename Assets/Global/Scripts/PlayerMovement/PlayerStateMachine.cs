using System.Collections.Generic;
using UnityEngine;

public enum PlayerStateType
{
    Ground,
    Falling,
    Jump
}

public class PlayerStateMachine
{
    private PlayerBaseState _currentState;
    private Dictionary<PlayerStateType, PlayerBaseState> _states;
    private PlayerMovement _player;
    
     public PlayerStateMachine(PlayerMovement player)
     {
         this._player = player;
         this.InitializeStates();
     }
     
    private void InitializeStates()
    {
        this._states = new()
        {
            { PlayerStateType.Ground, new PlayerGroundState(this._player, this) },
            { PlayerStateType.Falling, new PlayerFallingState(this._player, this) },
            { PlayerStateType.Jump, new PlayerJumpState(this._player, this) }
        };
    }
    
    public void Initialize(PlayerStateType startState)
    {
        this._currentState = this._states[startState];
        this._currentState.Enter();
    }
    
    public void ChangeState(PlayerStateType newState)
    {
        this._currentState?.Exit();
        this._currentState = this._states[newState];
        this._currentState.Enter();
    }
    
    public void Update() => this._currentState?.Update();
    public void FixedUpdate() => this._currentState?.FixedUpdate();
    
    public PlayerBaseState GetCurrentState() => this._currentState;
}
