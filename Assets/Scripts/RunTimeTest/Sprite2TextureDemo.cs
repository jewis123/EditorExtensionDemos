using UnityEngine;

public class Sprite2TextureDemo : MonoBehaviour
{
    public GameObject objWithSprite1;
    public GameObject objWithSprite2;
    public GameObject blendResult;

    private void OnEnable()
    {
        // Sprite sprite = objWithSprite1.GetComponent<SpriteRenderer>().sprite;
        // Sprite sprite2 = objWithSprite2.GetComponent<SpriteRenderer>().sprite;
        // Texture2D texture1 = sprite.texture;
        // Texture2D texture2 = sprite2.texture;
        // Texture2D blend = TwoToOne(texture1, texture2);
        // blendResult.GetComponent<SpriteRenderer>().sprite = Sprite.Create(blend, new Rect(0, 0, 64, 64), Vector2.zero);
    }

    public Texture2D TwoToOne(Texture2D source, Texture2D target)//图片合成
    {
        // Debug.Log(source);
        // Debug.Log(target);
        // for (int x = 0; x < target.width; x++)
        // {
        //     for (int y = 0; y < target.height; y++)
        //         if (target.GetPixel(x, y).a != 0)
        //         {
        //             source.SetPixel(x, y, new Color(target.GetPixel(x, y).r, target.GetPixel(x, y).g,
        //                     target.GetPixel(x, y).b, target.GetPixel(x, y).a));
        //         }
        // }
        // source.Apply();
        // return source;

        //todo
        return new Texture2D(100,100);
    }
}