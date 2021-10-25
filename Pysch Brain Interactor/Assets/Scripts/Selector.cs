using System;
using UnityEngine;

public class Selector : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    private void Update()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            
        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            hitInfo.collider.GetComponent<Selectable>()?.onSelect.Invoke();
        }
    }
}
