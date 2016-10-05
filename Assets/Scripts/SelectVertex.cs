using UnityEngine;
using System.Collections;

public class SelectVertex : MonoBehaviour
{
    
    private MatSwitch localComp;
    private MatSwitch[] childComps;

    public SelectionManager controller;

    private bool isSelected;

    // Use this for initialization
    void Start()
    {
        localComp = GetComponent<MatSwitch>();
        childComps = GetComponentsInChildren<MatSwitch>();
        isSelected = false;
    }

    public void OnMouseDown()
    {
        //Debug.Log("onMouseDown called");
        if (controller != null)
        {
            if (Input.GetButton("AltMode"))
            {
                if (isSelected)
                {
                    controller.removeSelected(this);
                    //Debug.Log("Specific Delect");
                }
                else
                {
                    controller.addSelected(this);
                    //Debug.Log("Additive Select");
                }
            }
            else
            {
                if(isSelected && controller.getNumSelected() <= 1)
                {
                    controller.removeSelected(this);
                    //Debug.Log("Deselect");
                }
                else
                {
                    controller.setSelected(this);
                    //Debug.Log("Replacement Select");
                }
            }
        }
    }

    public void enableSelected(bool fromController = true)
    {
        localComp.enableAltMat();
        foreach(MatSwitch comp in childComps)
        {
            comp.enableAltMat();
        }
        isSelected = true;
    }

    public void disableSelected()
    {
        localComp.disableAltMat();
        foreach (MatSwitch comp in childComps)
        {
            comp.disableAltMat();
        }
        isSelected = false;
    }

    public void toggleSelected()
    {
        if (isSelected) disableSelected();
        else enableSelected();
    }

    public bool checkIfSelected()
    {
        return isSelected;
    }

    public void move(Vector3 movement)
    {
        transform.Translate(movement);
    }

    public Vector3 getLocation()
    {
        Vector3 loc = transform.position;
        return new Vector3(loc.x, loc.y, loc.z);
    }

    public void moveSelection(Vector3 movement)
    {
        if (isSelected)
        {
            controller.moveSelection(movement);
        }
        else
        {
            controller.setSelected(this);
            controller.moveSelection(movement);
        }
    }
}
