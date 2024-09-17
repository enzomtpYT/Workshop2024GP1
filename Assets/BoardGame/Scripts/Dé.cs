using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Assurez-vous d'inclure le namespace pour TextMeshPro

public class DiceRoller : MonoBehaviour
{
    // Nombre de faces du dé
    public int diceSides = 6;

    // Variable pour stocker le résultat du dé
    public int diceResult { get; private set; }

    // Référence au composant Rigidbody
    private Rigidbody rb;

    // Impulsion appliquée au dé lors du lancer
    public float throwForce = 10f;

    // Booléen pour vérifier si le dé a été lancé
    private bool hasRolled = false;

    // Référence au TextMeshPro pour afficher le résultat
    public TextMeshProUGUI resultText;

    // Méthode appelée une fois au démarrage du jeu
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        RollDice();
        UpdateResultText(); // Met à jour le texte au démarrage
    }

    // Appelée à chaque frame
    void Update()
    {
        // Si la touche "R" est appuyée et que le dé n'a pas encore été lancé, lancer le dé
        if (Input.GetKeyDown(KeyCode.R) && !hasRolled)
        {
            RollDice();
            ThrowDice();
            hasRolled = true; // Indique que le dé a été lancé
            UpdateResultText(); // Met à jour le texte avec le nouveau résultat
        }
    }

    // Méthode pour lancer le dé
    void RollDice()
    {
        // Génère un nombre aléatoire entre 1 et le nombre de faces du dé
        diceResult = Random.Range(1, diceSides + 1);
        Debug.Log("Résultat du dé : " + diceResult);
    }

    // Méthode pour appliquer une impulsion au dé
    void ThrowDice()
    {
        // Réinitialise la vitesse pour éviter des mouvements persistants
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        // Applique une impulsion aléatoire pour simuler un lancer
        Vector3 randomDirection = Random.onUnitSphere;
        rb.AddForce(randomDirection * throwForce, ForceMode.Impulse);

        // Optionnel : Ajoute une rotation aléatoire pour un effet plus réaliste
        Vector3 randomTorque = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        rb.AddTorque(randomTorque * throwForce, ForceMode.Impulse);
    }

    // Méthode pour mettre à jour le texte avec le résultat
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
}
