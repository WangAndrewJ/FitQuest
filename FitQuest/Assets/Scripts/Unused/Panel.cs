using UnityEngine;

public class Panel : MonoBehaviour
{
    [SerializeField]
    private PageSwiper pageSwiper;
    [SerializeField]
    private GameObject content;
    [SerializeField]
    private int pageNumber;

    private void Update()
    {
        if (pageNumber == pageSwiper.currentPage)
        {
            content.SetActive(true);
        }
        else
        {
            content.SetActive(false);
        }
    }
}
