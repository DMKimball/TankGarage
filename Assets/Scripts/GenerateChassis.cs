using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class GenerateChassis : MonoBehaviour
{

    public Text infoBox;
    public Material chassisMat;
    public GameObject garageController;

    private SelectionManager selectController;
    private Mesh chassisMesh;
    private List<Vector3> chassisMeshVertices;
    private List<int> chassisMeshTriangles;
    private List<Vector2> chassisMeshUVs;
    private Dictionary<SelectVertex, List<int>> vertexGizmoMap;


    public void Start()
    {
        selectController = garageController.GetComponent<SelectionManager>();
        chassisMesh = new Mesh();

        chassisMeshVertices = new List<Vector3>();
        chassisMeshTriangles = new List<int>();
        chassisMeshUVs = new List<Vector2>();

        gameObject.AddComponent<MeshFilter>().mesh = chassisMesh;
        gameObject.AddComponent<MeshRenderer>().material = chassisMat;

        vertexGizmoMap = new Dictionary<SelectVertex, List<int>>();
    }

    public void generateChassisEdge()
    {
        int numSelected = selectController.getNumSelected();

        if (numSelected != 3)
        {
            if(infoBox != null)
            {
                infoBox.text = "Please select three vertices.";
            }
            return;
        }
        else
        {
            if(infoBox != null)
            {
                infoBox.text = "";
            }
        }

        for(int count = 0; count < 3; count++)
        {
            SelectVertex vertex = selectController.getSelected(count);
            if (!vertexGizmoMap.ContainsKey(vertex)) vertexGizmoMap.Add(vertex, new List<int>());

            chassisMeshVertices.Add(vertex.getLocation());
            chassisMeshUVs.Add(new Vector2(0, 0));

            vertexGizmoMap[vertex].Add(chassisMeshVertices.Count - 1);
        }
        for(int count = 2; count >= 0; count--)
        {
            SelectVertex vertex = selectController.getSelected(count);
            chassisMeshVertices.Add(vertex.getLocation());
            chassisMeshUVs.Add(new Vector2(0, 0));
            vertexGizmoMap[vertex].Add(chassisMeshVertices.Count - 1);
        }

        for(int count = 6; count > 0; count--)
        {
            chassisMeshTriangles.Add(chassisMeshVertices.Count - count);
        }

        Vector3[] vertices = new Vector3[chassisMeshVertices.Count];
        Vector2[] uvs = new Vector2[chassisMeshUVs.Count];
        int[] triangles = new int[chassisMeshTriangles.Count];

        chassisMeshVertices.CopyTo(vertices);
        chassisMeshUVs.CopyTo(uvs);
        chassisMeshTriangles.CopyTo(triangles);

        chassisMesh.vertices = vertices;
        chassisMesh.uv = uvs;
        chassisMesh.triangles = triangles;
        chassisMesh.RecalculateNormals();
    }

    public void UpdateMesh(SelectVertex vertexGizmo)
    {
        if (!vertexGizmoMap.ContainsKey(vertexGizmo)) return;

        List<int> affectedMeshVertices = vertexGizmoMap[vertexGizmo];

        foreach(int index in affectedMeshVertices)
        {
            chassisMeshVertices[index] = vertexGizmo.getLocation();
        }

        Vector3[] vertices = new Vector3[chassisMeshVertices.Count];
        chassisMeshVertices.CopyTo(vertices);
        chassisMesh.vertices = vertices;
        chassisMesh.RecalculateNormals();
    }
}
