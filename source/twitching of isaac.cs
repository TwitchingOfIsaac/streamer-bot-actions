using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

public struct Constants
{
    public const string GlobalVarNameAuthToken = "TOIAuthToken";
}

public class CPHInline
{
    protected string userID;
    protected string apiKey;
    protected TwitchingOfIsaac toi;
    public bool Execute()
    {
        var command = args["command"].ToString();
        var parameters = "";
        if (args.ContainsKey("parameters"))
        {
            parameters = args["parameters"].ToString();
        }

        toi = new TwitchingOfIsaac(CPH);
        toi.Execute(command, parameters);
        
        return true;
    }

    public void Init()
    {
        toi = new TwitchingOfIsaac(CPH);
    }

    public class TwitchingOfIsaac
    {
        private IInlineInvokeProxy CPH;
        private string userID;
        private string apiKey;
        private static readonly HttpClient client = new HttpClient();
        public TwitchingOfIsaac(IInlineInvokeProxy cph)
        {
            CPH = cph;
            try
            {
                var authToken = CPH.GetGlobalVar<string>(Constants.GlobalVarNameAuthToken, true);
                var parts = authToken.Split(':');
                userID = parts[0];
                apiKey = parts[1];
            }
            catch (Exception ex)
            {
                this.ShowDialog();
            }
        }

        public string ShowDialog()
        {
            Form prompt = new Form()
            {
                Width = 500,
                Height = 210,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = "Enter your Twitching of Isaac auth token:",
                StartPosition = FormStartPosition.CenterScreen,
                TopMost = true
            };
            LinkLabel inputLabel = new LinkLabel()
            {
                Left = 50,
                Top = 20,
                Width = 400,
                Height = 100,
                Text = "Please enter your Twitching of Isaac Auth Token. If you need to retrieve your auth token, you can retrieve it at https://twitchingofisaac.com/. Once saved, you will not see this message again.\n\nAuth Token:",
                LinkArea = new LinkArea(113, 29)
            };
            Label status = new Label()
            {
                Left = 50,
                Top = 130,
                Width = 225,
                Text = "Error: Could not validate token!",
                ForeColor = System.Drawing.Color.Red,
                Visible = false
            };
            TextBox textBox = new TextBox()
            {
                Left = 50,
                Top = 100,
                Width = 400,
            };
            textBox.TextChanged += (sender, e) =>
            {
                status.Visible = false;
            };
            Button validate = new Button()
            {
                Text = "Validate Token",
                Left = 345,
                Width = 100,
                Top = 130
            };
            validate.Click += (sender, e) =>
            {
                if (this.Validate(textBox.Text))
                {
                    CPH.SetGlobalVar(Constants.GlobalVarNameAuthToken, textBox.Text, true);
                    prompt.Close();
                }
                else
                {
                    status.Visible = true;
                }
            };
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(validate);
            prompt.Controls.Add(inputLabel);
            prompt.Controls.Add(status);
            prompt.AcceptButton = validate;
            return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : "";
        }

        public bool Validate(string authToken)
        {
            try
            {
                var parts = authToken.Split(':');
                userID = parts[0];
                apiKey = parts[1];
            }
            catch (Exception ex)
            {
                return false;
            }

            var builder = new UriBuilder(new Uri($"https://twitchingofisaac.com/api/v1/{userID}/validate"));
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, builder.Uri);
            request.Headers.Add("API-Key", apiKey);
            var task = Task.Run(() => client.SendAsync(request));
            task.Wait();
            HttpResponseMessage response = task.Result;
            if ((int)response.StatusCode == 200)
            {
                return true;
            }

            return false;
        }

        public bool Execute(string command, string parameters)
        {
            var payload = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("command", command),
                new KeyValuePair<string, string>("params", parameters),
            };
            var builder = new UriBuilder(new Uri($"https://twitchingofisaac.com/api/v1/{userID}/execute"));
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, builder.Uri)
            {
                Content = new FormUrlEncodedContent(payload)
            };
            request.Headers.Add("API-Key", apiKey);
                        
            var task = Task.Run(() => client.SendAsync(request));
            task.Wait();
            HttpResponseMessage response = task.Result;
            if ((int)response.StatusCode == 200)
            {
                return true;
            }

            return false;
        }
    }
}
