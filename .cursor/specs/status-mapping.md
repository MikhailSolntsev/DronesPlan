## Маппинг статусов Jira в приоритеты и цвета

Этот файл описывает конфигурационный маппинг, который затем используется в `appsettings`/опциях.

### Базовые правила

- 0–49: задача ещё не началась (разные статусы бэклога).
- 51: В работе (разработка).
- 61: Ревью.
- 69: Готово к тестированию.
- 71: Тестирование.
- 81: Сборка.
- 91: Релизное тестирование.
- 99: Релиз.
- 100: Закрыто.

### Пример логической таблицы (псевдо)

> Конкретные названия статусов/типов задач нужно будет подставить из вашей Jira.

```text
IssueType | JiraStatus              | Priority | Color
----------+-------------------------+----------+------
Story     | Open                    | 10       | none
Story     | Analysis                | 20       | none
Story     | Ready for Development   | 40       | none
Story     | In Progress             | 51       | blue
Story     | In Review               | 61       | blue
Story     | Ready for Testing       | 69       | blue
Story     | Testing                 | 71       | orange
Story     | Build                   | 81       | blue
Story     | Release Testing         | 91       | orange
Story     | Released                | 99       | none
Story     | Closed                  | 100      | none
Bug       | Open                    | 15       | none
Bug       | In Progress             | 51       | blue
Bug       | In Review               | 61       | blue
Bug       | Ready for Testing       | 69       | blue
Bug       | Testing                 | 71       | orange
Bug       | Closed                  | 100      | none
```

### Соответствие цветов

- **blue**: статусы, относящиеся к разработке/ревью/сборке.
- **orange**: статусы, относящиеся к тестированию/релизному тестированию.
- **none/grey**: прочие статусы (бэклог, релиз, закрыто).

