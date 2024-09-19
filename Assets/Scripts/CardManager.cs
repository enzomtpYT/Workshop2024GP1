using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardType {
    Dev,
    Network,
    Marketing,
    Trivia,
    Bonus,
    Malus
}

public class CardManager : MonoBehaviour
{
    public static string getCardTypeAsString(CardType cardType) {
        switch (cardType)
        {
            case CardType.Dev:
                return "Développement";
            case CardType.Network:
                return "Réseau";
            case CardType.Marketing:
                return "Marketing Digital";
            case CardType.Trivia:
                return "Culture Geek";
            case CardType.Bonus:
                return "Bonus";
            case CardType.Malus:
                return "Malus";
            default:
                return "";
        }
    }
}
