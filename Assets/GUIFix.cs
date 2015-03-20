using UnityEngine;
using System.Collections;

public class GUIFix : VRGUI 
{
	public override void OnVRGUI()
	{

		
		//if (showEnd) {
			
			
			GUI.Button(new Rect(100, 100, 250, 100), "Will U click me");
			
		//}

		GUILayout.BeginArea(new Rect(0f, 0f, Screen.width, Screen.height));
		if (GUILayout.Button("Click Me!"))
		{

		}
		GUILayout.EndArea();
	}
}