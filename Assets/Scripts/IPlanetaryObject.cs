using static MassClass; 
public interface IPlanetaryObject
{
    double Mass { get; }
    MassClassEnum MassClass { get; }
    void Orbit();
}
