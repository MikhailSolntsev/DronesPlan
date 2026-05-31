## Задачи по реализации Jira Snapshot Planner

### Подготовка инфраструктуры и контекста

- Создать .NET solution с проектами `DronesPlan.Domain`, `DronesPlan.Infrastructure`, `DronesPlan.Web`.
- Настроить базовый `appsettings.json` с секциями `Jira` и маппингом статусов → приоритетов.

### Домейн и конфигурация

- Реализовать доменные модели `IssueSnapshot` и `SnapshotBatch` в `DronesPlan.Domain`.
- Описать интерфейсы `IJiraClient` и `ISnapshotRepository`.
- Реализовать сервис `SnapshotService` (без деталей Infrastructure).

### Интеграция с Jira

- Реализовать `JiraClient` в `DronesPlan.Infrastructure`, читающий задачи по JQL.
- Настроить маппинг Jira DTO → доменных моделей.

### Хранилище снапшотов

- Настроить EF Core + PostgreSQL (в Docker-контейнере) для хранения снапшотов.
- Реализовать `SnapshotRepository`.

### Веб-интерфейс

- Реализовать страницу со списком задач (таблица) и кнопкой синхронизации.
- Реализовать простой календарный/ленточный вид (на основе текущего снапшота).
- Добавить фильтры по assignee и статусу.

