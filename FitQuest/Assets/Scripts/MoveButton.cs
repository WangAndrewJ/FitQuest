using UnityEngine;
using UnityEngine.EventSystems;

public class MoveButton : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public QuestButton questButton;

    public void OnDrag(PointerEventData data)
    {
        transform.parent.parent.position = data.position;
    }

    public void OnEndDrag(PointerEventData data)
    {
        questButton.buttonManager.RearrangeButtons();
    }
}
