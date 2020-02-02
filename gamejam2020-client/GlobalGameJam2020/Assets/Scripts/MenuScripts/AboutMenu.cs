using UnityEngine;

namespace MenuScripts
{
    public class AboutMenu : MonoBehaviour
    {
        public void WebsiteButton()
        {
            System.Diagnostics.Process.Start("http://www.architectnow.net");
        }
    }
}
