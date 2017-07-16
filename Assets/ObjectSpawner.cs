using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour {

    public GameObject objectToSpawn;
    public Transform spawnPoint;
    public ParticleSystem particleSystem;
    public Transform parentTransform;
    public bool spawnTimerStartsAtZero;
    public float spawnRate = 10.0f;
    public float animationTime = 2.0f;
    private float spawnTimer;

    private void Start() {
        ResetSpawnTimer();
    }

    public void ResetSpawnTimer() {
        if ( spawnTimerStartsAtZero ) {
            spawnTimer = animationTime;
        } else {
            spawnTimer = spawnRate + animationTime;
        }

        if ( particleSystem != null ) {
            particleSystem.Stop();
            ParticleSystem.MainModule mainModule = particleSystem.main;
            mainModule.duration = spawnRate;
            particleSystem.Play();
        }
    }

    void Update() {
        spawnTimer -= Time.deltaTime;
        if ( spawnTimer <= 0.0f ) {
            Instantiate( objectToSpawn, spawnPoint.transform.position, Quaternion.identity, parentTransform );
            spawnTimer += spawnRate;
        }
	}
}
