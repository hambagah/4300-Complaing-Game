using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Product : MonoBehaviour, IDropHandler, IPointerDownHandler
{
    public string[] AbsoluteNegativeWords;
    public string[] NegativeWords;
    public string[] PositiveWords;
    public TMP_Text starText;

    private int starRating = 5;

    private void Start()
    {
        AddPhysics2DRaycaster();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Clicked: " + eventData.pointerCurrentRaycast.gameObject.name);
    }

    private void AddPhysics2DRaycaster()
    {
        Physics2DRaycaster physicsRaycaster = FindObjectOfType<Physics2DRaycaster>();
        if (physicsRaycaster == null)
        {
            Camera.main.gameObject.AddComponent<Physics2DRaycaster>();
        }
    }

    public void OnDrop(PointerEventData eventData)
    {   
        GameObject droppedObject = eventData.pointerDrag;
        if (droppedObject != null)
        {
            DraggableWord draggableWord = droppedObject.GetComponent<DraggableWord>();
            if (draggableWord != null)
            {
                string category = draggableWord.category;
                Debug.Log($"Word '{droppedObject.name}' with category '{category}' dropped on {name}");

                if (System.Array.Exists(AbsoluteNegativeWords, word => word == category))
                {
                    Debug.Log("Absolute Negative Effect");
                    UpdateStars(-2);
                }
                else if (System.Array.Exists(NegativeWords, word => word == category))
                {
                    Debug.Log("Negative Effect");
                    UpdateStars(-1);
                }
                else if (System.Array.Exists(PositiveWords, word => word == category))
                {
                    Debug.Log("Positive Effect");
                    UpdateStars(1);
                }

                droppedObject.SetActive(false);
            }
        }
    }

    private void UpdateStars(int change)
    {
        starRating += change;
        starRating = Mathf.Clamp(starRating, 0, 5); // Ensure star rating is between 0 and 5
        starText.text = "Stars: " + starRating.ToString();
    }
}