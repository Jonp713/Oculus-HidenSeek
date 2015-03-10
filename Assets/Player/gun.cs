using UnityEngine;
using System.Collections;

public class gun : MonoBehaviour {

	public GameObject bullet;

	Object bulletgo;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKey (KeyCode.Space))


			bulletgo = Network.Instantiate (bullet, this.rigidbody.position, Quaternion.identity, 0);


	}
}
