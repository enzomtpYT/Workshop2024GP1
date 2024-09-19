using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance { get; private set; } // Instance unique pour le singleton

    private bool isPlayerTurn = true; // Indique si c'est le tour du joueur

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Assure qu'il n'y a qu'une seule instance
        }
    }

    public void EndTurn()
    {
        // Passer le tour
        isPlayerTurn = !isPlayerTurn; // Change le tour
        Debug.Log("Tour terminé. C'est maintenant le tour du joueur : " + (isPlayerTurn ? "Joueur" : "Autre"));
    }

    public bool IsPlayerTurn()
    {
        return isPlayerTurn; // Retourne si c'est le tour du joueur
    }
}
