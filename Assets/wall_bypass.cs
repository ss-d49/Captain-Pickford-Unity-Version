using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wall_bypass : MonoBehaviour {
	
	private GameObject wall_01;

	// Use this for initialization
	void Start () {
		wall_01 = GameObject.FindGameObjectWithTag("Wall");
		Physics2D.IgnoreCollision(wall_01.GetComponent<Collider2D>(), GetComponent<Collider2D>());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
