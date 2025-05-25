# ToDo-M321
Diese Applikation bietet eine API für eine ToDo-App.

## Setup
```
docker compose up -d --build
```

Dies startet zwei Container:
- todo-mariadb
- todo-backend

Diese beiden Container befinden sich im selben Netzwerk, damit die Kommunikation einfach abzuhandeln ist.

## todo-mariadb
Dies ist ein MariaDB Service, welcher für die Datenhaltung verantwortlich ist. Mit Hilfe eines Init-Scripts werden die benötigte Datenbank und Tabellen erstellt, sowie ein paar Testdaten.

Der Container hat ein Volume-Mapping auf das Host-System, damit die Daten auch nach einem Neustart des Containers persistent bleiben.

## todo-backend
Dies ist die API, welche die Manipulation der Daten gegen Aussen zur Verfügung stellt.
<br>
Der Service ist über **Port 8080** aufrufbar.

### Authentifizierung
Folgene Header müssen bei jeder Anfrage mitgegeben werden:
- email
- password

Diese werden dann Backend-seitig validiert. Falls ungültige Benutzerdaten mitgegeben werden, wird die Anfrage nicht weiter ausgeführt.

### Endpoints
Folgende Endpoints sind in der API definiert

#### GET /api/todo
Liefert alle Todos der Datenbank:
```json
[
  {
    "id": 1,
    "title": "Buy groceries",
    "description": "Milk, Bread, Eggs, Cheese"
  },
  {
    "id": 2,
    "title": "Finish report",
    "description": "Due by Friday, needs final review"
  }
]
```

#### GET /api/todo?todoId={todoId}
Es kann die ID eines Eintrages als Parameter mitgegeben werden, damit nur ein einziger Eintrag zurückkommt:
```json
[
  {
    "id": 5,
    "title": "Workout",
    "description": "45 minutes cardio and strength training"
  }
]
```

#### POST /api/todo
Erstellt ein neues ToDo in der Datenbank.
Folgender Body muss der Anfrage mitgegeben werden:
```json
{
  "title": "Create Video for M321",
  "description": "The Video needs to be good."
}
```

Bei erfolgreicher Ausführung kommt folgende Meldung zurück:
```json
{
  "message": "Todo created"
}
```

#### DELETE /api/todo?todoId={todoId}
Löscht einen gewünschten Eintrag aus der ToDo-Tabelle in der Datenbank.

Bei erfolgreicher Ausführung kommt folgende Meldung zurück:
```json
{
  "message": "Todo with id 9 deleted."
}
```