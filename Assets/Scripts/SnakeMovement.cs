﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SnakeMovement : MonoBehaviour {

	public List<Transform> bodyParts = new List<Transform> ();
	public float rotationSensitivity = 50.0f;
	public float speed = 3.5f;
	public float boostCharge = 100f;
	public Text boostText;

	// Use this for initialization
	void Start () {
		SpawnPlayer ();
		boostText.text = "Boost: " + boostCharge + "%";
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.A)) {
			currentRotation += rotationSensitivity * Time.deltaTime;
		}
		if (Input.GetKey (KeyCode.D)) {
			currentRotation -= rotationSensitivity * Time.deltaTime;
		}
		if (Input.GetKey (KeyCode.Space) && boostCharge > 1) {
			speed = 7.0f;
			boostCharge = boostCharge - 2f;
			setBoostText ();
		} else { 
			if (boostCharge < 99) {
				boostCharge = boostCharge + 0.1f;
				setBoostText ();
			}
			speed = 3.5f;
		}
	}

	public int respawnTime = 2;
	void OnTriggerEnter2D(Collider2D other) 
	{
		gameObject.SetActive (false);
		foreach (Transform body in bodyParts) {
			body.gameObject.SetActive (false);
		}
		Invoke("SpawnPlayer", respawnTime);
	}

	void SpawnPlayer() {
		float xPos = Random.Range (-3f, 7.5f);
		float yPos = Random.Range (-3.3f, 3.3f);
		transform.position = new Vector3(xPos, 0, 0);
		gameObject.SetActive (true);
		foreach (Transform body in bodyParts) {
			body.gameObject.transform.position = new Vector3 (xPos, 0, 0);
			body.gameObject.SetActive (true);
		}
	}

	void setBoostText() {
		boostText.text = "Boost: " + Mathf.Ceil(boostCharge) + "%";
	}

	public float currentRotation;
	void FixedUpdate() {
		MoveForward ();
		Rotation ();
	}

	void MoveForward() {
		transform.position += transform.up * speed * Time.deltaTime;
	}

	void Rotation() {
		transform.rotation = Quaternion.Euler (new Vector3 (transform.rotation.x, transform.rotation.y, currentRotation));
	}

	void Boost() {
		speed = 7f;
	}
}
