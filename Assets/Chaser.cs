using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaser : MonoBehaviour {

    private Player player;
    private Vector2 playerPredictedPosition;
    private Rigidbody2D playerRB;
    private Rigidbody2D rb;
    public float maxSpeed = 15.0f;
    public float acceleration = 5.0f;
    public Room roomToReset;
    // public float turnRate = 360.0f;

    void Awake () {
        player = FindObjectOfType<Player>();
        rb = GetComponent<Rigidbody2D>();
        playerRB = player.GetComponent<Rigidbody2D>();
        if ( roomToReset == null ) {
            roomToReset = GetComponentInParent<Transform>().GetComponentInParent<Transform>().GetComponentInParent<Room>();
        }
	}
	
	void Update () {
        if ( playerRB != null ) {
            playerPredictedPosition = ( Vector2 )player.transform.position + playerRB.velocity;
        } else {
            playerPredictedPosition = player.transform.position;
        }
        Vector2 difference = player.transform.position - transform.position;
        rb.AddForce( acceleration * difference.normalized, ForceMode2D.Force );
        if ( rb.velocity.magnitude > maxSpeed ) {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }

        rotateTowardsPlayer();
	}

    void rotateTowardsPlayer() {
        // Taken and re-written from http://answers.unity3d.com/questions/654222/make-sprite-look-at-vector2-in-unity-2d-1.html
        Vector2 difference = player.transform.position - transform.position;
        float angle = eulerDirectionFromVector( difference );
        transform.rotation = Quaternion.AngleAxis( angle, Vector3.forward );
    }

    private float eulerDirectionFromVector( Vector2 direction ) {
        float directionOffset = 0;
        float eulerDirection = Mathf.Atan2( direction.y, direction.x ) * Mathf.Rad2Deg + directionOffset;
        return eulerDirection;
    }

    private void OnCollisionEnter2D( Collision2D collision ) {
        if ( collision.collider.gameObject == player.gameObject ) {
            player.ResetPlayer();
            roomToReset.ResetRoom();
        }
    }
}
