# Two-factor authentication

In order to execute API calls successfully, you will need to generate two-factor codes programatically since they expire every 30 seconds. This protects against accidentally leaked API keys and some man-in-the-middle attacks on your account.

You do not need to worry about two-factor codes, they are automatically generated using Secret Code.

![alt text](https://img.icons8.com/color/48/000000/error.png "Warning icon") |
-------------- |
In the event that an incorrect two-factor code is generated, you need to synchronize the clock of your device. |

Device clock synchronization:

```text
# Ubuntu Example:

$ sudo apt-get install ntp -y
```

To check whether the two-factor code is generated correctly, execute the following code, and compare the obtained result with the code issued by your Authenticator App:

```csharp
Console.WriteLine(BitSkinsApi.Account.AccountData.GetTwoFactorCode());
```

[<Account setup](https://github.com/dmitrydnl/BitSkinsApi/blob/master/docs/eng/account/account_setup.md) &nbsp;&nbsp;&nbsp;&nbsp; [Account balance>](https://github.com/dmitrydnl/BitSkinsApi/blob/master/docs/eng/balance/account_balance.md)