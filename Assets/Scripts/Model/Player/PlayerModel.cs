

using Assets.Scripts.Event;
using QFramework;

namespace Assets.Scripts.Model.Player {
    public class PlayerModel : AbstractModel {
        public BindableProperty<int> Health { get; set; }
        public BindableProperty<int> Energy { get; set; }
        public int attackValue { get; set; }

     

        protected override void OnInit() {

        }

        public void InitRuntimeData(PlayerData_SO playerData) {
            Health = new BindableProperty<int>(playerData.health);
            Energy = new BindableProperty<int>(0);
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
            Health.Value += changeValue;
        }

        public void ChangeEnergy(int changeValue) {
            Energy.Value += changeValue;
        }
    }
}
