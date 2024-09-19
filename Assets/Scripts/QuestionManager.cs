using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public struct Question {
    private static int idCount = 0;

    public int id;
    public string question;
    public string[] answers;
    public int goodAnswer;

    public Question(string question, string[] answers, int goodAnswer) {
        this.id = idCount++;
        this.question = question;
        this.answers = answers;
        this.goodAnswer = goodAnswer;
    }
}

public enum QuestionType {
    Dev, Network, Marketing, Trivia
}

public class QuestionManager : MonoBehaviour
{
    public bool questionAnswered = true;
    public bool answeredCorrectly = true;
    public TextMeshProUGUI questionText;
    public ImageManager imageManager;
    public TextMeshProUGUI answer1;
    public TextMeshProUGUI answer2;
    public TextMeshProUGUI answer3;
    public TextMeshProUGUI answer4;
    public Image resultImage;
    public TextMeshProUGUI resultText;
    private Question currentQuestion;

    private int[] lastQuestionId = new int[4] {
        -1, // QuestionType.Dev
        -1, // QuestionType.Network
        -1, // QuestionType.Marketing
        -1, // QuestionType.Trivia
    };

    private List<Question> devQuestions = new List<Question> {
        new("Quelle est la signification de HTML ?", new string[4] {

            "HyperText Modeling Language",
            "HyperText Markup Language",
            "HomeText Markup Link",
            "HyperTransfer Markup Logic",
        }, 1),

        new("Quelle est la fonction principale du CSS dans une page web ?", new string[4] {

            "Gérer la présentation et la mise en forme d'une page web",
            "Ajouter du contenu dynamique à la page web",
            "Sécuriser la transmission des données entre le serveur et le client",
            "Optimiser le temps de chargement de la page",
        }, 0),

        new("Qu'est ce qu'un “commit” dans Git ?", new string[4] {

            "Processus d'intégration d'une nouvelle branche",
            "Commande permettant d'annuler des modifications",
            "Processus de suppression d'une branche dans le dépôt",
            "Sauvegarde des changements apportés à un projet",
        }, 3),

        new("Qu'est ce qu'une API ?", new string[4] {

            "Programme qui compile le code source en machine exécutable",
            "Logiciel qui surveille les performances d'une application",
            "Ensemble de règles et de protocoles qui permet à différents logiciels de communiquer entre eux",
            "Plateforme qui stocke les fichiers d'un site web",
        }, 2),

        new("À quoi sert une clé primaire dans une base de données ?", new string[4] {

            "Créer des relations entre deux bases de données différentes",
            "Identifiant unique pour chaque enregistrement dans une table de base de données",
            "Crypter les données stockées dans la base de données",
            "Accélérer les requêtes sur les données",
        }, 1),

        new("Qu'est ce que l'injection SQL dans une base de données ?", new string[4] {

            "Technique d'attaque dans laquelle un attaquant insère du code SQL malveillant dans une requête pour manipuler ou accéder illégalement aux données de la base de données",
            "Une méthode de backup des bases de données",
            "Une procédure pour optimiser les requêtes SQL",
            "Un moyen de crypter les données avant leur stockage",
        }, 0),

        new("Qu'est ce que le “responsive” en développement web ?", new string[4] {

            "Concevoir des pages web qui s'adaptent automatiquement à la taille et à la résolution de différents appareils (ordinateurs, tablettes, smartphones)",
            "Une approche de développement web qui rend les pages plus rapides",
            "Un outil permettant de créer des animations interactives",
            "Un type de serveur pour héberger des sites web",
        }, 0),

        new("À quoi sert une base de données dans une application web ?", new string[4] {

            "Héberger des images et des vidéos pour le site",
            "Sécuriser les transactions en ligne",
            "Stocker, organiser et gérer les informations utilisées par une application web",
            "Servir les pages HTML aux utilisateurs",
        }, 2),

        new("Que fait une boucle en programmation ?", new string[4] {

            "Fait fonctionner plusieurs programmes en parallèle",
            "Arrête l'exécution du code s'il y a une erreur",
            "Crypte les données pour les rendre plus sécurisées",
            "Exécute de manière répétée un bloc de code tant qu'une condition spécifique est remplie",
        }, 3),

        new("Quelle est la principale utilisation de PHP ?", new string[4] {

            "Créer des animations graphiques dans des jeux vidéo",
            "Compiler du code pour les applications mobiles",
            "Créer des logiciels de gestion de base de données locales",
            "Développer des sites web dynamiques côté serveur",
        }, 3),
    };

    private List<Question> networkQuestions = new List<Question> {
        new("Que veut dire NAS ?", new string[4] {
            "Network Attached Stockage",
            "Network Attached Storage",
            "Network Attack Storage",
            "Nationality Attached Stockage",
        }, 1),
        new("A quoi sert le DHCP ?", new string[4] {
            "Le dhcp sert à interconnecter les appareils entre eux",
            "Il permet d'attribuer des adresse ip et dns automatiquement",
            "Il permet d'allumer un ordinateur dans le réseau local",
            "Il permet de connecter des écrans sans fils",
        }, 1),
        new("Quel est l'utilité d'un Switch ?", new string[4] {
            "Un switch permet de connecter plusieurs appareils dans un réseau local pour qu'ils puissent communiquer entre eux.",
            "Un switch sert à amplifier le signal d'un réseau pour augmenter la portée.",
            "Un switch est un appareil qui convertit le signal analogique en numérique.",
            "Un switch est utilisé pour stocker des données dans un réseau comme un disque dur.",
        }, 0),
        new("Quelle est la différence entre Get et post dans une requête HTML ?", new string[4] {
            "GET et POST sont identiques et servent uniquement à afficher des pages web, sans envoyer de données au serveur.",
            "GET et POST sont des méthodes HTTP pour envoyer des requêtes; GET récupère des données, POST envoie des données",
            "",
            "",
        }, 1),

        new("Quel est l'utilité d'un VPN (Virtual Private Network) ?", new string[4] {
            "Un VPN crée une connexion sur un réseau public pour protéger la confidentialité de vos données.",
            "Un VPN est un service qui augmente la vitesse de votre connexion internet en réduisant la latence.",
            "",
            "",
        }, 0),

        new("Quelle est la fonction principale du protocoles IP ?", new string[4] {
            "Le protocole IP gère la sécurité des connexions réseau en cryptant les données.",
            "Le protocole IP contrôle la vitesse de transmission des données sur le réseau.",
            "Le protocole IP est responsable de la gestion des requêtes DNS.",
            "Le protocole IP attribue des adresses uniques à chaque appareil sur un réseau pour permettre leur identification et communication",
        }, 3),
        new("Quel est le rôle d'une passerelle (Gateway) dans un réseau ?", new string[4] {
            "Une passerelle est un appareil qui gère les adresses IP sur un réseau local.",
            "Une passerelle est un logiciel utilisé pour surveiller l'utilisation des données par les utilisateurs du réseau.",
            "Une passerelle permet de connecter deux réseaux différents, par exemple un réseau local et internet.",
            "Une passerelle crypte les données entre les appareils d'un réseau local pour garantir la sécurité.",
        }, 2),
        new("Quelle est la différence entre un switch et un HUB ?", new string[4] {
            "Un switch envoie les données uniquement aux appareils destinataires, tandis qu'un hub diffuse les données à tous les appareils du réseau.",
            "Un switch permet la communication entre différents réseaux, tandis qu'un hub est utilisé uniquement pour la sécurité.",
            "", "",
        }, 0),

        new("Quel est le rôle du protocole HTTPS ?", new string[4] {
            "HTTPS est utilisé pour accélérer le chargement des pages web en supprimant les images.",
            "HTTPS empêche les virus d'entrer sur un réseau local en filtrant les connexions.",
            "HTTPS est une version sécurisée de HTTP qui chiffre les communications entre le navigateur de l'utilisateur et le serveur pour protéger les données.",
            "HTTPS est utilisé uniquement pour les sites de commerce en ligne.",
        }, 2),

        new("Qu'est-ce qu'une adresse IP ?", new string[4] {
            "Une adresse IP est utilisée pour crypter les données sur Internet.",
            "Une adresse IP est un identifiant temporaire attribué par un routeur pour chaque connexion Wi-Fi.",
            "Une adresse IP est un code qui mesure la vitesse de connexion d'un appareil sur un réseau.",
            "Une adresse IP est une série de chiffres qui identifie un appareil sur un réseau.",
        }, 3),
    };

    private List<Question> marketingQuestions = new List<Question> {
        new ("Que veut dire SEO ?", new string[4] {
            "Search Engine Optimization",
            "Search Engine Operator",
            "Signalling Errors Offline",
            "Socially Enhanced Operator",
        }, 0),

        new ("Combien de piliers y a-t-il dans l'analyse SWOT ?", new string[4] {
            "7",
            "3",
            "4",
            "5",
        }, 2),

        new ("Combien de piliers y a-t-il dans l'analyse PESTEL ?", new string[4] {
            "4",
            "3",
            "5",
            "6",
        }, 3),

        new ("Quels sont les 3 piliers du SEO ?", new string[4] {
            "Optimisation du site, fréquence de nouveau contenu, popularité",
            "Technicité du site, contenu, popularité",
            "Nombre de visiteurs, classement dans le référencement, qualité du contenu",
            "Rapidité du site, maillage interne, indexation",
        }, 1),

        new ("Lors d'une segmentation de marché, 4 conditions sont à réunir ?", new string[4] {
            "Pertinence, possibilité de mesure, possibilité d'accès, taille suffisante",
            "Rationalité, universalité, identification précise des critères, possibilité de retour client",
            "Cibler un large type de prospects, universalité des critères, pertinence, longévité",
            "Cibler des types de prospects précis, possibilité de mesure, pertinence, universalité",
        }, 0),

        new ("Quelles sont les types de ressources créatrices de valeur pour une organisation/entreprise ?", new string[4] {
            "Humaine, économique, immobilière, intellectuelle",
            "Financière, intellectuelle, physique, légale",
            "Météorologique, Spirituelle, Politique, environnementale",
            "Financière, humaine, matérielle, immatérielle",
        }, 3),

        new ("Que veut dire RGPD ?", new string[4] {
            "Règlement Garant de la Protection des Données",
            "Règlement Générique sur la Préservation des Données",
            "Règlement Général sur la Préservation des Données",
            "Règlement Général sur la Protection des Données",
        }, 3),

        new ("Du niveau d'autorité le plus faible au plus important, quelle est la hiérarchie des sources du droit en France ?", new string[4] {
            "Loi, Ordonnance, Jurisprudence, Accord internationaux, Constitution",
            "Jurisprudence, Ordonnance, Loi, Accord internationaux, Constitution",
            "Constitution, Accord internationaux, Loi, Ordonnance, Jurisprudence",
            "Jurisprudence, Ordonnance, Loi, Constitution, Accord Internationaux",
        }, 1),

        new ("Que veut dire CTA ?", new string[4] {
            "Call to Acquisition",
            "Call to Action",
            "Coût Total d'Acquisition",
            "Content to Advertise",
        }, 1),

        new ("Que veut dire KPI ?", new string[4] {
            "Keyword Performance Indicator",
            "Key Performance Indicator",
            "Key Purchase Incentive",
            "Keyword Per Impression",
        }, 1),
    };

    private List<Question> triviaQuestions = new List<Question> {
        new ("Que veut dire EPSI?", new string[4] {
            "École en Piratage et Sécurité Informatique",
            "École professionnelle des sciences informatiques",
            "École des Professionnels en Solutions Informatiques",
            "Enseignement Professionnel des Systèmes Informatiques",
        }, 1),

        new ("Quel est le nom du personnage principal dans la série de jeux “The legend of Zelda” ?", new string[4] {
            "Link",
            "Zelda",
            "Ganon",
            "Ganondorf",
        }, 0),


        new ("Combien de ventes a atteint le jeu Minecraft à ce jour ?", new string[4] {
            "Plus de 50 millions",
            "Plus de 100 millions",
            "Plus de 200 millions",
            "Plus de 300 millions",
        }, 3),



        new ("Dans le manga One Piece, quel est le nom du chapeau que porte Monkey D. Luffy ?", new string[4] {
            "Chapeau de paille",
            "Chapeau de cowboy",
            "Chapeau haut-de-forme",
            "Casquette de baseball",
        }, 0),


        new ("Quelle est la planète d'origine de Superman ?", new string[4] {
            "Tatooine",
            "Vulcain",
            "Krypton",
            "Terre",
        }, 2),

        new ("Dans Spider Man Into/Across the Spider-Verse, de quel univers vient Miles :", new string[4] {
            "Terre-42",
            "Terre-1610",
            "Terre-199999",
            "Terre-616",
        }, 1),

        new ("Quel est le nom du vaisseau piloté par Han Solo dans Star Wars ?", new string[4] {
            "Starship Enterprise",
            "Serenity",
            "Death Star",
            "Millenium Falcon",
        }, 3),

        new ("Comment s'appelle la saga littéraire écrite par J.R.R. Tolkien qui se déroule en Terre du Milieu ?", new string[4] {
            "Harry Potter",
            "Le Seigneur des Anneaux",
            "Star Wars",
            "Dune",
        }, 1),

        new ("Quel est le premier Pokémon dans le Pokédex ?", new string[4] {
            "Bulbizarre",
            "Pikachu",
            "Dracaufeu",
            "Evoli",
        }, 0),


        new ("Comment s'appellent les personnages principaux de GTA V (Grand theft auto V) ?", new string[4] {
            "Enzo / Mathis / Alex",
            "Jason / Niko Bellic / Carl",
            "Franklin / Michael / Trevor",
            "Arthur / Rick / Glenn",
        }, 2),
    };

    public Question GetRandomQuestion(QuestionType source) {
        List<List<Question>> lists = new List<List<Question>> {
            devQuestions,
            networkQuestions,
            marketingQuestions,
            triviaQuestions
        };
        int sourceIdx = (int)source;
        int randIdx = Random.Range(0, lists[sourceIdx].Count);
        while (randIdx == lastQuestionId[sourceIdx])
            randIdx = Random.Range(0, lists[sourceIdx].Count);
        lastQuestionId[sourceIdx] = randIdx;
        return lists[sourceIdx][randIdx];
    }

    public void StartQuestion(QuestionType source) {
        this.questionAnswered = false;
        currentQuestion = GetRandomQuestion(source);
        imageManager.ChangeImage((CardType)source);
        questionText.text = currentQuestion.question;
        answer1.text = currentQuestion.answers[0];
        answer2.text = currentQuestion.answers[1];
        answer3.text = currentQuestion.answers[2];
        answer4.text = currentQuestion.answers[3];

        // set all answers and card text and image
        this.gameObject.SetActive(true);
    }

    public void TryAnswer(int answer) {
        resultImage.gameObject.SetActive(true);
        resultText.gameObject.SetActive(true);
        if (answer == currentQuestion.goodAnswer) {
            resultText.text = "Correct!";
            resultImage.color = Color.green;
            this.answeredCorrectly = true;
        } else {
            resultText.text = "Wrong!";
            resultImage.color = Color.red;
            this.answeredCorrectly = false;
        }
        StartCoroutine(AnswerCoroutine());
    }

    public IEnumerator AnswerCoroutine() {
        yield return new WaitForSeconds(1.5f);
        resultImage.gameObject.SetActive(false);
        resultText.gameObject.SetActive(false);
        this.gameObject.SetActive(false);
        this.questionAnswered = true;
    }
}
