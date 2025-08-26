using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices.WindowsRuntime;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Windows.Foundation;
using Windows.Foundation.Collections;

namespace cricket_app
{
    public sealed partial class NrrPage : Page
    {
        public NrrPage()
        {
            this.InitializeComponent();
        }
        private int currentTeamIndex = 1;
        private void Generate_teams_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            run_rate_pannel.Children.Clear();
            // Runs Scored
            TextBox runs_scored = new TextBox
            {
                Name = $"runs_scored{currentTeamIndex}",
                Header = "Runs Scored",
                Width = 200,
                Margin = new Thickness(0, 10, 0, 10),
                TextAlignment = TextAlignment.Center
            };

            // Overs Faced
            TextBox overs_faced = new TextBox
            {
                Name = $"overs_faced{currentTeamIndex}",
                Header = "Overs Faced",
                Width = 200,
                Margin = new Thickness(0, 10, 0, 10),
                TextAlignment = TextAlignment.Center
            };

            // Runs Conceded
            TextBox runs_conceded = new TextBox
            {
                Name = $"runs_conceded{currentTeamIndex}",
                Header = "Runs Conceded",
                Width = 200,
                Margin = new Thickness(0, 10, 0, 10),
                TextAlignment = TextAlignment.Center
            };

            // Overs Bowled
            TextBox overs_bowled = new TextBox
            {
                Name = $"overs_bowled{currentTeamIndex}",
                Header = "Overs Bowled",
                Width = 200,
                Margin = new Thickness(0, 10, 0, 10),
                TextAlignment = TextAlignment.Center
            };

           
            Button calculate = new Button
            {
                Content = $"Calculate Team {currentTeamIndex}",
                Tag = new[] { runs_scored, overs_faced, runs_conceded, overs_bowled }

            };
            calculate.Click += TeamCalculate_Click;

            
            run_rate_pannel.Children.Add(runs_scored);
            run_rate_pannel.Children.Add(overs_faced);
            run_rate_pannel.Children.Add(runs_conceded);
            run_rate_pannel.Children.Add(overs_bowled);
            run_rate_pannel.Children.Add(calculate);
        }

        private double net_run_rate(int runs_scored, int overs_faced, int total_runs_con, int total_overs_bowled)
        {
            if (overs_faced <= 0 || total_overs_bowled <= 0)
            {
                return 0;
            }
            double net_Run_rate = ((double)runs_scored / overs_faced) - (double)total_runs_con / total_overs_bowled;
            //Console.WriteLine(net_Run_rate);
            return net_Run_rate;
        }

        private void TeamCalculate_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is TextBox[] inputs)
            {
                int runs = int.Parse(inputs[0].Text);
                int faced = int.Parse(inputs[1].Text);
                int conceded = int.Parse(inputs[2].Text);
                int bowled = int.Parse(inputs[3].Text);

                double result = net_run_rate(runs, faced, conceded, bowled);
                
                if (int.TryParse(teams.Text, out int teamNumber))
                Generate(teamNumber, result);
                currentTeamIndex++;
            }
        }

        private int Generate(int teamNumber, double run_rate)
        { 

            if (currentTeamIndex <= teamNumber)
            {
                TextBlock teamBox = new TextBlock
                {
                    Name = $"team{currentTeamIndex}TextBox",
                    Text = $"Team {currentTeamIndex} net run rate: {run_rate:F2}",
                    Width = 200,
                    Margin = new Thickness(0, 10, 100, 0)
                };
                teamNum.Children.Add(teamBox);
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}
