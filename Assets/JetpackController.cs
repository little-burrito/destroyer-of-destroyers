using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ RequireComponent( typeof( Rage ) ) ]
[ DisallowMultipleComponent ]
public class JetpackController : MonoBehaviour {

    [ SerializeField ]
    private ParticleSystem jetpackParticleSystem;
    [ SerializeField ]
    private float emissionRate;
    [ SerializeField ]
    private float lightIntensity;
    public Light jetpackLight;

    public AudioSource jetpackLoopSound;

    [ SerializeField ]
    private float jetpackForce;
    public float jetpackSpeedForce;
    public float jetpackRageForce;

    [ SerializeField ]
    private Rigidbody2D playerRigidbody;

    private Vector2 cursorPosition = Vector2.zero;
    [SerializeField]
    private float cursorInputBoundsRadius = 30.00f;
    [SerializeField]
    private float cursorDisplayRadius = 6.0f;
    [ SerializeField ]
    private GameObject cursorGameObject;

    public Transform physicalDirectionMarker;
    public Transform cursorDirectionMarker;
    public Transform facingDirectionMarking;

    new private ParticleSystem particleSystem;
    public List<ParticleCollisionEvent> collisionEvents;
    public ParticleDamage particleDamage;

    public Color rageColor;
    public Color speedColor;
    public Material jetpackParticleMaterial;
    public ParticleDamage jetpackParticleDamage;
    public float jetpackSpeedStartSizeMin;
    public float jetpackSpeedStartSizeMax;
    public float jetpackRageStartSizeMin;
    public float jetpackRageStartSizeMax;
    public float jetpackParticleSpeedForce;
    public float jetpackParticleRageForce;
    public float jetpackParticleSpeedAngle;
    public float jetpackParticleRageAngle;
    public float speedDamage = 0.01f;
    public float rageDamage = 0.3f;
    private Color currentColor;
    private Rage rage;

    private void Start() {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Locked;
        collisionEvents = new List<ParticleCollisionEvent>();
        particleSystem = GetComponent<ParticleSystem>();
        rage = GetComponent<Rage>();
    }

    void Update() {
        rotatePlayerTowardsCursor();
        handleJetpackInput();
        updateCursorPosition();
        updateDirectionMarkers();
        updateCurrentColor();
        updateJetpack();
	}

    void rotatePlayerTowardsCursor() {
        // Taken and re-written from http://answers.unity3d.com/questions/654222/make-sprite-look-at-vector2-in-unity-2d-1.html
        Vector2 dir = getDirectionTowardsMouse();
        float angle = eulerDirectionFromVector( dir );
        transform.rotation = Quaternion.AngleAxis( angle, Vector3.forward );
    }

    void updateCurrentColor() {
        currentColor = new Color( Mathf.Lerp( speedColor.r, rageColor.r, rage.rage ),
                                  Mathf.Lerp( speedColor.g, rageColor.g, rage.rage ),
                                  Mathf.Lerp( speedColor.b, rageColor.b, rage.rage ),
                                  Mathf.Lerp( speedColor.a, rageColor.a, rage.rage ) );
    }

    void updateJetpack() {
        jetpackLight.color = currentColor;
        jetpackParticleMaterial.color = currentColor;
        Color emissionColor = currentColor;
        emissionColor.a = 0.8f;
        jetpackParticleMaterial.SetColor( "_EmissionColor", emissionColor );
        ParticleSystem.MainModule mainModule = jetpackParticleSystem.main;
        mainModule.startSize = new ParticleSystem.MinMaxCurve( Mathf.Lerp( jetpackSpeedStartSizeMin, jetpackRageStartSizeMin, rage.rage ),
                                                               Mathf.Lerp( jetpackSpeedStartSizeMax, jetpackRageStartSizeMax, rage.rage ) );
        ParticleSystem.CollisionModule collisionModule = jetpackParticleSystem.collision;
        collisionModule.colliderForce = Mathf.Lerp( jetpackParticleSpeedForce, jetpackParticleRageForce, rage.rage );
        ParticleSystem.ShapeModule shapeModule = jetpackParticleSystem.shape;
        shapeModule.angle = Mathf.Lerp( jetpackParticleSpeedAngle, jetpackParticleRageAngle, rage.rage );
        jetpackParticleDamage.damage = Mathf.Lerp( speedDamage, rageDamage, rage.rage );
        jetpackForce = Mathf.Lerp( jetpackSpeedForce, jetpackRageForce, rage.rage );
    }

    void updateCursorPosition() {
        Vector2 cursorDelta = new Vector2( Input.GetAxis( "Mouse X" ), Input.GetAxis( "Mouse Y" ) );
        cursorPosition += cursorDelta;
        if ( cursorPosition.magnitude > cursorInputBoundsRadius ) {
            cursorPosition = cursorPosition.normalized * cursorInputBoundsRadius;
        }
        cursorGameObject.transform.localPosition = new Vector3( cursorPosition.x / cursorInputBoundsRadius * cursorDisplayRadius, cursorPosition.y / cursorInputBoundsRadius * cursorDisplayRadius, cursorGameObject.transform.localPosition.z );
    }

    void updateDirectionMarkers() {
        cursorDirectionMarker.eulerAngles = new Vector3( 0.0f, 0.0f, eulerDirectionFromVector( cursorPosition ) );
        physicalDirectionMarker.eulerAngles = new Vector3( 0.0f, 0.0f, eulerDirectionFromVector( -playerRigidbody.velocity ) );
    }

    void handleJetpackInput() {
        if ( Input.GetAxis( "Fire Jetpack" ) > 0 ) {
            ParticleSystem.EmissionModule emission = jetpackParticleSystem.emission;
            emission.rateOverTime = emissionRate;
            jetpackLight.intensity = lightIntensity;
            playerRigidbody.AddForce( -getDirectionTowardsMouse().normalized * jetpackForce * Time.deltaTime );
            jetpackLoopSound.UnPause();
        } else {
            ParticleSystem.EmissionModule emission = jetpackParticleSystem.emission;
            emission.rateOverTime = 0;
            jetpackLight.intensity = 0;
            jetpackLoopSound.Pause();
        }
    }

    private Vector2 getDirectionTowardsMouse() {
        // Vector2 directionTowardsMouse = Camera.main.ScreenToWorldPoint( Input.mousePosition ) - transform.position;
        // return directionTowardsMouse;
        return cursorPosition;
    }

    private float eulerDirectionFromVector( Vector2 direction ) {
        float directionOffset = 90;
        float eulerDirection = Mathf.Atan2( direction.y, direction.x ) * Mathf.Rad2Deg + directionOffset;
        return eulerDirection;
    }
}
