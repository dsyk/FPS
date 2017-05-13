using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {
	
	public int bullet;
	public int bulletBox;
	int residualBullet;
	int reloadBullet;

	[SerializeField] float coolTime;
	float resetCoolTime;

	bool isReload = false;

	[SerializeField] AudioClip shootSound;
	[SerializeField] AudioClip reloadSound;
	AudioSource audioSource;

	[SerializeField] GameObject muzzleSparkle;
	[SerializeField] GameObject hitSparkle;
	[SerializeField] GameObject Muzzle;
	GameObject muzzleEffect;
	GameObject hitEffect;


	// Use this for initialization
	void Start () {
		resetCoolTime = coolTime;
		residualBullet = bullet;
		audioSource = transform.GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		coolTime -= Time.deltaTime;
		reloadBullet = bullet - residualBullet;

		if (Input.GetMouseButton(0) && residualBullet != 0) {
			if (coolTime <= 0f && isReload == false){
				Shoot();
			}
		}

		if (Input.GetKeyDown (KeyCode.R) && residualBullet < bullet && bulletBox > 0) {
			isReload = true;
			Reload ();
			StartCoroutine ("WaitForReload");
		}

	}

	void Shoot () {
		audioSource.PlayOneShot (shootSound);
		residualBullet -= 1;
		coolTime = resetCoolTime;

		Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
		RaycastHit hit = new RaycastHit();
			
		muzzleEffect = (GameObject)Instantiate (muzzleSparkle, Muzzle.transform.position, Muzzle.transform.rotation);
		Destroy (muzzleEffect, .2f);
		
		if (Physics.Raycast(ray, out hit)) {
			hitEffect = (GameObject)Instantiate (hitSparkle, hit.point, Muzzle.transform.rotation);
			Destroy (hitEffect, .2f);
		}
	}


	 void Reload(){
        audioSource.PlayOneShot (reloadSound);
        residualBullet += reloadBullet;
        bulletBox -= reloadBullet;
    }

	IEnumerator WaitForReload(){
		yield return new WaitForSeconds (3);
		isReload = false;
	}


}
