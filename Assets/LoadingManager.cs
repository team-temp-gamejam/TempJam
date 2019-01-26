using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour
{
    public int scene;
    public Slider progressBar;
    public Text loadingText;
    public Text completeText;
    private bool ready;
    private bool loaded;

    // Start is called before the first frame update
    void Start()
    {
        progressBar.value = 0;
        loadingText.text = "Loading...";
        progressBar.gameObject.SetActive(false);

        completeText.text = "Press Numlock to Start";
        completeText.gameObject.SetActive(false);

        ready = false;
        loaded = false;
        StartCoroutine(LoadScene());
    }

    // Update is called once per frame
    void Update()
    {
        if (loaded)
        {
            completeText.gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Numlock))
            {
                Debug.Log("ready!!!");
                ready = true;
            }
        }
    }

    IEnumerator LoadScene()
    {
        progressBar.gameObject.SetActive(true);

        AsyncOperation thread = SceneManager.LoadSceneAsync(scene);
        thread.allowSceneActivation = false;

        while (progressBar.value != 1)
        {
            progressBar.value += Mathf.Min(thread.progress / 0.85f - progressBar.value, 0.2f) * Time.deltaTime;
            yield return null;
        }
        loaded = true;

        progressBar.gameObject.SetActive(false);

        while (!ready)
        {
            yield return null;
        }

        thread.allowSceneActivation = true;
    }
}