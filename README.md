# Развертка сервиса

## Установка Docker

### Для MacOS

Colima является бесплатной альтернативой Docker Desktop для MacOS и Linux и представляет из себя инструмент для управления контейнерами Docker.

1. Необходимо удалить Docker Desktop, если он установлен.

2. Для непосредственной установки Colima будем использовать Homebrew командой консоли (далее мы будем всё время использовать консоль для управления):
> `/bin/bash -c "$(curl -fsSL https://raw.githubusercontent.com/Homebrew/install/HEAD/install.sh)"`

3. Таким же образом установите сам Docker и Colima:
>`brew install docker`

>`brew install docker-compose`

>`brew install --HEAD colima`

4. После установки запустите виртуальную машину:

*Для процессоров Apple Silicon (M1/M2/ARM), Lima >=0.14, MacOS >=13.0:*
>`colima start --arch aarch64 --vm-type=vz --vz-rosetta`

*Для процессоров Intel:*
>`colima start --vm-type=vz` (последний аргумент для Lima>=0.14, MacOS >= 13.0)

5. Убедитесь, что Colima запущена:
>`colima status`

> INFO[0000] colima is running using macOS Virtualization.Framework
<...>

6. Откройте командную строку и выполните команду:
> `docker -v`

Если отобразится версия Docker, это означает, что установка Docker прошла успешно.

7. Теперь можно использовать Docker.

### Для Astra Linux

Установка должна выполняться от имени пользователя, являющегося администратором системы (при включенном МКЦ - пользователя с высоким уровнем целостности). 

1. Для установки Docker требуется выполнить следующие команды:
> `sudo apt update`  
> `sudo apt install docker.io`

2. После установки Docker рекомендуется предоставить администратору право работать с контейнерами не используя sudo:
> `sudo usermod -aG docker $USER`

3. Откройте командную строку и выполните команду:
> `docker -v`

Если отобразится версия Docker, это означает, что установка Docker прошла успешно.

4. Теперь можно использовать Docker.

### Для Windows

Рассмотрим установку Docker Desktop for Windows — это Community-версия Docker для систем Microsoft Windows.

1. Включите функции Hyper-V Containers Window. Для этого перейдите в панель управления - установка и удаление программ - включение или отключение компонентов Windows. Активируйте пункт Hyper-V, который включает Hyper-V Managment Tools, Hyper-V Platform.

Также это можно выполнить через powershell или dism (все команды необходимо выполнять с правами администратора).

Powershell:

> `Enable-WindowsOptionalFeature -Online -FeatureName Microsoft-Hyper-V -All`

DISM:

> `DISM /Online /Enable-Feature /All /FeatureName:Microsoft-Hyper-V`

2. Скачайте установщик Docker (Docker Desktop Installer) с Docker Hub:
https://docs.docker.com/docker-for-windows/install

3. Запустите установщик Docker Desktop Installer.exe и ожидайте, пока он скачает все необходимые компоненты.

4. После установки система потребует перезагрузки. Перезагрузитесь и войдите в систему.

5. После входа может возникнуть запрос на установку дополнительного компонента WSL2. Перейдите по ссылке и скачайте необходимый пакет с официального сайта Microsoft.

6. После скачивания выполните установку WSL2, после которой снова потребуется перезагрузка.

7. Откройте командную строку и выполните команду:
> `docker -v`

Если отобразится версия docker, это означает, что установка docker в Windows прошла успешно.

8. Теперь можно использовать Docker.

## Установка сервиса

1. Скачайте архив проекта из репозитория GitHub. Для этого в репозитории необходимо нажать зелёную кнопку `<> Code` и в появившемся меню нажать `Download ZIP`.
(https://github.com/Krebkel/A.NCJsonService)
2. Разархивируйте скачанный архив проекта.

3. В терминале выполните следующую команду для создания хранилища контейнеров:
> `docker volume create pgdata`

4. Откройте в терминале директорию с проектом, в которой лежит файл `Dockerfile`. Все следующие команды необходимо выполнять из этой директории - корня проекта.

5. В терминале необходимо сбилдить проект командой:
> `docker build -t document .`

Обратите внимание, что в конце команды есть точка. 
В терминале должна через некоторое время появиться строка:
>Successfully tagged document:latest

5. После успешного билда запустите сервис вместе с базой данных командой:
> `docker-compose -f docker-compose.yml up`

6. В терминале выполните команду:
> `docker ps`

Должны появиться строки в виде таблицы, в которой в столбце IMAGE есть следующие записи:
>document:latest

>postgres:13

7. Сервис готов к работе.

## Запуск и использование сервиса

1. Скомпилируйте проект.

2. Откройте страницу в браузере `localhost:80`.

3. Для добавления новой записи пароля необходимо использовать соответствующую кнопку. При этом появляется модальное окно с полями `Название` и `Пароль`, а также переключателя `Сайт` или `Email`. 

3.1. На уровне фронтенда реализована валидация пароля и почты - в случае, если пароль содержит менее 8 символов, либо же если при установленном флажке `Email` в поле `Название` будет введена строка, не соответствующая `*@*.*`, либо же если не все поля будут заполнены, будет выведена ошибка с соответствующим пояснением.

4. Для поиска по названию необходимо использовать соответствующее поле поиска. При этом происходит автоматическая фильтрация содержимого таблицы.

5. Для остальных функций - удаления и редактирования записей, получения записи по ID - реализованы API, доступ к которым можно получить через Swagger на странице `localhost/swagger/index.html`.