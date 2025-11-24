# The Personal Organiser. Личный органайзер
The organizer application includes an alarm clock, a stopwatch, and a composable list of notes.

Приложение-органайзер включает в себя будильник, секундомер и составляемый список заметок.

# Subject Domain. Предметная область
## Alarm Clock and Stopwatch
A standard alarm clock and stopwatch. They flash a background color when the timer expires.

Обычные будильник и секундомер. Мигают фоновым цветом по завершении времени.

## Notes. Заметки
- A regular note with plain text.
- A "During the Day" note is a short note about what the user needs to do or learn within 24 hours. It also has a 24-hour storage limit. After this period, the note is deleted. Заметка типа «в течение дня» - это короткая заметка о том, что пользователю нужно сделать или узнать в течение суток (24 часов). Также имеет ограничение по времени хранения в 24 часа. После этого срока такая заметка удаляется.

## Relevance. Актуальность
Having an alarm clock and a stopwatch in one app is very convenient, as they are very similar time management tools. The "During the Day" mode allows you to take notes that delete themselves after 24 hours. People often write quick notes but then simply forget about them. They end up being stored for months, clogging up their device's memory.

В одном приложении очень удобно иметь будильник и секундомер, так как это очень похожие инструменты для организацией времени. Режим «в течение дня» позволяет делать заметки, удаляющиеся через 24 часа. Очень часто люди пишут какие-либо быстрые заметки, но потом просто забывают про них. В итоге, они хранятся месяцами и засоряют память  устройства.

# Architectural Characteristics. Архитектурные характеристики
## Stability. Устойчивость
If there is no free space in the memory or the memory itself, the application will not save any data.

Приложение, в случае отсутствия свободного места в памяти или самой памяти, не будет сохранять какие-либо данные.

## Autonomy. Автономность
The application is designed for PCs, allowing for autonomous operation of its components; no internet access or servers are required—all data is stored locally.

Приложение реализовано для ПК, что способствует автономности компонентов программы; не требуется доступ к Интернету или каким-либо серверам — все данные хранятся локально.

## Security. Безопасноть
Since all data is stored locally, it is stored securely, meaning that it is difficult to access such data from the outside.

Так как все данные хранятся локально, то они хранятся надёжно — трудно получить доступ к таким данным извне.

# The ER diagram and Use Cases. ER-диаграмма и сценарии использования
![Лаба-ER-диаграмма предметной области](https://github.com/izen57/PPO/blob/2c514c8854aa831435a64a3819361be08529ac2e/%D0%98%D0%B7%D0%BE%D0%B1%D1%80%D0%B0%D0%B6%D0%B5%D0%BD%D0%B8%D1%8F/%D0%9B%D0%B0%D0%B1%D0%B0-ER-%D0%B4%D0%B8%D0%B0%D0%B3%D1%80%D0%B0%D0%BC%D0%BC%D0%B0%20%D0%BF%D1%80%D0%B5%D0%B4%D0%BC%D0%B5%D1%82%D0%BD%D0%BE%D0%B9%20%D0%BE%D0%B1%D0%BB%D0%B0%D1%81%D1%82%D0%B8.svg)

![Лаба-Сценарии использования](https://github.com/izen57/PPO/blob/2c514c8854aa831435a64a3819361be08529ac2e/%D0%98%D0%B7%D0%BE%D0%B1%D1%80%D0%B0%D0%B6%D0%B5%D0%BD%D0%B8%D1%8F/%D0%9B%D0%B0%D0%B1%D0%B0-%D0%A1%D1%86%D0%B5%D0%BD%D0%B0%D1%80%D0%B8%D0%B8%20%D0%B8%D1%81%D0%BF%D0%BE%D0%BB%D1%8C%D0%B7%D0%BE%D0%B2%D0%B0%D0%BD%D0%B8%D1%8F.svg)

# Description of the Application Type and the Selected Technology Stack. Описание типа приложения и выбранного технологического стека
A desktop application written in C#. The graphical interface is implemented using Windows Presentation Foundation (WPF), part of the .NET ecosystem.

Desktop-приложение на C#. Графический интерфейс реализован через Windows Presentation Foundation (WPF) — часть экосистемы .NET.

# High-level Decomposition into Components. Верхнеуровневое разбиение на компоненты
![Лаба-Схема компонентов](https://github.com/izen57/PPO/blob/2c514c8854aa831435a64a3819361be08529ac2e/%D0%98%D0%B7%D0%BE%D0%B1%D1%80%D0%B0%D0%B6%D0%B5%D0%BD%D0%B8%D1%8F/%D0%9B%D0%B0%D0%B1%D0%B0-%D0%A1%D1%85%D0%B5%D0%BC%D0%B0%20%D0%BA%D0%BE%D0%BC%D0%BF%D0%BE%D0%BD%D0%B5%D0%BD%D1%82%D0%BE%D0%B2.svg)

# UML Class Diagram for Data and Logic. UML-схема классов для данных и логики
![Лаба-UML](https://github.com/izen57/PPO/blob/2c514c8854aa831435a64a3819361be08529ac2e/%D0%98%D0%B7%D0%BE%D0%B1%D1%80%D0%B0%D0%B6%D0%B5%D0%BD%D0%B8%D1%8F/%D0%9B%D0%B0%D0%B1%D0%B0-UML%20%D0%B4%D0%B0%D0%BD%D0%BD%D1%8B%D1%85%20%D0%B8%20%D0%BB%D0%BE%D0%B3%D0%B8%D0%BA%D0%B8.svg)

# UML Diagram of "Model" Classes. UML-схема «модельных» классов
![Лаба-UML модельных](https://github.com/izen57/PPO/blob/2c514c8854aa831435a64a3819361be08529ac2e/%D0%98%D0%B7%D0%BE%D0%B1%D1%80%D0%B0%D0%B6%D0%B5%D0%BD%D0%B8%D1%8F/%D0%9B%D0%B0%D0%B1%D0%B0-UML%20%D0%BC%D0%BE%D0%B4%D0%B5%D0%BB%D1%8C%D0%BD%D1%8B%D1%85.svg)
