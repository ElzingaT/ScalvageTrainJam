using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatBar : MonoBehaviour
{
    public PlayerShipController player;
    public StatMeter shields;
    public StatMeter hull;
    public StatMeter engine;

	void Start ()
    {
        player = GameObject.FindObjectOfType<PlayerShipController>();
    }
	
	void Update ()
    {
        UpdateStatMeters();
	}

    public void UpdateStatMeters()
    {
        // Shield
        shields.SetValue(player.shields);
        // Hull
        hull.SetValue(player.hull);
        // Boost / Engine
        if (player.engine is NoEngine)
            engine.gameObject.GetComponent<StatMeter>().SetValue(0);
        else if (player.engine is DamagedEngine)
            engine.gameObject.GetComponent<StatMeter>().SetValue(1);
        else
            engine.gameObject.GetComponent<StatMeter>().SetValue(3);
    }
}
