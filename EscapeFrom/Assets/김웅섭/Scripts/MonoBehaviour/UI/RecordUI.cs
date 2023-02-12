using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecordUI : MonoBehaviour
{
    [Header("setting")]
    [SerializeField] private Image img;
    [SerializeField] private Button left;
    [SerializeField] private Button right;
    [Header("Info")]
    [SerializeField] private List<Sprite> page;
    [SerializeField] private int currentPage;
    
    private void Awake() 
    {
        if(page.Count != 0) img.sprite = page[0];
    }

    private void Start() 
    {
        left.onClick.AddListener(() => {
            currentPage--;
        });

        right.onClick.AddListener(() => {
            currentPage++;
        });
    }
    
    private void Update() 
    {
        currentPage = Mathf.Clamp(currentPage, 0, page.Count - 1);
        img.sprite = page[currentPage];

        if (currentPage <= 0) left.gameObject.SetActive(false);
        else left.gameObject.SetActive(true);

        if (currentPage >= page.Count - 1) right.gameObject.SetActive(false);
        else right.gameObject.SetActive(true);
    }
}
