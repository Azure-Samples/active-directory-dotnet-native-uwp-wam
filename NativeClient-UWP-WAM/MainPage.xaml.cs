using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Security.Authentication.Web;
using Windows.Security.Authentication.Web.Core;
using Windows.Security.Credentials;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace NativeClient_UWP_WAM
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        //
        // The Client ID is used by the application to uniquely identify itself to Azure AD.
        // The Tenant is the name of the Azure AD tenant in which this application is registered.
        // The AAD Instance is the instance of Azure, for example public Azure or Azure China.
        // The Authority is the sign-in URL of the tenant.

        const string clientId = "a9b55b7d-66af-4de9-9ee7-c7b04106bdef";

        // const string clientId = "1847e0b6-739e-4786-81f7-d73a9bc8ba44";
        // const string tenant = "common";
        // const string tenant = "vibro21hotmail.onmicrosoft.com";
        const string tenant = "developertenant.onmicrosoft.com";
        //  const string tenant = "microsoft.onmicrosoft.com";
        //   const string tenant = "cloudidentity.net";
        // const string authority = "https://login.microsoftonline.com/" + tenant;
        const string authority = "organizations";

        // To authenticate to the directory Graph, the client needs to know its App ID URI.
        const string resource = "https://graph.windows.net";

        // For this preview, Windows10 universal apps require redirect URI in the format below
        string URI = string.Format("ms-appx-web://Microsoft.AAD.BrokerPlugIn/{0}", WebAuthenticationBroker.GetCurrentApplicationCallbackUri().Host.ToUpper());

        public MainPage()
        {
            this.InitializeComponent();
            List<UserSearchResult> results = new List<UserSearchResult>();
            results.Add(new UserSearchResult());
            SearchResults.ItemsSource = results;
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string a = null;
        }


        //private async Task<string> GetTokenViaADAL()
        //{
        //    AuthenticationContext ac = new AuthenticationContext(authority);
        //    AuthenticationResult ar = await ac.AcquireTokenAsync(resource, clientId, new Uri(URI));
        //    return ar.AccessToken;
        //}

        private async void Search(object sender, RoutedEventArgs e)
        {

            string accessToken = string.Empty;

            WebAccountProvider wap =
               await WebAuthenticationCoreManager.FindAccountProviderAsync("https://login.microsoft.com", authority);

            //   WebTokenRequest wtr = new WebTokenRequest(wap, string.Empty, clientId, WebTokenRequestPromptType.ForceAuthentication);
            WebTokenRequest wtr = new WebTokenRequest(wap, string.Empty, clientId);

            wtr.Properties.Add("resource", resource);

            WebTokenRequestResult wtrr = await WebAuthenticationCoreManager.RequestTokenAsync(wtr);
            if (wtrr.ResponseStatus == WebTokenRequestStatus.Success)
            {
                accessToken = wtrr.ResponseData[0].Token;
                var account = wtrr.ResponseData[0].WebAccount;
                var properties = wtrr.ResponseData[0].Properties;
            }

            // TEST TEST
            // accessToken = await GetTokenViaADAL();

            SearchResults.ItemsSource = await DirectorySearcher.SearchByAlias(SearchTermText.Text, accessToken, tenant);
        }
    }
}
