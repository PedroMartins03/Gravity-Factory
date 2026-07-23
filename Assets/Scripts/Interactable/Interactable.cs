using UnityEngine;

public class Interactable : MonoBehaviour
{
    private bool isSelected = false;

    public virtual void Select()
    {
        isSelected = true;
    }

    public virtual void Deselect()
    {
        isSelected = false;
    }

    public bool IsSelected()
    {
        return isSelected;
    }
}