using UnityEngine;
using System.Collections;

public class AddVertex : MonoBehaviour
{

    public GameObject chassisVertex;
    public GameObject cameraGlobe;
    public float vertexSensitivity = 0.015f;
    private GameObject lastCreated = null;
    private SelectionManager controller;

    public void Start()
    {
        controller = GetComponent<SelectionManager>();
    }

    public void addVertex()
    {
        //Debug.Log("addVertex() called");
        lastCreated = (GameObject)Instantiate(chassisVertex, Vector3.zero, Quaternion.identity);

        VertexMovement[] moveScripts = lastCreated.GetComponentsInChildren<VertexMovement>();
        foreach (VertexMovement scripts in moveScripts)
        {
            scripts.cameraGlobe = cameraGlobe;
            scripts.translateSpeed = vertexSensitivity;
        }

        lastCreated.GetComponent<SelectVertex>().controller = controller;
    }
}
