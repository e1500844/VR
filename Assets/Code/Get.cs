using System;
using System.Collections.Generic;
using SimpleJSON;
using System.IO;
using System.Net;
using UnityEngine;
using UnityEngine.Networking; 
using System.Collections;

public class Get : MonoBehaviour
{
    Cube[] cubes;
    List<GameObject> models = new List<GameObject>();
    GameObject instance;
	bool updateStatus = false;

	string data = "";

    void Start()
    {
        StartCoroutine(WaitForRequest());
	}

	void LateUpdate()
	{
		
		//StartCoroutine(Updaattori());
	}

	/*
	IEnumerator Updaattori()
	{
		string url = "http://www.cc.puv.fi/~e1500844/cord.json";
		WWW www = new WWW (url);

		yield return www;

		if (data != www.text) {
			Debug.Log (www.text);
			data = www.text;
			updateStatus = true;
		}
	}
	*/
	void Update(){
		if (updateStatus == true) 
		{
			updateStatus = false;
			DeleteAll ();
			StartCoroutine(WaitForRequest());
		}
		if (Input.GetKeyDown(KeyCode.Q)) {
            DeleteAll();
            StartCoroutine(WaitForRequest());
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
		string url = "http://www.cc.puv.fi/~e1500844/cord.json";
		string janne = "http://www.cc.puv.fi/~e1401150/3dmodel.json";
		string testi = @"C:\Users\Tomi\Documents\Koulu\Projekti\cord.json";
		WWW www = new WWW (url);

        yield return www;

        // check for errors
        if (www.error == null)
        {
            Debug.Log(www.text);
           
            string jono = www.text;

            //var N = JSON.Parse(www.text);

            int count = jono.Split('x').Length - 1;

			cubes = Cube.CreateCube (jono, count);

            Debug.Log(cubes[0].Y);

            for (int i = 0; i < count; i++) 
			{
				//GameObject parentObject = new GameObject();
				//Hakee Resources kansiosta Palikka nimisen objektin, jatkossa voi käyttää olion type muuttujaa
				instance = Instantiate (Resources.Load (cubes[i].Type, typeof(GameObject))) as GameObject;

				//instance.transform.parent = parentObject.transform;
				//Vector3 center = new Vector3 (240,1,200);

				instance.AddComponent<BoxCollider> ();
				instance.AddComponent<Rigidbody> ();


				//instance.transform.position = center;

				//Asettaa objektin sijainnin
				Vector3 v = new Vector3 (cubes[i].X, cubes[i].Y, cubes[i].Z);
				instance.transform.position = v;
				//parentObject.transform.position = v;

				//transform.TransformPoint(collider.bounds.center) 
				//instance.transform.TransformPoint(collider.bounds.center);


				//Hakee Jsonista värin ja asettaa sen Objektille
				Color MyColour = Color.clear; ColorUtility.TryParseHtmlString (cubes [i].Color, out MyColour);
				instance.GetComponentInChildren<Renderer> ().material.color = MyColour;

                models.Add(instance);
			}

        }
        else
        {
            Debug.Log("WWW Error: " + www.error);
        }
    
    }
}