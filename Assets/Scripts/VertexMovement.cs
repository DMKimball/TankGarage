using UnityEngine;
using System.Collections;

public class VertexMovement : MonoBehaviour
{
    public enum vertexAxis { x, y, z}
    public vertexAxis transDir = vertexAxis.x;
    public float translateSpeed = 0.01f;
    public GameObject cameraGlobe;
    private GameObject parent;
    private SelectVertex selectHandler;
    private float prevMouseX = 0;
    private float prevMouseY = 0;

    public void Start()
    {
        parent = transform.root.gameObject;
        selectHandler = (parent != null) ? parent.GetComponent<SelectVertex>() : null;
    }

    void Update()
    {
        prevMouseX = Input.mousePosition.x;
        prevMouseY = Input.mousePosition.y;
    }

    void OnMouseDrag()
    {
        if (!parent || !cameraGlobe ) return;
        float mouseX = Input.mousePosition.x - prevMouseX;
        float mouseY = Input.mousePosition.y - prevMouseY;
        prevMouseX = Input.mousePosition.x;
        prevMouseY = Input.mousePosition.y;

        float angleMovement = Mathf.Atan2(mouseY, mouseX); // direction the mouse moves in
        if (angleMovement < 0) angleMovement = (2 * Mathf.PI) + angleMovement;

        float angleMagnitude = Mathf.Sqrt(mouseX * mouseX + mouseY * mouseY); // amount mouse moves
        //Debug.Log("angleMovement: " + angleMovement);
        //Debug.Log("angleMagnitude: " + angleMagnitude);

        float cameraX = Mathf.Sin(cameraGlobe.transform.localEulerAngles.y * Mathf.Deg2Rad) * Mathf.Cos(cameraGlobe.transform.localEulerAngles.x * Mathf.Deg2Rad);
        float cameraY = -Mathf.Sin(cameraGlobe.transform.localEulerAngles.x * Mathf.Deg2Rad);
        float cameraZ = Mathf.Cos(cameraGlobe.transform.localEulerAngles.y * Mathf.Deg2Rad) * Mathf.Cos(cameraGlobe.transform.localEulerAngles.x * Mathf.Deg2Rad);

        Vector3 cameraDir = new Vector3(cameraX, cameraY, cameraZ); //vector indicating direction camera is pointing
        //Debug.Log("cameraDir: " + cameraDir);

        float refXX = Mathf.Cos(cameraGlobe.transform.localEulerAngles.y * Mathf.Deg2Rad);
        float refXY = 0;
        float refXZ = -Mathf.Sin(cameraGlobe.transform.localEulerAngles.y * Mathf.Deg2Rad);

        Vector3 planeReferenceX = new Vector3(refXX, refXY, refXZ); //vector pointing to right side of screen
        //Debug.Log("planeReferenceX: " + planeReferenceX);

        float refYX = Mathf.Sin(cameraGlobe.transform.localEulerAngles.y * Mathf.Deg2Rad) * Mathf.Sin(cameraGlobe.transform.localEulerAngles.x * Mathf.Deg2Rad);
        float refYY = Mathf.Cos(cameraGlobe.transform.localEulerAngles.x * Mathf.Deg2Rad);
        float refYZ = Mathf.Cos(cameraGlobe.transform.localEulerAngles.y * Mathf.Deg2Rad) * Mathf.Sin(cameraGlobe.transform.localEulerAngles.x * Mathf.Deg2Rad);

        Vector3 planeReferenceY = new Vector3(refYX, refYY, refYZ); //vector pointing to the top of screen
        //Debug.Log("planeReferenceY: " + planeReferenceY);
        //Debug.Log("Cross of refX and camDir: " + Vector3.Cross(cameraDir, planeReferenceX));

        Vector3 axis = new Vector3(0, 0, 0);

        float angleAdjust1 = 0;
        float angleAdjust2 = 0;
        float angleAdjust = 0;
        float moveMagnitude = 0;

        switch(transDir)
        {
            case vertexAxis.x:
                axis = Vector3.left;
                break;
            case vertexAxis.y:
                axis = Vector3.up;
                break;
            case vertexAxis.z:
                axis = Vector3.back;
                break;
            default:
                axis = Vector3.zero;
                break;
        }
        //Debug.Log("Axis Vector: " + axis);
        Vector3 projectedVector = Vector3.ProjectOnPlane(axis, cameraDir);
        angleAdjust1 = Vector3.Angle(planeReferenceX, projectedVector) * Mathf.Deg2Rad;
        angleAdjust2 = Vector3.Angle(planeReferenceY, projectedVector) * Mathf.Deg2Rad;

        //Debug.Log("Angle Adjust 1: " + (angleAdjust1 * Mathf.Rad2Deg));
        //Debug.Log("Angle Adjust 2: " + (angleAdjust2 * Mathf.Rad2Deg));

        if (angleAdjust2 <= Mathf.PI / 2)
        {
            angleAdjust = angleAdjust1;
        }
        else
        {
            angleAdjust = (2 * Mathf.PI) - angleAdjust1;
        }

        //Debug.Log("Angle Adjust: " + angleAdjust);
        //Debug.Log("Angle Difference: " + (angleMovement - angleAdjust));

        moveMagnitude = Mathf.Cos(angleMovement - angleAdjust) * angleMagnitude * translateSpeed;

        //Debug.Log("moveMagnitude: " + moveMagnitude);
        //if((axis * moveMagnitude).magnitude != 0) Debug.Log("Translate Vector: " + axis * moveMagnitude);

        if(selectHandler != null) selectHandler.moveSelection(axis * moveMagnitude);
    }
}
