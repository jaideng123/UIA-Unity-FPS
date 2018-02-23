using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBackground : MonoBehaviour {
    private bool hidden;

    // Use this for initialization
    void Start() {
        hidden = true;
        gameObject.SetActive(!hidden);
    }
    public void Toggle() {
        hidden = !hidden;
        gameObject.SetActive(!hidden);
    }

    // Update is called once per frame
    void Update() {

    }
}
