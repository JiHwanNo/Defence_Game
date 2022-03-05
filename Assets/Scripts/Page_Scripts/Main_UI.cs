using UnityEngine;

public class Main_UI : MonoBehaviour
{
    [Header("   ===== Setting ===== ")]
    MobileOptimization optimization;
    GameObject panel;
    float width, height;
    public void Set_UI()
    {
        panel = transform.GetChild(0).gameObject;
        width = panel.GetComponent<RectTransform>().rect.width;
        height = panel.GetComponent<RectTransform>().rect.height;
        optimization = MobileOptimization.Instance;

        optimization.Set_MobileValueCavas(width, height);
        optimization.UI_ScaleOptimization(panel.transform);
        
    }
}
