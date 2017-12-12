using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	public GameObject enemyPrefab;
	public Transform wall;
	public int numberOfEnemies = 1;
	public float WallDistance = 1;
	public float MinHeightDistance = 1;

	private float minX, maxX, minY, maxY, z;
	private List<string> edges = new List<string> ();
	private UnityAction spawnListener;

	void Awake(){
		spawnListener = new UnityAction (SpawnEnemy);
		edges.Add ("up");
		edges.Add ("down");
		edges.Add ("left");
		edges.Add ("right");
		Bounds bounds = wall.GetComponent<Renderer> ().bounds;
		minX = bounds.min.x;
		maxX = bounds.max.x;
		minY = bounds.min.y + MinHeightDistance;
		maxY = bounds.max.y;
		z = wall.position.z - WallDistance;
	}

	// Use this for initialization
	void Start () {
		
	}

	void OnEnable () {
		EventManager.StartListening ("SpawnEnemy", spawnListener);
	}

	void OnDisable () {
		EventManager.StopListening ("SpawnEnemy", spawnListener);
	}

	void SpawnEnemy(){
		Quaternion spawnRotation = 
			Camera.main.transform.rotation;
		Vector3 spawnPosition = 
			RandomEdge (Vector3.zero);
		
		Instantiate(enemyPrefab, spawnPosition, spawnRotation);
	}

	private bool RandomBool(){
		return (Random.value >= 0.5);
	}

	public Vector3 RandomEdge(
		Vector3 pos){
		Vector3 position = Vector3.zero;

		List<string> edges = new List<string>(this.edges);

		if (pos.x == maxX) {
			edges.Remove ("right");
		} else if (pos.x == minX) {
			edges.Remove ("left");
		} else if (pos.y == minY) {
			edges.Remove ("down");
		} else if (pos.y == maxY) {
			edges.Remove ("up");
		}

		int i = (int)Mathf.Round (Random.Range (0, edges.Count));

		switch(edges[i]){
		case "up" :
			position = new Vector3 (
				Random.Range(minX,maxX),
				maxY,z);
			break;
		case "down" :
			position = new Vector3 (
				Random.Range(minX,maxX),
				minY,z);
			break;
		case "left" :
			position = new Vector3 (
				minX,
				Random.Range(minY,maxY),z);
			break;
		case "right" :
			position = new Vector3 (
				maxX,
				Random.Range(minY,maxY),z);
			break;
		}
		return position;
	}
		
}