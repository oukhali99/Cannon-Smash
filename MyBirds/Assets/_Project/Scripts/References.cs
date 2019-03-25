using UnityEngine;

public class References : MonoBehaviour {
    public static GameObject Player_Ball;
    public static GameObject Main_Camera;
    public static GameObject Group_Obstacles;
    public static GameObject Group_Monsters;
    public static Object Prefab_Explosion;
    public static GameObject Catapult_FrontArm;
    public static GameObject Catapult_BackArm;
    public static GameObject Text_TopLeftPanel;
    public static GameObject Panel_Main;
    public static GameObject Button_Play;
    public static GameObject Panel_End;

    public GameObject Player_Ball_Helper;
    public GameObject Main_Camera_Helper;
    public GameObject Group_Obstacles_Helper;
    public GameObject Group_Monsters_Helper;
    public Object Prefab_Explosion_Helper;
    public GameObject Catapult_FrontArm_Helper;
    public GameObject Catapult_BackArm_Helper;
    public GameObject Text_TopLeftPanel_Helper;
    public GameObject Panel_Main_Helper;
    public GameObject Button_Play_Helper;
    public GameObject Panel_End_Helper;

    private void Awake()
    {
        References.Player_Ball = this.Player_Ball_Helper;
        References.Main_Camera = this.Main_Camera_Helper;
        References.Group_Obstacles = this.Group_Obstacles_Helper;
        References.Group_Monsters = this.Group_Monsters_Helper;
        References.Prefab_Explosion = this.Prefab_Explosion_Helper;
        References.Catapult_FrontArm = this.Catapult_FrontArm_Helper;
        References.Catapult_BackArm = this.Catapult_BackArm_Helper;
        References.Text_TopLeftPanel = this.Text_TopLeftPanel_Helper;
        References.Panel_Main = this.Panel_Main_Helper;
        References.Button_Play = this.Button_Play_Helper;
        References.Panel_End = this.Panel_End_Helper;
    }
}
