using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonDeployTroop : MonoBehaviour, IPointerClickHandler
{

    public int IDTroop;

    public GameManager gameManager;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            OnLeftClick();
        }
    }

    public void OnLeftClick()
    {
        gameManager.DeployTroop(IDTroop);
    }
}
