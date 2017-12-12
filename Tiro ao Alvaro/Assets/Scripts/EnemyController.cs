using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
	
	public float speed = 0.1f;
	public float lifeTime;

	private Rigidbody rgd;
	private EnemySpawner spw;
	private Vector3 destination = Vector3.zero;	
	private bool shouldMove = true;
	private float initTime;
	private float timeToDie = 0f;

	// Use this for initialization
	void Start () {
		initTime = Time.time;
		lifeTime = Random.Range (5f, 8f);
		spw = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
		rgd = gameObject.GetComponent<Rigidbody> ();
		rgd.useGravity = false;
	}

	void OnEnable () 
	{
		EventManager.StartListening ("DestroyEnemy", DestroyEnemy);
	}

	void OnDisable () 
	{
		EventManager.StopListening ("DestroyEnemy", DestroyEnemy);
	}

	void DestroyEnemy () 
	{
		EventManager.StopListening ("DestroyEnemy", DestroyEnemy);
		StartCoroutine (DestroyNow());
	}

	// Update is called once per frame
	void LateUpdate () {
		EdgeMov ();
	}

	public void SetParam(float speed, float lifeTime){
		this.speed = speed;
		this.lifeTime = lifeTime;
	}

	public void EdgeMov(){
		if (shouldMove)
			Mov ();
		if (Time.time - initTime >= lifeTime) {
			Bye (0f);
		}
	}

	private void OnCollisionEnter(Collision coll){
		if (coll.gameObject.tag == "Bullet") {
			shouldMove = false;
			rgd.useGravity = true;
			Debug.Log ("Good shot!");
		}
		if (coll.gameObject.name == "Ground") {
			Debug.Log ("fall");
			Bye (2f);
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

	private void Mov(){
		if (destination == transform.position || destination == Vector3.zero)
			destination = GetDest ();
		Debug.DrawLine (transform.position, destination);
		transform.position = 
			Vector3.MoveTowards (
				transform.position, destination,
				speed * Time.deltaTime);
	}

	private void Bye(float time){
		timeToDie = time;
		EventManager.TriggerEvent ("DestroyEnemy");
	}

	private IEnumerator DestroyNow(){
		yield return null;
		Destroy (this.gameObject, this.timeToDie);
	}
		
}
