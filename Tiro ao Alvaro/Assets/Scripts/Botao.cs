﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class Botao : MonoBehaviour, IVirtualButtonEventHandler {

    GameObject arma;

	// Use this for initialization
	void Start () {
        arma = GameObject.FindGameObjectWithTag("Arma");
        VirtualButtonBehaviour[] vbs = GetComponentsInChildren<VirtualButtonBehaviour>();
        for (int i = 0; i < vbs.Length; i++)
        {
            vbs[i].RegisterEventHandler(this);
        }
            
    }
    public void OnButtonPressed(VirtualButtonBehaviour vbs)
    {
        var shot = arma.GetComponent<Atirar>();
        shot.safety(false);
        Debug.Log("foi");
    }

    public void OnButtonReleased(VirtualButtonBehaviour vbs)
    {
        
    }


    // Update is called once per frame
    void Update () {
		
	}

}
