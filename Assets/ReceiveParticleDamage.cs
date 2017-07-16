using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ RequireComponent( typeof( Health ) ) ]
[ RequireComponent( typeof( Destructible ) ) ]
[ DisallowMultipleComponent ]
public class ReceiveParticleDamage : MonoBehaviour {

    private Health health;
    public List<ParticleCollisionEvent> collisionEvents;

    private void Start() {
        collisionEvents = new List<ParticleCollisionEvent>();
        health = GetComponent<Health>();
    }

    private void OnParticleCollision( GameObject other ) {
        ParticleSystem particleSystem = other.GetComponent<ParticleSystem>();
        if ( particleSystem != null ) {
            if ( collisionEvents != null ) {
                int numCollisionEvents = particleSystem.GetCollisionEvents( gameObject, collisionEvents );
                int i = 0;
                while ( i < numCollisionEvents ) {
                    ParticleSystem.Particle[] particles = new ParticleSystem.Particle[ particleSystem.main.maxParticles ];
                    int numParticlesAlive = particleSystem.GetParticles( particles );
                    for ( int j = 0; j < numParticlesAlive; j++ ) {
                        particles[ j ].remainingLifetime = 0;
                    }

                    ParticleDamage particleDamage = other.GetComponent<ParticleDamage>();
                    if ( particleDamage != null ) {
                        health.Damage( particleDamage.damage );
                    }
                    i++;
                }
            }
        }
    }
}
