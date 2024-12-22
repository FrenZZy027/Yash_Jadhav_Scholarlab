using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardDragAndDescPanel: MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler,  IPointerClickHandler
{

    // Card properties
    public AnimalData animalData;

    // Drag and drop state
    private Vector3 startPosition;
    private Transform startParent;

    // Description panel
    public GameObject descPanel;
    public Text descText;

    // On drag start
    public void OnBeginDrag(PointerEventData eventData)
    {
        startPosition = transform.position;
        startParent = transform.parent;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    // While dragging
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    // On drag end
    public void OnEndDrag(PointerEventData eventData)
    {
        GetComponent<CanvasGroup>().blocksRaycasts = true;

        // Check if dropped on a valid bucket
        GameObject target = eventData.pointerEnter;
        if (target != null && target.CompareTag("Bucket"))
        {
            FindObjectOfType<AnimalQuizManager>().EvaluateAnimal(animalData, target);
            Destroy(gameObject); // Remove the card
        }
        else
        {
            transform.position = startPosition; // Reset position if not dropped in a bucket
        }
    }

    // On card click
    public void OnPointerClick(PointerEventData eventData)
    {
        if (descPanel != null)
        {
            descPanel.SetActive(true);
            descText.text = animalData.description; // Update description text
        }
    }
}
