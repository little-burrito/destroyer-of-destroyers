using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ DisallowMultipleComponent ]
public class Health : MonoBehaviour {

    public float health = 10.0f;
    public float maxHealth = 10.0f;
    public Health IAmOnlyAReferenceToThisHealth;

    public void Damage( float damage ) {
        if ( IAmOnlyAReferenceToThisHealth == null ) {
            health = Mathf.Max( 0.0f, health - damage );
        } else {
            IAmOnlyAReferenceToThisHealth.Damage( damage );
        }
    }

    public void DamageFromExplosion( float explosionDamage, float explosionRange, float explosionDistance ) {
        if ( IAmOnlyAReferenceToThisHealth == null ) {
            // float damage = Mathf.InverseLerp( explosionRange, 0.0f, explosionDistance ) * explosionDamage;
            this.Damage( explosionDamage );
        } else {
            IAmOnlyAReferenceToThisHealth.DamageFromExplosion( explosionDamage, explosionRange, explosionDistance );
        }
    }

    public void Heal( float healthpoints ) {
        if ( IAmOnlyAReferenceToThisHealth == null ) {
            health = Mathf.Min( maxHealth, health + healthpoints );
        } else {
            IAmOnlyAReferenceToThisHealth.Heal( healthpoints );
        }
    }

    public float getMaxHealth() {
        if ( IAmOnlyAReferenceToThisHealth == null ) {
            return maxHealth;
        }
        return IAmOnlyAReferenceToThisHealth.getMaxHealth();
    }

    public bool IsDead() {
        return health <= 0;
    }
}
