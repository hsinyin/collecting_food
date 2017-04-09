using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFood : MonoBehaviour {
	public enum Direction {UP, DOWN, LEFT, RIGHT};

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
		Direction direction = getDirection (startPosition, endPosition);
		// move gameObject 
		float pathLength = Vector3.Distance (startPosition, endPosition);
		float totalTimeForPath = pathLength / speed;
		float currentTimeOnPath = Time.time - lastWaypointSwitchTime;
		gameObject.transform.position = Vector3.Lerp (startPosition, endPosition, currentTimeOnPath / totalTimeForPath);
//		Debug.Log (gameObject.name+ " current position "+gameObject.transform.position + " end position"+ endPosition);
		// switch to next waypoint
		if (isPassingPoint(gameObject.transform.position, endPosition, direction)) {	
			if (currentWaypoint < waypoints.Length - 2) {
				currentWaypoint++;
				lastWaypointSwitchTime = Time.time;
			} else {
//				Debug.Log (" Destroy "+gameObject.name);
				Destroy(gameObject);
			}
		}
	}

	Direction getDirection(Vector3 start, Vector3 end){
		float deltaX = end.x - start.x;
		float deltaY = end.y - start.y;
		if (Mathf.Abs (deltaX) > Mathf.Abs (deltaY)) {
			if (deltaX > 0) {
				return Direction.RIGHT;
			} else {
				return Direction.LEFT;
			}
		} else {
			if (deltaY > 0) {
				return Direction.UP;
			} else {
				return Direction.DOWN;
			}
		}
	}

	bool isPassingPoint(Vector3 currentPoint, Vector3 targetPosition, Direction direction){
		switch(direction){
			case Direction.DOWN:
				return currentPoint.y <= targetPosition.y;
		case Direction.UP:
			return currentPoint.y >= targetPosition.y;
		case Direction.RIGHT:
			return currentPoint.x >= targetPosition.x;
		case Direction.LEFT:
			return currentPoint.x <= targetPosition.x;
			default:
				return false;
		}
	}
}
