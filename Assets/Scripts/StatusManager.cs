using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatusManager : MonoBehaviour
{
    List<Func<int, int>> currentStatuses = new();
    public GameObject statusUI;
    public TextMeshProUGUI statusText;
    public ImageManager imageManager;
    public bool validatedStatus = true;

    List<(string, Func<int, int>)> bonuses = new() {
        ("Babyfoot:\n\nAjoute 1 à ton prochain lancer !", new((int a) => a+1)),
        // ("Pause café:\n\nDouble ton prochain lancer !", new((int a) => a*2)),
    };

    List<(string, Func<int, int>)> maluses = new() {
        ("Retard:\n\nEnlève 1 à ton prochain lancer !", new((int a) => a-1)),
        // ("Accident de tram:\n\nDouble ton prochain lancer !", new((int a) => a/2)),
    };

    public void AddStatus(CardType type) {
        validatedStatus = false;

        var source = type == CardType.Bonus ? bonuses : maluses;
        int idx = UnityEngine.Random.Range(0, source.Count);
        currentStatuses.Add(source[idx].Item2);

        imageManager.ChangeImage(type);
        statusText.text = source[idx].Item1;
        statusUI.SetActive(true);
    }

    public void ValidateStatus() {
        validatedStatus = true;
        statusUI.SetActive(false);
    }

    public int ApplyStatus(int input) {
        foreach (var status in currentStatuses) {
            input = status(input);
        }
        currentStatuses.Clear();
        return input;
    }
}
