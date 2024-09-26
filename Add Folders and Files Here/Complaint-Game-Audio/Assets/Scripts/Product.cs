using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Product : MonoBehaviour, IDropHandler, IPointerDownHandler
{
    public string[] AbsoluteNegativeWords;
    public string[] VeryNegativeWords;
    public string[] NegativeWords;
    public string[] PositiveWords;
    public string[] VeryPositiveWords;
    public TMP_Text starText;
    [SerializeField] GameObject Karen;

    
    public AudioClip absoluteNegativeClip;
    public AudioClip veryNegativeClip;
    public AudioClip negativeClip;
    public AudioClip positiveClip;
    public AudioClip veryPositiveClip;

    private int starRating;
    private AudioSource audioSource;

    private void Start()
    {
        AddPhysics2DRaycaster();
        starRating = Random.Range(3, 5);
        UpdateStars(0);

        audioSource = FindObjectOfType<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
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
                    PlaySound(absoluteNegativeClip);
                }
                else if (System.Array.Exists(VeryNegativeWords, word => word == category))
                {
                    Debug.Log("Very Negative Effect");
                    UpdateStars(-1);
                    PlaySound(veryNegativeClip);
                }
                else if (System.Array.Exists(NegativeWords, word => word == category))
                {
                    Debug.Log("Negative Effect");
                    UpdateStars(-1);
                    PlaySound(negativeClip);
                }
                else if (System.Array.Exists(PositiveWords, word => word == category))
                {
                    Debug.Log("Positive Effect");
                    UpdateStars(1);
                    PlaySound(positiveClip);
                }
                else if (System.Array.Exists(VeryPositiveWords, word => word == category))
                {
                    Debug.Log("Very Positive Effect");
                    UpdateStars(2);
                    PlaySound(veryPositiveClip);
                }

                // droppedObject.SetActive(false);
            }
        }
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.Stop();  // 停止当前正在播放的音频
            audioSource.loop = false;  // 确保不循环播放
            audioSource.clip = clip;
            audioSource.Play();  // 播放新的音频
        }
    }

    private void UpdateStars(int change)
    {
        starRating += change;
        starRating = Mathf.Clamp(starRating, 0, 5); // Ensure star rating is between 0 and 5
        starText.text = "Stars: " + starRating.ToString();
    }
}