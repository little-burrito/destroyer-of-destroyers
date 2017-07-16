using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateAfterTimeout : MonoBehaviour {
    public float secondsUntilActivation = 1.0f;
    public float randomMinSecondsUntilActivation = -1.0f;
    public float randomMaxSecondsUntilActivation = -1.0f;
    public GameObject gameObjectToActivate;

    private void Start() {
        if ( randomMinSecondsUntilActivation >= 0.0f && randomMaxSecondsUntilActivation >= 0.0f ) {
            secondsUntilActivation = Random.Range( randomMinSecondsUntilActivation, randomMaxSecondsUntilActivation );
            Debug.Log( secondsUntilActivation );
        }
    }

    void Update () {
        if ( secondsUntilActivation > 0.0f ) {
            secondsUntilActivation -= Time.deltaTime;
            if ( secondsUntilActivation <= 0.0f ) {
                if ( gameObjectToActivate != null ) {
                    gameObjectToActivate.SetActive( true );
                } else {
                    Debug.LogError( "GameObject to activate is null." );
                }
            }
        }
	}
}
