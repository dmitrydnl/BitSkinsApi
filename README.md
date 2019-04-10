[![nuget](https://img.shields.io/nuget/v/BitSkinsApi.svg)](https://www.nuget.org/packages/BitSkinsApi/)
\
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
```Install-Package BitSkinsApi -Version 1.0.0```

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
```BitSkinsApi.Balance.CurrentBalance.GetAccountBalance();```

## Features
* Made on .NET Standard
* Easy to use
* Full coverage of the BitSkins General API
* Automatic two-factor authentication

## Tests
For the tests to work it is necessary to enter your Api Key and Secret Code in the [Initilize.cs](https://github.com/Captious99/BitSkinsApi/blob/master/src/BitSkinsApiTests/Tests/Initilize.cs) file.
\
\
Requirements for Steam inventory and BitSkins inventory for tests:
1. In the Steam inventory must be at least one item available for sale. This item must be from a game that supported by BitSkins. This is necessary for the SellItemTest test. If successful, you will receive a Steam trade offer, which you can decline.
2. In BitSkins must be at least one item currently on sale by you. This is necessary for the RelistAndDelistItemTest and ModifySaleTest test.
3. In the BitSkins inventory must be at least one pending withdrawal item. This is necessary for the WithdrawItemTest test. If successful, you will receive a Steam trade offer, which you can decline.
4. On BitSkins account balance must be at least 0.01 USD. This is necessary for the BuyItemTest test.

## Project requirements for use the BitSkinsApi
BitSkinsApi build on .NET Standard 2.0. To use the BitSkinsApi, version of the platform you are using must implement .NET Standart 2.0.
\
Learn more about [.NET implementation support](https://docs.microsoft.com/en-us/dotnet/standard/net-standard#net-implementation-support).

## License
This project is licensed under the MIT License - see the [LICENSE.md](https://github.com/Captious99/BitSkinsApi/blob/master/LICENSE.md) file for details.
