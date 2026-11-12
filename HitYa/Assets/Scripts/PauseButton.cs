using UnityEngine;

public class PauseButton : MonoBehaviour
{
    public bool pause = false;

    public void pressedPause()
    {
        pause = true;
    }
}
