using UnityEngine;
using cakeslice;

public abstract class OutlineTrigger : ReticleInteractionTrigger {

    private bool useOutline = true;

    [HideInInspector]
    private Outline outline;

    protected bool UseOutline {
        get {   return useOutline;  }
        set {
            if(!value)
                EnableOutline(false);
            useOutline = value;
        }
    }

    protected override void OnReticleOn() {
        base.OnReticleOn();
        if(UseOutline)
            EnableOutline(true);
    }

    protected override void OnReticleOff() {
        base.OnReticleOff();
            EnableOutline(false);
    }

    private void EnableOutline(bool value) {
        if(outline != null)
            outline.enabled = value;
    }

    protected override void OnStart() {
        base.OnStart();
        outline = GetComponent<Outline>();
    }
}
