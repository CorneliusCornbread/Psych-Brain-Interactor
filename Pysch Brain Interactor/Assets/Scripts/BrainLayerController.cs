using System;
using TMPro;
using UnityEngine;

public class BrainLayerController : MonoBehaviour
{
    [SerializeField]
    private Collider[] outerBrain;

    [SerializeField]
    private Collider[] innerBrain;
    
    [SerializeField]
    private Collider[] brainReigions;

    [SerializeField]
    private Transform innerCamPos;

    private const int OuterBrainLayerId = 0;
    private const int InnerBrainLayerId = 1;
    private const int BrainRegionsLayerId = 2;
    
    private const string ChildTag = "Brain Child";
    
    private int _activeLayerId = -1;
    private Collider[] _activeLayerColliders;

    private CameraController _camControl;

    private Vector3 _oldCameraPos;
    private Quaternion _oldCameraRot;
    
    private void Start()
    {
        _camControl = Camera.main.GetComponent<CameraController>();

        _activeLayerId = OuterBrainLayerId;
        _activeLayerColliders = outerBrain;
    }

    public void OnSelectionChange(TMP_Dropdown dropdown)
    {
        switch (dropdown.value)
        {
            //Outer brain
            case OuterBrainLayerId:
                EnableOuterBrain();
                break;
            
            //Inner brain
            case InnerBrainLayerId:
                EnableInnerBrain();
                break;
            
            //Brain regions
            case BrainRegionsLayerId:
                EnableBrainRegions();
                break;
        }
    }

    private void DisableActiveLayer()
    {
        if (_activeLayerColliders == null) { return; }

        if (_activeLayerId == InnerBrainLayerId)
        {
            DisableCameraInnerMode();
        }
        
        BrainDescUI.Instance.HideDescription(); //Automatically hides the description and everything

        foreach (Collider col in _activeLayerColliders)
        {
            if (_activeLayerId == InnerBrainLayerId && !col.CompareTag(ChildTag))
            {
                Vector3 newPos = col.transform.position;
                newPos.x -= 20;
                col.transform.position = newPos;
            }
            
            col.enabled = false;
        }
    }
    
    private void EnableOuterBrain()
    {
        if (_activeLayerId == OuterBrainLayerId) { return; }
        
        DisableActiveLayer();
        _activeLayerId = OuterBrainLayerId;
        _activeLayerColliders = outerBrain;
        
        foreach (Collider col in _activeLayerColliders)
        {
            col.enabled = true;
        }
    }
    
    private void EnableInnerBrain()
    {
        if (_activeLayerId == InnerBrainLayerId) { return; }
        
        DisableActiveLayer();
        EnableCameraInnerMode();
        _activeLayerId = InnerBrainLayerId;
        _activeLayerColliders = innerBrain;
        
        foreach (Collider col in _activeLayerColliders)
        {
            if (!col.CompareTag(ChildTag))
            {
                Vector3 newPos = col.transform.position;
                newPos.x += 20;
                col.transform.position = newPos;
            }
            
            col.enabled = true;
        }
    }
    
    private void EnableBrainRegions()
    {
        if (_activeLayerId == BrainRegionsLayerId) { return; }
        
        DisableActiveLayer();
        _activeLayerId = BrainRegionsLayerId;
        _activeLayerColliders = brainReigions;
        
        foreach (Collider col in _activeLayerColliders)
        {
            col.enabled = true;
        }
    }

    private void EnableCameraInnerMode()
    {
        _camControl.enabled = false;

        _oldCameraPos = _camControl.transform.position;
        _oldCameraRot = _camControl.transform.rotation;

        _camControl.transform.position = innerCamPos.position;
        _camControl.transform.rotation = innerCamPos.rotation;
    }

    private void DisableCameraInnerMode()
    {
        _camControl.enabled = true;

        _camControl.transform.position = _oldCameraPos;
        _camControl.transform.rotation = _oldCameraRot;
    }
}
