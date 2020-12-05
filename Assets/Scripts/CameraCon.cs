using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCon : MonoBehaviour
{
    [SerializeField]
    private float CameraSpeed;
    [SerializeField]
    private float ScrollSpeed;
    [SerializeField]
    private float Scrolltop;
    public float Scrollbottom;
    private float CameraHeightOnStart;
    private float CameraSpeedOnStart;
    private Camera cam;
    public float currentScroll;
    private void Awake()
    {
        CameraHeightOnStart = transform.position.y;
        CameraSpeedOnStart = CameraSpeed;
        cam = GetComponent<Camera>();
        currentScroll = cam.fieldOfView;
    }

    void Update()
    {
        float Scroll = (Input.GetAxis("Mouse ScrollWheel") * ScrollSpeed * Time.deltaTime);
        currentScroll -= Scroll;
        currentScroll = Mathf.Clamp(currentScroll, Scrollbottom, Scrolltop);
        cam.fieldOfView = currentScroll;
        if (Input.GetMouseButton(2))
        {
            Vector3 PosChange = new Vector3
                (
                 Input.GetAxis("Mouse X") * CameraSpeed * Time.deltaTime,
                 0f,
                 Input.GetAxis("Mouse Y") * CameraSpeed * Time.deltaTime
                );
            CameraSpeed = CameraSpeedOnStart * ((cam.fieldOfView) / 120f);
            transform.position += PosChange;
        }
    }
}
