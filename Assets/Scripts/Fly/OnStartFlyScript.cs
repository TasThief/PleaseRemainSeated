using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnStartScript : OneWaySwitch {
    private void Update()
    {
      if(FlightControl.singleton != null)
        {
            FlightControl.singleton.transform.position = transform.position;
            GetComponent<ScriptedFlightControl>().StartScript();
            Destroy(this);
        }
    }
}
