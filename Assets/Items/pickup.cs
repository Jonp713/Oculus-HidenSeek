﻿using UnityEngine;
using System.Collections;

public class pickup : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider collision){

		if (collision.gameObject.tag == "Mario")
		{
			Destroy (gameObject);
		}
	}
}
