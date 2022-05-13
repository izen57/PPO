# Лаба 1
## Личный органайзер
Приложение-органайзер включает в себя будильник, секундомер и составляемый список заметок с возможностью переключения каждой "В течение дня".

## Предметная область
### Будильник и секундомер
Обычные будильник и секундомер. Мигают фоновым цветом по завершении времени или при остановке.

### Заметки
- Обычная заметка - короткий текст объёмом в 1000 символов. Только пользователь имеет право удалять такие заметки.
- Заметка в режиме "в течение дня" - это короткая заметка о том, что пользователю нужно сделать или узнать в течение суток (24 часов). Эта заметка тоже имеет ограничение по словам (300 слов) и времени хранения (24 часа). После этого срока такая заметка удаляется.

## Актуальность
В одном приложении очень удобно иметь будильник и секундомер, так как это очень похожие инструменты для работы с организацией времени. Режим "в течение дня" позволяет делать заметки, удаляющиеся через 24 часа. Очень часто люди пишут какие-либо "быстрые" заметки, но потом просто забывают про них. В итоге, они хранятся месяцами и засоряют память  устройства.

## Архитектурные характеристики
### Устойчивость
Приложение, в случае отсутствия свободного места в памяти или самой памяти, не будет сохранять какие-либо данные.

### Автономность
Приложение будет реализовано для ПК, что способствует автономности компонентов программы; не требуется доступ к Интернету или каким-либо серверам - все данные хранятся локально.

### Безопасноть
Так как все данные хранятся локально, то они хранятся надёжно - трудно получить доступ к таким данным извне.

## Use-case-диаграмма
![Лаба-ER-диаграмма предметной области](%D0%9B%D0%B0%D0%B1%D0%B0-%D0%A1%D1%86%D0%B5%D0%BD%D0%B0%D1%80%D0%B8%D0%B8%20%D0%B8%D1%81%D0%BF%D0%BE%D0%BB%D1%8C%D0%B7%D0%BE%D0%B2%D0%B0%D0%BD%D0%B8%D1%8F.svg)

## ER-диаграмма
![Лаба-Сценарии использования](%D0%9B%D0%B0%D0%B1%D0%B0-ER-%D0%B4%D0%B8%D0%B0%D0%B3%D1%80%D0%B0%D0%BC%D0%BC%D0%B0%20%D0%BF%D1%80%D0%B5%D0%B4%D0%BC%D0%B5%D1%82%D0%BD%D0%BE%D0%B9%20%D0%BE%D0%B1%D0%BB%D0%B0%D1%81%D1%82%D0%B8.svg)

# Лаба 2
## Описание типа приложения и выбранного технологического стека
Desktop-приложение на C#. Графический интерфейс реализован через Windows Forms.

## Верхнеуровневое разбиение на компоненты
![Лаба-Схема компонентов](https://github.com/izen57/PPO/blob/005f61d72e777f0f89d859bb1026fd7e9c967d80/%D0%9B%D0%B0%D0%B1%D0%B0-%D0%A1%D1%85%D0%B5%D0%BC%D0%B0%20%D0%BA%D0%BE%D0%BC%D0%BF%D0%BE%D0%BD%D0%B5%D0%BD%D1%82%D0%BE%D0%B2.svg)

## UML-схема классов для данных и логики
![Лаба-UML](https://github.com/izen57/PPO/blob/845c5091a7e6f864d22b4bc999faf51ef9c4d208/%D0%9B%D0%B0%D0%B1%D0%B0-UML%20%D0%B4%D0%B0%D0%BD%D0%BD%D1%8B%D1%85%20%D0%B8%20%D0%BB%D0%BE%D0%B3%D0%B8%D0%BA%D0%B8.svg)

## UML-схема "модельных" классов
![Лаба-UML модельных](https://github.com/izen57/PPO/blob/005f61d72e777f0f89d859bb1026fd7e9c967d80/%D0%9B%D0%B0%D0%B1%D0%B0-UML%20%D0%BC%D0%BE%D0%B4%D0%B5%D0%BB%D1%8C%D0%BD%D1%8B%D1%85.svg)
