using Assets.Architecture;
using QFramework;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

namespace Assets.Scripts.Model.Player {
    public class ScoreCount : MonoBehaviour, IController {
        public TextMeshProUGUI mText;
        PlayerModel mModel;

        private void Awake() {
            mText = GetComponent<TextMeshProUGUI>();
        }

        private void Start() {
            mModel = this.GetModel<PlayerModel>();
        }

        private void Update() {
            mText.text = mModel.scoreCount.ToString();
        }

        public IArchitecture GetArchitecture() {
            return GameArchitecture.Interface;
        }
    }
}
