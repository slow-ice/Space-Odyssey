using Assets.Scripts.Character;
using Assets.Scripts.Character.Player.State;
using Assets.Scripts.Model.Player;
using Assets.Scripts.Refactoring;
using Assets.Utility;
using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    PlayerData_SO mPlayerData;
    public PlayerData_SO PlayerData { get { return mPlayerData; } }
    public PlayerCore mCore { get; private set; }

    IOCContainer mContainer = new IOCContainer();
    PlayerFSM RootFSM;

    private void Awake() {
        OnFsmInit();
    }

    private void Update() {
        
    }

    #region FSM

    void OnFsmInit() {
        RootFSM = new PlayerFSM(PlayerEnumStates.Root);
        RootFSM.IsRootMachine = true;

        RegisterState(new PlayerMoveState(PlayerEnumStates.Move), RootFSM);

        RootFSM.OnInit();
    }

    void RegisterState<TState>(TState state, PlayerFSM parent) where TState : PlayerState {
        state.OnInit(this, mCore);
        parent.AddState(state);

        mContainer.Register<TState>(state);
    }

    void RegisterMachine<TState>(TState state, PlayerFSM parent) where TState : PlayerFSM {
        state.OnInit(this, mCore);
        parent.AddState(state);

        mContainer.Register<TState>(state);
    }

    public TState GetState<TState>() where TState : FSMState<PlayerEnumStates> {
        return mContainer.Get<TState>();
    }

    #endregion
}
