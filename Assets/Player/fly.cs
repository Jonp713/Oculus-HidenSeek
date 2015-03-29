using UnityEngine;
using System.Collections;

public class fly : MonoBehaviour {

	public GameObject oculusLeftEye;
	Rigidbody rr;
	
	public float forwardSpeed = 100f;
	public float rotationSpeed = 8f;

	public int topSpeed = 100;
	
	public bool win = false;

	bool corrected;

	Vector3 axis;

	public Vector3 direction = new Vector3(0,0,0);
	
	float rotationY;
	float rotationX;
	float rotationZ;

	Vector3 speed;

	float factor;

	tackle tackleScript;

	bool start = false;
	
	Vector3 temp;

	public OVRPlayerController ovrdevice ;

	void Start () {

		if (!networkView.isMine) {
			
			//GameObject rig = GameObject.Find("OVRCameraRig");

			GameObject pcon = GameObject.Find("OVRPlayerController");

			//rig.SetActive(false);
			pcon.SetActive(false);

		}

		//ovrdevice = GameObject.Find(OVRPlayerController).GetComponent<OVRPlayerController>() ;

		tackleScript = gameObject.GetComponent<tackle>();

	}


	//Vector3 vel;

	
	// Update is called once per frame
	void Update () {



	
		if (networkView.isMine)
		{

				
			InputMovement();
							
		}
		else
		{
			SyncedMovement();
		}
			

		/*FlightMode();

		vel = rigidbody.velocity;
		
		vel.y = 0;
		
		rigidbody.velocity = vel;
		print (rigidbody.velocity);	

*/

	}



	private void SyncedMovement()
	{
		syncTime += Time.deltaTime;
		rigidbody.position = Vector3.Lerp(syncStartPosition, syncEndPosition, syncTime / syncDelay);
		
		rigidbody.rotation = syncRot;
	}

	void InputMovement(){
		
		//get rotation values for the leftEye
		//rotationX = oculusLeftEye.transform.localRotation.x / 2;
		rotationY = oculusLeftEye.transform.localRotation.y / 2;
		//rotationZ = oculusLeftEye.transform.localRotation.z;
		
		//put them into a vector
		//axis = new Vector3 (0, rotationY, 0);
		
		//Rotate
		//transform.Rotate (axis * Time.deltaTime * rotationSpeed);

		//Debug.Log (rotationY);

		if (Input.GetKey (KeyCode.Space)) {

			corrected = true;

			/*

			//

			if (rotationY > .10) {

					factor = (float)((rotationY - .10) * 14);


					transform.Rotate (Vector3.up * Time.deltaTime * rotationSpeed * factor);
			}
			if (rotationY < -.10) {


					factor = (float)((rotationY + .10) * -14);


					transform.Rotate (Vector3.down * Time.deltaTime * rotationSpeed * factor);

			}

			*/

		}

		if (corrected && !Input.GetKey (KeyCode.Space)) {

			//ovrdevice.ResetOrientation();

			corrected = false;

		}



		direction = new Vector3(0,0,0);
		
		//rotation = player.transform.rotation;


		temp = new Vector3 (0,0,0);
		temp.x = oculusLeftEye.transform.forward.x;
		temp.z = oculusLeftEye.transform.forward.z;

		
		if (Input.GetKey (KeyCode.W)) {


			direction += temp;


			//rigidbody.velocity = temp * forwardSpeed;

		}
		if (Input.GetKey (KeyCode.S)) {


			direction += -temp;

			
			//rigidbody.velocity = -temp * forwardSpeed;
			
		}
		if (Input.GetKey (KeyCode.A)) {

			direction += -oculusLeftEye.transform.right;

			
			//rigidbody.velocity = -oculusLeftEye.transform.right * forwardSpeed;
			
		}
		if (Input.GetKey (KeyCode.D)) {

			direction += oculusLeftEye.transform.right;
			
			//rigidbody.velocity = oculusLeftEye.transform.right * forwardSpeed;
			
		}

		

		
		//rigidbody.AddForce(direction.normalized * Time.deltaTime);

		if (direction != new Vector3 (0, 0, 0) && tackleScript.tackled == false) {


			//rigidbody.AddForce(direction.normalized * forwardSpeed * Time.deltaTime);


			speed.x = direction.x;
			speed.z = direction.z;
			speed.y = 0;
			
			// get the direction it must walk in:
			speed = speed.normalized;
			// convert from local to world space and multiply by horizontal speed:
			speed = forwardSpeed * speed * Time.deltaTime * 10;
			// keep rigidbody vertical velocity to preserve gravity action:
			speed.y = rigidbody.velocity.y;
			// set new rigidbody velocity:
			rigidbody.velocity = speed;

		}

		if (rigidbody.velocity.magnitude > topSpeed) {


			rigidbody.velocity = rigidbody.velocity.normalized * topSpeed;
			
			
		}

		//print(rigidbody.velocity);





		
	}﻿


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

	
}
