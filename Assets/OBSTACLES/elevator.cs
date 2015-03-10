using UnityEngine;
using System.Collections;

public class elevator : MonoBehaviour {

	bool on = false;

	public int floor = 0;

	public GameObject elevatortomove;

	public float speed = 10;

	public int f1height = 0;

	public int f2height = 8;


	// Use this for initialization
	void Start () {
			
	}
	
	// Update is called once per frame
	void Update () {

		if (on) {

			int endy;
			Vector3 target = elevatortomove.transform.position;

			switch (floor) {

				case 0:
					endy = f2height;
				break;
				case 1:

					endy = f1height;
					

				break;
				default:


					endy = f1height;

				break;
	


			}
	
	
			target.y = endy;

			float step = speed * Time.deltaTime;
			elevatortomove.transform.position = Vector3.MoveTowards(elevatortomove.transform.position, target, step);

			if(elevatortomove.transform.position == target){

				on = false;

				switch (floor) {
					
					case 0:
						floor = 1;
					break;
					case 1:
						
						floor = 0;

						
					break;
					default:
						
						
						floor = 0;

					break;
					
					
					
				}

				networkView.RPC("ElevatorFloorChanged", RPCMode.Others, floor);

			}
		}

	
	}

	void OnTriggerExit(){

		on = false;

	}

	void OnTriggerStay(){

		if (Input.GetKey(KeyCode.Z) && !on){

			on = true;
			
			networkView.RPC("ElevatorOn", RPCMode.Others, on);

		}
	}


	[RPC]
	void ElevatorOn(bool onIn) {

		print ("Elevator On RPC");
		on = onIn;
	
	}
	
	[RPC]
	void ElevatorFloorChanged(int floorIn) {

		print ("Elevator Floor Change RPC");

		
		floor = floorIn;

		print (floor);

		on = false;

	}



}
