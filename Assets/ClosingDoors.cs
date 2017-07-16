using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ RequireComponent( typeof( AudioSource ) ) ]
public class ClosingDoors : MonoBehaviour {

    private Player player;
    public float closedAmount = 0.0f;
    public float closingTimeWindow = 1.0f;
    private float closingTimer = 0.8f;
    public float timeBeforeReset = 1.0f;
    public float resetTimer;
    private bool shouldReset = false;
    public bool isClosingDoors = false;
    public bool playerIsThrough = false;
    public GameObject[] doorObjects;
    public Room room;
    private AudioSource audioSource;

	void Awake () {
        player = FindObjectOfType<Player>();
        closingTimer = closingTimeWindow;
        room = GetComponentInParent<Transform>().GetComponentInParent<Room>();
        audioSource = GetComponent<AudioSource>();
        if ( room == null ) {
            Debug.LogError( name + " HAS NO ROOM SET" );
        }
	}
	
	void Update () {
        closeDoors();
        handleDoorsClosed();
	}

    void closeDoors() {
        if ( isClosingDoors ) {
            closingTimer -= Time.deltaTime;
            closedAmount = Mathf.InverseLerp( closingTimeWindow, 0.0f, closingTimer );
            if ( closedAmount < 1.0f ) {
                if ( !audioSource.isPlaying ) {
                    audioSource.Play();
                }
            } else {
                audioSource.Stop();
            }
        } else {
            audioSource.Stop();
        }

        for ( int i = 0; i < doorObjects.Length; i++ ) {
            float closedAmountEquivalent = Mathf.InverseLerp( 0, doorObjects.Length, i );
            if ( doorObjects[ i ] != null ) {
                if ( closedAmountEquivalent <= closedAmount ) {
                    doorObjects[ i ].SetActive( true );
                } else {
                    doorObjects[ i ].SetActive( false );
                }
            }
        }
    }

    void handleDoorsClosed() {
        if ( closedAmount >= 1.0f ) {
            if ( !playerIsThrough ) {
                if ( !shouldReset ) {
                    shouldReset = true;
                    resetTimer = timeBeforeReset;
                } else {
                    resetTimer -= Time.deltaTime;
                }

                if ( resetTimer <= 0.0f ) {
                    player.ResetPlayer();
                    room.ResetRoom();
                }
            }
        }
    }

    public void startClosingDoors() {
        isClosingDoors = true;
    }

    private void OnTriggerEnter2D( Collider2D collision ) {
        if ( collision.gameObject.GetComponent<Player>() != null ) {
            playerIsThrough = true;
        }
    }
}
