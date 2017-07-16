using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateObjectSpawnerWhenPlayerIsWithinRange : MonoBehaviour {

    private Player player;
    public float playerActivationRange = 20.0f;
    public ObjectSpawner objectSpawner; 

	// Use this for initialization
	void Start () {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update () {
        Vector2 difference = player.transform.position - transform.position;
		if ( difference.magnitude <= playerActivationRange ) {
            objectSpawner.gameObject.SetActive( true );
        } else {
            objectSpawner.gameObject.SetActive( false );
            objectSpawner.ResetSpawnTimer();
        }
	}

    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere( transform.position, playerActivationRange );
    }
}
