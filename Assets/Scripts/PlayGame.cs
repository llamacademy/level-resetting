using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayGame : MonoBehaviour
{
    public void Play()
    {
        SceneTransitioner.Instance.LoadScene("Game", SceneTransitionMode.Fade);
    }
}
