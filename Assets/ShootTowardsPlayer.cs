using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ RequireComponent( typeof( Rigidbody2D ) ) ]
public class ShootTowardsPlayer : MonoBehaviour {

    private Rigidbody2D rb;
    private GameObject playerGameObject;
    public float speed = 25.0f;

	void Start () {
        rb = GetComponent<Rigidbody2D>();
        playerGameObject = FindObjectOfType<Player>().gameObject;
        Vector3 difference = playerGameObject.transform.position - transform.position;
        rb.velocity = difference.normalized * speed;
    }
}
