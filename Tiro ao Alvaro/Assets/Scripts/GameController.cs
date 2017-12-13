using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
	private int rounds = 10;
	private int shots = 0;

	private EnemySpawner spw;

	public Text textScore;
	public Text textLeft;

	// Use this for initialization
	void Start () {
		UpdateScore ();
		spw = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
		Spawn ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void GameOver(){
		Debug.Log ("Game Over!");
		//UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
	}

	void Win(){
		Debug.Log ("You win!");
		//UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
	}

	void EndGame(){
		if (shots > (int)rounds / 2) {
			Win ();
		} else {
			GameOver ();
		}
	}

	public void OnFleeEnemy(){
		Check ();
	}
			
	public void OnDestroyEnemy(){
		Shot ();
		Check ();
	}

	void Shot(){
		shots += 1;
	}

	void Check(){
		rounds -= 1;
		//Debug.Log (rounds);
		//Debug.Log (shots);
		UpdateScore();
		if (rounds == 0)
			EndGame ();
		else
			Invoke ("Spawn", 2f);
		
	}

	void UpdateScore(){
		textScore.text = "Score: " + shots;
		textLeft.text = "Enemies left: "+ rounds;
	}

	void Spawn(){
		spw.SpawnEnemy ();
	}
}
