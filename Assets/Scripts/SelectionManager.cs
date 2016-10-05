using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SelectionManager : MonoBehaviour
{

    public GameObject tankChassis;

    private List<SelectVertex> vertices;
    private GenerateChassis chassisGenerator;

    public void Start()
    {
        if (tankChassis != null) chassisGenerator = tankChassis.GetComponent<GenerateChassis>();
        vertices = new List<SelectVertex>();
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

    public void moveSelection(Vector3 movement)
    {
        if (movement.magnitude == 0) return;
        foreach(SelectVertex vertex in vertices)
        {
            vertex.move(movement);
            if (chassisGenerator != null) chassisGenerator.UpdateMesh(vertex);
        }
    }
}
