﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpawner : MonoBehaviour {
	public GameObject[] fireObjects;
	[Range(0,10)]
	public int fireChance;
	public Vector2 minSpawnPoint;
	public Vector2 maxSpawnPoint;
	public float fireCooldown;
	public FireSpawnPoint[] spawnPoints;
	private int fireCount;
	// Use this for initialization
	void Start () {
		fireCount = 0;
		StartCoroutine(SpawnFire());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	IEnumerator SpawnFire() {
		yield return new WaitForSeconds(fireCooldown);
		if (fireCount <= spawnPoints.Length) {
			int spawnCount = Random.Range(0,fireObjects.Length);
			if (IsSpawnFire()) {
				int spawnPointIndex = GenerateSpawnPointIndex();
				if (spawnPointIndex != -1) {
					Vector3 spawnPosition = spawnPoints[spawnPointIndex].transform.position;
					GameObject fire = Instantiate(fireObjects[spawnCount],spawnPosition,this.transform.rotation);
					fire.GetComponent<Fire>().spawnPoint = spawnPoints[spawnPointIndex];
					fireCount = fireCount + 1;
				}
			}
		}
		StartCoroutine(SpawnFire());
	}
	int GenerateSpawnPointIndex() {
		int tryCount = 0;
		int choosenSpawnPoint = Random.Range(0,spawnPoints.Length);
		while (spawnPoints[choosenSpawnPoint].isOnFire && tryCount < spawnPoints.Length) {
			choosenSpawnPoint = Random.Range(0,spawnPoints.Length);
			tryCount = tryCount + 1;
		}
		if (tryCount == spawnPoints.Length) {
			return (-1);
		} else {
			return (choosenSpawnPoint);
		}
	}

	bool IsSpawnFire() {
		int randomNumber;
		randomNumber = Random.Range(0,10);
		return (randomNumber <= fireChance);
	}
}
