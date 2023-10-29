using Assets.Architecture;
using Assets.Scripts.Character;
using Assets.Scripts.Character.Player.State;
using Assets.Scripts.Event;
using Assets.Scripts.Model.Player;
using Assets.Scripts.Refactoring;
using Assets.Scripts.Utility.Input_System;
using Assets.Utility;
using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    Transform blackHole;
    public Vector3 targetScale;
    public float fadeTime;

    public PlayerModel mModel;


    private void Awake() {
        OnComponentInit();
        OnFsmInit();
    }

    private void Start() {
        InitModel();
        RegisterPlayerDie();
        blackHole = transform.GetChild(0);
    }

    void RegisterPlayerDie() {
        TypeEventSystem.Global.Register<PlayerDieEvent>(onEvent => {
            InputManager.Instance.inputActions.Disable();
            GetComponent<SpriteRenderer>().enabled = false;
            StartCoroutine(Bigger(2f));
        });
    }

    IEnumerator Bigger(float time) {
        var cur = 0f;
        while (cur < time) {
            mCore.mBlackHoleParticle.Play();
            cur+= Time.deltaTime;
            blackHole.localScale = Vector3.Lerp(blackHole.localScale, targetScale, Time.deltaTime 
                * cur / time);
            yield return null;
        }
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadSceneAsync("Gameover");
    }

    private void Update() {
        RootFSM.OnUpdate();

        if (Input.GetKeyDown(KeyCode.E)) {
            mModel.ChangeEnergy(5);
            Debug.Log("energy: " + mModel.Energy.Value);
        }
    }

    private void FixedUpdate() {
        RootFSM.OnFixedUpdate();
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, mPlayerData.absorbRadius);
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

    public void CheckBulletHit(int damage, int energy) {
        mCore.DealBulletHit(damage, energy);
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
