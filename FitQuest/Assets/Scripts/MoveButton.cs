using UnityEngine;
using UnityEngine.EventSystems;

public class MoveButton : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public QuestButton questButton;
    public GameObject highlight;

    public void OnDrag(PointerEventData data)
    {
        transform.parent.parent.position = new Vector3(transform.parent.parent.position.x, data.position.y, 0f);
        questButton.buttonManager.RearrangeMovingButtons();
        highlight.SetActive(true);
    }

    public void OnEndDrag(PointerEventData data)
    {
        Debug.Log("end drag");
        highlight.SetActive(false);
        questButton.buttonManager.SaveQuests();
    }
}
