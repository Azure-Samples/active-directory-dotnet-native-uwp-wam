NativeClient-WindowsUniversalPlatform-WebAccountManager
=========================

This sample demonstrates a Universal Windows Platform (UWP) app calling the directory Graph API to look up a user. The UWP app uses the Windows 10 WebAccountManager API to obtain an access token for the Graph as the currently signed in user, or any valid Azure AD account entered by the user.

For more information about how the protocols work in this scenario and other scenarios, see [Authentication Scenarios for Azure AD](http://go.microsoft.com/fwlink/?LinkId=394414).

## How To Run This Sample

To run this sample you will need:
- Visual Studio 2015 RTM
- Windows 10 Tools for Visual Studio
- Windows 10 (development mode enabled)
- An Internet connection
- An Azure subscription (a free trial is sufficient)
- A Microsoft account

Every Azure subscription has an associated Azure Active Directory tenant.  If you don't already have an Azure subscription, you can get a free subscription by signing up at [http://wwww.windowsazure.com](http://www.windowsazure.com).  All of the Azure AD features used by this sample are available free of charge.

### Step 1:  Clone or download this repository

From your shell or command line:

`git clone https://github.com/AzureADSamples/NativeClient-UWP-WAM.git`

### Step 2:  Create a user account in your Azure Active Directory tenant

If you already have a user account in your Azure Active Directory tenant, you can skip to the next step.  This sample will not work with a Microsoft account, so if you signed in to the Azure portal with a Microsoft account and have never created a user account in your directory before, you need to do that now.  If you create an account and want to use it to sign-in to the Azure portal, don't forget to add the user account as a co-administrator of your Azure subscription.

### Step 3:  [OPTIONAL] Register the sample with your Azure Active Directory tenant and update the code accordingly

The sample app can be ran as is with any Azure AD tenant. If you just want to see how the code behaves, you can simply launch the app and play with it.
If you want to restrict the use of the app to your tenant only, or if you want to learn how to register new UWP apps in Azure AD, you can follow the instructions in this step.   

#### Find the system assigned app's redirect URI

Before you can register the application in the Azure portal, you need to find out the application's redirect URI.  Windows 10 provides each application with a unique URI and ensures that messages sent to that URI are only sent to that application.  To determine the redirect URI for your project:

1. Open the solution in Visual Studio 2015.
2. Open the `MainPage.xaml.cs` file.
3. Find this line of code and set a breakpoint on it.

```C#
string URI = string.Format("ms-appx-web://Microsoft.AAD.BrokerPlugIn/{0}", WebAuthenticationBroker.GetCurrentApplicationCallbackUri().Host.ToUpper());
```

4. Hit F5.
5. When the breakpoint is hit, use the debugger to determine the value of redirectURI, and copy it aside for the next step.
6. Stop debugging, and clear the breakpoint.

The redirectURI value will look something like this:

```
ms-appx-web://Microsoft.AAD.BrokerPlugIn/S-1-15-2-694665007-945573255-503870805-3898041910-4166806349-50292026-2305040851
```

#### Register the app

1. Sign in to the [Azure management portal](https://manage.windowsazure.com).
2. Click on Active Directory in the left hand nav.
3. Click the directory tenant where you wish to register the sample application.
4. Click the Applications tab.
5. In the drawer, click Add.
6. Click "Add an application my organization is developing".
7. Enter a friendly name for the application, for example "NativeClient-UWP-WAM", select "Native Client Application", and click next.
8. Enter the Redirect URI value that you obtained during the previous step.  Click finish.
9. Click the Configure tab of the application.
10. Find the Client ID value and copy it aside, you will need this later when configuring your application.
11. In "Permissions to Other Applications", "Windows Azure Active Directory" row, open the "Delegated Permissions" dropdown. 
12. Check the "Access your organization's directory" checkbox.
13. Save the configuration.


#### Update the app code to reflect the new registration coordinates

1. Open `MainPage.xaml.cs'.
2. Comment out or delete the existing declaration for `authority`
3. Find the commented declaration of `tenant`, un-comment it and replace the value with the name of your Azure AD tenant.
3. Right below that line, there is a commented declaration of `authority`. Un-comment it     
3. Find the declaration of `clientId` and replace the value with the Client ID from the Azure portal.


### Step 4:  Run the sample

Clean the solution, rebuild the solution, and run it.

The application flow is very simple. As the app starts, you will either be automatically logged in (if you are signed in on your box with a valid Azure AD user) or you will be prompted to sign in. As soon as you do so, you will be able to type the alias of any user from the directory of your signed in account and get back some simple user attributes. If you want to query a different directory, simply click on the hyperlink button on top of the screen, enter the credentials for the new account, and repeat the process.
The app will remember the account you used the last time you run it, and attempt to sign in as that account at startup time.
You can expect the exact same behavior when running the app on a mobile device or emulator.

## About The Code

Coming soon.
