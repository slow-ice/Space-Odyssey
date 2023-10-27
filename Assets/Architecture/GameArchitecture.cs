

using Assets.Scripts.Model.Player;
using QFramework;

namespace Assets.Architecture {
    public class GameArchitecture : Architecture<GameArchitecture> {
        protected override void Init() {
            RegisterModel(new PlayerModel());
        }
    }
}
