# Map Generation

Maps are procedurally generated with some features to give layouts tactical implications:
- Terrain difficulty: Score from 1 to 5 (floating point) that determines how long it takes most enemies to traverse it.
- Impassable terrain: Only flying enemies can move through these
- Water/liquids: Only flying and swimming enemies can move through these. Water terrain's difficulty still matters for swimmers.
The settings on the frequency, shapes, sizes, et cetera of these features vary with different worlds and are a major part of what distinguishes them.
