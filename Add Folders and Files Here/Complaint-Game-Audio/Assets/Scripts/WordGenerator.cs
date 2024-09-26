using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class WordGenerator : MonoBehaviour
{
    public GameObject draggableWordPrefab;
    public Transform wordsTrans;
    public Product[] products;
    
    public string[] AbsoluteNegativeWords;
    private List<string> allWords = new List<string>();

    void Start()
    {
        foreach (var product in products)
        {
            product.AbsoluteNegativeWords = AbsoluteNegativeWords;
        }
        GenerateWords();
    }

    void GenerateWords()
    {
        foreach (var product in products)
        {
            allWords.AddRange(product.NegativeWords);
            allWords.AddRange(product.PositiveWords);
            allWords.AddRange(product.VeryNegativeWords);
            allWords.AddRange(product.VeryPositiveWords);
        }

        allWords.AddRange(AbsoluteNegativeWords);
        
        allWords = ShuffleList(allWords);
        
        foreach (var word in allWords)
        {
            GameObject draggableWordObj = Instantiate(draggableWordPrefab, wordsTrans);
            DraggableWord draggableWord = draggableWordObj.GetComponent<DraggableWord>();
            draggableWord.category = word;
            draggableWord.wordText.SetText(word);
            draggableWordObj.name = word;
            Text wordText = draggableWordObj.GetComponentInChildren<Text>();
            if (wordText != null)
            {
                wordText.text = word;
            }
        }
    }

    List<string> ShuffleList(List<string> list)
    {
        return list.OrderBy(_ => Random.value).ToList();
    }
}