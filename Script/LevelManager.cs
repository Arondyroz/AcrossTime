using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public bool isFuture;

    public int levelNow = 1;

    public Transform futureLevel;
    public Transform pastLevel;

    public Player player;    

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    IEnumerator Start()
    {
        GameManager.instance.tittleScreen.SetActive(true);
        Cursor.lockState = CursorLockMode.None;

        yield return new WaitForSeconds(1.0f);

        //GameManager.instance.isPlaying = true;
    }

    public void NextLevel()
    {
        GameManager.instance.ChangeScene("Level " + (levelNow + 1));
    }

    public void Teleport()
    {
        Vector3 tempPos = pastLevel.position;

        pastLevel.position = futureLevel.position;
        futureLevel.position = tempPos;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
