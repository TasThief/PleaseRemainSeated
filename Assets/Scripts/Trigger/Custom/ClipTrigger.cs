using UnityEngine;


public class ClipTrigger : OutlineTrigger {
    public Transform snatchPoint;
    public bool onFocus = false;


    private Transform auxTransform;
    public void OnTriggerEnter(Collider other) {
        if(!Shooter.IsHooked(this) && other.GetComponent<ClipString>()) {
            Shooter.Hook(this);
            UseOutline = true;
            reticleType = CrosshairType.Aim;
            Destroy(other.GetComponent<Rigidbody>());
            Destroy(other.GetComponent<Collider>());
            auxTransform = other.transform;
            auxTransform.SetParent(snatchPoint);
            StartCoroutine(Stopwatch.PlayUntilReady(0.6f, t => {
                if(auxTransform != null)
                    auxTransform.transform.position = Vector3.MoveTowards(auxTransform.transform.position, snatchPoint.position, t);
            }));
        }
    }

    protected override void OnReticleOff() {
        base.OnReticleOff();
        onFocus = false;
    }

    protected override void OnReticleOn() {
        base.OnReticleOn();
        onFocus = true;
    }

    public void Letlose() {
        if(Shooter.IsHooked(this)) {
            Shooter.Hook(null);
            UseOutline = false;
            OnReticleOff();
            reticleType = CrosshairType.Nothing;
        }
    }

    protected override void DrawIcon() {
        Gizmos.DrawIcon(transform.position, "SW_CLIP.png", true);
    }

    protected override void OnStart() {
        base.OnStart();
        reticleType = CrosshairType.Nothing;
        UseOutline = false;
    }
}
