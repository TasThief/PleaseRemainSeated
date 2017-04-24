using UnityEngine;
using cakeslice;

public class OutlineAnimation : MonoBehaviour
{
    OutlineEffect effect;
    void Awake() {
        effect = GetComponent<OutlineEffect>();
    }
    void Update(){
        effect.lineColor0.a = Mathf.PingPong(Time.time, 1);
        effect.UpdateMaterialsPublicProperties();
    }
}
