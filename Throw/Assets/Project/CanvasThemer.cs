using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvasThemer : MonoBehaviour
{
    [SerializeField] protected Camera SceneCamera;
    [SerializeField] protected CanvasScaler CanvasScaler;
    [SerializeField] protected GameObject Button1;
    [SerializeField] protected GameObject Button2;
    [SerializeField] protected GameObject Text1;
    [SerializeField] protected GameObject Panel1;

    // Start is called before the first frame update
    void Start()
    {
        ScaleUI(SceneCamera, CanvasScaler);
        Themeify();
    }

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }
    public void ReloadScene()
    {
        LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ScaleUIToHeightFactor(Camera cam, CanvasScaler cs, float heightFactor)
    {
        float resHeight = cam.pixelHeight;
        cs.scaleFactor = resHeight * PlayerPrefs.GetFloat("UIScale") / heightFactor;
    }


    public void Themeify()
    {
        ThemeifyButtons("Button1", Button1);
        ThemeifyButtons("Button2", Button2);
        ThemeifyTMProText("Text1", Text1);
        ThemeifyPanel("Panel1", Panel1);

    }
    public void ThemeifyButtons(string tag, GameObject buttonGameObject)
    {
        if (buttonGameObject == null)
        {
            return;
        }

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
        if (themer == null)
        {
            return;
        }

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
    public void ThemeifyPanel(string tag, GameObject themer)
    {
        if (themer == null)
        {
            return;
        }

        GameObject[] gameObjectList = GameObject.FindGameObjectsWithTag(tag);
        Image themerImage = themer.GetComponent<Image>();

        foreach (GameObject curGameObject in gameObjectList)
        {
            Image curImage = curGameObject.GetComponent<Image>();

            StreamlineImage(themerImage, curImage);

            curGameObject.SetActive(false);
            curGameObject.SetActive(true);
        }
    }

    private void ScaleUI(Camera cam, CanvasScaler cs)
    {
        ScaleUIToHeightFactor(cam, cs, cam.pixelHeight);
    }

    private void StreamlineButton(Button streamliner, Button streamlinee)
    {
        streamlinee.interactable = streamliner.interactable;
        streamlinee.transition = streamliner.transition;
        streamlinee.colors = streamliner.colors;
        streamlinee.navigation = streamliner.navigation;
    }
    private void StreamlineImage(Image streamliner, Image streamlinee)
    {
        streamlinee.sprite = streamliner.sprite;
        streamlinee.color = streamliner.color;
        streamlinee.material = streamliner.material;
        streamlinee.raycastTarget = streamliner.raycastTarget;
        streamlinee.type = streamliner.type;
        streamlinee.preserveAspect = streamliner.preserveAspect;
    }
    private void StreamLineTextMeshPro(TMPro.TextMeshProUGUI streamliner, TMPro.TextMeshProUGUI streamlinee)
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
