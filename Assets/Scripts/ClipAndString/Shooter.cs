using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour {
    private static Shooter singleton;

    void Start() {
        singleton = this;
    }

    [SerializeField]
    private GameObject clip;

    public float cd;

    private float cdTime;

    public float fireForce;

    public float destructionDelay = 1f;

    private GameObject loadedClip;

    public GameObject LoadedClip {
        get {   return loadedClip;  }
        set {
            if(loadedClip != null)
                Destroy(loadedClip, destructionDelay);
            loadedClip = value;
        }
    }

    void Fire() {
        LoadedClip = Instantiate(clip, transform.position, transform.rotation);
        LoadedClip.GetComponent<Rigidbody>().AddForce(LoadedClip.transform.forward * fireForce);
        loadedClip.GetComponent<ClipString>().start = transform.position;
        cdTime = Time.time;
        if(hookedTrigger != null)
            hookedTrigger = null;
    }
    void PushClip() {
        hookedTrigger.Activate();
        hookedTrigger.Letlose();
        Destroy(loadedClip,destructionDelay);
    }

    ClipTrigger hookedTrigger = null;

    public static void Hook(ClipTrigger caller) {
        singleton.hookedTrigger = caller;
    }

    public static bool IsHooked(ClipTrigger caller) {
        return singleton.hookedTrigger != null && singleton.hookedTrigger == caller;
    }

    void Update() {
        if(Input.GetMouseButtonDown(1))
            if(hookedTrigger != null && hookedTrigger.onFocus)
                PushClip();
            else if(Time.time - cdTime > cd)
                Fire();
    }
}
