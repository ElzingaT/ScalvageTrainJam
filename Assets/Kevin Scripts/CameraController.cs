using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public Transform player;

	void Update(){
		Vector3 temp = new Vector3(player.position.x, player.position.y,-10f);
		transform.position = temp;
	}
}
