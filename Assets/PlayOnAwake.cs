using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayOnAwake : MonoBehaviour {
    // THIS IS A PATCH SINCE UNITY PLAY ON AWAKE ALSO PLAYS IN THE EDITOR AND MAKES ME CRAZY

    private void Awake() {
        AudioSource audioSource = GetComponent<AudioSource>();
        if ( audioSource != null ) {
            if ( !audioSource.isPlaying ) {
                if ( audioSource.timeSamples != 0 ) {
                    audioSource.UnPause();
                } else {
                    audioSource.Play();
                }
            }
        }
    }
}
