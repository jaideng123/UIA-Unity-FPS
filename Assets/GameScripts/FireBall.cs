﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour {
    public float speed = 10.0f;
    public int damage = 1;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (GlobalVariables.Paused) {
            return;
        }
        transform.Translate(0, 0, speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other) {
        PlayerCharacter player = other.GetComponent<PlayerCharacter>();
        if (player != null) {
            Debug.Log("Player Hit!");
            player.Hurt(damage);
        }
        Destroy(this.gameObject);
    }
}
