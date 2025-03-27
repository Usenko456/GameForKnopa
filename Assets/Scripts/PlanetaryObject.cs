using UnityEngine;
using static MassClass;

public class PlanetaryObject : MonoBehaviour
{
    public double mass { get; private set; }
    public MassClassEnum massClass { get; private set; }
    private Transform center;
    private float orbitSpeed;
    private float orbitRadius;
    private Renderer planetRenderer;
    private LineRenderer orbitLineRenderer;

    public void Initialize(double mass, Transform centerPoint, float orbitRadius)
    {
        this.mass = mass;
        massClass = DetermineMassClass(mass);
        center = centerPoint;
        this.orbitRadius = orbitRadius;

        transform.position = new Vector3(center.position.x + orbitRadius, center.position.y, 0);

        // Ініціалізуємо рендер планети
        planetRenderer = GetComponent<Renderer>();
        if (planetRenderer == null)
        {
            planetRenderer = gameObject.AddComponent<SpriteRenderer>();
        }

        // Отримуємо колір планети
        Color planetColor = GetColorForMass(massClass);

        // Створюємо LineRenderer для орбіти
        orbitLineRenderer = gameObject.AddComponent<LineRenderer>();
        orbitLineRenderer.positionCount = 100;
        orbitLineRenderer.startWidth = 0.05f;
        orbitLineRenderer.endWidth = 0.05f;
        orbitLineRenderer.material = new Material(Shader.Find("Sprites/Default"));

        // Задаємо колір орбіти відповідно до кольору планети
        orbitLineRenderer.startColor = planetColor;
        orbitLineRenderer.endColor = planetColor;

       

        orbitSpeed = Random.Range(10f, 50f);

        // Масштабування планети
        float planetScale= Mathf.Clamp((float)(mass / 4f), 0f, 27f);
        transform.localScale = Vector3.one * planetScale;

        // Встановлюємо колір планети
        planetRenderer.material = new Material(Shader.Find("Sprites/Default"));
        planetRenderer.material.color = planetColor;
    }

    void Update()
    {
        if (center!= null)
        {
            // Рухаємо планету по орбіті
            transform.RotateAround(center.position, Vector3.forward, orbitSpeed * Time.deltaTime);
            DrawOrbit();
        }
    }

    public void DrawOrbit()
    {
        if (orbitLineRenderer == null) return;

        for (int i = 0; i < orbitLineRenderer.positionCount; i++)
        {
            float angle = i * Mathf.PI * 2 / orbitLineRenderer.positionCount;
            float x = Mathf.Cos(angle) * orbitRadius;
            float y = Mathf.Sin(angle) * orbitRadius;
            orbitLineRenderer.SetPosition(i, new Vector3(x, y, 0) + center.position);
        }
    }

    private MassClassEnum DetermineMassClass(double mass)
    {
        if (mass < 0.00001) return MassClassEnum.Asteroidan;
        if (mass < 0.1) return MassClassEnum.Mercurian;
        if (mass < 0.5) return MassClassEnum.Subterran;
        if (mass < 2) return MassClassEnum.Terran;
        if (mass < 10) return MassClassEnum.Superterran;
        if (mass < 50) return MassClassEnum.Neptunian;
        return MassClassEnum.Jovian;
    }

    private Color GetColorForMass(MassClassEnum massClass)
    {
        switch (massClass)
        {
            case MassClassEnum.Asteroidan: return Color.gray;
            case MassClassEnum.Mercurian: return Color.yellow;
            case MassClassEnum.Subterran: return Color.green;
            case MassClassEnum.Terran: return Color.blue;
            case MassClassEnum.Superterran: return Color.red;
            case MassClassEnum.Neptunian: return Color.cyan;
            case MassClassEnum.Jovian: return Color.white;
            default: return Color.white;
        }
    }
}
