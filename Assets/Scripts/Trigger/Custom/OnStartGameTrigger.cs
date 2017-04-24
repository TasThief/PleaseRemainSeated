using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnStartGameTrigger : OneWaySwitch {

	// Use this for initialization
	void Start () {
        Invoke("Activate",1f);	
	}

    
}
