using UnityEngine;

public class TransparentSquare : MonoBehaviour
{
    void Start()
    {
        // Crear una textura de 100x100 píxeles transparente
        Texture2D texture = new Texture2D(100, 100);
        Color transparentColor = new Color(0, 0, 0, 0.5f); // Color completamente transparente

        // Llenar la textura con píxeles transparentes
        for (int x = 0; x < texture.width; x++)
        {
            for (int y = 0; y < texture.height; y++)
            {
                texture.SetPixel(x, y, transparentColor);
            }
        }
        texture.Apply();

        // Crear un sprite a partir de la textura
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

        // Asignar el sprite al SpriteRenderer del GameObject
        GetComponent<SpriteRenderer>().sprite = sprite;
    }
}