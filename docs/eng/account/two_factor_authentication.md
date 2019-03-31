# Two-factor authentication

In order to execute API calls successfully, you will need to generate two-factor codes programatically since they expire every 30 seconds. This protects against accidentally leaked API keys and some man-in-the-middle attacks on your account.

You do not need to worry about two-factor codes, they are automatically generated using Secret Code.

{% hint style="warning" %}
In the event that an incorrect two-factor code is generated, you need to synchronize the clock of your device.
{% endhint %}

Device clock synchronization

```text
# Ubuntu Example:

$ sudo apt-get install ntp -y
```

To check whether the two-factor code is generated correctly, execute the following code, and compare the obtained result with the code issued by your Authenticator App

```text
BitSkinsApi.Account.AccountData.GetTwoFactorCode();
```

