using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TractorBeam : MonoBehaviour {

	public Material mat;

	public float strength;
	public float range;

    public bool tractorActive = false;
    public AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();    
    }

    public void ActivateBeam(){
        tractorActive = true;
        Vector2 origin = new Vector2(transform.position.x,transform.position.y);
        RaycastHit2D[] hits = Physics2D.CircleCastAll(origin, range, Vector2.zero);
        for (int i = 0; i < hits.Length; i++){
			if(hits[i] == true && hits[i].transform.tag != "Player" && hits[i].transform.tag == "Magnetic"){
                Rigidbody2D rb = hits[i].transform.GetComponent<Rigidbody2D>();

				Vector3 heading = transform.position - hits[i].transform.position;
				float dist = heading.magnitude;
				Vector2 direct = heading.normalized;
				//Figure out how to polish this speed
				hits[i].transform.GetComponent<Rigidbody2D>().velocity += direct * (strength * Time.deltaTime);
				GameObject temp = new GameObject();
				LineRenderer lr = temp.AddComponent<LineRenderer>();
				lr.SetPosition(0,origin);
				lr.SetPosition(1,hits[i].transform.position);
				lr.material = mat;
				lr.textureMode = LineTextureMode.Tile;
				lr.startWidth = .3f;
				lr.endWidth = .3f;
				
				Destroy(lr.gameObject,.05f);
			}
		}
	}

    void Update()
    {
        if (audioSource.isPlaying && !tractorActive)
            audioSource.Stop();
        else if (!audioSource.isPlaying && tractorActive)
            audioSource.Play();
    }
}
