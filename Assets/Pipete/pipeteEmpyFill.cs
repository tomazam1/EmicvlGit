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
