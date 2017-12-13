using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atirar : MonoBehaviour {
    public Rigidbody tiro;
    GameObject parede;
    private float velocidade = 1000;
    Vector3 pos;
    Quaternion ang;
    //dois bool foram criados de modo a transformar o retorno da função OnButtonPressed, que originalmente é contínua, para um formato de trigger
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
        {   //cria uma instância do projétil, que vai ser disparado
            Debug.Log("atirou");
            pos = transform.position;
            ang = transform.rotation;

            Rigidbody instance = (Rigidbody)Instantiate(tiro, pos, ang);
            //inicialmente, a instância tem como objeto pai o marcador da arma. Entretanto, isso faz com que suas coordenadas sejam calculadas
            //baseadas nas coordenadas desse marcador. Ao definir como filha do marcador da parede, o tiro se torna independente da arma após criado
            instance.transform.parent = parede.transform;
            limite = true;
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            instance.AddForce(forward * velocidade);

            Destroy(instance.gameObject, 2.5f);
        }
        //são permitidos apenas 2 tiros pro segundo
        new WaitForSeconds(0.5f);
        trava = true;
        limite = false;
    }
}
