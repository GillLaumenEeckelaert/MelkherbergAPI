SELECT show."Name", event."StartDateTime", event."EndDateTime", artist."Name"
FROM "Event" event
LEFT JOIN "Show" show
ON event."ShowId" = show."ShowId"
LEFT JOIN "EventArtist" eventartist
ON eventartist."EventId" = event."EventId"
LEFT JOIN "Artist" artist
ON artist."ArtistVersionId" = eventartist."ArtistVersionId"
ORDER BY event."StartDateTime" ASC