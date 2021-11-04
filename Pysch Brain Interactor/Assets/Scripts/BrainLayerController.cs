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
    private Collider[] cerebralCortex;

    [SerializeField]
    private GameObject otherRegions;
    
    [SerializeField]
    private GameObject associationAreas;
    
    [SerializeField]
    private Transform innerCamPos;
    
    [SerializeField]
    private GameObject spinalCord;
    
    [SerializeField]
    private Transform spinalCordCamPos;

    private const int OuterBrainLayerId = 0;
    private const int InnerBrainLayerId = 1;
    private const int BrainRegionsLayerId = 2;
    private const int CerebralCortexLayerId = 3;
    private const int OtherRegionsLayerId = 4;
    private const int AssociationAreasLayerId = 5;
    private const int SpinalCordLayerId = 6;
    
    private const string ChildTag = "Brain Child";
    
    private int _activeLayerId = -1;
    private Collider[] _activeLayerColliders;

    private CameraController _camControl;

    private Vector3 _oldCameraPos;
    private Quaternion _oldCameraRot;
    private bool _isGO;
    private GameObject _activeGOLayer;
    
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
            
            //Cerebral cortex
            case CerebralCortexLayerId:
                EnableCerebralCortex();
                break;
            
            //Other brain regions
            case OtherRegionsLayerId:
                EnableOtherBrainRegions();
                break;
            
            //Association areas
            case AssociationAreasLayerId:
                EnableAssociationAreas();
                break;
            
            //Spinal cord
            case SpinalCordLayerId:
                EnableSpinalMode();
                break;
        }
    }

    private void DisableActiveLayer()
    {
        if (_activeLayerId == InnerBrainLayerId)
        {
            DisableCameraInnerMode();
        }
        else if (_activeLayerId == SpinalCordLayerId)
        {
            DisableCameraSpinalMode();
        }
        
        BrainDescUI.Instance.HideDescription(); //Automatically hides the description and everything

        if (_isGO)
        {
            _activeGOLayer.SetActive(false);
            _activeGOLayer = null;
            _isGO = false;
            return;
        }
        
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

    private void EnableCerebralCortex()
    {
        if (_activeLayerId == CerebralCortexLayerId) return;
        
        DisableActiveLayer();
        _activeLayerId = BrainRegionsLayerId;
        _activeLayerColliders = cerebralCortex;
        
        foreach (Collider col in _activeLayerColliders)
        {
            col.enabled = true;
        }
    }

    private void EnableOtherBrainRegions()
    {
        if (_activeLayerId == BrainRegionsLayerId) return;
        
        DisableActiveLayer();
        _activeLayerId = BrainRegionsLayerId;
        _activeLayerColliders = null;
        _isGO = true;
        _activeGOLayer = otherRegions;
        
        otherRegions.SetActive(true);
    }
    
    private void EnableAssociationAreas()
    {
        if (_activeLayerId == AssociationAreasLayerId) return;
        
        DisableActiveLayer();
        _activeLayerId = AssociationAreasLayerId;
        _activeLayerColliders = null;
        _isGO = true;
        _activeGOLayer = associationAreas;
        
        associationAreas.SetActive(true);
    }

    private void EnableSpinalMode()
    {
        if (_activeLayerId == SpinalCordLayerId) { return; }
        
        DisableActiveLayer();
        EnableCameraSpinalMode();
        _activeLayerId = SpinalCordLayerId;
        _activeLayerColliders = null;
        _isGO = true;
        _activeGOLayer = spinalCord;
        
        spinalCord.SetActive(true);
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

    private void EnableCameraSpinalMode()
    {
        _camControl.enabled = false;

        _oldCameraPos = _camControl.transform.position;
        _oldCameraRot = _camControl.transform.rotation;

        _camControl.transform.position = spinalCordCamPos.position;
        _camControl.transform.rotation = spinalCordCamPos.rotation;
    }
    
    private void DisableCameraSpinalMode()
    {
        _camControl.enabled = true;

        _camControl.transform.position = _oldCameraPos;
        _camControl.transform.rotation = _oldCameraRot;
    }
}
