using UnityEngine;
using System.Collections;

public class MeshGenTest : MonoBehaviour {

    public GameObject generatedObject;
    public Material material;
    public bool genEnabled;

	// Use this for initialization
	void Start () {
        if (genEnabled)
        {
            generatedObject = new GameObject();

            Mesh mesh = new Mesh();
            generatedObject.AddComponent<MeshFilter>().mesh = mesh;

            mesh.vertices = new Vector3[12] { new Vector3(0, 1, 0), new Vector3(1, 0, 0), new Vector3(0, 0, 0),
                                          new Vector3(0, 0, 0), new Vector3(1, 0, 0), new Vector3(0, 0, 1),
                                          new Vector3(0, 0, 1), new Vector3(0, 1, 0), new Vector3(0, 0, 0),
                                          new Vector3(1, 0, 0), new Vector3(0, 1, 0), new Vector3(0, 0, 1) };

            mesh.triangles = new int[12] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };

            mesh.uv = new Vector2[4] { new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 0), new Vector2(1, 1) };

            mesh.RecalculateNormals();

            generatedObject.AddComponent<MeshRenderer>().material = material;
        }
	}
}
