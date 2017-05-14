using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour {

	Animator anim;

	[SerializeField] int maxlife;
	public int life;
	[SerializeField] int recoverytime;

	
	bool isBroken = false;
	public bool isRecovered = true;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		life = maxlife;
	}
	
	// Update is called once per frame
	void Update () {
		if(!isBroken && !isRecovered){
			GetUp();
			isRecovered = true;
		}
	}

	void GetUp(){
		anim.SetBool("broken", false);
		life = maxlife;
	}

	public void Attacked(){
		life--;
		if(life<=0){
			FallDown();
			StartCoroutine("WaitForRecover");
		}
	}

	void FallDown(){
		anim.SetBool("broken", true);
		isBroken = true;
		isRecovered = false;
	}

	IEnumerator WaitForRecover(){
		yield return new WaitForSeconds(recoverytime);
		isBroken = false;
	}
}
