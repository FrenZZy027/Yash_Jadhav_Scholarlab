using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimalQuizManager : MonoBehaviour
{
    //scriptable object list and incorrect animals list
    public List<AnimalData> animals;
    private List<string> incorrectAnimals = new List<string>();

    //gameobjects
    public GameObject redBucket, blueBucket;
    public GameObject finishScreenPanel;

    //text components
    public Text displayTaskText;
    public Text redBucketText;
    public Text blueBucketText;
    public Text scoreText;
    public Text congratulateText;
    public Text incorrectAnimalsText;


    //int values
    private int score = 0;
    private int correctCards = 0;
    private int remainingCards;    
    private System.Random random = new System.Random();

    //Quiz category in enum
    public enum QuizCategory { Flying, Insect, Diet, Social, Reproduction }

    //enum call
    private QuizCategory currentCategory;

    private void Start()
    {
        remainingCards = animals.Count;
        StartQuiz();
        if (finishScreenPanel != null)
        {
            finishScreenPanel.SetActive(false);
        }
    }


    //start quiz function
    public void StartQuiz()
    {
        
        
        currentCategory = (QuizCategory)random.Next(0, 5);
        Debug.Log("Sorting animals by: " + currentCategory);

        

        
        UpdateUI();
    }


    //updating UI function
    private void UpdateUI()
    {
        //sub categories;
        switch (currentCategory)
        {
            case QuizCategory.Flying:
                Debug.Log("Sort animals into Flying or Non-Flying.");
                displayTaskText.text = "Sort animals into Flying or Non-Flying.";
                redBucketText.text = "Flying";
                blueBucketText.text = "Non-Flying";

                break;
            case QuizCategory.Insect:
                Debug.Log("Sort animals into Insect or Non-Insect.");
                displayTaskText.text = "Sort animals into Insect or Non-Insect.";
                redBucketText.text = "Insect";
                blueBucketText.text = "Non-Insect";
                break;
            case QuizCategory.Diet:
                Debug.Log("Sort animals into Omnivorous or Herbivorous.");
                displayTaskText.text = "Sort animals into Omnivorous or Herbivorous.";
                redBucketText.text = "Omnivorous";
                blueBucketText.text = "Herbivorous";
                break;
            case QuizCategory.Social:
                Debug.Log("Sort animals into Lives in Group or Solo.");
                displayTaskText.text = "Sort animals into Lives in Group or Solo.";
                redBucketText.text = "Group";
                blueBucketText.text = "Solo";
                break;
            case QuizCategory.Reproduction:
                Debug.Log("Sort animals into Lays Eggs or Gives Birth.");
                displayTaskText.text = "Sort animals into Lays Eggs or Gives Birth.";
                redBucketText.text = "Lays Eggs";
                blueBucketText.text = "Gives Birth";
                break;
        }
    }


    //evaluation of which image is placed where
    public void EvaluateAnimal(AnimalData animal, GameObject selectedBucket)
    {
        bool isCorrect = false;

        
        Debug.Log("Evaluating: " + animal.animalName + " placed in " + selectedBucket.name);

        switch (currentCategory)
        {
            case QuizCategory.Flying:
                
                Debug.Log("Category: Flying, Animal Flying Category: " + animal.flyingCategory);

                isCorrect = (selectedBucket == redBucket && animal.flyingCategory == FlyingCategory.Flying) ||
                            (selectedBucket == blueBucket && animal.flyingCategory == FlyingCategory.NonFlying);
                break;

            case QuizCategory.Insect:
                
                Debug.Log("Category: Insect, Animal Insect Category: " + animal.insectCategory);

                isCorrect = (selectedBucket == redBucket && animal.insectCategory == InsectCategory.Insect) ||
                            (selectedBucket == blueBucket && animal.insectCategory == InsectCategory.NonInsect);
                break;

            case QuizCategory.Diet:
                
                Debug.Log("Category: Diet, Animal Diet Category: " + animal.dietCategory);

                isCorrect = (selectedBucket == redBucket && animal.dietCategory == DietCategory.Omnivorous) ||
                            (selectedBucket == blueBucket && animal.dietCategory == DietCategory.Herbivorous);
                break;

            case QuizCategory.Social:
                
                Debug.Log("Category: Social, Animal Social Category: " + animal.socialCategory);

                isCorrect = (selectedBucket == redBucket && animal.socialCategory == SocialCategory.Group) ||
                            (selectedBucket == blueBucket && animal.socialCategory == SocialCategory.Solo);
                break;

            case QuizCategory.Reproduction:
                
                Debug.Log("Category: Reproduction, Animal Reproduction Category: " + animal.reproductionCategory);

                isCorrect = (selectedBucket == redBucket && animal.reproductionCategory == ReproductionCategory.LaysEggs) ||
                            (selectedBucket == blueBucket && animal.reproductionCategory == ReproductionCategory.GivesBirth);
                break;
        }

        if (isCorrect)
        {
            score += 10; 
            correctCards++; 
            Debug.Log(animal.animalName + " placed correctly!");
        }
        else
        {
            Debug.Log(animal.animalName + " placed incorrectly!");
            incorrectAnimals.Add(animal.animalName);
        }

        UpdateScoreUI();
        
        remainingCards--;

        
        if (remainingCards <= 0)
        {
            EndGame();
        }
    }


    //end game logic
    private void EndGame()
    {
        Debug.Log("Game Over!");

        if (finishScreenPanel != null)
        {
            finishScreenPanel.SetActive(true);
        }

        if (incorrectAnimals.Count > 0)
        {
            incorrectAnimalsText.text = "Incorrectly placed animals:\n" + string.Join("\n", incorrectAnimals);
        }


        else
        {
            incorrectAnimalsText.text = "All animals placed correctly!";
        }

        if (score < 30)
        {
            congratulateText.text = "Better Luck Next Time!";
        }

        else
        {
            congratulateText.text = "Great Job!";
        }
    }


    //score update function
    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = $"Score: {score} | Correct Cards: {correctCards}";
        }
        
    }



}
