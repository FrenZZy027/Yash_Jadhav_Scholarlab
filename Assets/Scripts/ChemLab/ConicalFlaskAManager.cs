using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConicalFlaskAManager : MonoBehaviour
{
    // Singleton Instance
    public static ConicalFlaskAManager instance;

    // Serialized Fields
    [SerializeField] private GameObject flaskPositionPoint;
    [SerializeField] private float speed;

    //Public Fields
    public GameObject shakeButton;
    public Animator flaskAAnimator;
    public Renderer flaskARenderer;

    // Private Fields
    private bool isMoving = false;

    // Unity Event Methods
    private void Awake()
    {
        InitializeFlask();
    }

    private void OnMouseDown()
    {
        HandleFlaskClick();
    }

    // Private Methods
    private void InitializeFlask()
    {
        instance = this;
        shakeButton.SetActive(false);
    }

    private void HandleFlaskClick()
    {
        if (isMoving) return;
        StartCoroutine(MoveFlask());
    }

    // Coroutines
    private IEnumerator MoveFlask()
    {
        isMoving = true;

        while (isMoving)
        {
            MoveTowardsTarget();

            if (HasReachedTarget())
            {
                isMoving = false;
            }

            yield return new WaitForSeconds(0.001f);
        }
    }

    private void MoveTowardsTarget()
    {
        transform.position = Vector3.MoveTowards(
            transform.position, 
            flaskPositionPoint.transform.position, 
            speed * Time.deltaTime
        );
    }

    private bool HasReachedTarget()
    {
        return Vector3.Distance(transform.position, flaskPositionPoint.transform.position) < 0.001f;
    }
}