using UnityEngine;
using System.Collections;

public class PlayerFollow : MonoBehaviour {
	public GameObject player;
	private bool setup = false; 
	public float rotateSpeed = 1000f;
	
	public bool oculusOn = false;

	Quaternion rotation;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (player != null) {
			if (player.tag == "Mario") {
				if (!setup) {

					gameObject.transform.Rotate(90, 0, 0);
					camera.fieldOfView = 80;
					
					setup = true;
					
				}

				gameObject.transform.position = new Vector3 (player.transform.position.x, player.transform.position.y + 2.8f, player.transform.position.z);
				
				
			} else {
				if (!setup) {
					gameObject.transform.Rotate(-90, 0, 0);
					camera.fieldOfView = 50;
					
					setup = true;
				}

				if(!oculusOn){


					if(Input.mousePosition.x < Screen.width/3){

						player.transform.Rotate (Vector3.down * Time.deltaTime * rotateSpeed * -(Input.mousePosition.x - Screen.width/3)/2 );	
						 
					}

					if(Input.mousePosition.x > Screen.width - Screen.width/3){

						player.transform.Rotate (Vector3.up * Time.deltaTime * rotateSpeed * -(Screen.width - Screen.width/3 - Input.mousePosition.x)/2 );
					
					}


					rotation = player.transform.rotation;


					//Quaternion tempRotation = rotation;

					//tempRotation.x = player.transform.rotation.x;
					//tempRotation.z = player.transform.rotation.z;


					//player.transform.rotation = tempRotation;

				}else{

					float x = transform.rotation.x + (Input.mousePosition.x/2.5f - Screen.width/2) * 0.5f;
					float y = transform.rotation.y - (Input.mousePosition.y/2.5f - Screen.height/2) * 0.5f;
					rotation = Quaternion.Euler(y, x, 0);
				}

				transform.rotation = rotation;

				gameObject.transform.position = new Vector3 (player.transform.position.x, player.transform.position.y + 0.2f, player.transform.position.z);
			}
		}
		if (Input.GetKeyDown(KeyCode.Q)) {
			if (player.tag == "Player") {
				player.tag = "Mario";
				setup = false;
			} else if (player.tag == "Mario") {
				player.tag = "Player";
				setup = false;
			}
		}  
	}
	
}