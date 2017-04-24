using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class BinaryEyeInteraction : BinarySwitch {
    public float
        delayToTriggerIn,
        delayToTriggerOut;

    private float
        enterTime,
        exitTime;

    private string icon = "_CLOSE";

    public void OnMouseOver() {
        if(Time.time - enterTime > delayToTriggerIn) {
            TurnOn();
            icon = "";
            enterTime = Time.time;
        }
    }

    public void OnMouseEnter() {
        if(Time.time - exitTime > delayToTriggerOut)
            enterTime = Time.time;
        CancelInvoke();
    }

    public void OnMouseExit() {
        exitTime = Time.time;
        Invoke("Close", delayToTriggerOut);
    }

    public void Close() {
        TurnOff();
        icon = "_CLOSE";
    }
 
    protected override void DrawIcon() {
        Gizmos.DrawIcon(transform.position, "SW_EYE"+icon+".png", true);
    }
}