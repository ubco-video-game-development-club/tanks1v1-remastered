using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public static MusicPlayer instance;

    public float menuVolume = 1f;
    public float gameVolume = 0.2f;

    private AudioSource audioSource;

    void Awake() {
        if (instance != null) {
            Destroy(gameObject);
        } else {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        audioSource = GetComponent<AudioSource>();
    }

    public void SetGameVolume() {
        audioSource.volume = gameVolume;
    }

    public void SetMenuVolume() {
        audioSource.volume = menuVolume;
    }
}
