using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PawnMovement pawn;
    public Slot currentSlot;
    public TextMeshProUGUI dieText;
    public GameObject startRoundButton;
    public ChoosePathUI choosePathUI;
    public QuestionManager questionManager;
    public StatusManager statusManager;
    public float randAnimDuration = 1.5f;

    IEnumerator RoundCoroutine()
    {
        float startTime = Time.time;

        int pickedNumber = 0;
        while (Time.time < startTime + randAnimDuration)
        {
            yield return null;
        }

        for (int i = 0; i < pickedNumber; i++) {
            while (!pawn.finished) yield return null;
            Slot nextSlot;
            if (currentSlot.nextSlots.Length > 1) {
                Slot[] slots = new Slot[2] {// dont want to reset every single slot in the scene
                    currentSlot.nextSlots[0].GetComponent<Slot>(),
                    currentSlot.nextSlots[1].GetComponent<Slot>()
                };
                choosePathUI.Prompt(slots);
                while (!choosePathUI.playerChose) yield return null;
                nextSlot = choosePathUI.choice;
            } else
                nextSlot = currentSlot.nextSlots[0].GetComponent<Slot>();
            pawn.MoveTo(nextSlot.transform.position);
            currentSlot = nextSlot;
        }

        if (currentSlot.slotType == CardType.Bonus || currentSlot.slotType == CardType.Malus) {
            statusManager.GetStatus();
        } else {
            questionManager.StartQuestion((QuestionType)(int)currentSlot.slotType);
        }
        while (!questionManager.questionAnswered) yield return null;

    }

    void StartRound() {
        StartCoroutine(RoundCoroutine());
    }
}
