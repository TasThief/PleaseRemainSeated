using UnityEngine;
using UnityEngine.UI;

public class CrosshairManager : MonoBehaviour {

    public static CrosshairManager singleton;

    [SerializeField]
    public CrosshairControl[] list;

    [SerializeField]
    private float
        defaultFadeDuration = 1f,
        maxDisplace = 30f;

    [SerializeField]
    private AnimationCurve fadeCurve = null;

    [HideInInspector]
    private CrosshairControl active;

    public void SetOn(CrosshairType index, float? duration = null) {
        float d = (duration == null) ? defaultFadeDuration : duration.Value;
        for(int i = 0; i < list.Length; i++)
            if(i == (int)index)
                list[i].Turn(d, true, fadeCurve);
            else
                list[i].Turn(defaultFadeDuration, false, fadeCurve);
    }

    public void SetOff(CrosshairType index) {
        if(index != CrosshairType.Nothing)
            list[(int)index].Turn(defaultFadeDuration, false, fadeCurve);
    }

    public void Shake(float duration) {
        Quaternion initialPosition = active.transform.rotation;
        float totalDisplace = 0f;
        float displace = 0f;
        StartCoroutine(
            Stopwatch.PlayUntilReady(duration, t => {
                displace = Random.Range(-maxDisplace - totalDisplace, maxDisplace - totalDisplace);
                totalDisplace += displace;
                active.transform.Rotate(0f, 0f, displace);
            },
            () => active.transform.rotation = initialPosition
        ));
    }

    void Start() {
        singleton = this;
    }
   
    
    // [SerializeField]
   // public CrosshairControl fly, hand;
   /* public void SetFly(float duration) {
        hand.Turn(defaultFadeDuration, false, fadeCurve);
        fly.Turn(duration, true, fadeCurve);
    }

    public void SetHand(float duration) {
        hand.Turn(duration, true, fadeCurve);
        fly.Turn(defaultFadeDuration, false, fadeCurve);
    }*/

   /* public void SetNothing() {
        hand.Turn(defaultFadeDuration, false, fadeCurve);
        fly.Turn(defaultFadeDuration, false, fadeCurve);
    }*/
}

[System.Serializable]
public class CrosshairControl {
    [SerializeField]
    public Image crosshair;

    [HideInInspector]
    public Transform transform { get { return crosshair.transform; } }

    [HideInInspector][SerializeField]
    public Coroutine lerpRoutine;

    [HideInInspector][SerializeField]
    public bool state = false;

    public void Turn(float duration, bool newState, AnimationCurve curve) {
        if(newState != state) {
            state = newState;
            if(lerpRoutine != null)
                crosshair.StopCoroutine(lerpRoutine);
            lerpRoutine = Fade(duration, curve);
        }
    }

    private Coroutine Fade(float duration, AnimationCurve fadeCurve) {
        Color finalColor = crosshair.color;
        Color initialColor = crosshair.color;
        finalColor.a = (state) ? 1f : 0f;
        return crosshair.StartCoroutine(
            Stopwatch.PlayUntilReady(duration, t =>
                crosshair.color = Color.Lerp(initialColor, finalColor, fadeCurve.Evaluate(t))));
    }
}

public enum CrosshairType { Nothing = -1, Fly = 0, Hand = 1 }