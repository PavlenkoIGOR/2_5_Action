using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MySceneManager
{
    public void ChangeSceneMethod(string sceneName)
    {
        //switch (scene)
        //{
        //    case "MainMenu":
        //        SceneManager.LoadScene(scene);
        //        break;
        //    case "MainScene":
        //        break;
        //    case "Pause":
        //        break;
        //    default:
        //        break;
        //}
        switch (sceneName)
        {
            case "MainMenu":
                break;
            case "SampleScene":
                SceneManager.LoadScene(sceneName);
                Debug.Log($"Loaded scene {sceneName}");
                break;
            case "Pause":
                break;
            default:
                break;
        }
    }
}
//public enum Scenes
//{
//    MainMenu,
//    MainScene,
//    Pause
//}
