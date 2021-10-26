using System;
using System.Collections.Generic;
using UnityEngine;

public class SelectHighlight : MonoBehaviour
{
    public static List<SelectHighlight> CurrentSelections { get; private set; } = new List<SelectHighlight>();

    public static List<SelectHighlight> CurrentMouseOver { get; private set; } = new List<SelectHighlight>();
    
    [SerializeField]
    private MeshRenderer mesh;

    [SerializeField] 
    private Material selectedMaterial;
    
    [SerializeField] 
    private bool dontDisableLastSelection = false;
    
    [SerializeField] 
    private int[] materialIndicies;

    private Material[] _oldMaterials;

    public void OnSelect()
    {
        if (CurrentSelections != null && !dontDisableLastSelection)
        {
            DeselectCurrenSelections();
        }

        _oldMaterials = mesh.materials;
        Material[] newMats = mesh.materials;
        
        foreach (int ind in materialIndicies)
        {
            newMats[ind] = selectedMaterial;
        }

        mesh.materials = newMats;
        
        CurrentSelections.Add(this);
    }

    public void OnDeselect()
    {
        CurrentSelections.Remove(this);

        mesh.materials = _oldMaterials;
    }

    public static void DeselectCurrenSelections()
    {
        for (int i = CurrentSelections.Count - 1; i >= 0; i--)
        {
            CurrentSelections[i].OnDeselect();
        }
    }
}
