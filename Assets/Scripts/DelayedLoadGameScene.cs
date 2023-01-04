using System.Collections;
using UnityEngine;

public class DelayedLoadGameScene : MonoBehaviour
{
    [SerializeField]
    private float Delay = 3f;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(Delay);
        SceneTransitioner.Instance.LoadScene("Game", SceneTransitionMode.Circle);
    }
}
