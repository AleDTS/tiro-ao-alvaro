﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;


// Essa classe comanda as acoes do inimigo
public class EnemyController : MonoBehaviour {

	public float speed = 5f;
	public float lifeTime;
	public GameObject GameController;

	private TrackableBehaviour mTrackableBehaviour;
	private GameController gameCtrl;
	private Rigidbody rgd;
	private EnemySpawner spw;
	private Vector3 destination = Vector3.zero;	
	private bool shouldMove = true;
	private float initTime;
	private float timeToDie = 0f;
	private Animation_Test anim;



	// Use this for initialization
	void Start () {


		speed = Random.Range (4f, 8f);
		GameController = GameObject.Find ("GameController");
		gameCtrl = GameController.GetComponent<GameController> ();
		anim = GetComponent<Animation_Test>();
		initTime = Time.time;
		lifeTime = Random.Range (5f, 8f);
		spw = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
		rgd = gameObject.GetComponent<Rigidbody> ();
		rgd.useGravity = false;
	}

	// Quando o inimigo for derrotado
	void Dead () 
	{
		Destroy (this.gameObject,2f);
		gameCtrl.OnDestroyEnemy ();
	}

	// Quando o inimigo fugir
	void Flee () 
	{
		Destroy (this.gameObject);
		gameCtrl.OnFleeEnemy ();
	}


	void LateUpdate () {
		if (!shouldMove)
			return;
		Mov ();
		// Se passar o tempo de fuga, o inimigo foge
		if (Time.time - initTime >= lifeTime) {
			Flee ();
		}
//		if (Input.GetKeyDown ("space")) {
//			Hit ();
//		}
			
	}

	// Quando o inimigo for acertado
	void Hit(){
		timeToDie = 2f;
		anim.DeathAni ();
		Debug.Log ("Hit");
		shouldMove = false;
		rgd.useGravity = true;
	}

	public void SetParam(float speed, float lifeTime){
		this.speed = speed;
		this.lifeTime = lifeTime;
	}

	// Colisoes do inimigo
	private void OnCollisionEnter(Collision coll){
		if (coll.gameObject.name == "FlareGun") {
			Physics.IgnoreCollision (
				gameObject.GetComponent<Collider> (),
				coll.gameObject.GetComponent<Collider> ());
		}
		if (coll.gameObject.tag == "Bullet") {
			Hit ();
		}
		if (coll.gameObject.name == "Ground") {
			//Debug.Log ("fall");
			Dead ();
		}
		if (coll.gameObject.tag == "Enemy") {
			Physics.IgnoreCollision (
				gameObject.GetComponent<Collider> (),
				coll.gameObject.GetComponent<Collider> ());
		}
	}
		
	private Vector3 GetDest(){
		return spw.RandomEdge (transform.position);
	}


	// Movimento aleatorio do inimigo
	private void Mov(){
		if (destination == transform.position || destination == Vector3.zero)
			destination = GetDest ();
		Debug.DrawLine (transform.position, destination);
		transform.position = 
			Vector3.MoveTowards (
				transform.position, destination,
				speed * Time.deltaTime);
	}
}