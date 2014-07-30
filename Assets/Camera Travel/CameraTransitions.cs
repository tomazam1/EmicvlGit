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
