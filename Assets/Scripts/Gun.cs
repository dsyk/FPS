using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {
	
	//弾薬数
	public int bullet;
	public int bulletBox;
	int residualBullet;
	int reloadBullet;

	//クールタイム
	[SerializeField] float coolTime;
	float resetCoolTime;

	//リロード
	bool isReload = false;

	//オーディオ
	[SerializeField] AudioClip shootSound;
	[SerializeField] AudioClip reloadSound;
	AudioSource audioSource;

	//パーティクル
	[SerializeField] GameObject muzzleSparkle;
	[SerializeField] GameObject hitSparkle;
	[SerializeField] GameObject Muzzle;
	GameObject muzzleEffect;
	GameObject hitEffect;

	//ターゲット
	[SerializeField] GameObject target;
	[SerializeField] GameObject HeadMaker;
	float hitDistance;

	//Ray
	public RaycastHit hit;

	//Score
	[SerializeField] GameObject ScoreManager;


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
		hit = new RaycastHit();
			
		muzzleEffect = (GameObject)Instantiate (muzzleSparkle, Muzzle.transform.position, Muzzle.transform.rotation);
		Destroy (muzzleEffect, .2f);
		
		if (Physics.Raycast(ray, out hit)) {
			hitEffect = (GameObject)Instantiate (hitSparkle, hit.point, Muzzle.transform.rotation);
			Destroy (hitEffect, .2f);

			if(hit.collider.gameObject.transform.root.gameObject == target　&& target.GetComponent<TargetController>().isRecovered){
				hitDistance = Vector3.Distance(HeadMaker.transform.position, hit.point);
				target.GetComponent<TargetController>().Attacked();
				ScoreManager.GetComponent<ScoreController>().ScorePlus(hitDistance);
				}
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
