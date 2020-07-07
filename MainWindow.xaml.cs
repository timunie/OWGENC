using MahApps.Metro.Controls;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Schema;

namespace OWGENC
{
    // <summary>
    /// OWGENC's Goal is to better learn WPF, Net Core, and handling json data with C#.
    /// OWGENC Will generate teams for video game scrim matches based on a json database of managed players for your community,
    /// accounting for "benched" rounds when players have not played, captain status, team tracking... etc
    /// </summary>
    public partial class MainWindow : MetroWindow
    {

        public List<jPersonSimple> RosterDB = new List<jPersonSimple>();
        public MainWindow()
        {
            InitializeComponent();
            // ! Dirty insurance lol
            RosterDB.Clear();
            plist.ItemsSource = RosterDB;
        }



        #region UI Events
        //  TODO: fix event names to personal style after finished with json video
        private void cmdDeserialize_Click(object sender, RoutedEventArgs e)
        {
            deserializeJSON(txtRawInput.Text);
        }


        private void cmdClear_Click(object sender, RoutedEventArgs e)
        {
            txtOutput.Text = string.Empty;
        }


        private void cmdSaveJsonToFile_Click(object sender, RoutedEventArgs e)
        {
            foreach (var player in RosterDB)
            {
                debugOutput(player.name);
            }
            // TODO : Actually save to a file. crud time bby

        }

        #endregion

        #region Debug Output
        private void debugOutput(string strDebugText)
        {
            try
            {
                //System.Diagnostics.Debug.Write(strDebugText + Environment.NewLine);                
                txtOutput.Text = txtOutput.Text + strDebugText + Environment.NewLine;
                txtOutput.SelectionStart = txtOutput.Text.Length;
                txtOutput.ScrollToEnd();

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Write(ex.Message.ToString() + Environment.NewLine);

            }
        }
        #endregion
        #region JSON Functions
        private void deserializeJSON(string strJSON)
        {
            try
            {
                // ? not specifying a specific class, resolved a runtime -
                // ?     var jPerson = JsonConvert.DeserializeObject<dynamic>(strJSON);



                var jPerson = JsonConvert.DeserializeObject<jPersonSimple>(strJSON);
                AddToDB(jPerson);

            }
            catch (Exception ex)
            {

                debugOutput("We Had a problem... " + ex.Message.ToString());
            }
        }

        private void AddToDB(jPersonSimple player)
        {

            jPersonSimple p = new jPersonSimple
            {
                name = player.name,
                id = RosterDB.Count<jPersonSimple>() + 1,
                address = player.address,
                platforms = player.platforms,
            };
            // TODO: Fix platforms?
            RosterDB.Add(p);
            plist.Items.Refresh();



            debugOutput("Name: " + p.name);
            debugOutput("of Team: " + p.address.team.ToString() + " hailing from " + p.address.community);
            debugOutput("Attempting to print player's social platforms..");
            foreach (var platform in p.platforms)
            {
                debugOutput(platform.platformName.ToString() + " username: " + platform.handle);
            }

        }
        #endregion
        // addrr works

    }
}
