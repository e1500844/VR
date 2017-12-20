using System;
using System.Collections.Generic;
using SimpleJSON;
using System.IO;
using System.Net;
using UnityEngine;
using UnityEngine.Networking; 
using System.Collections;

public class Load : MonoBehaviour
{
	Model[] modelParameters;
    List<GameObject> models = new List<GameObject>();
    GameObject instance;
	bool updateStatus = false;
	bool allowUpdate = true;

	string data = "";

    void Start()
    {
        StartCoroutine(WaitForRequest());
	}

	void LateUpdate()
	{
	}

	void Update(){
		if (updateStatus == true) 
		{
			updateStatus = false;
			DeleteAll ();
			StartCoroutine(WaitForRequest());
		}
		if (Input.GetKeyDown(KeyCode.Q)) {
			if (allowUpdate) {
				allowUpdate = false;
				DeleteAll();
				StartCoroutine(WaitForRequest());
			}

		}
			
	}

    private void DeleteAll()
    {
        foreach (GameObject g in models)
        {
            GameObject.Destroy(g);
        }
    }

    IEnumerator WaitForRequest()
    {
		string fixJson = "http://www.cc.puv.fi/~e1500844/cord.json";
		string modelJson = "http://www.cc.puv.fi/~e1401150/3dmodel.json";

		WWW www = new WWW (fixJson);

        yield return www;

        // check for errors
        if (www.error == null)
        {          
            string jono = www.text;

            int count = jono.Split('x').Length - 1;

			modelParameters = Model.getModelParameter (jono, count);

            for (int i = 0; i < count; i++) 
			{
				try
				{
					//Hakee Resources kansiosta jsonissa määritellyn 3d mallin cubes[i].Type
					instance = Instantiate (Resources.Load (modelParameters[i].Type, typeof(GameObject))) as GameObject;

					instance.AddComponent<BoxCollider> ();
					instance.AddComponent<Rigidbody> ();

					//Asettaa objektin sijainnin
					Vector3 v = new Vector3 (modelParameters[i].X, modelParameters[i].Y, modelParameters[i].Z);
					instance.transform.position = v;

					//Hakee Jsonista värin ja asettaa sen Objektille
					Color MyColour = Color.clear; ColorUtility.TryParseHtmlString (modelParameters [i].Color, out MyColour);
					instance.GetComponentInChildren<Renderer> ().material.color = MyColour;

					//Lisää 3d mallit listaan, käytetään mallien poistamisessa maailmasta
					models.Add(instance);
				
				}
				catch{
					Debug.Log ("Failed to load model: " + modelParameters[i].Type + ". Model name might be incorrect.");
				};
			}

        }
        else
        {
            Debug.Log("WWW Error: " + www.error);
        }

		allowUpdate = true;
    }
}