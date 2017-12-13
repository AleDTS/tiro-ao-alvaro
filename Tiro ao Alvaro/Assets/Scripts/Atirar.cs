using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atirar : MonoBehaviour {
    public Rigidbody tiro;
    GameObject parede;
    private float velocidade = 700;
    Vector3 pos;
    Quaternion ang;
    public bool trava = true;
    public bool limite = false;

    // Use this for initialization
    void Start () {
        parede = GameObject.FindGameObjectWithTag("Parede");
    }

    public void safety(bool x)
    {
        if (!limite)
        {
            trava = x;
            limite = true;
        }
        
    }
	
	// Update is called once per frame
	void Update () {

        if (!trava)
        {
            Debug.Log("atirou");
            pos = transform.position;
            ang = transform.rotation;

            Rigidbody instance = (Rigidbody)Instantiate(tiro, pos, ang);
            instance.transform.parent = parede.transform;
            limite = true;
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            instance.AddForce(forward * velocidade);

            Destroy(instance.gameObject, 2.5f);
        }
        new WaitForSeconds(0.5f);
        trava = true;
        limite = false;
    }
}
