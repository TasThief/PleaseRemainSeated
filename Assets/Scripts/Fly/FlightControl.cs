using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FlightControl : MonoBehaviour {

    public static FlightControl 
        singleton;

    [SerializeField]
    public FlightControlModule
        proceduralModule;

    [HideInInspector]
    public FlightControlModule 
        scriptedModule;

    //-----------------Inspector Variables-----------------
    [SerializeField]
    private float
        flightTargetIntervalMin = 1f,
        flightTargetIntervalMax = 10f,
        flightForceMin = 5f,
        flightForceMax = 10f,
        avoidanceRadius = 2f,
        avoidanceForceScale = 10f;

    //-----------------Internal variables-----------------
    [HideInInspector]
    private float
        targetFlightForce,
        proceduralToScriptMeter;

    [HideInInspector]
    private Rigidbody 
        rb;

    [HideInInspector]
    private RaycastHit 
        hitInfo;

    [HideInInspector]
    private Collider[] 
        avoidanceColliders;

    [HideInInspector]
    private bool 
        isFlying;


    //-----------------Properties-----------------
    public bool IsFlying {
        get { return isFlying; }
        set {
            if(isFlying != value) {
                isFlying = value;
                if(isFlying) {
                    StartCoroutine(SetFlightTarget());
                    StartCoroutine(SetFlightForce());
                    StartCoroutine(SetAvoidanceForce());
                }
            }
        }
    }

    public Vector3 FlightTarget {
        get {   return proceduralModule.target.position;    }
        set {   proceduralModule.target.position = value; }
    }

    public bool IsScripted { get; set; }


    //-----------------Public methods-----------------
    public void ForceFlightPathChange() {
        StartCoroutine(PickFlightTarget());
    }

    //-----------------Coroutines-----------------
    private IEnumerator PickFlightTarget() {
        yield return new WaitUntil(() => Physics.Raycast(FPCamera.camera.transform.position, FPCamera.camera.transform.forward, out hitInfo));
        FlightTarget = ((hitInfo.point - FPCamera.camera.transform.position) * Random.Range(0.3f, 0.8f)) + FPCamera.camera.transform.position;
    }

    private IEnumerator WaitForRandomSeconds() {
        yield return new WaitForSeconds(Random.Range(flightTargetIntervalMin, flightTargetIntervalMax));
    }

    private IEnumerator SetFlightForce() {
        while(IsFlying) {
            targetFlightForce = Random.Range(flightForceMin, flightForceMax);
            yield return WaitForRandomSeconds();
        }
    }

    private IEnumerator SetFlightTarget() {
        while(IsFlying) {
            yield return PickFlightTarget();
            yield return WaitForRandomSeconds();
        }
    }

    private IEnumerator SetAvoidanceForce() {
        while(IsFlying) {
            avoidanceColliders = Physics.OverlapSphere(transform.position, avoidanceRadius);
            yield return new WaitForSeconds(0.1f);
        }
    }


    //-----------------Internal methods-----------------
    private Vector3 CalculateProceduralFlightForce() {
        Vector3 result = Vector3.zero;//rb.velocity;

        Vector3 impactPoint;

        //calculate total avoidance force
        foreach(Collider collider in avoidanceColliders) {
            if(collider != null) {
                impactPoint = collider.ClosestPoint(transform.position);
                result += (transform.position - impactPoint) * (avoidanceRadius - Vector3.Distance(transform.position, impactPoint)) * avoidanceForceScale;
            }
        }
        //calculate current moving speed
        proceduralModule.speed = Mathf.Lerp(proceduralModule.speed, targetFlightForce, 0.2f);

        //calculate "forward" flight force and sum it
        result += proceduralModule.GetDirectionVector(transform.position);

        //return the result vector
        return result;
    }

    //-----------------Unity Messages-----------------
    private void Start() {
        singleton = this;

        rb = GetComponent<Rigidbody>();

        proceduralModule.target = new GameObject().transform;
        proceduralModule.target.SetParent(transform.parent);
        proceduralModule.target.name = "Fly's Flight Target";
        proceduralModule.drag = rb.drag;

        avoidanceColliders = new Collider[0];

        IsFlying = true;
    }

    private void Push(float meterTarget, FlightControlModule module, System.Func<Vector3> forceFunction) {
        proceduralToScriptMeter = Mathf.Lerp(proceduralToScriptMeter, meterTarget, module.lerpSpeed);
        rb.AddForce(forceFunction());
        rb.drag = Mathf.Lerp(rb.drag, module.drag, 0.1f);
        rb.velocity = module.ClampForce(rb.velocity);
    }

    private void Update() {
        if(IsScripted) 
            Push(1f, scriptedModule, () => 
                Vector3.Lerp(CalculateProceduralFlightForce(), scriptedModule.GetDirectionVector(transform.position), proceduralToScriptMeter));
        else
            Push(0f, proceduralModule, CalculateProceduralFlightForce);
    }
}
