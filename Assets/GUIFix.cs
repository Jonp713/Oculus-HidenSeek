using UnityEngine;
using System.Collections;

public class GUIFix : VRGUI 
{

	NetworkManager playerScript;


	void Start(){

		playerScript = GameObject.FindWithTag("MainCamera").GetComponent<NetworkManager> ();

	}

	void Update(){

		playerScript = GameObject.FindWithTag("MainCamera").GetComponent<NetworkManager> ();

	}


	public override void OnVRGUI()
	{


		GUILayout.BeginArea(new Rect(0f, 0f, Screen.width, Screen.height));

		if (playerScript.showEnd) {
			
			
			GUI.Button(new Rect(100, 100, 250, 100), playerScript.winners + " wins!");
			
		}

		GUILayout.EndArea();
	}
}