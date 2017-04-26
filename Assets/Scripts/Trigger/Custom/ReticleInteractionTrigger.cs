using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class ReticleInteractionTrigger : OneWaySwitch {

    protected float crosshairFadeInTime = 0.4f;

    protected CrosshairType reticleType = CrosshairType.Nothing;

    protected virtual void OnReticleOn()  {
        CrosshairManager.singleton.SetOn(reticleType, crosshairFadeInTime);
    }

    protected virtual void OnReticleOff() {
        CrosshairManager.singleton.SetOff(reticleType);
    }

    protected virtual void OnMouseEnterEx() { }

    protected virtual void OnMouseExitEx() { }

    public void OnMouseEnter() {
        if(On)
            OnReticleOn();
        OnMouseEnterEx();
    }

    public void OnMouseExit() {
        OnReticleOff();
        OnMouseExitEx();
    }

    public void Disable() {
        OnReticleOff();
    }

    protected override void OnStart() {
        base.OnStart();
        onSetStateOffEvent += Disable;
    }
}
