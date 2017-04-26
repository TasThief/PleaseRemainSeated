using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour {
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
    }

    void Update() {
        if(Time.time - cdTime > cd && Input.GetMouseButtonDown(1))
            Fire();
    }
}
