# Incident Triage API — laboratorio ficticio

**Proyecto original de demostración** para entrevistas de desarrollo .NET y soporte de aplicaciones. No deriva de Kardexa, EventChecking, SDI Software ni de sistemas de clientes.

## Ejecutar

Requiere el SDK .NET 10.

```bash
dotnet run --project labs/incident-triage-api/IncidentTriage.Api.csproj
```

El puerto local se indica en la salida de ASP.NET Core. Endpoints:

| Método | Ruta | Descripción |
| --- | --- | --- |
| GET | `/health` | Comprobación de disponibilidad |
| GET | `/incidents` | Listado con filtro `?status=Open` |
| GET | `/incidents/{id}` | Consulta por identificador |
| POST | `/incidents` | Crea incidente |
| PATCH | `/incidents/{id}/status` | Cambia estado |

Ejemplo:

```bash
curl -X POST http://localhost:5000/incidents -H "Content-Type: application/json" -d '{"title":"Demo issue","priority":"High"}'
```

Sustituí el puerto por el mostrado por `dotnet run`.

## Decisiones y límites

- Minimal API sin librerías de terceros.
- Validación de entradas y códigos HTTP diferenciados.
- `ConcurrentDictionary` permite una demostración simple en memoria; no reemplaza transacciones ni persistencia.
- No hay autenticación, autorización, paginación, auditoría ni base de datos: **no utilizar en producción**.
- El incidente inicial es inventado. Todos los ejemplos son sintéticos.
- No contiene secretos, endpoints comerciales, consultas de clientes ni implementación de productos privados.

## Próximas mejoras de laboratorio

Pruebas de integración, persistencia, OpenAPI y observabilidad. Ninguna está implementada todavía.
