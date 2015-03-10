using UnityEngine;
using System.Collections;

public class gravity : MonoBehaviour {

	public int gravityInt;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	

		rigidbody.AddForce (Vector3.down * Time.deltaTime * gravityInt);

	}
}
