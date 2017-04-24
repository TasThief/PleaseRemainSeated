using UnityEngine;

public class FPCamera : MonoBehaviour {
    [SerializeField]
    private float verticalNeckLimit, horizontalNeckLimit, verticalNeckPadding, horizontalNeckPadding;

    [SerializeField]
    private float cameraSpeedX = 5, cameraSpeedY = 3;

    [SerializeField]
    private bool invertCamera = true;

    [HideInInspector]
    private float invertCameraValue = 1;

    [HideInInspector]
    private Transform yGyroAxis = null, zGyroAxis = null, auxiliarGyroAxis = null;

    [HideInInspector]
    public static Camera camera = null;

    public bool InvertCamera {
        get { return invertCameraValue == 1f; }
        set { invertCameraValue = (invertCameraValue == 1) ? -1 : 1; }
    }

    private void SetupGyro() {
        yGyroAxis = new GameObject().transform;
        zGyroAxis = new GameObject().transform;
        auxiliarGyroAxis = new GameObject().transform;

        auxiliarGyroAxis.name = "Auxiliar Gyro Axis";
        yGyroAxis.name = "Y Gyro Axis";
        zGyroAxis.name = "Z Gyro Axis";

        auxiliarGyroAxis.SetParent(transform);
        yGyroAxis.transform.SetParent(transform);
        zGyroAxis.transform.SetParent(yGyroAxis.transform);

        auxiliarGyroAxis.position = transform.position;
        auxiliarGyroAxis.rotation = transform.rotation;

        yGyroAxis.position = transform.position;
        zGyroAxis.position = transform.position;

        zGyroAxis.rotation = transform.rotation;
        yGyroAxis.rotation = transform.rotation;

        camera = GetComponentInChildren<Camera>();
        camera.transform.SetParent(zGyroAxis);

        camera.transform.position = transform.position;
        camera.transform.rotation = transform.rotation;
    }

    private void Awake() {
        SetupGyro();
        InvertCamera = invertCamera;
    }

    public void OnEnable() {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void OnDisable() {
        Cursor.lockState = CursorLockMode.None;
    }

    private void FixedUpdate() {

        auxiliarGyroAxis.rotation = yGyroAxis.rotation;
        auxiliarGyroAxis.Rotate(Vector3.up * cameraSpeedY * Input.GetAxis("Mouse X"));

        if( auxiliarGyroAxis.rotation.y * Mathf.Rad2Deg <  horizontalNeckLimit + horizontalNeckPadding &&
            auxiliarGyroAxis.rotation.y * Mathf.Rad2Deg > -horizontalNeckLimit + horizontalNeckPadding)
            yGyroAxis.rotation = auxiliarGyroAxis.rotation;

        auxiliarGyroAxis.rotation = zGyroAxis.rotation;
        auxiliarGyroAxis.Rotate(Vector3.right * cameraSpeedX * Input.GetAxis("Mouse Y") * invertCameraValue);

        if( auxiliarGyroAxis.rotation.x * Mathf.Rad2Deg <  verticalNeckLimit + verticalNeckPadding &&
            auxiliarGyroAxis.rotation.x * Mathf.Rad2Deg > -verticalNeckLimit + verticalNeckPadding)
            zGyroAxis.rotation = auxiliarGyroAxis.rotation;
    }

    [HideInInspector]
    private Vector3[] gizmoCoordinates = new Vector3[4];

    public void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        gizmoCoordinates[0] = (Quaternion.Euler(new Vector3(verticalNeckLimit + verticalNeckPadding, horizontalNeckLimit + horizontalNeckPadding, 0)) * Vector3.forward * 10f);
        gizmoCoordinates[1] = (Quaternion.Euler(new Vector3(-verticalNeckLimit + verticalNeckPadding, horizontalNeckLimit + horizontalNeckPadding, 0)) * Vector3.forward * 10f);
        gizmoCoordinates[2] = (Quaternion.Euler(new Vector3(-verticalNeckLimit + verticalNeckPadding, -horizontalNeckLimit + horizontalNeckPadding, 0)) * Vector3.forward * 10f);
        gizmoCoordinates[3] = (Quaternion.Euler(new Vector3(verticalNeckLimit + verticalNeckPadding, -horizontalNeckLimit + horizontalNeckPadding, 0)) * Vector3.forward * 10f);

        for(int i = 0; i < gizmoCoordinates.Length; i++) {
            Gizmos.DrawRay(transform.position, gizmoCoordinates[i]);
            Gizmos.DrawLine(gizmoCoordinates[i]+transform.position, gizmoCoordinates[(int)Mathf.Repeat(i+1,gizmoCoordinates.Length)]+transform.position);
        }
    }
}
