using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UFO : MonoBehaviour {

	private Rigidbody2D hero;
	// Use this for initialization
	void Start () {
		
		// pretty dirty way to do this, hehe
		hero = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
		Physics.IgnoreLayerCollision(8, 12, (hero.velocity.y > 0.0f));  

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
