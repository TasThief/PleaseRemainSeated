using UnityEngine;

[RequireComponent(typeof(Collider))]
public class EyeInteraction : ReticleInteractionTrigger {
    public float delayToTrigger;

    private float enteringTime;

    protected override void OnStart() {
        base.OnStart();
        reticleType = CrosshairType.Fly;
        crosshairFadeInTime = delayToTrigger;
        onSetStateOffEvent += () => CrosshairManager.singleton.SetOff(CrosshairType.Fly);
    }
    protected override void OnMouseEnterEx() {
        enteringTime = Time.time;
    }

    public void OnMouseOver() {
        if(Time.time - enteringTime > delayToTrigger) {
            Activate();
        }
    }

    protected override void DrawIcon() {
        Gizmos.DrawIcon(transform.position, "SW_EYE.png", true);
    }
}