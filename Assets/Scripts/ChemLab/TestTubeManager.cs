using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTubeManager : MonoBehaviour
{
    // Singleton instance
    public static TestTubeManager instance;

    // Serialized Fields
    [SerializeField] private Animator tubeAnimator;
    [SerializeField] private Renderer testTubeMaterial;
    [SerializeField] private GameObject pourButton;
    [SerializeField] private Animator characterAnimator;
    [SerializeField] private AudioSource excitedAudioSource;
    [SerializeField] private AudioClip excitedAudioClip;

    // Private Fields
    public bool _isTestTube = false;

    // Unity Event Methods
    private void Awake()
    {
        instance = this;
        InitializeTestTube();
    }

    private void OnMouseDown()
    {
        EnablePourButton();
    }

    // Public Methods
    public void PlayAnimation()
    {
        if (!_isTestTube)
        {
            DisablePourButton();
            StartCoroutine(AnimationPlay());
        }
        else
        {
            TriggerFlaskBAnimation();
        }
    }

    public void Shake()
    {
        if (!_isTestTube)
        {
            DisableShakeButton();
            StartCoroutine(ShakeAnimation());
        }
        else
        {
            DisableShakeButton();
            StartCoroutine(ConicalFlaskBManager.Instance.ShakeAnimation());
        }
    }

    // Private Methods
    private void InitializeTestTube()
    {
        pourButton.SetActive(false);
        ConicalFlaskAManager.instance.flaskAAnimator.enabled = false;
    }

    private void EnablePourButton()
    {
        pourButton.SetActive(true);
    }

    private void DisablePourButton()
    {
        pourButton.SetActive(false);
    }

    private void DisableShakeButton()
    {
        ConicalFlaskAManager.instance.shakeButton.SetActive(false);
    }

    private void TriggerFlaskBAnimation()
    {
        ConicalFlaskBManager.Instance.flaskBAnimator.enabled = true;
        DisablePourButton();
        StartCoroutine(ConicalFlaskBManager.Instance.TestTubeAnimation());
    }

    // Coroutines
    private IEnumerator AnimationPlay()
    {
        yield return new WaitForSeconds(1);
        tubeAnimator.SetBool("isPositionChanged", true);
        yield return new WaitForSeconds(1.5f);

        testTubeMaterial.material.SetFloat("_FillAmount", 0.55f);

        foreach (Material mat in ConicalFlaskAManager.instance.flaskARenderer.materials)
        {
            mat.SetFloat("_FillAmount", 0.23f);
        }

        yield return new WaitForSeconds(2);
        ConicalFlaskAManager.instance.flaskAAnimator.enabled = true;
        ConicalFlaskAManager.instance.shakeButton.SetActive(true);
        tubeAnimator.SetBool("isPositionChanged", false);
    }

    private IEnumerator ShakeAnimation()
    {
        yield return new WaitForSeconds(1);
        ConicalFlaskAManager.instance.flaskAAnimator.SetBool("isShaking", true);
        yield return new WaitForSeconds(1.75f);

        foreach (Material mat in ConicalFlaskAManager.instance.flaskARenderer.materials)
        {
            mat.SetColor("_Tint", new Color32(100, 0, 200, 170));
        }

        yield return new WaitForSeconds(1);
        ConicalFlaskAManager.instance.flaskAAnimator.SetBool("isShaking", false);
        characterAnimator.SetBool("isExcited", true);

        yield return new WaitForSeconds(3.3f);
        excitedAudioSource.PlayOneShot(excitedAudioClip);

        yield return new WaitForSeconds(3.3f);
        characterAnimator.SetBool("isExcited", false);
        ConicalFlaskAManager.instance.flaskAAnimator.enabled = false;
    }
}