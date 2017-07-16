using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ DisallowMultipleComponent ]
public class BurnsFromJetpackImpact : MonoBehaviour {

    private List<ParticleCollisionEvent> collisionEvents;
    public Transform parentTransform;

    private void Start() {
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    private void OnParticleCollision( GameObject other ) {
        ParticleSystem particleSystem = other.GetComponent<ParticleSystem>();
        if ( particleSystem != null ) {
            int numCollisionEvents = particleSystem.GetCollisionEvents( gameObject, collisionEvents );
            int i = 0;
            while ( i < numCollisionEvents ) {
                ParticleDamage particleDamage = other.GetComponent<ParticleDamage>();
                if ( particleDamage != null ) {
                    Vector3 hitPosition = collisionEvents[ i ].intersection;
                    GameObject particle = Instantiate( particleDamage.damageParticleObject, hitPosition, Quaternion.identity, parentTransform );
                }
                i++;
            }
        }
    }
}
