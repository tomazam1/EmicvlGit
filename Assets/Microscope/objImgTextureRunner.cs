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
