# Map Generation
[↑Index](../../index.md)
[Design](../../design/systems/map-generation.md)

In its initial pass, the MapGenerator is only concerned with placing three types of features: Impassable terrain, water, and difficulty. The first two are both boolean; that is, terrain is either impassable or not, and either water or not. Difficulty is a float, which should generally be within 1.0 to 5.0. This isn't a hard limit and in the future worlds may be able to make terrain with less than 1.0 difficulty or greater than 5.0. However, difficulty should **never** be negative.

Instances of the MapGenerator class contain a WorldGenSpecs ScriptableObject, which specifies shape, size, and frequency of the three features (plus intensity of difficulty features).

In addition, MapGenerator also has these parameters:
```csharp
[SerializeField] private float width;
[SerializeField] private float height;
```
which specify its width and height in Unity units.

MapGenerator should never deal in negative coordinates; the map spans from the origin to (width, height).

(Second pass: Call flow field to find reachable areas, if too much unreachable reject, place resource points in reachable areas)
