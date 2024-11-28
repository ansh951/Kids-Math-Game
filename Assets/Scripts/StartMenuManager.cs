using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenuManager : MonoBehaviour
{
    public Animator anim;
    public Animator aboutAnim;
    public GameObject setting;
    public GameObject About;

    public void PlayGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void SettingIncoming()
    {
        setting.SetActive(true);
        anim.SetBool("SettingIncoming", true);
        StartCoroutine(SettingIncomingIEnumerator());
    }

    public void SettingOutcoming()
    {
        anim.SetBool("SettingOutcoming", true);
        StartCoroutine (SettingOutcomingIEnumerator());

    }

    IEnumerator SettingIncomingIEnumerator()
    {
        yield return new WaitForSeconds(1f);
        anim.SetBool("SettingIncoming", false);

    }

    IEnumerator SettingOutcomingIEnumerator()
    {
        yield return new WaitForSeconds(1f);
        anim.SetBool("SettingOutcoming", false);
        setting.SetActive(false);
    }

    public void AboutIncoming()
    {
        About.SetActive(true);
        aboutAnim.SetBool("AboutIncoming", true);
        StartCoroutine(AboutIncomingIEnumerator());
    }

    public void AboutOutcoming()
    {
        aboutAnim.SetBool("AboutOutcoming", true);
        StartCoroutine(AboutOutcomingIEnumerator());
    }

    IEnumerator AboutIncomingIEnumerator()
    {
        yield return new WaitForSeconds(1f);
        aboutAnim.SetBool("AboutIncoming", false);
    }

    IEnumerator AboutOutcomingIEnumerator()
    {
        yield return new WaitForSeconds(1f);
        aboutAnim.SetBool("AboutOutcoming", false);
        About.SetActive(false);
    }

    public void ApplicationQuit()
    {
        Application.Quit();
    }
}
