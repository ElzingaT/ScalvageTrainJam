using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagedEngine : Engine {

	//Fire for short burts of low power.

	//Randomly cuts out
	//Maybe have a sputter up time after cut out before having a steady stream of power

	float maxThrust = 10f;
	[SerializeField]
	float thrustForce;
	public float thrustFactor = 5f;
	bool breakable;
	[SerializeField]
	float breakTimer;
	public float breakCooldown = 2f;

	float sputterTimer;
	float sputterCooldown = 5f;

	void Update(){
		if(!breakable){
			breakTimer -= Time.deltaTime;
			if(breakTimer <= 0 ){
				breakable = true;
			}
		}
	}

	public override float EngineThrust(){
		// if(breakable){
		// 	int sputter = Random.Range(1,100);
			// if(sputter % 50 == 0){
			// 	breakable = false;
			// 	breakTimer = breakCooldown;
			// 	thrustForce = 0f;	
			// } else{
				thrustForce += thrustFactor * Time.deltaTime;
				if(thrustForce > maxThrust){
					thrustForce = maxThrust;
				// }
			// }

		}
		
		return thrustForce;
	}
}
