using UnityEngine;

public class BrainDescription : MonoBehaviour
{
    [SerializeField]
    [TextArea]
    private string description;
    
    public void OnSelect()
    {
        BrainDescUI.Instance.SetText(description);
        BrainDescUI.Instance.ShowDescription();
    }
}
