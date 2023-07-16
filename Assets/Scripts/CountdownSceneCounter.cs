using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class CountdownSceneController : MonoBehaviour
{
    public TextMeshProUGUI countdownText;
    public int callBySceneNumber;
    public AudioClip countdownSound;
    private AudioSource audioSource;

    private Vector3 originalScale;
    private Vector3 targetScale;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        originalScale = countdownText.transform.localScale;
        targetScale = originalScale * 3;
        StartCoroutine(StartCountdown());
    }

    IEnumerator StartCountdown()
    {
        for (int i = 3; i > 0; i--)
        {
            if (i <= 2)
            { 
                audioSource.PlayOneShot(countdownSound);
            }
            countdownText.text = i.ToString();
            StartCoroutine(AnimateText());
            yield return new WaitForSeconds(1);
        }

        countdownText.text = "GO!";
        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(callBySceneNumber);
    }

    IEnumerator AnimateText()
    {
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime;
            countdownText.transform.localScale = Vector3.Lerp(originalScale, targetScale, t);
            yield return null;
        }

        countdownText.transform.localScale = originalScale;
    }
}


