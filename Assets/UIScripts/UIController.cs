using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {
    [SerializeField] private Text scoreLabel;
    [SerializeField] private SettingsPopup settingsPopup;
    [SerializeField] private UIBackground background;
    private int _score;
    // Use this for initialization
    void Start() {
        settingsPopup.Close();
        _score = 0;
        scoreLabel.text = _score.ToString();
    }
    void Update() {
        if (Input.GetKeyDown("escape")) {
            Debug.Log("GlobalVariables.Paused");
            GlobalVariables.Paused = !GlobalVariables.Paused;
            if (background) {
                background.Toggle();
            }
        }
    }
    void Awake() {
        Messenger.AddListener(GameEvent.ENEMY_HIT, OnEnemyHit);
    }
    public void OnDestroy() {
        Messenger.RemoveListener(GameEvent.ENEMY_HIT, OnEnemyHit);
    }
    private void OnEnemyHit() {
        _score += 1;
        scoreLabel.text = _score.ToString();
    }

    public void OnOpenSettings() {
        settingsPopup.Open();
    }
}
