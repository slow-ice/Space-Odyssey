using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Character.Particles {
    public class ParticleController : MonoBehaviour {

        protected ParticleSystem[] particles;

        protected virtual void OnEnable() {
            foreach (var particle in particles) {
                particle.Play();
            }
        }

        private void Awake() {
            particles = GetComponentsInChildren<ParticleSystem>();
        }

        public void Play() {
            foreach (var particle in particles) {
                particle.Play(true);
            }
        }
    }
}
