using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class ClipString : MonoBehaviour {

    public Vector3 start;
    LineRenderer line;

	// Use this for initialization
	void Start () {
		line = GetComponent<LineRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        line.SetPosition(0, start);
        line.SetPosition(1, transform.position);
	}



/*  
    public float vertexWidth;
    public List<Vector3> position;

	void Start () {
		line = GetComponent<LineRenderer>();
        StartCoroutine(BuildString());
	}

	void Update () {
        UpdateLine();
	}

    IEnumerator BuildString() {
        position = new List<Vector3>();
        position.Add(start);
        while(true) {
            yield return new WaitUntil(
                () => Vector3.Distance(position[position.Count - 1], transform.position) > vertexWidth);
            position.Add(transform.position);
            line.positionCount = position.Count + 1;
            line.SetPosition(line.positionCount - 1, transform.position);
        }
    }

    private void UpdateLine() {
        for(int i = 0; i < position.Count; i++)
            line.SetPosition(i, position[i]);
        line.SetPosition(line.positionCount - 1, transform.position);
    }
*/

}
