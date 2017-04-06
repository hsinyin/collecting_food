using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFood : MonoBehaviour {
	[HideInInspector]
	public GameObject[] waypoints;
	private int currentWaypoint = 0;
	private float lastWaypointSwitchTime;
	[HideInInspector]
	public float speed;

	// Use this for initialization
	void Start () {
		lastWaypointSwitchTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		// get positions
		Vector3 startPosition = waypoints [currentWaypoint].transform.position;
		Vector3 endPosition = waypoints [currentWaypoint + 1].transform.position;
		// move gameObject 
		float pathLength = Vector3.Distance (startPosition, endPosition);
		float totalTimeForPath = pathLength / speed;
		float currentTimeOnPath = Time.time - lastWaypointSwitchTime;
		gameObject.transform.position = Vector3.Lerp (startPosition, endPosition, currentTimeOnPath / totalTimeForPath);
//		Debug.Log (gameObject.name+ " current position "+gameObject.transform.position + " end position"+ endPosition);
		// switch to next waypoint
		if (gameObject.transform.position.Equals(endPosition)) {	
			if (currentWaypoint < waypoints.Length - 2) {
				currentWaypoint++;
				lastWaypointSwitchTime = Time.time;
			} else {
//				Debug.Log (" Destroy "+gameObject.name);
				Destroy(gameObject);
			}
		}
	}
}
