// 1. Create a new c# script in Unity:Assets->Create->c# script
// 2. name this c# file as spiralaKrogle 
// 3. open the spiralaKrogle.cs file in Unity 
// 4. delete ALL contents of this file
// 5. copy all the contents of this text file and paste it into the spiralaKrogle.cs 
// 6. save spiralaKrogle.cs 
// 7. Drag spiralaKrogle.cs from the project window on the kam0Mov object in the Hierarchy window 
// 8. in the Hierarchy window click on the in the kam0Mov object and then in the Inspector window fill the public variables MobileDiffuseSh and picTxtr0 

using UnityEngine;
using System.Collections;

public class spiralaKrogle : MonoBehaviour {
public Shader MobileDiffuseSh;// = Shader.Find("Diffuse");
public Texture picTxtr1;
public GameObject pspirala;
//public GameObject objIzgine;
//public GUISkin GUIskinMoja1 ;
//GUIStyle mojstil;
GameObject exkr,exkrprej,exo,exop,exuho,kocka;
string ime, txtrName;
int i,obn;
//int steviloObjektovPolza=5;
float hSliderValue,MorfValue;
int ax ;
float sinampl   = 0.0F;
float xampl   = 0.0F;
Vector3 velikost;
Transform legende,siji;
Behaviour sij;
//string izbraniObj="";
Event dogodek;
Transform objectHit ;
//float animhitrost;
Camera kamera;

	Texture TextTmp;

//public Texture gumbPlay,gumbBack,gumbHome, gumbQuit,gumbNext,gumbPrevious;
GameObject kam0Mov = null;


GameObject centerTrenutni = null;
GameObject cen0 = null;
	//public Shader shader2 = Shader.Find("Transparent/Diffuse");


void Start () {

		TextTmp=picTxtr1;
		kocka=GameObject.Find("kocka");if (kocka == null){Debug.Log("Start(): kocka not found");}
		kocka.renderer.material = new Material(MobileDiffuseSh);

	//init za ovale v polzu:
	for(i=3;i<103;i++){
		ime="exct"+i;
		exo=GameObject.Find(ime);
		exo.renderer.enabled=false;
		//print(ime+".x="+exo.transform.position.x);
		}


		TextTmp = (Texture)Resources.Load("t1") as Texture;
		kocka.renderer.material.SetTexture("_MainTex",TextTmp);



        kam0Mov = GameObject.Find("kam0Mov");if (kam0Mov == null){Debug.Log("Start(): kam0Mov not found");}


        centerTrenutni = GameObject.Find("cen0");if (centerTrenutni == null){Debug.Log("Start(): centerTrenutni not found");}
        cen0 = GameObject.Find("cen0");if (cen0 == null){Debug.Log("Start(): cen0 not found");}


		kam0Mov.transform.LookAt(cen0.transform);


	if (kam0Mov != null){
	   kam0Mov.camera.enabled = true;


		}
}	
	
void OnGUI () {

//hSliderValue = GUI.HorizontalSlider (new Rect (200,25,100,40),hSliderValue,3.0F,102.0F);
		hSliderValue = GUI.HorizontalSlider (new Rect (200,25,100,40),hSliderValue,1F,22F);
//ax=105-(int)hSliderValue;
ax=(int)hSliderValue;
ime="exct"+ax;
txtrName="t"+ax;
		TextTmp = (Texture)Resources.Load(txtrName) as Texture;
		kocka.renderer.material.SetTexture("_MainTex",TextTmp);

		exo=  GameObject.Find(ime);

if(exo){			
	//GameObject.Find("uhoMorf7").animation["koscice"].speed=animhitrost;
	//GameObject.Find("uhoMorf7").animation.Play("koscice");				
	if(exop){exop.renderer.enabled=false;};
	exo.renderer.enabled=true;
	exop=exo;
	velikost.x	= exo.transform.localScale.x;
	velikost.y	= exo.transform.localScale.y;
	velikost.z	= exo.transform.localScale.z;
	xampl=(float)hSliderValue*Time.fixedTime;
	sinampl=1.5F+Mathf.Sin(xampl);
	velikost.y = sinampl; 
	exo.transform.localScale = velikost;	

	}

}	
	
void Update () {
}	
	
	

		
	
}
