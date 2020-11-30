using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlayerData : MonoBehaviour
{
    public GameObject player;
    public GameManager playerSettings;
   
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Save();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            Load();
        }
    }

    void Save()
        //save
    {
        Vector3 playerPotsion = player.transform.position;
        int score = playerSettings.score;
        int currentHp = playerSettings.currentHp;

        SaveOject saveObject = new SaveOject
        {
            score = score,
            playerPosition = playerPotsion,
            currentHp = currentHp
        };
        string json = JsonUtility.ToJson(saveObject);
        File.WriteAllText(Application.dataPath+"/save.txt", json);

        Debug.Log("Saved");
    }

    void Load()
        //load
    {

        if (File.Exists(Application.dataPath + "/save.txt"))
        {
              
            string saveString = File.ReadAllText(Application.dataPath + "/save.txt");

            Debug.Log("**Loading Game**");

            SaveOject saveObject =JsonUtility.FromJson<SaveOject>(saveString);

            player.transform.position = saveObject.playerPosition;
            playerSettings.score = saveObject.score;
            playerSettings.currentHp = saveObject.currentHp;

        }
        else
        {
            Debug.Log("No Save");
        }
    }

    class SaveOject
    {
        public int score;
        public Vector3 playerPosition;
        public int currentHp;
    }
}

