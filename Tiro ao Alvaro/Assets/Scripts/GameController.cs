using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// Essa classe comanda a logica do jogo em si
public class GameController : MonoBehaviour {
	private int rounds = 10;
	private int shots = 0;

	private EnemySpawner spw;
	private bool over = false;

	public Text textScore;
	public Text textLeft;
	public Text gameOver;

	public void StartGame(){
		
		gameOver.text = "";
		UpdateScore ();
		Spawn ();
	}


	// Use this for initialization
	 void Start () {
		spw = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("up"))
			EndGame ();
	}

	void Reload(){
		UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
	}

	void GameOver(){
		gameOver.text = "Game Over!";
		over = true;
	}

	void Win(){
		gameOver.text = "Você venceu!";
	}

	void EndGame(){
		if (shots > (int)rounds / 2) {
			Win ();
		} else {
			GameOver ();
		}
		Invoke ("Reload", 10f);
	}

	// Funcao chamada quando o inimigo eh derrotado
	public void OnFleeEnemy(){
		if (over)
			return;
		Check ();
	}

	// Funcao chamada quando o inimigo eh derrotado
	public void OnDestroyEnemy(){
		if (over)
			return;
		Shot ();
		Check ();
	}

	void Shot(){
		shots += 1;
	}


	// Checa estado do jogo
	void Check(){
		rounds -= 1;
		//Debug.Log (rounds);
		//Debug.Log (shots);
		UpdateScore();
		if (rounds == 0)
			EndGame ();
		else
			Invoke ("Spawn", Random.Range(2f,3f));
		
	}
		
	void UpdateScore(){
		textScore.text = "Score: " + shots;
		textLeft.text = "Enemies left: "+ rounds;
	}

	public void Spawn(){
		if (over)
			return;
		spw.SpawnEnemy ();
	}
}
