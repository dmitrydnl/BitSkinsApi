# Двухфакторная аутентификация

Чтобы успешно выполнять API запросы, необходимо генерировать двухфакторные коды программно, поскольку срок их действия истекает каждые 30 секунд. Это защищает от случайной утечки API-ключей и некоторых атак типа «man-in-the-middle» на ваш аккаунт.

Вам не нужно беспокоиться о двухфакторных кодах, они автоматически генерируются с помощью Secret Code.

![alt text](https://img.icons8.com/color/48/000000/error.png "Warning icon") |
-------------- |
В случае если генерируется неверный двухфакторный код вам нужно синхронизировать часы вашего устройства. |

Синхронизация часов устройства:

```text
# Ubuntu Example:

$ sudo apt-get install ntp -y
```

Чтобы проверить верно ли генерируется двухфакторный код выполните следующий код, и сравните полученный результат с кодом, который выдаёт ваше Authenticator App:

```csharp
System.Console.WriteLine(BitSkinsApi.Account.AccountData.GetTwoFactorCode());
```

[<Настройка аккаунта](https://github.com/dmitrydnl/BitSkinsApi/blob/master/docs/ru/account/account_setup.md) &nbsp;&nbsp;&nbsp;&nbsp; [Баланс аккаунта>](https://github.com/dmitrydnl/BitSkinsApi/blob/master/docs/ru/balance/account_balance.md)