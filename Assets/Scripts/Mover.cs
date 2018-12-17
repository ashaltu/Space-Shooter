using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {

	public Rigidbody rigidBody;
	public float speed;
	
	void Start(){
		rigidBody.velocity = transform.forward * speed;
	}

}
