using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Assurez-vous d'inclure le namespace pour TextMeshPro

public class DiceRoller : MonoBehaviour
{
    public int diceSides = 6; // Nombre de faces du dé
    public int diceResult { get; private set; } // Résultat du dé
    private Rigidbody rb; // Référence au composant Rigidbody
    public float throwForce = 10f; // Impulsion appliquée au dé lors du lancer
    private bool hasRolled = false; // Vérifie si le dé a été lancé
    public TextMeshProUGUI resultText; // Référence au texte pour afficher le résultat

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        RollDice();
        UpdateResultText(); // Met à jour le texte au démarrage
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && !hasRolled)
        {
            RollDice();
            ThrowDice();
            hasRolled = true; // Indique que le dé a été lancé
            UpdateResultText(); // Met à jour le texte avec le nouveau résultat
        }
    }

    void RollDice()
    {
        diceResult = Random.Range(1, diceSides + 1); // Génère un nombre aléatoire entre 1 et diceSides inclus
        Debug.Log("Résultat du dé : " + diceResult); // Affiche le résultat dans la console
    }

    void ThrowDice()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        Vector3 randomDirection = Random.onUnitSphere;
        rb.AddForce(randomDirection * throwForce, ForceMode.Impulse);

        Vector3 randomTorque = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        rb.AddTorque(randomTorque * throwForce, ForceMode.Impulse);
    }

    void UpdateResultText()
    {
        if (resultText != null)
        {
            resultText.text = "Résultat du dé : " + diceResult;
        }
        else
        {
            Debug.LogWarning("ResultText n'est pas assigné dans l'inspecteur.");
        }
    }

    public void ResetRoll()
    {
        hasRolled = false; // Réinitialiser le flag pour permettre un nouveau lancer
    }
}
