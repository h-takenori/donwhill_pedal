using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprite : MonoBehaviour {

	public Transform playerBall;    // ターゲットへの参照
	public Transform camera;    // ターゲットへの参照

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<Transform>().position = playerBall.position;
	}
}
