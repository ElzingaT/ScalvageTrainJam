using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {

	public List<GameObject> availRegions;
	public GameObject startingRegion;
	public Transform[] spots;
	public GameObject player;

	public Region[] regions = new Region[9];

	float[] rotations = {0,90,180,270};

	public List<Transform> possibleSpawns;

	public List<GameObject> components;

	// Use this for initialization
	void Start () {
		for(int i = 0; i< spots.Length; i++){
			if(i == 4){
				GameObject region = Instantiate(startingRegion, spots[i].transform.position, spots[i].transform.rotation);
				regions[i] = region.GetComponent<Region>();
				player.transform.position = spots[i].transform.position;
			} else{
				int index1 = Random.Range(0,availRegions.Count);
				int rotationNum = Random.Range(0,4);
				GameObject region = Instantiate(availRegions[index1], spots[i].transform.position, spots[i].transform.rotation);
				regions[i] = region.GetComponent<Region>();
				region.transform.Rotate(new Vector3(0,0,rotations[rotationNum]));
				availRegions.RemoveAt(index1);
				if(regions[i].possibleSpawn != null){
					possibleSpawns.Add(regions[i].possibleSpawn);
				}
			}
		}
		//Spawn in the components here
		for(int i = 0; i < components.Count; i++){
			int index2 = Random.Range(0, possibleSpawns.Count);
			GameObject comp = Instantiate(components[i], possibleSpawns[i].position, possibleSpawns[i].rotation);
			possibleSpawns.RemoveAt(index2);
		}
	}
}
