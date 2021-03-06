﻿using UnityEngine;
using System.Collections;

public class player : MonoBehaviour {
	public float speed = 0f;
	public float rotatespeed = 10f;

	public int topSpeed = 100;

	public bool win = false;

	tackle tackleScript;

	private int attackMultiplier = 0;

	public bool oculusOn = false;
		
	public Vector3 direction;

	GameObject[] objects;

	Vector3 velocityTo;

	void Start(){

		objects = GameObject.FindGameObjectsWithTag("Pacdot");

	}
	
	void Update()
	{

		tackleScript = gameObject.GetComponent<tackle>();

		objects = GameObject.FindGameObjectsWithTag("Pacdot");

		if (objects.Length == 0) {

			win = true;
			networkView.RPC ("endGame", RPCMode.All, "Mario");

		} else {

			win = false;
			showEnd = false;

		}

		if (networkView.isMine)
		{

			if(!tackleScript.tackled){

				InputMovement();

			}else{

				print ("Frozen");

			}

		}
		else
		{
			SyncedMovement();
		}


	
	}
	
	private void SyncedMovement()
	{
		syncTime += Time.deltaTime;
		rigidbody.position = Vector3.Lerp(syncStartPosition, syncEndPosition, syncTime / syncDelay);

		rigidbody.rotation = syncRot;
	}
	
	void InputMovement()
	{

		direction = new Vector3(0,0,0);

		if (Input.GetKey (KeyCode.W)) {

			//rigidbody.MovePosition (rigidbody.position + Vector3.forward * speed/1000 * Time.deltaTime);
			direction += Vector3.forward;


		}

		
		if (Input.GetKey (KeyCode.S)) {

			//rigidbody.MovePosition(rigidbody.position - Vector3.forward * speed/1000 * Time.deltaTime);
			direction += Vector3.back;


		}


		if (Input.GetKey (KeyCode.D)) {


			direction += Vector3.right;


		}

		if (Input.GetKey (KeyCode.A)) {

		
			direction += Vector3.left;


		}

		if (!tackleScript.tackled) {


				//if (direction != new Vector3(0,0,0)){

					if (this.tag == "Mario") {

						//rigidbody.AddForce(direction.normalized * speed * Time.deltaTime);

						velocityTo.x = direction.x;
						velocityTo.z = direction.z;
						velocityTo.y = 0;
						
						// get the direction it must walk in:
						velocityTo = velocityTo.normalized;
						// convert from local to world space and multiply by horizontal speed:
						velocityTo = speed * velocityTo * Time.deltaTime * 10;
						// keep rigidbody vertical velocity to preserve gravity action:
						velocityTo.y = rigidbody.velocity.y;
						// set new rigidbody velocity:
						rigidbody.velocity = velocityTo;

					}else{


						rigidbody.AddRelativeForce(direction.normalized * speed * Time.deltaTime);


					}

			//	}

				if (rigidbody.velocity.magnitude > topSpeed) {
					
					rigidbody.velocity = rigidbody.velocity.normalized * topSpeed;
					

				}



		}

	}

	private float lastSynchronizationTime = 0f;
	private float syncDelay = 0f;
	private float syncTime = 0f;
	private Vector3 syncStartPosition = Vector3.zero;
	private Vector3 syncEndPosition = Vector3.zero;
	private Quaternion syncRot = Quaternion.identity;

	void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
	{
		Vector3 syncPosition = Vector3.zero;
		Vector3 syncVelocity = Vector3.zero;
		if (stream.isWriting)
		{
			syncPosition = rigidbody.position;
			stream.Serialize(ref syncPosition);
			
			syncVelocity = rigidbody.velocity;
			stream.Serialize(ref syncVelocity);

			syncRot = transform.rotation;
			stream.Serialize(ref syncRot);
		}
		else
		{
			stream.Serialize(ref syncPosition);
			stream.Serialize(ref syncVelocity);
			stream.Serialize(ref syncRot);

			
			syncTime = 0f;
			syncDelay = Time.time - lastSynchronizationTime;
			lastSynchronizationTime = Time.time;
			
			syncEndPosition = syncPosition + syncVelocity * syncDelay;
			syncStartPosition = rigidbody.position;
		}
	}

	public bool showEnd = false;
	public string winners = "Test"; 


	void OnGUI(){

		if (showEnd) {

			GUI.Button(new Rect(100, 100, 250, 100), winners);

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
