using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosingDoorsStartTrigger : MonoBehaviour {

    public ClosingDoors closingDoors;

    private void OnTriggerEnter2D( Collider2D collision ) {
        if ( collision.gameObject.GetComponent<Player>() != null ) {
            closingDoors.startClosingDoors();
        }
    }
}
