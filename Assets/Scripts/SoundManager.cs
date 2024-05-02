using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    [SerializeField]
    AudioSource audioSourceObject;

    public static SoundManager Instance;

    void Awake() {
        if (Instance == null) {
            Instance = this;
        }
    }

    public void PlaySound(AudioClip sound, float time = 0) {
        AudioSource source = Instantiate(audioSourceObject);
        source.clip = sound;
        source.time = time;
        source.Play();
        Destroy(source.gameObject, sound.length);
    }
}
