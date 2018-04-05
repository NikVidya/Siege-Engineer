using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMusic : MonoBehaviour {
    public AudioClip[] audioClips = null;
    public AudioSource audioSource;
    void Awake () {
        DontDestroyOnLoad(this);

    }
    void Update () {

    }
    public void PlaySong (int num) {
        audioSource.clip = audioClips[num];
        audioSource.Play();
    }
}