using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnFood : MonoBehaviour {
	public GameObject[] waypoints;
	public GameObject foodPrefab;
	public Sprite[] foodSprites;
	public float interval = 3.0f;
	public float speed = 50.0f;
	private float lastFoodTime;

	// Use this for initialization
	void Start () {
		GenerateRandomeFood ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > lastFoodTime + interval) {
			GenerateRandomeFood ();
		}
	}

	void GenerateRandomeFood () {
		int randomIndex = Random.Range(0,foodSprites.Length);
		lastFoodTime = Time.time;
		GameObject food = Instantiate (foodPrefab);
		food.transform.SetParent(GameObject.FindGameObjectWithTag("Level").transform, false);
		food.GetComponent<Image>().sprite = foodSprites [randomIndex];
		food.name = foodSprites [randomIndex].name;
		food.GetComponent<MoveFood> ().waypoints = waypoints;
		food.GetComponent<MoveFood> ().speed = speed;
	}
}
