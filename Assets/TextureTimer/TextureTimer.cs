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

public class TextureTimer : MonoBehaviour {
public Shader MobileDiffuseSh;
public Texture picTxtr0;
GameObject ImgCube;
string txtrName;
float hSliderValue,delay,dd;
int ax,imgNo,imgMaxNo ;
Texture TextTmp ;

void Start () {
	TextTmp=picTxtr0;
	imgMaxNo=23;
	imgNo=1;
	delay=2;
	dd=0;
	hSliderValue=delay;
	ImgCube=GameObject.Find("ImgCube");if (ImgCube == null){Debug.Log("Start(): ImgCube not found");}
	ImgCube.renderer.material = new Material(MobileDiffuseSh);
	TextTmp = (Texture)Resources.Load("t1") as Texture;
	ImgCube.renderer.material.SetTexture("_MainTex",TextTmp);
	}	
	
void OnGUI () {
	hSliderValue = GUI.HorizontalSlider (new Rect (200,25,400,40),hSliderValue,0.1F,3F);
	delay=hSliderValue;
	}	
	
void Update () {
	//print(imgNo+"=imgNo,Time.deltaTime="+Time.deltaTime);
	txtrName="t"+imgNo;
	TextTmp = (Texture)Resources.Load(txtrName) as Texture;
	ImgCube.renderer.material.SetTexture("_MainTex",TextTmp);
	dd+=Time.deltaTime;
	if(dd>=delay){
		imgNo++;
		dd=0;
		}
	if(imgNo>=imgMaxNo){
		imgNo=1;
		dd=0;
		}
	}	
}
