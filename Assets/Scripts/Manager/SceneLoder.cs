using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoder
{
    /// <summary>
    /// �V�[����J�ڂ�����֐�
    /// �����Ɏ��̃V�[������n��
    /// </summary>
    /// <param name="name"></param>
    public static void LoadScene(string name)
    {
        SceneManager.LoadSceneAsync(name);
    }
}
