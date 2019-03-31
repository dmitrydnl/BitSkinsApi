[![Codacy Badge](https://api.codacy.com/project/badge/Grade/c311ec19d2f14879924882810795f4a7)](https://www.codacy.com?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=Captious99/BitSkinsApi&amp;utm_campaign=Badge_Grade)

# What is BitSkinsApi?
An extended wrapper for the BitSkins API. It is a NuGet Package, that build on .NET Standard 2.0. BitSkinsApi allows you to interact with your BitSkins account through methods call. You can sell/buy items, get all BitSkins market data, get your Steam inventory and more. All games available on bitSkins are supported.
\
\
Learn more about BitSkins API you can an official [BitSkins website](https://bitskins.com/api).
\
Learn more about [NuGet](https://www.nuget.org).

# How do I install BitSkinsApi?
To install the NuGet package, you can the Package Manager Console. For more information, see [Package consumption overview and workflow](https://docs.microsoft.com/en-us/nuget/consume-packages/overview-and-workflow).
1. In Visual Studio select the Tools > NuGet Package Manager > Package Manager Console menu command.
2. Once the console opens, check that the Default project drop-down list shows the project into which you want to install the package.
3. Enter the command:
\
```$ npm install bitskins```.

# How do I use BitSkinsApi?
All about using BitSkinsApi you can find in [documentation](https://github.com/Captious99/BitSkinsApi/blob/master/docs/index.md).
\
\
In short:
1. Register on [BitSkins website](https://bitskins.com).
2. Enable API access and two-factor authentication for your BitSkins account.
3. Initialize your BitSkins account in code:
\
```BitSkinsApi.Account.AccountData.SetupAccount(ApiKey, SecreCode);```
\
API Key you can retrieve through the BitSkins settings page. The secret code can be found when you enable two-factor authentication for your BitSkins account.
4. Now you can use BitSkinsApi. For example to retrieve your balance BitSkins you need execute function:
\
```BitSkinsApi.Balance.AccountBalance.GetAccountBalance();```

## Features
* Made on .NET Standard.
* Full coverage of the BitSkins General API.
* Automatic two-factor authentication

## Tests

## License
This project is licensed under the MIT License - see the [LICENSE.md](https://github.com/Captious99/BitSkinsApi/blob/master/LICENSE.md) file for details.