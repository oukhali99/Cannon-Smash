using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GlobalEnvironnment : MonoBehaviour
{
    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void ReloadScene()
    {
        LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Log(bool verbose, string str)
    {
        if (verbose)
        {
            Debug.Log(str);
        }
    }

    public void ScaleUI(Camera cam, CanvasScaler cs, float heightFactor)
    {
        float resHeight = cam.pixelHeight;
        cs.scaleFactor = resHeight * PlayerPrefs.GetFloat("UIScale") / heightFactor;
    }

    public void ThemeifyButtons(string tag, GameObject buttonGameObject)
    {
        GameObject[] allButtons = GameObject.FindGameObjectsWithTag(tag);
        Button buttonButtonComponent = buttonGameObject.GetComponent<Button>();
        Image buttonImageComponent = buttonGameObject.GetComponent<Image>();
        TMPro.TextMeshProUGUI buttonTextComponent = buttonGameObject.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>();

        foreach (GameObject cur in allButtons)
        {
            Button curButtonComponent = cur.GetComponent<Button>();
            Image curImageComponent = cur.GetComponent<Image>();
            TMPro.TextMeshProUGUI curTextComponent = cur.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>();

            StreamlineButton(buttonButtonComponent, curButtonComponent);
            StreamlineImage(buttonImageComponent, curImageComponent);
            StreamLineTextMeshPro(buttonTextComponent, curTextComponent);

            cur.SetActive(false);
            cur.SetActive(true);
        }
    }
    public void ThemeifyTMProText(string tag, GameObject themer)
    {
        GameObject[] allTMProTextGameObjects = GameObject.FindGameObjectsWithTag(tag);
        TMPro.TextMeshProUGUI themerTMProText = themer.GetComponent<TMPro.TextMeshProUGUI>();

        foreach (GameObject cur in allTMProTextGameObjects)
        {
            TMPro.TextMeshProUGUI curTMProText = cur.GetComponent<TMPro.TextMeshProUGUI>();

            StreamLineTextMeshPro(themerTMProText, curTMProText);

            cur.SetActive(false);
            cur.SetActive(true);
        }
    }

    public void StreamlineButton(Button streamliner, Button streamlinee)
    {
        streamlinee.interactable = streamliner.interactable;
        streamlinee.transition = streamliner.transition;
        streamlinee.colors = streamliner.colors;
        streamlinee.navigation = streamliner.navigation;
    }
    public void StreamlineImage(Image streamliner, Image streamlinee)
    {
        streamlinee.sprite = streamliner.sprite;
        streamlinee.color = streamliner.color;
        streamlinee.material = streamliner.material;
        streamlinee.raycastTarget = streamliner.raycastTarget;
        streamlinee.type = streamliner.type;
        streamlinee.preserveAspect = streamliner.preserveAspect;
    }
    public void StreamLineTextMeshPro(TMPro.TextMeshProUGUI streamliner, TMPro.TextMeshProUGUI streamlinee)
    {
        streamlinee.font = streamliner.font;
        streamlinee.material = streamliner.material;
        streamlinee.fontStyle = streamliner.fontStyle;
        streamlinee.color = streamliner.color;
        streamlinee.colorGradient = streamliner.colorGradient;
        streamlinee.overrideColorTags = streamliner.overrideColorTags;
        streamlinee.fontSize = streamliner.fontSize;
        streamlinee.alignment = streamliner.alignment;
        streamlinee.enableKerning = streamliner.enableKerning;
    }
}
