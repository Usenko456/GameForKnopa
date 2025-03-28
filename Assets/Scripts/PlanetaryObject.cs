using UnityEngine;
using static MassClass;
public class PlanetaryObject : MonoBehaviour, IPlanetaryObject
{
    public double Mass { get; private set; }
    public MassClassEnum MassClass { get; private set; }
    private Transform center;
    private float orbitSpeed;
    private float orbitRadius;
    private SpriteRenderer spriteRenderer;
    private LineRenderer orbitLineRenderer;
    public void Initialize(double mass, Transform centerPoint, float orbitRadius)
    {
        this.Mass = mass;
        this.MassClass = DetermineMassClass(mass);
        this.center = centerPoint;
        this.orbitRadius = orbitRadius;
        orbitSpeed = Random.Range(10f, 50f);

        transform.position = new Vector3(center.position.x + orbitRadius, center.position.y, 0);

        spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = 1;

        Color planetColor = GetPlanetColor(MassClass);
        spriteRenderer.color = planetColor;

        spriteRenderer.sprite = CreateCircleSprite((float)mass);

        float scaleFactor = Mathf.Clamp(Mathf.Pow((float)mass, 0.9f) / 5f * 4f, 0.4f, 80f); 
        transform.localScale = Vector3.one * scaleFactor;

        orbitLineRenderer = gameObject.AddComponent<LineRenderer>();
        orbitLineRenderer.positionCount = 100; 
        orbitLineRenderer.loop = true;

        orbitLineRenderer.startWidth = 0.1f;
        orbitLineRenderer.endWidth = 0.1f;
        orbitLineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        orbitLineRenderer.startColor = planetColor;  
        orbitLineRenderer.endColor = planetColor;    

        DrawOrbit();
    }
    public void Orbit()
    {
        if (center != null)
        {
            transform.RotateAround(center.position, Vector3.forward, orbitSpeed * Time.deltaTime);
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
    private Color GetPlanetColor(MassClassEnum massClass)
    {
        switch (massClass)
        {
            case MassClassEnum.Asteroidan:
                return Color.gray;
            case MassClassEnum.Mercurian:
                return Color.red;
            case MassClassEnum.Subterran:
                return Color.green;
            case MassClassEnum.Terran:
                return Color.blue;
            case MassClassEnum.Superterran:
                return Color.yellow;
            case MassClassEnum.Neptunian:
                return Color.cyan;
            case MassClassEnum.Jovian:
                return Color.magenta;
            default:
                return Color.white;
        }
    }
    private Sprite CreateCircleSprite(float mass)
    {    
        int size = Mathf.CeilToInt(mass * 5); 

        Texture2D texture = new Texture2D(size, size);
        for (int x = 0; x < texture.width; x++)
        {
            for (int y = 0; y < texture.height; y++)
            {
                float dist = Vector2.Distance(new Vector2(x, y), new Vector2(texture.width / 2, texture.height / 2));
                if (dist < texture.width / 2)
                    texture.SetPixel(x, y, Color.white);
                else
                    texture.SetPixel(x, y, Color.clear);
            }
        }
        texture.Apply();
        return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
    }
    private void DrawOrbit()
    {
        float angleStep = 360f / orbitLineRenderer.positionCount;
        for (int i = 0; i < orbitLineRenderer.positionCount; i++)
        {
            float angle = i * angleStep * Mathf.Deg2Rad;
            float x = Mathf.Cos(angle) * orbitRadius;
            float y = Mathf.Sin(angle) * orbitRadius;
            orbitLineRenderer.SetPosition(i, new Vector3(center.position.x + x, center.position.y + y, 0));
        }
    }
}
