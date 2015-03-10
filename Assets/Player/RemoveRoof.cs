using UnityEngine;
using System.Collections;

public class RemoveRoof : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider collider){

		//print (collider.gameObject.tag);

		if (collider.gameObject.tag == "Roof") {
				
			collider.gameObject.renderer.enabled = false;

		}

	}

	void OnTriggerExit(Collider collider){

		//print (collider.gameObject.tag);
		
		if (collider.gameObject.tag == "Roof") {

			collider.gameObject.renderer.enabled = true;

		}
		
	}
}
