using UnityEngine;
using System.Collections;

public class haloEffectScript : MonoBehaviour {

	GameObject haloLoc;//the object that hosts the halo effect
	Behaviour shineAround;//the halo effect itself


	void Start () {
		haloLoc=GameObject.Find("haloLoc");
		if (haloLoc == null){Debug.Log("haloLoc not found");}
		if(haloLoc){
			shineAround=(Behaviour)haloLoc.GetComponent("Halo");
			if (shineAround == null){Debug.Log("halo effect not found");}
			if (shineAround){
				shineAround.enabled=true  ;
				}
			}

	
	}
	

	void OnGUI () {	
		if((shineAround.enabled)&&(GUI.Button(new Rect(10,20,200,50),"hide halo effect"))){
			shineAround.enabled=false;
			}
		if((!shineAround.enabled)&&(GUI.Button(new Rect(10,20,200,50),"show halo effect"))){
			shineAround.enabled=true;
			}

	}
}
