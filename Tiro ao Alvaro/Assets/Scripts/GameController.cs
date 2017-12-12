using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		EventManager.TriggerEvent ("SpawnEnemy");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnEnable ()
	{
		EventManager.StartListening ("DestroyEnemy", OnDestroyEnemy);
	}

	void OnDisable ()
	{
		EventManager.StopListening ("DestroyEnemy", OnDestroyEnemy);
	}
			
	void OnDestroyEnemy(){
		StartCoroutine(Spawn());
	}

	IEnumerator Spawn(){
		yield return new WaitForSeconds(1f);
		EventManager.TriggerEvent ("SpawnEnemy");
	}
}
