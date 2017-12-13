using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Contact : MonoBehaviour {


	// Use this for initialization
	void Start () {
        
	}

    private void OnCollisionEnter(Collision collision)//ao colidir com inimigos, parede ou chão, o projétil deve ser destruído
    {
        if(collision.gameObject.tag == "Enemy")
            Destroy(this.gameObject);
        if(collision.gameObject.name == "Wall")
            Destroy(this.gameObject);
        if(collision.gameObject.name == "Ground")
            Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
