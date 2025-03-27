using System.Collections.Generic;
using UnityEngine;
using static MassClass;

public class PlanetarySystem : MonoBehaviour
{
    public List<IPlanetaryObjectInterface> Planets { get; private set; } = new List<IPlanetaryObjectInterface>();
    public Transform systemCenter;
    public float totalMass = 1000f;         // Загальна маса для системи
    public int planetCount = 5;             // Кількість планет

    private PlanetaryFactory factory;  // Створюємо поле для PlanetaryFactory

    void Start() // Викликаємо Initialize в Start
    {
        // Додаємо PlanetaryFactory як компонент
        factory = gameObject.AddComponent<PlanetaryFactory>();
        Initialize();
    }

    // Метод ініціалізації
    public void Initialize()
    {
        // Викликаємо GenerateSystem через доданий компонент PlanetaryFactory
        if (systemCenter == null)
        {
            Debug.LogError("System center is not assigned! Cannot generate planets.");
            return; // Якщо центр не задано, не генеруємо планети
        }

        Planets = factory.GenerateSystem(totalMass, planetCount, systemCenter);

    }

    // Оновлення орбіт планет
    void Update()
    {
        foreach (var planet in Planets)
        {
            planet?.Orbit();
        }
    }
}
