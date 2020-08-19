using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace MenuWithSubMenu
{
    class SideMenuViewModel
    {
        //to call resource dictionary in our code behind
        ResourceDictionary dict = Application.LoadComponent(new Uri("/MenuWithSubMenu;component/Assets/IconDictionary.xaml", UriKind.RelativeOrAbsolute)) as ResourceDictionary;


        //Our Source List for Menu Items
        public List<MenuItemsData> MenuList
        {
            get
            {
                return new List<MenuItemsData>
                {
                    //MainMenu without SubMenu Button 
                    new MenuItemsData(){ PathData= (PathGeometry)dict["icon_dashboard"], MenuText="Dashboard", SubMenuList=null},
                 
                    //MainMenu Button
                    new MenuItemsData(){ PathData= (PathGeometry)dict["icon_users"], MenuText="Profile"
                    
                    //SubMenu Button
                    , SubMenuList=new List<SubMenuItemsData>{
                    new SubMenuItemsData(){ PathData=(PathGeometry)dict["icon_adduser"], SubMenuText="New User" },
                    new SubMenuItemsData(){ PathData=(PathGeometry)dict["icon_alluser"], SubMenuText="All Users" }}
                    },

                    //MainMenu Button
                    new MenuItemsData(){ PathData= (PathGeometry)dict["icon_mails"], MenuText="Mails"

                    //SubMenu Button
                    , SubMenuList=new List<SubMenuItemsData>{
                    new SubMenuItemsData(){ PathData=(PathGeometry)dict["icon_inbox"], SubMenuText="Inbox" },
                    new SubMenuItemsData(){ PathData=(PathGeometry)dict["icon_outbox"], SubMenuText="Outbox" },
                    new SubMenuItemsData(){ PathData=(PathGeometry)dict["icon_sentmail"], SubMenuText="Sent" }}},

                    //MainMenu without SubMenu Button
                    new MenuItemsData(){ PathData= (PathGeometry)dict["icon_settings"], MenuText="Settings", SubMenuList=null}
                };
            }
        }
    }

    public class MenuItemsData
    {
        //Icon Data
        public PathGeometry PathData { get; set; }
        public string MenuText { get; set; }
        public List<SubMenuItemsData> SubMenuList { get; set; }

        //To Add click event to our Buttons we will use ICommand here to switch pages when specific button is clicked
        public MenuItemsData()
        {
            Command = new CommandViewModel(Execute);
        }

        public ICommand Command { get; }

        private void Execute()
        {
            //our logic comes here
            string MT = MenuText.Replace(" ", string.Empty);
            if (!string.IsNullOrEmpty(MT))
                navigateToPage(MT);
        }

        private void navigateToPage(string Menu)
        {
            //We will search for our Main Window in open windows and then will access the frame inside it to set the navigation to desired page..
            //lets see how... ;)
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(MainWindow))
                {
                    (window as MainWindow).MainWindowFrame.Navigate(new Uri(string.Format("{0}{1}{2}", "Pages/", Menu, ".xaml"), UriKind.RelativeOrAbsolute));
                }
            }
        }
    }
    public class SubMenuItemsData
    {
        public PathGeometry PathData { get; set; }
        public string SubMenuText { get; set; }

        //To Add click event to our Buttons we will use ICommand here to switch pages when specific button is clicked
        public SubMenuItemsData()
        {
            SubMenuCommand = new CommandViewModel(Execute);
        }

        public ICommand SubMenuCommand { get; }

        private void Execute()
        {
            //our logic comes here
            string SMT = SubMenuText.Replace(" ", string.Empty);
            if (!string.IsNullOrEmpty(SMT))
                navigateToPage(SMT);
        }

        private void navigateToPage(string Menu)
        {
            //We will search for our Main Window in open windows and then will access the frame inside it to set the navigation to desired page..
            //lets see how... ;)
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(MainWindow))
                {
                    (window as MainWindow).MainWindowFrame.Navigate(new Uri(string.Format("{0}{1}{2}", "Pages/", Menu, ".xaml"), UriKind.RelativeOrAbsolute));
                }
            }
        }
    }
}