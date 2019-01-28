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
    public Sprite[] images;
    public Image frameA;
    public Image frameB;
    private bool ready;
    private bool loaded;

    // Start is called before the first frame update
    void Start()
    {
        progressBar.value = 0;
        loadingText.text = "Loading";
        progressBar.gameObject.SetActive(false);

        completeText.text = "Press Spacebar to Start";
        completeText.gameObject.SetActive(false);

        ready = false;
        loaded = false;
        StartCoroutine(LoadScene());
        StartCoroutine(ShowImages());
    }

    // Update is called once per frame
    void Update()
    {
        if (loaded)
        {
            completeText.gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ready = true;
            }
        }
    }

    IEnumerator ShowImages()
    {
        int counter = 2;
        Image frontImage = frameA;
        frontImage.sprite = images[0];
        Image backImage = frameB;
        backImage.sprite = images[1];
        while (true)
        {
            Debug.Log("start");
            Debug.Log(frontImage.color);
            Debug.Log(backImage.color);
            while (frontImage.color.a < 1 || backImage.color.a > 0)
            {
                frontImage.color = new Color(1, 1, 1, Mathf.Min(frontImage.color.a + 1 * Time.deltaTime, 1));
                backImage.color = new Color(1, 1, 1, Mathf.Max(backImage.color.a - 1 * Time.deltaTime, 0));
                yield return null;
            }

            yield return new WaitForSeconds(2);

            Image temp = frontImage;
            frontImage = backImage;
            backImage = temp;
            frontImage.sprite = images[counter % images.Length];
            counter++;
        }
    }

    IEnumerator LoadScene()
    {
        progressBar.gameObject.SetActive(true);

        AsyncOperation thread = SceneManager.LoadSceneAsync(scene);
        thread.allowSceneActivation = false;

        int dotCount = 0;
        while (progressBar.value != 1)
        {
            if ((dotCount / 40) % 2 == 0 && dotCount % 2 == 0)
            {
                loadingText.text = loadingText.text + ".";
            }
            else if (dotCount % 2 == 0)
            {
                loadingText.text = loadingText.text.Substring(0, loadingText.text.Length - 1);
            }
            dotCount++;
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