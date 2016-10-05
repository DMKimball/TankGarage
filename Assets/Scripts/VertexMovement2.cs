using UnityEngine;
using System.Collections;

public class VertexMovement2 : MonoBehaviour {

    public enum vertexAxis { x, y, z }
    public vertexAxis transDir = vertexAxis.x;
    public float translateSpeed = 0.01f;
    public GameObject cameraGlobe;
    private Camera playerCamera;
    private GameObject parent;
    private SelectVertex selectHandler;
    private float prevMouseX = 0;
    private float prevMouseY = 0;

    public void Start()
    {
        parent = transform.root.gameObject;
        selectHandler = (parent != null) ? parent.GetComponent<SelectVertex>() : null;
        playerCamera = (cameraGlobe != null) ? cameraGlobe.GetComponentInChildren<Camera>() : null;
    }

    void Update()
    {
        prevMouseX = Input.mousePosition.x;
        prevMouseY = Input.mousePosition.y;
    }

    void OnMouseDrag()
    {
        if (!parent || !playerCamera) return;
        float mouseX = Input.mousePosition.x - prevMouseX;
        float mouseY = Input.mousePosition.y - prevMouseY;
        prevMouseX = Input.mousePosition.x;
        prevMouseY = Input.mousePosition.y;

        float angleMovement = Mathf.Atan2(mouseY, mouseX); // direction the mouse moves in
        if (angleMovement < 0) angleMovement = (2 * Mathf.PI) + angleMovement;

        float angleMagnitude = Mathf.Sqrt(mouseX * mouseX + mouseY * mouseY); // amount mouse moves
        //Debug.Log("angleMovement: " + angleMovement);
        //Debug.Log("angleMagnitude: " + angleMagnitude);

        Vector3 arrowScreenPos = playerCamera.WorldToScreenPoint(transform.position);
        Vector3 parentScreenPos = playerCamera.WorldToScreenPoint(parent.transform.position);

        float arrowX = arrowScreenPos.x;
        float arrowY = arrowScreenPos.y;
        float parentX = parentScreenPos.x;
        float parentY = parentScreenPos.y;

        float angleAdjust = Mathf.Atan2(arrowX - parentX, arrowY - parentY);
        Debug.Log("Angle Adjust: " + angleAdjust);
        Debug.Log("Angle Difference: " + (angleMovement - angleAdjust));

        float moveMagnitude = Mathf.Cos(angleMovement - angleAdjust) * angleMagnitude * translateSpeed;

        Vector3 axis = Vector3.zero;

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
        }

        Debug.Log("moveMagnitude: " + moveMagnitude);
        if((axis * moveMagnitude).magnitude != 0) Debug.Log("Translate Vector: " + axis * moveMagnitude);

        if (selectHandler != null) selectHandler.moveSelection(axis * moveMagnitude);
    }
}
