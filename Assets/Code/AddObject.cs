using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddObject : MonoBehaviour {
    List<GameObject> cubes;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey(KeyCode.B))
        {
            Vector3 v = new Vector3(-3, 0.5f, 0);

            if (cubes.Count > 0)
            {
                GameObject last = cubes[cubes.Count - 1];
                v.x = last.transform.position.x + 2;
            }

            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.position += v;

            cube.GetComponent<Renderer>().material.color = Color.black;
            cubes.Add(cube);
        }
	}
}
