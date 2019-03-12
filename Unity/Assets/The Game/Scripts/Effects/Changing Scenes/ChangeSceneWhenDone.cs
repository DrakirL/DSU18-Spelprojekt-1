using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class ChangeSceneWhenDone : ChangeScene
{
    VideoPlayer player;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<VideoPlayer>();
    }

    private void Update()
    {
        if (player.time >= player.length)
            Change();
    }
}