using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour {

	//得点
	public int scoreExcellent;
	public int scoreGreat;
	public int scoreGood;
	public int scoreHit;
	int getScore;
	public int totalScore;

	//ターゲット
	[SerializeField] GameObject HeadMaker;
	float makerSize;

	// Use this for initialization
	void Start () {
		totalScore = 0;
		makerSize = 0.35f;
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void ScorePlus(float dis){
		if(dis < makerSize/3){
			getScore = scoreExcellent;
		}else if(dis < makerSize*2/3){
			getScore = scoreGreat;
		}else if(dis < makerSize){
			getScore = scoreGood;
		}else{
			getScore = scoreHit;
		}
		totalScore += getScore;
	}
}
