using UnityEngine;

public class CamScroll : MonoBehaviour
{
    private Global global;
    private Vector2 center;

    private void Awake()
    {
        global = Global.Instance;
        center = new Vector2(global.Camera.pixelWidth / 2f, global.Camera.pixelHeight / 2f);
    }

    private void Update()
    {
        // the position of the mouse according to the center of the screen
        Vector2 mousePosCentralized = (Vector2)Input.mousePosition - center;
        float mouseAngle = Vector2.SignedAngle(Vector2.right, mousePosCentralized);

        
    }
}
