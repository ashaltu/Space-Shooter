using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;

}
public class PController : MonoBehaviour
{

    public Rigidbody rigidBody;
    public Boundary boundary;
    public float speed;
    public float tilt;
	public float fireRate = 0.5f;
	private float nextFire = 0.0f;

	public GameObject shot;
	public Transform shotSpawn;

	public AudioSource weaponShoot;
	 

	void Update(){
		if(Input.GetButton("Shoot") && Time.time > nextFire){
			nextFire = Time.time + fireRate;
			Instantiate(shot,shotSpawn.position,shotSpawn.rotation);
			weaponShoot.Play();
		}
	}
	
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        rigidBody.velocity = new Vector3(moveHorizontal, 0.0f, moveVertical) * speed;
        rigidBody.position = new Vector3
        (
            Mathf.Clamp(rigidBody.position.x, boundary.xMin, boundary.xMax),
            0.0f,
            Mathf.Clamp(rigidBody.position.z, boundary.zMin, boundary.zMax)
        );

        rigidBody.rotation = Quaternion.Euler(0.0f, 0.0f, rigidBody.velocity.x * -tilt);

    }

}