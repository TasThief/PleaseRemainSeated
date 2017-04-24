using UnityEngine;
using System.Collections;
[RequireComponent(typeof(AudioSource))]
public class AudioTrigger : OneWaySwitch {
    AudioSource source;

    protected override void Action() {
        source.Play();
        StartCoroutine(PlayCallback());
    }
    IEnumerator PlayCallback() {
        yield return new WaitWhile(() => source.isPlaying);
        onActivate.Invoke();
        Broadcast();
    }
    protected override void DrawIcon() {
        Gizmos.DrawIcon(transform.position, "SW_AUDIO.png", true);

    }
}
