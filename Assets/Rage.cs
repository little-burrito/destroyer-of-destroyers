using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ RequireComponent( typeof( Health ) ) ]
[ DisallowMultipleComponent ]
public class Rage : MonoBehaviour {

    private Health health;
    public float rage;
    public float rageIndicatorFillTime = 1.0f;
    public Image rageIndicatorFill;

	void Start () {
        health = GetComponent<Health>();
	}
	
	void Update () {
        rage = Mathf.InverseLerp( health.maxHealth, 0.0f, health.health );
        float maxFillSpeed = Time.deltaTime / rageIndicatorFillTime;
        rageIndicatorFill.fillAmount = Mathf.Clamp( rage, rageIndicatorFill.fillAmount - maxFillSpeed, rageIndicatorFill.fillAmount + maxFillSpeed );
	}
}
