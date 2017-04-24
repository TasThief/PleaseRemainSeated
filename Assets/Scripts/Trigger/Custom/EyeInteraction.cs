using UnityEngine;

[RequireComponent(typeof(Collider))]
public class EyeInteraction : OneWaySwitch {
    public float delayToTrigger;
    private float enteringTime;

    public void OnMouseOver() {
        if(Time.time - enteringTime > delayToTrigger) {
            Activate();
        }
    }

    public void OnMouseEnter() {
        enteringTime = Time.time;
    }

    protected override void DrawIcon() {
        Gizmos.DrawIcon(transform.position, "SW_EYE.png", true);
    }
}