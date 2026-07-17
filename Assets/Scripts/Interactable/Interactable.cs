using UnityEngine;

public class Interactable : MonoBehaviour
{
    private bool isSelected = false;

    public virtual void Select()
    {
        isSelected = true;
        Debug.Log(gameObject.name + " selecionado.");
    }

    public virtual void Deselect()
    {
        isSelected = false;
        Debug.Log(gameObject.name + " desselecionado.");
    }

    public bool IsSelected()
    {
        return isSelected;
    }
}