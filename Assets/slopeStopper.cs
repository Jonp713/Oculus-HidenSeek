using UnityEngine;
using System.Collections;

public class slopeStopper : MonoBehaviour {

	bool onSlope = false;

	fly flyScript;

	gravity Gravity;

	
	public GameObject flyer;


	// Use this for initialization
	void Start () {
	

		flyScript = flyer.GetComponent<fly>();
		Gravity = flyer.GetComponent<gravity>();

	}
	
	// Update is called once per frame
	void Update () {


		if (onSlope && flyScript.direction == new Vector3 (0, 0, 0)) {

			flyer.rigidbody.velocity = Vector3.zero;
			print ("helloe, im in");
			Gravity.enabled = false;

		} else {


			if(onSlope){

				print ("onSlope");



			}else{
				print ("not in");

			}


		}
	
	}

	void OnTriggerEnter(Collider collision){

		print ("Tiggered");


		if (collision.gameObject.tag == "Slope") {

			onSlope = true;

			print ("onSlope tiggered");


		}





	}

	void OnTriggerExit(Collider collision){
		
		if (collision.gameObject.tag == "Slope") {
			
			onSlope = false;


			Gravity.enabled = true;

		}
		
		
		
	}
}
