using UnityEngine;
using System.Collections;

public class Aftermath : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	//displays who one after game is over 
	
	public bool showGUI;
	public string winners = "Test"; 
	
	// Update is called once per frame
	void Update () {

		if (showGUI) {
			GUI.Button (new Rect (100, 100, 250, 100), winners + " Win!");
		}
	
	}


	[RPC]
	void endGame(string Winners) {
		
		print (Winners + " Win!");
		
		GUI.Button (new Rect (100, 100, 250, 100), Winners + " Win!");
		
		
	}
	

}
