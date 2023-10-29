

using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Character.Audio {
    public class AudioManager : Singleton<AudioManager> {
        public List<AudioClip> clipList;
        public AudioSource audioSource;

        public void Play(int index) {
            audioSource.clip = clipList[index];
            audioSource.Play();
        }

        public void Stop() {
            audioSource.Stop();
        }
    }
}
