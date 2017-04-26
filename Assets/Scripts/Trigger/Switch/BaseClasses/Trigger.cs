using UnityEngine;

public abstract class Switch : MonoBehaviour {
    [SerializeField]
    protected bool on = true;

    public bool On {
        get { return on; }
        private set {
            if(on != value) {
                on = value;
                if(on) {
                    if(onSetStateOnEvent != null)
                        onSetStateOnEvent();
                }
                else
                    if(onSetStateOffEvent != null)
                        onSetStateOffEvent();
            }
        }
    }

    protected event System.Action onSetStateOnEvent;

    protected event System.Action onSetStateOffEvent;

    [SerializeField]
    public bool oneShot = false;

    [SerializeField]
    protected Switch[] connectedList;

    public void Ibernate() {
        On = false;
    }

    public void Wake() {
        On = true;
    }

    public void Sleep(float sleepDuration) {
        On = false;
        Invoke("Wake", sleepDuration);
    }

    public void Activate() {
        if(On) {
            Action();
            if(oneShot)
                Ibernate();
        }
    }

    protected virtual void Broadcast() {
        foreach(Switch connected in connectedList)
            connected.Activate();
    }

    protected virtual void Action() {
        Broadcast();
    }

    protected virtual void DrawIcon() {}

    protected virtual void OnStart() {}

    private void Start() {
        OnStart();
    }

    private void OnDrawGizmos() {
        DrawIcon();
        if(connectedList != null && connectedList.Length != 0) {
            Gizmos.color = Color.black;
            foreach(Switch connected in connectedList) {
                Gizmos.DrawLine(transform.position, connected.transform.position);
                Gizmos.DrawLine(transform.position + Vector3.left * 0.04f, connected.transform.position);
                Gizmos.DrawLine(transform.position + Vector3.up * 0.04f, connected.transform.position);
                Gizmos.DrawLine(transform.position + Vector3.down * 0.04f, connected.transform.position);
                Gizmos.DrawLine(transform.position + Vector3.right * 0.04f, connected.transform.position);
            }
        }
    }
}

public interface IBinary {
    bool State { get; set; }
    void TurnOn();
    void TurnOff();
}