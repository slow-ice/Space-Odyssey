

using Assets.Scripts.Event;
using QFramework;
using System;
using UnityEngine;

namespace Assets.Scripts.Model.Player {
    public class PlayerModel : AbstractModel {
        public BindableProperty<int> Health { get; set; }
        public BindableProperty<int> Energy { get; set; }
        public PlayerData_SO PlayerData { get; set; }
        public int attackValue { get; set; }
        public int scoreCount;
     

        protected override void OnInit() {

        }

        public void InitRuntimeData(PlayerData_SO playerData) {
            Health = new BindableProperty<int>(playerData.health);
            Energy = new BindableProperty<int>(0);
            scoreCount = 0;
            PlayerData = playerData;
            attackValue = playerData.attackValue;
            
            RegisterHealthEvent();
        }

        void RegisterHealthEvent() {
            Health.Register(onValueChanged => {
                if (onValueChanged <= 0) {
                    TypeEventSystem.Global.Send(new PlayerDieEvent());
                }
            });
        }

        public void ChangeHealth(int changeValue) {
            var target = Health.Value + changeValue;
            Health.Value = Mathf.Clamp(target, -10, PlayerData.health);
        }

        public void ChangeEnergy(int changeValue) {
            if (changeValue > 0) {
                scoreCount += changeValue;
            }
            var target = Energy.Value + changeValue;
            Energy.Value = Mathf.Clamp(target, 0, PlayerData.maxEnergy);
        }
    }
}
