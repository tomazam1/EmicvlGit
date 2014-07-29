EmicvlGit
=========

Emicvl Unity code related to youtube tutorials by Tomaz Amon

You have here the source code in C# that was used with may youtube tutorials published on 

http://www.youtube.com/channel/UC92FD39sxM-GZ5Oyb3ACtTw

Below there are several C# code packages related to different tutorials. You find the beginning of the code package by searching for the string: //**

You insert the code into the unity project like this (the name atest refers to the first C#  just below):

1. Create a new c# script in Unity:Assets->Create->c# script
2. name this c# file as atest 
3. open the atest.cs file in Unity 
4. delete ALL contents of this file
5. copy all the contents of this text file below (between the BUTTON CONTROL BEGIN SCRIPT and ...END SCRIPT line ) and paste it into the atest.cs 
6. save atest.cs 
7. Drag atest.cs from the project window on the kam0Mov object in the Hierarchy window 
8. in the Hierarchy window click on the in the kam0Mov object and then in the Inspector window fill the public variables MobileDiffuseSh and picTxtr0 

Here starts the source code:


//**BUTTON CONTROL BEGIN SCRIPT:

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

//BUTTON CONTROL end SCRIPT.

//**CAMERA SELECT BEGIN:
using UnityEngine;
using System.Collections;

public class CameraSelect : MonoBehaviour {


Camera kamera;
	
GameObject kam0Mov = null;
GameObject kam0 = null;
GameObject kam1 = null;
GameObject kam2 = null;
GameObject kam3 = null;
GameObject centerCurrent = null;
GameObject cen0 = null;
GameObject cen1 = null;
GameObject cen2 = null;
GameObject cen3 = null;
float transitionDuration = 2.5f;
int actCamNo =0;
bool actCamTravel=false;	
	
bool mouseEmulRot=true;
bool firstPinch=true;
float pinchzoomX=0;
float pinchzoomY=0;
float pinchzoomDist = 30;
float pinchzoomMinDist = 10;
float pinchzoomMaxDist = 150;
Touch pinchzoomTouch;
Quaternion pinchzoomRot,kamBeforeRot;
Vector3 pinchzoomPos;
bool afterPinchzoom=false;
float mouseSimOsX,mouseSimOsY,mouseSimX,mouseSimY;
bool mouseSimdol;
	
void Start () {
        kam0Mov = GameObject.Find("kam0Mov");if (kam0Mov == null){Debug.Log("Start(): kam0Mov not found");}
		kam0 = GameObject.Find("kam0");if (kam0 == null){Debug.Log("Start(): Objective not found");}
        kam1 = GameObject.Find("kam1");if (kam1 == null){Debug.Log("Start(): kam1 not found");}
        kam2 = GameObject.Find("kam2");if (kam2 == null){Debug.Log("Start(): kam2 not found");}
        kam3 = GameObject.Find("kam3");if (kam3 == null){Debug.Log("Start(): kam3 not found");}
        centerCurrent = GameObject.Find("cen0");if (centerCurrent == null){Debug.Log("Start(): centerCurrent not found");}
        cen0 = GameObject.Find("cen0");if (cen0 == null){Debug.Log("Start(): cen0 not found");}
		cen1 = GameObject.Find("cen1");if (cen1 == null){Debug.Log("Start(): cen1 not found");}
		cen2 = GameObject.Find("cen2");if (cen2 == null){Debug.Log("Start(): cen2 not found");}
		cen3 = GameObject.Find("cen3");if (cen3 == null){Debug.Log("Start(): cen3 not found");}
		kam0.transform.LookAt(cen0.transform);
		kam1.transform.LookAt(cen1.transform);
		kam2.transform.LookAt(cen2.transform);
		kam3.transform.LookAt(cen3.transform);
		kam0Mov.transform.LookAt(cen0.transform);
		kamBeforeRot=kam0Mov.transform.rotation;
		kam0Mov.camera.enabled = true;
	    kam0.camera.enabled = false;
        kam1.camera.enabled = false;
        kam2.camera.enabled = false;
        kam3.camera.enabled = false;
}	
	
void OnGUI () {
if((!actCamTravel)&&(GUI.Button(new Rect((Screen.width/2)-200,10,80,40),"MICR"))){
	centerCurrent=cen0;
	StartCoroutine(Transition(kam0Mov,kam0));
	actCamNo=0;
	firstPinch=true;
	}		  	
if((!actCamTravel)&&(GUI.Button(new Rect((Screen.width/2)-100,10,80,40),"OCUL"))){
	centerCurrent=cen1;
	StartCoroutine(Transition(kam0Mov,kam1));
	actCamNo=1;
	firstPinch=true;
}		  	
if((!actCamTravel)&&(GUI.Button(new Rect((Screen.width/2),10,80,40),"OBJEC"))){
	centerCurrent=cen2;
	StartCoroutine(Transition(kam0Mov,kam2));
	actCamNo=2;
	firstPinch=true;
}		  	
if((!actCamTravel)&&(GUI.Button(new Rect((Screen.width/2)+100,10,80,40),"SPEC"))){
	centerCurrent=cen3;
	StartCoroutine(Transition(kam0Mov,kam3));
	actCamNo=3;
	firstPinch=true;
}		  	
if((!actCamTravel)&&(GUI.Button(new Rect((Screen.width/2)+200,10,80,40),"NEXT"))){
	switch(actCamNo){ 
	case 0:	
		centerCurrent=cen1;StartCoroutine(Transition(kam0,kam1));
		actCamNo++;
		firstPinch=true;
		break;
	case 1:	
		centerCurrent=cen2;StartCoroutine(Transition(kam1,kam2));
		actCamNo++;
		firstPinch=true;
		break;
	case 2:	
		centerCurrent=cen3;StartCoroutine(Transition(kam2,kam3));
		actCamNo++;
		firstPinch=true;
		break;
	case 3:	
		centerCurrent=cen0;StartCoroutine(Transition(kam3,kam0));
		actCamNo=0;
		firstPinch=true;
		break;
	}	
}		  	
}	
	
void Update () {
		if(Input.GetMouseButton(0)){mouseSimdol=true;}else{mouseSimdol=false;}

		if(!actCamTravel){
			if(Input.mousePosition.y<(Screen.height-70)){	
			pinchzoomDist=Vector3.Distance(centerCurrent.transform.position,kam0Mov.transform.position);
		    if(mouseEmulRot&&mouseSimdol){
				afterPinchzoom=true;
		     	mouseSimOsX=Input.GetAxis("Mouse X");
		     	mouseSimOsY=Input.GetAxis("Mouse Y");
		        mouseSimX += (float)(mouseSimOsX * 11 * 0.2);
		        mouseSimY -= (float)(mouseSimOsY * 11 * 0.2);
				pinchzoomX=mouseSimX;
				pinchzoomY=mouseSimY;
				}
			if(afterPinchzoom){
				if(pinchzoomDist <= pinchzoomMinDist){
					//minimum camera distance
					pinchzoomDist = pinchzoomMinDist;
					}		
				if(pinchzoomDist >= pinchzoomMaxDist){
					//maximum camera distance
					pinchzoomDist = pinchzoomMaxDist;
					}
				//Sets rotation
				pinchzoomRot = kamBeforeRot*Quaternion.Euler(pinchzoomY, pinchzoomX, 0);
				pinchzoomPos =  pinchzoomRot*(new Vector3(0,0,-pinchzoomDist)) + centerCurrent.transform.position;
				//Applies rotation and position
				if(firstPinch){
					kam0Mov.transform.position=pinchzoomPos;
					kam0Mov.transform.rotation=pinchzoomRot;
					firstPinch=false;
					}
				else{
					kam0Mov.transform.position=pinchzoomPos;
					kam0Mov.transform.rotation=pinchzoomRot;
					}
				afterPinchzoom=false;
				}
			}
			
		}


}	
	
	
IEnumerator Transition(GameObject k1,GameObject k2){
    float t = 0.0f;
    Vector3 startingPos = k1.transform.position;
    Vector3 targetPos = k2.transform.position;
	Quaternion startingRot = k1.transform.rotation;
	Quaternion targetRot = k2.transform.rotation;
	float startingFView = k1.camera.fieldOfView;
	float targetFView = k2.camera.fieldOfView;
	actCamTravel=true;
    while (t < 1.0f){
        t += Time.deltaTime * (Time.timeScale/transitionDuration);
        kam0Mov.transform.position = Vector3.Lerp(startingPos, targetPos, t);
		kam0Mov.transform.rotation = Quaternion.Lerp(startingRot, targetRot,t);
		kam0Mov.camera.fieldOfView = Mathf.Lerp(startingFView, targetFView,t);
        yield return 0;
        }
	actCamTravel=false;
	pinchzoomPos=kam0Mov.transform.position;
	pinchzoomRot=kam0Mov.transform.rotation;
	kamBeforeRot=kam0Mov.transform.rotation;
	}		
}
//CAMERA SELECT END.
//**CAMERA TRANSITIONS BEGIN
using UnityEngine;
using System.Collections;

public class CameraTransitions : MonoBehaviour {


Camera kamera;
	
GameObject kam0Mov = null;
GameObject kam0 = null;
GameObject kam1 = null;
GameObject kam2 = null;
GameObject kam3 = null;
GameObject centerCurrent = null;
GameObject cen0 = null;
GameObject cen1 = null;
GameObject cen2 = null;
GameObject cen3 = null;
float transitionDuration = 2.5f;
int actCamNo =0;
bool actCamTravel=false;	
	
bool mouseEmulRot=true;
bool firstPinch=true;
float pinchzoomX=0;
float pinchzoomY=0;
float pinchzoomDist = 30;
float pinchzoomMinDist = 10;
float pinchzoomMaxDist = 150;
Touch pinchzoomTouch;
Quaternion pinchzoomRot,kamBeforeRot;
Vector3 pinchzoomPos;
bool afterPinchzoom=false;
float mouseSimOsX,mouseSimOsY,mouseSimX,mouseSimY;
bool mouseSimdol;
	
void Start () {
        kam0Mov = GameObject.Find("kam0Mov");if (kam0Mov == null){Debug.Log("Start(): kam0Mov not found");}
		kam0 = GameObject.Find("kam0");if (kam0 == null){Debug.Log("Start(): Objective not found");}
        kam1 = GameObject.Find("kam1");if (kam1 == null){Debug.Log("Start(): kam1 not found");}
        kam2 = GameObject.Find("kam2");if (kam2 == null){Debug.Log("Start(): kam2 not found");}
        kam3 = GameObject.Find("kam3");if (kam3 == null){Debug.Log("Start(): kam3 not found");}
        centerCurrent = GameObject.Find("cen0");if (centerCurrent == null){Debug.Log("Start(): centerCurrent not found");}
        cen0 = GameObject.Find("cen0");if (cen0 == null){Debug.Log("Start(): cen0 not found");}
		cen1 = GameObject.Find("cen1");if (cen1 == null){Debug.Log("Start(): cen1 not found");}
		cen2 = GameObject.Find("cen2");if (cen2 == null){Debug.Log("Start(): cen2 not found");}
		cen3 = GameObject.Find("cen3");if (cen3 == null){Debug.Log("Start(): cen3 not found");}
		kam0.transform.LookAt(cen0.transform);
		kam1.transform.LookAt(cen1.transform);
		kam2.transform.LookAt(cen2.transform);
		kam3.transform.LookAt(cen3.transform);
		kam0Mov.transform.LookAt(cen0.transform);
		kamBeforeRot=kam0Mov.transform.rotation;
		kam0Mov.camera.enabled = true;
	    kam0.camera.enabled = false;
        kam1.camera.enabled = false;
        kam2.camera.enabled = false;
        kam3.camera.enabled = false;
}	
	
void OnGUI () {
if((!actCamTravel)&&(GUI.Button(new Rect(Screen.width/2,10,80,40),"NEXT"))){
	switch(actCamNo){ 
		case 0:	
				centerCurrent=cen1;StartCoroutine(Transition(kam0,kam1));
				actCamNo++;
				firstPinch=true;
				break;
		case 1:	
				centerCurrent=cen2;StartCoroutine(Transition(kam1,kam2));
				actCamNo++;
				firstPinch=true;
				break;
		case 2:	
				centerCurrent=cen3;StartCoroutine(Transition(kam2,kam3));
				actCamNo++;
				firstPinch=true;
				break;
		case 3:	
				centerCurrent=cen0;StartCoroutine(Transition(kam3,kam0));
				actCamNo=0;
				firstPinch=true;
				break;
		}	
	}		  	
}	
	
void Update () {
		if(Input.GetMouseButton(0)){mouseSimdol=true;}else{mouseSimdol=false;}
		if(!actCamTravel){
			if(Input.mousePosition.y<(Screen.height-70)){	
			pinchzoomDist=Vector3.Distance(centerCurrent.transform.position,kam0Mov.transform.position);
		    if(mouseEmulRot&&mouseSimdol){
				afterPinchzoom=true;
		     	mouseSimOsX=Input.GetAxis("Mouse X");
		     	mouseSimOsY=Input.GetAxis("Mouse Y");
		        mouseSimX += (float)(mouseSimOsX * 11 * 0.2);
		        mouseSimY -= (float)(mouseSimOsY * 11 * 0.2);
				pinchzoomX=mouseSimX;
				pinchzoomY=mouseSimY;
				}
			if(afterPinchzoom){
				if(pinchzoomDist <= pinchzoomMinDist){
					//minimum camera distance
					pinchzoomDist = pinchzoomMinDist;
					}		
				if(pinchzoomDist >= pinchzoomMaxDist){
					//maximum camera distance
					pinchzoomDist = pinchzoomMaxDist;
					}
				//Sets rotation
				pinchzoomRot = kamBeforeRot*Quaternion.Euler(pinchzoomY, pinchzoomX, 0);
				pinchzoomPos =  pinchzoomRot*(new Vector3(0,0,-pinchzoomDist)) + centerCurrent.transform.position;
				//Applies rotation and position
				if(firstPinch){
					kam0Mov.transform.position=pinchzoomPos;
					kam0Mov.transform.rotation=pinchzoomRot;
					firstPinch=false;
					}
				else{
					kam0Mov.transform.position=pinchzoomPos;
					kam0Mov.transform.rotation=pinchzoomRot;
					}
				afterPinchzoom=false;
				}
			}
			
		}
}	
	
	
IEnumerator Transition(GameObject k1,GameObject k2){
    float t = 0.0f;
    Vector3 startingPos = k1.transform.position;
    Vector3 targetPos = k2.transform.position;
	Quaternion startingRot = k1.transform.rotation;
	Quaternion targetRot = k2.transform.rotation;
	float startingFView = k1.camera.fieldOfView;
	float targetFView = k2.camera.fieldOfView;
	actCamTravel=true;
    while (t < 1.0f){
        t += Time.deltaTime * (Time.timeScale/transitionDuration);
        kam0Mov.transform.position = Vector3.Lerp(startingPos, targetPos, t);
		kam0Mov.transform.rotation = Quaternion.Lerp(startingRot, targetRot,t);
		kam0Mov.camera.fieldOfView = Mathf.Lerp(startingFView, targetFView,t);
        yield return 0;
        }
	actCamTravel=false;
	pinchzoomPos=kam0Mov.transform.position;
	pinchzoomRot=kam0Mov.transform.rotation;
	kamBeforeRot=kam0Mov.transform.rotation;
	}		
}
//CAMERA TRANSITIONS END
//**MICROSCOPE OBJECTIVE GLASS SELECT BEGIN
using UnityEngine;
using System.Collections;

public class MicrObjGlassSelect : MonoBehaviour {
	/*
	If you select your animation in the Project window, 
	then go to the top right corner of the Inspector window 
		and select Debug from the drop down menu (next to the lock), 
		then change the Animation Type to 1 will mark it as Legacy.
   */


Camera kamera;
	
GameObject kam0Mov = null;
GameObject kam0 = null;
GameObject kam1 = null;
GameObject kam2 = null;
GameObject kam3 = null;
GameObject centerCurrent = null;
GameObject cen0 = null;
GameObject cen1 = null;
GameObject cen2 = null;
GameObject cen3 = null;
float transitionDuration = 2.5f;
int actCamNo =0;
bool actCamTravel=false;	
	
bool mouseEmulRot=true;
bool firstPinch=true;
float pinchzoomX=0;
float pinchzoomY=0;
float pinchzoomDist = 30;
float pinchzoomMinDist = 10;
float pinchzoomMaxDist = 150;
Touch pinchzoomTouch;
Quaternion pinchzoomRot,kamBeforeRot;
Vector3 pinchzoomPos;
bool afterPinchzoom=false;
float mouseSimOsX,mouseSimOsY,mouseSimX,mouseSimY;
bool mouseSimdol;

GameObject ObjGlas;//**
GameObject 	ObjPicturePlane;//**
float objLimDist=0.2f;//**
bool GlassPosAchieved=false;//**
	Animation an1;
	AnimationState ans1;
	
void Start () {
        kam0Mov = GameObject.Find("kam0Mov");if (kam0Mov == null){Debug.Log("Start(): kam0Mov not found");}
		kam0 = GameObject.Find("kam0");if (kam0 == null){Debug.Log("Start(): Objective not found");}
        kam1 = GameObject.Find("kam1");if (kam1 == null){Debug.Log("Start(): kam1 not found");}
        kam2 = GameObject.Find("kam2");if (kam2 == null){Debug.Log("Start(): kam2 not found");}
        kam3 = GameObject.Find("kam3");if (kam3 == null){Debug.Log("Start(): kam3 not found");}
        centerCurrent = GameObject.Find("cen0");if (centerCurrent == null){Debug.Log("Start(): centerCurrent not found");}
        cen0 = GameObject.Find("cen0");if (cen0 == null){Debug.Log("Start(): cen0 not found");}
		cen1 = GameObject.Find("cen1");if (cen1 == null){Debug.Log("Start(): cen1 not found");}
		cen2 = GameObject.Find("cen2");if (cen2 == null){Debug.Log("Start(): cen2 not found");}
		cen3 = GameObject.Find("cen3");if (cen3 == null){Debug.Log("Start(): cen3 not found");}
		kam0.transform.LookAt(cen0.transform);
		kam1.transform.LookAt(cen1.transform);
		kam2.transform.LookAt(cen2.transform);
		kam3.transform.LookAt(cen3.transform);
		kam0Mov.transform.LookAt(cen0.transform);
		kamBeforeRot=kam0Mov.transform.rotation;
		kam0Mov.camera.enabled = true;
	    kam0.camera.enabled = false;
        kam1.camera.enabled = false;
        kam2.camera.enabled = false;
        kam3.camera.enabled = false;

		ObjGlas = GameObject.Find("objGlas");if (ObjGlas == null){Debug.Log("Start(): objGlas not found");}//**
		ObjPicturePlane = GameObject.Find("ObjPicturePlane");if (ObjPicturePlane == null){Debug.Log("Start(): ObjPicturePlane not found");}//**
		//ObjGlas.animation.Stop();

		//ao1 = GameObject.Find(ai1);if (ao1 == null){Debug.Log("an1Pozeni:animObj not found: "+ai1);}
		
		//an1= ObjGlas.animation;if (an1 == null){Debug.Log("Start: ObjGlas animacija not found");}
		//an1.Stop();

		//ans1=ObjGlas.animation["objGlassMoves"];if (ans1 == null){Debug.Log("Start: AnimationState not found");}
		//print(ans1);




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

		/*
		if(GUI.Button(new Rect(Screen.width-8*(globalne.stranicaGumba+globalne.robobGumbu),globalne.robNadGumbom,globalne.stranicaGumba,globalne.stranicaGumba),gumbPlay)){
			//if((!actCamPotuje)&&(GUI.Button(new Rect(Screen.width-4*(stranicaGumba+robobGumbu),robobGumbu,stranicaGumba,stranicaGumba),gumbHelp))){
			
			animTece  = true;
			animacija0.speed = 1.0f;
			animacija0.time = animTrenPos;
			premikajociObjekt.animation.Play(globalne.aslAnim0ime);
		}
		*/
if((!actCamTravel)&&(GUI.Button(new Rect((Screen.width/2)-500,10,80,40),"PLAY"))){
			ObjGlas.animation["glassApproach"].speed = 1.0f;
			ObjGlas.animation.Play();
	}	
		//*
if((!actCamTravel)&&(GUI.Button(new Rect((Screen.width/2)-400,10,80,40),"STOP"))){
			ObjGlas.animation["glassApproach"].speed = 1.0f;
			ObjGlas.animation.Stop();
}	
			//*/

if((!actCamTravel)&&(GUI.Button(new Rect((Screen.width/2)-300,10,80,40),"BACK"))){
			ObjGlas.animation["glassApproach"].time = ObjGlas.animation["glassApproach"].length;
			ObjGlas.animation["glassApproach"].speed = -1.0f;
			ObjGlas.animation.Play();
}		  	



if((!actCamTravel)&&(GUI.Button(new Rect((Screen.width/2)-200,10,80,40),"MICR"))){
	centerCurrent=cen0;
	StartCoroutine(Transition(kam0Mov,kam0));
	actCamNo=0;
	firstPinch=true;
	}		  	
if((!actCamTravel)&&(GUI.Button(new Rect((Screen.width/2)-100,10,80,40),"OCUL"))){
	centerCurrent=cen1;
	StartCoroutine(Transition(kam0Mov,kam1));
	actCamNo=1;
	firstPinch=true;
}		  	
if((!actCamTravel)&&(GUI.Button(new Rect((Screen.width/2),10,80,40),"OBJEC"))){
	centerCurrent=cen2;
	StartCoroutine(Transition(kam0Mov,kam2));
	actCamNo=2;
	firstPinch=true;
}		  	
if((!actCamTravel)&&(GUI.Button(new Rect((Screen.width/2)+100,10,80,40),"SPEC"))){
	centerCurrent=cen3;
	StartCoroutine(Transition(kam0Mov,kam3));
	actCamNo=3;
	firstPinch=true;
}		  	
if((!actCamTravel)&&(GUI.Button(new Rect((Screen.width/2)+200,10,80,40),"NEXT"))){
	switch(actCamNo){ 
	case 0:	
		centerCurrent=cen1;StartCoroutine(Transition(kam0,kam1));
		actCamNo++;
		firstPinch=true;
		break;
	case 1:	
		centerCurrent=cen2;StartCoroutine(Transition(kam1,kam2));
		actCamNo++;
		firstPinch=true;
		break;
	case 2:	
		centerCurrent=cen3;StartCoroutine(Transition(kam2,kam3));
		actCamNo++;
		firstPinch=true;
		break;
	case 3:	
		centerCurrent=cen0;StartCoroutine(Transition(kam3,kam0));
		actCamNo=0;
		firstPinch=true;
		break;
	}	
}		  	
}	
	
void Update () {
		if(((Mathf.Abs(ObjGlas.transform.position.x))<objLimDist)){
			GlassPosAchieved=true;
			ObjPicturePlane.renderer.enabled=true;
			} 
		else{
			ObjPicturePlane.renderer.enabled=false;
			GlassPosAchieved=false;
			}
		//print(Mathf.Abs(ObjGlas.transform.position.x)+"=pos,target="+GlassPosAchieved);



		if(Input.GetMouseButton(0)){mouseSimdol=true;}else{mouseSimdol=false;}

		if(!actCamTravel){
			if(Input.mousePosition.y<(Screen.height-70)){	
			pinchzoomDist=Vector3.Distance(centerCurrent.transform.position,kam0Mov.transform.position);
		    if(mouseEmulRot&&mouseSimdol){
				afterPinchzoom=true;
		     	mouseSimOsX=Input.GetAxis("Mouse X");
		     	mouseSimOsY=Input.GetAxis("Mouse Y");
		        mouseSimX += (float)(mouseSimOsX * 11 * 0.2);
		        mouseSimY -= (float)(mouseSimOsY * 11 * 0.2);
				pinchzoomX=mouseSimX;
				pinchzoomY=mouseSimY;
				}
			if(afterPinchzoom){
				if(pinchzoomDist <= pinchzoomMinDist){
					//minimum camera distance
					pinchzoomDist = pinchzoomMinDist;
					}		
				if(pinchzoomDist >= pinchzoomMaxDist){
					//maximum camera distance
					pinchzoomDist = pinchzoomMaxDist;
					}
				//Sets rotation
				pinchzoomRot = kamBeforeRot*Quaternion.Euler(pinchzoomY, pinchzoomX, 0);
				pinchzoomPos =  pinchzoomRot*(new Vector3(0,0,-pinchzoomDist)) + centerCurrent.transform.position;
				//Applies rotation and position
				if(firstPinch){
					kam0Mov.transform.position=pinchzoomPos;
					kam0Mov.transform.rotation=pinchzoomRot;
					firstPinch=false;
					}
				else{
					kam0Mov.transform.position=pinchzoomPos;
					kam0Mov.transform.rotation=pinchzoomRot;
					}
				afterPinchzoom=false;
				}
			}
			
		}


}	
	
	
IEnumerator Transition(GameObject k1,GameObject k2){
    float t = 0.0f;
    Vector3 startingPos = k1.transform.position;
    Vector3 targetPos = k2.transform.position;
	Quaternion startingRot = k1.transform.rotation;
	Quaternion targetRot = k2.transform.rotation;
	float startingFView = k1.camera.fieldOfView;
	float targetFView = k2.camera.fieldOfView;
	actCamTravel=true;
    while (t < 1.0f){
        t += Time.deltaTime * (Time.timeScale/transitionDuration);
        kam0Mov.transform.position = Vector3.Lerp(startingPos, targetPos, t);
		kam0Mov.transform.rotation = Quaternion.Lerp(startingRot, targetRot,t);
		kam0Mov.camera.fieldOfView = Mathf.Lerp(startingFView, targetFView,t);
        yield return 0;
        }
	actCamTravel=false;
	pinchzoomPos=kam0Mov.transform.position;
	pinchzoomRot=kam0Mov.transform.rotation;
	kamBeforeRot=kam0Mov.transform.rotation;
	}		
}
//MICROSCOPE OBJECTIVE GLASS SELECT END
//**IMAGE TEXTURE RUNNER BEGIN
// 1. Create a new c# script in Unity:Assets->Create->c# script
// 2. name this c# file as TextureRunner 
// 3. open the TextureRunner.cs file in Unity 
// 4. delete ALL contents of this file
// 5. copy all the contents of this text file and paste it into the TextureRunner.cs 
// 6. save TextureRunner.cs 
// 7. Drag TextureRunner.cs from the project window on the kam0Mov object in the Hierarchy window 
// 8. in the Hierarchy window click on the in the kam0Mov object and then in the Inspector window fill the public variables MobileDiffuseSh and picTxtr0 

using UnityEngine;
using System.Collections;

public class objImgTextureRunner : MonoBehaviour {
public Shader MobileDiffuseSh;
public Texture picTxtr0;
GameObject ImgCube;
string txtrName;
int imgNo,imgMaxNo,i,j ;
Texture TextTmp ;

void Start () {
	TextTmp=picTxtr0;
	imgMaxNo=561;
	imgNo=1;
		ImgCube=GameObject.Find("ObjPicturePlane");if (ImgCube == null){Debug.Log("Start(): ImgCube not found");}
	ImgCube.renderer.material = new Material(MobileDiffuseSh);
	TextTmp = (Texture)Resources.Load("t1") as Texture;
	ImgCube.renderer.material.SetTexture("_MainTex",TextTmp);
	}	
	

void Update () {
		imgNo=Time.frameCount%imgMaxNo;
		//print(Time.frameCount+"=frameCount,imgNo="+imgNo);
		txtrName="daphnia"+imgNo;
		TextTmp = (Texture)Resources.Load(txtrName) as Texture;
		ImgCube.renderer.material.SetTexture("_MainTex",TextTmp);
		imgNo++;
		if(imgNo>imgMaxNo){
			imgNo=1;
			}
		}	
}
//IMAGE TEXTURE RUNNER END
//**PIPETTE BLEND SHAPE BEGIN
using UnityEngine;
using System.Collections;

public class blendShapeMoj1 : MonoBehaviour
{
	
	int blendShapeCount;
	SkinnedMeshRenderer skinnedMeshRenderer;
	Mesh skinnedMesh;
	float blendOne = 0f;
	float blendTwo = 0f;
	float blendSpeed = 1f;
	bool blendOneFinished = false;
	GameObject morfObjekt = null;//ga izvozis kot fbx iz Maya, kjer je narejena blendShape. Glej kb1.mb

	
	void Awake ()
	{
		morfObjekt = GameObject.Find("kb1");if (morfObjekt == null){Debug.Log("Start(): morfObjekt  not found");}
		skinnedMeshRenderer = morfObjekt.GetComponent<SkinnedMeshRenderer> ();
		skinnedMesh = morfObjekt.GetComponent<SkinnedMeshRenderer> ().sharedMesh;
	}
	
	void Start ()
	{
		blendShapeCount = skinnedMesh.blendShapeCount; 
	}
	
	void Update ()
	{
		if (blendShapeCount > 2) {
			
			if (blendOne < 100f) {
				skinnedMeshRenderer.SetBlendShapeWeight (0, blendOne);
				blendOne += blendSpeed;
			} else {
				blendOneFinished = true;
			}
			
			if (blendOneFinished == true && blendTwo < 100f) {
				skinnedMeshRenderer.SetBlendShapeWeight (1, blendTwo);
				blendTwo += blendSpeed;
			}
			
		}
	}

	void OnGUI()
	{
		float horizontalSliderPos= skinnedMeshRenderer.GetBlendShapeWeight(0);
		float horizontalSliderPos2 = skinnedMeshRenderer.GetBlendShapeWeight(1);
		float horizontalSliderPos3 = skinnedMeshRenderer.GetBlendShapeWeight(2);
		
		horizontalSliderPos = GUI.HorizontalSlider(new Rect(25, 25, 100, 30), horizontalSliderPos, 0.0F, 100.0F);
		skinnedMeshRenderer.SetBlendShapeWeight(0, horizontalSliderPos);
		
		horizontalSliderPos2 = GUI.HorizontalSlider(new Rect(25, 75, 100, 30), horizontalSliderPos2, 0.0F, 100.0F);
		skinnedMeshRenderer.SetBlendShapeWeight(1, horizontalSliderPos2);
		
		//horizontalSliderPos3 = GUI.HorizontalSlider(new Rect(25, 125, 100, 30), horizontalSliderPos3, 0.0F, 100.0F);
		//skinnedMeshRenderer.SetBlendShapeWeight(2, horizontalSliderPos3);
		
	}

}
//PIPETTE BLEND SHAPE END
//**PIPETTE EMPTY FILL BEGIN
using UnityEngine;
using System.Collections;

public class pipeteEmpyFill : MonoBehaviour {
	//animacija, ki vsebuje tudi premikanje kamere. Pozicija in usmeritev kamere se shrani v global takoj na startu in se potem poklice nazaj

//ko objekt uvozis iz maye kot fbx datoteko, tipicno das scale xyz na 222 in animation type na legacy 
//public	Texture gumbPlay,gumbPause,gumbAnim,gumbGuidedTour,gumbExplore;
GameObject premikajociObjekt = null;
GameObject	kam0Mov;
float hSliderValue,dolzinaAnim,animTrenPos,animPrejPos;
float timeFrameDelta=0.05f;
bool animTece  = false;
bool konectimeline  = false;
	bool animReset  = false; 
	AnimationState animacija0; 
//string aslObjektIme="photosystemIIanim";// !! to ime mora biti ime uvozenega animiranega objekta
//string aslAnim0ime="Take 001";// !! to ime mora biti ime animacije v uvozenem animiranem objektu
GameObject kamSlidPlay = null;
	//GameObject kamTrenO = null;
	//GameObject kameraSidro = null;
	Quaternion kamSlidPlay0Rot,kamSidro0Rot;
	Vector3 kamSlidPlay0Pos,kamSidro0Pos;
	float kamSlidPlay0FoV;
	float transitionDuration = 0.5f;

	void Start () {
//globalne.animKamVuporabi=true;

		/*
		gumbPlay = (Texture)Resources.LoadAssetAtPath("Assets/icons/gumbPlay.png",  typeof(Texture));
		gumbPause = (Texture)Resources.LoadAssetAtPath("Assets/icons/gumbPause.png",  typeof(Texture));
		gumbAnim = (Texture)Resources.LoadAssetAtPath("Assets/icons/gumbAnim.png",  typeof(Texture));
		gumbGuidedTour = (Texture)Resources.LoadAssetAtPath("Assets/icons/gumbGuidedTour.png",  typeof(Texture));
		gumbExplore = (Texture)Resources.LoadAssetAtPath("Assets/icons/gumbExplore.png",  typeof(Texture));
		gumbAnim = (Texture)Resources.LoadAssetAtPath("Assets/icons/gumbAnim.png",  typeof(Texture));
		*/
//kameraSidro = GameObject.Find(globalne.kameraSidroIme);if (kameraSidro == null){Debug.Log("Start(): kameraSidro "+kameraSidro+" not found");}
//if (kameraSidro){
//	kamSidro0Pos=kameraSidro.transform.position;
//	kamSidro0Rot=kameraSidro.transform.rotation;
//	}
		kam0Mov = GameObject.Find("kam0Mov");if (kam0Mov == null){Debug.Log("Start(): kam0Mov not found");}
premikajociObjekt = GameObject.Find("water");if (premikajociObjekt == null){Debug.Log("Start(): water not found");}
animacija0 = premikajociObjekt.animation["Take 001"];if (animacija0 == null){Debug.Log("Start(): animation not found");}
animacija0.speed = 1.0f;
animacija0.time = 0f;
dolzinaAnim=animacija0.length;
premikajociObjekt.animation.Stop();	
//kamSlidPlay = GameObject.FindWithTag("MainCamera");
//		kamSlidPlay = GameObject.Find("kam0Mov");if (kamSlidPlay == null){Debug.Log("Start(): kam0Mov not found");}
		kamSlidPlay = kam0Mov;
if(kamSlidPlay){
	//print ("kamSlidPlay.name="+kamSlidPlay.name);
	kamSlidPlay0Pos=kamSlidPlay.transform.position;
	kamSlidPlay0Rot=kamSlidPlay.transform.rotation;
	kamSlidPlay0FoV=kamSlidPlay.camera.fieldOfView;
	//kamSlidPlayPos=kamSlidPlay.transform.position;
	//kamSlidPlayRot=kamSlidPlay.transform.rotation;
	//kamSlidPlayFoV=kamSlidPlay.camera.fieldOfView;
	//print ("kamSlidPlay pos, rot, FoV="+kamSlidPlayPos+" "+kamSlidPlayRot+" "+kamSlidPlayFoV);
}
else{print ("globalne,camera.main==NULL!");}

//print(globalne.SliderJeSpodaj+"=SliderJeSpodaj-animslider-globalne.hslAY="+globalne.hslAY);



}//start
	
void Update () {
animTrenPos=animacija0.time;
		/*
if((!globalne.animKamVuporabi)&&(!globalne.animBrezAnimKamVuporabi)){
	premikajociObjekt.animation.Stop(globalne.aslAnim0ime);
	animacija0.time = animPrejPos;
	}
		*/
if((dolzinaAnim-animTrenPos)<timeFrameDelta){
	konectimeline=true;	
	}
		if(kamSlidPlay&&animReset){
	animReset=false;
	animTrenPos=0f;
	animacija0.time = animTrenPos;
			//StartCoroutine(UsmeriPremakniProti(kamSlidPlay.transform.position,kamSlidPlay.transform.rotation,kamSlidPlay.camera.fieldOfView,kamSlidPlay0Pos,kamSlidPlay0Rot,kamSlidPlay0FoV));
			//StartCoroutine(UsmeriPremakniProti(kamSlidPlay.transform.position,kamSlidPlay.transform.rotation,kamSlidPlay.camera.fieldOfView,kameraSidro.transform.position,kamSlidPlay0Rot,kamSlidPlay0FoV));
	}
}
	
void OnGUI () {
		/*
		GUI.skin = globalne.skinMoja1;
		if((globalne.animKamVuporabi)&&(!globalne.animBrezAnimKamVuporabi)){// I default - je tudi kamera vkljucena v animacijo	
			if(GUI.Button(new Rect(Screen.width-9*(globalne.stranicaGumba+globalne.robobGumbu),globalne.robNadGumbom,globalne.stranicaGumba,globalne.stranicaGumba), gumbExplore)){
				globalne.animKamVuporabi=false;
				premikajociObjekt.animation.Stop(globalne.aslAnim0ime);	
				animTece=false;
				if(kamSlidPlay){
					animPrejPos=animTrenPos;
					//kamSlidPlayPos=kamSlidPlay.transform.position;
					//kamSlidPlayRot=kamSlidPlay.transform.rotation;
					//kamSlidPlayFoV=kamSlidPlay.camera.fieldOfView;
				}
			}
			hSliderValue=GUI.HorizontalScrollbar(new Rect(globalne.hslAX,globalne.hslAY,globalne.hslAL,globalne.stranicaGumba),animTrenPos, globalne.SliderDebelina,0.0f,dolzinaAnim);
			//hSliderValue = GUI.HorizontalSlider (new Rect(globalne.hslAX,globalne.hslAY,globalne.hslAL,globalne.stranicaGumba),animTrenPos,0.0F,dolzinaAnim);
			globalne.SliderSpodajViden=true;
			globalne.ScreenYspMeja=globalne.SliderSirina;
			if(animTece){
				if(GUI.Button(new Rect(Screen.width-8*(globalne.stranicaGumba+globalne.robobGumbu),globalne.robNadGumbom,globalne.stranicaGumba,globalne.stranicaGumba),gumbPause)){
					premikajociObjekt.animation.Stop(globalne.aslAnim0ime);	
					animTece=false;
				}
				if(konectimeline&&(animTrenPos==0)){
					animTece=false;	
					konectimeline=false;
				}
			}	
			if(!animTece){
				animacija0.time = hSliderValue;
				animacija0.speed = 1.0f;//hitrost je vseeno kaksna je
				premikajociObjekt.animation.Play(globalne.aslAnim0ime);	
				//print ("animplay");
				if(GUI.Button(new Rect(Screen.width-8*(globalne.stranicaGumba+globalne.robobGumbu),globalne.robNadGumbom,globalne.stranicaGumba,globalne.stranicaGumba),gumbPlay)){
				//if((!actCamPotuje)&&(GUI.Button(new Rect(Screen.width-4*(stranicaGumba+robobGumbu),robobGumbu,stranicaGumba,stranicaGumba),gumbHelp))){

					animTece  = true;
					animacija0.speed = 1.0f;
					animacija0.time = animTrenPos;
					premikajociObjekt.animation.Play(globalne.aslAnim0ime);
				}
			}
		}
		*/
		//if((!globalne.animKamVuporabi)&&(globalne.animBrezAnimKamVuporabi)){// II, animacija dovoljena toda brez animirane kamere - sami gledamo s svojo kamero	

			hSliderValue=GUI.HorizontalSlider(new Rect(10,10,500,50),animTrenPos,0.0f,dolzinaAnim);
		animacija0.time = hSliderValue;
		print ("animacija0.time="+animacija0.time);
		//premikajociObjekt.animation.Sample(hSliderValue);
		//animTece=false;
			//globalne.SliderSpodajViden=true;
			//globalne.ScreenYspMeja=globalne.SliderSirina;
		/*
			if(animTece){
				if(GUI.Button(new Rect(Screen.width-8*(globalne.stranicaGumba+globalne.robobGumbu),globalne.robNadGumbom,globalne.stranicaGumba,globalne.stranicaGumba),gumbPause)){
					premikajociObjekt.animation.Stop(globalne.aslAnim0ime);	
					animTece=false;
					}
				if(konectimeline&&(animTrenPos==0)){
					animTece=false;	
					konectimeline=false;
					}
				}	
			if(!animTece){
				animacija0.time = hSliderValue;
				animacija0.speed = 1.0f;//hitrost je vseeno kaksna je
				premikajociObjekt.animation.Play(globalne.aslAnim0ime);	
				if(GUI.Button(new Rect(Screen.width-8*(globalne.stranicaGumba+globalne.robobGumbu),globalne.robNadGumbom,globalne.stranicaGumba,globalne.stranicaGumba),gumbPlay)){
					animTece  = true;
					animacija0.speed = 1.0f;
					animacija0.time = animTrenPos;
					premikajociObjekt.animation.Play(globalne.aslAnim0ime);
				}
			}
			*/
		//}
		/*
		if((!globalne.animKamVuporabi)&&(!globalne.animBrezAnimKamVuporabi)){// III, animacija popolnoma izkljucena
			if(GUI.Button(new Rect(Screen.width-9*(globalne.stranicaGumba+globalne.robobGumbu),globalne.robNadGumbom,globalne.stranicaGumba,globalne.stranicaGumba), gumbGuidedTour)){
				globalne.animKamVuporabi=true;
				animReset=true;
				}
			if(GUI.Button(new Rect(Screen.width-8*(globalne.stranicaGumba+globalne.robobGumbu),globalne.robNadGumbom,globalne.stranicaGumba,globalne.stranicaGumba), gumbAnim)){
				globalne.animBrezAnimKamVuporabi=true;
				globalne.animKamVuporabi=false;
				if(kamSlidPlay){
					animTrenPos=animPrejPos;
					//kamTrenO = kamSlidPlay;
					}
			}
			globalne.SliderSpodajViden=false;
			globalne.ScreenYspMeja=0;
		} 
		*/
	}//ongui

	/*
	IEnumerator UsmeriPremakniProti(Vector3 startingPos, Quaternion startingRot, float startingFoV, Vector3 targetPos, Quaternion targetRot,  float targetFoV){
		float t = 0.0f;
		globalne.actCamPotuje=true;
		kamSlidPlay.camera.farClipPlane=1000f;
		globalne.actCamPotuje=true;
		globalne.usmerjamPremikamProti=true;
		//print ("UsmeriPremakniProti:startingPosRot="+startingPos+" "+startingRot+" targetPosRot="+targetPos+" "+targetRot);	
		while (t < 1.0f){
			//print ("UsmeriPremakniProti...");
			t += Time.deltaTime * (Time.timeScale/transitionDuration);
			kamSlidPlay.transform.position = Vector3.Lerp(startingPos, targetPos, t);
			kamSlidPlay.transform.rotation = Quaternion.Lerp(startingRot, targetRot,t);
			kamSlidPlay.camera.fieldOfView = Mathf.Lerp(startingFoV, targetFoV,t);
			yield return 0;
		}
		globalne.actCamPotuje=false;
		//usmerjamRot=kamSlidPlay.transform.rotation;
		globalne.usmerjamPremikamProti=false;
		globalne.pnchZoomPrvi=true;//**

	}
	*/

}
//PIPETTE EMPTY FILL END

