using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atirar : MonoBehaviour {

    public Rigidbody tiro;
    private float velocidade = 700;
    Vector3 pos;
    Quaternion ang;
    public bool trava = true;

    // Use this for initialization
    void Start () {
    }

    public void safety(bool x)
    {
        trava = x;
    }
	
	// Update is called once per frame
	void Update () {
        
        if (!trava)
        {
            Debug.Log("atirou");
            pos = transform.position;
            ang = transform.rotation;

            Rigidbody instance = (Rigidbody)Instantiate(tiro, pos, ang);
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            instance.AddForce(forward * velocidade);

            Destroy(instance.gameObject, 2.5f);
        }
    }
}
