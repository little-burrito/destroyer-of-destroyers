using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypeText : MonoBehaviour {
    public Text text;
    public float timeBetweenCharacters;
    public string[] texts;
    public float[] timeAfterTexts;
    private float timeAfterTextsTimer = 0.0f;
    private float timeBetweenCharactersTimer = 0.0f;
    private int currentText = 0;
    private string currentTextString = "";
    public bool changeLevel = true;
	
	void Update () {
        timeBetweenCharactersTimer -= Time.deltaTime;
        timeAfterTextsTimer -= Time.deltaTime;
        Debug.Log( timeBetweenCharactersTimer );
        Debug.Log( timeAfterTextsTimer );
        Debug.Log( currentText );
        Debug.Log( currentTextString );
        if ( timeBetweenCharactersTimer <= 0.0f && timeAfterTextsTimer <= 0.0f ) {
            if ( currentTextString.Length < texts[ currentText ].Length ) {
                currentTextString = texts[ currentText ].Substring( 0, currentTextString.Length + 1 );
                text.text = currentTextString;
                timeBetweenCharactersTimer = timeBetweenCharacters;
            } else {
                resetTimer();
            }
        }

        if ( Input.GetAxis( "Debug skip and reset" ) > 0.0f ) {
            if ( changeLevel ) {
                Application.LoadLevel( Application.loadedLevel + 1 );
            }
        }
	}

    void resetTimer() {
        if ( currentText >= texts.Length - 1 ) {
            if ( changeLevel ) {
                Application.LoadLevel( Application.loadedLevel + 1 );
            }
        } else {
            timeAfterTextsTimer = timeAfterTexts[ currentText ];
            currentTextString = "";
        }
        currentText++;
    }
}
