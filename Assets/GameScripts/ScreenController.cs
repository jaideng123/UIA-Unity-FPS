using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenController : MonoBehaviour {
	[SerializeField] private GameObject enemyPrefab;

	public int cloneLimit = 3;
	private GameObject[] _enemies;
	// Use this for initialization
	void Start () {
		_enemies = new GameObject[cloneLimit];
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < _enemies.Length; i++)
		{
			if(_enemies[i] == null){
			_enemies[i] = Instantiate(enemyPrefab) as GameObject;
			_enemies[i].transform.position = new Vector3(0,1,0);
			float angle = Random.Range(0,360);
			_enemies[i].transform.Rotate(0,angle,0);

		}	
		}
	}
}
