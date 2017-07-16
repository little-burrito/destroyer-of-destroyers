using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {

    private Player player;
    public Transform playerResetPoint;
    public GameObject contentContainer;
    public GameObject contentContainerPrefab;

	void Start () {
        player = FindObjectOfType<Player>();
	}

    private void Update() {
        if ( Input.GetAxis( "Debug skip and reset" ) > 0.0f ) {
            ResetRoom();
        }
    }

    public void ResetRoom() {
        Transform trans = contentContainer.transform;
        Destroy( contentContainer );
        contentContainer = Instantiate( contentContainerPrefab, trans.position, trans.rotation, transform );
    }

    private void OnTriggerEnter2D( Collider2D collision ) {
        if ( collision.gameObject.GetComponent<Player>() != null ) {
            player.resetPoint = playerResetPoint;
            Chaser[] chasers = contentContainer.GetComponentsInChildren<Chaser>( true );
            foreach ( Chaser chaser in chasers ) {
                chaser.gameObject.SetActive( true );
            }
        }
    }
}
