## Архитектура Jira Snapshot Planner

### Общий обзор

Приложение состоит из трёх основных слоёв/проектов:

- `DronesPlan.Domain`
- `DronesPlan.Infrastructure`
- `DronesPlan.Web`

Взаимодействие слоёв:

- Web обращается к Domain-сервисам (например, `SnapshotService`), не зная деталей Jira API или БД.
- Domain определяет интерфейсы `IJiraClient`, `ISnapshotRepository` и доменные модели (`IssueSnapshot`, `SnapshotBatch`).
- Infrastructure реализует эти интерфейсы с помощью HTTP-клиента и EF Core.

### Domain

- Доменные модели:
  - `IssueSnapshot` (ключ задачи, заголовок, статус, assignee, priority, issueType, jiraUrl, snapshotTimestamp).
  - `SnapshotBatch` (идентификатор батча, timestamp, источник JQL, коллекция `IssueSnapshot`).
- Интерфейсы:
  - `IJiraClient` — получение задач из Jira по JQL.
  - `ISnapshotRepository` — сохранение и получение батчей.
- Доменные сервисы:
  - `SnapshotService` — оркестрация синхронизации и вычисления приоритетов.

### Infrastructure

- Реализация `IJiraClient`:
  - Использует `HttpClient`.
  - Настройки: baseUrl, креды, JQL.
- Реализация `ISnapshotRepository`:
  - EF Core + PostgreSQL (MVP), база данных развёрнута в Docker-контейнере.
  - Таблица для IssueSnapshot с привязкой к батчу.
- Конфигурация:
  - Маппинг от Jira-DTO к доменным моделям.
  - Загрузка маппинга статусов ↔ приоритетов из конфигурации (appsettings).

### Web

- ASP.NET Core (Razor Pages или MVC).
- Функции:
  - Страница со списком задач и фильтрами.
  - Страница/секция с календарным видом.
  - Кнопка/эндпоинт для запуска синхронизации.
- Возможное API:
  - `/api/snapshots/current` — текущий батч.
  - `/api/snapshots/batch/{id}` — исторический батч.

