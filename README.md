# vkWinPlayer for .NET
Контакты: botx68@gmail.com

***vkWinPlayer*** - это музыкальный проигрыватель, который позволяет слушать музыку с популярной российской социальной сети VK.COM.



![Screenshot](https://pp.vk.me/c637320/v637320554/4739/DXbloaSMAYc.jpg)
![Screenshot](https://pp.vk.me/c637320/v637320554/4731/-TuxPrlYrvM.jpg)


***vkWinPlayer*** написан на C# 4.6.1. 

Официальный сайт: [retro3f.github.io](http://retro3f.github.io/)
# Как использовать
1. [Создайте приложение](https://vk.com/editapp?act=create)
2.  После удачной проверки вы перейдете на страницу редактирования приложения и далее идете во вкладку ***"Настройки"*** там скопируете два параметра (`ID приложения` и `Защищенный ключ`)
3.  Откройте файл ***vkWinPlayerSettings.ini*** и в параметры ``appId=`` и ``appSecretKey=`` вставьте данные которые мы копировали. Параметр ``scope=`` не трогать!!! Сохраняем и запускаем приложение.

# План развития проекта

- [ ]  Добавить синхронизацию с [Last.fm API](http://www.last.fm/ru/api)
- [ ]  Исправить некоторые недочеты
- [ ]  Скачивание аудизаписей
- [ ]  Поиск музыки, добавление музыки в свой плей-лист и просмотр музыки у друзей.


## Содействие
Изменения и улучшения приветствуются. Не стесняйтесь делать форки и пул реквесты :)

#Credits

Большое спасибо следующим организациям и проектам, работа которых имеет важное значение для развития проекта:
- [Json.Net](http://www.newtonsoft.com/json)
- [MetroFramework](https://github.com/dennismagno/metroframework-modern-ui)
