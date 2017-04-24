using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyNearTrigger : OneWaySwitch {
    [SerializeField]
    private float threshold;
	
	// Update is called once per frame
	void Update () {
        if(FlightControl.singleton != null)
            if (Vector3.Distance(FlightControl.singleton.transform.position, transform.position) < threshold)
                Activate();
	}
}
