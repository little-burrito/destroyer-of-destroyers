using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ RequireComponent( typeof( Rigidbody2D ) ) ]
[ RequireComponent( typeof( Collider2D ) ) ]
[ RequireComponent( typeof( Destructible ) ) ]
[ DisallowMultipleComponent ]
public class HealthPickup : MonoBehaviour {

    public float healthPointsToHealOnPickup = 1.0f;
    public float pushForce = 0.0f;
    private Destructible destructible;

    private void Start() {
        destructible = GetComponent<Destructible>();
    }

    private void OnCollisionEnter2D( Collision2D collision ) {

        Health health = collision.collider.gameObject.GetComponent<Health>();
        if ( health != null ) {

            health.Heal( healthPointsToHealOnPickup );

            Rigidbody2D rb = collision.collider.GetComponent<Rigidbody2D>();
            if ( rb != null ) {
                Vector2 difference = collision.collider.transform.position - transform.position;
                rb.AddForceAtPosition( difference.normalized * pushForce, transform.position, ForceMode2D.Impulse );
            }
        }

        destructible.Destruct();
    }
}
