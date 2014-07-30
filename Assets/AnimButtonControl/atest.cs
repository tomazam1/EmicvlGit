using UnityEngine;
using System.Collections;

public class atest : MonoBehaviour {

	GameObject ball;
	/*
	If you select your animation in the Project window, 
	then go to the top right corner of the Inspector window 
		and select Debug from the drop down menu (next to the lock), 
		then change the Animation Type to 1 will mark it as Legacy.
   */
	void Start () {

		ball = GameObject.Find("ball");if (ball == null){Debug.Log("Start(): ball not found");}//**
		/*
		int size = ball.animation.GetClipCount();
		string[] ani_names = new string[size];
		int counter = 0;
		print ("animation "+ball.animation.name+" has "+size+" clips");
		foreach (AnimationState states in ball.animation) {
			ani_names[counter++] = states.name;
		}
		foreach (string s in ani_names) {
			print("animClipName="+s);
		}
		*/
	}




	void OnGUI () {
		if(GUI.Button(new Rect((Screen.width/2)-300,10,80,40),"PLAY")){
			ball.animation.Play();
		}		  	
		if(GUI.Button(new Rect((Screen.width/2)-200,10,80,40),"STOP")){
			ball.animation.Stop();
		}		  	

	}
}
