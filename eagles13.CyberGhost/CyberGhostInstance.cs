using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace eagles13.CyberGhost {
    public class CyberGhostInstance {
        private readonly CookieContainer cookieJar = new CookieContainer();

        public bool login(string username, string password) {
            var initialRequest = CyberghostRequest("https://account.cyberghostvpn.com/en_us/login");
            var initialResponse = (HttpWebResponse)initialRequest.GetResponse();

            var csrfToken = new StreamReader(initialResponse.GetResponseStream()).ReadToEnd();
            csrfToken = csrfToken.Substring(csrfToken.IndexOf("var CSRF", StringComparison.Ordinal) + 12, 64);


            var loginRequest = CyberghostRequest("https://account.cyberghostvpn.com/en_us/proxy/users/me?flags=17&csrf=" + csrfToken);
            var postData = "username="+ username + "&authentication=" + password + "&captcha=";
            var data = Encoding.ASCII.GetBytes(postData);

            loginRequest.Method = "POST";
            loginRequest.ContentType = "application/x-www-form-urlencoded";
            loginRequest.ContentLength = data.Length;

            using (var stream = loginRequest.GetRequestStream()) {
                stream.Write(data, 0, data.Length);
            }
            try {
                var loginResponse = (HttpWebResponse)loginRequest.GetResponse();
                var responded = new StreamReader(loginResponse.GetResponseStream()).ReadToEnd();
                return responded.Contains("user_name");
            } catch (Exception) {
                return false;
            }
            
        }


        public bool createNewCredentials() {
            var loginRequest = CyberghostRequest("https://account.cyberghostvpn.com/en_us/proxy/users/me/access_tokens");
            var postData = "";
            var data = Encoding.ASCII.GetBytes(postData);

            loginRequest.Method = "POST";
            loginRequest.ContentType = "application/x-www-form-urlencoded";
            loginRequest.ContentLength = data.Length;

            using (var stream = loginRequest.GetRequestStream()) {
                stream.Write(data, 0, data.Length);
            }
            try {
                var loginResponse = (HttpWebResponse)loginRequest.GetResponse();
                var responded = new StreamReader(loginResponse.GetResponseStream()).ReadToEnd();
                return responded != "";
            } catch (Exception) {
                return false;
            }



        }

        public string getServers() {
            try {
                var serversRequest = CyberghostRequest("https://account.cyberghostvpn.com/en_us/proxy/servers/groupedbycountry?protocol=openvpn");
                var serversResponse = (HttpWebResponse)serversRequest.GetResponse();
                return new StreamReader(serversResponse.GetResponseStream()).ReadToEnd();
            } catch (Exception) {
                return "";
            }
        }


        public string getCredentials() {
            try {
                var detailsRequest = CyberghostRequest("https://account.cyberghostvpn.com/en_us/proxy/users/me/access_tokens?flags=17");
                var detailsResponse = (HttpWebResponse)detailsRequest.GetResponse();
                return new StreamReader(detailsResponse.GetResponseStream()).ReadToEnd();
            } catch (Exception) {
                return "";
            }
        }

        public string configureConnection(string url, string username, string password) {
            try{
                var zipPath = Application.StartupPath + @"\cyberghost-config.zip";
                var extractPath = Application.StartupPath + @"\cyberghost-config";
                if (File.Exists(zipPath)){
                    File.Delete(zipPath);
                }
                if (Directory.Exists(extractPath)){
                    DeleteDirectory(extractPath);
                    
                }

                HttpWebRequest webRequest = CyberghostRequest("https://api2.cyberghostvpn.com/cg/serverconfigurations/download/4?country=" + url.Substring(2, 2).ToUpper() + "&os=linux&url=" + url);
                webRequest.Method = "GET";
                webRequest.Timeout = 5000;
                using (HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse()) {
                    var buffer = new byte[1024];

                    FileStream fileStream = File.OpenWrite(zipPath);
                    using (Stream input = webResponse.GetResponseStream()) {


                        var size = input.Read(buffer, 0, buffer.Length);
                        while (size > 0) {
                            fileStream.Write(buffer, 0, size);

                            size = input.Read(buffer, 0, buffer.Length);
                        }
                    }

                    fileStream.Flush();
                    fileStream.Close();
                }
                if (File.Exists(zipPath)) {
                    var finfo = new FileInfo(zipPath);
                    if (finfo.Length > 0){
                        ZipFile.ExtractToDirectory(zipPath, extractPath);
                        File.WriteAllText(extractPath + @"\login.conf", username + Environment.NewLine + password);
                        var currentConfiguration = File.ReadAllText(extractPath + @"\openvpn.ovpn");
                        File.WriteAllText(extractPath + @"\openvpn.ovpn", currentConfiguration + Environment.NewLine + "auth-user-pass login.conf");
                        return extractPath + @"\openvpn.ovpn";
                    }
                }
            } catch (Exception){
                // ignored
            }
            return "";
        }

        private HttpWebRequest CyberghostRequest(string url) {
            var request = WebRequest.Create(url) as HttpWebRequest;
            request.CookieContainer = cookieJar;
            request.Referer = "https://account.cyberghostvpn.com/en_us";
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/41.0.2272.101 Safari/537.36";

            return request;
        }


        public static void DeleteDirectory(string target_dir) {
            var files = Directory.GetFiles(target_dir);
            var dirs = Directory.GetDirectories(target_dir);
            foreach (var file in files) {
                File.SetAttributes(file, FileAttributes.Normal);
                File.Delete(file);
            }
            foreach (var dir in dirs) {
                DeleteDirectory(dir);
            }
            Directory.Delete(target_dir, false);
        }

    }
}
