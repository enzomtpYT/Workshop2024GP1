using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PawnMovement pawn;
    public Slot startSlot;
    public Slot endSlot;
    public Slot currentSlot;
    public TextMeshProUGUI dieText;
    public GameObject dieUI;
    public GameObject startRoundButton;
    public ChoosePathUI choosePathUI;
    public QuestionManager questionManager;
    public StatusManager statusManager;
    public float randAnimDuration = 1.5f;

    IEnumerator RoundCoroutine()
    {
        startRoundButton.SetActive(false);
        if (currentSlot != startSlot) {
            if (currentSlot.slotType == CardType.Bonus || currentSlot.slotType == CardType.Malus) {
                statusManager.AddStatus(currentSlot.slotType);
                while (!statusManager.validatedStatus) yield return null;
            } else {
                questionManager.StartQuestion((QuestionType)currentSlot.slotType);
                while (!questionManager.questionAnswered) yield return null;
            }
            if (!questionManager.answeredCorrectly) {
                startRoundButton.SetActive(true);
                yield break;
            }
        }

        float startTime = Time.time;
        dieUI.SetActive(true);

        int pickedNumber = 0;
        while (Time.time < startTime + randAnimDuration)
        {
            pickedNumber = Random.Range(1, 7);
            dieText.text = pickedNumber.ToString();
            yield return null;
        }
        pickedNumber = 4;
        dieText.text = pickedNumber.ToString();
        yield return null;
        yield return new WaitForSeconds(.5f);
        pickedNumber = statusManager.ApplyStatus(pickedNumber);
        dieText.text = pickedNumber.ToString();
        yield return new WaitForSeconds(1f);
        dieUI.SetActive(false);

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
            } else {
                nextSlot = currentSlot.nextSlots[0].GetComponent<Slot>();
            }
            pawn.MoveTo(nextSlot.transform.position + new Vector3(0f, .25f, 0f));
            currentSlot = nextSlot;
        }

        if (currentSlot == endSlot)
            Application.Quit(); // switch to end scene

        startRoundButton.SetActive(true);
    }

    public void StartRound() {
        StartCoroutine(RoundCoroutine());
    }
}
