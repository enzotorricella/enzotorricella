-- Laboratorio original, datos 100% ficticios. Compatible con SQLite.
-- No proviene de una base de datos empresarial o de clientes.
DROP TABLE IF EXISTS incident_events;
CREATE TABLE incident_events (
  id INTEGER PRIMARY KEY,
  category TEXT NOT NULL,
  severity TEXT NOT NULL CHECK (severity IN ('Low','Medium','High')),
  opened_at TEXT NOT NULL,
  resolved_at TEXT
);
INSERT INTO incident_events (id, category, severity, opened_at, resolved_at) VALUES
(1,'Printing','High','2026-01-08 09:00:00','2026-01-08 10:30:00'),
(2,'Login','Medium','2026-01-08 09:20:00',NULL),
(3,'Network','High','2026-01-09 11:00:00','2026-01-09 13:15:00'),
(4,'Printing','Low','2026-01-10 08:00:00','2026-01-10 08:30:00');

-- Tickets abiertos por categoría, útil para priorizar incidentes.
SELECT category, COUNT(*) AS open_incidents
FROM incident_events
WHERE resolved_at IS NULL
GROUP BY category
ORDER BY open_incidents DESC;

-- Tiempo de resolución promedio, expresado en minutos.
SELECT severity,
       ROUND(AVG((julianday(resolved_at) - julianday(opened_at)) * 1440), 1) AS avg_resolution_minutes
FROM incident_events
WHERE resolved_at IS NOT NULL
GROUP BY severity
ORDER BY severity;
