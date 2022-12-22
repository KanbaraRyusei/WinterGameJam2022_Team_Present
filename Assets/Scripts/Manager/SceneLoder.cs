using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoder
{
    /// <summary>
    /// シーンを遷移させる関数
    /// 引数に次のシーン名を渡す
    /// </summary>
    /// <param name="name"></param>
    public static void LoadScene(string name)
    {
        SceneManager.LoadSceneAsync(name);
    }
}
