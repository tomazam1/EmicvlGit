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