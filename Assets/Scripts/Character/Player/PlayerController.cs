using Assets.Architecture;
using Assets.Scripts.Character;
using Assets.Scripts.Character.Player.State;
using Assets.Scripts.Model.Player;
using Assets.Scripts.Refactoring;
using Assets.Utility;
using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour, IController
{
    [SerializeField]
    PlayerData_SO mPlayerData;
    public PlayerData_SO PlayerData { get { return mPlayerData; } }
    public PlayerCore mCore { get; private set; }

    Rigidbody2D mRigidbody;

    IOCContainer mContainer = new IOCContainer();
    PlayerFSM RootFSM;

    public PlayerModel mModel;

    private void Awake() {
        OnComponentInit();
        OnFsmInit();
    }

    private void Start() {
        InitModel();
    }

    private void Update() {
        RootFSM.OnUpdate();

        if (Input.GetKeyDown(KeyCode.E)) {
            Debug.Log("model health: " + mModel.Health.Value);
        }
    }

    private void FixedUpdate() {
        RootFSM.OnFixedUpdate();
    }

    void OnComponentInit() {
        mRigidbody = GetComponent<Rigidbody2D>();

        mCore = new PlayerCore(this);
        mCore.OnInit();
    }

    void InitModel() {
        mModel = this.GetModel<PlayerModel>();
        mModel.InitRuntimeData(mPlayerData);
        mCore.mModel = mModel;
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

    public IArchitecture GetArchitecture() {
        return GameArchitecture.Interface;
    }

    #endregion
}
