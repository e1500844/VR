using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class Get : MonoBehaviour
{

    void Start()
    {
        string url = "http://www.cc.puv.fi/~e1500844/cord.json";
        WWW www = new WWW(url);
        StartCoroutine(WaitForRequest(www));
        
    }

    IEnumerator WaitForRequest(WWW www)
    {
        yield return www;

        // check for errors
        if (www.error == null)
        {
            Debug.Log(www.text);
           
            string jono = www.text;

            //var N = JSON.Parse(www.text);

            int count = jono.Split('x').Length - 1;

			Cube[] cubes = Cube.CreateCube (jono, count);

            Debug.Log(cubes[0].Y);
			Debug.Log(cubes[1].X);

			for (int i = 0; i < count; i++) 
			{
				//Hakee Resources kansiosta Palikka nimisen objektin, jatkossa voi käyttää olion type muuttujaa
				GameObject instance = Instantiate (Resources.Load ("palikka", typeof(GameObject))) as GameObject;

				//GameObject c = GameObject.CreatePrimitive (PrimitiveType.Cube);
				//Asettaa objektin sijainnin
				Vector3 v = new Vector3 (cubes[i].X, cubes[i].Y, cubes[i].Z);
				instance.transform.position = v;

				//Hakee Jsonista värin ja asettaa sen Objektille
				Color MyColour = Color.clear; ColorUtility.TryParseHtmlString (cubes [i].Color, out MyColour);
				instance.GetComponentInChildren<Renderer> ().material.color = MyColour;

			}

        }
        else
        {
            Debug.Log("WWW Error: " + www.error);
        }
    
    }
    
}
