using UnityEngine;
using System.Collections;

public class tackle : MonoBehaviour {

	public int speed = 0;

	public bool tackled = false;

	bool timing = false;

	public int recoverTime;

	float countdown = 0;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		//if (gameObject.tag != "Mario") {

			if (!tackled) {

				if (Input.GetKey (KeyCode.E)) {

					rigidbody.AddRelativeForce (Vector3.forward * speed * Time.deltaTime);

					tackled = true;

					StartTimer(recoverTime);


				}

			}

		//}


		if(timing)
		{
			countdown -= Time.deltaTime;
			if(countdown <= 0)
			{
				tackled = false;
				timing = false;
			}
		}

	}

	void OnCollisionEnter(Collision collision) {

		//print (collision.gameObject.tag);

		//print (collision.gameObject.name);

	
		if (collision.gameObject.tag == "Mario" && tackled) {


			print ("Collided");

			
			networkView.RPC("endGame", RPCMode.Others, "Chasers");
			endGame("Toads");

		}
		
	}

	void StartTimer(float time)
	{
		timing = true;
		countdown = time;
	}

	//variables to control the end GUI 
	public bool showGUI;
	public string winners = "Test"; 
	[RPC]
	void endGame(string Winners) {
		//sasses the string on to the function 
		winners = Winners;
		print (Winners + " Win!");
		showGUI = true;
		
	}
	//displays who one after game is over 
	void OnGUI () {
		if (showGUI) {
			GUI.Button (new Rect (100, 100, 250, 100), winners + " Win!");
		}
	}
	

}


