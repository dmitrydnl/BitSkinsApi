# Настройка аккаунта

Для работы с BitSkinsApi вам нужно создать аккаунт на [сайте BitSkins](https://bitskins.com).

## Параметры для инициализации аккаунта

Перед началом работы с BitSkinsApi, вам нужно инициализировать ваш аккаунт BitSkins:

* ApiKey
* Secret Code
* Лимит API запросов в секунду

ApiKey можно получить на странице [настроек](https://bitskins.com/settings) BitSkins аккаунта, после активации доступа к API для вашей учетной записи.

Secret Code можно узнать при включении [двухфакторной аутентификации](https://bitskins.com/settings) для вашей учетной записи BitSkins. Если у вас нет этого кода, то просто отключите двухфакторную аутентификацию и включите её заново. [Более подробно про двухфакторную аутентификацию в BitSkins API](https://github.com/Captious99/BitSkinsApi/blob/master/docs/ru/account/two_factor_authentication.md).

Лимит API запросов в секунду по умолчанию равен 8. [Свяжитесь](https://bitskins.com/contact) с BitSkins если вам нужны более высокие лимиты.

## Инициализация аккаунта

Для того чтобы инициализировать ваш аккаунт и начать работать с BitSkinsApi, вам лишь нужно выполнить одну строчку кода. 

Если ваш лимит API запросов в секунду равен 8:

```csharp
BitSkinsApi.Account.AccountData.SetupAccount(ApiKey, SecretCode);
```

Если у вас другой лимит API запросов в секунду:

```csharp
BitSkinsApi.Account.AccountData.SetupAccount(ApiKey, SecretCode, Лимит API);
```

![alt text](https://img.icons8.com/color/48/000000/error.png "Warning icon") |
-------------- |
Если вы попытаетесь вызвать какую-либо функцию API, до того как аккаунт будет инициализирован, то вы получите ошибку _SetupAccountException_. |

Теперь всё настроено и можно приступить к работе с BitSkins API.

[Двухфакторная аутентификация>](https://github.com/Captious99/BitSkinsApi/blob/master/docs/ru/account/two_factor_authentication.md)