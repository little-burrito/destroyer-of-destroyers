using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ RequireComponent( typeof( Health ) ) ]
[ DisallowMultipleComponent ]
public class Destructible : MonoBehaviour {
    private Health health;
    public GameObject explosion;
    public GameObject remains;
    public Transform parentTransform;

    private void Start() {
        health = GetComponent<Health>();
    }

	void Update () {
        if ( health != null ) {
            if ( health.IsDead() ) {
                Destruct();
            }
        }
    }

    public void Destruct() {
        Instantiate( explosion, transform.position, Quaternion.identity, parentTransform );
        if ( remains != null ) {
            Instantiate( remains, transform.position, transform.rotation, parentTransform );
        }
        Destroy( gameObject );
    }
}
