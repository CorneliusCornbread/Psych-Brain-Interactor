using UnityEngine;
using UnityEngine.Events;

public class Selectable : MonoBehaviour
{
    public UnityEvent onSelect;
    
    private static Selectable _lastSelection;
}
