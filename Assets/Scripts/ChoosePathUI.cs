using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChoosePathUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text1;
    [SerializeField] TextMeshProUGUI text2;

    Slot[] slots;

    public Slot choice;
    public bool playerChose = true;

    public void Prompt(Slot[] slots) {
        this.slots = slots;
        text1.text = CardManager.getCardTypeAsString(slots[0].slotType);
        text2.text = CardManager.getCardTypeAsString(slots[1].slotType);
        this.gameObject.SetActive(true);
        this.playerChose = false;
        this.choice = null;
    }

    public void Choose(int choice) {
        this.choice = this.slots[choice];
        playerChose = true;
    }
}
