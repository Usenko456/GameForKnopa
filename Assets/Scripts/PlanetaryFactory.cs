using System.Collections.Generic;
using UnityEngine;

public class PlanetaryFactory : MonoBehaviour
{
    public List<IPlanetaryObjectInterface> GenerateSystem(float totalMass, int planetCount, Transform systemCenter)
    {
        List<IPlanetaryObjectInterface> planets = new List<IPlanetaryObjectInterface>();

        if (systemCenter == null)
        {
            Debug.LogError("System center is not assigned!");
            return planets;
        }

        float baseOrbitRadius = 10f; // Початкова відстань від центру
        float previousPlanetSize = 3f; // Початковий розмір планети (для першої планети)

        for (int i = 0; i < planetCount; i++)
        {

            double mass = Random.Range(0.01f, 60f);
            GameObject planetObj = new GameObject("Planet" + i);

            SpriteRenderer spriteRenderer = planetObj.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = CreateCircleSprite();

            // Визначаємо розмір планети відповідно до маси
            float planetSize = Mathf.Clamp((float)(mass / 4f), 0f, 27f);

            // Визначаємо відстань між орбітами з урахуванням розміру планети
            float orbitRadius = baseOrbitRadius + previousPlanetSize * 30;
            previousPlanetSize = planetSize; // Оновлюємо розмір для наступної ітерації
            PlanetaryObject planet = planetObj.AddComponent<PlanetaryObject>();
            planet.Initialize(mass, systemCenter, orbitRadius);

            planetObj.transform.position = new Vector3(
                systemCenter.position.x + Mathf.Cos(i * Mathf.PI * 2 / planetCount) * orbitRadius,
                systemCenter.position.y + Mathf.Sin(i * Mathf.PI * 2 / planetCount) * orbitRadius,
                0);

            planets.Add(planet as IPlanetaryObjectInterface);
            
        }

        return planets;
    }


    private Sprite CreateCircleSprite()
    {
        Texture2D texture = new Texture2D(128, 128);
        Color[] pixels = texture.GetPixels();

        for (int x = 0; x < texture.width; x++)
        {
            for (int y = 0; y < texture.height; y++)
            {
                float dx = x - texture.width / 2;
                float dy = y - texture.height / 2;
                if (dx * dx + dy * dy <= (texture.width / 2) * (texture.width / 2))
                {
                    pixels[x + y * texture.width] = Color.white;
                }
                else
                {
                    pixels[x + y * texture.width] = Color.clear;
                }
            }
        }

        texture.SetPixels(pixels);
        texture.Apply();
        return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
    }
}
