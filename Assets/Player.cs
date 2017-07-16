using UnityEngine;

public class Player : MonoBehaviour {
    // This script is mostly used to find the player

    public Transform resetPoint;
    public Transform containerTransform;
    public AudioSource resetSound;
    public ParticleSystem jetpackParticleSystem;

    private void Start() {
        resetPoint = transform;
    }

    private void Update() {
        if ( Input.GetAxis( "Debug skip and reset" ) > 0.0f ) {
            ResetPlayer();
        }
    }

    public void ResetPlayer() {
        Health health = GetComponent<Health>();
        // health.Heal( health.getMaxHealth() );
        transform.position = resetPoint.position;
        resetSound.Play();
        jetpackParticleSystem.Clear();
    }
}
