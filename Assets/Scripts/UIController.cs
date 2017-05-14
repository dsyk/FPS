using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

	[SerializeField] Gun Gun;
    [SerializeField] ScoreController ScoreController;
	[SerializeField] Canvas canvas;
	[SerializeField] Text timeText;
	[SerializeField] Text ptText;
	[SerializeField] Text bulletBoxText;
	[SerializeField] Text bulletText;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		timeText.text = "Time：" + (90-Time.time).ToString("f1") + "s";
		ptText.text = "Pt：" + ScoreController.totalScore;
		bulletBoxText.text = "BulletBox：" + Gun.bulletBox;
		bulletText.text = "Bullet：" + Gun.residualBullet + "/" + Gun.bullet;
	}
}
