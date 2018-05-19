using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Region : MonoBehaviour {

	public int x;
	public int y;

	public Transform possibleSpawn;

	void OnDrawGizmosSelected() {
		Gizmos.color = new Color(1, 0, 0, 0.5f);
		Gizmos.DrawCube(transform.position, new Vector3(x, y, 1));
	}
}
