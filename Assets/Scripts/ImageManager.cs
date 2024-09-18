using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageManager : MonoBehaviour
{
    [SerializeField] List<Sprite> sprites;

    private Image image;
    private int currentImg = 0;

    private void Awake() {
        image = GetComponent<Image>();
        if (!image) {
            this.enabled = false;
        }
    }

    public void ChangeImage(CardType cardType) {
        currentImg = (int)cardType;
        image.sprite = sprites[currentImg];
    }
}
