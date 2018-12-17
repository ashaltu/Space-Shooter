using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnContact : MonoBehaviour {

	public GameObject tinyAsteroid;
	public GameObject mediumAsteroid;
	public GameObject explosion;
	public GameObject playerExplosion;
	public int scoreValue;
	public bool isBig;
	public bool isMedium;
	private GameController gameController;

	void Start(){
		GameObject gameControllerObject = GameObject.FindWithTag("GameController");
		if(gameControllerObject != null){
			gameController = gameControllerObject.GetComponent<GameController>();
		}
		if(gameControllerObject==null){
			Debug.Log("Can not find 'GameController' script");
		}
	}

	void OnTriggerEnter(Collider collider){
		if(collider.tag == "Boundary"){
			return;
		}
		Vector3 offset = new Vector3(1,0,0);
		Instantiate(explosion, transform.position, transform.rotation);
		if(isBig){
			Instantiate(mediumAsteroid, transform.position + offset, Quaternion.identity);
			Instantiate(mediumAsteroid, transform.position - offset, Quaternion.identity);
		}else if(isMedium){
			Instantiate(tinyAsteroid, transform.position + offset, Quaternion.identity);
			Instantiate(tinyAsteroid, transform.position - offset, Quaternion.identity);
		}
		if(collider.tag == "Player"){
			Instantiate(playerExplosion, collider.transform.position, collider.transform.rotation);
			gameController.GameOver();
		}
		gameController.AddScore(scoreValue);
		Destroy(this.gameObject);
		Destroy(collider.gameObject);
	}
}
