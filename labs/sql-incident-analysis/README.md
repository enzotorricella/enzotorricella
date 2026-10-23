# SQL — análisis de incidentes (datos sintéticos)

Ejercicio independiente de diagnóstico y agregación de datos para soporte de aplicaciones.

- `demo.sql` crea una tabla SQLite, inserta cuatro casos inventados y ejecuta consultas de incidentes abiertos y tiempos medios de resolución.
- No contiene información de usuarios, clientes, sistemas comerciales ni código de SDI Software.
- Para reproducir: `sqlite3 :memory: < labs/sql-incident-analysis/demo.sql`.

**Limitaciones:** cuatro filas no representan métricas productivas; el cálculo de duración simplifica zonas horarias y horarios laborales. Para un sistema real se necesitarían reglas de SLA, auditoría, seguridad, fuentes de tiempo consistentes y mayor cobertura de datos.
