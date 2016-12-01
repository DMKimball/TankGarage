using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SelectionManager : MonoBehaviour
{

    public GameObject tankChassis;

    private List<SelectVertex> vertices, verticesMirrorX, verticesMirrorY, verticesMirrorZ;
    private GenerateChassis chassisGenerator;
    private AddVertex addVertex;
    private bool mirrorModeX, mirrorModeY, mirrorModeZ;

    public void Start()
    {
        if (tankChassis != null) chassisGenerator = tankChassis.GetComponent<GenerateChassis>();
        addVertex = GetComponent<AddVertex>();
        vertices = new List<SelectVertex>();
        verticesMirrorX = new List<SelectVertex>();
        verticesMirrorY = new List<SelectVertex>();
        verticesMirrorZ = new List<SelectVertex>();
    }

    public void addSelected(SelectVertex vertex)
    {
        vertices.Remove(vertex); // makes sure there are no duplicates
        vertices.Add(vertex);
        vertex.enableSelected();
    }

    public void clearSelected()
    {
        foreach(SelectVertex vertex in vertices)
        {
            vertex.disableSelected();
        }
        vertices.Clear();
    }

    public void setSelected(SelectVertex vertex)
    {
        clearSelected();
        addSelected(vertex);
    }

    public SelectVertex getSelected(int index = 0)
    {
        if (index >= vertices.Count) return null;
        else return vertices[index];
    }

    public void removeSelected(SelectVertex vertex)
    {
        vertices.Remove(vertex);
        vertex.disableSelected();
    }

    public int getNumSelected()
    {
        return vertices.Count;
    }

    public void moveSelection(Vector3 movement, SelectVertex caller)
    {
        if (movement.magnitude == 0) return;
        if (vertices.Contains(caller))
        {
            foreach (SelectVertex vertex in vertices)
            {
                vertex.move(movement);
                if (chassisGenerator != null) chassisGenerator.UpdateMesh(vertex);
            }
        }
    }

    public bool isMirroredX()
    {
        return mirrorModeX;
    }

    public bool isMirroredY()
    {
        return mirrorModeY;
    }

    public bool isMirroredZ()
    {
        return mirrorModeZ;
    }

    public void setMirroredX(bool val)
    {
        mirrorModeX = val;
        if(mirrorModeX)
        {

        }
        else
        {

        }
    }

    public void setMirroredY(bool val)
    {
        mirrorModeY = val;
    }

    public void setMirroredZ(bool val)
    {
        mirrorModeZ = val;
    }
}
