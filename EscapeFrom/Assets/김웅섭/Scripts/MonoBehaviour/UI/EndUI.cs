using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndUI : MonoBehaviour
{
    [SerializeField] private GameObject bg;
    [SerializeField] private RectTransform img;
    [SerializeField] private bool isEndAnimation;

    private void Update() 
    {
        if(GameManager.manager.isEnd && !isEndAnimation)
        {
            isEndAnimation = true;
            StartCoroutine(EndAnimation());
        }
    }

    IEnumerator EndAnimation()
    {
        SoundManager.PlaySound("hitSound", 0, 1.0f, Player.player.transform.position);
        bg.SetActive(true);

        yield return new WaitForSeconds(5);

        img.gameObject.SetActive(true);
        SoundManager.PlaySound("windSound", 0, 1.0f, Player.player.transform.position, true);
    }
}
