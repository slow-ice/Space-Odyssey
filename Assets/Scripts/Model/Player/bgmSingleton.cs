using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Model.Player {
    public class bgmSingleton : Singleton<bgmSingleton> {

        private void Start() {
            GetComponent<AudioSource>().Play();
        }
    }
}
