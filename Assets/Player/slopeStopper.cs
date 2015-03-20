using UnityEngine;
using System.Collections;

public class slopeStopper : MonoBehaviour {

	public bool onSlope = false;

	fly flyScript;

	player moveScript;

	gravity Gravity;


	Vector3 checker;
	
	public GameObject flyer;


	// Use this for initialization
	void Start () {

		if (flyer.tag == "Mario") {

			moveScript = flyer.GetComponent<player> ();

		}

		if (flyer.tag == "Toad") {

			flyScript = flyer.GetComponent<fly>();

		}
		Gravity = flyer.GetComponent<gravity>();

	}
	
	// Update is called once per frame
	void Update () {

		if (flyer.tag == "Mario") {

			checker = moveScript.direction;

		}

		if (flyer.tag == "Toad") {

			checker = flyScript.direction;

		}

		if (onSlope && checker == new Vector3 (0, 0, 0)) {

			flyer.rigidbody.velocity = Vector3.zero;
			//print ("helloe, im in");
			Gravity.enabled = false;

		} else {


			if(onSlope){

				//print ("onSlope");



			}else{
			//	print ("not in");

			}


		}
	
	}

	void OnTriggerEnter(Collider collision){

		//print ("Tiggered");


		if (collision.gameObject.tag == "Slope") {

			onSlope = true;

			//print ("onSlope tiggered");


		}





	}

	void OnTriggerExit(Collider collision){
		
		if (collision.gameObject.tag == "Slope") {
			
			onSlope = false;


			Gravity.enabled = true;

			//print ("ITS BACK");

		}
		
		
		
	}
}
