using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvasThemer : MonoBehaviour
{
    public static CanvasThemer Instance { get; private set; }
    
    [SerializeField] private CanvasScaler CanvasScaler;
    [SerializeField] private GameObject Button1;
    [SerializeField] private GameObject Button2;
    [SerializeField] private GameObject Text1;
    [SerializeField] private GameObject Text2;
    [SerializeField] private GameObject Panel1;
    [SerializeField] private GameObject Panel2;
    [SerializeField] private float[] UIScales;
    [SerializeField] private float HeightFactor;

    private Camera sceneCamera;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        sceneCamera = Camera.main;
        Application.targetFrameRate = 30;
    }

    void Start()
    {
        ScaleUI();
        Themeify();
    }

    public void ScaleUI()
    {
        int UIScaleIndex = SaveManager.Instance.LoadUIScale();

        CanvasScaler.scaleFactor = sceneCamera.pixelHeight * UIScales[UIScaleIndex] / HeightFactor;
    }

    public void SaveGUISmall()
    {
        SaveManager.Instance.SaveUIScale(0);
    }
    public void SaveGUIMedium()
    {
        SaveManager.Instance.SaveUIScale(1);
    }
    public void SaveGUILarge()
    {
        SaveManager.Instance.SaveUIScale(2);
    }

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }
    public void ReloadScene()
    {
        LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
            
    public void Themeify()
    {
        ThemeifyButtons("Button1", Button1);
        ThemeifyButtons("Button2", Button2);
        ThemeifyTMProText("Text1", Text1);
        ThemeifyTMProText("Text2", Text2);
        ThemeifyPanel("Panel1", Panel1);
        ThemeifyPanel("Panel2", Panel2);
    }

    private void ThemeifyButtons(string tag, GameObject buttonGameObject)
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
    private void ThemeifyTMProText(string tag, GameObject themer)
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
    private void ThemeifyPanel(string tag, GameObject themer)
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
        streamlinee.pixelsPerUnitMultiplier = streamliner.pixelsPerUnitMultiplier;
    }
    private void StreamLineTextMeshPro(TMPro.TextMeshProUGUI streamliner, TMPro.TextMeshProUGUI streamlinee)
    {
        streamlinee.font = streamliner.font;
        streamlinee.material = streamliner.material;
        streamlinee.fontStyle = streamliner.fontStyle;
        streamlinee.color = streamliner.color;
        streamlinee.colorGradient = streamliner.colorGradient;
        streamlinee.overrideColorTags = streamliner.overrideColorTags;
        //streamlinee.fontSize = streamliner.fontSize;
        //streamlinee.alignment = streamliner.alignment;
        streamlinee.enableKerning = streamliner.enableKerning;
    }
}
