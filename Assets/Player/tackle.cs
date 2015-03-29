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

			
			networkView.RPC("endGame", RPCMode.All, "Chasers");
			//endGame("Toads");

		}
		
	}

	void StartTimer(float time)
	{
		timing = true;
		countdown = time;
	}

	//variables to control the end GUI 

	public bool showEnd;
	public string winners = "Test"; 
	

	void OnGUI(){
		
		if (showEnd) {

			
			GUI.Button(new Rect(100, 100, 250, 100), winners + "Win!");
		}
		
	}	
	
	[RPC]
	void endGame(string Winners) {
		
		print (Winners + " Win!");
		
		winners = Winners;
		showEnd = true;

		NetworkManager playerScript;

		playerScript = GameObject.FindWithTag("MainCamera").GetComponent<NetworkManager> ();

		playerScript.winners = winners;

		playerScript.showEnd = true;

		//GUI.Button (new Rect (100, 100, 250, 100), Winners + " Win!");
		
		//GUI.TextField(new Rect (100, 100, 250, 100), Winners, 0, "Label");
		
	}

}


