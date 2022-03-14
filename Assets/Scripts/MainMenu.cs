using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject playLight, exitLight;
    [SerializeField] Animator global;
    [SerializeField] GameObject play;

    Light2D lit;
    float transTime = 2f, fade = 7;

    private void Start() {
        lit = playLight.GetComponent<Light2D>();
    }

    public void PlayGameFor4()
    {
        StartCoroutine(Transition());
    }

    IEnumerator Transition() {
        float time = 0;
        global.enabled = true;
        float currentIntensity = lit.intensity;
        lit.GetComponent<Animator>().enabled = false;
        lit.intensity = currentIntensity;
        while (time < transTime) {
            time += Time.deltaTime;
            lit.intensity = Mathf.Clamp(lit.intensity, 0, lit.intensity - Time.deltaTime / fade);
            yield return new WaitForEndOfFrame();
        }
        SceneManager.LoadScene("SampleScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void HoverPlay() {
        playLight.SetActive(true);
    }

    public void HoverExitPlay() {
        playLight.SetActive(false);
    }

    public void HoverExit() {
        exitLight.SetActive(true);
    }

    public void HoverExitExit() {
        exitLight.SetActive(false);
    }
}
