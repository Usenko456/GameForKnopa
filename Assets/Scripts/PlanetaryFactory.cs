using System.Collections.Generic;
using UnityEngine;
public class PlanetaryFactory : MonoBehaviour
{
    public List<IPlanetaryObject> GenerateSystem(Transform systemCenter)
    {
        List<IPlanetaryObject> planets = new List<IPlanetaryObject>();
        if (systemCenter == null)
        {
            Debug.LogError("System center is not assigned!");
            return planets;
        }
        int planetCount = Random.Range(5, 15); 
        float baseOrbitRadius = 40f;
        float minOrbitSpacing = 10f;
        float currentOrbitRadius = baseOrbitRadius;
        for (int i = 0; i < planetCount; i++)
        {
            double mass = Random.Range(0.01f, 60f);
            GameObject planetObj = new GameObject($"Planet_{i}");

            PlanetaryObject planet = planetObj.AddComponent<PlanetaryObject>();

            planet.Initialize(mass, systemCenter, currentOrbitRadius);

            planetObj.transform.SetParent(systemCenter);
            planets.Add(planet);

            float planetSize = planetObj.transform.localScale.x;
            float randomSpacingFactor = Random.Range(1f, 22f);
            currentOrbitRadius += (planetSize * 1.25f) + (minOrbitSpacing * randomSpacingFactor);

            Debug.Log($"Generated Planet_{i}: Mass = {mass}, OrbitRadius = {currentOrbitRadius}");
        }
        Debug.Log($"Generated a system with {planetCount} planets.");
        return planets;
    }
}
