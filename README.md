## Тестовое задание от компании Ёлва.

![задание](https://i.imgur.com/jJV8m4f.png)

#### Запрос на апи осуществляется путем GET запроса на страницу 
**`{url}/get/polygon?address=москва&dotPolygon=10&fileName=moscow&geoService=0`** <br/>
- address = город, полигоны которого будут выведены <br/>
- dotPolygon = частота точек, чтобы не возвращать миллиард точек, если указать 5, то будет возвращена каждая 5 точка и т.д <br/>
- fileName = название файла, которое будет загружено после выполнения <br/>
- geoService = выбор геосервиса с которым будет производиться работа. 
> 0 - OpenStreetMap

- #### [Контроллер](https://github.com/egorrrmiller/YolvaTestWork/blob/master/YolvaTestWork/Controllers/HomeController.cs)
- #### [Тут добавление геосервисов](https://github.com/egorrrmiller/YolvaTestWork/blob/master/YolvaTestWork/Enums/GeoServicesEnum.cs)
- #### [Тут можно посмотреть как выглядит реализация геосервисов](https://github.com/egorrrmiller/YolvaTestWork/tree/master/YolvaTestWork/GeoServices)
