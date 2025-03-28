using System.Collections.Generic;
using UnityEngine;
public class PlanetarySystem : MonoBehaviour
{
    public List<IPlanetaryObject> Planets { get; private set; } = new List<IPlanetaryObject>();
    public Transform systemCenter;    
    private PlanetaryFactory factory;
    void Start()
    {
        Debug.Log("PlanetarySystem: Initializing...");
        factory = gameObject.AddComponent<PlanetaryFactory>();

        if (systemCenter == null)
        {
            Debug.LogError("System center is not assigned!");
            return;
        }
        GenerateSystem();
    }
    private void GenerateSystem()
    {
        Planets = factory.GenerateSystem(systemCenter);

        if (Planets.Count > 0)
            Debug.Log($"PlanetarySystem: Successfully generated {Planets.Count} planets.");
        else
            Debug.LogError("PlanetarySystem: No planets were generated!");
    }
    void Update()
    {
        foreach (var planet in Planets)
        {
            planet?.Orbit();
        }
    }
}
