using UnityEngine;

public class Main_Field : MonoBehaviour
{
    [Header("   ===== Setting ===== ")]
    SpriteRenderer spriteRenderer;
    Transform backGround_obj;
    public void Set_Field()
    {
        backGround_obj = transform.GetChild(0);
        spriteRenderer = backGround_obj.GetComponent<SpriteRenderer>();

        MobileOptimization.Instance.Set_BgObjScale(spriteRenderer, backGround_obj);
    }
}
