using System;
using System.Collections.Generic;
using System.Linq;
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

namespace Folsense.Views
{
    /// <summary>
    /// Interaction logic for DashboardView.xaml
    /// </summary>
    public partial class DashboardView : UserControl
    {
        public DashboardView()
        {
            InitializeComponent();

            Database.ItemContainerGenerator.ItemsChanged += ItemContainerGenerator_ItemsChanged;
        }

        private void ItemContainerGenerator_ItemsChanged(object sender, ItemsChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                // Get the newly added item container
                var newItemContainer = Database.ItemContainerGenerator.ContainerFromIndex(e.Position.Index) as UIElement;

                // Apply animation to the newly added item container
                if (newItemContainer != null)
                {
                    var storyboard = new Storyboard();
                    var fadeInAnimation = new DoubleAnimation
                    {
                        From = 0,
                        To = 1,
                        Duration = TimeSpan.FromSeconds(0.5)
                    };
                    Storyboard.SetTargetProperty(fadeInAnimation, new PropertyPath(OpacityProperty));
                    storyboard.Children.Add(fadeInAnimation);
                    newItemContainer.BeginAnimation(OpacityProperty, fadeInAnimation);
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                var removedItemContainer = Database.ItemContainerGenerator.ContainerFromIndex(e.Position.Index) as UIElement;

                // Apply animation to the removed item container
                if (removedItemContainer != null)
                {
                    var fadeOutStoryboard = new Storyboard();
                    var fadeOutAnimation = new DoubleAnimation
                    {
                        From = 1,
                        To = 0,
                        Duration = TimeSpan.FromSeconds(0.5)
                    };
                    fadeOutAnimation.Completed += (s, _) =>
                    {
                        // After animation completes, remove the item from UI
                        Database.Items.Remove(Database.Items[e.Position.Index]);
                    };
                    Storyboard.SetTargetProperty(fadeOutAnimation, new PropertyPath(OpacityProperty));
                    fadeOutStoryboard.Children.Add(fadeOutAnimation);
                    removedItemContainer.BeginAnimation(OpacityProperty, fadeOutAnimation);
                }
            }
        }
    }
}
