using Assets.Scripts.Character;
using Assets.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Refactoring {
    public class FSMState<TState> : IFSMState<TState> {
        public TState stateType { get; set; }

        // 父状态
        public FSMState<TState> mParentState;

        public List<FSMTransition<TState>> mCurrentLayerTransitions;


        private Action<FSMState<TState>> mOnEnter;
        private Action<FSMState<TState>> mOnExit;
        private Action<FSMState<TState>> mOnUpdate;
        private Action<FSMState<TState>> mOnFixedUpdate;


        public FSMState(TState stateType) {
            this.stateType = stateType;
        }

        public FSMState() { }

        public virtual void OnInit() {

        }

        public virtual void OnInit(Action<FSMState<TState>> enter = null,
                    Action<FSMState<TState>> update = null, Action<FSMState<TState>> exit = null) {
            mOnEnter = enter;
            mOnUpdate = update;
            mOnExit = exit;
        }

        /// <summary>
        /// 添加状态转换
        /// </summary>
        /// <param name="transition"></param>
        public virtual void AddTransition(FSMTransition<TState> transition) {
            mCurrentLayerTransitions = mCurrentLayerTransitions ?? new List<FSMTransition<TState>>();
            mCurrentLayerTransitions.Add(transition);
        }

        public virtual void OnEnter() {
            mOnEnter?.Invoke(this);
        }

        public virtual void OnExit() {
            mOnExit?.Invoke(this);
        }

        public virtual void OnFixedUpdate() {
            mOnFixedUpdate?.Invoke(this);
        }

        public virtual void OnUpdate() {
            mOnUpdate?.Invoke(this);
        }
    }

    public class FSMState : FSMState<string> {
        public FSMState(string state) : base(state) { }
    }

    public class PlayerState : FSMState<PlayerEnumStates> {
        protected PlayerCore core;
        protected PlayerController controller;

        public PlayerState(PlayerEnumStates state) : base(state) { }

        /// <summary>
        /// 在该层次状态机中, 从此状态转变到其他状态
        /// </summary>
        /// <param name="toState">目标状态</param>
        /// <param name="cond">条件</param>
        protected void InitialTransition(PlayerEnumStates toState, Func<bool> cond) {
            var transition = new PlayerTransition(stateType, toState);
            transition.AddCondition(cond);
            AddTransition(transition);
        }

        public void OnInit(PlayerController tcontroller, PlayerCore tcore) {
            controller = tcontroller;
            core = tcore;
        }

    }
}