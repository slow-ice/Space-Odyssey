

using Assets.Scripts.Event;
using QFramework;

namespace Assets.Scripts.Model.Player {
    public class PlayerModel : AbstractModel {
        public BindableProperty<int> Health { get; set; }
        public BindableProperty<int> Energy { get; set; }

        protected override void OnInit() {

        }

        public void InitRuntimeData(int initHealth) {
            Health = new BindableProperty<int>(initHealth);
            Energy = new BindableProperty<int>(0);

            RegisterHealthEvent();
        }

        void RegisterHealthEvent() {
            Health.Register(onValueChanged => {
                if (onValueChanged <= 0) {
                    TypeEventSystem.Global.Send(new PlayerDieEvent());
                }
            });
        }
    }
}
