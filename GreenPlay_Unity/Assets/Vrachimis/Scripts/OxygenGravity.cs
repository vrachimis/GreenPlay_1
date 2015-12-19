﻿using UnityEngine;
using System.Collections;

public class OxygenGravity : MonoBehaviour {

	public GameObject gravityObject;
	public float acceleration = 9.81f;

	private Transform lastPosition;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		//add gravity
		GetComponent<Rigidbody2D>().gravityScale = -acceleration;
	}

	void FixedUpdate() {
		RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, 2f);
		if (hit.collider != null) {
			//Debug.DrawLine(transform.position, transform.position,Color.blue);
		}
	}
}
