using UnityEngine;
using System.Collections;

public class StairClumb : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision collision){

		print ("PLAYER SCALE = " + transform.localScale.y);

		print ("PLAYER Y = " + transform.position.y);
		//print (collision.gameObject.name);
		print ("COLLISION POINT = " + collision.contacts [0].point.y);


		if (collision.gameObject.tag == "Stairs") {

			//&& collision.contacts[0].point.y > (transform.position.y - transform.localScale.y) + 0.3f

			if (collision.contacts [0].point.y < transform.position.y + 1f && collision.contacts[0].point.y > transform.position.y - 0.7f){

					print ("DID" + collision.gameObject.name);

					Vector3 tempPos = Vector3.zero;

					tempPos.z = transform.position.z;
					tempPos.x = transform.position.x;
					tempPos.y = transform.position.y + collision.contacts [0].point.y;

					transform.position = tempPos;

					//rigidbody.AddForce (Vector3.up * 100000f);

			}

		}

	}
}
