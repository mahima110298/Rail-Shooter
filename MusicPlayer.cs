
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour {

    private void Awake()
    {
        int numMusicPlayer = FindObjectsOfType<MusicPlayer>().Length;
        print("number of music player is " + numMusicPlayer);
        if (numMusicPlayer > 1)
        {
           Destroy(gameObject);
        }
        else
        DontDestroyOnLoad(this);
    }

}
