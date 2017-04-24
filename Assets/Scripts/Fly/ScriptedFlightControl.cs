using UnityEngine;

public class ScriptedFlightControl : MonoBehaviour {

    [SerializeField]
    public FlightControlModule scriptedFlightModule;

    public void StartScript() {
        if (FlightControl.singleton != null)
        {
            FlightControl.singleton.scriptedModule = scriptedFlightModule;
            FlightControl.singleton.IsScripted = true;
        }
    }

    public void StopScript() {
        if(FlightControl.singleton != null)
            FlightControl.singleton.IsScripted = false;
    }

    public void OnDisable() {
        if(FlightControl.singleton != null)
            FlightControl.singleton.IsScripted = false;
    }

}
