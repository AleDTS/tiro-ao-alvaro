using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atirar : MonoBehaviour {

    public Rigidbody tiro;
    private float velocidade = 700;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Rigidbody instance = (Rigidbody)Instantiate(tiro, transform.position, transform.rotation);
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            instance.AddForce(forward * velocidade);

            Destroy(instance.gameObject, 2.5f);
        }
    }
}
