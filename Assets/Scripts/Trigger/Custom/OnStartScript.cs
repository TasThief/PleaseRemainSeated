using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnStartFlyScript : MonoBehaviour {
    private void Update()
    {
      if(FlightControl.singleton != null)
        {
            GetComponent<ScriptedFlightControl>().StartScript();
            Destroy(this);
        }
    }
}
