using UnityEngine;
using static MassClass;

public interface IPlanetaryObjectInterface
{
    double Mass { get; }
    MassClassEnum MassClass { get; }
    void Orbit();
}
