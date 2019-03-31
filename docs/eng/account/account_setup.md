# Account setup

For work with the BitSkins API, you need to create an account on the [BitSkins website](https://bitskins.com).

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

```text
BitSkinsApi.Account.AccountData.SetupAccount(ApiKey, Secret Code);
```

If you have a different API request limit per second

```text
BitSkinsApi.Account.AccountData.SetupAccount(ApiKey, Secret Code, Лимит API);
```

{% hint style="warning" %}
If you try to call any API function before initializing the account, you will get a _SetupAccountException_.
{% endhint %}

Now everything is set up, and you can start working with the BitSkins API.
