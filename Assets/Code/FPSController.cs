using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour {

	private float speed = 15f;
    public float sensitivity = 3f;
    public float jumpForce = 4f;
	private bool hasJumped, moveFast;
    private CharacterController player;
	Vector3 move1 = new Vector3(1,2,1);
	Vector3 move2 = new Vector3(1,100,1);
	Vector3 move3 = new Vector3(442,400,1);
	bool flying = false;

    public GameObject eyes;

    private float moveFB, moveLR, rotX, rotY, vertVelocity;

	// Use this for initialization
	void Start () {
        player = GetComponent<CharacterController>();

	}
	
	// Update is called once per frame
	void Update () {

        Movement();

		if (Input.GetButtonDown("Jump")) 
		{
			hasJumped = true;
		}

		if (Input.GetButtonDown ("Fast")) 
		{

			if (moveFast == false) 
			{
				speed = speed * 5;
				moveFast = true;
			} else 
			{
				speed = speed / 5;
				moveFast = false;
			}
		}

		if(Input.GetKeyDown(KeyCode.Alpha1)){
			flying = false;
			player.transform.position = move1;
			speed = 15f;
		}
		if(Input.GetKeyDown(KeyCode.Alpha2)){
			player.transform.position = move2;
			flying = true;
			speed = 50;
		}
		if(Input.GetKeyDown(KeyCode.Alpha3)){
			flying = false;
			player.transform.position = move3;
			speed = 15f;
		}

		ApplyGravity (flying);

    }

    void Movement()
    {
        moveFB = Input.GetAxis("Vertical") * speed;
        moveLR = Input.GetAxis("Horizontal") * speed;

        rotX = Input.GetAxis("Mouse X") * sensitivity;
        rotY = Input.GetAxis("Mouse Y") * sensitivity;

		Vector3 movement = new Vector3(moveLR, vertVelocity, moveFB);

        transform.Rotate(0, rotX, 0);
		eyes.transform.Rotate (-rotY, 0, 0);

        movement = transform.rotation * movement;

		//eyes.transform.localRotation = Quaternion.Euler(rotY, 0, 0);

        player.Move(movement * Time.deltaTime);
    }

	private void ApplyGravity(bool fly)
	{
		if (player.isGrounded == true) 
		{	
			if (hasJumped == false) 
			{
				vertVelocity = Physics.gravity.y;
			} else 
			{
				vertVelocity = jumpForce;
			}
		} else 
		{
			if(fly){
				vertVelocity = 0;
				hasJumped = false;
			}else{
				vertVelocity += Physics.gravity.y * Time.deltaTime;
				vertVelocity = Mathf.Clamp (vertVelocity, -50f, jumpForce);
				hasJumped = false;
			}

		}
	}
}
