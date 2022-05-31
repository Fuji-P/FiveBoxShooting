using UnityEngine;
using System.Collections;

public class PlayerProjectile : MonoBehaviour {
	public GameObject shoot_effect;
	public GameObject hit_effect;

	// Use this for initialization
	void Start ()
	{
		//⓯5倍に変更
		transform.localScale = new Vector3(5, 5, 1);
		Instantiate(shoot_effect, transform.position, Quaternion.identity); //Spawn muzzle flash
		Destroy(gameObject, 3f); 
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
	
	
	void OnTriggerEnter2D(Collider2D col)
	{

		//Don't want to collide with the ship that's shooting this thing, nor another projectile.
		if (col.gameObject.tag == "Asteroid" ||
			col.gameObject.tag == "Enemy")
		{
			Instantiate(hit_effect, transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
	}
}
