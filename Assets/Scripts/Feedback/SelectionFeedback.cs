using UnityEngine;
using cakeslice;
[RequireComponent(typeof(Outline))]
public class SelectionFeedback : MonoBehaviour {
    Outline outline;
    public void OnMouseEnter() {
        if(enabled)
            outline.enabled = true;
    }

    public void OnMouseExit() {
        if(enabled)
            outline.enabled = false;
    }

    public void Disable() {
        enabled = false;
        outline.enabled = false;
    }

    void Start() {
        outline = GetComponent<Outline>();
        outline.enabled = false;
    }
}
