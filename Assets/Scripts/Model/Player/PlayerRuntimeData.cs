

using QFramework;

namespace Assets.Scripts.Model.Player {
    public class PlayerRuntimeData {
        public BindableProperty<int> Health { get; set; }
        public BindableProperty<int> Energy { get; set; }

        public PlayerRuntimeData(int initHealth) { 
            Health = new BindableProperty<int>(initHealth);
            Energy = new BindableProperty<int>();
        }

    }
}
