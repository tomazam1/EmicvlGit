// 1. Create a new c# script in Unity:Assets->Create->c# script
// 2. name this c# file as TextureSwitch 
// 3. open the TextureSwitch.cs file in Unity 
// 4. delete ALL contents of this file
// 5. copy all the contents of this text file and paste it into the TextureSwitch.cs 
// 6. save TextureSwitch.cs 
// 7. Drag TextureSwitch.cs from the project window on the kam0Mov object in the Hierarchy window 
// 8. in the Hierarchy window click on the in the kam0Mov object and then in the Inspector window fill the public variables MobileDiffuseSh and picTxtr0 

using UnityEngine;
using System.Collections;

public class TextureSwitch : MonoBehaviour {
public Shader MobileDiffuseSh;
public Texture picTxtr0;
GameObject ImgCube;
string txtrName;
float hSliderValue,duration;
int ax ;
Texture TextTmp ;

void Start () {
	hSliderValue=1;
	TextTmp=picTxtr0;
	ImgCube=GameObject.Find("ImgCube");if (ImgCube == null){Debug.Log("Start(): ImgCube not found");}
	ImgCube.renderer.material = new Material(MobileDiffuseSh);
	TextTmp = (Texture)Resources.Load("t1") as Texture;
	ImgCube.renderer.material.SetTexture("_MainTex",TextTmp);
	}	
	
void OnGUI () {
	hSliderValue = GUI.HorizontalSlider (new Rect (200,25,400,40),hSliderValue,1F,22F);
	ax=(int)hSliderValue;
	txtrName="t"+ax;
	//print (ax+"=ax,name="+txtrName);
	TextTmp = (Texture)Resources.Load(txtrName) as Texture;
	ImgCube.renderer.material.SetTexture("_MainTex",TextTmp);
	}	

}
