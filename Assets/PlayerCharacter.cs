using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour {
    private int _health;
    public int maxHealth = 5;
    // Use this for initialization
    void Start() {
        _health = maxHealth;
    }

    // Update is called once per frame
    void Update() {

    }

    public void Hurt(int damage) {
        _health -= damage;
        Debug.Log("Health: " + _health);
    }
}
