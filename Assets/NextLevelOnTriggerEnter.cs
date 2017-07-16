﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelOnTriggerEnter : MonoBehaviour {
    private void OnTriggerEnter2D( Collider2D collision ) {
        if ( collision.gameObject.GetComponent<Player>() != null ) {
            Application.LoadLevel( Application.loadedLevel + 1 );
        }
    }
}
