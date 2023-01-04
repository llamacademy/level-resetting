using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DemoUI : MonoBehaviour
{
    [SerializeField]
    private float ResetButtonShowDelay = 5f;
    [SerializeField]
    private Button ResetButton;
    [SerializeField]
    private bool UseLoadingIntermediaryScene = true;

    private IEnumerator Start()
    {
        ResetButton.gameObject.SetActive(false);
        yield return new WaitForSeconds(ResetButtonShowDelay);
        ResetButton.gameObject.SetActive(true);
    }

    public void ResetLevel()
    {
        //SceneManager.LoadScene(UseLoadingIntermediaryScene ? "Loading" : "Game");
        SceneTransitioner.Instance.LoadScene(
            UseLoadingIntermediaryScene ? "Loading" : "Game",
            SceneTransitionMode.Circle
        );
    }
}
