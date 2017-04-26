using UnityEngine;
using cakeslice;

public class SelectionFeedback : MonoBehaviour {
    [SerializeField]
    private CrosshairType useCroshair = CrosshairType.Nothing;

    [SerializeField]
    private float crosshairFadeInTime = 0.4f;

    [HideInInspector]
    private Outline outline;

    public void OnMouseEnter() {
        EnableOutline(true);
        CrosshairManager.singleton.SetOn(useCroshair, crosshairFadeInTime);
    }

    public void OnMouseExit() {
        EnableOutline(false);
        CrosshairManager.singleton.SetOff(useCroshair);
    }

    public void Disable() {
        enabled = false;
        EnableOutline(false);
        CrosshairManager.singleton.SetOff(useCroshair);
    }

    private void EnableOutline(bool value) {
        if(outline != null)
            outline.enabled = value;
    }

    void Start() {
        outline = GetComponent<Outline>();
        EnableOutline(false);
    }
}

