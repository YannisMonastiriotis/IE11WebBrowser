using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.LinkLabel;

namespace IE11WebBrowser
{
    [ComVisible(true)]
    public partial class Form1 : Form
    {
        private string htmlContent = "";
        public Form1()
        {
            InitializeComponent();
            webBrowser1.DocumentCompleted += webBrowser_DocumentCompleted;
            webBrowser1.ObjectForScripting = this;
            // Enable script errors and developer tools
            webBrowser1.ScriptErrorsSuppressed = false;
            webBrowser1.IsWebBrowserContextMenuEnabled = true;
            webBrowser1.WebBrowserShortcutsEnabled = true;
            webBrowser1.AllowWebBrowserDrop = true;
            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            webBrowser1.GoBack();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            webBrowser1.Refresh();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            webBrowser1.GoForward();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string url = addressTextBox.Text;
            if (!string.IsNullOrEmpty(url))
            {
                if (!url.StartsWith("http://") && !url.StartsWith("https://"))
                {
                    url = "http://" + url; // Ensure the URL has a valid scheme
                }
                webBrowser1.Navigate(url);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private bool isPageLoaded = false;
        private void webBrowser_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            addressTextBox.Text = webBrowser1.Url.ToString();

        }
        private void webBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            // Check if the Document is not null and the Body element exists
            if (webBrowser1.Document != null && webBrowser1.Document.Body != null)
            {
                if (!isPageLoaded)
                {
                    isPageLoaded = true;
                  
                        // Inject JavaScript code to open developer tools
                        webBrowser1.Document.InvokeScript("eval", new object[] {
            @"var devTools = window.open('', '_blank');
            devTools.document.write('<html><head><title>Developer Tools</title></head><body><div id=""root""></div></body></html>');
            devTools.document.close();
            var script = devTools.document.createElement('script');
            script.src = 'https://cdn.jsdelivr.net/npm/react-devtools@4.12.2/dist/react-devtools.js';
            devTools.document.body.appendChild(script);"
                        });
                   
                    // Access the Body element's style property and change the background color
                    // webBrowser1.Document.Body.Style = "background-color: lightblue;";


                    //                    // Inject JavaScript code to wait for all resources to load
                    //                    webBrowser1.Document.InvokeScript("execScript", new Object[] {
                    //    "function waitForPage() { " +
                    //    "  if (document.readyState != 'complete') { " +
                    //    "    setTimeout(waitForPage, 100); " +
                    //    "  } else { " +
                    //    "    window.external.ExecuteAction(); " + // Call TriggerButton5Click
                    //    "  } " +
                    //    "} " +
                    //    "waitForPage();", "JavaScript"
                    //});


                    // Store the HTML content in the htmlContent variable

                }
            }
        }
        public void ExecuteAction()
        {
            // Implement the desired action here
            // For example, call your button5_Click method
            button5_Click(this, EventArgs.Empty);
        }
        private void button5_Click(object sender, EventArgs e)
        {
            // Create an instance of the HtmlContentPopupForm
            HtmlContentPopUpForm popupForm = new HtmlContentPopUpForm();
            htmlContent = webBrowser1.DocumentText;
            // Set the HTML content in the popup window's TextBox (assuming you have a TextBox named htmlContentTextBox)
            popupForm.richTextBox1.Text = htmlContent;

            // Show the popup window as a dialog (blocking until it's closed)
            popupForm.ShowDialog();
        }
       

        }
}

