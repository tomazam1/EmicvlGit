// 1. Create a new c# script in Unity:Assets->Create->c# script
// 2. name this c# file as TextureTimer 
// 3. open the TextureTimer.cs file in Unity 
// 4. delete ALL contents of this file
// 5. copy all the contents of this text file and paste it into the TextureTimer.cs 
// 6. save TextureTimer.cs 
// 7. Drag TextureTimer.cs from the project window on the kam0Mov object in the Hierarchy window 
// 8. in the Hierarchy window click on the in the kam0Mov object and then in the Inspector window fill the public variables MobileDiffuseSh and picTxtr0 

using UnityEngine;
using System.Collections;

public class TextureStimulator : MonoBehaviour {
public Shader MobileDiffuseSh;
public Texture stimTxtr;
public Texture pauseTxtr;
GameObject ImgCube;
string txtrName;
float delay,dd;
float stimTime=0.4f;
float pauseTime=0.7f;
bool stimOn=true;
Texture TextTmp ;

void Start () {
	txtrName=stimOn?stimTxtr.name:pauseTxtr.name;
	TextTmp=stimOn?stimTxtr:pauseTxtr;
	delay=2;
	dd=0;
	ImgCube=GameObject.Find("ImgCube");if (ImgCube == null){Debug.Log("Start(): ImgCube not found");}
	ImgCube.renderer.material = new Material(MobileDiffuseSh);
	TextTmp = (Texture)Resources.Load(txtrName) as Texture;
	ImgCube.renderer.material.SetTexture("_MainTex",TextTmp);
	}	
	
void OnGUI () {
		stimTime = GUI.HorizontalSlider (new Rect (200,25,400,40),stimTime,0.1F,3F);
		pauseTime = GUI.HorizontalSlider (new Rect (200,85,400,40),pauseTime,0.1F,3F);
	}	
	
void Update () {
	txtrName=stimOn?stimTxtr.name:pauseTxtr.name;
	TextTmp = (Texture)Resources.Load(txtrName) as Texture;
	ImgCube.renderer.material.SetTexture("_MainTex",TextTmp);
	dd+=Time.deltaTime;
	if(dd>=delay){
		dd=0;
		if(stimOn){
		delay=pauseTime;
		stimOn=false;
		}
		else{
		delay=stimTime;
		stimOn=true;
		}
	}
	}	
}
