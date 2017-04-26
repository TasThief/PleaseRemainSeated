using UnityEngine;

public class MouseInteraction : OutlineTrigger {

    protected override void DrawIcon() {
        Gizmos.DrawIcon(transform.position, "SW_MOUSE.png", true);
    }

    public void OnMouseDown() {
        Activate();
    }

    protected override void OnStart() {
        base.OnStart();
        reticleType = CrosshairType.Hand;
    }
}
