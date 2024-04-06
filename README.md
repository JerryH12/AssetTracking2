<h1>AssetTracking2</h1>

<h2>About the projekt</h2>
My plan was to use the previous AssetTracking, which already had some functionality. The biggest challenge was to combine the database tables with the existing base class and subclasses. TPC strategy was the first idea I came across but it caused so much trouble that I gave that up.

I decided to create proper tables with foreign keys and map the data from entities to other objects. Not really a business logical layer because I use it mostly for storing the objects in a list and select from there rather than the database.    
