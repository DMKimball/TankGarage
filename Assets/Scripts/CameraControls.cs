using UnityEngine;
using System.Collections;

public class CameraControls : MonoBehaviour {

    public float sensitivityH = 30;
    public float sensitivityV = 30;
    public float maxClampVal = 45;
    public float minClampVal = -45;
    public static float xRotation = 0;
    public static float yRotation = 0;
    public static float zRotation = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        float hz = Input.GetAxis("Horizontal");
        float vt = Input.GetAxis("Vertical");

        transform.Rotate(sensitivityH * hz * Vector3.up * Time.deltaTime, Space.World);
        transform.Rotate(sensitivityV * vt * Vector3.right * Time.deltaTime);

        if (transform.localEulerAngles.x > maxClampVal)
        {
            if (transform.localEulerAngles.x < 360 + minClampVal)
            {
                float newAngle = (transform.localEulerAngles.x < 180) ? (maxClampVal) : (360 + minClampVal);
                transform.localEulerAngles = new Vector3(newAngle, transform.localEulerAngles.y, 0);
            }
        }

        if (transform.localEulerAngles.z != 0)
        {
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 0);
        }

        xRotation = transform.localEulerAngles.x;
        yRotation = transform.localEulerAngles.y;
        zRotation = transform.localEulerAngles.z;
    }
}
