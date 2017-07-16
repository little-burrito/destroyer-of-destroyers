using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ RequireComponent( typeof( Destructible ) ) ]
public class SelfDestructUsingDestructible : MonoBehaviour {

    public float secondsUntilSelfDesctruct = 1.0f;
	
	void Update () {
        secondsUntilSelfDesctruct -= Time.deltaTime;
        if ( secondsUntilSelfDesctruct <= 0.0f ) {
            GetComponent<Destructible>().Destruct();
        }
	}
}
