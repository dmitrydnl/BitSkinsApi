## Parameters for account initialization

Before you start working with the BitSkins API, you need to initialize your BitSkins account data:

* ApiKey
* Secret Code
* API requests limits per second

API Key you can retrieve through the BitSkins [settings](https://bitskins.com/settings) page after you enable API access for your account.

The secret code can be found when you enable [two-factor authentication](https://bitskins.com/settings) for your BitSkins account. If you do not have this code, simply disable two-factor authentication and re-enable it. [Learn more about two-factor authentication in the BitSkins API.](https://github.com/Captious99/BitSkinsApi/blob/master/docs/eng/account/two_factor_authentication.md)

Default API limits are 8 requests per second. [Contact](https://bitskins.com/contact) with BitSkins if you need higher limits.

## Account initialization

To initialize your account and start working with the BitSkins API, you just need to run one line of code.

If your API requests limit per second is 8

```csharp
BitSkinsApi.Account.AccountData.SetupAccount(ApiKey, SecreCode);
```

If you have a different API request limit per second

```csharp
BitSkinsApi.Account.AccountData.SetupAccount(ApiKey, SecretCode, Лимит API);
```

***
![alt text](https://img.icons8.com/color/48/000000/error.png "Warning icon")\
If you try to call any API function before initializing the account, you will get a _SetupAccountException_.
***

Now everything is set up, and you can start working with the BitSkins API.