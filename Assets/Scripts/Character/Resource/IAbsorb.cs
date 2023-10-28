

using System;
using UnityEngine;

namespace Assets.Scripts.Character.Resource {
    public interface IAbsorb {
        int GetEnergy();
        void OnAbsorbAction(Transform playerTrans);
    }
}
