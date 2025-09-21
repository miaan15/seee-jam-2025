using UnityEngine;
using UnityEngine.SceneManagement;


public class StartGame : MonoBehaviour
{
    private bool openTut = false;
    private bool canClickToLoad = false;

    public GameObject tutPanel;

    private void Update()
    {
        if (!openTut && Input.GetMouseButtonDown(0))
        {
            openTut = true;
            transform.GetChild(0).gameObject.SetActive(false);
            StartCoroutine(OpenTutorialSequence());
        }

        if (canClickToLoad && Input.GetMouseButtonDown(0))
        {
            LoadNew();
        }
    }

    private System.Collections.IEnumerator OpenTutorialSequence()
    {
        if (tutPanel != null)
        {
            tutPanel.SetActive(true);
        }
        yield return new WaitForSeconds(2f);
        canClickToLoad = true;
    }

    private void LoadNew()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (sceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(sceneIndex);
        }
    }
}