using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ DisallowMultipleComponent ]
public class Explosion : MonoBehaviour {

    public float range = 5.0f;
    public float power = 5.0f;
    public float damage = 2.0f;

	// Use this for initialization
	void Start () {
        Rigidbody2D[] allRigidBodies = GameObject.FindObjectsOfType<Rigidbody2D>();
        foreach ( Rigidbody2D rb in allRigidBodies ) {
            if ( rb.gameObject != gameObject ) {
                Vector2 difference = rb.transform.position - transform.position;
                if ( difference.magnitude <= range ) {
                    // Vector2 force = difference.normalized * Mathf.InverseLerp( range, 0.0f, difference.magnitude ) * explosionPower;
                    Vector2 force = difference.normalized * power;
                    rb.AddForceAtPosition( force, transform.position, ForceMode2D.Impulse );
                    Health otherHealth = rb.GetComponent<Health>();
                    if ( otherHealth != null ) {
                        otherHealth.DamageFromExplosion( damage, range, difference.magnitude );
                    }
                }
            }
        }
	}

    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere( transform.position, range );
    }
}
