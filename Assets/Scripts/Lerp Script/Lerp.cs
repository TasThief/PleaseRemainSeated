using UnityEngine;

public class Lerp : MonoBehaviour {

    public Transform startPos;
    public Transform endPos;
    [Range(0f,1f)]
    public float openTime;
    private Transform goal;

    public void Open()
    {
        Debug.Log("boo");
        goal = endPos;
    }
    public void Close()
    {
        goal = startPos;
    }


	// Use this for initialization
	void Start () {
        Close();
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = Vector3.Lerp(transform.position, goal.position, openTime); 
	}
}
