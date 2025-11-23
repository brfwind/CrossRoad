using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour
{
    public static TransitionManager instance;
    private CanvasGroup canvasGroup;
    public float scaler;

    #region 单例模式
    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();

        //instance是该静态类的一个静态对象
        //下面代码维持intance的单例模式
        //没有就加，有就删
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        //让渐变Panel在切换场景后，依旧保留，不被销毁
        DontDestroyOnLoad(this);
    }
    #endregion

    #region 开始游戏时，让画面变白
    private void Start()
    {
        StartCoroutine(Fade(0));
    }
    #endregion

    #region 供外部类调用切换场景方法的方法(Public)
    public void Transition(string sceneName)
    {
        Time.timeScale = 1;
        StartCoroutine(TransitionToScene(sceneName));
    }
    #endregion

    #region 切换场景方法
    //让屏幕先渐变黑，然后切换场景，再渐变白
    private IEnumerator TransitionToScene(string sceneName)
    {
        yield return Fade(1);

        yield return SceneManager.LoadSceneAsync(sceneName);

        yield return Fade(0);
    }
    #endregion

    #region 渐变实现逻辑
    //这里传入的amount是需要的终Alpha值
    //为1就执行渐变黑，为0执行渐变白
    private IEnumerator Fade(int amount)
    {
        //通过CanvasGroup组件，拦截输入
        canvasGroup.blocksRaycasts = true;

        //循环直到达到目标Alpha值
        while (canvasGroup.alpha != amount)
        {
            switch (amount)
            {
                case 1:
                    canvasGroup.alpha += Time.deltaTime * scaler;
                    break;
                case 0:
                    canvasGroup.alpha -= Time.deltaTime * scaler;
                    break;
            }

            //一帧只执行一次while循环
            yield return null;
        }

        //恢复输入功能
        canvasGroup.blocksRaycasts = false;
    }
    #endregion
}