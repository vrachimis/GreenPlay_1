﻿using UnityEngine;
using System.Collections;

public class CupFilterMovement : MonoBehaviour {


	public GameObject center;
	public Transform[] wayPointList;
	public Transform exitWaypoint;
	public int currentWayPoint = 0; 
	public float speed = 4f;
	public float turningRate = 30f;
	public float arrivalDistance = 0.1f;

	public GameManager gameManager;

	Transform targetWayPoint;
	bool isStop = false;

	private Rigidbody2D rb2d;
	private float timer;
	private float ang = 0f;
	float lastDistanceToTarget = 0f;
	private Vector3 direction;
	private float angle;
	private bool stopWaypoints;
	private bool doesCollide;
	private int destroyed = 0;
	private bool isIncrement = false;


	// Use this for initialization
	void Start () {

		for (int x = 1; x <= 8; x++) {
			wayPointList[x-1] = GameObject.Find("Waypoint " + x).transform;
		}
		center = GameObject.Find("circleTree");
		exitWaypoint = GameObject.Find("Exit Waypoint").transform;

		stopWaypoints = false;
		doesCollide = false;
		rb2d  = GetComponent<Rigidbody2D>();
		//invokeRepeating
		InvokeRepeating("incrementSpeed",20,15);

	}

	// Update is called once per frame
	void Update () {

		direction = center.transform.position - transform.position;
		ang = Vector2.Angle(rb2d.transform.position, direction);

		if ((transform.position.y < 0) && (transform.position.x > -0.65f) && (transform.position.x < 1.5f)) //rotate towards the tree in the center
		{

			Vector3 cross = Vector3.Cross(rb2d.transform.position, direction);

			if (cross.z > 0) ang = 360 - ang;
			//rb2d.MoveRotation(ang);
			transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0,0,ang), 1000 * Time.deltaTime);

		} 
		else if ((transform.position.y > 0) && (transform.position.x < -1.5f)) {
			stopWaypoints = true;
			isIncrement = true;
		}
		else {
			//RotateObject(Vector3.down);
			//rb2d.MoveRotation(0);
			transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0,0,0), 1000 * Time.deltaTime);
		}

		//NOT SURE IF THIS IS NEEDED. TEST LATER
		//transform.rotation = Quaternion.RotateTowards(transform.rotation, _targetRotation, turningRate * Time.deltaTime);

		//touchInput();
		// check if there is a waypoint in the list
		if(currentWayPoint < this.wayPointList.Length)
		{
			if(targetWayPoint == null)
			{
				targetWayPoint = wayPointList[currentWayPoint];
				//Debug.Log("Waypoint was empty. Object: " + gameObject.name);
			}
			if (stopWaypoints == false) {
				walk();
			}
			else if (stopWaypoints && isIncrement) {
				exit();
				isIncrement = false;
			}
		}
	}

	void walk()
	{
		//If we're close to target, or overshot it, get next waypoint;
		float distanceToTarget = Vector3.Distance(transform.position, targetWayPoint.position);
		if((distanceToTarget < arrivalDistance) || (distanceToTarget > lastDistanceToTarget))
		{
			currentWayPoint++;
			if(currentWayPoint >= wayPointList.Length)
			{
				currentWayPoint = 0;
			}
			targetWayPoint = wayPointList[currentWayPoint];
			lastDistanceToTarget = Vector3.Distance(transform.position, targetWayPoint.position);
			//Debug.Log("Added the next waypoint(" + currentWayPoint + "). Object: " + gameObject.name);
		}
		else {
			lastDistanceToTarget = distanceToTarget;
		}

		//Get direction to the waypoint.
		//Normalize so it doesn't change with distance.
		Vector3 dir = (targetWayPoint.position - transform.position).normalized;

		//(speed * Time.fixedDeltaTime) makes the object move by 'speed' units per second, framerate independent
		rb2d.MovePosition(transform.position + dir * (speed * Time.fixedDeltaTime));

	}

	void exit()
	{
		Vector3 direct = (exitWaypoint.position - transform.position).normalized;
		rb2d.MovePosition(transform.position + direct * (speed * Time.fixedDeltaTime));
		destroyed = 7 - GameObject.FindGameObjectsWithTag("Cup").Length;
		//transform.Translate(new Vector3(-5,0,0) * Time.deltaTime);
		if (transform.position.x < -4.8f && destroyed < 6) {
			Destroy(transform.parent.gameObject);
		}
	}

	void incrementSpeed()
	{
		speed += 0.1f;
	}
}