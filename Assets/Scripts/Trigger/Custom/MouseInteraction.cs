using UnityEngine;

[RequireComponent(typeof(Collider))]
public class MouseInteraction : OneWaySwitch {
    public void OnMouseDown() {
        Activate();
    }

    protected override void DrawIcon() {
        Gizmos.DrawIcon(transform.position, "SW_MOUSE.png", true);
    }
}
