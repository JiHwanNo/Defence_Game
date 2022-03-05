using System;
using UnityEngine;

public class MobileOptimization : Singleton<MobileOptimization>
{
    [HideInInspector] float base_width, base_height;
    [HideInInspector] float width = 3200, height = 1440;
    [HideInInspector] float ratio_w, ratio_h;


    public float Ratio_W { get { return ratio_w; } }
    public float Ratio_H { get { return ratio_h; } }
    public float Width { get { return width; } }
    public float Height { get { return height; } }

    /// <summary>
    /// �޴��� ũ�⿡ �´� UI ũ�⸦ �ʹݿ� �������ִ� �Լ�
    /// </summary>
    public void Set_MobileValueCavas(float w, float h)
    {
        base_width = w;
        base_height = h;

        ratio_w = base_width / width;
        ratio_h = base_height / height;
    }

    public void UI_ScaleOptimization(Transform panel)
    {
        Transform[] UI = new Transform[panel.GetChild(0).childCount];
        for (int i = 0; i < UI.Length; i++)
        {
            UI[i] = panel.GetChild(0).GetChild(i);
            UI[i].localScale = new Vector2(Ratio_W, Ratio_H);
        }
    }
   
    /// <summary>
    /// obj�� ���� ī�޶� ȭ�� ũ�⿡ �� �°� �������� �ϱ� ���� ���� �Լ�.
    /// </summary>
    /// <param name="sr">��������Ʈ ������ ����</param>
    /// <param name="Bg">����ȭ �� ������Ʈ</param>
    public void Set_BgObjScale(SpriteRenderer sr, Transform Bg)
    {
        float spriteX = sr.sprite.bounds.size.x;
        float spriteY = sr.sprite.bounds.size.y;
        float screenY = Camera.main.orthographicSize * 2;
        float screenX = screenY / Screen.height * Screen.width;
        Bg.localScale = new Vector3((float)Math.Truncate((screenX / spriteX) * 1000) / 1000, (float)Math.Truncate((screenY / spriteY) * 1000) / 1000, 1f);
    }


}

