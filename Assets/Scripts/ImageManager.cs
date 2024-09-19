using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageManager : MonoBehaviour
{
    [SerializeField] List<Sprite> sprites;
    [SerializeField] Image image;

    private int currentImg = 0;

    public void ChangeImage(CardType cardType) {
        currentImg = (int)cardType;
        Debug.Log(image);
        Debug.Log(image.sprite);
        Debug.Log((sprites, sprites.Count));
        Debug.Log(currentImg);
        image.sprite = sprites[currentImg];
    }
}
