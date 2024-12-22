using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConicalFlaskBManager : MonoBehaviour
{
    // Singleton Instance
    public static ConicalFlaskBManager Instance;

    // Serialized Fields
    [SerializeField] private GameObject flaskA;
    [SerializeField] private GameObject flaskB;
    [SerializeField] private Transform flaskATargetPosition;
    [SerializeField] private Transform flaskBTargetPosition;
    [SerializeField] private float speed = 1f;
    [SerializeField] private Animator testTubeAnimator;
    [SerializeField] private Renderer testTubeMaterial;
    [SerializeField] private Renderer flaskBRenderer;
    [SerializeField] private AudioSource irritatedAudioSource;
    [SerializeField] private AudioClip irritatedAudioClip;
    [SerializeField] private Animator characterAnimator;

    // Public Fields
    public Animator flaskBAnimator;

    // Private Fields
    private bool isMoving = false;

    // Unity Event Methods
    private void Awake()
    {
        InitializeFlaskBManager();
    }

    private void OnMouseDown()
    {
        HandleMouseClick();
    }

    // Public Methods
    public IEnumerator TestTubeAnimation()
    {
        yield return new WaitForSeconds(1);
        testTubeAnimator.SetBool("isPositionChanged", true);

        yield return new WaitForSeconds(1.5f);
        testTubeMaterial.material.SetFloat("_FillAmount", 0.8f);

        foreach (Material mat in flaskBRenderer.materials)
        {
            mat.SetFloat("_FillAmount", 0.23f);
        }

        yield return new WaitForSeconds(2);
        ConicalFlaskAManager.instance.shakeButton.SetActive(true);
    }

    public IEnumerator ShakeAnimation()
    {
        yield return new WaitForSeconds(1);
        flaskBAnimator.SetBool("isShaking", true);

        yield return new WaitForSeconds(1.75f);
        yield return new WaitForSeconds(1);

        flaskBAnimator.SetBool("isShaking", false);
        characterAnimator.SetBool("isIrritated", true);

        yield return new WaitForSeconds(3.3f);
        irritatedAudioSource.PlayOneShot(irritatedAudioClip);

        yield return new WaitForSeconds(3.3f);
        characterAnimator.SetBool("isIrritated", false);
    }

    // Private Methods
    private void InitializeFlaskBManager()
    {
        Instance = this;
        flaskBAnimator.enabled = false;
    }

    private void HandleMouseClick()
    {
        if (isMoving) return;

        StartCoroutine(MoveFlask());
        TestTubeManager.instance._isTestTube = true;
    }

    private IEnumerator MoveFlask()
    {
        isMoving = true;

        yield return StartCoroutine(MoveObject(flaskA, flaskATargetPosition.position));
        yield return StartCoroutine(MoveObject(flaskB, flaskBTargetPosition.position));

        isMoving = false;
    }

    private IEnumerator MoveObject(GameObject flask, Vector3 targetPosition)
    {
        while (!HasReachedTarget(flask, targetPosition))
        {
            MoveTowardsTarget(flask, targetPosition);
            yield return null;
        }

        flask.transform.position = targetPosition;
    }

    private bool HasReachedTarget(GameObject flask, Vector3 targetPosition)
    {
        return Vector3.Distance(flask.transform.position, targetPosition) <= 0.01f;
    }

    private void MoveTowardsTarget(GameObject flask, Vector3 targetPosition)
    {
        flask.transform.position = Vector3.MoveTowards(
            flask.transform.position,
            targetPosition,
            speed * Time.deltaTime
        );
    }
}