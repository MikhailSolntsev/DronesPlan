import { useState } from 'react'

const MOCK_TASKS = [
  { id: 'PROJ-123', priority: 71, summary: 'Авторизация для мобильного клиента', assignee: 'Iv', color: 'bg-orange-500' },
  { id: 'PROJ-120', priority: 61, summary: 'Оптимизация запросов', assignee: 'Pe', color: 'bg-blue-500' },
  { id: 'PROJ-110', priority: 51, summary: 'Сохранение черновиков', assignee: 'Si', color: 'bg-blue-500' },
  { id: 'PROJ-130', priority: 40, summary: 'Улучшение логирования', assignee: '', color: 'bg-slate-400' },
];

const DATES = ['18.05', '19.05', '20.05', '21.05', '22.05', '23.05', '24.05', '25.05', '26.05', '27.05'];

function App() {
  return (
    <div className="flex flex-col h-screen bg-white text-slate-800 font-sans">
      <header className="flex items-center justify-between px-6 py-4 border-b border-slate-200 shrink-0">
        <div className="flex items-center gap-6">
          <h1 className="text-xl font-bold text-slate-900 tracking-tight">DronesPlan</h1>
          <button className="px-4 py-2 bg-blue-600 text-white rounded-md hover:bg-blue-700 transition-colors text-sm font-semibold shadow-sm">
            Синхронизировать сейчас
          </button>
        </div>
        <div className="flex items-center gap-4 text-sm">
          <select className="border border-slate-300 rounded-md px-3 py-2 bg-white text-slate-700 outline-none focus:border-blue-500">
            <option>Текущая неделя</option>
            <option>Прошлая неделя</option>
          </select>
          <input type="text" placeholder="Фильтр..." className="border border-slate-300 rounded-md px-3 py-2 outline-none focus:border-blue-500 w-48" />
        </div>
      </header>

      <main className="flex flex-1 overflow-hidden">
        {/* Left Panel: Task Table */}
        <div className="w-[450px] shrink-0 border-r border-slate-200 flex flex-col overflow-auto bg-slate-50/50">
          <table className="w-full text-left border-collapse">
            <thead className="bg-slate-100/90 sticky top-0 z-20 shadow-sm backdrop-blur-sm">
              <tr>
                <th className="py-3 px-4 font-semibold text-slate-600 text-xs uppercase tracking-wider border-b border-slate-200 w-16">Pri</th>
                <th className="py-3 px-4 font-semibold text-slate-600 text-xs uppercase tracking-wider border-b border-slate-200 w-28">Key</th>
                <th className="py-3 px-4 font-semibold text-slate-600 text-xs uppercase tracking-wider border-b border-slate-200">Summary</th>
              </tr>
            </thead>
            <tbody>
              {MOCK_TASKS.map(task => (
                <tr key={task.id} className="border-b border-slate-100 hover:bg-slate-100/50 transition-colors group">
                  <td className="py-3 px-4">
                    <span className={`inline-block w-8 text-center rounded text-white text-xs font-bold py-1 shadow-sm ${task.color}`}>
                      {task.priority}
                    </span>
                  </td>
                  <td className="py-3 px-4 text-sm font-medium text-slate-700">{task.id}</td>
                  <td className="py-3 px-4 text-sm text-slate-600 truncate max-w-[200px]" title={task.summary}>{task.summary}</td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>

        {/* Right Panel: Gantt/Calendar Grid */}
        <div className="flex-1 overflow-auto bg-white flex flex-col relative">
          <div className="flex min-w-max border-b border-slate-200 bg-slate-100/90 sticky top-0 z-20 backdrop-blur-sm">
            {DATES.map(date => (
              <div key={date} className="w-24 shrink-0 py-3 text-center border-r border-slate-200 font-semibold text-slate-600 text-xs uppercase tracking-wider">
                {date}
              </div>
            ))}
          </div>
          
          <div className="flex flex-col min-w-max">
            {MOCK_TASKS.map((task, rowIndex) => (
              <div key={task.id} className="flex border-b border-slate-100 relative h-[53px] items-center hover:bg-slate-50/50 transition-colors">
                {DATES.map((date, colIndex) => (
                  <div key={date} className="w-24 shrink-0 h-full border-r border-slate-100/50 flex items-center justify-center">
                    {/* Fake blocks for visual testing of the grid */}
                    {colIndex >= rowIndex && colIndex <= rowIndex + 2 && task.assignee && (
                       <div className={`w-[96%] h-8 rounded-md flex items-center justify-center text-white text-xs font-bold shadow-sm ${task.color} cursor-grab hover:brightness-110 active:cursor-grabbing border border-black/10`}>
                          {task.assignee}
                       </div>
                    )}
                  </div>
                ))}
              </div>
            ))}
          </div>
        </div>
      </main>
    </div>
  )
}

export default App
