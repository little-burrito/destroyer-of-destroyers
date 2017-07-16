using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ RequireComponent( typeof( AudioSource ) ) ]
public class PlayRandomly : MonoBehaviour {

    private AudioSource audioSource;
    private float timer;

    public float minTimeBetween;
    public float maxTimeBetween;

	// Use this for initialization
	void Start () {
        resetTimer();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update () {
        timer -= Time.deltaTime;
        if ( timer <= 0.0f ) {
            audioSource.Play();
            resetTimer();
        }
	}

    void resetTimer() {
        timer = Random.Range( minTimeBetween, maxTimeBetween );
    }
}
