using UnityEngine;
using System.Collections;

public class Flashlight : MonoBehaviour {
	
	public Light flashlight;

	// Update is called once per frame
	void Update () {

		if (networkView.isMine)
		{

			flashlight.transform.position = transform.position;
			flashlight.transform.rotation = transform.rotation;
	
		}
		else
		{
			SyncedMovement();
		}


	}

	private void SyncedMovement()
	{
		syncTime += Time.deltaTime;
		flashlight.transform.position = Vector3.Lerp(syncStartPosition, syncEndPosition, syncTime / syncDelay);
		
		flashlight.transform.rotation = syncRot;
	}


	private float lastSynchronizationTime = 0f;
	private float syncDelay = 0f;
	private float syncTime = 0f;
	private Vector3 syncVelocity = Vector3.zero;
	private Vector3 syncStartPosition = Vector3.zero;
	private Vector3 syncEndPosition = Vector3.zero;
	private Quaternion syncRot = Quaternion.identity;

	void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
	{
		Vector3 syncPosition = Vector3.zero;
		Vector3 syncVelocity = Vector3.zero;
		if (stream.isWriting)
		{
			syncPosition = flashlight.transform.position;
			stream.Serialize(ref syncPosition);
			
			syncVelocity = rigidbody.velocity;
			stream.Serialize(ref syncVelocity);
			
			syncRot = flashlight.transform.rotation;
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
			syncStartPosition = flashlight.transform.position;
		}
	}

}
