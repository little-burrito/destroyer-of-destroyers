using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDesctruct : MonoBehaviour {

    public float secondsUntilSelfDesctruct = 1.0f;
	
	void Update () {
        secondsUntilSelfDesctruct -= Time.deltaTime;
        if ( secondsUntilSelfDesctruct <= 0.0f ) {
            Destroy( gameObject );
        }
	}
}
