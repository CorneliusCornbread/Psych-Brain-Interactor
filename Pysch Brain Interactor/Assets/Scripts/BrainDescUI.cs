using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

public class BrainDescUI : MonoBehaviour
{
    [SerializeField] 
    private TMP_Text uiText;

    [SerializeField]
    private GameObject description;
    
    public static BrainDescUI Instance { get; private set; }

    private void Start()
    {
        Assert.IsNull(Instance);
        Instance = this;
    }

    public void SetText(string desc)
    {
        uiText.text = desc;
    }

    public void ShowDescription()
    {
        description.SetActive(true);
    }

    public void HideDescription()
    {
        SelectHighlight.DeselectCurrenSelections();

        description.SetActive(false);
    }
}
